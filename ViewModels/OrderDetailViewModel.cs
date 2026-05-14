using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoFood.Models;
using EcoFood.Services;

namespace EcoFood.ViewModels;

[QueryProperty(nameof(OrderId), "orderId")]
public sealed partial class OrderDetailViewModel : ObservableObject
{
	readonly IOrdersService _ordersService;

	public OrderDetailViewModel(IOrdersService ordersService)
	{
		_ordersService = ordersService;
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(HasOrder))]
	[NotifyPropertyChangedFor(nameof(IsActive))]
	[NotifyPropertyChangedFor(nameof(StatusLabel))]
	[NotifyPropertyChangedFor(nameof(StatusColor))]
	[NotifyPropertyChangedFor(nameof(TotalFormatted))]
	[NotifyPropertyChangedFor(nameof(QuantityLabel))]
	Order? currentOrder;

	string _orderId = "";
	public string OrderId
	{
		get => _orderId;
		set
		{
			_orderId = value;
			CurrentOrder = _ordersService.Orders.FirstOrDefault(o => o.Id == value);
		}
	}

	public bool HasOrder => CurrentOrder is not null;
	public bool IsActive => CurrentOrder?.IsActive ?? false;

	public string StatusLabel => CurrentOrder?.IsActive == true ? "Ativa" : "Concluída";

	public Color StatusColor => CurrentOrder?.IsActive == true
		? Color.FromArgb("#2ECC71")
		: Color.FromArgb("#6B7280");

	public string TotalFormatted =>
		CurrentOrder is null ? "" : $"R$ {CurrentOrder.Total:F2}".Replace(".", ",");

	public string QuantityLabel =>
		CurrentOrder is null ? "" :
		CurrentOrder.Quantity == 1 ? "1 item" : $"{CurrentOrder.Quantity} itens";

	[RelayCommand]
	async Task CancelOrderAsync()
	{
		if (CurrentOrder is null) return;

		bool confirm = await Shell.Current.DisplayAlertAsync(
			"Cancelar reserva",
			$"Cancelar a reserva de \"{CurrentOrder.ProductName}\"?\n\nEssa ação não pode ser desfeita.",
			"Sim, cancelar",
			"Não");

		if (!confirm) return;

		_ordersService.Remove(_orderId);
		await Shell.Current.GoToAsync("..");
	}

	[RelayCommand]
	static Task GoBackAsync() => Shell.Current.GoToAsync("..");
}
