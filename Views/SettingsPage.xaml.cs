namespace EcoFood.Views;

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
