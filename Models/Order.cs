namespace EcoFood.Models;

public sealed class Order
{
	public required string Id { get; init; }
	public required string ProductName { get; init; }
	public required string RestaurantName { get; init; }
	public required string StatusLabel { get; init; }
	public required string PickupWindow { get; init; }
	public required string ProductImageUrl { get; init; }
	public DateTime OrderedAtUtc { get; init; }
	public decimal Total { get; init; }

	public string Address { get; init; } = "";
	public int Quantity { get; init; } = 1;
	public bool IsActive { get; init; }

	public string BookingCode => $"#EF{Id.Replace("ord_", "").ToUpper()}";
}
