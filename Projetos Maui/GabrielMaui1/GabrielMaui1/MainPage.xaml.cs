using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GabrielMaui1
{
    public partial class MainPage : Shell
    {
        IConnectivity connectivity;
        public ObservableCollection<string>  Lista { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<int>  ListaNumber { get; set; } = new ObservableCollection<int>();
        public MainPage(IConnectivity connectivity)
        {
            InitializeComponent();
            this.connectivity = connectivity;
        }

        async void Add(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(entrytask.Text))
                return;

            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Oh não!", "Sem conexão com a internet", "Ok");
                return;
            }

            Lista.Add(entrytask.Text);
            entrytask.Text = string.Empty;
        }

        async void AddNumber(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(entrytask_number.Text))
                return;

            if (int.TryParse(entrytask_number.Text, out int number))
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Oh não!", "Sem conexão com a internet", "Ok");
                    return;
                }

                ListaNumber.Add(number);
                entrytask_number.Text = string.Empty;
            }
        }

        public void Remove(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var text = item?.BindingContext as string;
            if (text != null)
                Lista.Remove(text);
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (sender is Frame frame && frame.Content is Label label)
            {
                await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?str={label.Text}");
            }
        }
    }
}
