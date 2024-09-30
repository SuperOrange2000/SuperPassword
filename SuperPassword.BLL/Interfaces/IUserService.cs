using SuperPassword.BLL.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Interfaces
{
    public partial interface IBLLService
    {
        Task<BLLResponse> SignUpAsync(IUser user);

        Task<BLLResponse> LoginAsync(IUser user);
    }
}
