using Catering.Models;
using Catering.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Catering.ViewModels
{
    public class SelectPlaceViewModel : BaseViewModel<CateringEntry>
    { 
        readonly ICateringDataService _cateringService;
        IGoogleMapsApiService googleMapsApi = new GoogleMapsApiService();
        CateringEntry _entry;
        public CateringEntry Entry
        {
            get { return _entry; }
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }

        GooglePlaceAutoCompletePrediction _placeSelected;
        public GooglePlaceAutoCompletePrediction PlaceSelected
        {
            get
            {
                return _placeSelected;
            }
            set
            {
                _placeSelected = value;
                OnPropertyChanged();
                if (_placeSelected != null)
                    GetPlaceDetailCommand.Execute(_placeSelected);
            }
        }

        public ICommand GetPlacesCommand { get; set; }
        public ICommand GetPlaceDetailCommand { get; set; }

        ObservableCollection<GooglePlaceAutoCompletePrediction> _places;
        public ObservableCollection<GooglePlaceAutoCompletePrediction> Places
        {
            get
            {
                return _places;
            }
            set
            {
                _places = value;
                OnPropertyChanged();
            }
        }
        //probably will be removed
        ObservableCollection<GooglePlaceAutoCompletePrediction> _recentPlaces;
        public ObservableCollection<GooglePlaceAutoCompletePrediction> RecentPlaces
        {
            get
            {
                return _recentPlaces;
            }
            set
            {
                _recentPlaces = value;
                OnPropertyChanged();
            }
        }

        bool _showRecentPlaces { get; set; }
        public bool ShowRecentPlaces
        {
            get
            {
                return _showRecentPlaces;
            }
            set
            {
                _showRecentPlaces = value;
                OnPropertyChanged();
            }
        }

        string _pickupText;
        public string PickupText
        {
            get
            {
                return _pickupText;
            }
            set
            {
                _pickupText = value;
                OnPropertyChanged();
                if (!string.IsNullOrEmpty(_pickupText))
                {
                    GetPlacesCommand.Execute(_pickupText);
                }
            }
        }

        public SelectPlaceViewModel(INavService navService,
                                    ICateringDataService cateringService) : base(navService)
        {
            _cateringService = cateringService;
            GetPlacesCommand = new Command<string>(async (param) => await GetPlacesByName(param));
            GetPlaceDetailCommand = new Command<GooglePlaceAutoCompletePrediction>(async (param) => await GetPlacesDetail(param));
        }

        public override async Task Init(CateringEntry cateringEntry)
        {
            ShowRecentPlaces = true;
            Entry = cateringEntry;
        }


        public async Task GetPlacesByName(string placeText)
        {
            var places = await googleMapsApi.GetPlaces(placeText);
            var placeResult = places.AutoCompletePlaces;
            if (placeResult != null && placeResult.Count > 0)
            {
                Places = new ObservableCollection<GooglePlaceAutoCompletePrediction>(placeResult);
            }

            ShowRecentPlaces = (placeResult == null || placeResult.Count == 0);
        }

        public async Task GetPlacesDetail(GooglePlaceAutoCompletePrediction placeA)
        {
            var place = await googleMapsApi.GetPlaceDetails(placeA.PlaceId);
            if (place != null)
            {
                    PickupText = place.Name;
                    Entry.Latitude = place.Latitude;
                    Entry.Longitude = place.Longitude;

                    if (IsBusy)
                        return;

                    IsBusy = true;

                    try
                    {
                        await _cateringService.AddEntryAsync(Entry);
                        await NavService.NavigateTo<MainViewModel>();
                        await NavService.ClearBackStack();
                        await NavService.ClearBackStack();
                    }
                    finally
                    {
                        IsBusy = false;
                    }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
