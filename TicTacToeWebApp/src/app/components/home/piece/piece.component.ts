import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Game } from 'src/app/models/Game';

@Component({
  selector: 'app-piece',
  templateUrl: './piece.component.html',
  styleUrls: ['./piece.component.css']
})
export class PieceComponent implements OnInit {

  @Input() pieceName: number;
  @Input() playerSymbol: string;
  @Input() activeGame: Game;

  @Output() pieceClicked = new EventEmitter<any>();

  active: boolean = false;
  symbol: string;

  constructor() { }

  ngOnInit(): void {
    

    if (this.activeGame.playedPositions.indexOf(this.pieceName) >= 0) {
      for (let index = 0; index < this.activeGame.playerXHistory.length; index++) {
        const move = this.activeGame.playerXHistory[index];
        if (this.CalculateAxis(move.xAxis, move.yAxis) == this.pieceName) {
          this.active = true;
          this.symbol = "X";
          break;
        }
      }

      for (let index = 0; index < this.activeGame.playerOHistory.length; index++) {
        const move = this.activeGame.playerOHistory[index];
        if (this.CalculateAxis(move.xAxis, move.yAxis) == this.pieceName) {
          this.active = true;
          this.symbol = "O";
          break;
        }
      }
    }
  }

  makeMove() {
    if (this.activeGame.completed != true && this.activeGame.playedPositions.indexOf(this.pieceName) < 0) {
      this.active = true;
      this.symbol = this.playerSymbol;
      let eventData = {
        playerSymbol: this.playerSymbol,
        pieceName: this.pieceName
      }
      this.pieceClicked.emit(eventData);
    }
  }

  CalculateAxis(xAxis: number, yAxis: string): number {
    let position = 0;
    let line = xAxis * 3;

    if (yAxis == "A")
      position = line - 2;
    else if (yAxis == "B")
      position = line - 1;
    else if (yAxis == "C")
      position = line;

    return position;
  }
}
