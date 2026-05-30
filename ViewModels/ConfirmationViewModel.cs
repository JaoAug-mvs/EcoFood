using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoFood.Models;
using EcoFood.Services;
using EcoFood.ViewModels.Base;

namespace EcoFood.ViewModels;

public sealed partial class ConfirmationViewModel : ObservableObject
{
    readonly ReservationSession _session;
    readonly IOrdersService _ordersService;

    public ConfirmationViewModel(ReservationSession session, IOrdersService ordersService)
    {
        _session = session;
        _ordersService = ordersService;
    }

    Product? Product => _session.DraftProduct;

    public string ProductImageUrl => Product?.ImageUrl ?? string.Empty;

    public string ProductName => Product?.Name ?? string.Empty;

    public string RestaurantName => Product?.RestaurantName ?? string.Empty;

    public string PickupLine => Product?.PickupWindow ?? string.Empty;

    public string OrderQuantity => $"Quantidade: {_session.DraftQuantity}";

    public string AddressLine => Product?.Restaurant?.Address ?? string.Empty;

    public string DistanceLine => Product?.Restaurant is null ? string.Empty : ViewModelLocalization.Km(Product.Restaurant.DistanceKm);

    public string TotalFormatted => Product is null
        ? ViewModelLocalization.Money(0)
        : ViewModelLocalization.Money(Product.DiscountedPrice * Math.Max(1, _session.DraftQuantity));

    [RelayCommand]
    async Task ConfirmReservationAsync()
    {
        if (Product is null || Product.Restaurant is null)
            return;

        var booking = new ConfirmedBooking
        {
            ProductName = Product.Name,
            PickupWindow = Product.PickupWindow,
            BookingCode = $"REF{DateTime.UtcNow:HHmmss}",
            Total = Product.DiscountedPrice * Math.Max(1, _session.DraftQuantity),
            ProductImageUrl = Product.ImageUrl
        };

        var idCore = Guid.NewGuid().ToString("N")[..12];
        _ordersService.Add(new Order
        {
            Id = $"ord_{idCore}",
            ProductName = Product.Name,
            RestaurantName = Product.Restaurant.Name,
            StatusLabel = "Confirmado",
            PickupWindow = $"Retirada • {Product.PickupWindow}",
            ProductImageUrl = Product.ImageUrl,
            OrderedAtUtc = DateTime.UtcNow,
            Total = booking.Total,
            IsActive = true
        });

        _session.LastConfirmed = booking;
        _session.ClearDraft();

        await Shell.Current.GoToAsync(nameof(Views.SuccessPage));
    }

    [RelayCommand]
    static Task EditReservationAsync()
        => Shell.Current.GoToAsync(nameof(Views.ReservationPage));
}
