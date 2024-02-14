using SuperPassword.Extensions;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using SuperPassword.Common;
using SuperPassword.Shared.DTOs;

namespace SuperPassword.ViewModels
{
    public class MainWindowViewModel : BindableBase, IConfigureService
    {
        private string _title = "Title1";
        private readonly IContainerProvider containerProvider;
        private readonly IRegionManager? _regionManager;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
        {
            this._regionManager = regionManager;
            this.containerProvider = containerProvider;
        }

        public void Configure(UserDTO user)
        {
            _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("MainView", new NavigationParameters { { "ActiveUser", user } });
        }
    }
}
