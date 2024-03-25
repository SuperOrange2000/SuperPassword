using Prism.Mvvm;
using SuperPassword.Entity.Data;
using System.Windows;

namespace SuperPassword.Models
{
    public class InfoGroupViewItem : BindableBase
    {
        public InfoGroupEntity InfoGroup {  get; set; }

		private Visibility _visibility = Visibility.Visible;

		public Visibility Visibility
		{
			get { return _visibility; }
			set { _visibility = value; RaisePropertyChanged(); }
		}

	}
}
