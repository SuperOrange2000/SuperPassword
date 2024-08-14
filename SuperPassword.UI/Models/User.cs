using CommunityToolkit.Mvvm.ComponentModel;
using Mapster;
using SuperPassword.Entity.Interface;
using System;


namespace SuperPassword.UI.Models
{
    public partial class User : ObservableObject, IUser
    {
        [ObservableProperty]
        private uint id;

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private string? email;

        [ObservableProperty]
        private string? password;

        [ObservableProperty]
        private string? nickName;

        [ObservableProperty]
        private byte[] salt;

        [ObservableProperty]
        private string? token;

        public Guid UserGuid { get; set; } = Guid.NewGuid();

        public User() { }

        public User(IUser user)
        {
            user.Adapt(this);
        }
    }
}
