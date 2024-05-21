using CommunityToolkit.Mvvm.ComponentModel;
using SuperPassword.UI.Models;
using System.Collections.ObjectModel;

namespace SuperPassword.UI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<InfoGroupViewItem> infoGroupViewItems;

    public MainViewModel() 
    {
        infoGroupViewItems = new ObservableCollection<InfoGroupViewItem>()
        {
            new InfoGroupViewItem() {Visibility = true, InfoGroup = new Entity.Data.InfoGroupEntity(){ Site="test1" } },
            new InfoGroupViewItem() {Visibility = true, InfoGroup = new Entity.Data.InfoGroupEntity(){ Site="test2" } },
        };
    }
}
