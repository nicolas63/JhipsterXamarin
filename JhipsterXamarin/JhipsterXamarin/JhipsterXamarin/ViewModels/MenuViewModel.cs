using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace JhipsterXamarin.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public IMvxCommand ShowMyEntitiesCommand { get; }
        public IMvxCommand ShowLoginCommand { get; }

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowMyEntitiesCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MyEntityViewModel>());
            ShowLoginCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginViewModel>());
        }
    }
}
