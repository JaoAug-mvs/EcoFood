namespace EcoFood.Models;

/// <summary>
/// Dados de um restaurante fictício utilizados em mapas e fluxo de reserva.
/// </summary>
public sealed class Restaurant
{
	public required string Id { get; init; }
	public required string Name { get; init; }
	public required string Address { get; init; }
	public double Latitude { get; init; }
	public double Longitude { get; init; }
	public double DistanceKm { get; init; }
}
