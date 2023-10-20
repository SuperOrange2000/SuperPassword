using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using SuperPassword.Views;
using SuperPassword.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyToDo.Extensions;

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

            //_regionManager.RegisterViewWithRegion("MainViewRegion", typeof(MainView));
        }

        public void Configure()
        {
            //UserName = AppSession.UserName;
            //CreateMenuBar();
            _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("MainView");
        }
    }
}
