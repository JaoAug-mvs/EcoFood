using System.Collections.ObjectModel;
using EcoFood.Models;

namespace EcoFood.Services;

public interface IOrdersService
{
	ObservableCollection<Order> Orders { get; }
	void Add(Order order);
	void Remove(string orderId);
}
