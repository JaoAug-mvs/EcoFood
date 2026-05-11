using System.Collections.ObjectModel;
using EcoFood.Models;

namespace EcoFood.Services;

/// <summary>Lista mutável observável para pedidos (mock), usada pela tela Pedidos.</summary>
public interface IOrdersService
{
	ObservableCollection<Order> Orders { get; }
	void Add(Order order);
}
