using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SuperPassword.Entity;
using System;
using System.Collections.ObjectModel;
using System.Security;
using SuperPassword.BLL;
using SuperPassword.Entity.Data;

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

        private UserEntity _activeUser;

        public UserEntity ActiveUser
        {
            get { return _activeUser; }
            set { _activeUser = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<UserEntity> _allUsers;

        public ObservableCollection<UserEntity> AllUsers
        {
            get { return _allUsers; }
            set { _allUsers = value; RaisePropertyChanged(); }
        }

        private readonly IUserServiceBLL _userServiceBLL;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand<UserEntity> LoginCommand { get; private set; }
        public DelegateCommand<UserEntity> SignUpCommand { get; private set; }

        public string Title { get; set; } = "SuperPassword";

        public LoginViewModel(IUserServiceBLL userServiceBLL)
        {
            _userServiceBLL = userServiceBLL;

            LoginCommand = new DelegateCommand<UserEntity>(Login);
            SignUpCommand = new DelegateCommand<UserEntity>(SignUp);
            ActiveUser = new UserEntity();
            GlobalEntity.ActiveUsser = ActiveUser;
        }


        private async void Login(UserEntity user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) ||
                string.IsNullOrWhiteSpace(user.UserName))
            {
                return;
            }

            var loginResult = await _userServiceBLL.Login(user);
            if (loginResult != null && loginResult.Status == System.Net.HttpStatusCode.OK)
            {
                user.Token = loginResult.Content;
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters() { { "User", ActiveUser } }));
            }
        }

        private async void SignUp(UserEntity user)
        {
            var result = await _userServiceBLL.SignUp(user);
        }
    }
}
