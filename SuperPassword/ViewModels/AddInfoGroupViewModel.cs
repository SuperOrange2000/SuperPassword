using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SuperPassword.Entity.Data;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SuperPassword.ViewModels
{
    class AddInfoGroupViewModel : BindableBase, IDialogAware, INotifyPropertyChanged
    {
        private InfoGroupEntity _model;
        public InfoGroupEntity Model
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

        private bool _isInputing = false;

        public bool IsInputing 
        { 
            set 
            { 
                _isInputing = value; 
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(newTagButtonVisibility))); 
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(newTagInputBoxVisibility))); 
            } 
        }

        public Visibility newTagButtonVisibility 
        { 
            get { return _isInputing ? Visibility.Collapsed : Visibility.Visible; } 
        }
        public Visibility newTagInputBoxVisibility 
        { 
            get { return _isInputing ? Visibility.Visible : Visibility.Collapsed; } 
        }

        private string _newTagText;

        public string NewTagText
        {
            get { return _newTagText; }
            set { _newTagText = value; RaisePropertyChanged(); }
        }


        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand StartInputingCommand { get; set; }
        public DelegateCommand EndInputingCommand { get; set; }


        AddInfoGroupViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
            StartInputingCommand = new DelegateCommand(StartInputing);
            EndInputingCommand = new DelegateCommand(EndInputing);
        }

        private void Cancel()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
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
            RequestClose.Invoke(new DialogResult(ButtonResult.OK, param));
        }

        private void StartInputing()
        {
            IsInputing = true;
        }

        private void EndInputing()
        {
            IsInputing = false;
            byte maxId = Model.TagEntities.Max(t => t.NonceID);
            Model.TagEntities.Add(new Entity.TagEntity(++maxId) { Salt = Model.Salt, Content=NewTagText });
            NewTagText = string.Empty;
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
                Model = parameters.GetValue<InfoGroupEntity>("Value");
            }
            else
            {
                Model = new InfoGroupEntity();
                if (parameters.ContainsKey("ID"))
                    Model.ID = parameters.GetValue<uint>("ID");
            }

            if (parameters.ContainsKey("Title"))
                Title = parameters.GetValue<string>("Title");
            else
                Title = "修改";
        }
    }
}
