using Microsoft.Extensions.DependencyInjection;
using EcoFood.Views;
using MauiApplication = Microsoft.Maui.Controls.Application;
using MauiWindow = Microsoft.Maui.Controls.Window;

namespace EcoFood;

public partial class App : MauiApplication
{
	public App()
	{
		InitializeComponent();
		Services.ThemeService.LoadSaved();
	}

	protected override MauiWindow CreateWindow(IActivationState? activationState)
	{
		var services = Current?.Handler?.MauiContext?.Services
			?? throw new InvalidOperationException("Serviços MAUI indisponíveis em CreateWindow.");

		// Splash sempre aparece — é a tela de boas-vindas/login
		var startPage = services.GetRequiredService<SplashPage>();

		var window = new MauiWindow(startPage)
		{
			Title = "EcoFood",
			Width = 420,
			Height = 780,
		};

#if WINDOWS
		void TryActivate()
		{
			try
			{
				if (window.Handler?.PlatformView is Microsoft.UI.Xaml.Window nativeWin)
				{
					nativeWin.Activate();
					WinUiWindowFocus.BringToFront(nativeWin);
					MainThread.BeginInvokeOnMainThread(async () =>
					{
						await Task.Delay(400).ConfigureAwait(true);
						if (window.Handler?.PlatformView is Microsoft.UI.Xaml.Window w)
						{
							w.Activate();
							WinUiWindowFocus.BringToFront(w);
						}
					});
				}
			}
			catch { }
		}

		window.Created += (_, _) => TryActivate();
		window.HandlerChanged += (_, _) => TryActivate();
#endif

		return window;
	}
}
