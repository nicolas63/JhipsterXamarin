using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross;
using MvvmCross.Logging;

namespace JhipsterXamarin
{
    public static class Logs
    {
        public static IMvxLog Instance { get; } = Mvx.IoCProvider.Resolve<IMvxLogProvider>().GetLogFor("JhipsterXamarin");
    }
}
