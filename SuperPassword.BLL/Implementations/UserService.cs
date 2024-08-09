using SuperPassword.BLL.Implementations.Models;
using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Implementations
{
    public partial class BLLService
    {
        public async Task<IBLLResponse> SignUp(IUser user)
        {
            IDALResponse responseDAL = await DALService.SignUp(user.Name, user.Password);
            var result = Deserialize<string>(responseDAL);
            if (result.Status == ResponseStatus.Success && result.Content != null)
            {
                activeUser = new BLLUser(user);
                activeUser.Token = result.Content;
            }
            return new BLLResponse() { Status = result.Status, Message = result.Message };
        }

        public async Task<IBLLResponse> Login(IUser user)
        {
            IDALResponse responseDAL = await DALService.Login(user.Name, user.Password);
            var result = Deserialize<string>(responseDAL);
            if (result.Status == ResponseStatus.Success && result.Content != null)
            {
                activeUser = new BLLUser(user);
                activeUser.Token = result.Content;
            }
            return new BLLResponse() { Status = result.Status, Message = result.Message };
        }
    }
}
