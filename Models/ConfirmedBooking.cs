namespace EcoFood.Models;

/// <summary>Snapshot do último pedido confirmado, usado em Sucesso/Mapa fora da pilha anterior.</summary>
public sealed class ConfirmedBooking
{
	public required string ProductId { get; init; }
	public required string ProductName { get; init; }
	public required string RestaurantName { get; init; }
	public required string RestaurantAddress { get; init; }
	public required string PickupWindow { get; init; }
	public required string PaymentMethodLabel { get; init; }
	public required string BookingCode { get; init; }

	public decimal Total { get; init; }
	public int Quantity { get; init; }

	public double RestaurantLatitude { get; init; }
	public double RestaurantLongitude { get; init; }

	public string ProductImageUrl { get; init; } = "";
}
