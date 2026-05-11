using System.Collections.ObjectModel;
using EcoFood.Models;

namespace EcoFood.Services;

/// <seealso cref="IOrdersService"/>
public sealed class OrdersService : IOrdersService
{
	public OrdersService()
	{
		Orders = BuildSeedOrders();
	}

	public ObservableCollection<Order> Orders { get; }

	void IOrdersService.Add(Order order)
	{
		// Novos ficam sempre no topo (lista vertical).
		if (Orders.Count == 0)
			Orders.Add(order);
		else
			Orders.Insert(0, order);
	}

	static ObservableCollection<Order> BuildSeedOrders()
	{
		var now = DateTime.UtcNow;
		return
		[
			new Order
			{
				Id = "ord_301",
				ProductName = "Combo Vegano",
				RestaurantName = "Green Bowl",
				StatusLabel = "Confirmado",
				PickupWindow = "Retirada hoje • 17:00 - 19:00",
				ProductImageUrl = "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=400&q=80",
				OrderedAtUtc = now.AddHours(-1),
				Total = 22.95m,
				IsActive = true
			},
			new Order
			{
				Id = "ord_210",
				ProductName = "Caixa Surprise Doces",
				RestaurantName = "Panari Bakery",
				StatusLabel = "Retirado",
				PickupWindow = "Retirado em 09/mai • 21:05",
				ProductImageUrl = "https://images.unsplash.com/photo-1578985545062-c699dcb9d068?w=400&q=80",
				OrderedAtUtc = now.AddDays(-1),
				Total = 24.99m,
				IsActive = false
			},
			new Order
			{
				Id = "ord_099",
				ProductName = "Combo Sushi (12 peças)",
				RestaurantName = "Sushi No Ar",
				StatusLabel = "Retirado",
				PickupWindow = "Retirado em 07/mai • 19:52",
				ProductImageUrl = "https://images.unsplash.com/photo-1579584425555-c3ce17fd4351?w=400&q=80",
				OrderedAtUtc = now.AddDays(-3),
				Total = 55.74m,
				IsActive = false
			}
		];
	}
}
