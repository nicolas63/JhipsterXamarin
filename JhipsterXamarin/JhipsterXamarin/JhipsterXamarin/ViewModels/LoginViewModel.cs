using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using JhipsterXamarin.Views;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JhipsterXamarin.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        public MvvmCross.Commands.IMvxCommand SignIn { get; set; }

        public IMvxAsyncCommand Navigate { get; private set; }

        private IAuthenticationService _authenticationService;

        private IMvxNavigationService _navigationService;

        private HttpClient _httpClient;

        private string _username;

        private bool _active = false;

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                RaisePropertyChanged(() => Active);
            }
        }

        private bool _enabled = false;

        public bool Enabled {
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

        private string _password;
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

        private bool _rememberMe;
        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                _rememberMe = value;
                RaisePropertyChanged(() => RememberMe);
            }
        }

        public string FirstName
        {
            get => _authenticationService.CurrentUser.FirstName;
        }

        public LoginViewModel(IMvxNavigationService navigationService, HttpClient httpClient)
        {
            _navigationService = navigationService;

            _httpClient = httpClient;
            _authenticationService = new AuthenticationService(_httpClient);

            Navigate = new MvxAsyncCommand(() =>
            _navigationService.Navigate<MyEntityViewModel>());

            SignIn = new MvxCommand(async () => {
                Enabled = !(await signIn());  
            });
        }

        public void ReloadActive()
        {
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username))
                Active = false;
            else
                Active = (Password.Length > 3);
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
