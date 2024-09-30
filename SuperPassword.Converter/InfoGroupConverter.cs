using SuperPassword.BLL.Models;
using SuperPassword.UI.Models;

namespace SuperPassword.Converter
{
    public class InfoGroupConverter
    {
        public static InfoGroup ToUI(BLLInfoGroup infoGroup)
        {
            InfoGroup res = new InfoGroup();
            res.InfoGroupGuid = infoGroup.InfoGroupGuid;
            res.Site = infoGroup.Site;
            res.Username = infoGroup.Username;
            res.Password = infoGroup.Password;
            res.Tags = infoGroup.Tags;
            return res;
        }
    }
}
