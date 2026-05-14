using EcoFood.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace EcoFood.Views;

public partial class OnboardingPage : ContentPage
{
    readonly IServiceProvider _services;
    readonly OnboardingViewModel _vm;

    public OnboardingPage(OnboardingViewModel viewModel, IServiceProvider services)
    {
        InitializeComponent();
        _vm = viewModel;
        BindingContext = _vm;
        _services = services;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.CurrentIndex = 0; // sempre começa no slide 1, sem timing hack
    }

    void OnSkipClicked(object? sender, EventArgs e)
        => NavigateToAppShell();

    void OnStartClicked(object? sender, EventArgs e)
    {
        if (_vm.IsLastSlide)
        {
            NavigateToAppShell();
            return;
        }
        _vm.CurrentIndex++;
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
