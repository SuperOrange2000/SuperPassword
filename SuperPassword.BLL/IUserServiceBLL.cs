using SuperPassword.Entity.Interface;
using SuperPassword.BLL.Models;

namespace SuperPassword.BLL
{
    public interface IUserServiceBLL
    {
        Task<ResponseBLL<string>> SignUp(IUser user);

        Task<ResponseBLL<string>> Login(IUser user);
    }
}
