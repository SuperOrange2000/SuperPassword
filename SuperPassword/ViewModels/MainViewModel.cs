using Prism.Commands;
using Prism.Mvvm;
using SuperPassword.Common.Models;
using SuperPassword.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private ObservableCollection<PWDto> _pwDtos;
        public ObservableCollection<PWDto> PWDtos
        {
            get { return _pwDtos; }
            set { _pwDtos = value; RaisePropertyChanged(); }
        }

        private readonly IOfflineService OfflineService;

        public DelegateCommand<PWDto> DeleteCommand { get; private set; }

        public MainViewModel(IOfflineService offlineService)
        {
            this.OfflineService = offlineService;

            PWDtos = new ObservableCollection<PWDto>();

            DeleteCommand = new DelegateCommand<PWDto>(Delete);

            CreateTestToDoList();
        }

        private void Delete(PWDto obj)
        {
            OfflineService.DeleteAsync(obj.ID);
            var model = PWDtos.FirstOrDefault(t => t.ID.Equals(obj.ID));
            if (model != null)
                PWDtos.Remove(model);
        }

        void CreateTestToDoList()
        {
            for (int i = 0; i < 10; i++)
            {
                PWDtos.Add(new PWDto() { ID = i, Website = "TestWebsite" + i, Username = "用户名", Password = "密码" });
            }
        }
    }
}
