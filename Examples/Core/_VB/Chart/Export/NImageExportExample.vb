Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Chart.Export
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Image Export Example
	''' </summary>
	Public Class NImageExportExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' Static constructor
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Chart.NImageExportExample.NImageExportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NImageExportExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ChartView = New Nevron.Nov.Chart.NChartView()
            Me.m_ChartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			Me.m_ChartView.Surface.Titles(CInt((0))).Text = "Image Export Example"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' setup X axis
			Dim xScale As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            xScale.MajorGridLines.Visible = True

			' setup Y axis
			Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            yScale.MajorGridLines.Visible = True
			
			' add interlaced stripe
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            yScale.Strips.Add(strip)

			' setup shape series
			Dim range As Nevron.Nov.Chart.NRangeSeries = New Nevron.Nov.Chart.NRangeSeries()
            chart.Series.Add(range)
            range.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            range.UseXValues = True
            range.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkOrange)
            range.Stroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkRed)

			' fill data
			Dim intervals As Double() = New Double() {5, 5, 5, 5, 5, 5, 5, 5, 5, 15, 30, 60}
            Dim values As Double() = New Double() {4180, 13687, 18618, 19634, 17981, 7190, 16369, 3212, 4122, 9200, 6461, 3435}
            Dim count As Integer = System.Math.Min(intervals.Length, values.Length)
            Dim x As Double = 0

            For i As Integer = 0 To count - 1
                Dim interval As Double = intervals(i)
                Dim value As Double = values(i)
                Dim x1 As Double = x
                Dim y1 As Double = 0
                x += interval
                Dim x2 As Double = x
                Dim y2 As Double = value / interval
                range.DataPoints.Add(New Nevron.Nov.Chart.NRangeDataPoint(x1, y1, x2, y2))
            Next

            Me.m_ChartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return Me.m_ChartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim copyImageToClipboardButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Copy Image to Clipboard")
            AddHandler copyImageToClipboardButton.Click, AddressOf Me.OnCopyImageToClipboardButtonClick
            stack.Add(copyImageToClipboardButton)
            Dim saveAsImageFileButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Save as Image File...")
            AddHandler saveAsImageFileButton.Click, AddressOf Me.OnSaveAsImageFileButtonClick
            stack.Add(saveAsImageFileButton)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to export images to the clipboard or file.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSaveAsImageFileButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim imageExporter As Nevron.Nov.Chart.Export.NChartImageExporter = New Nevron.Nov.Chart.Export.NChartImageExporter(Me.m_ChartView.Document)
            imageExporter.SaveAsImage(New Nevron.Nov.Graphics.NSize(400, 300), 96)

			' Note: You can also use SaveToFile like:
			' imageExporter.SaveToFile("c:\\SomeImage.png", NImageFormat.Png, new NSize(400, 300), 96);

			' or SaveToStream:
			' MemoryStream memoryStream = new MemoryStream();
			' imageExporter.SaveToStream(memoryStream, NImageFormat.Png, new NSize(400, 300), 96);
			' byte[] imageBytes = memoryStream.ToArray();
		End Sub

        Private Sub OnCopyImageToClipboardButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim imageExporter As Nevron.Nov.Chart.Export.NChartImageExporter = New Nevron.Nov.Chart.Export.NChartImageExporter(Me.m_ChartView.Document)
            imageExporter.CopyToClipboard(New Nevron.Nov.Graphics.NSize(500, 400), 96)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ChartView As Nevron.Nov.Chart.NChartView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NImageExportExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
