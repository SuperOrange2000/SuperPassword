using Microsoft.Extensions.DependencyInjection;
using SuperPassword.DAL.Interfaces.Online;
using SuperPassword.DAL.Online.Client;

namespace SuperPassword.DAL.Implementations.Online
{
    public partial class OnlineService : IOnlineService
    {
        private readonly HttpRestClient client;

        private string Token { get; set; }

        public OnlineService(HttpRestClient client)
        {
            this.client = client;
        }
        public static void AddService(ServiceCollection container)
        {
            container.AddSingleton<HttpRestClient>();
        }
    }
}
