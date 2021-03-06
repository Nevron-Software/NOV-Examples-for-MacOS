using System;
using System.Collections.Generic;
using System.Linq;

#if UNIFIEDAPI
using Foundation;
using AppKit;
#else
using MonoMac.Foundation;
using MonoMac.AppKit;
#endif

namespace Nevron.Nov.ExamplesApp.Mac
{
	public partial class MainWindowController : NSWindowController
	{
		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}
	}
}

