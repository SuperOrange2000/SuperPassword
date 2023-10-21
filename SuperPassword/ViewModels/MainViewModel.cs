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
        private ObservableDictionary<long, InfoGroupDTO> _infoGroupDTOs;
        public ObservableDictionary<long, InfoGroupDTO> InfoGroupDTOs
        {
            get { return _infoGroupDTOs; }
            set { _infoGroupDTOs = value; RaisePropertyChanged(); }
        }

        private long _tail = 0;
        public long Tail
        {
            get { return _tail; }
            set { _tail = value; }
        }

        private readonly IOfflineService _offlineService;
        private readonly IOnlineService _onlineService;

        //private readonly IRegionManager regionManager;

        private readonly IDialogService _dialogService;

        public DelegateCommand<InfoGroupDTO> DeleteCommand { get; private set; }
        public DelegateCommand<InfoGroupDTO> AddCommand { get; private set; }
        public DelegateCommand<InfoGroupDTO> EditCommand { get; private set; }

        public MainViewModel(IContainerProvider provider)
        {
            this._offlineService = provider.Resolve<IOfflineService>();
            this._onlineService = provider.Resolve<IOnlineService>();
            this._dialogService = provider.Resolve<IDialogService>();

            InfoGroupDTOs = new ObservableDictionary<long, InfoGroupDTO>();
            DeleteCommand = new DelegateCommand<InfoGroupDTO>(Delete);
            AddCommand = new DelegateCommand<InfoGroupDTO>(Add);
            EditCommand = new DelegateCommand<InfoGroupDTO>(Add);

            CreateTestToDoList();
        }

        private void Delete(InfoGroupDTO dto)
        {
            _offlineService.DeleteAsync(dto.ID);
            InfoGroupDTOs.Remove(dto.ID);
            Tail--;
        }

        async void Add(InfoGroupDTO dto)
        {
            DialogParameters param = new DialogParameters();
            if (dto != null)
                param.Add("Value", dto);
            else
                param.Add("Title", "新建");

            Action<IDialogResult> eventHandler = async (IDialogResult dialogResult) =>
            {
                if (dialogResult.Result == ButtonResult.OK)
                {
                    try
                    {
                        //UpdateLoading(true);
                        var infoGroup = dialogResult.Parameters.GetValue<InfoGroupDTO>("Value");
                        bool isNew = dialogResult.Parameters.GetValue<bool>("isNew");
                        if (isNew)
                        {
                            var updateResult = await _onlineService.UpdateAsync(infoGroup);
                            if (updateResult.Status)
                            {
                                InfoGroupDTOs.Add(Tail++, infoGroup);
                            }
                        }
                        else
                        {
                            InfoGroupDTOs[infoGroup.ID] = infoGroup;
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

        void CreateTestToDoList()
        {
            SecurityModule securityM = new SecurityModule();
            for (int i = 0; i < 10; i++)
            {
                InfoGroupDTOs.Add(Tail, new InfoGroupDTO() { ID = Tail++, Website = "TestWebsite" + i, Username = "用户名", Password = "密码" });
            }
        }
    }
}
