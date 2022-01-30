import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {

  grid: number[] = [];
  currentPlayer: string = "X";

  constructor() {
    this.grid = Array(3).fill(0).map((x, i) => i);

  }

  ngOnInit(): void {
  }

  moveMade(event) {
    if (event == "O")
      this.currentPlayer = "X";
    else
      this.currentPlayer = "O";
  }

}
