using SuperPassword.BLL.Models;
using SuperPassword.Entity.Interface;
using SuperPassword.Security.Sercvice;
using System.Security.Cryptography;
using System.Text;

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
                byte[] internalPwd = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(user.Password),
                    configService.UserProperties.LocalSalt,
                    configService.AppConfig.EncryptInterations,
                    HashAlgorithmName.SHA512,
                    configService.AppConfig.PasswordLength
                );
                var offlineResponse = await offlineService.SignUpAsync(user.Name, internalPwd);
                response.OfflineResponse = offlineResponse;
                securityService.SwitchCipher(SecurityMode.AesGcm, offlineResponse.Content);
                activeUser = new BLLUser(user);
            }
            if (IsOnline)
            {
                byte[] internalPwd = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(user.Password),
                    configService.UserProperties.ServerSalt,
                    configService.AppConfig.EncryptInterations,
                    HashAlgorithmName.SHA512,
                    configService.AppConfig.PasswordLength
                );
                var onlineResponse = await onlineService.SignUpAsync(user.Name, internalPwd);
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
            {
                byte[] internalPwd = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(user.Password),
                    configService.UserProperties.ServerSalt,
                    configService.AppConfig.EncryptInterations,
                    HashAlgorithmName.SHA512,
                    configService.AppConfig.PasswordLength
                );
                response.OnlineResponse = await onlineService.LoginAsync(configService.UserProperties.ServerId, internalPwd);
            }
            if (IsOffline)
            {
                byte[] internalPwd = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(user.Password),
                    configService.UserProperties.LocalSalt,
                    configService.AppConfig.EncryptInterations,
                    HashAlgorithmName.SHA512,
                    configService.AppConfig.PasswordLength
                );
                var offlineResponse = await offlineService.LoginAsync(user.Name, internalPwd);
                response.OfflineResponse = offlineResponse;
                activeUser = new BLLUser(user);
                securityService.SwitchCipher(SecurityMode.AesGcm, offlineResponse.Content);
            }
            return response;
        }
    }
}
