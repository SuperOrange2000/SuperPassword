using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperPassword.DAL.Implementations.Models
{
    [Table(nameof(DALTag))]
    internal class DALTag
    {
        [Key]
        public uint Id { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
