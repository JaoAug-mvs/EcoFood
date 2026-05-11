using EcoFood.ViewModels;

namespace EcoFood.Views;

public partial class SuccessPage : ContentPage
{
	public SuccessPage(SuccessViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is SuccessViewModel vm)
			vm.Reload();
	}
}
