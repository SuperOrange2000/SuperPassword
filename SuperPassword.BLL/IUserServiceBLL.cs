using SuperPassword.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.BLL
{
    public interface IUserServiceBLL
    {
        Task<ResponseBLL<string>> SignUp(UserEntity user);

        Task<ResponseBLL<string>> Login(UserEntity user);
    }
}
