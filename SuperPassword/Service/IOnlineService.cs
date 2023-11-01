using SuperPassword.Shared;
using SuperPassword.Shared.Contact;
using SuperPassword.Shared.Dtos;
using SuperPassword.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        Task<ApiResponse<string>> SignUp(User user);
    }
}
