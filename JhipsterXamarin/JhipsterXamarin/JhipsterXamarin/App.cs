using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using JhipsterXamarin.Services;
using JhipsterXamarin.ViewModels;
using JhipsterXamarin.Views;
using MvvmCross.Navigation;
using System.Net.Http;

namespace JhipsterXamarin
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<IListService, ListService>();
            Mvx.IoCProvider.RegisterType<IAuthenticationService, AuthenticationService>();
            Mvx.IoCProvider.RegisterType<IMyEntityService, MyEntityService>();


            RegisterAppStart<ListViewModel>();

        }
    }
}
