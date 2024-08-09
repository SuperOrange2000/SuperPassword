using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Interfaces.Models
{
    public interface IDALResponse
    {
        public ResponseStatus Status { get; set; }

        public string? Content { get; set; }
    }
}
