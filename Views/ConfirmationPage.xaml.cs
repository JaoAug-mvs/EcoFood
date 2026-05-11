using EcoFood.ViewModels;

namespace EcoFood.Views;

public partial class ConfirmationPage : ContentPage
{
    public ConfirmationPage(ConfirmationViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}
