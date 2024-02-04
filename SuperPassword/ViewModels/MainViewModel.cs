using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using SuperPassword.Common;
using SuperPassword.Service;
using SuperPassword.Shared.Dtos;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SuperPassword.ViewModels
{
    public class MainViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<InfoGroupDTO> _infoGroupDTOs;
        public ObservableCollection<InfoGroupDTO> InfoGroupDTOs
        {
            get { return _infoGroupDTOs; }
            set { _infoGroupDTOs = value; RaisePropertyChanged(); }
        }

        private UserDto _activeUser;

        public UserDto ActiveUser
        {
            get { return _activeUser; }
            set { _activeUser = value; }
        }

        public uint MaxIndex { get; set; } = 0;

        private readonly IOfflineService _offlineService;
        private readonly IOnlineService _onlineService;
        private readonly IDialogService _dialogService;

        public DelegateCommand<InfoGroupDTO> DeleteCommand { get; private set; }
        public DelegateCommand<InfoGroupDTO> AddCommand { get; private set; }
        public DelegateCommand<InfoGroupDTO> EditCommand { get; private set; }

        public MainViewModel(IContainerProvider provider)
        {
            this._offlineService = provider.Resolve<IOfflineService>();
            this._onlineService = provider.Resolve<IOnlineService>();
            this._dialogService = provider.Resolve<IDialogService>();

            InfoGroupDTOs = new ObservableCollection<InfoGroupDTO>();
            DeleteCommand = new DelegateCommand<InfoGroupDTO>(Delete);
            AddCommand = new DelegateCommand<InfoGroupDTO>(Add);
            EditCommand = new DelegateCommand<InfoGroupDTO>(Update);

        }

        private async void Delete(InfoGroupDTO dto)
        {
            var result = await _onlineService.DeleteAsync(ActiveUser, dto.ID);
            if (result.Status == System.Net.HttpStatusCode.NoContent)
                InfoGroupDTOs.Remove(dto);
        }

        private void Add(InfoGroupDTO dto)
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
                        var infoGroup = dialogResult.Parameters.GetValue<InfoGroupDTO>("Value");
                        var result = await _onlineService.AddAsync(ActiveUser, infoGroup);
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

        private void Update(InfoGroupDTO dto)
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
                        var infoGroup = dialogResult.Parameters.GetValue<InfoGroupDTO>("Value");
                        var result = await _onlineService.UpdateAsync(ActiveUser, infoGroup);
                        if (result.Status == System.Net.HttpStatusCode.OK) // !ToDo BUG
                        {
                            var todoModel = InfoGroupDTOs.FirstOrDefault(t => t.ID.Equals(infoGroup.ID));
                            if (todoModel != null)
                            {
                                todoModel.Username = infoGroup.Username;
                                todoModel.Password = infoGroup.Password;
                                todoModel.Site = infoGroup.Site;
                                todoModel.TagDTOs = infoGroup.TagDTOs;
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
            //SecurityModule securityM = new SecurityModule();
            var result = await _onlineService.GetAllAsync(ActiveUser);
            if (result.Status == System.Net.HttpStatusCode.OK)
            {
                InfoGroupDTOs.AddRange(result.Content);
            }
            //for (int i = 0; i < 10; i++)
            //{
            //    InfoGroupDTOs.Add(new InfoGroupDTO() {Site = "TestWebsite" + i, Username = "用户名", Password = "密码" });
            //}
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("ActiveUser"))
            {
                ActiveUser = navigationContext.Parameters.GetValue<UserDto>("ActiveUser");
            }
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
