using EcoFood.ViewModels;

namespace EcoFood.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is HomeViewModel vm)
			vm.ReloadImpactFromUser();
	}
}
