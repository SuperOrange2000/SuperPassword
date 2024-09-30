using SuperPassword.BLL.Models;
using SuperPassword.Entity.Interface;
using SuperPassword.Security.Sercvice;

namespace SuperPassword.BLL.Implementations
{
    public partial class BLLService
    {
        public async Task<BLLResponse> SignUpAsync(IUser user)
        {
            BLLResponse response = new();
            configService.SwitchUser(user.Name, user.UserGuid);
            if (IsOffline)
            {
                var offlineResponse = await offlineService.SignUpAsync(user);
                response.OfflineResponse = offlineResponse;
                securityService.SwitchCipher(SecurityMode.AesGcm, offlineResponse.Content);
                activeUser = new BLLUser(user);
            }
            if (IsOnline)
            {
                var onlineResponse = await onlineService.SignUpAsync(user.Name, user.Password);
                response.OnlineResponse = onlineResponse;
                if (onlineResponse.NetworkStatusCode == System.Net.HttpStatusCode.OK)
                {
                    configService.UserProperties.ServerId = onlineResponse.Content.Id;
                }
            }
            return response;
        }

        public async Task<BLLResponse> LoginAsync(IUser user)
        {
            BLLResponse response = new();
            configService.SwitchUser(user.Name, user.UserGuid);
            if (IsOnline)
                response.OnlineResponse = await onlineService.LoginAsync(configService.UserProperties.ServerId, user.Password);
            if (IsOffline)
            {
                var offlineResponse = await offlineService.LoginAsync(user);
                response.OfflineResponse = offlineResponse;
                activeUser = new BLLUser(user);
                securityService.SwitchCipher(SecurityMode.AesGcm, offlineResponse.Content);
            }
            return response;
        }
    }
}
