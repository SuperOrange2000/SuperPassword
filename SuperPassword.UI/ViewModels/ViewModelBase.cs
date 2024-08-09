using CommunityToolkit.Mvvm.ComponentModel;

namespace SuperPassword.UI.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private int width = 100;
    [ObservableProperty]
    private int height = 100;
}
