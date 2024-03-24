using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SuperPassword.Entity.Data;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace SuperPassword.ViewModels
{
    internal class AddInfoGroupViewModel : BindableBase, IDialogAware, INotifyPropertyChanged
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

        private static Random _rng = new Random();
        private static string _randomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand StartInputingCommand { get; set; }
        public DelegateCommand EndInputingCommand { get; set; }
        public DelegateCommand<string> RandomFillCommand { get; set; }

        private AddInfoGroupViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
            StartInputingCommand = new DelegateCommand(StartInputing);
            EndInputingCommand = new DelegateCommand(EndInputing);
            RandomFillCommand = new DelegateCommand<string>(RandomFill);
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
            Model.TagEntities.Add(new TagEntity(++maxId) { Salt = Model.Salt, Content = NewTagText });
            NewTagText = string.Empty;
        }

        private void RandomFill(string boxName)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                builder.Append(_randomChars[_rng.Next(_randomChars.Length)]);
            }
            if (boxName == "username")
                Model.Username = builder.ToString();
            else if (boxName == "password")
                Model.Password = builder.ToString();
            else
                throw new NotImplementedException();
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
