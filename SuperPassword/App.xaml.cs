using Prism.DryIoc;
using Prism.Ioc;
using SuperPassword.Service;
using SuperPassword.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IOfflineService, OfflineService>();
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
        }
    }
}
