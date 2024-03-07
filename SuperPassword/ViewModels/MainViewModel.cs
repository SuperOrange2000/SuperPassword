using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using SuperPassword.BLL;
using SuperPassword.Entity;
using SuperPassword.Entity.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SuperPassword.ViewModels
{
    public class MainViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<InfoGroupEntity> _infoGroupDTOs;
        public ObservableCollection<InfoGroupEntity> InfoGroupDTOs
        {
            get { return _infoGroupDTOs; }
            set { _infoGroupDTOs = value; RaisePropertyChanged(); }
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

        public DelegateCommand<InfoGroupEntity> DeleteCommand { get; private set; }
        public DelegateCommand<InfoGroupEntity> AddCommand { get; private set; }
        public DelegateCommand<InfoGroupEntity> EditCommand { get; private set; }

        public MainViewModel(IDialogService dialogService, IDataServiceBLL dataServiceBLL)
        {
            _dialogService = dialogService;
            _dataServiceBLL = dataServiceBLL;

            InfoGroupDTOs = new ObservableCollection<InfoGroupEntity>();

            DeleteCommand = new DelegateCommand<InfoGroupEntity>(Delete);
            AddCommand = new DelegateCommand<InfoGroupEntity>(Add);
            EditCommand = new DelegateCommand<InfoGroupEntity>(Update);
        }

        private async void Delete(InfoGroupEntity dto)
        {
            var result = await _dataServiceBLL.DeleteAsync(ActiveUser, dto.ID);
            if (result.Status == System.Net.HttpStatusCode.NoContent)
                InfoGroupDTOs.Remove(dto);
        }

        private void Add(InfoGroupEntity dto)
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
                            InfoGroupDTOs.Add(infoGroup);
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
                            var todoModel = InfoGroupDTOs.FirstOrDefault(t => t.ID.Equals(infoGroup.ID));
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
        async void InitToDoList()
        {
            var result = await _dataServiceBLL.GetAllAsync(ActiveUser);
            if (result.Status == System.Net.HttpStatusCode.OK)
            {
                InfoGroupDTOs.AddRange(result.Content);
            }
            if (InfoGroupDTOs.Count > 0)
                MaxIndex = InfoGroupDTOs.Max(i => i.ID);
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
