using System.Collections.ObjectModel;
using EcoFood.Models;

namespace EcoFood.Services;

/// <summary>Implementação de dados fictícios com valores inspirados nos wireframes EcoFood.</summary>
public sealed class MockDataService : IMockDataService
{
	public MockDataService()
	{
		_catalog = BuildCatalog();
		CurrentUser = new AppUser
		{
			FullName = "João Silva",
			Email = "joao.silva@email.com",
			Impact = new ImpactStats
			{
				SavedMeals = 23,
				TotalSavingsMoney = 158.50m,
				SavedCo2Kg = 12.4
			}
		};
		Categories =
		[
			new CategoryChip { Key = "Todos", Title = "Todos" },
			new CategoryChip { Key = "Restaurantes", Title = "Restaurantes" },
			new CategoryChip { Key = "Vegano", Title = "Vegano" },
			new CategoryChip { Key = "Padaria", Title = "Padaria" },
			new CategoryChip { Key = "Doces", Title = "Doces" },
			new CategoryChip { Key = "Japones", Title = "Japonês" }
		];
	}

	readonly ReadOnlyCollection<Product> _catalog;

	public AppUser CurrentUser { get; }

	public IReadOnlyList<CategoryChip> Categories { get; }

	public IReadOnlyList<Product> AllProducts => _catalog;

	public Restaurant? FindRestaurant(string id)
	{
		foreach (var p in _catalog)
			if (p.Restaurant?.Id == id)
				return p.Restaurant;
		return null;
	}

	public Product? FindProduct(string id)
		=> _catalog.FirstOrDefault(p => p.Id == id);

	static ReadOnlyCollection<Product> BuildCatalog()
	{
		// Coordenadas aproximadas (centro paulistano): rota fictícia “curta”.
		static Restaurant rb(string id, string name, string addr, double lat, double lng, double km)
			=> new()
			{
				Id = id,
				Name = name,
				Address = addr,
				Latitude = lat,
				Longitude = lng,
				DistanceKm = km
			};

		var r1 = rb("rst_greenbowl", "Green Bowl",
			"Rua Augusta, 1200 • Consolação, SP",
			-23.5542, -46.6625, 0.8);

		var r2 = rb("rst_panari", "Panari Bakery",
			"Alameda Santos, 420 • Bela Vista, SP",
			-23.5621, -46.6683, 1.5);

		var r3 = rb("rst_sushino", "Sushi No Ar",
			"Rua dos Pinheiros, 930 • Pinheiros, SP",
			-23.5691, -46.6942, 2.2);

		var items = new List<Product>
		{
			new()
			{
				Id = "p_combo_vegano",
				Name = "Combo Vegano",
				RestaurantName = r1.Name,
				RestaurantId = r1.Id,
				Category = "Vegano",
				ImageUrl = "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=900&q=80",
				Rating = 4.6,
				DistanceKm = r1.DistanceKm,
				OriginalPrice = 45.90m,
				DiscountedPrice = 22.95m,
				DiscountPercent = 50,
				UnitsLeft = 4,
				Description =
					"Marmita com quinoa, tofu grelhado, legumes assados e molho oriental. Produzido com ingredientes frescos e ideal para não desperdiçar o dia.",
				PickupWindow = "Hoje • 17:00 - 19:00",
				Restaurant = r1
			},
			new()
			{
				Id = "p_kit_padaria",
				Name = "Kit Pães Artesanais",
				RestaurantName = r2.Name,
				RestaurantId = r2.Id,
				Category = "Padaria",
				ImageUrl = "https://images.unsplash.com/photo-1509440159599-074908fcdf28?w=900&q=80",
				Rating = 4.8,
				DistanceKm = r2.DistanceKm,
				OriginalPrice = 38.00m,
				DiscountedPrice = 19.90m,
				DiscountPercent = 47,
				UnitsLeft = 10,
				Description =
					"Seleção de pães de fermentação natural com crosta crocante. Perfeitos para café da manhã e redução de sobras no balcão.",
				PickupWindow = "Hoje • 12:30 - 14:30",
				Restaurant = r2
			},
			new()
			{
				Id = "p_combo_sushi",
				Name = "Combo Sushi (12 peças)",
				RestaurantName = r3.Name,
				RestaurantId = r3.Id,
				Category = "Japones",
				ImageUrl = "https://images.unsplash.com/photo-1579584425555-c3ce17fd4351?w=900&q=80",
				Rating = 4.7,
				DistanceKm = r3.DistanceKm,
				OriginalPrice = 92.90m,
				DiscountedPrice = 55.74m,
				DiscountPercent = 40,
				UnitsLeft = 7,
				Description =
					"Variado com rolls clássicos e sashimi fresco. Produzido com sobras do expediente preservando qualidade e sabor.",
				PickupWindow = "Amanhã • 18:30 - 20:00",
				Restaurant = r3
			},
			new()
			{
				Id = "p_doce_caixa",
				Name = "Caixa Surprise Doces",
				RestaurantName = r2.Name,
				RestaurantId = r2.Id,
				Category = "Doces",
				ImageUrl = "https://images.unsplash.com/photo-1578985545062-c699dcb9d068?w=900&q=80",
				Rating = 4.9,
				DistanceKm = r2.DistanceKm,
				OriginalPrice = 62.50m,
				DiscountedPrice = 24.99m,
				DiscountPercent = 60,
				UnitsLeft = 11,
				Description =
					"Mix aleatório de bolos individuais, brownies e brownies veganos. Surprise box para diminuir desperdício de vitrine.",
				PickupWindow = "Hoje • 19:30 - 21:00",
				Restaurant = r2
			},
			new()
			{
				Id = "p_sopa_garden",
				Name = "Sopa Gardênia XL",
				RestaurantName = r1.Name,
				RestaurantId = r1.Id,
				Category = "Vegano",
				ImageUrl = "https://images.unsplash.com/photo-1547592166-23ac45744acd?w=900&q=80",
				Rating = 4.5,
				DistanceKm = r1.DistanceKm,
				OriginalPrice = 34.90m,
				DiscountedPrice = 13.96m,
				DiscountPercent = 60,
				UnitsLeft = 14,
				Description =
					"Pote térmico 700ml com creme nutritivo + croutons. Ideal para aquecer seu dia sustentável.",
				PickupWindow = "Hoje • 11:00 - 12:50",
				Restaurant = r1
			},
			new()
			{
				Id = "p_lanche_cl",
				Name = "Lanche Clube Noturno",
				RestaurantName = r3.Name,
				RestaurantId = r3.Id,
				Category = "Restaurantes",
				ImageUrl = "https://images.unsplash.com/photo-1528605248644-c14bde6cce18?w=900&q=80",
				Rating = 4.6,
				DistanceKm = r3.DistanceKm,
				OriginalPrice = 72.90m,
				DiscountedPrice = 29.99m,
				DiscountPercent = 58,
				UnitsLeft = 5,
				Description =
					"Dupla de sanduíches com frango karaage defumado + salada rápida. Restaurante fecha cedo — aproveita excedentes.",
				PickupWindow = "Hoje • 22:40 - 23:59",
				Restaurant = r3
			}
		}.AsReadOnly();

		return items;
	}
}
