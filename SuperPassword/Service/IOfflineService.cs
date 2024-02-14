using SuperPassword.Shared.DTOs;
using System.Threading.Tasks;

namespace SuperPassword.Service
{
    public interface IOfflineService
    {
        Task<InfoGroupDTO> AddAsync(InfoGroupDTO entity);

        Task<InfoGroupDTO> UpdateAsync(InfoGroupDTO entity);

        Task DeleteAsync(string id);
    }
}
