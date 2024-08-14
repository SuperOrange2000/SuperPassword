namespace SuperPassword.Entity.Interface
{
    public interface IUser
    {
        public Guid UserGuid { get; set; }

        public string Name { get; set; }

        public string? Email { get; set; }

        public string Password { get; set; }
    }
}
