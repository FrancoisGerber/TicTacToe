using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToeService.Controllers
{
    public class BaseController : ControllerBase
    {
        internal static string ConnectionString = "mongodb://localhost:27017";

        internal static string DatabaseName = "TicTacToe";

        internal GameDataRepository db = new GameDataRepository(ConnectionString, DatabaseName);

        public BaseController()
        {
        }

        public BaseController(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }
    }
}