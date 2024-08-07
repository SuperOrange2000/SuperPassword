using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SuperPassword.BLL;
using SuperPassword.Entity;
using SuperPassword.UI.Models;
using SuperPassword.UI.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Ursa.Controls;

namespace SuperPassword.UI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<InfoGroupViewItem> infoGroupViewItems;

    [ObservableProperty]
    private User activeUser;

    private IServiceProvider serviceProvider; 
    private IDataServiceBLL dataServiceBLL;
    public MainViewModel(IServiceProvider serviceProvider, IDataServiceBLL dataServiceBLL)
    {
        this.serviceProvider = serviceProvider;
        this.dataServiceBLL = dataServiceBLL;
        InfoGroupViewItems = new ObservableCollection<InfoGroupViewItem>()
        {
            new InfoGroupViewItem() {Visibility = true,  Site="test1", ViewTags=new ObservableCollection<string> {"1", "hello" } },
            new InfoGroupViewItem() {Visibility = true, Site="test2" },
        };
    }

    public MainViewModel()
    {
        InfoGroupViewItems = new ObservableCollection<InfoGroupViewItem>()
        {
            new InfoGroupViewItem() {Visibility = true,  Site="test1", ViewTags=new ObservableCollection<string> {"1", "hello" } },
            new InfoGroupViewItem() {Visibility = true, Site="test2" },
        };
    }

    [RelayCommand]
    private async Task ShowLoginDialogAsync()
    {
        LoginViewModel vm = serviceProvider?.GetService<LoginViewModel>()!;
        User? user = await Dialog.ShowCustomModal<LoginView, LoginViewModel, User>(vm, options: new DialogOptions()
        {
            IsCloseButtonVisible = false
        }); ;
        if (user != null)
            ActiveUser = user;
        InitToDoList();
    }

    private async void InitToDoList()
    {
        if(dataServiceBLL == null) return;
        var result = await dataServiceBLL.GetAllAsync(ActiveUser.Name!, ActiveUser.Token!);
        if (result.Status == System.Net.HttpStatusCode.OK && result.Content != null)
        {
            InfoGroupViewItems.Concat(result.Content.Select(info => new InfoGroupViewItem(info)));
        }
    }

    [RelayCommand]
    private async Task Update(InfoGroupViewItem infoGroup)
    {
        
        if (infoGroup.IsEditable)
            return;
        else
        {
            if (infoGroup.IsNew)
                await dataServiceBLL.AddAsync(ActiveUser.Name!, ActiveUser.Token!, infoGroup);
            else
                await dataServiceBLL.UpdateAsync(ActiveUser.Name!, ActiveUser.Token!, infoGroup);
        }
    }

    [RelayCommand]
    private async Task Delete(InfoGroupViewItem infoGroup)
    {
        var result = await dataServiceBLL.DeleteAsync(ActiveUser.Name!, ActiveUser.Token!, infoGroup.Id);
        if (result.Status == System.Net.HttpStatusCode.NoContent)
            InfoGroupViewItems.Remove(infoGroup);
    }

    [RelayCommand]
    private void Add()
    {
        InfoGroupViewItem newItem = new InfoGroupViewItem() { IsEditable=true, IsNew=true };
        InfoGroupViewItems.Add(newItem);
    }
}
