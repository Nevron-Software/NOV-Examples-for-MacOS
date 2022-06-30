using System;

using Nevron.Nov.Examples;
using Nevron.Nov.Mac;
using CoreGraphics;

#if UNIFIEDAPI
using Foundation;
using AppKit;
#else
using MonoMac.Foundation;
using MonoMac.AppKit;
#endif

namespace Nevron.Nov.ExamplesApp.Mac
{
	public partial class MainWindow : NSWindow
	{
		#region Constructors

		public MainWindow()
			: base(new CGRect(0, 0, 400, 400), NSWindowStyle.Resizable | NSWindowStyle.Titled | NSWindowStyle.Closable, NSBackingStore.Buffered, false)
		{
			Initialize();
		}

        #endregion

        #region Implementation

        /// <summary>
        /// Shared initialization code.
        /// </summary>
        private void Initialize()
		{
			Title = "Nevron Open Vision Examples";
			SetFrame(NSScreen.MainScreen.VisibleFrame, true);

			//NUISettings.EnableMultiThreadedPainting = false;
			//NUISettings.EnablePaintCache = false;

			// place the host inside the mac window
			NNovWidgetHost widget = new NNovWidgetHost(new NExamplesContent());
			ContentView = widget;
		}

		#endregion
	}
}