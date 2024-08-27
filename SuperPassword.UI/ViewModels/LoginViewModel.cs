using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperPassword.BLL.Interfaces;
using SuperPassword.Config.Service;
using SuperPassword.Entity.Interface;
using SuperPassword.UI.Models;
using SuperPassword.UI.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SuperPassword.UI.ViewModels;
public partial class LoginViewModel : ViewModelBase
{
    [ObservableProperty]
    private User activeUser = new();

    [ObservableProperty]
    private ObservableCollection<User> allUsers;

    [ObservableProperty]
    private string userNameNotice = string.Empty;

    [ObservableProperty]
    private string passwordNotice = string.Empty;

    private readonly IBLLService BLLService;
    private readonly IConfigService configService;
    private readonly INavigationService navigationService;

    public string Title { get; set; } = "SuperPassword";

    public LoginViewModel(IBLLService BLLService, IConfigService configService, INavigationService navigationService)
    {
        Width = 300;
        Height = 500;

        this.BLLService = BLLService;
        this.configService = configService;
        this.navigationService = navigationService;
    }

    //private bool CanLogin() => !string.IsNullOrWhiteSpace(ActiveUser.Name) && !string.IsNullOrWhiteSpace(ActiveUser.Password);

    [RelayCommand]
    private async Task Login(IUser user)
    {
        var loginResult = await BLLService.Login(user);
        if (loginResult == null)
            return;
        else if (loginResult.DataStatus == ResponseDataStatus.Success)
        {
            navigationService.NavigateTo<MainViewModel>();
            configService.UserConfig.Name = user.Name;
        }
        else if (loginResult.DataStatus == ResponseDataStatus.ResourcesNotFoundError)
            UserNameNotice = "用户名未注册";
        else if (loginResult.DataStatus == ResponseDataStatus.Forbidden)
            PasswordNotice = "密码错误";
    }

    [RelayCommand]
    private async Task SignUp(IUser user)
    {
        var loginResult = await BLLService.SignUp(user);
        if (loginResult == null)
            return;
        else if (loginResult.DataStatus == ResponseDataStatus.Success)
        {
            navigationService.NavigateTo<MainViewModel>();
            configService.UserConfig.Name = user.Name;
        }
        else if (loginResult.DataStatus == ResponseDataStatus.NameConflictError)
            UserNameNotice = "用户名已被占用";
    }

    [RelayCommand]
    private void ClearNotice(string? control = null)
    {
        if (control == null || control == "username") UserNameNotice = string.Empty;
        if (control == null || control == "password") PasswordNotice = string.Empty;

    }
}

