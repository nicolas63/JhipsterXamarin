using System.Threading.Tasks;
using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace JhipsterXamarin.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMvxNavigationService _navigationService;

        private bool _active;
        private bool _rememberMe;
        private bool _success;
        private string _password;
        private string _username;

        public IMvxCommand SignIn { get; }
        public IMvxCommand SignUp { get; }
        public IMvxCommand ChangeStateCommand { get; }

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
                RaisePropertyChanged(() => Failed);
            }
        }

        public bool Failed { get => !_success; }

        public LoginViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;

            Success = true;

            SignIn = new MvxCommand(async () =>
            {
                Active = false;
                Success = await SignInConnection();               
                if (Success) await _navigationService.Navigate<HomeViewModel>();
            });

            SignUp = new MvxCommand(async () =>
            {
                await _navigationService.Navigate<RegisterViewModel>();
            });

            ChangeStateCommand = new MvxCommand(() =>
            {
                RememberMe = !RememberMe;
            });
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

        public override async Task Initialize()
        {
            await base.Initialize();
        }
    }
}