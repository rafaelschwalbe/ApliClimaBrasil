using Dapper;
using Microsoft.Data.Sqlite;

namespace WebApplicationApiClime.Sqlite
{
    public class DatabaseBootstrap : IDatabaseBootstrap
    {
        private readonly DatabaseConfig databaseConfig;

        public DatabaseBootstrap(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public void Setup()
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND (name = 'climacidade' or name = 'climaaeroporto' or name = 'log');");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && (tableName == "climacidade" || tableName == "climaaeroporto" || tableName == "log"))
                return;

            connection.Execute("CREATE TABLE climacidade ( " +
                               "id TEXT(37) PRIMARY KEY," +
                               "cidade TEXT(100)," +
                               "estado TEXT(2)," +
                               "atualizado_em TEXT(25)," +
                               "data TEXT(25)," +
                               "condicao TEXT(10)," +
                               "condicao_desc TEXT(100)," +
                               "min INTEGER(4)," +
                               "max INTEGER(4)," +
                               "indice_uv INTEGER(4)" +
                               ");");

            connection.Execute("CREATE TABLE climaaeroporto ( " +
                               "id TEXT(37) PRIMARY KEY," +
                               "umidade INTEGER(5)," +
                               "visibilidade TEXT(25)," +
                               "codigo_icao TEXT(4)," +
                               "pressao_atmosferica INTEGER(5)," +
                               "vento INTEGER(5)," +
                               "direcao_vento INTEGER(5)," +
                               "condicao TEXT(5)," +
                               "condicao_desc TEXT(100)," +
                               "temp INTEGER(5)," +
                               "atualizado_em TEXT(25)" +
                               ");");

            connection.Execute("CREATE TABLE log (" +
                               "id TEXT(37) PRIMARY KEY," +
                               "rota TEXT(100)," +
                               "parametro TEXT(100)," +
                               "erro TEXT(1000));");            
        }
    }
}
