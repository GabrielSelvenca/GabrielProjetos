#if ANDROID
using Android.Widget;
#endif

using WS_Tower.Service;
using WS_Tower.Models;

namespace WS_Tower;

public partial class LoginPage : ContentPage
{
	private int erros = 0;

	public LoginPage()
	{
		InitializeComponent();
	}

	public async void Logar(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(entrySenha.Text) || string.IsNullOrEmpty(entryUsuario.Text))
			return;

		Usuario user = new Usuario
		{
			Nome = entryUsuario.Text,
			Senha = entryUsuario.Text
		};

		var response = await ApiService<Usuario>.Post("login", user);

		if (response == null)
			Errou();
		else
		{
			Application.Current.Windows[0].Page = new HomePage(response);
		}
	}

	public void Errou()
	{
		entryUsuario.PlaceholderColor = entrySenha.PlaceholderColor = Color.FromArgb("#ff0000");
		#if ANDROID
		Toast.MakeText(Android.App.Application.Context, "Usuario/Senha incorreto.", ToastLength.Short).Show();
		#endif
	}
}