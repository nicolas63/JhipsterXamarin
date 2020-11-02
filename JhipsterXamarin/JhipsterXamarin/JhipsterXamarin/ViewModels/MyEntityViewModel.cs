using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JhipsterXamarin.ViewModels
{
    public class MyEntityViewModel : MvxViewModel<HttpClient>
    {
        private IMyEntityService _myEntityService;
        private IMvxNavigationService _navigationService;

        public MvvmCross.Commands.IMvxCommand AddCommand { get; set; }
        public MvvmCross.Commands.IMvxCommand RemoveCommand { get; set; }

        private List<MyEntityModel> _listElement;
        public List<MyEntityModel> ListElement
        {
            get => _listElement;
            set
            {
                _listElement = value;
                RaisePropertyChanged(() => ListElement);
            }
        }

        private HttpClient _httpClient;

        public MyEntityViewModel(IMvxNavigationService navigationService)
        {            
            _navigationService = navigationService;
            AddCommand = new MvxCommand(async () =>
            {
                // Add a new MyEntity
            });
        }

        public override void Prepare(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _myEntityService = new MyEntityService(_httpClient);
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            ListElement = await _myEntityService.GetEntities();
        }
    }
}
