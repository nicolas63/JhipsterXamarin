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
        private readonly IUserEntityService<UserModel> _userService;
        private readonly IMvxNavigationService _navigationService;

        private UserModel _currentElement;
        private List<UserModel> _userModels = new List<UserModel>();

        public IMvxCommand AddCommand { get; }
        public IMvxCommand RemoveCommand { get; }
        public IMvxCommand EditCommand { get; }

        private UserModel _currentUser;
        public UserModel GetCurrentUser()
        {
            return _currentUser;
        }
        public async Task SetCurrentUserAsync(UserModel model)
        {
            _currentUser = model;
            _userModels.Add(model);
            await _userService.Update(_currentUser);
        }

        private async Task ActiveUser(UserModel user, bool activated)
        {
            _currentUser = user;
            _currentUser.Activated = activated;
            await _userService.Update(user);          
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
            get => _currentElement.Login;
            set
            {
                _currentElement.Login = value;
                RaisePropertyChanged(() => Login);
            }
        }
        public string FirstName
        {
            get => _currentElement.FirstName;
            set
            {
                _currentElement.FirstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        public string LastName
        {
            get => _currentElement.LastName;
            set
            {
                _currentElement.LastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        public UserEntityViewModel(IMvxNavigationService navigationService, IUserEntityService<UserModel> userService)
        {
            _navigationService = navigationService;
            _userService = userService;

            AddCommand = new MvxCommand(async () =>
            {
                await _userService.Add(CurrentElement);
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
