using System;
using System.Threading.Tasks;
using Catering.Models;
using Catering.Services;
using Xamarin.Forms;

namespace Catering.ViewModels
{
    public class DetailViewModel : BaseViewModel<CateringEntry>
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

        Command _deleteCommand;
        public Command DeleteCommand
        {
            get
            {
                return _deleteCommand
                    ?? (_deleteCommand = new Command<bool>(async (answer) => await ExecuteDeleteCommand(answer)));
            }
        }


        async Task ExecuteDeleteCommand(bool answer)
        {
            if (answer)
            {

                if (IsBusy)
                    return;

                IsBusy = true;
                try
                {
                    await _cateringService.RemoveEntryAsync(Entry);
                    await NavService.GoBack();
                    await NavService.ClearBackStack();
                }

                finally
                {
                    IsBusy = false;
                }
            }
        }

        public DetailViewModel(INavService navService,
                               ICateringDataService cateringService) : base(navService) 
        {
            _cateringService = cateringService;
        }

        public override async Task Init(CateringEntry cateringEntry)
        {
            Entry = cateringEntry;
        }
    }
}
