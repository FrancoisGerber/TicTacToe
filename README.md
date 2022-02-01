# TicTacToe
A simple Tic Tac Toe Online Web Implementation with vs AI or vs Player modes.

## Tech Used:
Database - MongoDB \
Backend - ASP.NET 6 Web API \
Frontend - Angular 13

## How to run locally:
UI - npm install then ng serve \
API - dotnet restore then dotnet run

## Hosting:
Docker Containers:
- Backend - RUN docker build -t tictactoeservice . 
- Backend - RUN docker run --publish 5000:5000 --detach --name tictactoeservice tictactoeservice:latest
- Frontend - add backend URL to config.json file 
- Frontend - RUN docker build -t tictactoewebapp . 
- Frontend - RUN docker run --publish 80:80 --detach --name tictactoewebapp tictactoewebapp:latest

## Live Demo
http://ec2-13-244-224-18.af-south-1.compute.amazonaws.com/
