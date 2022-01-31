import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base-service';
import { Game } from '../models/Game';
import config from '../../../config.json';

@Injectable({
    providedIn: 'root'
})
export class GameService extends BaseService<Game> {

    constructor(http: HttpClient) {
        super(http, "Games")
    }


    public async MakeMove(move: any): Promise<Game> {
        return await this.http.post<Game>(config.BaseURL + this.Service + "/MakeMove", move, this.httpOptions()).toPromise();
    }
}
