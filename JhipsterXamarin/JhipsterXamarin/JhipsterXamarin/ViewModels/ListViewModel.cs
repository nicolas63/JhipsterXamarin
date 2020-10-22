using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using JhipsterXamarin.Services;

namespace JhipsterXamarin.ViewModels
{
    public class ListViewModel : MvxViewModel
    {
        public MvvmCross.Commands.IMvxCommand AddCommand { get; set; }
        public MvvmCross.Commands.IMvxCommand RemoveCommand { get; set; }

        readonly IListService _listService;

        public ListViewModel(IListService listService)
        {
            _listService = listService;
            AddCommand = new MvxCommand(() =>
            {
                NbElement++;
            });
            RemoveCommand = new MvxCommand(() =>
            {
                NbElement--;
            });

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
