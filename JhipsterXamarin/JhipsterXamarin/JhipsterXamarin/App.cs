using System;
using Akavache;
using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using JhipsterXamarin.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using MvvmCross.Logging;

namespace JhipsterXamarin
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Akavache.Registrations.Start("JhipsterXamarin");
            var log = Mvx.IoCProvider.Resolve<IMvxLogProvider>().GetLogFor("JhipsterXamarin");

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Configuration.BaseUri);

            var authenticationService = new AuthenticationService(httpClient);
            var registerService = new RegisterService(httpClient, log);
            var myEntityService = new MyEntityService<MyEntityModel>(httpClient, "api/myentities");

            Mvx.IoCProvider.RegisterSingleton<IAuthenticationService>(authenticationService);
            Mvx.IoCProvider.RegisterSingleton<IRegisterService>(registerService);
            Mvx.IoCProvider.RegisterSingleton<IMyEntityService<MyEntityModel>>(myEntityService);
            Mvx.IoCProvider.RegisterSingleton<IMvxLog>(log);
            Mvx.IoCProvider.RegisterSingleton(httpClient);

            try
            {
                BlobCache.Secure.GetObject<JwtToken>("token").Subscribe(async token =>
                {
                    await authenticationService.SignIn(token);
                });
            }
            catch (Exception ex)
            {
                log.ErrorException("Failed to fetch token and auto-login.", ex);
            }

            RegisterAppStart<HomeViewModel>();
        }
    }
}