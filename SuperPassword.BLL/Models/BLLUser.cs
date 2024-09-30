using Mapster;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Models
{
    public class BLLUser
    {
        public Guid UserGuid { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }

        public BLLUser() { }

        public BLLUser(IUser user)
        {
            user.Adapt(this);
        }
    }
}
