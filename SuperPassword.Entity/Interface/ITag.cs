namespace SuperPassword.Entity.Interface
{
    public interface ITag
    {
        public Guid InfoGroupGuid { get; set; }
        public byte[] Content { get; set; }
    }
}
