using EcoFood.ViewModels;

namespace EcoFood.Views;

public partial class ExplorePage : ContentPage
{
	public ExplorePage(ExploreViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}
