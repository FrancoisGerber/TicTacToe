import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Game } from 'src/app/models/Game';
import { GameService } from 'src/app/services/game-service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {

  grid: number[] = [];
  currentPlayer: string = "X";
  @Input() activeGame: Game;
  @Output() reload = new EventEmitter<any>();

  constructor(private gameService: GameService) {
    this.grid = Array(3).fill(0).map((x, i) => i);

  }

  ngOnInit(): void {

    let xCount = this.activeGame.playerXHistory.length;
    let oCount = this.activeGame.playerOHistory.length;
    if (xCount > oCount)
      this.currentPlayer = "O";
    else
      this.currentPlayer = "X";

  }

  async moveMade(event) {

    this.activeGame.playedPositions.push(event.pieceName);
    this.activeGame = await this.gameService.Put(this.activeGame.id, this.activeGame);

    let newMove = {
      gameID: this.activeGame.id,
      activePlayer: event.playerSymbol,
      move: this.CalculateAxis(event.pieceName)
    }

    this.activeGame = await this.gameService.MakeMove(newMove);

    if (event.playerSymbol == "O")
      this.currentPlayer = "X";
    else
      this.currentPlayer = "O";

    if (this.activeGame.gameMode == "AI")
      this.reload.emit(true);

  }

  CalculateAxis(position: number) {
    let axis = {};
    let yAxis;

    if (position == 1 || position == 4 || position == 7)
      yAxis = "A";
    else if (position == 2 || position == 5 || position == 8)
      yAxis = "B";
    else if (position == 3 || position == 6 || position == 9)
      yAxis = "C";

    switch (position) {
      case 1:
      case 2:
      case 3:
        axis = {
          yAxis: yAxis,
          xAxis: 1
        }
        break;

      case 4:
      case 5:
      case 6:
        axis = {
          yAxis: yAxis,
          xAxis: 2
        }
        break;

      case 7:
      case 8:
      case 9:
        axis = {
          yAxis: yAxis,
          xAxis: 3
        }
        break;
    }
    return axis;
  }

}
