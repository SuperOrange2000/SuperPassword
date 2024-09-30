using RestSharp;
using SuperPassword.DAL.Implementations.Online.Client;
using SuperPassword.DAL.Models;
using SuperPassword.DAL.Online.Client;

namespace SuperPassword.DAL.Implementations.Online
{
    public partial class OnlineService
    {
        public async Task<OnlineResponse> AddAsync(string username, DALInfoGroup newInfoGroup)
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

        public async Task<OnlineResponse> DeleteAsync(string username, Guid id)
        {
            BaseRequest request = new BaseRequest($"info-group/{id}", RestSharp.Method.Delete);
            return await client.RequestAsync(request);
        }

        public async Task<OnlineResponse<List<DALInfoGroup>>> GetAllAsync(string username)
        {
            // TODO
            BaseRequest request = new BaseRequest("api/{}", RestSharp.Method.Put);
            var response = await client.RequestAsync<IList<InfoGroupDto>>(request);
            return response.Convert(list => list.Select(item => item.ConvertBack()).ToList());
        }

        public async Task<OnlineResponse<DALInfoGroup>> GetFirstOfDefaultAsync(string username, Guid id)
        {
            BaseRequest request = new BaseRequest($"info-group/{id}", RestSharp.Method.Get);
            var response = await client.RequestAsync<InfoGroupDto>(request);
            return response.Convert(item => item.ConvertBack());
        }

        public async Task<OnlineResponse> UpdateAsync(string username, DALInfoGroup infoGroup)
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
