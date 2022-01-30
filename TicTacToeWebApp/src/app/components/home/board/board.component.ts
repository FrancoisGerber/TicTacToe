import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {

  grid: number[] = [];

  constructor() {
    this.grid = Array(3).fill(0).map((x, i) => i);

  }

  ngOnInit(): void {
  }

}
