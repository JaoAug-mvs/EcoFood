using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EcoFood.Services;
using EcoFood.ViewModels;
using EcoFood.Views;
#if ANDROID
using Microsoft.Maui.Handlers;
#endif

namespace EcoFood;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureMauiHandlers(handlers =>
			{
#if ANDROID
				EntryHandler.Mapper.AppendToMapping("EcoFoodNoUnderline", (handler, view) =>
				{
					handler.PlatformView.Background = null;
					handler.PlatformView.SetPadding(0, 0, 0, 0);
				});
#endif
			})
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		ConfigureServices(builder.Services);

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	static void ConfigureServices(IServiceCollection services)
	{
		services.AddSingleton<AppShell>();

		services.AddSingleton<IMockDataService, MockDataService>();
		services.AddSingleton<IFavoritesService, FavoritesService>();
		services.AddSingleton<IOrdersService, OrdersService>();
		services.AddSingleton<ReservationSession>();

		services.AddTransient<HomeViewModel>();
		services.AddTransient<ExploreViewModel>();
		services.AddTransient<FavoritesViewModel>();
		services.AddTransient<OrdersViewModel>();
		services.AddTransient<ProfileViewModel>();
		services.AddTransient<ProductDetailViewModel>();
		services.AddTransient<ReservationViewModel>();
		services.AddTransient<ConfirmationViewModel>();
		services.AddTransient<SuccessViewModel>();
		services.AddTransient<OnboardingViewModel>();

		services.AddTransient<SplashPage>();
		services.AddTransient<OnboardingPage>();
		services.AddTransient<HomePage>();
		services.AddTransient<ExplorePage>();
		services.AddTransient<FavoritesPage>();
		services.AddTransient<OrdersPage>();
		services.AddTransient<ProfilePage>();
		services.AddTransient<ProductDetailPage>();
		services.AddTransient<ReservationPage>();
		services.AddTransient<ConfirmationPage>();
		services.AddTransient<SuccessPage>();
		services.AddTransient<SettingsPage>();
	}
}
