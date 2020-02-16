using Prism;
using Prism.Ioc;
using SASMobileApp1.Interfaces;
using SASMobileApp1.Services;
using SASMobileApp1.ViewModels;
using SASMobileApp1.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SASMobileApp1
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterSingleton<AppDataService>();
            containerRegistry.RegisterSingleton<PatientService>();
            containerRegistry.RegisterSingleton<ObsService>();
            containerRegistry.RegisterSingleton<LeaveService>();

            containerRegistry.RegisterForNavigation<HomeMDPage, HomeMDPageViewModel>();
            containerRegistry.RegisterForNavigation<LogoutPage, LogoutPageViewModel>();
            containerRegistry.RegisterForNavigation<AdminPage, AdminPageViewModel>();
            containerRegistry.RegisterForNavigation<PatientListPage, PatientListPageViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutPageViewModel>();
            containerRegistry.RegisterForNavigation<EnviromentalPage, EnviromentalPageViewModel>();
            containerRegistry.RegisterForNavigation<HomeTabPage, HomeTabPageViewModel>();
            containerRegistry.RegisterForNavigation<ObsPage, ObsPageViewModel>();
            containerRegistry.RegisterForNavigation<LeavePage, LeavePageViewModel>();
            containerRegistry.RegisterForNavigation<LeaveEditPage, LeaveEditPageViewModel>();
            containerRegistry.RegisterForNavigation<PropertyPage, PropertyPageViewModel>();
            containerRegistry.RegisterForNavigation<PatientDetailsPage, PatientDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<WardChanger, WardChangerViewModel>();
        }
    }
}
