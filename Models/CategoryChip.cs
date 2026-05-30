using CommunityToolkit.Mvvm.ComponentModel;

namespace EcoFood.Models;

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
