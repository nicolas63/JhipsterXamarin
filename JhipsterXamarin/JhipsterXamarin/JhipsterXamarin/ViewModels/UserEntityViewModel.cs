using JhipsterXamarin.Constants;
using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JhipsterXamarin.ViewModels
{
    public class UserEntityViewModel : MvxViewModel
    {
        private readonly IUserEntityService<UserModel> _userService;
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;

        private UserModel _currentElement = new UserModel();
        private List<UserModel> _userModels = new List<UserModel>();
        private IList<string>_listSelectedRoles = new List<string>();

        public IMvxCommand AddCommand => new MvxAsyncCommand(AddCommandClicked);
        public IMvxCommand RemoveCommand => new MvxAsyncCommand(RemoveCommandClicked);
        public IMvxCommand EditCommand => new MvxAsyncCommand(EditCommandClicked);

        public bool IsConnected => _authenticationService.IsAuthenticated;
        public string Username => (IsConnected) ? _authenticationService.CurrentUser.Login : null;

        public List<string> ListAuth { get; } = new List<string>()
        {
            RolesConstants.ADMIN,
            RolesConstants.ANONYMOUS,
            RolesConstants.USER,
        };

        public IList<string> SelectedRoles
        {
            get => _listSelectedRoles;
            set
            {
                _listSelectedRoles = value;
                RaisePropertyChanged(() => SelectedRoles);
            }
        }

        public List<UserModel> UserModels
        {
            get => _userModels;
            set
            {
                _userModels = value;
                RaisePropertyChanged(() => UserModels);
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
                    Email = _currentElement.Email;
                    SelectedRoles = (IList<string>)_currentElement.Authorities;
                }
                RaisePropertyChanged(() => CurrentElement);
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

        public string Email
        {
            get => _currentElement.Email;
            set
            {
                _currentElement.Email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public UserEntityViewModel(IMvxNavigationService navigationService, IUserEntityService<UserModel> userService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _userService = userService;
            _authenticationService = authenticationService;
        }

        public async Task RefreshList()
        {
            UserModels = (List<UserModel>)await _userService.GetAll();
            if (UserModels != null)
                CurrentElement = _userModels[0];
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await RefreshList();
        }

        public async Task AddCommandClicked()
        {
            var model = new UserModel
            {
                Login = FirstName + "_" + LastName,
                Activated = true,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                CreatedBy = Username,
                LastModifiedDate = DateTime.Now,
                Authorities = SelectedRoles
            };
            await _userService.Add(model);
            await RefreshList();
        }

        public async Task RemoveCommandClicked()
        {
            await _userService.Delete(CurrentElement.Login);
            await RefreshList();
        }

        public async Task EditCommandClicked()
        {
            CurrentElement.FirstName = FirstName;
            CurrentElement.LastName = LastName;
            CurrentElement.Email = Email;
            CurrentElement.LastModifiedDate = DateTime.Now;
            CurrentElement.LastModifiedBy = Username;

            await _userService.Update(CurrentElement);
            await RefreshList();
        }
    }
}
