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

  constructor(private gameService: GameService, private route: ActivatedRoute, private router: Router) { }

  async ngOnInit(mustReload?: boolean) {
    //check if active game
    
    let route = this.router.parseUrl(this.router.url).queryParams["id"];

    if (route == null || mustReload) {

      this.activeGame = {
        createdDate: new Date(),
        gameMode: "AI", //PLAYER
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
    else {
      this.activeGame = await this.gameService.Get(route);
      
    }
  }

  async StartNewGame() {
    this.ngOnInit(true);
  }

}
