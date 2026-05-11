namespace EcoFood.Models;

/// <summary>Pedido simulado (ativo ou já retirado) exibido na tela Pedidos.</summary>
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

	/// <summary>True quando ainda espera retirada; caso contrário faz parte do histórico.</summary>
	public bool IsActive { get; init; }

	public string BookingCode => $"#EF{Id.Replace("ord_", "").ToUpper()}";
}
