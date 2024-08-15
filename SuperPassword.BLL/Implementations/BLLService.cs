using Microsoft.Extensions.DependencyInjection;
using SuperPassword.BLL.Implementations.Models;
using SuperPassword.BLL.Interfaces;
using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Implementations;
using SuperPassword.DAL.Interfaces;
using SuperPassword.Security.Service;

namespace SuperPassword.BLL.Implementations
{
    public partial class BLLService : IBLLService
    {
        private IDALService _DALService;
        private ISecurityService securityService;
        private BLLUser activeUser;

        private byte[] internalPassword;
        public BLLService(IDALService DALService, ISecurityService securityService)
        {
            _DALService = DALService;
            this.securityService = securityService;
        }

        public static void AddService(ServiceCollection container)
        {
            container.AddSingleton<IDALService, DALService>();
            container.AddSingleton<ISecurityService, SecurityService>();
            SecurityService.AddService(container);
            DALService.AddService(container);
        }

        public IBLLUser ActiveUser { get => activeUser; }
    }
}
