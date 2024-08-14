using SuperPassword.DAL.Interfaces.Models;

namespace SuperPassword.DAL.Implementations.Models
{
    internal class DALUser : IDALUser
    {
        public uint Id { get; set; }

        public Guid UserGuid { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string? Email { get; set; }
    }
}
