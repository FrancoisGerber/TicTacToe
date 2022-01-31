import { Component, NgZone, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Game } from 'src/app/models/Game';
import { GameService } from 'src/app/services/game-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {

  activeGame: Game;
  gameModes: string[] = ["Offline Player", "AI", "Online Player"];
  currentGameMode: string = "Offline Player";
  SSESource: EventSource;

  constructor(private gameService: GameService, private route: ActivatedRoute, private router: Router, private zone: NgZone) { }

  async ngOnInit() {
    this.ngOnDestroy();
    await this.LoadGame();

    let route = this.router.parseUrl(this.router.url).queryParams["player"];
    if (route != null) {
      this.SSESource = this.gameService.GetLive("Online");
      let me = this;
      this.SSESource.onmessage = function (event) {
        me.zone.run(() => {
          if (event.data != "Connected!") {
            me.LoadGame();
          }
        });
      }
    }
  }

  async LoadGame() {
    let routeID = this.router.parseUrl(this.router.url).queryParams["id"];
    if (routeID != null) {
      this.activeGame = null;
      this.activeGame = await this.gameService.Get(routeID);
      this.currentGameMode = this.activeGame.gameMode;
    }
    else {
      let route = this.router.parseUrl(this.router.url).queryParams["player"];
      if (route != null) {
        this.activeGame = null;
        this.activeGame = await this.gameService.GetPlayerRoom(route);
        this.currentGameMode = this.activeGame.gameMode;
      }
    }
  }

  async StartNewGame() {

    this.activeGame = {
      createdDate: new Date(),
      gameMode: this.currentGameMode,
      playedPositions: [],
      playerOHistory: [],
      playerXHistory: [],
      completed: false
    }
    this.activeGame = await this.gameService.Post(this.activeGame);

    if (this.currentGameMode == "Online Player")
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: {
          player: "Online"
        },
        skipLocationChange: false
      });
    else
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: {
          id: this.activeGame.id
        },
        skipLocationChange: false
      });
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
    this.LoadGame();
  }

  ngOnDestroy() {
    if (this.SSESource != null) {
      this.SSESource.close();
      this.SSESource = null;
    }
  }

}
