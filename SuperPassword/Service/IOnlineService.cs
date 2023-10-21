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
        Task<ApiResponse<InfoGroupDTO>> AddAsync(InfoGroupDTO entity);

        Task<ApiResponse<InfoGroupDTO>> UpdateAsync(InfoGroupDTO entity);

        Task<ApiResponse> DeleteAsync(int id);

        Task<ApiResponse<InfoGroupDTO>> GetFirstOfDefaultAsync(int id);

        Task<ApiResponse<PagedList<InfoGroupDTO>>> GetAllAsync(QueryParameter parameter);
    }
}
