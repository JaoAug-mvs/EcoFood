using EcoFood.ViewModels;

namespace EcoFood.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfileViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is ProfileViewModel vm)
		{
			vm.RefreshFavoriteTotals();
			vm.ReloadImpact();
		}
	}
}
