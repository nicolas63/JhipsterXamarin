using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SharedModel.Constants;
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
        private bool _error;
        private bool _errorMail;
        private bool _errorLogin;
        private bool _success;


        private string _password;
        private string _username;
        private string _email;
        private string _confirmPassword;

        public IMvxCommand SignUp { get; }
        public IMvxCommand GoBack { get;  }
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

        public bool Error
        {
            get => _error;
            set
            {
                _error = value;
                RaisePropertyChanged(() => Error);
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                RaisePropertyChanged(() => ConfirmPassword);
                ReloadActive();
            }
        }

        public bool ErrorMail
        {
            get => _errorMail;
            set
            {
                _errorMail = value;
                RaisePropertyChanged(() => ErrorMail);
            }
        }

        public bool ErrorLogin
        {
            get => _errorLogin;
            set
            {
                _errorLogin = value;
                RaisePropertyChanged(() => ErrorLogin);
            }
        }

        public bool Success
        {
            get => _success;
            set
            {
                _success = value;
                RaisePropertyChanged(() => Success);
                RaisePropertyChanged(() => NotSuccess);
            }
        }

        public bool NotSuccess
        {
            get => !Success;
        }

        public RegisterViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService, IRegisterService registerService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _registerService = registerService;

            Error = false;

            SignUp = new MvxCommand(async () =>
            {
                Active = false;
                var resultError = await _registerService.Save(new UserSaveModel
                {
                    Password = Password,
                    Login = Username,
                    Email = Email,
                    LangKey = "en"
                });

                Error = resultError == ErrorConst.ProblemBaseUrl;
                ErrorLogin = resultError == ErrorConst.LoginAlreadyUsedType;
                ErrorMail = resultError == ErrorConst.EmailAlreadyUsedType;

                Success = (!new List<bool>() { Error, ErrorLogin, ErrorMail }.Contains(true));
            });

            GoBack = new MvxCommand(() =>
            {
                _navigationService.Navigate<LoginViewModel>();
            });

            ChangeStateCommand = new MvxCommand(() =>
            {
                RememberMe = !RememberMe;
            });
        }

        public void ReloadActive()
        {
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username))
                Active = false;
            else
            {
                var emailSplit = Email.Split('@');
                Active = Password.Length > 3 && (ConfirmPassword == Password) && Email.Length > 4 && emailSplit.Length == 2 &&
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