using SuperPassword.DAL.Interfaces;
using SuperPassword.DAL.Online.Clinet;

namespace SuperPassword.DAL.Implementations.Online
{
    internal partial class OnlineService : IOnlineService
    {
        private readonly HttpRestClient client;

        private string Token { get; set; }

        public OnlineService(HttpRestClient client)
        {
            this.client = client;
        }
    }
}
