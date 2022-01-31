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

            if (activeGame.PlayedPositions.Count() >= 9)
            {
                CompleteGame(activeGame, '-');
                return activeGame;
            }

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
            if ((PlayerHistory.Exists(c => c.YAxis == "A" && c.XAxis == 1) && PlayerHistory.Exists(c => c.YAxis == "B" && c.XAxis == 2) && PlayerHistory.Exists(c => c.YAxis == "C" && c.XAxis == 3)) ||
            (PlayerHistory.Exists(c => c.YAxis == "C" && c.XAxis == 1) && PlayerHistory.Exists(c => c.YAxis == "B" && c.XAxis == 2) && PlayerHistory.Exists(c => c.YAxis == "A" && c.XAxis == 3)))
                return true;
            else
                return false;
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
            bool numberTrue = false;
            bool letterTrue = false;

            //Check for Vertical = 1 of each number and same letter
            if (PlayerHistory.Exists(c => c.XAxis == 1))
                if (PlayerHistory.Exists(c => c.XAxis == 2))
                    if (PlayerHistory.Exists(c => c.XAxis == 3))
                        numberTrue = true;

            if (numberTrue)
            {
                var allPositions = PlayerHistory.Where(c => c.XAxis == 1).ToList();
                foreach (var item in allPositions)
                {
                    var y1 = PlayerHistory.Exists(c => c.XAxis == 1 && c.YAxis == item.YAxis) ? PlayerHistory.FirstOrDefault(c => c.XAxis == 1 && c.YAxis == item.YAxis).YAxis : null;
                    var y2 = PlayerHistory.Exists(c => c.XAxis == 2 && c.YAxis == item.YAxis) ? PlayerHistory.FirstOrDefault(c => c.XAxis == 2 && c.YAxis == item.YAxis).YAxis : null;
                    var y3 = PlayerHistory.Exists(c => c.XAxis == 3 && c.YAxis == item.YAxis) ? PlayerHistory.FirstOrDefault(c => c.XAxis == 3 && c.YAxis == item.YAxis).YAxis : null;

                    if (y1 == null)
                        break;
                    if (y2 == null)
                        break;
                    if (y3 == null)
                        break;

                    if (y1 == y2 && y2 == y3 && y3 == y1)
                        letterTrue = true;
                }
            }


            return numberTrue && letterTrue ? true : false;

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