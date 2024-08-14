using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperPassword.BLL.Interfaces;
using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.Entity.Interface;
using SuperPassword.UI.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SuperPassword.UI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<InfoGroupViewItem> infoGroupViewItems;

    [ObservableProperty]
    private User activeUser;

    private IBLLService BLLService;
    public MainViewModel(IBLLService BLLService)
    {
        Width = 800;
        Height = 450;

        this.BLLService = BLLService;

        InfoGroupViewItems = [];
        InitToDoList();
    }

    public MainViewModel()
    {
        InfoGroupViewItems =
        [
            new InfoGroupViewItem() {Visibility = true,  Site="test1", ViewTags=new ObservableCollection<string> {"1", "hello" } },
            new InfoGroupViewItem() {Visibility = true, Site="test2" },
        ];
    }

    private async void InitToDoList()
    {
        if (BLLService == null) return;
        IBLLResponse<IList<IBLLInfoGroup>> result = await BLLService.GetAllAsync();
        if (result.DataStatus == ResponseDataStatus.Success && result.Content != null)
        {
            foreach (var item in result.Content)
            {
                InfoGroupViewItems.Add(new InfoGroupViewItem(item));
            }
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
            {
                await BLLService.AddAsync(infoGroup);
                infoGroup.IsNew = false;
            }
            else
                await BLLService.UpdateAsync(infoGroup);
        }
    }

    [RelayCommand]
    private async Task Delete(InfoGroupViewItem infoGroup)
    {
        if (!infoGroup.IsNew)
        {
            var result = await BLLService.DeleteAsync(infoGroup.InfoGroupGuid);
            if (result.DataStatus != ResponseDataStatus.Success)
                return;
        }
        InfoGroupViewItems.Remove(infoGroup);
    }

    [RelayCommand]
    private void Add()
    {
        InfoGroupViewItem newItem = new InfoGroupViewItem() { IsEditable = true, IsNew = true };
        InfoGroupViewItems.Add(newItem);
    }
}
