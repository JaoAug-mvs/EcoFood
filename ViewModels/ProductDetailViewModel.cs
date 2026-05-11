using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoFood.Models;
using EcoFood.Services;
using EcoFood.ViewModels.Base;

namespace EcoFood.ViewModels;

/// <summary>Página detalhes com preços, texto e início da reserva.</summary>
public sealed partial class ProductDetailViewModel : ObservableObject
{
	readonly IMockDataService _mock;
	readonly IFavoritesService _favorites;
	readonly ReservationSession _session;

	public ProductDetailViewModel(IMockDataService mock, IFavoritesService favorites, ReservationSession session)
	{
		_mock = mock;
		_favorites = favorites;
		_session = session;

		_favorites.FavoritesChanged += (_, e) =>
		{
			if (Product is not null && e.ProductId == Product.Id)
			{
				OnPropertyChanged(nameof(FavoriteLabel));
				OnPropertyChanged(nameof(IsFavorite));
				OnPropertyChanged(nameof(FavoriteGlyph));
			}
		};
	}

	Product? _product;

	public Product? Product
	{
		get => _product;
		private set
		{
			if (!SetProperty(ref _product, value))
				return;

			OnPropertyChanged(nameof(HasProduct));
			OnPropertyChanged(nameof(TitleDisplay));
			OnPropertyChanged(nameof(RestaurantDisplay));
			OnPropertyChanged(nameof(RatingFormatted));
			OnPropertyChanged(nameof(DistanceFormatted));
			OnPropertyChanged(nameof(OldPriceFormatted));
			OnPropertyChanged(nameof(NewPriceFormatted));
			OnPropertyChanged(nameof(BadgeDiscount));
			OnPropertyChanged(nameof(StatusLeftFormatted));
			OnPropertyChanged(nameof(PickupFormatted));
			OnPropertyChanged(nameof(DescriptionFormatted));
			OnPropertyChanged(nameof(SavingsFormatted));
			OnPropertyChanged(nameof(CategoryLabel));
			OnPropertyChanged(nameof(PickupShort));
			OnPropertyChanged(nameof(UnitsLeftShort));
			OnPropertyChanged(nameof(RestaurantAddress));
			OnPropertyChanged(nameof(RestaurantDistanceFormatted));
			OnPropertyChanged(nameof(FavoriteLabel));
			UpdateFavoriteFlag();
		}
	}

	public bool HasProduct => Product is not null;

	public string TitleDisplay => Product?.Name ?? "";

	public string RestaurantDisplay => Product?.RestaurantName ?? "";

	public string RatingFormatted =>
		Product is null ? "" : $"{Product.Rating:N1}".Replace(",", ",");

	public string DistanceFormatted =>
		Product is null ? "" : ViewModelLocalization.Km(Product.DistanceKm);

	public string OldPriceFormatted =>
		Product is null ? "" : ViewModelLocalization.Money(Product.OriginalPrice);

	public string NewPriceFormatted =>
		Product is null ? "" : ViewModelLocalization.Money(Product.DiscountedPrice);

	public string BadgeDiscount =>
		Product is null ? "" : $"-{Product.DiscountPercent}%";

	public string StatusLeftFormatted =>
		Product is null ? "" : $"Restam {Product.UnitsLeft} unidades disponíveis";

	public string PickupFormatted =>
		Product is null ? "" : $"Retirada • {Product.PickupWindow}";

	public string DescriptionFormatted => Product?.Description ?? "";

	public string SavingsFormatted =>
		Product is null ? "" : $"Você economiza {ViewModelLocalization.Money(Product.OriginalPrice - Product.DiscountedPrice)}";

	public string CategoryLabel => Product?.Category ?? "";

	public string PickupShort => Product?.PickupWindow ?? "";

	public string UnitsLeftShort =>
		Product is null ? "" : $"Restam {Product.UnitsLeft} unidades";

	public string RestaurantAddress => Product?.Restaurant?.Address ?? "";

	public string RestaurantDistanceFormatted =>
		Product?.Restaurant is null ? "" : ViewModelLocalization.Km(Product.Restaurant.DistanceKm);

	public string FavoriteLabel =>
		Product is not null && _favorites.Contains(Product.Id)
			? "Remover dos favoritos"
			: "Salvar nos favoritos";

	public bool IsFavorite => Product is not null && _favorites.Contains(Product.Id);

	/// <summary>Glyph simples para botão circular de favoritos (♥ / ♡).</summary>
	public string FavoriteGlyph => IsFavorite ? "♥" : "♡";

	public void Load(string? productId)
	{
		if (string.IsNullOrWhiteSpace(productId))
			Product = null;
		else
			Product = _mock.FindProduct(productId);
	}

	void UpdateFavoriteFlag()
	{
		OnPropertyChanged(nameof(FavoriteLabel));
		OnPropertyChanged(nameof(IsFavorite));
		OnPropertyChanged(nameof(FavoriteGlyph));
	}

	[RelayCommand]
	void ToggleFavorite()
	{
		if (Product is null)
			return;

		_favorites.Toggle(Product.Id);
		OnPropertyChanged(nameof(FavoriteLabel));
		OnPropertyChanged(nameof(IsFavorite));
		OnPropertyChanged(nameof(FavoriteGlyph));
	}

	[RelayCommand]
	async Task StartReservationAsync()
	{
		if (Product is null)
			return;

		_session.DraftProduct = Product;
		_session.DraftQuantity = 1;
		await Shell.Current.GoToAsync(nameof(Views.ReservationPage));
	}
}
