using Akavache;
using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using JhipsterXamarin.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace JhipsterXamarin
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Akavache.Registrations.Start("JhipsterXamarin");

            var httpClient = new HttpClient();
            var authenticationService = new AuthenticationService(httpClient);
            Mvx.IoCProvider.RegisterSingleton(httpClient);
            Mvx.IoCProvider.RegisterSingleton<IAuthenticationService>(authenticationService);
            Mvx.IoCProvider.RegisterType<IMyEntityService, MyEntityService>();

            bool success = false;

            try
            {
                var token = Task.Run(async () => await BlobCache.Secure.GetObject<JwtToken>("token")).Result;
                success = Task.Run(async () => await authenticationService.SignIn(token)).Result;
            }
            catch { }

            if (success) RegisterAppStart<HomeViewModel>();
            else RegisterAppStart<LoginViewModel>();

        }
    }
}