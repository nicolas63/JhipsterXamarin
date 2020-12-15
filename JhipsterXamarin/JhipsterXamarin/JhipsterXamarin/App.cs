using System;
using Akavache;
using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using JhipsterXamarin.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;
using System.Net.Http;
using MvvmCross.Logging;

namespace JhipsterXamarin
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {

            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
            };

            var httpClient = new HttpClient(httpHandler);
            
            Akavache.Registrations.Start("JhipsterXamarin");
            var log = Mvx.IoCProvider.Resolve<IMvxLogProvider>().GetLogFor("JhipsterXamarin");

            httpClient.BaseAddress = new Uri(Configuration.BaseUri);

            var authenticationService = new AuthenticationService(httpClient);
            var registerService = new RegisterService(httpClient, log);
            var myEntityService = new MyEntityService(httpClient);
            var userEntityService = new UserEntityService(httpClient, authenticationService);

            Mvx.IoCProvider.RegisterSingleton<IAuthenticationService>(authenticationService);
            Mvx.IoCProvider.RegisterSingleton<IRegisterService>(registerService);
            Mvx.IoCProvider.RegisterSingleton<IMyEntityService>(myEntityService);
            Mvx.IoCProvider.RegisterSingleton<IUserEntityService<UserModel>>(userEntityService);
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