using SuperPassword.DAL.Interfaces.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperPassword.DAL.Implementations.Models
{
    [Table(nameof(DALInfoGroup))]
    public class DALInfoGroup : IDALInfoGroup
    {
        [Key]
        public uint Id { get; set; }

        [Required]
        public Guid InfoGroupGuid { get; set; } = Guid.NewGuid();

        [Required]
        public byte[] Site { get; set; }
        [Required]
        public byte[] SiteNonce { get; set; }
        [Required]
        public byte[] SiteTag { get; set; }


        [Required]
        public byte[] Username { get; set; }
        [Required]
        public byte[] UsernameNonce { get; set; }
        [Required]
        public byte[] UsernameTag { get; set; }


        [Required]
        public byte[] Password { get; set; }
        [Required]
        public byte[] PasswordNonce { get; set; }
        [Required]
        public byte[] PasswordTag { get; set; }

        public ICollection<DALTag>? Tags { get; set; }
    }
}
