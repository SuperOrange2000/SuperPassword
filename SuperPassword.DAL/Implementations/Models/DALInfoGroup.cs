using Mapster;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;
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
        public Guid InfoGroupGuid { get; set; }

        [Required]
        public string Site { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        internal List<DALTag>? DALTags { get; set; }

        [NotMapped]
        public IList<string>? Tags
        {
            get => DALTags?.Select(tag => tag.Content).ToList();
            set => DALTags = value?.Select(tag => new DALTag() { Content = tag }).ToList();
        }
    }
}
