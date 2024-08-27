using CommunityToolkit.Mvvm.ComponentModel;
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

        [ObservableProperty]
        private ObservableCollection<string> editingTags = [];

        public IList<string>? Tags
        {
            get => [.. EditingTags];
            set
            {
                EditingTags = new ObservableCollection<string>(value == null ? [] :
                new ObservableCollection<string>(value));

                ViewTags = new ObservableCollection<Tag>(value == null ? [] :
                new ObservableCollection<Tag>(value.Select(tagContent => new Tag { Content = tagContent })));

                InitTagsChangedMethod();
            }
        }

        public InfoGroup()
        {
            InitTagsChangedMethod();
        }

        public InfoGroup(IInfoGroup infoGroup) : this()
        {
            InfoGroupGuid = infoGroup.InfoGroupGuid;
            Site = infoGroup.Site;
            Username = infoGroup.Username;
            Password = infoGroup.Password;
            Tags = infoGroup.Tags;
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

        private void InitTagsChangedMethod()
        {
            ViewTags.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
                {
                    foreach (var item in e.NewItems)
                        if (item is INotifyPropertyChanged tag)
                            tag.PropertyChanged += OnTagSelectedChanged;
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
                    foreach (var item in e.OldItems)
                        if (item is INotifyPropertyChanged tag)
                            tag.PropertyChanged -= OnTagSelectedChanged;
            };

            EditingTags.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
                    foreach (var item in e.NewItems)
                    {
                        if (item is string tagContent)
                        {
                            var newTag = new Tag { Content = tagContent };
                            ViewTags.Add(newTag);
                        }
                    }
                else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
                {
                    for (int i = 0; i < ViewTags.Count; i++)
                    {
                        if (e.OldItems.Contains(ViewTags[i].Content))
                        {
                            ViewTags.RemoveAt(i);
                        }
                    }
                }
            };
        }
    }
}
