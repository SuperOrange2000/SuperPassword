using System.Collections.ObjectModel;
using System.Text.Json.Serialization;


namespace SuperPassword.Entity.Data
{
    public class InfoGroupEntity : EncryptedBase
    {
        private uint _id;
        [JsonPropertyName("id")]
        public uint ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private byte[] _site;
        public string? Site
        {
            get
            {
                return Get(_site, 0x00);
            }
            set
            {
                Set(value, ref _site, 0x00);
                OnPropertyChanged();
            }
        }
        [JsonPropertyName("site")]
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
                return Get(_username, 0x01);
            }
            set
            {
                Set(value, ref _username, 0x01);
                OnPropertyChanged();
            }
        }
        [JsonPropertyName("username")]
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
                return Get(_password, 0x02);
            }
            set
            {
                Set(value, ref _password, 0x02);
                OnPropertyChanged();
            }
        }
        [JsonPropertyName("password")]
        public string EncryptedPassword
        {
            get { return Convert.ToBase64String(_password); }
            set { _password = Convert.FromBase64String(value); }
        }

        private DateTime _createTime;
        [JsonPropertyName("create-time")]
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        private DateTime _updateTime;
        [JsonPropertyName("update-time")]
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }

        private byte maxTagNonceID = 0x80;


        private ObservableCollection<TagEntity> _tagEntities;
        public ObservableCollection<TagEntity> TagEntities
        {
            get { return _tagEntities; }
            set { _tagEntities = value; }
        }
        [JsonPropertyName("tags")]
        public List<string> EncryptedTagEntities
        {
            get
            {
                return _tagEntities.Select(t => t.EncryptedContent).ToList();
            }
            set
            {
                if (value == null)
                    TagEntities = new ObservableCollection<TagEntity>();
                else
                    TagEntities = new ObservableCollection<TagEntity>(
                        value.Select(s => new TagEntity { EncryptedContent = s})
                    );

                maxTagNonceID = TagEntities.Max(t => t.NonceID);
            }
        }

        private void UpdateTagsSalt()
        {
            foreach (var tag in TagEntities)
            {
                tag.Salt = Salt;
            }
        }

        public InfoGroupEntity() : base()
        {
            if (Salt == null)
            {
                Salt = new byte[32];
                Random random = new Random();
                random.NextBytes(Salt);
            }
            OnSaltChanged += UpdateTagsSalt;

            if (TagEntities == null)
            {
                TagEntities = new ObservableCollection<TagEntity>();
                for (int i = 0; i < 1; i++)
                    TagEntities.Add(new TagEntity(maxTagNonceID++) { Salt = Salt, Content = "test" + i });
            }
        }
    }

}
