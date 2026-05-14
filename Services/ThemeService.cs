namespace EcoFood.Services;

public static class ThemeService
{
    static readonly Dictionary<string, Color> _dark = new()
    {
        ["Background"]    = Color.FromArgb("#0B1512"),
        ["Surface"]       = Color.FromArgb("#132218"),
        ["SurfaceMuted"]  = Color.FromArgb("#0E1C14"),
        ["Border"]        = Color.FromArgb("#1E3528"),
        ["TextPrimary"]   = Color.FromArgb("#F3F4F6"),
        ["TextSecondary"] = Color.FromArgb("#9CA3AF"),
        ["TextAccent"]    = Color.FromArgb("#2ECC71"),
        ["PrimaryLight"]  = Color.FromArgb("#1A3D25"),
        ["Gray200"]       = Color.FromArgb("#1F2D24"),
        ["Gray300"]       = Color.FromArgb("#263823"),
    };

    static readonly Dictionary<string, Color> _light = new()
    {
        ["Background"]    = Color.FromArgb("#F9FBF8"),
        ["Surface"]       = Color.FromArgb("#FFFFFF"),
        ["SurfaceMuted"]  = Color.FromArgb("#F2F5F0"),
        ["Border"]        = Color.FromArgb("#E5E7EB"),
        ["TextPrimary"]   = Color.FromArgb("#1F2937"),
        ["TextSecondary"] = Color.FromArgb("#6B7280"),
        ["TextAccent"]    = Color.FromArgb("#155E3B"),
        ["PrimaryLight"]  = Color.FromArgb("#E6F8EA"),
        ["Gray200"]       = Color.FromArgb("#E5E7EB"),
        ["Gray300"]       = Color.FromArgb("#D1D5DB"),
    };

    public static bool IsDark { get; private set; }

    public static void Apply(bool dark)
    {
        IsDark = dark;
        var palette = dark ? _dark : _light;
        var res = Application.Current!.Resources;

        foreach (var (key, color) in palette)
        {
            res[key] = color;
            var brushKey = key + "Brush";
            if (res.ContainsKey(brushKey))
                res[brushKey] = new SolidColorBrush(color);
        }

        // AppThemeBinding no Shell só responde a Application.UserAppTheme —
        // SetTabBarBackgroundColor em runtime não funciona no WinUI (NavigationView cacheia cores).
        Application.Current!.UserAppTheme = dark ? AppTheme.Dark : AppTheme.Light;

        Preferences.Set("theme_dark", dark);
    }

    public static void LoadSaved()
        => Apply(Preferences.Get("theme_dark", false));
}
