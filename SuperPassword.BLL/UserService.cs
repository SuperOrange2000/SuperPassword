using SuperPassword.DAL;
using SuperPassword.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.BLL
{
    public class UserService : JsonDeserialization, IUserServiceBLL
    {
        private readonly IUserServiceDAL _userServiceDAL;

        public UserService(IUserServiceDAL userDAL)
        {
            _userServiceDAL = userDAL;
        }

        public async Task<ResponseBLL<string>> SignUp(UserEntity user)
        {
            ResponseDAL responseDAL = await _userServiceDAL.SignUp(user);
            return Deserialize<string>(responseDAL);
        }

        public async Task<ResponseBLL<string>> Login(UserEntity user)
        {
            ResponseDAL responseDAL = await _userServiceDAL.Login(user);
            return Deserialize<string>(responseDAL);
        }
    }
}
