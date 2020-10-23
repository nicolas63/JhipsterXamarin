using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
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

        readonly IAuthenticationService _authenticationService;

        public LoginViewModel(IMvxNavigationService navigationService)
        {
            _authenticationService = new AuthenticationService(new HttpClient());
            SignIn = new MvxCommand(async () => {
                await signIn();               
            });
        }

        public Task<bool> signIn()
        {          
            var model = new LoginModel();
            model.Username = Username;
            model.Password = Password;
            model.RememberMe = RememberMe;
            return _authenticationService.SignIn(model);            
        }

        private string _username;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged(() => Username);
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

        public override async Task Initialize()
        {
            await base.Initialize();
        }
    }
}
