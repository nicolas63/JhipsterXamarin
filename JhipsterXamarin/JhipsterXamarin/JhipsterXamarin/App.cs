using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using JhipsterXamarin.Services;
using JhipsterXamarin.ViewModels;
using JhipsterXamarin.Views;

namespace JhipsterXamarin
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<IListService, ListService>();

            RegisterAppStart<ListViewModel>();
        }
    }
}
