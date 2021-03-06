﻿using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace JhipsterXamarin.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public IMvxAsyncCommand ShowMenuViewModelCommand => new MvxAsyncCommand(ShowMenuViewModelCommandClicked);
        public IMvxAsyncCommand ShowWelcomeViewModelCommand => new MvxAsyncCommand(ShowWelcomeViewModelCommandClicked);

        public HomeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private async Task ShowWelcomeViewModelCommandClicked()
        {
            await _navigationService.Navigate<WelcomeViewModel>();
        }

        private async Task ShowMenuViewModelCommandClicked()
        {
            await _navigationService.Navigate<MenuViewModel>();
        }
    }
}