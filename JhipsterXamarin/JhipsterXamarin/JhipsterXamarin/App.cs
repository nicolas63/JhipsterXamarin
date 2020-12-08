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

            var client = new HttpClient(httpHandler);
            
            Akavache.Registrations.Start("JhipsterXamarin");
            var log = Mvx.IoCProvider.Resolve<IMvxLogProvider>().GetLogFor("JhipsterXamarin");

            client.BaseAddress = new Uri(Configuration.BaseUri);

            var authenticationService = new AuthenticationService(client);
            var registerService = new RegisterService(client, log);
            var myEntityService = new MyEntityService(client);
            var userEntityService = new UserEntityService(client,authenticationService);

            Mvx.IoCProvider.RegisterSingleton<IAuthenticationService>(authenticationService);
            Mvx.IoCProvider.RegisterSingleton<IRegisterService>(registerService);
            Mvx.IoCProvider.RegisterSingleton<IMyEntityService>(myEntityService);
            Mvx.IoCProvider.RegisterSingleton<IUserEntityService<UserModel>>(userEntityService);
            Mvx.IoCProvider.RegisterSingleton<IMvxLog>(log);
            Mvx.IoCProvider.RegisterSingleton(client);

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