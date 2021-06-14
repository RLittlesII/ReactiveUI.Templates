using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Template
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            var _ = new AppBootstrapper(Locator.CurrentMutable);

            MainPage = AppBootstrapper.CreateMainPage();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
