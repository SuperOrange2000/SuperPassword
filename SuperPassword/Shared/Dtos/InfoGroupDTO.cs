using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace SuperPassword.Shared.Dtos
{
    public class InfoGroupDTO
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


        private ObservableCollection<TagDto> _tagDTOs;

        public ObservableCollection<TagDto> TagDTOs
        {
            get { return _tagDTOs; }
            set { _tagDTOs = value; }
        }

        private byte[] _salt;

        public byte[] Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }

        public InfoGroupDTO()
        {
            Salt = new byte[32];
            Random random = new Random();
            random.NextBytes(Salt);
        }

        public byte[] ToByteArray()
        {
            //var list = from item in TagDTOs select item.Content;
            List<object> collectoin = new List<object>() { Site, Username, Password, from item in TagDTOs select item.Content };
            string jsonString = JsonConvert.SerializeObject(collectoin);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        public string Encrypt(string data)
        {
            if (data == null)
                return data;

            var encodedString = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(encodedString);
        }

        public string Decrypt(string data)
        {
            if (data == null)
                return data;

            var decodedBytes = Convert.FromBase64String(data);
            var decodedString = Encoding.UTF8.GetString(decodedBytes);
            return decodedString;
        }

    }

}
