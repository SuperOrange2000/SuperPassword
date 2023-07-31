using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SuperPassword.Common.Models
{
	public class PWDto
	{
		private int _id;

		public int ID
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _website;

		public string Website
		{
			get { return _website; }
			set { _website = value; }
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


        private ObservableCollection<Tag> _tags;

		public ObservableCollection<Tag> Tags
		{
			get { return _tags; }
			set { _tags = value; }
		}

        public PWDto()
		{
			_id = -1;
			_website = string.Empty;
			_username = string.Empty;
			_password = string.Empty;

			_tags = new ObservableCollection<Tag>();
            for (int i = 0; i < 10; i++)
                _tags.Add(new Tag() { Content = "a"+i, Color="#f00" });
		}

	}
    public class Tag
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

		public Tag()
		{
            _content = string.Empty;
            _color = string.Empty;
		}
	}
}
