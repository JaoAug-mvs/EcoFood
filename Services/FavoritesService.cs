namespace EcoFood.Services;

/// <seealso cref="IFavoritesService"/>
public sealed class FavoritesService : IFavoritesService
{
	readonly HashSet<string> _favoriteIds = new(StringComparer.Ordinal);

	public event EventHandler<FavoritesChangedEventArgs>? FavoritesChanged;

	public bool Contains(string productId)
	{
		if (string.IsNullOrWhiteSpace(productId))
			return false;
		return _favoriteIds.Contains(productId);
	}

	public void Toggle(string productId)
	{
		if (string.IsNullOrWhiteSpace(productId))
			return;

		bool added;
		if (_favoriteIds.Contains(productId))
		{
			_favoriteIds.Remove(productId);
			added = false;
		}
		else
		{
			_favoriteIds.Add(productId);
			added = true;
		}

		FavoritesChanged?.Invoke(this, new FavoritesChangedEventArgs { ProductId = productId, Added = added });
	}
}
