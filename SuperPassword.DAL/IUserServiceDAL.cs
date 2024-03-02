using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperPassword.Entity;

namespace SuperPassword.DAL
{
    public interface IUserServiceDAL
    {
        Task<ResponseDAL> SignUp(UserEntity user);

        Task<ResponseDAL> Login(UserEntity user);
    }
}
