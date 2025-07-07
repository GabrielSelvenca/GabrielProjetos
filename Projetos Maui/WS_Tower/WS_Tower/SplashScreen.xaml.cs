namespace WS_Tower;

public partial class SplashScreen : ContentPage
{
	public SplashScreen()
	{
		InitializeComponent();
        StartSplash();
    }

	private async void StartSplash()
	{
        await Task.Delay(3000);
        Application.Current.Windows[0].Page = new NavigationPage(new LoginPage());
    }
}