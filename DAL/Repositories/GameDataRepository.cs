using DAL.Database;
using DAL.Models;

namespace DAL.Repositories
{
    public class GameDataRepository
    {

        public IEntityRepository<Game> Games { get; set; }
        public IEntityRepository<Player> Players { get; set; }
        public IEntityRepository<AuditLog> AuditLogs { get; set; }
        public IEntityRepository<ErrorLog> ErrorLogs { get; set; }

        public GameDataRepository(string connectionString, string databaseName)
        {
            Games = new MongoEntityRepository<Game>(connectionString, databaseName, "Games");
            Players = new MongoEntityRepository<Player>(connectionString, databaseName, "Players");
            AuditLogs = new MongoEntityRepository<AuditLog>(connectionString, databaseName, "AuditLogs");
            ErrorLogs = new MongoEntityRepository<ErrorLog>(connectionString, databaseName, "ErrorLogs");
        }
    }
}