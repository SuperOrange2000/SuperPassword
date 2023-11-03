using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using SuperPassword.Service;
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

        private readonly IOnlineService _onlineService;

        public DelegateCommand<UserDto> LoginCommand { get; private set; }

        public LoginViewModel(IContainerProvider provider)
        {
            LoginCommand = new DelegateCommand<UserDto>(Login);
            _onlineService = provider.Resolve<OnlineService>();
        }

        private void Login(UserDto user)
        {
            var result = _onlineService.Login(user);
        }
    }
}
