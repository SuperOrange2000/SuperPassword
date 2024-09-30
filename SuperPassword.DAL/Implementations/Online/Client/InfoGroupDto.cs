using SuperPassword.DAL.Models;
using System.Text.Json.Serialization;

namespace SuperPassword.DAL.Implementations.Online.Client
{
    internal class InfoGroupDto
    {
        [JsonPropertyName("id")]
        public Guid InfoGroupGuid { get; set; }

        [JsonPropertyName("site")]
        public string Site { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("tags")]
        public IList<string>? Tags { get; set; }

        public InfoGroupDto() { }

        public InfoGroupDto(DALInfoGroup infoGroup)
        {
            //InfoGroupGuid = infoGroup.InfoGroupGuid;
            Site = Convert.ToBase64String([.. infoGroup.Site, .. infoGroup.SiteNonce, .. infoGroup.SiteTag]);
            Username = Convert.ToBase64String([.. infoGroup.Username, .. infoGroup.UsernameNonce, .. infoGroup.UsernameTag]);
            Password = Convert.ToBase64String([.. infoGroup.Password, .. infoGroup.PasswordNonce, .. infoGroup.PasswordTag]);
            if (infoGroup.Tags != null)
            {
                Tags = [];
                foreach (var tag in infoGroup.Tags)
                {
                    Tags.Add(Convert.ToBase64String([.. tag.Content, .. tag.ContentNonce, .. tag.ContentTag]));
                }
            }
        }

        public DALInfoGroup ConvertBack()
        {
            var result = new DALInfoGroup { InfoGroupGuid = InfoGroupGuid };
            SplitData(Site, out byte[] data, out byte[] nonce, out byte[] tag);
            result.Site = data;
            result.SiteNonce = nonce;
            result.SiteTag = tag;
            SplitData(Username, out data, out nonce, out tag);
            result.Username = data;
            result.UsernameNonce = nonce;
            result.UsernameTag = tag;
            SplitData(Password, out data, out nonce, out tag);
            result.Password = data;
            result.PasswordNonce = nonce;
            result.PasswordTag = tag;
            if (Tags != null)
            {
                result.Tags = [];
                for (int i = 0; i < Tags.Count; i++)
                {
                    SplitData(Tags[i], out data, out nonce, out tag);
                    result.Tags.Add(new DALTag { Content = data, ContentNonce = nonce, ContentTag = tag, });
                }
            }
            return result;
        }

        private void SplitData(string dataText, out byte[] data, out byte[] dataNonce, out byte[] dataTag)
        {
            byte[] byteData = Convert.FromBase64String(dataText);
            data = byteData.SkipLast(28).ToArray();
            dataNonce = byteData.SkipLast(16).TakeLast(12).ToArray();
            dataTag = byteData.TakeLast(16).ToArray();
        }
    }
}
