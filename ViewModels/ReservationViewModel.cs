using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoFood.Models;
using EcoFood.Services;
using EcoFood.ViewModels.Base;

namespace EcoFood.ViewModels;

/// <summary>Recapitula valores e confirma reserva usando pedidos/session simuladas.</summary>
public sealed partial class ReservationViewModel : ObservableObject
{
	readonly ReservationSession _session;
	readonly IOrdersService _ordersService;

	public ReservationViewModel(ReservationSession session, IOrdersService ordersService)
	{
		_session = session;
		_ordersService = ordersService;
	}

	public string LoadedProductId { get; private set; } = "";

	Product? Product => _session.DraftProduct;

	/// <summary>Imagem utilizada na miniatura do resumo.</summary>
	public string ThumbnailUrl => Product?.ImageUrl ?? "";

	public string SummaryTitle => Product?.Name ?? "";

	public string SummaryRestaurant => Product?.RestaurantName ?? "";

	public string SummarySubtitle =>
		Product is null
			? ""
			: $"Unidade ({ViewModelLocalization.Money(Product.DiscountedPrice)}) × Quantidade ({Quantity})";

	public string PickupLine => Product is null ? "" : Product.PickupWindow;

	public string AddressLine =>
		Product?.Restaurant is null ? "" : Product.Restaurant.Address;

	public string DistanceLine =>
		Product?.Restaurant is null ? "" : ViewModelLocalization.Km(Product.Restaurant.DistanceKm);

	public string PaymentSimpleLabel => "Pagamento na retirada 💵";

	public string QuantityLabelDisplay => $"{Quantity}x";

	public bool CanIncreaseQty =>
		Product is not null && Quantity < Math.Max(1, Product.UnitsLeft);

	public string TotalFormatted =>
		Product is null
			? ViewModelLocalization.Money(0)
			: ViewModelLocalization.Money(Product.DiscountedPrice * Quantity);

	[ObservableProperty]
	int quantity = 1;

	partial void OnQuantityChanged(int value)
	{
		_session.DraftQuantity = value;
		NotifyReservationUiChanged();
		ContinueReservationCommand.NotifyCanExecuteChanged();
	}

	public void ReloadFromSession()
	{
		if (Product is null)
		{
			LoadedProductId = "";
			if (Quantity != 1)
				Quantity = 1;
			else
				NotifyReservationUiChanged(); // garante atualização quando já estava “1”, mas estado sumiu.

			ContinueReservationCommand.NotifyCanExecuteChanged();
			return;
		}

		var maxQty = Math.Max(1, Product.UnitsLeft);

		if (!string.Equals(LoadedProductId, Product.Id, StringComparison.Ordinal))
		{
			LoadedProductId = Product.Id;
			var preferred = _session.DraftQuantity <= 0 ? 1 : _session.DraftQuantity;
			Quantity = Math.Clamp(preferred, 1, maxQty);
		}
		else
		{
			Quantity = Math.Clamp(Quantity, 1, maxQty);
		}

		NotifyReservationUiChanged();
		ContinueReservationCommand.NotifyCanExecuteChanged();
	}

	void NotifyReservationUiChanged()
	{
		OnPropertyChanged(nameof(ThumbnailUrl));
		OnPropertyChanged(nameof(SummaryTitle));
		OnPropertyChanged(nameof(SummaryRestaurant));
		OnPropertyChanged(nameof(SummarySubtitle));
		OnPropertyChanged(nameof(PickupLine));
		OnPropertyChanged(nameof(AddressLine));
		OnPropertyChanged(nameof(DistanceLine));
		OnPropertyChanged(nameof(PaymentSimpleLabel));
		OnPropertyChanged(nameof(QuantityLabelDisplay));
		OnPropertyChanged(nameof(TotalFormatted));
		OnPropertyChanged(nameof(CanIncreaseQty));
	}

	[RelayCommand]
	void IncreaseQty()
	{
		if (!CanIncreaseQty || Product is null)
			return;

		Quantity++;
	}

	[RelayCommand]
	void DecreaseQty()
	{
		if (Quantity <= 1)
			return;

		Quantity--;
	}

	bool CanConfirmReservation() => Product is not null && Product.Restaurant is not null && Quantity >= 1;

	[RelayCommand(CanExecute = nameof(CanConfirmReservation))]
	async Task ContinueReservationAsync()
	{
		if (Product is null || Product.Restaurant is null)
			return;

		await Shell.Current.GoToAsync(nameof(Views.ConfirmationPage));
	}
}
