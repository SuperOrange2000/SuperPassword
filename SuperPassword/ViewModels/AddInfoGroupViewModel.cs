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


        AddInfoGroupViewModel()
        {
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
                Model = new InfoGroupDTO();

            if (parameters.ContainsKey("Title"))
                Title = parameters.GetValue<string>("Title");
            else
                Title = "新建项";
        }
    }
}
