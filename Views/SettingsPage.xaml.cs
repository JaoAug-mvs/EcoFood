namespace EcoFood.Views;

/// <summary>Stub amigável até que persistência verdadeira seja solicitada pela equipe de produtos.</summary>
public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

	async void OnCloseClicked(object? sender, EventArgs args)
	{
		await Shell.Current.GoToAsync("..");
	}
}
