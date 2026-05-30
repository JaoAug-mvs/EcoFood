namespace EcoFood.Models;

public sealed class ConfirmedBooking
{
	public required string ProductName { get; init; }
	public required string PickupWindow { get; init; }
	public required string BookingCode { get; init; }

	public decimal Total { get; init; }
	public string ProductImageUrl { get; init; } = "";
}
