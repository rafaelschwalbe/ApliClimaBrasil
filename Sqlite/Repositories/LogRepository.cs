using Dapper;
using Microsoft.Data.Sqlite;
using WebApplicationApiClime.Sqlite.Repositories.Interfaces;

namespace WebApplicationApiClime.Sqlite.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly DatabaseConfig DatabaseConfig;

        public LogRepository(DatabaseConfig databaseConfig)
        {
            DatabaseConfig = databaseConfig;
        }

        public void InsertLog(Log log)
        {
            using var connection = new SqliteConnection(DatabaseConfig.Name);
            string query = $"INSERT INTO log(id, rota, parametro, erro) " +
                $"VALUES('{Guid.NewGuid().ToString()}', '{log.Rota}', '{log.Parametro}', '{log.Erro.Replace('\'', '"')}');";
            connection.Execute(query);
        }
    }
}
