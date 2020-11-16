using System;
using System.Threading.Tasks;
using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace JhipsterXamarin.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public IMvxAsyncCommand ShowEntitiesViewModelCommand { get; }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; }
        public IMvxAsyncCommand ShowWelcomeViewModelCommand { get;  }

        public HomeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowWelcomeViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<WelcomeViewModel>());
            ShowEntitiesViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MyEntityViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
        }
    }
}