using EcoFood.Models;

namespace EcoFood.Services;

/// <summary>Rascunhos e snapshot do último pedido confirmado (passagem simples entre views sem backend).</summary>
public sealed class ReservationSession
{
	/// <summary>Produto selecionado antes de confirmar a reserva atual.</summary>
	public Product? DraftProduct { get; set; }

	/// <summary>Quantidade selecionada no fluxo de reserva.</summary>
	public int DraftQuantity { get; set; } = 1;

	/// <summary>Valor apresentado após Confirmação (Sucesso / Mapa).</summary>
	public ConfirmedBooking? LastConfirmed { get; set; }

	public void ClearDraft()
	{
		DraftProduct = null;
		DraftQuantity = 1;
	}
}
