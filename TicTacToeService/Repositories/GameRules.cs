using DAL.Interfaces;
using DAL.Repositories;
using DAL.Models;
using MongoDB.Driver;
using TicTacToeService.Controllers;

namespace TicTacToeService.Repositories
{
    public class GameRules : IGameRules
    {
        private GameDataRepository db;

        public GameRules(GameDataRepository Database)
        {
            db = Database;
        }

        public Game CheckGame(string GameID, char ActivePlayer)
        {
            //Get Active Game
            Game activeGame = db.Games.SingleOrDefault(c => c.Id == GameID);

            //Get Active Player History
            List<PlayerHistory> playerHistory = ActivePlayer == 'X' ? activeGame.PlayerXHistory : activeGame.PlayerOHistory;

            //Check for Diagonal
            if (CheckDiagonal(playerHistory))
            {
                CompleteGame(activeGame, ActivePlayer);
                return activeGame;
            }
            //Check for Horizontal
            else if (CheckHorizontal(playerHistory))
            {
                CompleteGame(activeGame, ActivePlayer);
                return activeGame;
            }
            //Check for Vertical
            else if (CheckVertical(playerHistory))
            {
                CompleteGame(activeGame, ActivePlayer);
                return activeGame;
            }

            return activeGame;
        }

        private bool CheckDiagonal(List<PlayerHistory> PlayerHistory)
        {
            //Check for Diagonal = 1 of each number and 1 of each letter
            bool VertialTrue = false;
            bool HorizontalTrue = false;

            if (PlayerHistory.Exists(c => c.XAxis == 1))
                if (PlayerHistory.Exists(c => c.XAxis == 2))
                    if (PlayerHistory.Exists(c => c.XAxis == 3))
                        VertialTrue = true;

            if (PlayerHistory.Exists(c => c.YAxis == "A"))
                if (PlayerHistory.Exists(c => c.YAxis == "B"))
                    if (PlayerHistory.Exists(c => c.YAxis == "C"))
                        HorizontalTrue = true;

            return VertialTrue && HorizontalTrue ? true : false;
        }

        private bool CheckHorizontal(List<PlayerHistory> PlayerHistory)
        {
            //Check for Horizontal = 3 of same number
            foreach (var line in PlayerHistory)
            {
                int count = PlayerHistory.Where(c => c.XAxis == line.XAxis).Count();
                if (count == 3)
                    return true;
            }
            return false;
        }

        private bool CheckVertical(List<PlayerHistory> PlayerHistory)
        {
            //Check for Vertical = 1 of each number
            if (PlayerHistory.Exists(c => c.XAxis == 1))
                if (PlayerHistory.Exists(c => c.XAxis == 2))
                    if (PlayerHistory.Exists(c => c.XAxis == 3))
                        return true;

            return false;
        }

        private void CompleteGame(Game activeGame, char ActivePlayer)
        {
            //If winner found save result in DB and return game save file
            activeGame.Completed = true;
            activeGame.Winner = ActivePlayer;

            var filter = Builders<Game>.Filter.Eq(x => x.Id, activeGame.Id);
            db.Games.Complete(filter, activeGame);
        }
    }
}