using System;
using JhipsterXamarin.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace JhipsterXamarin.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, NoHistory = true)]
    public partial class HomeView : MvxMasterDetailPage<HomeViewModel>
    {
        private bool _firstTime = true;

        public HomeView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (_firstTime)
            {
                ViewModel.ShowMenuViewModelCommand.Execute(null);
                ViewModel.ShowEntitiesViewModelCommand.Execute(null);

                _firstTime = false;
            }

            base.OnAppearing();
        }
    }
}