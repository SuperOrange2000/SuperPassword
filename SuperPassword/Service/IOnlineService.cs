using SuperPassword.Shared.Contact;
using SuperPassword.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperPassword.Service
{
    public interface IOnlineService
    {
        Task<ApiResponse<object>> AddAsync(InfoGroupDTO entity);

        Task<ApiResponse<InfoGroupDTO>> UpdateAsync(InfoGroupDTO entity);

        Task<ApiResponse<object>> DeleteAsync(string id);

        Task<ApiResponse<InfoGroupDTO>> GetFirstOfDefaultAsync(string id);

        Task<ApiResponse<List<InfoGroupDTO>>> GetAllAsync();

        Task<ApiResponse<string>> SignUp(UserDto user, string password);

        Task<ApiResponse<string>> Login(UserDto user, string password);
    }
}
