import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-piece',
  templateUrl: './piece.component.html',
  styleUrls: ['./piece.component.css']
})
export class PieceComponent implements OnInit {

  @Input() pieceName: number;
  @Input() playerSymbol: string;

  @Output() pieceClicked = new EventEmitter<string>();

  active: boolean = false;
  symbol: string;

  constructor() { }

  ngOnInit(): void {
  }

  makeMove() {
    this.active = true;
    this.symbol = this.playerSymbol;
    this.pieceClicked.emit(this.playerSymbol);
  }


}
