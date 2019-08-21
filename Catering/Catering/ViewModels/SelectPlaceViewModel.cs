using Catering.Models;
using Catering.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Catering.ViewModels
{
    public class SelectPlaceViewModel : BaseViewModel<CateringEntry>
    { 
        readonly ICateringDataService _cateringService;
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

        public SelectPlaceViewModel(INavService navService,
                                    ICateringDataService cateringService) : base(navService)
        {
            _cateringService = cateringService;
        }

        public override async Task Init(CateringEntry cateringEntry)
        {
            Entry = cateringEntry;
        }

        Command _saveCommand;
        public Command SaveCommand
        {
            get
            {
                return _saveCommand
                    ?? (_saveCommand = new Command(async () => await ExecuteSaveCommand()));
            }
        }

        // ...

        async Task ExecuteSaveCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await _cateringService.AddEntryAsync(Entry);
                await NavService.NavigateTo<MainViewModel>();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
