using EcoFood.ViewModels;

namespace EcoFood.Views;

public partial class ReservationPage : ContentPage
{
	readonly ReservationViewModel _vm;

	public ReservationPage(ReservationViewModel viewModel)
	{
		BindingContext = _vm = viewModel;
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		_vm.ReloadFromSession();
		if (string.IsNullOrWhiteSpace(_vm.ThumbnailUrl))
			await Shell.Current.GoToAsync("..");
	}
}
