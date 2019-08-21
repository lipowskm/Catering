using Catering.ViewModels;
using System;
using System.ComponentModel; 
using System.Threading.Tasks;

namespace Catering.Services
{
    public interface INavService
    {
        bool CanGoBack { get; }
        Task GoBack();
        Task NavigateTo<TVM>()
            where TVM : BaseViewModel;
        Task NavigateTo<TVM, TParameter>(TParameter parameter)
            where TVM : BaseViewModel;
        Task RemoveLastView();
        Task ClearBackStack();
        Task NavigateToUri(Uri uri);

        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
