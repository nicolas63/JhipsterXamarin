using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace JhipsterXamarin.ViewModels
{
    public class MyEntityViewModel : MvxViewModel
    {
        private readonly HttpClient _httpClient;
        private int _age;
        private MyEntityModel _currentElement;
        private List<MyEntityModel> _listElement;
        private readonly IMyEntityService _myEntityService;
        private string _name;
        private IMvxNavigationService _navigationService;

        public MyEntityViewModel(IMvxNavigationService navigationService, HttpClient httpClient)
        {
            _navigationService = navigationService;
            _httpClient = httpClient;
            _myEntityService = new MyEntityService(_httpClient);

            AddCommand = new MvxCommand(async () =>
            {
                await _myEntityService.CreateEntity(Name, Age);
                await RefreshList();
            });
            RemoveCommand = new MvxCommand(async () =>
            {
                await _myEntityService.DeleteEntity(CurrentElement);
                await RefreshList();
            });
            EditCommand = new MvxCommand(async () =>
            {
                CurrentElement.Age = Age;
                CurrentElement.Name = Name;
                await _myEntityService.UpdateEntity(CurrentElement);
                await RefreshList();
            });
        }

        public IMvxCommand AddCommand { get; }
        public IMvxCommand RemoveCommand { get; }
        public IMvxCommand EditCommand { get; }

        public List<MyEntityModel> ListElement
        {
            get => _listElement;
            set
            {
                _listElement = value;
                RaisePropertyChanged(() => ListElement);
            }
        }

        public MyEntityModel CurrentElement
        {
            get => _currentElement;
            set
            {
                _currentElement = value;
                if (_currentElement != null)
                {
                    Name = _currentElement.Name;
                    Age = _currentElement.Age;
                }

                RaisePropertyChanged(() => CurrentElement);
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                RaisePropertyChanged(() => Age);
            }
        }

        public async Task RefreshList()
        {
            ListElement = await _myEntityService.GetEntities();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await RefreshList();
        }
    }
}