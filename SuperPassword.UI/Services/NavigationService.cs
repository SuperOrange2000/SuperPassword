using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SuperPassword.UI.ViewModels;
using System;

namespace SuperPassword.UI.Services
{
    public class NavigationService : INavigationService
    {
        private ViewModelBase? currentViewModel;

        public ViewModelBase? CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                // notify property changed
                CurrentViewModelChanged?.Invoke();
            }
        }

        public event Action? CurrentViewModelChanged;

        public void NavigateTo<T>() where T : ViewModelBase
            => CurrentViewModel = App.Current.ServiceProvider.GetService<T>();
    }
}
