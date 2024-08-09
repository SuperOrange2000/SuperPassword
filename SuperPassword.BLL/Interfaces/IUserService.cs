using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IBLLResponse> SignUp(IUser user);

        Task<IBLLResponse> Login(IUser user);

        IBLLUser ActiveUser { get; }
    }
}
