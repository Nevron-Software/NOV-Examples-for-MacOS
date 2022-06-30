#if UNIFIEDAPI
using Foundation;
#else
using MonoMac.Foundation;
#endif


namespace Nevron.Nov.ExamplesApp.Mac
{
	// Should subclass MonoMac.AppKit.NSResponder
	[Register ("AppDelegate")]
	public partial class AppDelegate
	{
	}
}

