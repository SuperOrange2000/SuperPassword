using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SuperPassword.BLL;
using SuperPassword.Config.Service;
using SuperPassword.DAL;
using SuperPassword.DAL.OnlineService;
using SuperPassword.DAL.OnlineService.Clinet;
using SuperPassword.Security.Sercvice;
using SuperPassword.UI.Models;
using SuperPassword.UI.ViewModels;
using SuperPassword.UI.Views;
using System;
using Ursa.Controls;

namespace SuperPassword.UI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public new static App? Current => Application.Current as App;
    public IServiceProvider? ServiceProvider { get; private set; }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);
        var services = new ServiceCollection();
        services.AddSingleton<IConfigService, ConfigService>();
        services.AddSingleton<ISecurityService, SecurityService>();
        services.AddSingleton(provider => new HttpRestClient(@"https://s.oragne.top/"));
        services.AddSingleton<IUserServiceBLL, UserService>();
        services.AddSingleton<IDataServiceBLL, DataService>();
        services.AddSingleton<IUserServiceDAL, UserServiceOnline>();
        services.AddSingleton<IDataServiceDAL, DataserviceOnline>();

        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<MainViewModel>();
        ServiceProvider = services.BuildServiceProvider();
        if (ServiceProvider == null) throw new NullReferenceException(nameof(ServiceProvider));

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = ServiceProvider!.GetService<MainViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = ServiceProvider!.GetService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
