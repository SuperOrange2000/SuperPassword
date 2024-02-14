using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Markup;


namespace SuperPassword.Shared.DTOs
{
    public class InfoGroupDTO : BaseDTO
    {
        private uint _id;
        public uint ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _site;
        public string Site
        {
            get { return Decrypt(_site); }
            set { _site = Encrypt(value); }
        }
        [JsonProperty("site")]
        public string EncryptedSite
        {
            get { return _site; }
            set { _site = value; }
        }

        private string _username;
        public string Username
        {
            get { return Decrypt(_username); }
            set { _username = Encrypt(value); }
        }
        [JsonProperty("username")]
        public string EncryptedUsername
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password;
        public string Password
        {
            get { return Decrypt(_password); }
            set { _password = Encrypt(value); }
        }
        [JsonProperty("password")]
        public string EncryptedPassword
        {
            get { return _password; }
            set { _password = value; }
        }

        private DateTime _createTime;
        [JsonProperty("create-time")]
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        private DateTime _updateTime;
        [JsonProperty("update-time")]
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }


        private ObservableCollection<TagDTO> _tagDTOs;

        public ObservableCollection<TagDTO> TagDTOs
        {
            get { return _tagDTOs; }
            set { _tagDTOs = value; }
        }
        [JsonProperty("tags")]
        public string EncryptedTagDTOs
        {
            get {
                var data = string.Join("&", _tagDTOs.Select(tag => tag.EncryptedContent));
                var encodedString = Encoding.UTF8.GetBytes(data);
                return Convert.ToBase64String(encodedString);
            }
            set {
                var decodedBytes = Convert.FromBase64String(value);
                var decodedString = Encoding.UTF8.GetString(decodedBytes);
                string[] contentArray = decodedString.Split('&');

                // 创建一个新的ObservableCollection<TagDTO>
                ObservableCollection<TagDTO> tagCollection = new ObservableCollection<TagDTO>();

                // 遍历分割后的字符串数组，创建TagDTO对象并添加到集合中
                foreach (string content in contentArray)
                {
                    if (!string.IsNullOrEmpty(content)) // 确保内容不为空
                    {
                        TagDTO tagDTO = new TagDTO { EncryptedContent = content };
                        tagCollection.Add(tagDTO);
                    }
                }

                _tagDTOs = tagCollection;
            }
        }

        private byte[] _salt;

        public byte[] Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }

        public InfoGroupDTO()
        {
            if(Salt == null)
            {
                Salt = new byte[32];
                Random random = new Random();
                random.NextBytes(Salt);
            }

            if(TagDTOs == null)
            {
                TagDTOs = new ObservableCollection<TagDTO>();
                for (int i = 0; i < 5; i++)
                    TagDTOs.Add(new TagDTO() { Content = "a" + i, Color = "#f00" });
            }
        }

        public byte[] ToByteArray()
        {
            //var list = from item in TagDTOs select item.Content;
            List<object> collectoin = new List<object>() { Site, Username, Password, from item in TagDTOs select item.Content };
            string jsonString = JsonConvert.SerializeObject(collectoin);
            return Encoding.UTF8.GetBytes(jsonString);
        }

    }

}
