using SuperPassword.Entity;
using SuperPassword.Entity.Data;

namespace SuperPassword.BLL
{
    public interface IUserServiceBLL
    {
        Task<ResponseBLL<string>> SignUp(UserEntity user);

        Task<ResponseBLL<string>> Login(UserEntity user);
    }
}
