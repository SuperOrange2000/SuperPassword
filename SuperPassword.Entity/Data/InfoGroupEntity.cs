using Newtonsoft.Json;
using SuperPassword.Entity.Setting;
using SuperPassword.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Markup;


namespace SuperPassword.Entity.Data
{
    public class InfoGroupEntity : EncryptedBase
    {

        private byte[] _site;
        public string? Site
        {
            get
            {
                if (_site == null) return null;
                byte[]? plaintext = DecryptionHandler?.Invoke(_site, GetNonce(0));
                if (plaintext == null)
                    return null;
                return Encoding.UTF8.GetString(plaintext);
            }
            set
            {
                if (value == null) return;
                byte[] plaintext = Encoding.UTF8.GetBytes(value);
                var encryptedData = EncryptionHandler?.Invoke(plaintext, GetNonce(0));
                if (encryptedData == null) return;
                else _site = encryptedData;
            }
        }
        [JsonProperty("site")]
        public string EncryptedSite
        {
            get { return Convert.ToBase64String(_site); }
            set { _site = Convert.FromBase64String(value); }
        }

        private byte[] _username;
        public string? Username
        {
            get
            {
                if (_username == null) return null;
                byte[]? plaintext = DecryptionHandler?.Invoke(_username, GetNonce(1));
                if (plaintext == null)
                    return null;
                return Encoding.UTF8.GetString(plaintext);
            }
            set
            {
                if (value == null) return;
                byte[] plaintext = Encoding.UTF8.GetBytes(value);
                var encryptedData = EncryptionHandler?.Invoke(plaintext, GetNonce(1));
                if (encryptedData == null) return;
                else _username = encryptedData;
            }
        }
        [JsonProperty("username")]
        public string EncryptedUsername
        {
            get { return Convert.ToBase64String(_username); }
            set { _username = Convert.FromBase64String(value); }
        }

        private byte[] _password;
        public string? Password
        {
            get
            {
                if (_password == null) return null;
                byte[]? plaintext = DecryptionHandler?.Invoke(_password, GetNonce(2));
                if (plaintext == null)
                    return null;
                return Encoding.UTF8.GetString(plaintext);
            }
            set
            {
                if (value == null) return;
                byte[] plaintext = Encoding.UTF8.GetBytes(value);
                var encryptedData = EncryptionHandler?.Invoke(plaintext, GetNonce(2));
                if (encryptedData == null) return;
                else _password = encryptedData;
            }
        }
        [JsonProperty("password")]
        public string EncryptedPassword
        {
            get { return Convert.ToBase64String(_password); }
            set { _password = Convert.FromBase64String(value); }
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

        private ObservableCollection<TagEntity> _tagEntities;

        public ObservableCollection<TagEntity> TagEntities
        {
            get { return _tagEntities; }
            set { _tagEntities = value; }
        }
        [JsonProperty("tags")]
        public string EncryptedTagEntities
        {
            get
            {
                string data = string.Join("&", TagEntities.Select(tag => tag.Content));
                byte[] encodedString = Encoding.UTF8.GetBytes(data);
                byte[]? encryptedData = EncryptionHandler?.Invoke(encodedString, GetNonce(3));
                if (encryptedData == null) return string.Empty;
                else return Convert.ToBase64String(encryptedData);
            }
            set
            {
                TagEntities = new ObservableCollection<TagEntity>();
                if (value == null) return;

                byte[] decodedBytes = Convert.FromBase64String(value);
                byte[]? plaintext = DecryptionHandler?.Invoke(decodedBytes, GetNonce(3));
                if (plaintext == null) return;

                var decodedString = Encoding.UTF8.GetString(decodedBytes);
                string[] contentArray = decodedString.Split('&');

                // 遍历分割后的字符串数组，创建TagDTO对象并添加到集合中
                for (uint i = 0; i < contentArray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(contentArray[i])) // 确保内容不为空
                    {
                        TagEntity tagDTO = new TagEntity(contentArray[i]);
                        TagEntities.Add(tagDTO);
                    }
                }
            }
        }

        public InfoGroupEntity() : base(UserSetting.Instance.CipherType)
        {
            if (Salt == null)
            {
                Salt = new byte[32];
                Random random = new Random();
                random.NextBytes(Salt);
            }

            if (TagEntities == null)
            {
                TagEntities = new ObservableCollection<TagEntity>();
                for (int i = 0; i < 5; i++)
                    TagEntities.Add(new TagEntity("a" + i));
            }
        }
    }

}
