using Prism.Mvvm;
using Prism.Regions;
using SuperPassword.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Title1";
		private readonly IRegionManager? _regionManager;

        public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public MainWindowViewModel(IRegionManager regionManager)
		{
			this._regionManager = regionManager;

			_regionManager.RegisterViewWithRegion("MainViewRegion", typeof(MainView));
		}

	}
}
