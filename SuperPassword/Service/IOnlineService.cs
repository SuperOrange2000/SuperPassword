using SuperPassword.Shared.Contact;
using SuperPassword.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperPassword.Service
{
    public interface IOnlineService
    {
        Task<ApiResponse<object>> AddAsync(UserDTO user, InfoGroupDTO entity);

        Task<ApiResponse<InfoGroupDTO>> UpdateAsync(UserDTO user, InfoGroupDTO entity);

        Task<ApiResponse<object>> DeleteAsync(UserDTO user, uint id);

        Task<ApiResponse<InfoGroupDTO>> GetFirstOfDefaultAsync(UserDTO user, uint id);

        Task<ApiResponse<List<InfoGroupDTO>>> GetAllAsync(UserDTO user);

        Task<ApiResponse<string>> SignUp(UserDTO user, string password);

        Task<ApiResponse<string>> Login(UserDTO user, string password);
    }
}
