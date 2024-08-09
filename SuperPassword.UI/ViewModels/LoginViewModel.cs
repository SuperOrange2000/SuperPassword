using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperPassword.BLL.Interfaces;
using SuperPassword.Config.Config;
using SuperPassword.Config.Service;
using SuperPassword.Entity.Interface;
using SuperPassword.UI.Models;
using SuperPassword.UI.Services;
//using SuperPassword.Entity.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SuperPassword.UI.ViewModels;
public partial class LoginViewModel : ViewModelBase
{
    [ObservableProperty]
    private User activeUser;

    [ObservableProperty]
    private ObservableCollection<User> allUsers;

    private readonly IBLLService BLLService;
    private readonly IConfigService configService;
    private readonly INavigationService navigationService;

    public string Title { get; set; } = "SuperPassword";

    public LoginViewModel(IBLLService BLLService, IConfigService configService, INavigationService navigationService)
    {
        Width = 300;
        Height = 450;

        this.BLLService = BLLService;
        this.configService = configService;
        this.navigationService = navigationService;

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
        var loginResult = await BLLService.Login(user);

        if (loginResult != null && loginResult.Status == ResponseStatus.Success)
        {
            navigationService.NavigateTo<MainViewModel>();
        }
    }

    [RelayCommand]
    private async Task SignUp(IUser user)
    {
        var loginResult = await BLLService.SignUp(user);
        if (loginResult != null && loginResult.Status == ResponseStatus.Success)
        {
            navigationService.NavigateTo<MainViewModel>();
        }
    }
}

