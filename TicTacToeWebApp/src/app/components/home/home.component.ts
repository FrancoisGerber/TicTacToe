import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Game } from 'src/app/models/Game';
import { GameService } from 'src/app/services/game-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  activeGame: Game;
  gameModes: string[] = ["Offline Player", "AI", "Online Player"];
  currentGameMode: string = "Offline Player";

  constructor(private gameService: GameService, private route: ActivatedRoute, private router: Router) { }

  async ngOnInit(mustReload?: boolean) {
    //check if active game

    if (this.currentGameMode != "Online Player") {
      let route = this.router.parseUrl(this.router.url).queryParams["id"];

      if (mustReload) {

        this.activeGame = {
          createdDate: new Date(),
          gameMode: this.currentGameMode,
          playerXId: "",
          playerOId: "",
          playedPositions: [],
          playerOHistory: [],
          playerXHistory: [],
          completed: false
        }
        this.activeGame = await this.gameService.Post(this.activeGame);

        this.router.navigate([], {
          relativeTo: this.route,
          queryParams: {
            id: this.activeGame.id
          },
          skipLocationChange: false
        });
      }
      else if (route != null) {
        this.activeGame = null;
        this.activeGame = await this.gameService.Get(route);
        this.currentGameMode = this.activeGame.gameMode;
      }
    }
  }

  async StartNewGame() {
    this.ngOnInit(true);
  }

  changeGameMode() {
    let currentIndex = this.gameModes.indexOf(this.currentGameMode);
    if (currentIndex == 2)
      currentIndex = 0;
    else
      currentIndex++;

    this.currentGameMode = this.gameModes[currentIndex];
  }

  reloadBoard(event) {
    this.ngOnInit(false);
  }

}
