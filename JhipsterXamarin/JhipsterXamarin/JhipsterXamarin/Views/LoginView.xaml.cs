using JhipsterXamarin.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace JhipsterXamarin.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class LoginView : MvxContentPage<LoginViewModel>
    {
        public LoginView()
        {
            InitializeComponent();
        }
    }
}