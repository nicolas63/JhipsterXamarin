using MvvmCross.ViewModels;
using JhipsterXamarin.Core.Resources;

namespace JhipsterXamarin.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        /// <summary>
        /// Gets the internationalized string at the given <paramref name="index"/>, which is the key of the resource.
        /// </summary>
        /// <param name="index">Index key of the string from the resources of internationalized strings.</param>
        public string this[string index] => Strings.ResourceManager.GetString(index);
    }
}
