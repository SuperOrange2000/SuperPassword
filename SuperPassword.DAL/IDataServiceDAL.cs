using SuperPassword.Entity;
using SuperPassword.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.DAL
{
    public interface IDataServiceDAL
    {
        Task<ResponseDAL> AddAsync(UserEntity user, InfoGroupEntity entity);

        Task<ResponseDAL> UpdateAsync(UserEntity user, InfoGroupEntity entity);

        Task<ResponseDAL> DeleteAsync(UserEntity user, uint id);

        Task<ResponseDAL> GetFirstOfDefaultAsync(UserEntity user, uint id);

        Task<ResponseDAL> GetAllAsync(UserEntity user);
    }
}
