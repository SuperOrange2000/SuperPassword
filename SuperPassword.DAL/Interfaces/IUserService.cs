using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Interfaces
{
    public interface IUserService
    {
        Task<IDALResponse<byte[]>> SignUpAsync(IUser user);

        Task<IDALResponse<byte[]>> LoginAsync(IUser user);
    }
}
