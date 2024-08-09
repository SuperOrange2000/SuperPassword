using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Implementations.Models
{
    public class DALResponse : Interfaces.Models.IDALResponse
    {
        public ResponseStatus Status { get; set; }

        public string? Content { get; set; }
    }
}
