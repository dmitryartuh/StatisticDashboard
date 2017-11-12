using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;

namespace DataStoringModule.Interfaces
{
    public interface IPlayerFrameDataRepository
    {
        Task InsertAsync(PlayerFrameData playerFrameData);
        Task<PlayerFrameData> GetLastFrameForPlayerAsync(Guid playerId);
        Task<IEnumerable<PlayerFrameData>> GetAllAsync();
        Task<IEnumerable<PlayerFrame>> GetAllFramesAsync();
    }
}