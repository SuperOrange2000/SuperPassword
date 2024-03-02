using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using SuperPassword.BLL;
using SuperPassword.Common;
using SuperPassword.DAL;
using SuperPassword.Entity;
using SuperPassword.Entity.Setting;
using SuperPassword.ViewModels;
using SuperPassword.Views;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;

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

            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Current.Shutdown(0);
                    return;
                }

                var service = Current.MainWindow.DataContext as IConfigureService;
                var activeUser = callback.Parameters.GetValue<UserEntity>("User");

                if (service != null)
                    service.Configure(activeUser);
                base.OnInitialized();
            });
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer()
                            .Register<DAL.OnlineService.Clinet.HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "apiUrl"));
            containerRegistry.GetContainer().RegisterInstance(@"https://s.oragne.top/", serviceKey: "apiUrl");

            containerRegistry.Register<IUserServiceBLL, UserService>();
            containerRegistry.Register<IDataServiceBLL, DataService>();
            containerRegistry.Register<IUserServiceDAL, DAL.OnlineService.UserServiceOnline>();
            containerRegistry.Register<IDataServiceDAL, DAL.OnlineService.DataserviceOnline>();
            containerRegistry.Register<IDialogService, DialogService>();

            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<AddInfoGroupView, AddInfoGroupViewModel>();
            containerRegistry.RegisterDialog<AddInfoGroupView>();
        }
    }
}
