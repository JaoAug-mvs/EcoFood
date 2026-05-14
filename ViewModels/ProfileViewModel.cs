using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoFood.Models;
using EcoFood.Services;
using EcoFood.ViewModels.Base;

namespace EcoFood.ViewModels;

public sealed partial class ProfileViewModel : ObservableObject
{
	readonly IFavoritesService _favorites;
	readonly IMockDataService _mock;

	public ProfileViewModel(IMockDataService mock, IFavoritesService favorites)
	{
		_mock = mock;
		_favorites = favorites;

		RefreshFavoriteTotals();
		_favorites.FavoritesChanged += (_, _) => RefreshFavoriteTotals();
		ReloadImpact();
		isDarkTheme = ThemeService.IsDark;
	}

	public string FullName => _mock.CurrentUser.FullName;

	public string Email => _mock.CurrentUser.Email;

	public ImpactBannerVm Impact { get; } = new();

	[ObservableProperty]
	int favoritesCount;

	[ObservableProperty]
	bool isDarkTheme;

	partial void OnIsDarkThemeChanged(bool value)
		=> ThemeService.Apply(value);

	public void RefreshFavoriteTotals()
	{
		FavoritesCount = _mock.AllProducts.Count(p => _favorites.Contains(p.Id));
	}

	public void ReloadImpact()
	{
		var i = _mock.CurrentUser.Impact;
		Impact.Meals = $"{i.SavedMeals}";
		Impact.MoneyLabel = ViewModelLocalization.Money(i.TotalSavingsMoney);
		Impact.Co2 = $"{i.SavedCo2Kg:N1} kg".Replace(",", ",");
		OnPropertyChanged(nameof(Impact));
	}

	[RelayCommand]
	static Task OpenOrdersShortcutAsync()
		=> Shell.Current.GoToAsync("//OrdersTab/OrdersRoot");

	[RelayCommand]
	static Task OpenFavoritesShortcutAsync()
		=> Shell.Current.GoToAsync("//FavoritesTab/FavoritesRoot");

	[RelayCommand]
	static Task OpenAddressesAsync()
		=> Shell.Current.DisplayAlertAsync("Endereços", "Funcionalidade para gerenciar endereços estará disponível em breve.", "OK");

	[RelayCommand]
	static Task OpenPaymentMethodsAsync()
		=> Shell.Current.DisplayAlertAsync("Formas de pagamento", "Em breve você poderá editar cartões e formas de pagamento.", "OK");

	[RelayCommand]
	static Task OpenFoodPreferencesAsync()
		=> Shell.Current.DisplayAlertAsync("Preferências alimentares", "Em breve você poderá definir restrições e preferências alimentares.", "OK");

	[RelayCommand]
	static Task OpenNotificationsSettingsAsync()
		=> Shell.Current.DisplayAlertAsync("Notificações", "Ative ou desative alertas e promoções na próxima atualização.", "OK");

	[RelayCommand]
	static Task OpenSupportAsync()
		=> Shell.Current.DisplayAlertAsync("Ajuda e suporte", "Nossa equipe está pronta para ajudar por e-mail ou chat.", "OK");

	[RelayCommand]
	static Task OpenAboutAsync()
		=> Shell.Current.DisplayAlertAsync("Sobre o EcoFood", "EcoFood é um app para resgatar comida e reduzir desperdício.", "OK");

	[RelayCommand]
	static Task SignOutAsync()
		=> Shell.Current.DisplayAlertAsync("Sair", "Você será desconectado em breve.", "OK");

	[RelayCommand]
	static Task OpenSettingsShortcutAsync()
		=> Shell.Current.GoToAsync(nameof(Views.SettingsPage));
}

public sealed class ImpactBannerVm
{
	public string Meals { get; set; } = "0";

	public string MoneyLabel { get; set; } = ViewModelLocalization.Money(0);

	public string Co2 { get; set; } = "0,0 kg";
}
