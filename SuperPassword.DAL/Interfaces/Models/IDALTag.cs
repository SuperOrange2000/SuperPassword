using SuperPassword.DAL.Implementations.Models;

namespace SuperPassword.DAL.Interfaces.Models
{
    public interface IDALTag
    {
        public Guid TagGuid { get; set; }
        public byte[] Content { get; set; }
        public uint InfoGroupId { get; set; }
        public DALInfoGroup InfoGroup { get; set; }
    }
}
