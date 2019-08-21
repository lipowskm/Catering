using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Catering.Models;
using Catering.ViewModels;
using Catering.Services;
using Android.Support.V4.App;
using Android;

namespace Catering.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MainViewModel _vm
        {
            get { return BindingContext as MainViewModel; }
        }
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Initialize MainViewModel
            if (_vm != null)
                await _vm.Init();
        }

        void Caterings_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var catering = (CateringEntry)e.Item;
            _vm.ViewCommand.Execute(catering);

            // Clear selection
            caterings.SelectedItem = null;
        }
    }
}
