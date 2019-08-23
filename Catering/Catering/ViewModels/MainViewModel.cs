using Akavache;
using Catering.Models;
using Catering.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Catering.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly ICateringDataService _cateringService;
        readonly IBlobCache _cache;

        Command<CateringEntry> _viewCommand;
        public Command<CateringEntry> ViewCommand
        {
            get
            {
                return _viewCommand
                    ?? (_viewCommand = new Command<CateringEntry>(async (entry) => await ExecuteViewCommand(entry)));
            }
        }

        Command _refreshCommand;
        public Command RefreshCommand
        {
            get
            {
                return _refreshCommand
                    ?? (_refreshCommand = new Command(LoadEntries));
            }
        }

        Command _newCommand;
        public Command NewCommand
        {
            get
            {
                return _newCommand
                    ?? (_newCommand = new Command(async () => await ExecuteNewCommand()));
            }
        }

        async Task ExecuteViewCommand(CateringEntry entry)
        {
            await NavService.NavigateTo<DetailViewModel, CateringEntry>(entry);
        }

        async Task ExecuteNewCommand()
        {
            await NavService.NavigateTo<NewEntryViewModel>();
        }

        ObservableCollection<CateringEntry> _cateringEntries;
        public ObservableCollection<CateringEntry> CateringEntries
        {
            get { return _cateringEntries; }
            set
            {
                _cateringEntries = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel(INavService navService, 
                             ICateringDataService cateringService,
                             IBlobCache cache) : base(navService)
        {
            _cache = cache;
            _cateringService = cateringService;
            CateringEntries = new ObservableCollection<CateringEntry>();
        }

        public override async Task Init()
        {
            LoadEntries();
        }

        void LoadEntries()
        {
            if (IsBusy)
            {

                return;
            }

            IsBusy = true;

            CateringEntries.Clear();

            try
            {
                // Load from local cache and then immediately load from API
                _cache.GetAndFetchLatest("entries", async ()
                           => await _cateringService.GetEntriesAsync())
                          .Subscribe(entries
                           => CateringEntries = new ObservableCollection<CateringEntry>(entries));

                //delete before release
                CateringEntries.Add(new CateringEntry
                {
                    Title = "Test",
                    Latitude = 25,
                    Longitude = 25,
                    Date = DateTime.Now
                });
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

}
