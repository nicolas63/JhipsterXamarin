﻿using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Threading.Tasks;

namespace JhipsterXamarin.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;

        public IMvxCommand ShowMyEntitiesCommand => new MvxAsyncCommand(ShowMyEntitiesCommandClicked);
        public IMvxCommand ShowWelcomeCommand => new MvxAsyncCommand(ShowWelcomeCommandClicked);
        public IMvxCommand SignIn => new MvxAsyncCommand(SignInClicked);
        public IMvxCommand SignUp => new MvxAsyncCommand(SignUpClicked);
        public IMvxCommand SignOut => new MvxAsyncCommand(SignOutClicked);
        public bool IsConnected  => _authenticationService.IsAuthenticated;

        public MenuViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;          
        }

        private async Task ShowMyEntitiesCommandClicked()
        {
            await _navigationService.Navigate<MyEntityViewModel>();
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