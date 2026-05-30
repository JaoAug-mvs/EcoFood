namespace EcoFood.Models;

public sealed class Restaurant
{
	public required string Id { get; init; }
	public required string Name { get; init; }
	public required string Address { get; init; }
	public double Latitude { get; init; }
	public double Longitude { get; init; }
	public double DistanceKm { get; init; }
}
