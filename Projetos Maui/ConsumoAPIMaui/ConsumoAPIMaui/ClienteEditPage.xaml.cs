using ConsumoAPIMaui.Models;
using ConsumoAPIMaui.Services;
using System.Diagnostics;

namespace ConsumoAPIMaui;

public partial class ClienteEditPage : ContentPage
{
	private readonly Cliente? cliente;

	public ClienteEditPage(Cliente c)
	{
		if (c == null)
		{
			DisplayAlert("Not Found", "Usuário não encontrado, verifique os dados.", "Ok");
			return;
		}

		this.cliente = c;

		InitializeComponent();

		nomeInput.Text = cliente.Nome;
		emailInput.Text = cliente.Email;
	}

	public async void Salvar(Object sender, EventArgs e)
	{
		Cliente novosDados = new Cliente();

		if (cliente == null)
		{
			await DisplayAlert("Ops!", "Usuário inexistente.", "Ok");
			return;
		}

		if (nomeInput.Text != cliente.Nome)
			novosDados.Nome = nomeInput.Text;
        else
            novosDados.Nome = cliente.Nome;
        if (emailInput.Text != cliente.Email)
			novosDados.Email = emailInput.Text;
		else
			novosDados.Email = cliente.Email;

		if (string.IsNullOrEmpty(novosDados.Nome) && string.IsNullOrEmpty(novosDados.Email))
		{
			await DisplayAlert("Ops!", "Nenhum dos dados foram alterados.", "Ok.");
			return;
		}

		var sucess = await ApiService<Cliente>.Patch("clientes", novosDados, cliente.Id);

		await Navigation.PopAsync();
	}
}