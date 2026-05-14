using EcoFood.ViewModels;

namespace EcoFood.Views;

public partial class OrderDetailPage : ContentPage
{
	public OrderDetailPage(OrderDetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
