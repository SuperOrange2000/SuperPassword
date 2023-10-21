using SuperPassword.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Service
{
    public interface IOfflineService
    {
        Task<InfoGroupDTO> AddAsync(InfoGroupDTO entity);

        Task<InfoGroupDTO> UpdateAsync(InfoGroupDTO entity);

        Task DeleteAsync(long id);
    }
}
