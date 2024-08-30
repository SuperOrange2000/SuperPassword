using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Interfaces
{
    public partial interface IBLLService
    {
        Task<IBLLResponse> SignUpAsync(IUser user);

        Task<IBLLResponse> LoginAsync(IUser user);

        IBLLUser ActiveUser { get; }
    }
}
