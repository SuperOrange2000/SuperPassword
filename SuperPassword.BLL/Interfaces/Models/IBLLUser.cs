using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Interfaces.Models
{
    public interface IBLLUser : IUser
    {
        string Token { get; set; }
    }
}
