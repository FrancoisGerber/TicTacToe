using DAL.Models;

namespace DAL.Interfaces
{
    public interface IGameRules
    {
        public Game CheckGame(string GameID, char ActivePLayer);
    }
}