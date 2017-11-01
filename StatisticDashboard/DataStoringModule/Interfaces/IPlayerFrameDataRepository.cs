using System.Threading.Tasks;
using Models.Entities;

namespace DataStoringModule.Interfaces
{
    public interface IPlayerFrameDataRepository
    {
        Task InsertAsync(PlayerFrameData playerFrameData);
    }
}