using Xamarin.Forms;
using Catering.Views;
using Catering.ViewModels;
using Ninject.Modules;
using Ninject;
using Catering.Modules;

namespace Catering
{
    public partial class App : Application
    {
        public IKernel Kernel { get; set; }
        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();

            var mainPage = new NavigationPage(new MainPage());

            var settings = new Ninject.NinjectSettings() { LoadExtensions = false };
            // Register core services
            Kernel = new StandardKernel(
                          settings,
                          new CateringCoreModule(),
                          new CateringNavModule(mainPage.Navigation));

            // Register platform specific services
            Kernel.Load(platformModules);

            // Get the MainViewModel from the IoC
            mainPage.BindingContext = Kernel.Get<MainViewModel>();

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
