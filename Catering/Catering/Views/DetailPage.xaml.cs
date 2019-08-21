using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Catering.Models;
using Catering.ViewModels;
using Catering.Services;
using System.ComponentModel;

namespace Catering.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        DetailViewModel _vm
        {
            get { return BindingContext as DetailViewModel; }
        }
        public DetailPage()
        {
            InitializeComponent();
        }

        void UpdateMap()
        {
            if (_vm.Entry == null)
            {
                return;
            }

            // Center the map around the log entry's location
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(_vm.Entry.Latitude,
                             _vm.Entry.Longitude),
                             Distance.FromMiles(.5)));

            // Place a pin on the map for the log entry's location
            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = _vm.Entry.Title,
                Position = new Position(_vm.Entry.Latitude, _vm.Entry.Longitude)
            });
        }
        void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(DetailViewModel.Entry))
            {
                UpdateMap();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_vm != null)
            {
                _vm.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (_vm != null)
            {
                _vm.PropertyChanged -= OnViewModelPropertyChanged;
            }
        }

        async void DeleteItem_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Usuwanie elementu", "Czy na pewno chcesz usunąć catering?", "Tak", "Nie");
            _vm.DeleteCommand.Execute(answer);

        }
    }
}