using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;

namespace DataStoringModule.Interfaces
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAllAsync();
        Task AddAsync(Player model);
    }
}