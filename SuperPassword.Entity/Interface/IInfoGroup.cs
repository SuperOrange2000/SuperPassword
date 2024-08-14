namespace SuperPassword.Entity.Interface
{
    public interface IInfoGroup
    {
        public Guid InfoGroupGuid { get; set; }

        public string Site { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public IList<string>? Tags { get; set; }
    }
}
