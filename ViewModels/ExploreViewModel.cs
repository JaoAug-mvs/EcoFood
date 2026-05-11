using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoFood.Models;
using EcoFood.Services;
using EcoFood.ViewModels.Base;
using Microsoft.Maui.ApplicationModel;

namespace EcoFood.ViewModels;

/// <summary>Exploração completa das ofertas com busca rápida (aba Explorar).</summary>
public partial class ExploreViewModel : ObservableObject
{
	readonly IMockDataService _mock;
	readonly IFavoritesService _favs;

	public ExploreViewModel(IMockDataService mock, IFavoritesService favs)
	{
		_mock = mock;
		_favs = favs;
		foreach (var c in mock.Categories)
			Categories.Add(new CategoryChip { Key = c.Key, Title = c.Title });

		UpdateSelectedCategory();
		ApplyFilter();
		_favs.FavoritesChanged += (_, _) =>
		{
			MainThread.BeginInvokeOnMainThread(RefreshFavoriteFlagsOnly);
		};
	}

	public ObservableCollection<CategoryChip> Categories { get; } = new();

	public ObservableCollection<ProductListItemVm> Items { get; } = new();

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
				|| p.RestaurantName.Contains(q, StringComparison.OrdinalIgnoreCase)
				|| p.Description.Contains(q, StringComparison.OrdinalIgnoreCase));
		}

		query = query.OrderByDescending(p => p.DiscountPercent).ThenBy(p => p.DistanceKm);

		Items.Clear();
		foreach (var p in query)
		{
			var row = ProductListItemVm.FromProduct(p);
			row.IsFavorite = _favs.Contains(p.Id);
			Items.Add(row);
		}
	}

	void RefreshFavoriteFlagsOnly()
	{
		foreach (var row in Items)
			row.IsFavorite = _favs.Contains(row.Product.Id);
	}

	[RelayCommand]
	async Task ProductDetails(ProductListItemVm? row)
	{
		if (row is null)
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

	[RelayCommand]
	static Task OpenNotificationsAsync()
		=> Shell.Current.DisplayAlertAsync(
			"Avisos",
			"Você receberá promoções e alertas do EcoFood em breve.",
			"OK");
}
