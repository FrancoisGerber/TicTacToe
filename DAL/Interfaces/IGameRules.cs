using DAL.Models;

namespace DAL.Interfaces
{
    public interface IGameRules
    {
        public Game CheckGame(string GameID, char ActivePLayer);
        public void AIMove(string gameID, char activePlayer);
    }
}