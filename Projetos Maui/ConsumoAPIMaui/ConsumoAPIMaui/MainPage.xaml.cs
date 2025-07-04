using ConsumoAPIMaui.Models;
using ConsumoAPIMaui.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsumoAPIMaui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void CarregarLista()
        {
            var response = await ApiService<Cliente>.GetList("clientes");

            collection.ItemsSource = response;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CarregarLista();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            CarregarLista();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (nameInput == null || emailInput == null)
            {
                await DisplayAlert("Erro", "Os campos não estão ligados corretamente no XAML.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(emailInput?.Text) || string.IsNullOrWhiteSpace(nameInput?.Text))
            {
                await DisplayAlert("Campos inválidos", "Preencha os campos corretamente.", "OK");
                return;
            }

            var novoCliente = new Cliente
            {
                Email = emailInput.Text,
                Nome = nameInput.Text
            };

            var sucesso = await ApiService<Cliente>.Post("clientes", novoCliente);

            if (sucesso != null)
                collection.ItemsSource = await ApiService<Cliente>.GetList("clientes");
            else
                await DisplayAlert("Ops!", "Erro ao criar usuário!", "Ok");
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cliente = (Cliente)button.BindingContext;

            if (cliente == null)
            {
                await DisplayAlert("Dados não encontrados.", "Os dados do usuário que você tentou editar não foram encontrados no contexto atual.", "Ok");
                return;
            }

            await Navigation.PushAsync(new ClienteEditPage(cliente));
        }
    }
}