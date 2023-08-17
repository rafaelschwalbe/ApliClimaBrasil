using Dapper;
using Microsoft.Data.Sqlite;
using WebApplicationApiClime.Sqlite.Repositories.Interfaces;

namespace WebApplicationApiClime.Sqlite.Repositories
{
    public class ClimaAeroportoRepository : IClimaAeroportoRepository
    {
        private readonly DatabaseConfig DatabaseConfig;

        public ClimaAeroportoRepository(DatabaseConfig databaseConfig)
        {
            DatabaseConfig = databaseConfig;
        }

        public void InsertClimaAeroporto(ClimaAeroporto climaAeroporto)
        {
            using var connection = new SqliteConnection(DatabaseConfig.Name);
            string query = $"INSERT INTO climaaeroporto(id, umidade, visibilidade, codigo_icao, pressao_atmosferica, vento, direcao_vento, condicao, condicao_desc, temp, atualizado_em) " +
                $"VALUES('{Guid.NewGuid().ToString()}', " +
                $"'{climaAeroporto.Umidade}', " +
                $"'{climaAeroporto.Visibilidade}', " +
                $"'{climaAeroporto.Codigo_Icao}', " +
                $"'{climaAeroporto.Pressao_Atmosferica}', " +
                $"'{climaAeroporto.Vento}', " +
                $"'{climaAeroporto.Direcao_Vento}', " +
                $"'{climaAeroporto.Condicao}', " +
                $"'{climaAeroporto.Condicao_Desc}', " +
                $"'{climaAeroporto.Temp}', " +
                $"'{climaAeroporto.Atualizado_Em}');";
            connection.Execute(query);
        }
    }
}
