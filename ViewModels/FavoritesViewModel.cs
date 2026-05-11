using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoFood.Services;
using EcoFood.ViewModels.Base;
using Microsoft.Maui.ApplicationModel;

namespace EcoFood.ViewModels;

/// <summary>Listagem dos produtos favoritados pelo usuário (sessão atual).</summary>
public sealed partial class FavoritesViewModel : ObservableObject
{
	readonly IMockDataService _mock;
	readonly IFavoritesService _favs;

	public FavoritesViewModel(IMockDataService mock, IFavoritesService favs)
	{
		_mock = mock;
		_favs = favs;
		Reload();
		RefreshRestaurantFavorites();
		_favs.FavoritesChanged += (_, _) =>
			MainThread.BeginInvokeOnMainThread(() =>
			{
				Reload();
				RefreshRestaurantFavorites();
			});
	}

	public ObservableCollection<ProductListItemVm> Items { get; } = new();

	public ObservableCollection<FavoriteRestaurantItem> RestaurantItems { get; } = new();

	[ObservableProperty]
	bool productsTabSelected = true;

	partial void OnProductsTabSelectedChanged(bool value)
		=> OnPropertyChanged(nameof(RestaurantsTabSelected));

	public bool RestaurantsTabSelected => !ProductsTabSelected;

	public bool HasAny => Items.Count > 0;

	public void Reload()
	{
		Items.Clear();
		foreach (var p in _mock.AllProducts.OrderByDescending(p => p.DiscountPercent))
		{
			if (!_favs.Contains(p.Id))
				continue;

			var row = ProductListItemVm.FromProduct(p);
			row.IsFavorite = true;
			Items.Add(row);
		}

		OnPropertyChanged(nameof(HasAny));
	}

	public void RefreshRestaurantFavorites()
	{
		RestaurantItems.Clear();
		foreach (var group in _mock.AllProducts
				.Where(p => _favs.Contains(p.Id))
				.GroupBy(p => p.RestaurantName)
				.OrderBy(g => g.Key))
		{
			RestaurantItems.Add(new FavoriteRestaurantItem
			{
				Name = group.Key,
				Subtitle = $"{group.Count()} pratos salvos",
				Distance = group.First().DistanceKm
			});
		}
	}

	[RelayCommand]
	void ShowProductsTab() => ProductsTabSelected = true;

	[RelayCommand]
	void ShowRestaurantsTab() => ProductsTabSelected = false;

	[RelayCommand]
	async Task ProductDetails(ProductListItemVm? row)
	{
		if (row is null)
			return;

		var id = Uri.EscapeDataString(row.Product.Id);
		await Shell.Current.GoToAsync($"{nameof(Views.ProductDetailPage)}?ProductId={id}");
	}

	[RelayCommand]
	void ToggleFavorite(ProductListItemVm? row)
	{
		if (row is null)
			return;

		_favs.Toggle(row.Product.Id);
		Reload();
	}
}

public sealed class FavoriteRestaurantItem
{
	public string Name { get; init; } = "";
	public string Subtitle { get; init; } = "";
	public double Distance { get; init; }

	public string DistanceLabel => ViewModelLocalization.Km(Distance);
}
