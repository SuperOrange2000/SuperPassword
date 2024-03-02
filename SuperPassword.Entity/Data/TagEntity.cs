using SuperPassword.Security;
using System.Text;

namespace SuperPassword.Entity
{
    public class TagEntity
    {
        private string _content;

        public string? Content
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

        public TagEntity(string content)
        {
            Content = content;
            Color = string.Empty;
        }
    }
}
