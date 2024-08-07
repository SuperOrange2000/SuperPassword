namespace SuperPassword.Entity.Interface
{
    public interface IUser
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public byte[] Salt { get; set; }
    }
}
