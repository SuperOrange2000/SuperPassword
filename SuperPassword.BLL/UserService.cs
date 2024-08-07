using SuperPassword.Config.Service;
using SuperPassword.DAL;
using SuperPassword.Entity.Interface;
using SuperPassword.Security.Sercvice;
using SuperPassword.DAL.Models;
using SuperPassword.BLL.Models;

namespace SuperPassword.BLL
{
    public class UserService : JsonDeserialization, IUserServiceBLL
    {
        private readonly IUserServiceDAL _userServiceDAL;
        private readonly ISecurityService _securityService;
        private readonly IConfigService _configService;

        public UserService(IUserServiceDAL userDAL, ISecurityService securityService, IConfigService configService)
        {
            _userServiceDAL = userDAL;
            _securityService = securityService;
            _configService = configService;
        }

        public async Task<ResponseBLL<string>> SignUp(IUser user)
        {
            ResponseDAL responseDAL = await _userServiceDAL.SignUp(user);
            return Deserialize<string>(responseDAL);
        }

        public async Task<ResponseBLL<string>> Login(IUser user)
        {
            ResponseDAL responseDAL = await _userServiceDAL.Login(user);
            return Deserialize<string>(responseDAL);
        }
    }
}
