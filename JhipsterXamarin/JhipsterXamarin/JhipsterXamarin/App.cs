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

            var httpclient = new HttpClient(httpHandler);
            
            Akavache.Registrations.Start("JhipsterXamarin");
            var log = Mvx.IoCProvider.Resolve<IMvxLogProvider>().GetLogFor("JhipsterXamarin");

            httpclient.BaseAddress = new Uri(Configuration.BaseUri);

            var authenticationService = new AuthenticationService(httpclient);
            var registerService = new RegisterService(httpclient, log);
            var myEntityService = new MyEntityService(httpclient);
            var userEntityService = new UserEntityService(httpclient, authenticationService);

            Mvx.IoCProvider.RegisterSingleton<IAuthenticationService>(authenticationService);
            Mvx.IoCProvider.RegisterSingleton<IRegisterService>(registerService);
            Mvx.IoCProvider.RegisterSingleton<IMyEntityService>(myEntityService);
            Mvx.IoCProvider.RegisterSingleton<IUserEntityService<UserModel>>(userEntityService);
            Mvx.IoCProvider.RegisterSingleton<IMvxLog>(log);
            Mvx.IoCProvider.RegisterSingleton(httpclient);

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