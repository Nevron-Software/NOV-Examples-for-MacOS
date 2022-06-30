using Nevron.Nov.Mac;
using Nevron.Nov.Text;
using Nevron.Nov.Chart;
using Nevron.Nov.Diagram;
using Nevron.Nov.Schedule;
using Nevron.Nov.Grid;
using Nevron.Nov.Barcode;

#if UNIFIEDAPI
using AppKit;
#else
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
#endif

namespace Nevron.Nov.ExamplesApp.Mac
{
	class MainClass
	{
		static void Main(string[] args)
		{
			NSApplication.Init();

			// Install the NOV Mac integration services
			NNovApplicationInstaller.Install<MainWindow>(
				NBarcodeModule.Instance,
				NTextModule.Instance,
				NGridModule.Instance,
				NChartModule.Instance,
				NDiagramModule.Instance,
				NScheduleModule.Instance);

			NSApplication.SharedApplication.Run();
		}
	}
}