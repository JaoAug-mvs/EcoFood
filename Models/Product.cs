namespace EcoFood.Models;

/// <summary>
/// Representa uma oferta de refeição com desconto, pronta para retirada no restaurante parceiro.
/// </summary>
public sealed class Product
{
	public required string Id { get; init; }
	public required string Name { get; init; }
	public required string RestaurantName { get; init; }
	public required string RestaurantId { get; init; }
	public required string Category { get; init; }

	/// <summary>URL fictícia (Unsplash/Picsum); funciona bem para demonstrações sem backend.</summary>
	public required string ImageUrl { get; init; }

	public double Rating { get; init; }
	public double DistanceKm { get; init; }

	public decimal OriginalPrice { get; init; }
	public decimal DiscountedPrice { get; init; }
	public int DiscountPercent { get; init; }
	public int UnitsLeft { get; init; }

	public required string Description { get; init; }
	public required string PickupWindow { get; init; }

	public Restaurant? Restaurant { get; init; }
}
