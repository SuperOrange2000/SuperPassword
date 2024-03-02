using SuperPassword.Entity;
using SuperPassword.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.BLL
{
    public interface IDataServiceBLL
    {
        Task<ResponseBLL<object>> AddAsync(UserEntity user, InfoGroupEntity entity);

        Task<ResponseBLL<InfoGroupEntity>> UpdateAsync(UserEntity user, InfoGroupEntity entity);

        Task<ResponseBLL<object>> DeleteAsync(UserEntity user, uint id);

        Task<ResponseBLL<InfoGroupEntity>> GetFirstOfDefaultAsync(UserEntity user, uint id);

        Task<ResponseBLL<List<InfoGroupEntity>>> GetAllAsync(UserEntity user);
    }
}
