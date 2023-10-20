using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using SuperPassword.Service;
using SuperPassword.ViewModels;
using SuperPassword.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SuperPassword.Views;

namespace SuperPassword
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void OnInitialized()
        {
            var dialog = Container.Resolve<IDialogService>();

            var service = App.Current.MainWindow.DataContext as IConfigureService;
            if (service != null)
                service.Configure();
            base.OnInitialized();
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IOfflineService, OfflineService>();
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            containerRegistry.RegisterForNavigation<AddInfoGroupView, AddInfoGroupViewModel>();
            containerRegistry.RegisterDialog<AddInfoGroupView>();
        }
    }
}
