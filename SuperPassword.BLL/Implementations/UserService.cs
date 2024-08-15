using SuperPassword.BLL.Implementations.Models;
using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;
using SuperPassword.Security.Sercvice;

namespace SuperPassword.BLL.Implementations
{
    public partial class BLLService
    {
        public async Task<IBLLResponse> SignUp(IUser user)
        {
            IDALResponse<byte[]> responseDAL = await _DALService.SignUpAsync(user);
            if (responseDAL.DataStatus == ResponseDataStatus.Success)
            {
                activeUser = new BLLUser(user);
                if(responseDAL.Content != null) 
                    securityService.SwitchCipher(SecurityMode.AesGcm, responseDAL.Content);
            }
            return new BLLResponse()
            {
                DataStatus = responseDAL.DataStatus,
                NetworkStatusCode = responseDAL.NetworkStatusCode,
                ServerMessage = responseDAL.ServerMessage,
            };
        }

        public async Task<IBLLResponse> Login(IUser user)
        {
            IDALResponse<byte[]> responseDAL = await _DALService.LoginAsync(user);
            if (responseDAL.DataStatus == ResponseDataStatus.Success)
            {
                activeUser = new BLLUser(user);
                if (responseDAL.Content != null)
                    securityService.SwitchCipher(SecurityMode.AesGcm, responseDAL.Content);
            }
            return new BLLResponse()
            {
                DataStatus = responseDAL.DataStatus,
                NetworkStatusCode = responseDAL.NetworkStatusCode,
                ServerMessage = responseDAL.ServerMessage,
            };
        }
    }
}
