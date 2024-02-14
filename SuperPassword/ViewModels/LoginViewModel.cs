using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SuperPassword.Extensions;
using SuperPassword.Service;
using SuperPassword.Shared.DTOs;
using System;
using System.Collections.ObjectModel;
using System.Security;
namespace SuperPassword.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware 
    {
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
        private UserDTO _activeUser;

        public UserDTO ActiveUser
        {
            get { return _activeUser; }
            set { _activeUser = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<UserDTO> _allUsers;

        public ObservableCollection<UserDTO> AllUsers
        {
            get { return _allUsers; }
            set { _allUsers = value; RaisePropertyChanged(); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(); }
        }

        private readonly IOnlineService _onlineService;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand<UserDTO> LoginCommand { get; private set; }
        public DelegateCommand<UserDTO> SignUpCommand { get; private set; }

        public string Title { get; set; } = "SuperPassword";

        public LoginViewModel(IContainerProvider provider)
        {
            LoginCommand = new DelegateCommand<UserDTO>(Login);
            SignUpCommand = new DelegateCommand<UserDTO>(SignUp);
            _onlineService = provider.Resolve<OnlineService>();
            ActiveUser = new UserDTO();
        }

        private async void Login(UserDTO user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) ||
                string.IsNullOrWhiteSpace(Password))
            {
                return;
            }

            var loginResult = await _onlineService.Login(user, Password);
            if (loginResult != null && loginResult.Status == System.Net.HttpStatusCode.OK)
            {
                user.Token = loginResult.Content;
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters() { { "User", ActiveUser } }));
            }
        }

        private async void SignUp(UserDTO user)
        {
            var result = await _onlineService.SignUp(user, Password);
        }

        public UserDTO GetActiveUser()
        {
            return ActiveUser;
        }
    }
}
