using Catering.Droid.Services;
using Catering.Services;
using Ninject.Modules;

namespace Catering.Droid.Modules
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