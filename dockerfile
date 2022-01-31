FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
COPY ./TicTacToeService /app/TicTacToeService
COPY ./DAL /app/DAL
WORKDIR /app/TicTacToeService

RUN dotnet restore TicTacToeService.csproj
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app/TicTacToeService
COPY --from=build-env /app/TicTacToeService/out .
ENTRYPOINT ["dotnet", "TicTacToeService.dll"]
EXPOSE 5000