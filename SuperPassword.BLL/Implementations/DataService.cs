using Mapster;
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
            IDALResponse responseDAL = await _DALService.AddAsync(ActiveUser.Name, Encrypt(infoGroup));
            return new BLLResponse(responseDAL);
        }

        public async Task<IBLLResponse> DeleteAsync(Guid id)
        {
            IDALResponse responseDAL = await _DALService.DeleteAsync(ActiveUser.Name, id);
            return new BLLResponse(responseDAL);
        }

        public async Task<IBLLResponse<IList<IBLLInfoGroup>>> GetAllAsync()
        {
            IDALResponse<IList<IDALInfoGroup>> responseDAL = await _DALService.GetAllAsync(ActiveUser.Name);
            BLLResponse<IList<IBLLInfoGroup>> result = new();
            responseDAL.Adapt(result);
            result.Content = responseDAL.Content?.Select(Decrypt).ToList();
            return result;
        }

        public async Task<IBLLResponse<IBLLInfoGroup>> GetFirstOfDefaultAsync(Guid id)
        {
            IDALResponse<IDALInfoGroup> responseDAL = await _DALService.GetFirstOfDefaultAsync(ActiveUser.Name, id);
            BLLResponse<IBLLInfoGroup> result = new();
            responseDAL.Adapt(result);
            if (responseDAL.Content != null)
                result.Content = Decrypt(responseDAL.Content);
            return result;
        }

        public async Task<IBLLResponse> UpdateAsync(IInfoGroup infoGroup)
        {
            IDALResponse responseDAL = await _DALService.UpdateAsync(ActiveUser.Name, Encrypt(infoGroup));
            return new BLLResponse(responseDAL);
        }

        private DALInfoGroup Encrypt(IInfoGroup infoGroup)
        {
            byte[] siteNonce, siteTag;
            byte[]? encryptedSite = securityService.Encrypt(Encoding.UTF8.GetBytes(infoGroup.Site), out siteNonce, out siteTag!);
            byte[] usernameNonce, usernameTag;
            byte[]? encryptedUsername = securityService.Encrypt(Encoding.UTF8.GetBytes(infoGroup.Site), out usernameNonce, out usernameTag!);
            byte[] passwordNonce, passwordTag;
            byte[]? encryptedPassword = securityService.Encrypt(Encoding.UTF8.GetBytes(infoGroup.Site), out passwordNonce, out passwordTag!);
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
