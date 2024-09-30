using Mapster;
using SuperPassword.DAL.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Models
{
    public class BLLInfoGroup
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
        public BLLInfoGroup(DALInfoGroup infoGroup)
        {
            infoGroup.Adapt(this);

        }
    }
}
