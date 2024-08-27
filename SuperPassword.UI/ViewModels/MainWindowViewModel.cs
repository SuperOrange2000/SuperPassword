using CommunityToolkit.Mvvm.ComponentModel;
using SuperPassword.UI.Services;

namespace SuperPassword.UI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly INavigationService navigator;

        [ObservableProperty]
        private ViewModelBase? currentViewModel;

        public MainWindowViewModel(INavigationService navigationService)
        {
            navigator = navigationService;

            navigator.CurrentViewModelChanged += () =>
            {
                CurrentViewModel = navigationService.CurrentViewModel;
                if (CurrentViewModel != null)
                {
                    Height = CurrentViewModel.Height;
                    Width = CurrentViewModel.Width;
                }
            };

            // navigate to login page at first
            navigator.NavigateTo<LoginViewModel>();
        }
    }
}
