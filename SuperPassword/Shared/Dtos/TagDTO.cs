using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Shared.DTOs
{
    public class TagDTO : BaseDTO
    {
        private string _content;

        public string Content
        {
            get { return Decrypt(_content); }
            set { _content = Encrypt(value); }
        }

        public string EncryptedContent
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


        public TagDTO()
        {
            Content = string.Empty;
            Color = string.Empty;
            IsNewButton = false;
        }

        public TagDTO(string content)
        {
            Content = content;
            Color = string.Empty;
            IsNewButton = false;
        }
    }
}
