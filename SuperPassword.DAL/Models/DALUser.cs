namespace SuperPassword.DAL.Models
{
    internal class DALUser
    {
        public uint Id { get; set; }

        public Guid UserGuid { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string? Email { get; set; }
    }
}
