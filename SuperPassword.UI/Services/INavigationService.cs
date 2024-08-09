using SuperPassword.UI.ViewModels;
using System;

namespace SuperPassword.UI.Services
{
    public interface INavigationService
    {
        public ViewModelBase? CurrentViewModel { get; set; }

        public event Action? CurrentViewModelChanged;

        public void NavigateTo<T>() where T : ViewModelBase;
    }
}
