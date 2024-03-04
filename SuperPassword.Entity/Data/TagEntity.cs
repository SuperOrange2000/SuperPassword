using SuperPassword.Entity.Setting;
using SuperPassword.Security;
using System.Collections;
using System.Text;

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
                if (_content == null) return null;
                byte[]? plaintext = DecryptionHandler?.Invoke(_content, GetNonce(_nonceID));
                if (plaintext == null)
                    return null;
                return Encoding.UTF8.GetString(plaintext);
            }
            set
            {
                if (value == null) return;
                byte[] plaintext = Encoding.UTF8.GetBytes(value);
                var encryptedData = EncryptionHandler?.Invoke(plaintext, GetNonce(_nonceID));
                if (encryptedData == null) return;
                else _content = encryptedData;

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
                _content = new byte[EncryptedData.Length -1];
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

        public TagEntity() : base(UserSetting.Instance.CipherType)
        {
        }

        public TagEntity(byte nonceID) : base(UserSetting.Instance.CipherType)
        {
            _nonceID = nonceID;
        }

        private void SetColor()
        {
            Color = string.Empty;
        }
    }
}
