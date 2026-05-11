namespace EcoFood;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		RegisterRoutes();
	}

	void RegisterRoutes()
	{
		Routing.RegisterRoute(nameof(Views.ProductDetailPage), typeof(Views.ProductDetailPage));
		Routing.RegisterRoute(nameof(Views.ReservationPage), typeof(Views.ReservationPage));
		Routing.RegisterRoute(nameof(Views.ConfirmationPage), typeof(Views.ConfirmationPage));
		Routing.RegisterRoute(nameof(Views.SuccessPage), typeof(Views.SuccessPage));
		Routing.RegisterRoute(nameof(Views.SettingsPage), typeof(Views.SettingsPage));
	}
}
