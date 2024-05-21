using CommunityToolkit.Mvvm.ComponentModel;
using SuperPassword.Entity.Data;

namespace SuperPassword.UI.Models
{
    public partial class InfoGroupViewItem : ObservableObject
    {
        public InfoGroupEntity InfoGroup { get; set; }

        [ObservableProperty]
        private bool _visibility;

    }
}
