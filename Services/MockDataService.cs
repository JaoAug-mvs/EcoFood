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
			FullName = "Marina Costa",
			Email = "marina.costa@email.com",
			Impact = new ImpactStats
			{
				SavedMeals = 23,
				TotalSavingsMoney = 158.50m,
				SavedCo2Kg = 12.4
			}
		};
		Categories =
		[
			new CategoryChip { Key = "Todos",      Title = "Todos",      Icon = "cat_todos.png" },
			new CategoryChip { Key = "Hamburguer", Title = "Hambúrguer", Icon = "cat_hamburguer.png" },
			new CategoryChip { Key = "Pizza",      Title = "Pizza",      Icon = "cat_pizza.png" },
			new CategoryChip { Key = "Japones",    Title = "Japonês",   Icon = "cat_japones.png" },
			new CategoryChip { Key = "Acai",       Title = "Açaí",      Icon = "cat_acai.png" },
			new CategoryChip { Key = "Massas",     Title = "Massas",    Icon = "cat_massas.png" },
			new CategoryChip { Key = "Padaria",    Title = "Padaria",   Icon = "cat_padaria.png" },
			new CategoryChip { Key = "Doces",      Title = "Doces",     Icon = "cat_doces.png" },
		];
		HomeCategories =
		[
			new CategoryChip { Key = "Todos",        Title = "Todos",        Icon = "🏠" },
			new CategoryChip { Key = "Restaurantes", Title = "Restaurantes", Icon = "🍽️" },
			new CategoryChip { Key = "Padarias",     Title = "Padarias",     Icon = "🥐" },
			new CategoryChip { Key = "Mercados",     Title = "Mercados",     Icon = "🛒" },
			new CategoryChip { Key = "Cafeterias",   Title = "Cafeterias",   Icon = "☕" },
		];
	}

	readonly ReadOnlyCollection<Product> _catalog;

	public AppUser CurrentUser { get; }

	public IReadOnlyList<CategoryChip> Categories { get; }

	public IReadOnlyList<CategoryChip> HomeCategories { get; }

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
		static Restaurant rb(string id, string name, string addr, double lat, double lng, double km)
			=> new() { Id = id, Name = name, Address = addr, Latitude = lat, Longitude = lng, DistanceKm = km };

		var r1 = rb("rst_burgerhouse", "Burger House",
			"Av. do Contorno, 5001 • Savassi, BH",        -19.9390, -43.9378, 0.7);
		var r2 = rb("rst_pizzaroma",   "Pizzaria Roma",
			"Rua Pernambuco, 450 • Funcionários, BH",     -19.9328, -43.9372, 1.2);
		var r3 = rb("rst_sushino",     "Sushi No Ar",
			"Av. Raja Gabaglia, 1000 • Gutierrez, BH",    -19.9500, -43.9550, 2.2);
		var r4 = rb("rst_acaigreen",   "AçaíGreen",
			"Rua Fernandes Tourinho, 200 • Savassi, BH",  -19.9375, -43.9360, 0.5);
		var r5 = rb("rst_trattoria",   "Trattoria Bella",
			"Rua Levindo Lopes, 150 • Savassi, BH",       -19.9395, -43.9400, 1.3);
		var r6 = rb("rst_panari",      "Panari Bakery",
			"Rua Sergipe, 1001 • Savassi, BH",            -19.9360, -43.9420, 1.5);

		var items = new List<Product>
		{
			// ── HAMBÚRGUER ────────────────────────────────────────────────────────
			new()
			{
				Id = "p_smash_burger", Name = "Smash Burger Clássico",
				RestaurantName = r1.Name, RestaurantId = r1.Id, Restaurant = r1,
				Category = "Hamburguer",
				EstablishmentType = "Restaurantes",
				ReviewCount = 142,
				ImageUrl = "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=900&q=90",
				Rating = 4.8, DistanceKm = r1.DistanceKm,
				OriginalPrice = 58.00m, DiscountedPrice = 27.84m, DiscountPercent = 52,
				UnitsLeft = 6,
				Description = "Blend smash de 180 g com cheddar artesanal, bacon crocante, alface americana e maionese da casa. Acompanha batata frita.",
				PickupWindow = "Hoje • 21:30 – 23:00",
				PickupUntilHour = "23h",
			},
			new()
			{
				Id = "p_double_bacon", Name = "Double Bacon Burger",
				RestaurantName = r1.Name, RestaurantId = r1.Id, Restaurant = r1,
				Category = "Hamburguer",
				EstablishmentType = "Restaurantes",
				ReviewCount = 98,
				ImageUrl = "https://images.unsplash.com/photo-1572802419224-296b0aeee0d9?w=900&q=90",
				Rating = 4.7, DistanceKm = r1.DistanceKm,
				OriginalPrice = 72.00m, DiscountedPrice = 36.00m, DiscountPercent = 50,
				UnitsLeft = 4,
				Description = "Dois blends de 150 g, queijo prato derretido, bacon duplo, cebola caramelizada e molho barbecue defumado.",
				PickupWindow = "Hoje • 21:00 – 22:30",
				PickupUntilHour = "22h",
			},
			new()
			{
				Id = "p_crispy_chicken", Name = "Crispy Chicken Burger",
				RestaurantName = r1.Name, RestaurantId = r1.Id, Restaurant = r1,
				Category = "Hamburguer",
				EstablishmentType = "Restaurantes",
				ReviewCount = 73,
				ImageUrl = "https://images.unsplash.com/photo-1551782450-a2132b4ba21d?w=900&q=90",
				Rating = 4.6, DistanceKm = r1.DistanceKm,
				OriginalPrice = 52.00m, DiscountedPrice = 24.96m, DiscountPercent = 52,
				UnitsLeft = 8,
				Description = "Frango empanado crocante no buttermilk, alface romana, picles e maionese temperada. Pão brioche tostado.",
				PickupWindow = "Hoje • 20:30 – 22:00",
				PickupUntilHour = "22h",
			},
			new()
			{
				Id = "p_combo_familia", Name = "Combo Família (2 Burgers)",
				RestaurantName = r1.Name, RestaurantId = r1.Id, Restaurant = r1,
				Category = "Hamburguer",
				EstablishmentType = "Restaurantes",
				ReviewCount = 45,
				ImageUrl = "https://images.unsplash.com/photo-1763689389824-dd2cea2e5772?w=900&q=90",
				Rating = 4.7, DistanceKm = r1.DistanceKm,
				OriginalPrice = 110.00m, DiscountedPrice = 49.50m, DiscountPercent = 55,
				UnitsLeft = 3,
				Description = "Dois smash burgers clássicos + duas batatas fritas médias + dois refrigerantes. Ideal para compartilhar.",
				PickupWindow = "Hoje • 22:00 – 23:30",
				PickupUntilHour = "23h",
			},

			// ── PIZZA ─────────────────────────────────────────────────────────────
			new()
			{
				Id = "p_pizza_margherita", Name = "Pizza Margherita (Família)",
				RestaurantName = r2.Name, RestaurantId = r2.Id, Restaurant = r2,
				Category = "Pizza",
				EstablishmentType = "Restaurantes",
				ReviewCount = 167,
				ImageUrl = "https://images.unsplash.com/photo-1513104890138-7c749659a591?w=900&q=90",
				Rating = 4.7, DistanceKm = r2.DistanceKm,
				OriginalPrice = 82.00m, DiscountedPrice = 45.10m, DiscountPercent = 45,
				UnitsLeft = 3,
				Description = "Borda fina, molho de tomate San Marzano, mozzarella de búfala fresca e manjericão. Tamanho família (35 cm).",
				PickupWindow = "Hoje • 22:00 – 23:30",
				PickupUntilHour = "23h",
			},
			new()
			{
				Id = "p_pizza_frango", Name = "Pizza Frango c/ Catupiry",
				RestaurantName = r2.Name, RestaurantId = r2.Id, Restaurant = r2,
				Category = "Pizza",
				EstablishmentType = "Restaurantes",
				ReviewCount = 203,
				ImageUrl = "https://images.unsplash.com/photo-1565299624946-b28f40a0ae38?w=900&q=90",
				Rating = 4.8, DistanceKm = r2.DistanceKm,
				OriginalPrice = 88.00m, DiscountedPrice = 44.00m, DiscountPercent = 50,
				UnitsLeft = 5,
				Description = "Frango desfiado temperado, catupiry original derretido, milho e azeitona preta. O clássico brasileiro.",
				PickupWindow = "Hoje • 21:30 – 23:00",
				PickupUntilHour = "23h",
			},
			new()
			{
				Id = "p_pizza_queijos", Name = "Pizza Quatro Queijos",
				RestaurantName = r2.Name, RestaurantId = r2.Id, Restaurant = r2,
				Category = "Pizza",
				EstablishmentType = "Restaurantes",
				ReviewCount = 88,
				ImageUrl = "https://images.unsplash.com/photo-1732223229355-95a1433404bf?w=900&q=90",
				Rating = 4.6, DistanceKm = r2.DistanceKm,
				OriginalPrice = 90.00m, DiscountedPrice = 40.50m, DiscountPercent = 55,
				UnitsLeft = 4,
				Description = "Mozzarella, parmesão, gorgonzola e provolone. Cobertura extra generosa para os amantes de queijo.",
				PickupWindow = "Amanhã • 19:00 – 21:00",
				PickupUntilHour = "21h",
			},

			// ── JAPONÊS ───────────────────────────────────────────────────────────
			new()
			{
				Id = "p_combo_sushi", Name = "Combo Sushi (12 peças)",
				RestaurantName = r3.Name, RestaurantId = r3.Id, Restaurant = r3,
				Category = "Japones",
				EstablishmentType = "Restaurantes",
				ReviewCount = 134,
				ImageUrl = "https://images.unsplash.com/photo-1712192644058-092c39b36b6f?w=900&q=90",
				Rating = 4.7, DistanceKm = r3.DistanceKm,
				OriginalPrice = 92.90m, DiscountedPrice = 55.74m, DiscountPercent = 40,
				UnitsLeft = 7,
				Description = "Rolls clássicos e sashimi fresco de salmão e atum. Produzido no expediente do dia, com qualidade garantida.",
				PickupWindow = "Amanhã • 18:30 – 20:00",
				PickupUntilHour = "20h",
			},
			new()
			{
				Id = "p_temaki", Name = "Temaki de Salmão (4 unid.)",
				RestaurantName = r3.Name, RestaurantId = r3.Id, Restaurant = r3,
				Category = "Japones",
				EstablishmentType = "Restaurantes",
				ReviewCount = 91,
				ImageUrl = "https://images.unsplash.com/photo-1710962169574-d1b52a2670e2?w=900&q=90",
				Rating = 4.6, DistanceKm = r3.DistanceKm,
				OriginalPrice = 68.00m, DiscountedPrice = 44.20m, DiscountPercent = 35,
				UnitsLeft = 5,
				Description = "Cones de alga nori recheados com salmão fresco, cream cheese e cebolinha. Prontos para retirada.",
				PickupWindow = "Hoje • 19:00 – 21:00",
				PickupUntilHour = "21h",
			},
			new()
			{
				Id = "p_hossomaki", Name = "Combo Hossomaki (20 peças)",
				RestaurantName = r3.Name, RestaurantId = r3.Id, Restaurant = r3,
				Category = "Japones",
				EstablishmentType = "Restaurantes",
				ReviewCount = 62,
				ImageUrl = "https://images.unsplash.com/photo-1674699991568-668e3465b2f9?w=900&q=90",
				Rating = 4.5, DistanceKm = r3.DistanceKm,
				OriginalPrice = 74.00m, DiscountedPrice = 44.40m, DiscountPercent = 40,
				UnitsLeft = 9,
				Description = "Vinte peças de hossomaki com salmão, atum e pepino. Acompanha shoyu, gengibre e wasabi.",
				PickupWindow = "Hoje • 20:00 – 22:00",
				PickupUntilHour = "22h",
			},

			// ── AÇAÍ ──────────────────────────────────────────────────────────────
			new()
			{
				Id = "p_acai_500", Name = "Açaí 500 ml Tradicional",
				RestaurantName = r4.Name, RestaurantId = r4.Id, Restaurant = r4,
				Category = "Acai",
				EstablishmentType = "Restaurantes",
				ReviewCount = 312,
				ImageUrl = "https://images.unsplash.com/photo-1684403783619-90836693c27e?w=900&q=90",
				Rating = 4.9, DistanceKm = r4.DistanceKm,
				OriginalPrice = 28.00m, DiscountedPrice = 14.00m, DiscountPercent = 50,
				UnitsLeft = 20,
				Description = "Açaí puro da Amazônia, cremoso e sem adição de conservantes. Acompanha granola crocante, banana e leite condensado.",
				PickupWindow = "Hoje • 14:00 – 17:00",
				PickupUntilHour = "17h",
			},
			new()
			{
				Id = "p_acai_bowl", Name = "Bowl de Açaí com Frutas",
				RestaurantName = r4.Name, RestaurantId = r4.Id, Restaurant = r4,
				Category = "Acai",
				EstablishmentType = "Restaurantes",
				ReviewCount = 241,
				ImageUrl = "https://images.unsplash.com/photo-1611073052682-6b943814156f?w=900&q=90",
				Rating = 4.8, DistanceKm = r4.DistanceKm,
				OriginalPrice = 36.00m, DiscountedPrice = 18.00m, DiscountPercent = 50,
				UnitsLeft = 15,
				Description = "Bowl 400 g com açaí, morango, manga, kiwi, granola e mel. Rico em antioxidantes e energia para o dia.",
				PickupWindow = "Hoje • 13:00 – 16:00",
				PickupUntilHour = "16h",
			},
			new()
			{
				Id = "p_acai_1l", Name = "Açaí 1 Litro (para compartilhar)",
				RestaurantName = r4.Name, RestaurantId = r4.Id, Restaurant = r4,
				Category = "Acai",
				EstablishmentType = "Restaurantes",
				ReviewCount = 178,
				ImageUrl = "https://images.unsplash.com/photo-1524904237821-786af6d620ca?w=900&q=90",
				Rating = 4.9, DistanceKm = r4.DistanceKm,
				OriginalPrice = 54.00m, DiscountedPrice = 24.30m, DiscountPercent = 55,
				UnitsLeft = 8,
				Description = "Pote de 1 litro de açaí cremoso. Perfeito para a família ou para dividir com amigos. Toppings incluídos.",
				PickupWindow = "Hoje • 15:00 – 18:00",
				PickupUntilHour = "18h",
			},

			// ── MASSAS ────────────────────────────────────────────────────────────
			new()
			{
				Id = "p_bolonhesa", Name = "Macarrão à Bolonhesa",
				RestaurantName = r5.Name, RestaurantId = r5.Id, Restaurant = r5,
				Category = "Massas",
				EstablishmentType = "Restaurantes",
				ReviewCount = 119,
				ImageUrl = "https://images.unsplash.com/photo-1551892374-ecf8754cf8b0?w=900&q=90",
				Rating = 4.7, DistanceKm = r5.DistanceKm,
				OriginalPrice = 55.00m, DiscountedPrice = 27.50m, DiscountPercent = 50,
				UnitsLeft = 8,
				Description = "Espaguete al dente com ragù de carne bovina moída na hora, tomates pelados e parmesão ralado na mesa.",
				PickupWindow = "Hoje • 20:30 – 22:00",
				PickupUntilHour = "22h",
			},
			new()
			{
				Id = "p_lasanha", Name = "Lasanha à Bolonhesa (2 porções)",
				RestaurantName = r5.Name, RestaurantId = r5.Id, Restaurant = r5,
				Category = "Massas",
				EstablishmentType = "Restaurantes",
				ReviewCount = 156,
				ImageUrl = "https://images.unsplash.com/photo-1709429790175-b02bb1b19207?w=900&q=90",
				Rating = 4.8, DistanceKm = r5.DistanceKm,
				OriginalPrice = 78.00m, DiscountedPrice = 35.10m, DiscountPercent = 55,
				UnitsLeft = 5,
				Description = "Lasanha clássica com massa fresca, ragù de carne, bechamel cremoso e mozzarella derretida. Duas porções generosas.",
				PickupWindow = "Hoje • 20:00 – 21:30",
				PickupUntilHour = "21h",
			},
			new()
			{
				Id = "p_carbonara", Name = "Fettuccine Carbonara",
				RestaurantName = r5.Name, RestaurantId = r5.Id, Restaurant = r5,
				Category = "Massas",
				EstablishmentType = "Restaurantes",
				ReviewCount = 87,
				ImageUrl = "https://images.unsplash.com/photo-1627207644206-a2040d60ecad?w=900&q=90",
				Rating = 4.6, DistanceKm = r5.DistanceKm,
				OriginalPrice = 62.00m, DiscountedPrice = 30.38m, DiscountPercent = 51,
				UnitsLeft = 6,
				Description = "Fettuccine com pancetta italiana, gema de ovo caipira, pecorino romano e pimenta do reino. Receita original.",
				PickupWindow = "Amanhã • 19:30 – 21:00",
				PickupUntilHour = "21h",
			},

			// ── PADARIA ───────────────────────────────────────────────────────────
			new()
			{
				Id = "p_kit_padaria", Name = "Kit Pães Artesanais",
				RestaurantName = r6.Name, RestaurantId = r6.Id, Restaurant = r6,
				Category = "Padaria",
				EstablishmentType = "Padarias",
				ReviewCount = 234,
				ImageUrl = "https://images.unsplash.com/photo-1559811814-e2c57b5e69df?w=900&q=90",
				Rating = 4.8, DistanceKm = r6.DistanceKm,
				OriginalPrice = 38.00m, DiscountedPrice = 19.90m, DiscountPercent = 47,
				UnitsLeft = 10,
				Description = "Seleção de pães de fermentação natural com crosta crocante. Sourdough, pão integral e brioche artesanal.",
				PickupWindow = "Hoje • 12:30 – 14:30",
				PickupUntilHour = "14h",
			},
			new()
			{
				Id = "p_croissant", Name = "Croissant de Presunto e Queijo (4 unid.)",
				RestaurantName = r6.Name, RestaurantId = r6.Id, Restaurant = r6,
				Category = "Padaria",
				EstablishmentType = "Padarias",
				ReviewCount = 189,
				ImageUrl = "https://images.unsplash.com/photo-1765100213033-ce0f38b4f478?w=900&q=90",
				Rating = 4.9, DistanceKm = r6.DistanceKm,
				OriginalPrice = 48.00m, DiscountedPrice = 21.60m, DiscountPercent = 55,
				UnitsLeft = 5,
				Description = "Croissants amanteigados recheados com presunto e queijo mussarela. Saídos do forno, crocantes por fora e macios por dentro.",
				PickupWindow = "Hoje • 16:00 – 18:00",
				PickupUntilHour = "18h",
			},

			// ── DOCES ─────────────────────────────────────────────────────────────
			new()
			{
				Id = "p_doce_caixa", Name = "Caixa Surprise Doces",
				RestaurantName = r6.Name, RestaurantId = r6.Id, Restaurant = r6,
				Category = "Doces",
				EstablishmentType = "Padarias",
				ReviewCount = 298,
				ImageUrl = "https://images.unsplash.com/photo-1695568181363-af5c78f4d059?w=900&q=90",
				Rating = 4.9, DistanceKm = r6.DistanceKm,
				OriginalPrice = 62.50m, DiscountedPrice = 24.99m, DiscountPercent = 60,
				UnitsLeft = 11,
				Description = "Mix aleatório de bolos individuais, brownies e cookies. Surprise box para diminuir desperdício de vitrine.",
				PickupWindow = "Hoje • 19:30 – 21:00",
				PickupUntilHour = "21h",
			},
			new()
			{
				Id = "p_brigadeiros", Name = "Caixa de Brigadeiros Gourmet (12 unid.)",
				RestaurantName = r6.Name, RestaurantId = r6.Id, Restaurant = r6,
				Category = "Doces",
				EstablishmentType = "Padarias",
				ReviewCount = 445,
				ImageUrl = "https://images.unsplash.com/photo-1702982852429-e0d0b27eb990?w=900&q=90",
				Rating = 4.9, DistanceKm = r6.DistanceKm,
				OriginalPrice = 72.00m, DiscountedPrice = 36.00m, DiscountPercent = 50,
				UnitsLeft = 8,
				Description = "Brigadeiros artesanais em sabores tradicionais e especiais: chocolate belga, pistache, churros e maracujá.",
				PickupWindow = "Hoje • 18:00 – 20:00",
				PickupUntilHour = "20h",
			},
		}.AsReadOnly();

		return items;
	}
}
