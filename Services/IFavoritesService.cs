namespace EcoFood.Services;

public interface IFavoritesService
{
	event EventHandler<FavoritesChangedEventArgs>? FavoritesChanged;
	bool Contains(string productId);
	void Toggle(string productId);
}

public sealed class FavoritesChangedEventArgs : EventArgs
{
	public required string ProductId { get; init; }
	public bool Added { get; init; }
}
