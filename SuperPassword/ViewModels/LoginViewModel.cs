using Prism.Mvvm;
using SuperPassword.Shared.Dtos;
using System.Collections.ObjectModel;
namespace SuperPassword.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private UserDto _activeUser;

        public UserDto ActiveUser
        {
            get { return _activeUser; }
            set { _activeUser = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<UserDto> _allUsers;

        public ObservableCollection<UserDto> AllUsers
        {
            get { return _allUsers; }
            set { _allUsers = value; RaisePropertyChanged(); }
        }

        //public DelegateCommand<InfoGroupDTO> DeleteCommand { get; private set; }
    }
}
