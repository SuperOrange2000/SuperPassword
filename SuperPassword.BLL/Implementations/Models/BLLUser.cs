using Mapster;
using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Implementations.Models
{
    internal class BLLUser : IBLLUser
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }

        public string Token { get; set; }

        public BLLUser() { }

        public BLLUser(IUser user)
        {
            this.Adapt(user);
        }
    }
}
