#if WINDOWS
using System.Runtime.InteropServices;
using WinRT.Interop;

namespace EcoFood;

/// <summary>Força a janela WinUI para primeiro plano (útil quando parece que “não abre nada”).</summary>
public static class WinUiWindowFocus
{
	public static void BringToFront(Microsoft.UI.Xaml.Window window)
	{
		try
		{
			var hWnd = WindowNative.GetWindowHandle(window);
			if (hWnd == IntPtr.Zero)
				return;
			_ = ShowWindow(hWnd, SW_SHOW);
			_ = ShowWindow(hWnd, SW_RESTORE);
			_ = SetForegroundWindow(hWnd);
		}
		catch
		{
			/* ignorar */
		}
	}

	const int SW_SHOW = 5;
	const int SW_RESTORE = 9;

	[DllImport("user32.dll", SetLastError = true)]
	static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	[DllImport("user32.dll", SetLastError = true)]
	static extern bool SetForegroundWindow(IntPtr hWnd);
}
#endif
