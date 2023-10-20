using Prism.Commands;
using Prism.Mvvm;
using SuperPassword.Shared.Dtos;
using SuperPassword.Service;
using SuperPassword.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using SuperPassword.Common;
using Prism.Services.Dialogs;
using Prism.Regions;

namespace SuperPassword.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private ObservableCollection<InfoGroupDTO> _InfoGroupDTOs;
        public ObservableCollection<InfoGroupDTO> InfoGroupDTOs
        {
            get { return _InfoGroupDTOs; }
            set { _InfoGroupDTOs = value; RaisePropertyChanged(); }
        }

        private readonly IOfflineService _offlineService;

        private readonly IRegionManager regionManager;

        private readonly IDialogService _dialogService;

        public DelegateCommand<InfoGroupDTO> DeleteCommand { get; private set; }
        public DelegateCommand<string> ExecuteCommand { get; private set; }

        public MainViewModel(IDialogService dialogService, IOfflineService offlineService)
        {
            this._offlineService = offlineService;
            this._dialogService = dialogService;

            //provider.Resolve<IOfflineService>();
            //provider.Resolve<IRegionManager>();

            InfoGroupDTOs = new ObservableCollection<InfoGroupDTO>();

            DeleteCommand = new DelegateCommand<InfoGroupDTO>(Delete);
            ExecuteCommand = new DelegateCommand<string>(Execute);

            CreateTestToDoList();
        }

        private void Delete(InfoGroupDTO obj)
        {
            _offlineService.DeleteAsync(obj.ID);
            var model = InfoGroupDTOs.FirstOrDefault(t => t.ID.Equals(obj.ID));
            if (model != null)
                InfoGroupDTOs.Remove(model);
        }

        private void Execute(string cmd)
        {
            switch (cmd)
            {
                case "Add": AddPwd(null);break;
            }
        }

        private async void AddPwd(InfoGroupDTO model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model);

            Action<IDialogResult> eventHandler = (IDialogResult result) => { };

            _dialogService.Show("AddInfoGroupView", param, eventHandler);
        }

        void CreateTestToDoList()
        {
            for (int i = 0; i < 10; i++)
            {
                InfoGroupDTOs.Add(new InfoGroupDTO() { ID = i, Website = "TestWebsite" + i, Username = "用户名", Password = "密码" });
            }
        }
    }
}
