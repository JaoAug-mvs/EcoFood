using System.Runtime.InteropServices;
using Microsoft.UI.Xaml;

namespace EcoFood.WinUI;

public partial class App : MauiWinUIApplication
{
	public App()
	{
		if (GetConsoleWindow() != IntPtr.Zero)
			_ = FreeConsole();

		this.InitializeComponent();
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	[DllImport("kernel32.dll")]
	static extern IntPtr GetConsoleWindow();

	[DllImport("kernel32.dll", SetLastError = true)]
	static extern bool FreeConsole();
}
