using System.Linq;
using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using JhipsterXamarin.Constants;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.ViewModels
{
     public class MenuViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;

        public IMvxCommand ShowMyUsersCommand => new MvxAsyncCommand(ShowMyUsersCommandClicked);
        public IMvxCommand ShowMyEntitiesCommand => new MvxAsyncCommand(ShowMyEntitiesCommandClicked);
        public IMvxCommand ShowWelcomeCommand => new MvxAsyncCommand(ShowWelcomeCommandClicked);
        public IMvxCommand SignIn => new MvxAsyncCommand(SignInClicked);
        public IMvxCommand SignUp => new MvxAsyncCommand(SignUpClicked);
        public IMvxCommand SignOut => new MvxAsyncCommand(SignOutClicked);
        public bool IsConnected  => _authenticationService.IsAuthenticated;
        
        public UserModel User => IsConnected ? _authenticationService.CurrentUser : null;
        public bool IsAdmin => IsConnected && User != null && User.Authorities.Contains(RolesConstants.ADMIN);

        public MenuViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;          
        }

        private async Task ShowMyEntitiesCommandClicked()
        {
            await _navigationService.Navigate<MyEntityViewModel>();
        }

        private async Task ShowMyUsersCommandClicked()
        {
            await _navigationService.Navigate<UserEntityViewModel>();
        }

        private async Task ShowWelcomeCommandClicked()
        {
            await _navigationService.Navigate<WelcomeViewModel>();
        }

        private async Task SignInClicked()
        {
            await _navigationService.Navigate<LoginViewModel>();
        }

        private async Task SignUpClicked()
        {
            await _navigationService.Navigate<RegisterViewModel>();
        }

        private async Task SignOutClicked()
        {
            _authenticationService.SignOut();
            await RaisePropertyChanged(() => IsConnected);
            await _navigationService.Navigate<WelcomeViewModel>();
        }
    }
}
