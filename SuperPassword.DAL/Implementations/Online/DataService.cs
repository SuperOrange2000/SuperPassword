using SuperPassword.DAL.Implementations.Models;
using SuperPassword.DAL.Implementations.Online.Client;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.DAL.Interfaces.Online;
using SuperPassword.DAL.Online.Client;

namespace SuperPassword.DAL.Implementations.Online
{
    public partial class OnlineService
    {
        public async Task<IOnlineResponse> AddAsync(string username, IDALInfoGroup newInfoGroup)
        {
            BaseRequest request = new BaseRequest("api/add", RestSharp.Method.Post);
            request.AddParameter("token", Token);
            request.AddParameter("id", newInfoGroup.InfoGroupGuid);

            InfoGroupDto infoGroupDto = new(newInfoGroup);
            request.AddParameter("site", infoGroupDto.Site);
            request.AddParameter("username", infoGroupDto.Username);
            request.AddParameter("password", infoGroupDto.Password);
            if (infoGroupDto.Tags != null)
                request.AddParameter("tags", infoGroupDto.Tags);
            return await client.RequestAsync(request);
        }

        public async Task<IOnlineResponse> DeleteAsync(string username, Guid id)
        {
            BaseRequest request = new BaseRequest("api/delete", RestSharp.Method.Post);
            request.AddParameter("token", Token);
            request.AddParameter("ids", new List<Guid> { id });
            return await client.RequestAsync(request);
        }

        public async Task<IOnlineResponse<IList<IDALInfoGroup>>> GetAllAsync(string username)
        {
            BaseRequest request = new BaseRequest("api/get", RestSharp.Method.Post);
            //request.AddParameter("ids", []);
            request.AddParameter("token", Token);
            var response = await client.RequestAsync<IList<InfoGroupDto>>(request);
            return response.Convert<IList<IDALInfoGroup>>(list => list.Select(item => item.ConvertBack()).ToList());
        }

        public async Task<IOnlineResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(string username, Guid id)
        {
            BaseRequest request = new BaseRequest("api/get-data", RestSharp.Method.Post);
            request.AddParameter("token", Token);
            var response = await client.RequestAsync<InfoGroupDto>(request);
            return response.Convert(item => item.ConvertBack());
        }

        public async Task<IOnlineResponse> UpdateAsync(string username, IDALInfoGroup infoGroup)
        {
            BaseRequest request = new BaseRequest("api/update", RestSharp.Method.Post);
            request.AddParameter("token", Token);
            request.AddParameter("id", infoGroup.InfoGroupGuid);
            request.AddParameter("username", infoGroup.Username);
            request.AddParameter("password", infoGroup.Password);
            request.AddParameter("site", infoGroup.Site);
            request.AddParameter("tags", infoGroup.Tags);
            return await client.RequestAsync(request);
        }
    }
}
