using Microsoft.Extensions.DependencyInjection;
using SuperPassword.BLL.Implementations.Models;
using SuperPassword.BLL.Interfaces;
using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Implementations;
using SuperPassword.DAL.Interfaces;

namespace SuperPassword.BLL.Implementations
{
    public partial class BLLService : JsonDeserialization, IBLLService
    {
        private ServiceProvider serviceProvider;
        private IDALService DALService;
        private BLLUser activeUser;
        public BLLService()
        {
            ServiceCollection container = new ServiceCollection();
            container.AddSingleton<IDALService, DALService>();
            serviceProvider = container.BuildServiceProvider();
            DALService = serviceProvider.GetService<IDALService>()!;
        }

        public IBLLUser ActiveUser { get => activeUser; }
    }
}
