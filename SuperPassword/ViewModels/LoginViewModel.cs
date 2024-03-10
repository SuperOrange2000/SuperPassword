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
using SuperPassword.Config.Service;
using System.Linq;
using SuperPassword.Config.Config;

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
        private readonly IConfigService _configService;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand<UserEntity> LoginCommand { get; private set; }
        public DelegateCommand<UserEntity> SignUpCommand { get; private set; }

        public string Title { get; set; } = "SuperPassword";

        public LoginViewModel(IUserServiceBLL userServiceBLL, IConfigService configService)
        {
            _userServiceBLL = userServiceBLL;
            _configService = configService;

            LoginCommand = new DelegateCommand<UserEntity>(Login);
            SignUpCommand = new DelegateCommand<UserEntity>(SignUp);
            UserConfig userConfig;
            uint localId;
            if (configService.GlobalConfig.NameMap.Count ==  0)
            {
                localId = configService.GlobalConfig.MaxLocalId++;
                configService.GlobalConfig.NameMap.Add(localId, null);
            }
            else
            {
                localId = configService.GlobalConfig.NameMap.Keys.FirstOrDefault();
            }
            userConfig = configService.UserConfig[localId];
            ActiveUser = new UserEntity() { Salt = userConfig.Salt};
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
                GlobalEntity.ActiveUsser = user;
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }
        }

        private async void SignUp(UserEntity user)
        {
            var result = await _userServiceBLL.SignUp(user);
        }
    }
}
