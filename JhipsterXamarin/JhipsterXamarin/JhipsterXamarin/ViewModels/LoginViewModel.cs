using System.Net.Http;
using System.Threading.Tasks;
using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace JhipsterXamarin.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly HttpClient _httpClient;
        private bool _active;
        private readonly IAuthenticationService _authenticationService;
        private bool _enabled;
        private readonly IMvxNavigationService _navigationService;
        private string _password;
        private bool _rememberMe;
        private string _username;

        public LoginViewModel(IMvxNavigationService navigationService, HttpClient httpClient)
        {
            _navigationService = navigationService;

            _httpClient = httpClient;
            _authenticationService = new AuthenticationService(_httpClient);

            Navigate = new MvxAsyncCommand(() =>
                _navigationService.Navigate<MyEntityViewModel>());

            SignIn = new MvxCommand(async () => { Enabled = !await signIn(); });
        }

        public IMvxCommand SignIn { get; }
        public IMvxAsyncCommand Navigate { get; }

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                RaisePropertyChanged(() => Active);
            }
        }

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                RaisePropertyChanged(() => Enabled);
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

        public void ReloadActive()
        {
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username))
                Active = false;
            else
                Active = Password.Length > 3;
        }

        public Task<bool> signIn()
        {
            var model = new LoginModel();
            model.Username = Username;
            model.Password = Password;
            model.RememberMe = RememberMe;
            return _authenticationService.SignIn(model);
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }
    }
}