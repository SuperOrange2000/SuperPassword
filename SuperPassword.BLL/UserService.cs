using SuperPassword.DAL;
using SuperPassword.Entity;
using SuperPassword.Security.SecurityModule;
using SuperPassword.Security.Sercvice;
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
        private readonly ISecurityService _securityService;

        public UserService(IUserServiceDAL userDAL, ISecurityService securityService)
        {
            _userServiceDAL = userDAL;
            _securityService = securityService;
        }

        public async Task<ResponseBLL<string>> SignUp(UserEntity user)
        {
            ResponseDAL responseDAL = await _userServiceDAL.SignUp(user);
            return Deserialize<string>(responseDAL);
        }

        public async Task<ResponseBLL<string>> Login(UserEntity user)
        {
            ResponseDAL responseDAL = await _userServiceDAL.Login(user);
            _securityService.SwitchCipher<ChaCha20>(user.Key);
            return Deserialize<string>(responseDAL);
        }
    }
}
