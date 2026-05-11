using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoFood.Services;
using EcoFood.ViewModels.Base;

namespace EcoFood.ViewModels;

public sealed partial class SuccessViewModel : ObservableObject
{
	readonly ReservationSession _session;

	public SuccessViewModel(ReservationSession session)
	{
		_session = session;
	}

	public void Reload()
	{
		OnPropertyChanged(nameof(ProductNameDisplay));
		OnPropertyChanged(nameof(PickupLineDisplay));
		OnPropertyChanged(nameof(TotalFormatted));
		OnPropertyChanged(nameof(ThumbUrl));
		OnPropertyChanged(nameof(Subtitle));
	}

	public string ProductNameDisplay => _session.LastConfirmed?.ProductName ?? "";

	public string PickupLineDisplay =>
		_session.LastConfirmed is null ? "" : $"Retirada • {_session.LastConfirmed.PickupWindow}";

	public string ThumbUrl => _session.LastConfirmed?.ProductImageUrl ?? "";

	public string Subtitle =>
		"Seu pedido foi reservado com sucesso e já aparece na aba Pedidos. Nos vemos na retirada — obrigado por combater desperdício! 💚";

	public string TotalFormatted =>
		_session.LastConfirmed is null
			? ViewModelLocalization.Money(0)
			: ViewModelLocalization.Money(_session.LastConfirmed.Total);

	public string BookingCodeLabel =>
		_session.LastConfirmed is null ? string.Empty : $"Código: {_session.LastConfirmed.BookingCode}";

	[RelayCommand]
	static Task OpenHomeAsync()
		=> Shell.Current.GoToAsync("//HomeTab/HomeRoot");

	[RelayCommand]
	static Task OpenOrdersAsync()
		=> Shell.Current.GoToAsync("//OrdersTab/OrdersRoot");

}
