using ImTools;
using Newtonsoft.Json;
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

        private ObservableCollection<TagDto> _tagDtos;

        public ObservableCollection<TagDto> TagDtos
        {
            get { return _tagDtos; }
            set { _tagDtos = value; }
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

            TagDtos = new ObservableCollection<TagDto>();
            for (int i = 0; i < 5; i++)
                TagDtos.Add(new TagDto() { Content = "a" + i, Color = "#f00" });
            TagDtos.Add(new TagDto() { IsNewButton = true });
        }

        public InfoGroupDTO(uint id, byte[] data, byte[] salt)
        {
            string jsonString = Encoding.UTF8.GetString(data);
            var deserializedStringCollection = JsonConvert.DeserializeObject<StringCollection>(jsonString);
            if(deserializedStringCollection != null)
            {
                ID = id;
                Site = deserializedStringCollection[0];
                Username = deserializedStringCollection[1];
                Password = deserializedStringCollection[2];
                TagDtos = new ObservableCollection<TagDto>();
                Salt = salt;
            }
        }

        public byte[] ToByteArray()
        {
            //var list = from item in TagDtos select item.Content;
            List<object> collectoin = new List<object>() {Site, Username, Password, from item in TagDtos select item.Content };
            string jsonString = JsonConvert.SerializeObject(collectoin);
            return Encoding.UTF8.GetBytes(jsonString);
        }

    }
    public class TagDto
    {
        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private string _color;

        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        private bool _isNewButton;

        public bool IsNewButton
        {
            get { return _isNewButton; }
            set { _isNewButton = value; }
        }


        public TagDto()
        {
            Content = string.Empty;
            Color = string.Empty;
            IsNewButton = false;
        }

        public TagDto(string content)
        {
            Content = content;
            Color = string.Empty;
            IsNewButton = false;
        }
    }
}
