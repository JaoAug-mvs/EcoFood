using EcoFood.Models;

namespace EcoFood.Services;

/// <summary>Fornece produtos/restaurantes e dados de usuário simulados (sem rede).</summary>
public interface IMockDataService
{
	AppUser CurrentUser { get; }
	IReadOnlyList<CategoryChip> Categories { get; }
	IReadOnlyList<Product> AllProducts { get; }
	Restaurant? FindRestaurant(string id);
	Product? FindProduct(string id);
}
