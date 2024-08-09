using SuperPassword.DAL.Interfaces.Models;

namespace SuperPassword.DAL.Interfaces
{
    public interface IUserService
    {
        Task<IDALResponse> SignUp(string name, string password);

        Task<IDALResponse> Login(string name, string password);
    }
}
