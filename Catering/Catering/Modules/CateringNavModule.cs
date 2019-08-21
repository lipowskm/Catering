using Catering.Services;
using Catering.ViewModels;
using Catering.Views;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Catering.Modules
{
    public class CateringNavModule : NinjectModule
    {
        readonly INavigation _xfNav;

        public CateringNavModule(INavigation xamarinFormsNavigation)
        {
            _xfNav = xamarinFormsNavigation;
        }
        public override void Load()
        {
            var navService = new XamarinFormsNavService();
            navService.XamarinFormsNav = _xfNav;

            navService.RegisterViewMapping(typeof(MainViewModel),
                                           typeof(MainPage));
            navService.RegisterViewMapping(typeof(DetailViewModel),
                                           typeof(DetailPage));
            navService.RegisterViewMapping(typeof(NewEntryViewModel),
                                           typeof(NewEntryPage));
            Bind<INavService>()
              .ToMethod(x => navService)
              .InSingletonScope();
        }
    }
}
