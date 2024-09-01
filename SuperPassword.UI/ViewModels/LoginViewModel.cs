using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperPassword.BLL.Implementations.Models;
using SuperPassword.BLL.Interfaces;
using SuperPassword.Config.Service;
using SuperPassword.DAL.Implementations.Models;
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

    [ObservableProperty]
    private bool enableServer = false;

    [ObservableProperty]
    private bool isServerSwitching = false;

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
    private async Task LoadCompletedAsync()
    {
        IsServerSwitching = true;
        await BLLService.UpdateStorageModeAsync((StorageMode)configService.AppConfig.StorageMode);
        EnableServer = (configService.AppConfig.StorageMode & 0b010) != 0;
        IsServerSwitching = false;
    }

    [RelayCommand]
    private async Task SwitchServerAsync()
    {
        IsServerSwitching = true;
        if (EnableServer)
        {
            configService.AppConfig.StorageMode = (byte)StorageMode.ServerSync;
            await BLLService.UpdateStorageModeAsync(StorageMode.ServerSync);
        }
        else
        {
            configService.AppConfig.StorageMode = (byte)StorageMode.LocalOnly;
            await BLLService.UpdateStorageModeAsync(StorageMode.LocalOnly);
        }
        IsServerSwitching = false;
    }

    [RelayCommand]
    private async Task Login(IUser user)
    {
        var loginResult = await BLLService.LoginAsync(user);
        if (loginResult == null)
            return;
        else if (loginResult.IsSuccess)
        {
            navigationService.NavigateTo<MainViewModel>();
            configService.UserConfig.Name = user.Name;
        }
        else if (loginResult.OfflineResponse.DataStatus == ResponseDataStatus.ResourcesNotFoundError)
            UserNameNotice = "用户名未注册";
        else if (loginResult.OfflineResponse.DataStatus == ResponseDataStatus.Forbidden)
            PasswordNotice = "密码错误";
    }

    [RelayCommand]
    private async Task SignUp(IUser user)
    {
        var loginResult = await BLLService.SignUpAsync(user);
        if (loginResult == null)
            return;
        else if (loginResult.IsSuccess)
        {
            navigationService.NavigateTo<MainViewModel>();
            configService.UserConfig.Name = user.Name;
        }
        else if (loginResult.OfflineResponse.DataStatus == ResponseDataStatus.NameConflictError)
            UserNameNotice = "用户名已被占用";
    }

    [RelayCommand]
    private void ClearNotice(string? control = null)
    {
        if (control == null || control == "username") UserNameNotice = string.Empty;
        if (control == null || control == "password") PasswordNotice = string.Empty;

    }
}

