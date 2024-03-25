using ImTools;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using SuperPassword.BLL;
using SuperPassword.Entity;
using SuperPassword.Entity.Data;
using SuperPassword.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SuperPassword.ViewModels
{
    public class MainViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<InfoGroupViewItem> _infoGroupDTOs;
        public ObservableCollection<InfoGroupViewItem> InfoGroupItems
        {
            get { return _infoGroupDTOs; }
            set { _infoGroupDTOs = value; }
        }

        private UserEntity _activeUser;

        public UserEntity ActiveUser
        {
            get { return _activeUser; }
            set { _activeUser = value; }
        }

        public uint MaxIndex { get; set; } = 0;

        private readonly IDialogService _dialogService;
        private readonly IDataServiceBLL _dataServiceBLL;

        private string filterText;

        public string FilterText
        {
            get { return filterText; }
            set { filterText = value; RaisePropertyChanged(); }
        }


        public DelegateCommand<InfoGroupViewItem> DeleteCommand { get; private set; }
        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand<InfoGroupEntity> EditCommand { get; private set; }
        public DelegateCommand<string> FilterCommand { get; private set; }
        public DelegateCommand ClearFilterCommand { get; private set; }

        public MainViewModel(IDialogService dialogService, IDataServiceBLL dataServiceBLL)
        {
            _dialogService = dialogService;
            _dataServiceBLL = dataServiceBLL;

            InfoGroupItems = new ObservableCollection<InfoGroupViewItem>();

            DeleteCommand = new DelegateCommand<InfoGroupViewItem>(Delete);
            AddCommand = new DelegateCommand(Add);
            EditCommand = new DelegateCommand<InfoGroupEntity>(Update);
            FilterCommand = new DelegateCommand<string>(Filter);
            ClearFilterCommand = new DelegateCommand(ClearFilter);
        }

        private async void Delete(InfoGroupViewItem entity)
        {
            var result = await _dataServiceBLL.DeleteAsync(ActiveUser, entity.InfoGroup.ID);
            if (result.Status == System.Net.HttpStatusCode.NoContent)
                InfoGroupItems.Remove(entity);
        }

        private void Add()
        {
            DialogParameters param = new DialogParameters
            {
                { "Title", "新建" },
                { "ID", ++MaxIndex }
            };

            Action<IDialogResult> eventHandler = async (IDialogResult dialogResult) =>
            {
                if (dialogResult.Result == ButtonResult.OK)
                {
                    try
                    {
                        //UpdateLoading(true);
                        var infoGroup = dialogResult.Parameters.GetValue<InfoGroupEntity>("Value");
                        var result = await _dataServiceBLL.AddAsync(ActiveUser, infoGroup);
                        if (result.Status == System.Net.HttpStatusCode.Created)
                        {
                            InfoGroupItems.Add(new InfoGroupViewItem { InfoGroup = infoGroup });
                        }
                    }
                    finally
                    {
                        //UpdateLoading(false);
                    }
                }
            };

            _dialogService.ShowDialog("AddInfoGroupView", param, eventHandler);
        }

        private void Update(InfoGroupEntity dto)
        {
            DialogParameters param = new DialogParameters
            {
                { "Value", dto }
            };

            Action<IDialogResult> eventHandler = async (IDialogResult dialogResult) =>
            {
                if (dialogResult.Result == ButtonResult.OK)
                {
                    try
                    {
                        //UpdateLoading(true);
                        var infoGroup = dialogResult.Parameters.GetValue<InfoGroupEntity>("Value");
                        var result = await _dataServiceBLL.UpdateAsync(ActiveUser, infoGroup);
                        if (result.Status == System.Net.HttpStatusCode.OK) // !ToDo BUG
                        {
                            var todoModel = InfoGroupItems.FirstOrDefault(t => t.InfoGroup.ID.Equals(infoGroup.ID))?.InfoGroup;
                            if (todoModel != null)
                            {
                                todoModel.Username = infoGroup.Username;
                                todoModel.Password = infoGroup.Password;
                                todoModel.Site = infoGroup.Site;
                                todoModel.TagEntities = infoGroup.TagEntities;
                            }
                        }
                    }
                    finally
                    {
                        //UpdateLoading(false);
                    }
                }
            };

            _dialogService.ShowDialog("AddInfoGroupView", param, eventHandler);
        }

        private void Filter(string site)
        {
            foreach (var item in InfoGroupItems)
            {
                if(item.InfoGroup.Site != site)
                    item.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void ClearFilter()
        {
            foreach (var item in InfoGroupItems)
                item.Visibility = System.Windows.Visibility.Visible;
            FilterText = string.Empty;
        }

        async void InitToDoList()
        {
            var result = await _dataServiceBLL.GetAllAsync(ActiveUser);
            if (result.Status == System.Net.HttpStatusCode.OK)
            {
                InfoGroupItems.AddRange(result.Content!.Select(info => new InfoGroupViewItem { InfoGroup = info }));
            }
            if (InfoGroupItems.Count > 0)
                MaxIndex = InfoGroupItems.Max(i => i.InfoGroup.ID);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ActiveUser = GlobalEntity.ActiveUsser;
            InitToDoList();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
