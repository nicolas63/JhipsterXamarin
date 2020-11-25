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
        public IMvxAsyncCommand LoginViewModelCommand { get; }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; }

        public HomeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowEntitiesViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MyEntityViewModel>());
            LoginViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
        }
    }
}