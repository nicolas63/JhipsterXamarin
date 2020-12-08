using System.Threading.Tasks;
using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace JhipsterXamarin.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMvxNavigationService _navigationService;

        private bool _active;
        private bool _rememberMe;
        private string _password;
        private string _username;
        private bool _success = true;

        public IMvxCommand SignIn => new MvxAsyncCommand(SignInClicked);
        public IMvxCommand SignUp => new MvxAsyncCommand(SignUpClicked);
        public IMvxCommand ChangeStateCommand => new MvxCommand(InvertCheckBox);

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                RaisePropertyChanged(() => Active);
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged(() => Username);
                ReloadActive();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
                ReloadActive();
            }
        }

        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                _rememberMe = value;
                RaisePropertyChanged(() => RememberMe);
            }
        }

        public bool Success
        {
            get => _success;
            set
            {
                _success = value;
                RaisePropertyChanged(() => Success);
            }
        }

        public LoginViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
        }

        private void InvertCheckBox()
        {
            RememberMe = !RememberMe;
        }

        private async Task SignUpClicked()
        {
            await _navigationService.Navigate<RegisterViewModel>();
        }

        private async Task SignInClicked()
        {
            Active = false;
            Success = await SignInConnection();
            if (Success) await _navigationService.Navigate<HomeViewModel>();
        }

        public void ReloadActive()
        {
            Active = !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Username) && Password.Length > 3;
        }

        public Task<bool> SignInConnection()
        {
            var model = new LoginModel
            {
                Username = Username,
                Password = Password,
                RememberMe = RememberMe
            };
            return _authenticationService.SignIn(model);
        }
    }
}