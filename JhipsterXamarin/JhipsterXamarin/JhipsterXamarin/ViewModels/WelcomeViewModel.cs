using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Xamarin.Essentials;

namespace JhipsterXamarin.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;

        public IMvxCommand OpenHomepage => new MvxCommand(OpenHomepageClicked);
        public IMvxCommand OpenStack => new MvxCommand(OpenStackClicked);
        public IMvxCommand OpenBugTracker => new MvxCommand(OpenBugTrackerClicked);
        public IMvxCommand OpenChatRoom => new MvxCommand(OpenChatRoomClicked);
        public IMvxCommand OpenTwitter => new MvxCommand(OpenTwitterClicked);
        public IMvxCommand OpenGitHub => new MvxCommand(OpenGitHubClicked);
        public IMvxCommand SignIn => new MvxCommand(SignInClicked);
        public IMvxCommand SignUp => new MvxCommand(SignUpClicked);
        public bool IsConnected => _authenticationService.IsAuthenticated;
        public string Username => (IsConnected) ? _authenticationService.CurrentUser.Login : null;

        public WelcomeViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
        }

        private void OpenHomepageClicked()
        {
            Launcher.OpenAsync("https://www.jhipster.tech/");
        }

        private void OpenStackClicked()
        {
            Launcher.OpenAsync("http://stackoverflow.com/tags/jhipster/info");
        }

        private void OpenBugTrackerClicked()
        {
            Launcher.OpenAsync("https://github.com/jhipster/generator-jhipster/issues?state=open");
        }

        private void OpenChatRoomClicked()
        {
            Launcher.OpenAsync("https://gitter.im/jhipster/generator-jhipster");
        }

        private void OpenTwitterClicked()
        {
            Launcher.OpenAsync("https://twitter.com/jhipster");
        }

        private void OpenGitHubClicked()
        {
            Launcher.OpenAsync("https://github.com/jhipster/generator-jhipster");
        }

        private void SignInClicked()
        {
            _navigationService.Navigate<LoginViewModel>();
        }

        private void SignUpClicked()
        {
            _navigationService.Navigate<RegisterViewModel>();
        }
    }
}