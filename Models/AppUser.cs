namespace EcoFood.Models;

/// <summary>Usuário simulado (sem autenticação real).</summary>
public sealed class AppUser
{
	public required string FullName { get; init; }
	public string FirstName => FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? FullName;
	public required string Email { get; init; }
	public ImpactStats Impact { get; init; } = new();
}
