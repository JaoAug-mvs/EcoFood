using EcoFood.ViewModels;

namespace EcoFood.Views;

public partial class OrdersPage : ContentPage
{
	public OrdersPage(OrdersViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}
