using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SuperPassword.Shared.Dtos;
using System;

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
            DialogParameters param = new DialogParameters
            {
                { "Value", Model }
            };
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
            }
            else
            {
                Model = new InfoGroupDTO();
            }

            if (parameters.ContainsKey("Title"))
                Title = parameters.GetValue<string>("Title");
            else
                Title = "修改";
        }
    }
}
