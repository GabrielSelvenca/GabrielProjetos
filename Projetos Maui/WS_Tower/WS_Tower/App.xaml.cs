namespace WS_Tower
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Application.Current.Windows[0].Page = new SplashScreen();
        }
    }
}