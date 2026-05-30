using Microsoft.Extensions.DependencyInjection;

namespace EcoFood.Views;

public partial class SplashPage : ContentPage
{
    readonly IServiceProvider _services;

    public SplashPage(IServiceProvider services)
    {
        _services = services;
        InitializeComponent();

        // Windows MAUI não renderiza corretamente filhos com VerticalOptions="End"
        // na primeira passagem de layout quando ContentPage é a raiz da Window.
        // Forçar IsVisible toggle em OnAppearing resolve o bug sem flash visível.
        SizeChanged += OnFirstSizeChanged;
    }

    bool _layoutFixed;
    void OnFirstSizeChanged(object? sender, EventArgs e)
    {
        if (_layoutFixed || Height <= 0) return;
        _layoutFixed = true;
        SizeChanged -= OnFirstSizeChanged;

        // Força o MAUI a repassar o layout da seção inferior
        BottomSection.IsVisible = false;
        Dispatcher.Dispatch(() => BottomSection.IsVisible = true);
    }

    void OnStartOnboardingClicked(object? sender, EventArgs e)
        => NavigateTo<OnboardingPage>();

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
