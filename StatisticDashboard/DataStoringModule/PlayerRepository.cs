using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataStoringModule.Interfaces;
using Models.DtoModels;
using Models.Entities;

namespace DataStoringModule
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly string _connectionString;

        public PlayerRepository(string conn)
        {
            _connectionString = conn;
        }

        async Task<IEnumerable<Player>> IPlayerRepository.GetAllAsync()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<Player>("SELECT * FROM Player");
            }
        }

        async Task IPlayerRepository.AddAsync(Player model)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(
                    @"INSERT INTO Player (Id, WotId, Nickname, Lang, Clan) VALUES(@Id, @WotId, @Nickname, @Lang, @Clan)",
                    model);
            }
        }
    }
}