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

        private List<long> _idList;

        public long Tail
        {
            get
            {
                int i = 0;
                for (; i < _idList.Count; i++)
                    if (i != _idList[i])
                        break;
                return i;
            }
        }

        private readonly IOfflineService _offlineService;

        //private readonly IRegionManager regionManager;

        private readonly IDialogService _dialogService;

        public DelegateCommand<InfoGroupDTO> DeleteCommand { get; private set; }
        public DelegateCommand<InfoGroupDTO> AddCommand { get; private set; }
        public DelegateCommand<InfoGroupDTO> EditCommand { get; private set; }

        public MainViewModel(IDialogService dialogService, IOfflineService offlineService)
        {
            this._offlineService = offlineService;
            this._dialogService = dialogService;

            //provider.Resolve<IOfflineService>();
            //provider.Resolve<IRegionManager>();

            InfoGroupDTOs = new ObservableDictionary<long, InfoGroupDTO>();
            DeleteCommand = new DelegateCommand<InfoGroupDTO>(Delete);
            AddCommand = new DelegateCommand<InfoGroupDTO>(Add);
            EditCommand = new DelegateCommand<InfoGroupDTO>(Add);

            _idList = new List<long>();

            CreateTestToDoList();
        }

        private void Delete(InfoGroupDTO dto)
        {
            _offlineService.DeleteAsync(dto.ID);
            InfoGroupDTOs.Remove(dto.ID);
        }

        private void Add(InfoGroupDTO dto)
        {
            DialogParameters param = new DialogParameters();
            if (dto != null)
                param.Add("Value", dto);

            Action<IDialogResult> eventHandler = (IDialogResult dialogResult) =>
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
                            InfoGroupDTOs.Add(Tail, infoGroup);
                            _idList.Add(Tail);
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
                InfoGroupDTOs.Add(Tail, new InfoGroupDTO() { ID = i, Website = "TestWebsite" + i, Username = "用户名", Password = "密码" });
                _idList.Add(i);
            }
        }
    }
}
