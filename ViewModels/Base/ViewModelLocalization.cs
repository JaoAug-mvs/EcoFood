using System.Globalization;

namespace EcoFood.ViewModels.Base;

internal static class ViewModelLocalization
{
	internal static CultureInfo PtBr { get; } = new("pt-BR");

	internal static string Money(decimal value)
		=> value.ToString("C", PtBr);

	internal static string Km(double kilometers)
		=> $"{kilometers.ToString("N1", PtBr)} km";
}
