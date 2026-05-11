using EcoFood.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace EcoFood.Views;

public partial class OnboardingPage : ContentPage
{
    readonly IServiceProvider _services;
    readonly OnboardingViewModel _vm;
    CancellationTokenSource? _resetCts;

    public OnboardingPage(OnboardingViewModel viewModel, IServiceProvider services)
    {
        InitializeComponent();
        _vm = viewModel;
        BindingContext = _vm;
        _services = services;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _resetCts?.Cancel();
        _resetCts = new CancellationTokenSource();
        try
        {
            // Wait long enough for the Windows CarouselView init bug to fire
            await Task.Delay(500, _resetCts.Token);
            SlidesCarousel.Position = 0;
            _vm.CurrentIndex = 0;
        }
        catch (OperationCanceledException) { }
    }

    void OnSkipClicked(object? sender, EventArgs e)
    {
        _resetCts?.Cancel();
        NavigateToAppShell();
    }

    void OnStartClicked(object? sender, EventArgs e)
    {
        _resetCts?.Cancel(); // prevent reset from undoing user action
        _vm.CurrentIndex = SlidesCarousel.Position; // sync with actual carousel state

        if (_vm.IsLastSlide)
        {
            NavigateToAppShell();
            return;
        }

        var next = _vm.CurrentIndex + 1;
        SlidesCarousel.Position = next;
        _vm.CurrentIndex = next;
    }

    void NavigateToAppShell()
    {
        Preferences.Set("onboarding_done", true);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            try
            {
                var window = Window ?? Application.Current?.Windows.FirstOrDefault();
                if (window is null) return;
                window.Page = _services.GetRequiredService<AppShell>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[OnboardingPage] Erro ao navegar: {ex}");
            }
        });
    }
}
