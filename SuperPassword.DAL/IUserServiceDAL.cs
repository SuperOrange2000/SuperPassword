using SuperPassword.Entity.Interface;
using SuperPassword.DAL.Models;

namespace SuperPassword.DAL
{
    public interface IUserServiceDAL
    {
        Task<ResponseDAL> SignUp(IUser user);

        Task<ResponseDAL> Login(IUser user);
    }
}
