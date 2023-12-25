using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SuperPassword.Extensions;
using SuperPassword.Service;
using SuperPassword.Shared.Dtos;
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
        private UserDto _activeUser;

        public UserDto ActiveUser
        {
            get { return _activeUser; }
            set { _activeUser = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<UserDto> _allUsers;

        public ObservableCollection<UserDto> AllUsers
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

        public DelegateCommand<object> LoginCommand { get; private set; }

        public string Title { get; set; } = "SuperPassword";

        public LoginViewModel(IContainerProvider provider)
        {
            LoginCommand = new DelegateCommand<object>(Login);
            _onlineService = provider.Resolve<OnlineService>();
            ActiveUser = new UserDto();
        }

        private async void Login(object parameter)
        {
            var result = await _onlineService.Login(ActiveUser, Password);
        }


    }
}
