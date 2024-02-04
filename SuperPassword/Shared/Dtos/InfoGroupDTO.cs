using ImTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuperPassword.Common;
using SuperPassword.Common.CustomControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;


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
            get { return _site; }
            set { _site = value; }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _createTime;
        public string CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        private string _updateTime;
        public string UpdateTime
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

        public InfoGroupDTO(uint id)
        {
            ID = id;
            Site = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            Salt = new byte[32];
            Random random = new Random();
            random.NextBytes(Salt);

            TagDTOs = new ObservableCollection<TagDto>();
            for (int i = 0; i < 5; i++)
                TagDTOs.Add(new TagDto() { Content = "a" + i, Color = "#f00" });
            TagDTOs.Add(new TagDto() { IsNewButton = true });
        }


        public InfoGroupDTO(InfoGroupBaseDTO baseDTO)
        {
            ID = baseDTO.ID;
            CreateTime = baseDTO.CreateTime;
            UpdateTime = baseDTO.UpdateTime;
            Salt = baseDTO.Salt;
            var decodedBytes = Convert.FromBase64String(baseDTO.Base64Data);
            var decodedString = Encoding.UTF8.GetString(decodedBytes);

            var jArray = (JArray)JsonConvert.DeserializeObject(decodedString);
            Site = jArray[0].ToString();
            Username = jArray[1].ToString();
            Password = jArray[2].ToString();
            TagDTOs = new ObservableCollection<TagDto>(jArray[3].Select(item => new TagDto(item.ToString())).ToList());
        }

        public byte[] ToByteArray()
        {
            //var list = from item in TagDTOs select item.Content;
            List<object> collectoin = new List<object>() {Site, Username, Password, from item in TagDTOs select item.Content };
            string jsonString = JsonConvert.SerializeObject(collectoin);
            return Encoding.UTF8.GetBytes(jsonString);
        }

    }

}
