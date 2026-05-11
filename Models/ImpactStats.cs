namespace EcoFood.Models;

/// <summary>Métricas de impacto ambiental/financeira exibidas no perfil/home (gamificação leve).</summary>
public sealed class ImpactStats
{
	public int SavedMeals { get; init; }
	public decimal TotalSavingsMoney { get; init; }
	public double SavedCo2Kg { get; init; }
}
