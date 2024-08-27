using CommunityToolkit.Mvvm.ComponentModel;
using Mapster;
using SuperPassword.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace SuperPassword.UI.Models
{
    public partial class InfoGroup : ObservableObject, IInfoGroup
    {
        private bool visibility = true;
        public bool Visibility
        {
            get { return ViewTags.Any(item => item.IsSelected); }
            set { visibility = value; SetProperty(ref visibility, value); }
        }

        [ObservableProperty]
        private bool isEditable;

        [ObservableProperty]
        private string? site;

        [ObservableProperty]
        private string? username;

        [ObservableProperty]
        private string? password;

        [ObservableProperty]
        private bool isNew = false;

        public Guid InfoGroupGuid { get; set; } = Guid.NewGuid();

        [ObservableProperty]
        private ObservableCollection<Tag> viewTags = [];
        public IList<string>? Tags
        {
            get => ViewTags.Select(item => item.ToString()).ToList();
            set => ViewTags = new ObservableCollection<Tag>(value == null ? [] :
                new ObservableCollection<Tag>(value.Select(item => new Tag() { Content = item })));
        }

        public InfoGroup()
        {
            ViewTags.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add && s is INotifyPropertyChanged tag)
                    tag.PropertyChanged += OnTagSelectedChanged;
                else if (e.Action == NotifyCollectionChangedAction.Remove && s is INotifyPropertyChanged oldTag)
                    oldTag.PropertyChanged -= OnTagSelectedChanged;
            };
        }

        public InfoGroup(IInfoGroup infoGroup) : this()
        {
            infoGroup.Adapt(this);
            for (int i = 0; i < ViewTags.Count; i++)
            {
                viewTags[i].PropertyChanged += OnTagSelectedChanged;
            }
        }

        private void OnTagSelectedChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
                OnPropertyChanged(nameof(Visibility));
        }
    }
}
