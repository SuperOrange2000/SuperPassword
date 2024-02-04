using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Shared.Dtos
{
    public class InfoGroupBaseDTO
    {
        private uint _id;
        public uint ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _base64Data;
        [JsonProperty("data")]
        public string Base64Data
        {
            get { return _base64Data; }
            set { _base64Data = value; }
        }

        private string _createTime;
        [JsonProperty("create-time")]
        public string CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        private string _updateTime;
        [JsonProperty("update-time")]
        public string UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }

        private byte[] _salt;
        public byte[] Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }
    }
}
