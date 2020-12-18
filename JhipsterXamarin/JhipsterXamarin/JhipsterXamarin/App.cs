using System;
using Akavache;
using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using JhipsterXamarin.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;
using System.Net.Http;
using System.Reactive.Linq;
using MvvmCross.Logging;
using System.Threading.Tasks;

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
            var userEntityService = new UserEntityService(httpClient, authenticationService);

            Mvx.IoCProvider.RegisterSingleton<IAuthenticationService>(authenticationService);
            Mvx.IoCProvider.RegisterSingleton<IRegisterService>(registerService);
            Mvx.IoCProvider.RegisterSingleton<IUserEntityService<UserModel>>(userEntityService);
            Mvx.IoCProvider.RegisterSingleton<IAbstractEntityService<AbstractEntityModel>>(myEntityService);
            Mvx.IoCProvider.RegisterSingleton<IMvxLog>(log);
            Mvx.IoCProvider.RegisterSingleton(httpClient);

            try
            {
                // sync trying to connect before loading home view
                var token = Task.Run(async () => await BlobCache.Secure.GetObject<JwtToken>("token")).Result;
                Task.Run(async () => await authenticationService.SignIn(token));
            }
            catch (Exception ex)
            {
                log.ErrorException("Failed to fetch token and auto-login.", ex);
            }

            RegisterAppStart<HomeViewModel>();

        }
    }
}
