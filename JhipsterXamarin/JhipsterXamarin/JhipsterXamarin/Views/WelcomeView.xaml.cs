using System;
using JhipsterXamarin.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace JhipsterXamarin.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class WelcomeView : MvxContentPage<WelcomeViewModel>
    {
        public WelcomeView()
        {
            InitializeComponent();
        }
    }
}