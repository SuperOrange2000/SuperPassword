using Mapster;
using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Implementations.Models
{
    public class BLLInfoGroup : IBLLInfoGroup
    {
        public Guid InfoGroupGuid { get; set; }

        public string Site { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public IList<string>? Tags { get; set; }

        public BLLInfoGroup() { }

        public BLLInfoGroup(IInfoGroup infoGroup)
        {
            infoGroup.Adapt(this);
        }
        public BLLInfoGroup(IDALInfoGroup infoGroup)
        {
            infoGroup.Adapt(this);

        }
    }
}
