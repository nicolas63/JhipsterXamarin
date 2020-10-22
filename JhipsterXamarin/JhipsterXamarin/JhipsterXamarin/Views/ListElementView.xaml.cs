using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JhipsterXamarin.ViewModels;

namespace JhipsterXamarin.Views
{
    public partial class ListElementView : MvxContentPage<ListViewModel>
    {
        public ListElementView()
        {
            InitializeComponent();
        }
    }
}