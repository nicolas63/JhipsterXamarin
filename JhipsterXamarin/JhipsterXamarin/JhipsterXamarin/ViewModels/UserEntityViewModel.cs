using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JhipsterXamarin.ViewModels
{
    public class UserEntityViewModel : MvxViewModel
    {
        private readonly UserEntityService<UserModel> _userService;
        private readonly IMvxNavigationService _navigationService;

        private UserModel _currentElement;
        private List<UserModel> _userModels;
        private string _login;
        private string _firstName;
        private string _lastName;

        public IMvxCommand AddCommand { get; }
        public IMvxCommand RemoveCommand { get; }
        public IMvxCommand EditCommand { get; }

        private UserModel CurrentUser { get; set; }

        private async Task ActiveUser(UserModel user, bool activated)
        {
            user.Activated = activated;
            await _userService.Update(user);
            CurrentUser = user;
        }

        public List<UserModel> UserModels
        {
            get => _userModels;
            set
            {
                _userModels = value;
                RaisePropertyChanged(() => _userModels);
            }
        }

        public UserModel CurrentElement
        {
            get => _currentElement;
            set
            {
                _currentElement = value;
                if (_currentElement != null)
                {
                    FirstName = _currentElement.FirstName;
                    LastName = _currentElement.LastName;
                }
                RaisePropertyChanged(() => CurrentElement);
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                RaisePropertyChanged(() => Login);
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        public UserEntityViewModel(IMvxNavigationService navigationService, UserEntityService<UserModel> userService)
        {
            _navigationService = navigationService;
            _userService = userService;

            AddCommand = new MvxCommand(async () =>
            {
                await _userService.Add(Login,FirstName,LastName, CurrentUser.FirstName + " | " + CurrentUser.LastName);
                await RefreshList();
            });
            RemoveCommand = new MvxCommand(async () =>
            {
                await _userService.Delete(CurrentElement.Id);
                await RefreshList();
            });
            EditCommand = new MvxCommand(async () =>
            {
                CurrentElement.FirstName = FirstName;
                CurrentElement.LastName = LastName;
                await _userService.Update(CurrentElement);
                await RefreshList();
            });
        }

        public async Task RefreshList()
        {
            UserModels = (List<UserModel>)await _userService.GetAll();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await RefreshList();
        }
    }
}
