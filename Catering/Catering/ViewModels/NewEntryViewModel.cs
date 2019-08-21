using Catering.Models;
using Catering.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Catering.ViewModels
{
    public class NewEntryViewModel : BaseViewModel
    {
        readonly ILocationService _locService;
        readonly ICateringDataService _cateringService;

        string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
                SelectPlaceCommand.ChangeCanExecute();
            }
        }

        double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }

        double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }

        DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        int _rating;
        public int Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                OnPropertyChanged();
            }
        }

        string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        public NewEntryViewModel(INavService navService,
                                 ILocationService locService,
                                 ICateringDataService cateringService) : base(navService)
        {
            _locService = locService;
            _cateringService = cateringService;
            Date = DateTime.Today;
            Rating = 1;
        }

        Command _selectPlaceCommand;
        public Command SelectPlaceCommand
        {
            get
            {
                return _selectPlaceCommand
                    ?? (_selectPlaceCommand = new Command(async () => await ExecuteSelectPlaceCommand(), CanSave));
            }
        }

        // ...

        async Task ExecuteSelectPlaceCommand()
        {
            var newItem = new CateringEntry
            {
                Title = Title,
                Latitude = Latitude,
                Longitude = Longitude,
                Date = Date,
                Rating = Rating,
                Notes = Notes
            };
            await NavService.NavigateTo<SelectPlaceViewModel, CateringEntry>(newItem);
        }

        bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }

        public override async Task Init()
        {
            var coords = await _locService.GetGeoCoordinatesAsync();
            Latitude = coords.Latitude;
            Longitude = coords.Longitude;
        }
    }
}
