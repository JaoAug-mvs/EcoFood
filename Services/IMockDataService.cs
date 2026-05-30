using EcoFood.Models;

namespace EcoFood.Services;

public interface IMockDataService
{
	AppUser CurrentUser { get; }
	IReadOnlyList<CategoryChip> Categories { get; }
	IReadOnlyList<Product> AllProducts { get; }
	Product? FindProduct(string id);
}
