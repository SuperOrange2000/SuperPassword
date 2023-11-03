using SuperPassword.Common;
using System.Collections.ObjectModel;


namespace SuperPassword.Shared.Dtos
{
    public class InfoGroupDTO
    {
        private string _id;

        public string ID
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

        public InfoGroupDTO()
        {
            _id = RandomString.GenerateRandomString(32);
            _site = string.Empty;
            _username = string.Empty;
            _password = string.Empty;

            _tagDtos = new ObservableCollection<TagDto>();
            for (int i = 0; i < 5; i++)
                _tagDtos.Add(new TagDto() { Content = "a" + i, Color = "#f00" });
            _tagDtos.Add(new TagDto() { IsNewButton = true });
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
            _content = string.Empty;
            _color = string.Empty;
            _isNewButton = false;
        }
    }
}
