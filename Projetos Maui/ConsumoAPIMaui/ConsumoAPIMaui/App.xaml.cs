namespace ConsumoAPIMaui
{
    public partial class App : Application
    {
        public App() => InitializeComponent();

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var rootPage = new NavigationPage(new MainPage());

            return new Window(rootPage);
        }
    }
}