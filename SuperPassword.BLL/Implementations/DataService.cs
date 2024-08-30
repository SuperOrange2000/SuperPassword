using SuperPassword.BLL.Implementations.Models;
using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Implementations.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;
using System.Text;


namespace SuperPassword.BLL.Implementations
{
    public partial class BLLService
    {
        public async Task<IBLLResponse> AddAsync(IInfoGroup infoGroup)
        {
            BLLResponse response = new();
            var DALInfoGroup = Encrypt(infoGroup);
            if (IsOnline)
                response.OnlineResponse = await onlineService.AddAsync(ActiveUser.Name, DALInfoGroup);
            if (IsOffline)
                response.OfflineResponse = await offlineService.AddAsync(DALInfoGroup);
            return response;
        }

        public async Task<IBLLResponse> DeleteAsync(Guid id)
        {
            BLLResponse response = new();
            if (IsOnline)
                response.OnlineResponse = await onlineService.DeleteAsync(ActiveUser.Name, id);
            if (IsOffline)
                response.OfflineResponse = await offlineService.DeleteAsync(id);
            return response;
        }

        public async Task<IBLLResponse<IList<IDALInfoGroup>, IList<IBLLInfoGroup>>> GetAllAsync()
        {
            BLLResponse<IList<IDALInfoGroup>, IList<IBLLInfoGroup>> response = new();
            if (IsOnline)
                response.OnlineResponse = await onlineService.GetAllAsync(ActiveUser.Name);
            if (IsOffline)
                response.OfflineResponse = await offlineService.GetAllAsync();

            if (response.OfflineResponse != null)
                response.Content = response.OfflineResponse.Content.Select(Decrypt).ToList();
            else if (response.OnlineResponse != null)
                response.Content = response.OnlineResponse.Content.Select(Decrypt).ToList();
            return response;
        }

        public async Task<IBLLResponse<IDALInfoGroup, IBLLInfoGroup>> GetFirstOfDefaultAsync(Guid id)
        {
            BLLResponse<IDALInfoGroup, IBLLInfoGroup> response = new();
            if (IsOnline)
                response.OnlineResponse = await onlineService.GetFirstOfDefaultAsync(ActiveUser.Name, id);
            if (IsOffline)
                response.OfflineResponse = await offlineService.GetFirstOfDefaultAsync(id);

            if (response.OfflineResponse != null)
                response.Content = Decrypt(response.OfflineResponse.Content);
            else if (response.OnlineResponse != null)
                response.Content = Decrypt(response.OnlineResponse.Content);
            return response;
        }

        public async Task<IBLLResponse> UpdateAsync(IInfoGroup infoGroup)
        {
            BLLResponse response = new();
            var DALInfoGroup = Encrypt(infoGroup);
            if (IsOnline)
                response.OnlineResponse = await onlineService.UpdateAsync(ActiveUser.Name, DALInfoGroup);
            if (IsOffline)
                response.OfflineResponse = await offlineService.UpdateAsync(DALInfoGroup);
            return response;
        }

        private DALInfoGroup Encrypt(IInfoGroup infoGroup)
        {
            byte[] siteNonce, siteTag;
            byte[]? encryptedSite = securityService.Encrypt(Encoding.UTF8.GetBytes(infoGroup.Site), out siteNonce, out siteTag!);
            byte[] usernameNonce, usernameTag;
            byte[]? encryptedUsername = securityService.Encrypt(Encoding.UTF8.GetBytes(infoGroup.Username), out usernameNonce, out usernameTag!);
            byte[] passwordNonce, passwordTag;
            byte[]? encryptedPassword = securityService.Encrypt(Encoding.UTF8.GetBytes(infoGroup.Password), out passwordNonce, out passwordTag!);
            if (encryptedPassword == null || encryptedUsername == null || encryptedSite == null)
                throw new Exception("加密错误");
            return new DALInfoGroup()
            {
                InfoGroupGuid = infoGroup.InfoGroupGuid,
                Site = encryptedSite,
                SiteNonce = siteNonce,
                SiteTag = siteTag,
                Username = encryptedUsername,
                UsernameNonce = usernameNonce,
                UsernameTag = usernameTag,
                Password = encryptedPassword,
                PasswordNonce = passwordNonce,
                PasswordTag = passwordTag,
                Tags = infoGroup.Tags?.Select((item) =>
                {
                    byte[] tagContentNonce, tagContentTag;
                    byte[]? encryptedTagContent = securityService.Encrypt(Encoding.UTF8.GetBytes(item), out tagContentNonce, out tagContentTag!);
                    if (encryptedTagContent == null) throw new Exception("加密错误");
                    return new DALTag()
                    {
                        Content = encryptedTagContent,
                        ContentNonce = tagContentNonce,
                        ContentTag = tagContentTag,
                    };
                }).ToList(),
            };
        }

        private IBLLInfoGroup Decrypt(IDALInfoGroup infoGroup)
        {
            var decryptedSite = securityService.Decrypt(infoGroup.Site, infoGroup.SiteNonce, infoGroup.SiteTag);
            var decryptedUsername = securityService.Decrypt(infoGroup.Username, infoGroup.UsernameNonce, infoGroup.UsernameTag);
            var decryptedPassword = securityService.Decrypt(infoGroup.Password, infoGroup.PasswordNonce, infoGroup.PasswordTag);
            if (decryptedSite == null || decryptedUsername == null || decryptedPassword == null)
                throw new Exception("解密错误");
            return new BLLInfoGroup()
            {
                InfoGroupGuid = infoGroup.InfoGroupGuid,
                Site = Encoding.UTF8.GetString(decryptedSite),
                Username = Encoding.UTF8.GetString(decryptedUsername),
                Password = Encoding.UTF8.GetString(decryptedPassword),
                Tags = infoGroup.Tags?.Select(item =>
                {
                    var decryptedTag = securityService.Decrypt(item.Content, item.ContentNonce, item.ContentTag);
                    if (decryptedTag == null)
                        throw new Exception("解密错误");
                    return Encoding.UTF8.GetString(decryptedTag);
                }).ToList()
            };

        }
    }
}
