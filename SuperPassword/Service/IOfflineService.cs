using SuperPassword.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Service
{
    public interface IOfflineService
    {
        Task<PWDto> AddAsync(PWDto entity);

        Task<PWDto> UpdateAsync(PWDto entity);

        Task DeleteAsync(int id);
    }
}
