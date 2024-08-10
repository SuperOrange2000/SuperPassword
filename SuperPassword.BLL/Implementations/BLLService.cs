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
        private IDALService _DALService;
        private BLLUser activeUser;
        public BLLService(IDALService DALService)
        {
            _DALService = DALService;
        }

        public static void AddService(ServiceCollection container)
        {
            container.AddSingleton<IDALService, DALService>();
            DALService.AddService(container);
        }

        public IBLLUser ActiveUser { get => activeUser; }
    }
}
