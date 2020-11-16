using System;
using System.Threading.Tasks;
using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JhipsterXamarin.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public IMvxCommand OpenHomepage { get; }
        public IMvxCommand OpenStack { get; }
        public IMvxCommand OpenBugTracker { get; }
        public IMvxCommand OpenChatRoom { get; }
        public IMvxCommand OpenTwitter { get; }
        public IMvxCommand OpenGitHub { get; }
        public IMvxCommand SignIn { get; }
        public IMvxCommand SignUp { get; }

        public WelcomeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            OpenHomepage = new MvxCommand(() =>
            {
                Launcher.OpenAsync("https://www.jhipster.tech/");
            });
            OpenStack = new MvxCommand(() =>
            {
                Launcher.OpenAsync("http://stackoverflow.com/tags/jhipster/info");
            });
            OpenBugTracker = new MvxCommand(() =>
            {
                Launcher.OpenAsync("https://github.com/jhipster/generator-jhipster/issues?state=open");
            });
            OpenChatRoom = new MvxCommand(() =>
            {
                Launcher.OpenAsync("https://gitter.im/jhipster/generator-jhipster");
            });
            OpenTwitter = new MvxCommand(() =>
            {
                Launcher.OpenAsync("https://twitter.com/jhipster");
            });
            OpenGitHub = new MvxCommand(() =>
            {
                Launcher.OpenAsync("https://github.com/jhipster/generator-jhipster");
            });
            SignIn = new MvxCommand(() =>
            {
                _navigationService.Navigate<LoginViewModel>();
            });
            SignUp = new MvxCommand(() =>
            {
                _navigationService.Navigate<RegisterViewModel>();
            });
        }

    }
}