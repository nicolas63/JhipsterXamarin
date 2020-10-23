using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using JhipsterXamarin.Services;
using MvvmCross.Navigation;
using System.Net.Http;

namespace JhipsterXamarin.ViewModels
{
    public class ListViewModel : MvxViewModel
    {
        public MvvmCross.Commands.IMvxCommand AddCommand { get; set; }
        public MvvmCross.Commands.IMvxCommand RemoveCommand { get; set; }

        private readonly IMvxNavigationService _navigationService;

        readonly IListService _listService;

        public IMvxAsyncCommand NavigateToSecondPageCommand { get; private set; }

        public ListViewModel(IMvxNavigationService navigationService, IListService listService)
        {
            _navigationService = navigationService;

            _listService = listService;
            AddCommand = new MvxCommand(() =>
            {
                NbElement++;
            });
            RemoveCommand = new MvxCommand(() =>
            {
                NbElement--;
            });

            NavigateToSecondPageCommand = new MvxAsyncCommand(() => 
            _navigationService.Navigate<LoginViewModel>());

        }

        public override async Task Initialize()
        {
            await base.Initialize();

            _nbElement = 5;

            Regenerate();
        }

        private int _nbElement;
        public int NbElement
        {
            get => _nbElement;
            set
            {
                _nbElement = value;
                RaisePropertyChanged(() => NbElement);
                Regenerate();
            }
        }

        private List<string> _listElement;
        public List<string> ListElement
        {
            get => _listElement;
            set
            {
                _listElement = value;
                RaisePropertyChanged(() => ListElement);
            }
        }

        private void Regenerate()
        {
            ListElement = _listService.GenerateList(NbElement);
        }
    }
}
