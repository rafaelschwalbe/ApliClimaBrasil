using Dapper;
using Microsoft.Data.Sqlite;
using WebApplicationApiClime.Sqlite.Repositories.Interfaces;

namespace WebApplicationApiClime.Sqlite.Repositories
{
    public class ClimaCidadeRepository : IClimaCidadeRepository
    {
        private readonly DatabaseConfig DatabaseConfig;

        public ClimaCidadeRepository(DatabaseConfig databaseConfig)
        {
            DatabaseConfig = databaseConfig;
        }

        public void InsertClimaCidade(ClimaCidade climaCidade)
        {
            using var connection = new SqliteConnection(DatabaseConfig.Name);
            string query = $"INSERT INTO climacidade(id, cidade, estado, atualizado_em, data, condicao, condicao_desc, min, max, indice_uv) " +
                $"VALUES('{Guid.NewGuid().ToString()}', " +
                $"'{climaCidade.Cidade}', " +
                $"'{climaCidade.Estado}', " +
                $"'{climaCidade.Atualizado_Em}', " +
                $"'{climaCidade.Clima.First().Data}', " +
                $"'{climaCidade.Clima.First().Condicao}', " +
                $"'{climaCidade.Clima.First().Condicao_Desc}', " +
                $"'{climaCidade.Clima.First().Min}', " +
                $"'{climaCidade.Clima.First().Max}', " +
                $"'{climaCidade.Clima.First().Indice_Uv}');";
            connection.Execute(query);
        }
    }
}
