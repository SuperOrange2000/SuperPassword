namespace SuperPassword.Shared.DTOs
{
    public class UserDTO : BaseDTO
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        private string account;

        public string Account
        {
            get { return account; }
            set { account = value; OnPropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private string _token;

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}
