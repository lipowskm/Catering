using Catering.iOS.Services;
using Catering.Services;
using Ninject.Modules;

namespace Catering.iOS.Modules
{
    public class CateringPlatformModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILocationService>()
                              .To<LocationService>()
                              .InSingletonScope();
        }
    }
}