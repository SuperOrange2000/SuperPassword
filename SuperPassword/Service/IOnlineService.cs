using SuperPassword.Shared.Contact;
using SuperPassword.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperPassword.Service
{
    public interface IOnlineService
    {
        Task<ApiResponse<object>> AddAsync(UserDto user, InfoGroupDTO entity);

        Task<ApiResponse<InfoGroupDTO>> UpdateAsync(UserDto user, InfoGroupDTO entity);

        Task<ApiResponse<object>> DeleteAsync(UserDto user, uint id);

        Task<ApiResponse<InfoGroupDTO>> GetFirstOfDefaultAsync(UserDto user, uint id);

        Task<ApiResponse<List<InfoGroupDTO>>> GetAllAsync(UserDto user);

        Task<ApiResponse<string>> SignUp(UserDto user, string password);

        Task<ApiResponse<string>> Login(UserDto user, string password);
    }
}
