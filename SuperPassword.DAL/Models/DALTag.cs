using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperPassword.DAL.Models
{
    [Table(nameof(DALTag))]
    public class DALTag
    {
        [Key]
        public uint Id { get; set; }

        [Required]
        public Guid TagGuid { get; set; } = Guid.NewGuid();

        [Required]
        public byte[] Content { get; set; }
        [Required]
        public byte[] ContentNonce { get; set; }
        [Required]
        public byte[] ContentTag { get; set; }

        [Required]
        public uint InfoGroupId { get; set; }
        public DALInfoGroup InfoGroup { get; set; }
    }
}
