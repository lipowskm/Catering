using Catering.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Catering.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPlacePage : ContentPage
    {
        SelectPlaceViewModel _vm
        {
            get { return BindingContext as SelectPlaceViewModel; }
        }
        public SelectPlacePage()
        {
            InitializeComponent();
        }
    }
}