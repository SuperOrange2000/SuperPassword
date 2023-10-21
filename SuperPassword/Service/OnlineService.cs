using SuperPassword.Shared.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperPassword.Shared.Parameters;
using SuperPassword.Shared.Dtos;

namespace SuperPassword.Service
{
    class OnlineService : IOnlineService
    {
        private readonly HttpRestClient client;

        public OnlineService(HttpRestClient client)
        {
            this.client = client;
        }

        public async Task<ApiResponse<InfoGroupDTO>> AddAsync(InfoGroupDTO entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/Add";
            request.Parameter = entity;
            return await client.ExecuteAsync<InfoGroupDTO>(request);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Delete;
            request.Route = $"api/Delete?id={id}";
            return await client.ExecuteAsync(request);
        }

        public async Task<ApiResponse<PagedList<InfoGroupDTO>>> GetAllAsync(QueryParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/GetAll?pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"&search={parameter.Search}";
            return await client.ExecuteAsync<PagedList<InfoGroupDTO>>(request);
        }

        public async Task<ApiResponse<InfoGroupDTO>> GetFirstOfDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/Get?id={id}";
            return await client.ExecuteAsync<InfoGroupDTO>(request);
        }

        public async Task<ApiResponse<InfoGroupDTO>> UpdateAsync(InfoGroupDTO entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/Update";
            request.Parameter = entity;
            return await client.ExecuteAsync<InfoGroupDTO>(request);
        }
    }
}
