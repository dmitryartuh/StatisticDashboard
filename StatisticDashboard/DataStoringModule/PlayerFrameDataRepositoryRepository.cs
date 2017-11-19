using System;
using System.Collections.Generic;
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
                    @"INSERT INTO PlayerFrameData_new
                        (Id, PlayerId, Json, DateTime, FrameId)
                    VALUES
                        (@Id, @PlayerId, @Json, @DateTime, @FrameId)",
                    playerFrameData);
            }
        }
        async Task<PlayerFrameData> IPlayerFrameDataRepository.GetLastFrameForPlayerAsync(Guid playerId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                try
                {
                    return (await db.QueryAsync<PlayerFrameData>(
                        @"SELECT TOP(1)* FROM PlayerFrameData_new
                        WHERE PlayerId = @playerId
                        ORDER BY DateTime desc", new
                        {
                            playerId
                        }))?.FirstOrDefault();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
		
		async Task<IEnumerable<PlayerFrameData>> IPlayerFrameDataRepository.GetAllAsync()
        {
            using (var db = new SqlConnection(_connectionString))
            {
				return await db.QueryAsync<PlayerFrameData>(
                    @"SELECT * FROM PlayerFrameData_new
					ORDER BY DateTime desc");
            }
        }
		
		async Task<IEnumerable<PlayerFrame>> IPlayerFrameDataRepository.GetAllFramesAsync()
        {
            using (var db = new SqlConnection(_connectionString))
            {
				return await db.QueryAsync<PlayerFrame>(
                    @"SELECT PlayerFrameData_new.*,Player.Nickname, Player.Clan FROM PlayerFrameData_new
                        JOIN Player on Player.Id = PlayerFrameData_new.PlayerId
					ORDER BY DateTime desc");
            }
        }
    }
}