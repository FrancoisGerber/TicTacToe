import { PlayerHistory } from "./PlayerHistory";

export interface Game {
    id?: string;
    createdDate: Date;
    gameMode: string;
    playerXId?: string;
    playerOId?: string;
    playerXHistory?: PlayerHistory[];
    playerOHistory?: PlayerHistory[];
    playedPositions?: number[];
    winner?: string;
    completed?: boolean;
}
