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
        private bool _errorMail;
        private bool _errorLogin;
        private bool _success;
        private string _password;
        private string _username;
        private string _email;
        private string _confirmPassword;
        private bool _error = false;

        public IMvxCommand SignUp => new MvxAsyncCommand(HandleSignUp);
        public IMvxCommand ChangeStateCommand => new MvxCommand(InvertCheckBox);
        public IMvxCommand GoBack => new MvxCommand(GoBackClicked);

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
            get => !_success;
        }

        public RegisterViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService, IRegisterService registerService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _registerService = registerService;
        }

        private void GoBackClicked()
        {
            _navigationService.Navigate<LoginViewModel>();
        }

        private void InvertCheckBox()
        {
            RememberMe = !RememberMe;
        }

        private async Task HandleSignUp()
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
        }

        private void ReloadActive()
        {
            Active = CheckActive();
        }

        private bool CheckActive()
        {
            var fields = new List<string> { Password, ConfirmPassword, Username, Email };
            if (fields.Any(field => string.IsNullOrEmpty(field))) return false; 
            if (Password.Length < 4) return false;
            if (ConfirmPassword != Password) return false;
            if (Email.Length < 5) return false;
            if (Email.Split('@').Length != 2) return false;
            if (!Email.Split('@').All(val => val.Length > 0)) return false;
            return true;
        }
    }
}