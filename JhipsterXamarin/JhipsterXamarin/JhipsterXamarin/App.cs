using System.Net.Http;
using JhipsterXamarin.Services;
using JhipsterXamarin.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;

namespace JhipsterXamarin
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<IAuthenticationService, AuthenticationService>();
            Mvx.IoCProvider.RegisterType<IMyEntityService, MyEntityService>();
            Mvx.IoCProvider.RegisterSingleton(new HttpClient());

            RegisterAppStart<LoginViewModel>();
        }
    }
}