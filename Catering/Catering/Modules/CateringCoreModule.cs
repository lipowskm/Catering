using Catering.Services;
using Catering.ViewModels;
using Ninject.Modules;
using System;

namespace Catering.Modules
{
    public class CateringCoreModule : NinjectModule
    {
        public override void Load()
        {
            // ViewModels
            Bind<MainViewModel>().ToSelf();
            Bind<DetailViewModel>().ToSelf();
            Bind<NewEntryViewModel>().ToSelf();
            Bind<SelectPlaceViewModel>().ToSelf();

            // Core Services
            var cateringService = new CateringApiDataService(new
                Uri("https://5d66c6f06847d40014f65b97.mockapi.io"));

            Bind<ICateringDataService>()
                .ToMethod(x => cateringService)
                .InSingletonScope();

            Bind<Akavache.IBlobCache>().ToConstant(Akavache.BlobCache.LocalMachine);
        }
    }
}