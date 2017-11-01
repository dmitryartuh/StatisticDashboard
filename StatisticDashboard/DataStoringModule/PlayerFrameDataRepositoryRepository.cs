using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DataStoringModule.Interfaces;
using Models.Entities;

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
                    @"INSERT INRO PlayerFrameData (Id, PlayerId, Json, DateTime) VALUES(@Id, @PlayerId, @Json, @DateTime)",
                    playerFrameData);
            }
        }
    }
}