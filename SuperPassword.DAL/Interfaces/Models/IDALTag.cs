using SuperPassword.DAL.Implementations.Models;
using System.ComponentModel.DataAnnotations;

namespace SuperPassword.DAL.Interfaces.Models
{
    public interface IDALTag
    {
        public Guid TagGuid { get; set; }
        public byte[] Content { get; set; }
        public byte[] ContentNonce { get; set; }
        public byte[] ContentTag { get; set; }
        public uint InfoGroupId { get; set; }
        public DALInfoGroup InfoGroup { get; set; }
    }
}
