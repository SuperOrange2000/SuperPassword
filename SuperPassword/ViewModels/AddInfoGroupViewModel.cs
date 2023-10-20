using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SuperPassword.Common;
using Prism.Commands;
using Prism.Services.Dialogs;
using SuperPassword.Shared.Dtos;

namespace SuperPassword.ViewModels
{
    class AddInfoGroupViewModel : BindableBase, IDialogAware
    {
        private InfoGroupDTO _model;
        public InfoGroupDTO Model
        {
            get { return _model; }
            set { _model = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public bool isNew { get; set; }

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }


        AddInfoGroupViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            RequestClose(new DialogResult(ButtonResult.Cancel));
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Model.Password) ||
                string.IsNullOrWhiteSpace(Model.Username)) return;

            //确定时,把编辑的实体返回并且返回OK
            DialogParameters param = new DialogParameters();
            param.Add("Value", Model);
            param.Add("isNew", isNew);
            RequestClose(new DialogResult(ButtonResult.OK, param));
        }

        public event Action<IDialogResult> RequestClose;

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                Model = parameters.GetValue<InfoGroupDTO>("Value");
                isNew = false;
            }
            else
            {
                Model = new InfoGroupDTO();
                isNew = true;
            }

            if (parameters.ContainsKey("Title"))
                Title = parameters.GetValue<string>("Title");
            else
                Title = "新建项";
        }
    }
}
