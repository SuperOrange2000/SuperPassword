using Mapster;
using SuperPassword.DAL.Implementations.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.DAL.Online.Clinet;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Implementations.Online
{
    internal partial class OnlineService
    {

        public async Task<IDALResponse<byte[]>> SignUpAsync(IUser user)
        {
            BaseRequest request = new BaseRequest("api/sign-up", RestSharp.Method.Post);
            request.AddParameter("username", user.Name);
            request.AddParameter("password", user.Password);
            var response = await client.RequestAsync<string>(request);
            if(response.DataStatus == ResponseDataStatus.Success && response.Content != null)
            {
                Token = response.Content;
            }
            DALResponse<byte[]> result = new();
            response.Adapt(result);
            return result;
        }

        public async Task<IDALResponse<byte[]>> LoginAsync(IUser user)
        {
            BaseRequest request = new BaseRequest("api/login", RestSharp.Method.Post);
            request.AddParameter("username", user.Name);
            request.AddParameter("password", user.Password);
            request.AddParameter("device", "windows");
            var response = await client.RequestAsync<string>(request);
            if (response.DataStatus == ResponseDataStatus.Success && response.Content != null)
            {
                Token = response.Content;
            }
            DALResponse<byte[]> result = new();
            response.Adapt(result);
            return result;
        }
    }
}
