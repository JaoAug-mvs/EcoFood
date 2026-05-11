using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoFood.Models;
using EcoFood.Services;
using EcoFood.ViewModels.Base;
using Microsoft.Maui.ApplicationModel;

namespace EcoFood.ViewModels;

/// <summary>Home EcoFood — saudações, métricas, busca, chips e lista de produtos próximos.</summary>
public partial class HomeViewModel : ObservableObject
{
	readonly IMockDataService _mock;
	readonly IFavoritesService _favs;

	public HomeViewModel(IMockDataService mock, IFavoritesService favs)
	{
		_mock = mock;
		_favs = favs;
		foreach (var c in mock.Categories)
			Categories.Add(new CategoryChip { Key = c.Key, Title = c.Title });

		UpdateSelectedCategory();
		ReloadImpactFromUser();
		ApplyFilter();
		_favs.FavoritesChanged += (_, _) =>
		{
			MainThread.BeginInvokeOnMainThread(RefreshFavoriteFlagsOnly);
		};
	}

	/// <summary>Chaves adicionadas para badges “favoritar” rápido nos cards da Home.</summary>
	public ObservableCollection<ProductListItemVm> VisibleProducts { get; } = new();

	public ObservableCollection<CategoryChip> Categories { get; } = new();

	public AppUser CurrentUser => _mock.CurrentUser;

	public string LocationLabel => "São Paulo, SP";

	public string Greeting => $"Olá, {CurrentUser.FirstName} 👋";

	public string HomeSubtitle => "Que bom ter você por aqui!";

	public ImpactStats Impact => CurrentUser.Impact;

	[ObservableProperty]
	string impactMealsFormatted = "0";

	[ObservableProperty]
	string impactMoneyFormatted = ViewModelLocalization.Money(0);

	[ObservableProperty]
	string impactCo2Formatted = "0,0 kg";

	public string FriendlySubtitle => "Que bom ter você aqui!";

	[ObservableProperty]
	string searchQuery = string.Empty;

	[ObservableProperty]
	string selectedCategoryKey = "Todos";

	partial void OnSearchQueryChanged(string value) => ApplyFilter();

	partial void OnSelectedCategoryKeyChanged(string value)
	{
		UpdateSelectedCategory();
		ApplyFilter();
	}

	[RelayCommand]
	void SelectCategory(CategoryChip? chip)
	{
		if (chip is null)
			return;

		SelectedCategoryKey = chip.Key;
		UpdateSelectedCategory();
	}

	void UpdateSelectedCategory()
	{
		foreach (var chip in Categories)
			chip.IsSelected = string.Equals(chip.Key, SelectedCategoryKey, StringComparison.OrdinalIgnoreCase);
	}

	void ApplyFilter()
	{
		string q = SearchQuery.Trim();
		IEnumerable<Product> query = _mock.AllProducts;

		if (!string.Equals(SelectedCategoryKey, "Todos", StringComparison.OrdinalIgnoreCase))
		{
			query = SelectedCategoryKey switch
			{
				"Restaurantes" => query.Where(p => string.Equals(p.Category, "Restaurantes", StringComparison.OrdinalIgnoreCase)),
				"Vegano" => query.Where(p => string.Equals(p.Category, "Vegano", StringComparison.OrdinalIgnoreCase)),
				"Padaria" => query.Where(p => string.Equals(p.Category, "Padaria", StringComparison.OrdinalIgnoreCase)),
				"Doces" => query.Where(p => string.Equals(p.Category, "Doces", StringComparison.OrdinalIgnoreCase)),
				"Japones" => query.Where(p => string.Equals(p.Category, "Japones", StringComparison.OrdinalIgnoreCase)),
				_ => query
			};
		}

		if (!string.IsNullOrWhiteSpace(q))
		{
			query = query.Where(p =>
				p.Name.Contains(q, StringComparison.OrdinalIgnoreCase)
				|| p.RestaurantName.Contains(q, StringComparison.OrdinalIgnoreCase));
		}

		query = query.OrderBy(p => p.DistanceKm);

		RebuildVisibleProducts(BuildRows(query).ToList());
	}

	IEnumerable<ProductListItemVm> BuildRows(IEnumerable<Product> products)
	{
		foreach (var p in products)
		{
			var row = ProductListItemVm.FromProduct(p);
			row.IsFavorite = _favs.Contains(p.Id);
			yield return row;
		}
	}

	void RebuildVisibleProducts(IReadOnlyList<ProductListItemVm> rows)
	{
		VisibleProducts.Clear();
		foreach (var r in rows)
			VisibleProducts.Add(r);
	}

	void RefreshFavoriteFlagsOnly()
	{
		foreach (var row in VisibleProducts)
			row.IsFavorite = _favs.Contains(row.Product.Id);
	}

	public void ReloadImpactFromUser()
	{
		ImpactMealsFormatted = $"{Impact.SavedMeals}";
		ImpactMoneyFormatted = ViewModelLocalization.Money(Impact.TotalSavingsMoney);
		ImpactCo2Formatted = $"{Impact.SavedCo2Kg:N1} kg".Replace(",", ",");
	}

	[RelayCommand]
	static Task OpenNotificationsAsync()
		=> Shell.Current.DisplayAlertAsync(
			"Avisos",
			"Você receberá promoções próximas e lembretes de retirada em breve (simulação).",
			"OK");

	[RelayCommand]
	async Task ProductDetails(ProductListItemVm? row)
	{
		if (row is null || Shell.Current is null)
			return;

		var id = Uri.EscapeDataString(row.Product.Id);
		await Shell.Current.GoToAsync($"{nameof(Views.ProductDetailPage)}?ProductId={id}");
	}

	[RelayCommand]
	void ToggleFavoriteQuick(ProductListItemVm? row)
	{
		if (row is null)
			return;

		_favs.Toggle(row.Product.Id);
		row.IsFavorite = _favs.Contains(row.Product.Id);
	}
}

/// <summary>Model leve só para dados binding no card horizontal/vertical sem poluir modelo de domínio.</summary>
public sealed partial class ProductListItemVm : ObservableObject
{
	ProductListItemVm(Product product)
	{
		Product = product;
	}

	public Product Product { get; }

	[ObservableProperty]
	bool isFavorite;

	partial void OnIsFavoriteChanged(bool value)
		=> OnPropertyChanged(nameof(FavoriteGlyph));

	public string FavoriteGlyph => IsFavorite ? "♥" : "♡";

	public string BadgeDiscount => $"-{Product.DiscountPercent}%";

	public string StatusLeft => $"Restam {Product.UnitsLeft} unidades";

	public string RatingFormatted => $"{Product.Rating:N1}".Replace(",", ",");

	public string DistanceFormatted =>
		ViewModelLocalization.Km(Product.DistanceKm);

	public string OldPriceFormatted => ViewModelLocalization.Money(Product.OriginalPrice);

	public string NewPriceFormatted => ViewModelLocalization.Money(Product.DiscountedPrice);

	public static ProductListItemVm FromProduct(Product p)
		=> new(p);
}
