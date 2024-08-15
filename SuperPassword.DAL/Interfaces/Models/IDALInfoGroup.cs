using SuperPassword.DAL.Implementations.Models;

namespace SuperPassword.DAL.Interfaces.Models
{
    public interface IDALInfoGroup
    {
        public uint Id { get; }
        public Guid InfoGroupGuid { get; set; }
        public byte[] Site { get; set; }
        public byte[] SiteNonce { get; set; }
        public byte[] SiteTag { get; set; }


        public byte[] Username { get; set; }
        public byte[] UsernameNonce { get; set; }
        public byte[] UsernameTag { get; set; }


        public byte[] Password { get; set; }
        public byte[] PasswordNonce { get; set; }
        public byte[] PasswordTag { get; set; }
        public ICollection<DALTag>? Tags { get; set; }
    }
}
