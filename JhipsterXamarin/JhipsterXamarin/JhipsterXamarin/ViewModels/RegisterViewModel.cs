using System.Linq;
using System.Threading.Tasks;
using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace JhipsterXamarin.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMvxNavigationService _navigationService;
        private readonly IRegisterService _registerService;

        private bool _active;
        private bool _rememberMe;
        private string _password;
        private string _username;
        private string _email;

        public IMvxCommand SignUp { get; }
        public IMvxCommand GoBack { get;  }

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

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
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

        public RegisterViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService, IRegisterService registerService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _registerService = registerService;

            SignUp = new MvxCommand(async () =>
            {
                Active = false;
                var result = await _registerService.Save(new UserSaveModel
                {
                    Password = Password,
                    Login = Username,
                    Email = Email,
                    LangKey = "en"
                });
                
                if (result.IsSuccessStatusCode)
                {
                    var success = await SignInConnection();
                    if (success) await _navigationService.Navigate<HomeViewModel>();
                }
                Active = true;
            });
            GoBack = new MvxCommand(() =>
            {
                _navigationService.Navigate<LoginViewModel>();
            });
        }

        public void ReloadActive()
        {
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username))
                Active = false;
            else
            {
                var emailSplit = Email.Split('@');
                Active = Password.Length > 3 && Email.Length > 4 && emailSplit.Length == 2 &&
                         emailSplit.All(val => val.Length > 0);
            }
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