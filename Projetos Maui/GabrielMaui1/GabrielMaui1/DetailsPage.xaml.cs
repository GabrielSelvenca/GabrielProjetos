namespace GabrielMaui1;

[QueryProperty(nameof(Str), "str")]
public partial class DetailsPage : ContentPage
{
    public string Str { get; set; }
    public DetailsPage()
	{
		InitializeComponent();
		BindingContext = this;
		Titulo.Text = Str;
    }
}