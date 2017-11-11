using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DataStoringModule.Interfaces;
using Models.Entities;
using System.Linq;

namespace DataStoringModule
{
    public class PlayerFrameDataRepositoryRepository : IPlayerFrameDataRepository
    {
        private readonly string _connectionString;

        public PlayerFrameDataRepositoryRepository(string conn)
        {
            _connectionString = conn;
        }

        async Task IPlayerFrameDataRepository.InsertAsync(PlayerFrameData playerFrameData)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(
                    @"INSERT INTO PlayerFrameData
                        (Id, PlayerId, Json, DateTime, FrameId)
                    VALUES
                        (@Id, @PlayerId, @Json, @DateTime, @FrameId)",
                    playerFrameData);
            }
        }
        async Task<PlayerFrameData> IPlayerFrameDataRepository.GetLastFrameForPlayer(Guid playerId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                return (await db.QueryAsync<PlayerFrameData>(
                    @"SELECT TOP(1)* FROM PlayerFrameData
                        WHERE PlayerId = @payerId
                        ORDER BY DateTime")).FirstOrDefault();
            }
        }
    }
}