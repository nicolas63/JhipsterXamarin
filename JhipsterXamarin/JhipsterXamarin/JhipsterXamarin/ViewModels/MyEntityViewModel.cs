using System.Collections.Generic;
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
        private readonly IMyEntityService<MyEntityModel> _myEntityService;
        private readonly IMvxNavigationService _navigationService;

        private int _age;
        private MyEntityModel _currentElement;
        private List<MyEntityModel> _listElement;
        private string _name;

        public IMvxCommand AddCommand => new MvxAsyncCommand(AddCommandClicked);
        public IMvxCommand RemoveCommand => new MvxAsyncCommand(RemoveCommandClicked);
        public IMvxCommand EditCommand => new MvxAsyncCommand(EditCommandClicked);

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

        public MyEntityViewModel(IMvxNavigationService navigationService, IMyEntityService<MyEntityModel> myEntityService)
        {
            _navigationService = navigationService;
            _myEntityService = myEntityService;
        }

        public async Task AddCommandClicked()
        {
            var entity = new MyEntityModel
            {
                Id = null,
                Name = Name,
                Age = Age
            };

            await _myEntityService.CreateEntity(entity);
            await RefreshList();
        }

        public async Task RemoveCommandClicked()
        {
            await _myEntityService.DeleteEntity(CurrentElement.Id);
            await RefreshList();
        }

        public async Task EditCommandClicked()
        {
            CurrentElement.Age = Age;
            CurrentElement.Name = Name;
            await _myEntityService.UpdateEntity(CurrentElement);
            await RefreshList();
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