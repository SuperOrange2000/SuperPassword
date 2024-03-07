namespace SuperPassword.Entity
{
    public class TagEntity : EncryptedBase
    {
        private byte[] _content;

        private byte _nonceID;

        public byte NonceID
        {
            get { return _nonceID; }
            set { _nonceID = value; }
        }

        public string? Content
        {
            get
            {
                return Get(_content, _nonceID);
            }
            set
            {
                Set(value, ref _content, _nonceID);
                SetColor();
            }
        }

        public string? EncryptedContent
        {
            get
            {
                return Convert.ToBase64String(_content.Concat(new byte[] { _nonceID }).ToArray());
            }
            set
            {
                byte[] EncryptedData = Convert.FromBase64String(value);
                _nonceID = EncryptedData[^1];
                _content = new byte[EncryptedData.Length - 1];
                Array.Copy(EncryptedData, 0, _content, 0, _content.Length);
                SetColor();
            }
        }

        private string _color;

        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public TagEntity()
        {

        }

        public TagEntity(byte nonceID) : base()
        {
            _nonceID = nonceID;
        }

        private void SetColor()
        {
            Color = string.Empty;
        }
    }
}
