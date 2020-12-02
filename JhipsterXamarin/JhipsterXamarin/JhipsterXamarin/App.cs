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

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true;

            var httpClient = new HttpClient(httpHandler);
            httpClient.BaseAddress = new Uri(Configuration.BaseUri);

            var authenticationService = new AuthenticationService(httpClient, log);
            var registerService = new RegisterService(httpClient, log);
            var myEntityService = new AbstractEntityService<AbstractEntityModel>(httpClient, "api/myentities");

            Mvx.IoCProvider.RegisterSingleton<IAuthenticationService>(authenticationService);
            Mvx.IoCProvider.RegisterSingleton<IRegisterService>(registerService);
            Mvx.IoCProvider.RegisterSingleton<IAbstractEntityService<AbstractEntityModel>>(myEntityService);
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