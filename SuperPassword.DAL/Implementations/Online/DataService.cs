using RestSharp;
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
            BaseRequest request = new BaseRequest("info-group", RestSharp.Method.Post);
            InfoGroupDto infoGroupDto = new(newInfoGroup);
            request.AddJsonBody(new
            {
                guid = newInfoGroup.InfoGroupGuid,
                site = infoGroupDto.Site,
                username = infoGroupDto.Username,
                password = infoGroupDto.Password,
                //tags = infoGroupDto.Tags
            });
            return await client.RequestAsync(request);
        }

        public async Task<IOnlineResponse> DeleteAsync(string username, Guid id)
        {
            BaseRequest request = new BaseRequest($"info-group/{id}", RestSharp.Method.Delete);
            return await client.RequestAsync(request);
        }

        public async Task<IOnlineResponse<IList<IDALInfoGroup>>> GetAllAsync(string username)
        {
            // TODO
            BaseRequest request = new BaseRequest("api/{}", RestSharp.Method.Put);
            var response = await client.RequestAsync<IList<InfoGroupDto>>(request);
            return response.Convert<IList<IDALInfoGroup>>(list => list.Select(item => item.ConvertBack()).ToList());
        }

        public async Task<IOnlineResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(string username, Guid id)
        {
            BaseRequest request = new BaseRequest($"info-group/{id}", RestSharp.Method.Get);
            var response = await client.RequestAsync<InfoGroupDto>(request);
            return response.Convert(item => item.ConvertBack());
        }

        public async Task<IOnlineResponse> UpdateAsync(string username, IDALInfoGroup infoGroup)
        {
            BaseRequest request = new BaseRequest($"info-group/{infoGroup.InfoGroupGuid}", RestSharp.Method.Put);
            InfoGroupDto infoGroupDto = new(infoGroup);
            request.AddParameter("username", infoGroupDto.Username, ParameterType.QueryString);
            request.AddParameter("site", infoGroupDto.Site, ParameterType.QueryString);
            request.AddParameter("password", infoGroupDto.Password, ParameterType.QueryString);
            return await client.RequestAsync(request);
        }
    }
}
