using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace EcoFood.ViewModels;

public sealed partial class OnboardingViewModel : ObservableObject
{
    public OnboardingViewModel()
    {
        Slides.Add(new OnboardingSlide(
            "EcoFood",
            "Alimentação saudável e sustentável para seu dia a dia, com menos desperdício e mais sabor.",
            "logo_verde_sem_fundo.png"));
        Slides.Add(new OnboardingSlide(
            "Menos desperdício",
            "Encontre pratos preparados com ingredientes selecionados e entregues com responsabilidade ambiental.",
            "logo_branca_sem_fundo.png"));
        Slides.Add(new OnboardingSlide(
            "Pedidos rápidos",
            "Reserve, acompanhe e retire com facilidade — uma experiência pensada para o seu tempo.",
            "logo_verde_sem_fundo.png"));
    }

    public ObservableCollection<OnboardingSlide> Slides { get; } = new();

    [ObservableProperty]
    int currentIndex;

    public bool IsLastSlide => CurrentIndex == Slides.Count - 1;

    public string PrimaryButtonText => IsLastSlide ? "Começar" : "Próximo";

    partial void OnCurrentIndexChanged(int value)
    {
        OnPropertyChanged(nameof(IsLastSlide));
        OnPropertyChanged(nameof(PrimaryButtonText));
    }
}

public sealed class OnboardingSlide
{
    public OnboardingSlide(string title, string subtitle, string imageSource)
    {
        Title = title;
        Subtitle = subtitle;
        ImageSource = imageSource;
    }

    public string Title { get; }

    public string Subtitle { get; }

    public string ImageSource { get; }
}
