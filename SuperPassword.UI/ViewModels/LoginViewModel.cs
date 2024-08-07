using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperPassword.BLL;
using SuperPassword.Config.Config;
using SuperPassword.Config.Service;
//using SuperPassword.Entity.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SuperPassword.UI.Models;
using SuperPassword.Entity.Interface;
using System.Collections.Generic;
using Irihi.Avalonia.Shared.Contracts;
using System;

namespace SuperPassword.UI.ViewModels
{
    public partial class LoginViewModel : ViewModelBase, IDialogContext
    {
        [ObservableProperty]
        private User activeUser;

        [ObservableProperty]
        private ObservableCollection<User> allUsers;

        private readonly IUserServiceBLL _userServiceBLL;
        private readonly IConfigService _configService;

        public event EventHandler<object?>? RequestClose;

        public string Title { get; set; } = "SuperPassword";

        public LoginViewModel(IUserServiceBLL userServiceBLL, IConfigService configService)
        {
            _userServiceBLL = userServiceBLL;
            _configService = configService;

            UserConfig userConfig;
            uint localId;
            if (configService.GlobalConfig.NameMap.Count == 0)
            {
                localId = configService.GlobalConfig.MaxLocalId++;
                configService.GlobalConfig.NameMap.Add(localId, null);
            }
            else
            {
                localId = configService.GlobalConfig.NameMap.Keys.FirstOrDefault();
            }
            userConfig = configService.UserConfig[localId];
            ActiveUser = new User() { Salt = userConfig.Salt };
        }

        //private bool CanLogin() => !string.IsNullOrWhiteSpace(ActiveUser.Name) && !string.IsNullOrWhiteSpace(ActiveUser.Password);

        [RelayCommand]
        private async Task Login(IUser user)
        {
            var loginResult = await _userServiceBLL.Login(user);

            if (loginResult != null && loginResult.Status == System.Net.HttpStatusCode.OK)
            {
                ActiveUser = new User(user);
                ActiveUser.Token = loginResult.Content;
            }
            RequestClose?.Invoke(this, ActiveUser);
        }

        [RelayCommand]
        private async Task SignUp(IUser user)
        {
            var loginResult = await _userServiceBLL.SignUp(user);
            if (loginResult != null && loginResult.Status == System.Net.HttpStatusCode.OK)
            {
                ActiveUser = new User(user);
                ActiveUser.Token = loginResult.Content;
            }
            RequestClose?.Invoke(this, ActiveUser);
        }

        public void Close()
        {
            RequestClose?.Invoke(this, ActiveUser);
        }
    }
}
