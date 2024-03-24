using SuperPassword.Entity;
using SuperPassword.Entity.Data;

namespace SuperPassword.DAL
{
    public interface IUserServiceDAL
    {
        Task<ResponseDAL> SignUp(UserEntity user);

        Task<ResponseDAL> Login(UserEntity user);
    }
}
