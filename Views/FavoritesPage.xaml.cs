using EcoFood.ViewModels;

namespace EcoFood.Views;

public partial class FavoritesPage : ContentPage
{
	public FavoritesPage(FavoritesViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is FavoritesViewModel vm)
			vm.Reload();
	}
}
