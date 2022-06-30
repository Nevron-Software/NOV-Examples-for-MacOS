#if UNIFIEDAPI 
using Foundation;
#else
using MonoMac.Foundation;
#endif


namespace Nevron.Nov.ExamplesApp.Mac
{
	// Should subclass MonoMac.AppKit.NSWindow
	[Register ("MainWindow")]
	public partial class MainWindow
	{
	}
	// Should subclass MonoMac.AppKit.NSWindowController
	[Register ("MainWindowController")]
	public partial class MainWindowController
	{
	}
}

