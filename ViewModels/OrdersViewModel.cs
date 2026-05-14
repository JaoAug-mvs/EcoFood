using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoFood.Models;
using EcoFood.Services;

namespace EcoFood.ViewModels;

/// <summary>Histórico/ativos a partir dos pedidos fictícios de <see cref="IOrdersService"/>.</summary>
public sealed partial class OrdersViewModel : ObservableObject
{
	readonly IOrdersService _ordersService;

	public OrdersViewModel(IOrdersService ordersService)
	{
		_ordersService = ordersService;
		_ordersService.Orders.CollectionChanged += OrdersOnCollectionChanged;
		SyncBuckets();
	}

	public ObservableCollection<Order> ActiveOrders { get; } = new();

	public ObservableCollection<Order> HistoryOrders { get; } = new();

	public bool HasActive => ActiveOrders.Count > 0;

	public bool HasHistory => HistoryOrders.Count > 0;

	/// <summary>Controla segmentação “ativos vs histórico” na UI.</summary>
	[ObservableProperty]
	bool showingActiveOrders = true;

	partial void OnShowingActiveOrdersChanged(bool value)
	{
		OnPropertyChanged(nameof(IsActiveOrdersVisible));
		OnPropertyChanged(nameof(IsHistoryOrdersVisible));
		OnPropertyChanged(nameof(ActiveChipBackground));
		OnPropertyChanged(nameof(HistoryChipBackground));
	}

	public bool IsActiveOrdersVisible => ShowingActiveOrders;

	public bool IsHistoryOrdersVisible => !ShowingActiveOrders;

	public Color ActiveChipBackground => ShowingActiveOrders ? Color.FromArgb("#2ECC71") : Colors.Transparent;

	public Color ActiveChipForeground => ShowingActiveOrders ? Colors.White : Color.FromArgb("#6B7280");

	public Color HistoryChipBackground => !ShowingActiveOrders ? Color.FromArgb("#2ECC71") : Colors.Transparent;

	public Color HistoryChipForeground => !ShowingActiveOrders ? Colors.White : Color.FromArgb("#6B7280");

	[RelayCommand]
	void SwitchToActiveOrders() => ShowingActiveOrders = true;

	[RelayCommand]
	void SwitchToHistoryOrders() => ShowingActiveOrders = false;

	[RelayCommand]
	static Task ViewOrderDetailAsync(Order order)
		=> Shell.Current.GoToAsync($"{nameof(Views.OrderDetailPage)}?orderId={order.Id}");

	void OrdersOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
		=> SyncBuckets();

	void SyncBuckets()
	{
		ActiveOrders.Clear();
		HistoryOrders.Clear();

		foreach (var o in _ordersService.Orders.OrderByDescending(o => o.OrderedAtUtc))
			(o.IsActive ? ActiveOrders : HistoryOrders).Add(o);

		OnPropertyChanged(nameof(HasActive));
		OnPropertyChanged(nameof(HasHistory));

		OnPropertyChanged(nameof(ActiveSubtitle));
		OnPropertyChanged(nameof(HistorySubtitle));
	}

	public string ActiveSubtitle =>
		ActiveOrders.Count == 0
			? "Nenhum pedido confirmado esperando retirada."
			: "Retire dentro da janela indicada pelo restaurante.";

	public string HistorySubtitle =>
		HistoryOrders.Count == 0
			? "Seu histórico aparecerá aqui."
			: "Obrigado por reduzir desperdício 💚.";
}
