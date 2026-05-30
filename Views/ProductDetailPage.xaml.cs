using EcoFood.ViewModels;

namespace EcoFood.Views;

[QueryProperty(nameof(ProductRouteId), "ProductId")]
public partial class ProductDetailPage : ContentPage
{
	readonly ProductDetailViewModel _vm;

	public ProductDetailPage(ProductDetailViewModel viewModel)
	{
		BindingContext = _vm = viewModel;
		InitializeComponent();
	}

	public string ProductRouteId
	{
		set
		{
			var raw = string.IsNullOrWhiteSpace(value) ? string.Empty : Uri.UnescapeDataString(value);
			_vm.Load(raw);
		}
	}
}
