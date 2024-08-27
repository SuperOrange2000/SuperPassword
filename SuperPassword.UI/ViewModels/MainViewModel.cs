using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperPassword.BLL.Interfaces;
using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.Entity.Interface;
using SuperPassword.UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace SuperPassword.UI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<InfoGroup> infoGroupViewItems = [];

    [ObservableProperty]
    private User activeUser;

    //[ObservableProperty]
    private ObservableCollection<string> filterTags = new();
    public ObservableCollection<string> FilterTags
    {
        get => filterTags;
        set => SetProperty(ref filterTags, value);
    }

    private readonly IBLLService BLLService;
    public MainViewModel(IBLLService BLLService)
    {
        Width = 800;
        Height = 450;

        this.BLLService = BLLService;

        FilterTags.CollectionChanged += Filter;
        //FilterTags.GetWeakCollectionChangedObservable().Subscribe(_ => TestFunc(_));

        InitToDoList();
    }

    private void TestFunc(NotifyCollectionChangedEventArgs args)
    {

    }

    public MainViewModel()
    {
        InfoGroupViewItems =
        [
            new InfoGroup() {Visibility = true,  Site="test1", ViewTags=[] },
            new InfoGroup() {Visibility = true, Site="test2", ViewTags = [] },
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
                InfoGroupViewItems.Add(new InfoGroup(item));
            }
        }
    }

    [RelayCommand]
    private async Task Update(InfoGroup infoGroup)
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
    private async Task Delete(InfoGroup infoGroup)
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
        InfoGroup newItem = new InfoGroup() { IsEditable = true, IsNew = true };
        InfoGroupViewItems.Add(newItem);
    }

    [RelayCommand]
    private async Task Dragging(IList<object?> parameters)
    {
        PointerPressedEventArgs? e = parameters[0] as PointerPressedEventArgs;
        string? content = parameters[1]?.ToString();

        if (e is null || content is null)
            return;

        var dragData = new DataObject();
        dragData.Set(DataFormats.Text, content);
        var result = await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Copy);
        Console.WriteLine($"DragAndDrop result: {result}");
    }

    private void Filter(object? sender, NotifyCollectionChangedEventArgs e)
    {
        //ToDo use Dictionary
        if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null && e.NewItems[0] is string newTagContent)
        {
            for (int i = 0; i < InfoGroupViewItems.Count; i++)
            {
                for (int j = 0; j < InfoGroupViewItems[i].ViewTags.Count; j++)
                {
                    if (InfoGroupViewItems[i].ViewTags[j].Content == newTagContent)
                    {
                        InfoGroupViewItems[i].ViewTags[j].IsSelected = true;
                        break;
                    }
                }
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null && e.OldItems[0] is string oldTagContent)
            for (int i = 0; i < InfoGroupViewItems.Count; i++)
            {
                for (int j = 0; j < InfoGroupViewItems[i].ViewTags.Count; j++)
                {
                    if (InfoGroupViewItems[i].ViewTags[j].Content == oldTagContent)
                    {
                        InfoGroupViewItems[i].ViewTags[j].IsSelected = false;
                        break;
                    }
                }
            }

    }

    [RelayCommand]
    private void TagClick(Tag tag)
    {
        bool targetSelected = !tag.IsSelected;
        if (targetSelected)
            FilterTags.Add(tag.Content);
        else
            FilterTags.Remove(tag.Content);
    }
}