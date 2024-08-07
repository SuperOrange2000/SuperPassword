using CommunityToolkit.Mvvm.ComponentModel;
using SuperPassword.Entity.Interface;
using Mapster;


namespace SuperPassword.UI.Models
{
    public partial class User : ObservableObject, IUser
    {
        [ObservableProperty]
        private int id;

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

        public User() { }

        public User(IUser user) 
        {
            this.Adapt(user);
        }
    }
}
