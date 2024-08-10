using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SuperPassword.BLL.Implementations;
using SuperPassword.BLL.Interfaces;
using SuperPassword.Config.Service;
using SuperPassword.Security.Sercvice;
using SuperPassword.UI.Services;
using SuperPassword.UI.ViewModels;
using SuperPassword.UI.Views;
using System;

namespace SuperPassword.UI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public new static App Current => (Application.Current! as App)!;
    public IServiceProvider ServiceProvider { get; private set; }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);
        ServiceCollection container = new ServiceCollection();
        container.AddSingleton<IConfigService, ConfigService>();
        container.AddSingleton<ISecurityService, SecurityService>();
        container.AddSingleton<IBLLService, BLLService>();
        container.AddSingleton<INavigationService, NavigationService>();
        container.AddTransient<MainWindowViewModel>();
        container.AddTransient<LoginViewModel>();
        container.AddTransient<MainViewModel>();

        BLLService.AddService(container);

        ServiceProvider = container.BuildServiceProvider();

        if (ServiceProvider == null) throw new NullReferenceException(nameof(ServiceProvider));

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                DataContext = ServiceProvider.GetService<MainWindowViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = ServiceProvider.GetService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
