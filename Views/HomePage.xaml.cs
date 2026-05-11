using EcoFood.ViewModels;

namespace EcoFood.Views;

/// <summary>Tela inicial com métricas, busca, categorias e cards de produtos próximos.</summary>
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
