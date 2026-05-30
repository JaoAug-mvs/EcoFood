using EcoFood.Models;

namespace EcoFood.Services;

public sealed class ReservationSession
{
	public Product? DraftProduct { get; set; }
	public int DraftQuantity { get; set; } = 1;
	public ConfirmedBooking? LastConfirmed { get; set; }

	public void ClearDraft()
	{
		DraftProduct = null;
		DraftQuantity = 1;
	}
}
