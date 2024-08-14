using CommunityToolkit.Mvvm.ComponentModel;
using Mapster;
using SuperPassword.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SuperPassword.UI.Models
{
    public partial class InfoGroupViewItem : ObservableObject, IInfoGroup
    {
        [ObservableProperty]
        private bool visibility = true;

        [ObservableProperty]
        private bool isEditable;

        [ObservableProperty]
        private uint id;

        [ObservableProperty]
        private string? site;

        [ObservableProperty]
        private string? username;

        [ObservableProperty]
        private string? password;

        [ObservableProperty]
        private bool isNew = false;

        public Guid InfoGroupGuid { get; set; } = Guid.NewGuid();

        private ObservableCollection<string> viewTags = new();

        public ObservableCollection<string> ViewTags
        {
            get => viewTags;
            set => SetProperty(ref viewTags, value);
        }
        public IList<string>? Tags
        {
            get => ViewTags.ToList();
            set => ViewTags = new ObservableCollection<string>(value == null ? [] : value);
        }

        public InfoGroupViewItem() { }

        public InfoGroupViewItem(IInfoGroup infoGroup)
        {
            infoGroup.Adapt(this);
        }
    }
}
