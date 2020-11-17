using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace JhipsterXamarin.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        public IMvxCommand ShowMyEntitiesCommand { get; }
        public IMvxCommand ShowWelcomeCommand { get; }
        public IMvxCommand SignIn { get; }
        public IMvxCommand SignUp { get; }
        public IMvxCommand SignOut { get; }
        public bool IsConnected { get => _authenticationService.IsAuthenticated; }
        public bool IsNotConnected { get => !_authenticationService.IsAuthenticated; }

        public MenuViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            
            ShowMyEntitiesCommand = new MvxCommand(async () => await _navigationService.Navigate<MyEntityViewModel>());
            ShowWelcomeCommand = new MvxCommand(async () => await _navigationService.Navigate<WelcomeViewModel>());
            SignIn = new MvxCommand(async () => await _navigationService.Navigate<LoginViewModel>());
            SignUp = new MvxCommand(async () => await _navigationService.Navigate<RegisterViewModel>());
            SignOut = new MvxCommand(() => {
                _authenticationService.SignOut();
                RaisePropertyChanged(() => IsNotConnected);
                RaisePropertyChanged(() => IsConnected);
                _navigationService.Navigate<WelcomeViewModel>();
            });            
        }
    }
}
