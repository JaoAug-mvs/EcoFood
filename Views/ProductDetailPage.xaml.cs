using EcoFood.ViewModels;

namespace EcoFood.Views;

/// <summary>Detalhe do produto; carrega modelo via argumento Shell <c>ProductId</c>.</summary>
[QueryProperty(nameof(ProductRouteId), "ProductId")]
public partial class ProductDetailPage : ContentPage
{
	readonly ProductDetailViewModel _vm;

	public ProductDetailPage(ProductDetailViewModel viewModel)
	{
		BindingContext = _vm = viewModel;
		InitializeComponent();
	}

	/// <summary>Setter gerado pela navegação relativa <c>?ProductId=...</c>.</summary>
	public string ProductRouteId
	{
		set
		{
			var raw = string.IsNullOrWhiteSpace(value) ? string.Empty : Uri.UnescapeDataString(value);
			_vm.Load(raw);
		}
	}
}
