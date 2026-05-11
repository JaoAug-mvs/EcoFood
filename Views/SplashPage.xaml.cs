using Microsoft.Extensions.DependencyInjection;

namespace EcoFood.Views;

public partial class SplashPage : ContentPage
{
    readonly IServiceProvider _services;

    public SplashPage(IServiceProvider services)
    {
        _services = services;
        InitializeComponent();
    }

    void OnStartOnboardingClicked(object? sender, EventArgs e)
    {
        if (Preferences.Get("onboarding_done", false))
            NavigateTo<AppShell>();
        else
            NavigateTo<OnboardingPage>();
    }

    void OnLoginClicked(object? sender, EventArgs e)
        => NavigateTo<AppShell>();

    void NavigateTo<T>() where T : Page
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            try
            {
                var window = Window ?? Application.Current?.Windows.FirstOrDefault();
                if (window is null) return;
                window.Page = _services.GetRequiredService<T>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SplashPage] Erro ao navegar: {ex}");
            }
        });
    }
}
