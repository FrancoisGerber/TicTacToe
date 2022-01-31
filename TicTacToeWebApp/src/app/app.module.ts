import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

//i maed dis
import { HomeComponent } from './components/home/home.component';
import { BoardComponent } from './components/home/board/board.component';
import { PieceComponent } from './components/home/piece/piece.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    BoardComponent,
    PieceComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
