using CommunityToolkit.Mvvm.ComponentModel;

namespace EcoFood.Models;

/// <summary>Item exibido na faixa horizontal de categorias/chips na Home.</summary>
public sealed partial class CategoryChip : ObservableObject
{
	[ObservableProperty]
	string key = string.Empty;

	[ObservableProperty]
	string title = string.Empty;

	[ObservableProperty]
	string icon = string.Empty;

	[ObservableProperty]
	bool isSelected;
}
