using SuperPassword.BLL.Implementations.Models;
using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Implementations.Models;
using SuperPassword.Entity.Interface;
using SuperPassword.Security.Sercvice;

namespace SuperPassword.BLL.Implementations
{
    public partial class BLLService
    {
        public async Task<IBLLResponse> SignUpAsync(IUser user)
        {
            BLLResponse response = new();
            if (IsOnline)
                response.OnlineResponse = await onlineService.SignUpAsync(user);
            if (IsOffline)
            {
                var offlineResponse = await offlineService.SignUpAsync(user);
                response.OfflineResponse = offlineResponse;
                if (offlineResponse.DataStatus == ResponseDataStatus.Success)
                {
                    activeUser = new BLLUser(user);
                    securityService.SwitchCipher(SecurityMode.AesGcm, offlineResponse.Content);
                }
            }
            return response;
        }

        public async Task<IBLLResponse> LoginAsync(IUser user)
        {
            BLLResponse response = new();
            if (IsOnline)
                response.OnlineResponse = await onlineService.LoginAsync(user);
            if (IsOffline)
            {
                var offlineResponse = await offlineService.LoginAsync(user);
                response.OfflineResponse = offlineResponse;
                if (offlineResponse.DataStatus == ResponseDataStatus.Success)
                {
                    activeUser = new BLLUser(user);
                    securityService.SwitchCipher(SecurityMode.AesGcm, offlineResponse.Content);
                }
            }
            return response;
        }
    }
}
