using System.Security.Cryptography;
using System.Text;

namespace SuperPassword.Entity.Data
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
            OnSaltChanged += SetColor;
        }

        public TagEntity(byte nonceID) : base()
        {
            _nonceID = nonceID;
            OnSaltChanged += SetColor;
        }

        private void SetColor()
        {
            Color = string.Empty;
            if (Content != null)
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Content));

                    Color = $"#{Convert.ToUInt16(hashBytes[0]).ToString("X2")}{Convert.ToUInt16(hashBytes[1]).ToString("X2")}{Convert.ToUInt16(hashBytes[2]).ToString("X2")}";
                }
        }
    }
}
