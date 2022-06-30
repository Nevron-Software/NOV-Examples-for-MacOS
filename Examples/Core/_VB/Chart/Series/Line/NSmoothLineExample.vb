Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard Line Example
	''' </summary>
	Public Class NSmoothLineExample
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
            Nevron.Nov.Examples.Chart.NSmoothLineExample.NSmoothLineExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NSmoothLineExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Smooth Line"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' add interlaced stripe to the Y axis
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Strips.Add(strip)
            Me.m_Line = New Nevron.Nov.Chart.NLineSeries()
            Me.m_Line.Name = "Line Series"
            Me.m_Line.InflateMargins = True
            Me.m_Line.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle("<value>")
            Me.m_Line.MarkerStyle = New Nevron.Nov.Chart.NMarkerStyle(New Nevron.Nov.Graphics.NSize(4, 4))
            Me.m_Line.LineSegmentMode = Nevron.Nov.Chart.ENLineSegmentMode.Spline
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 8 - 1
                Me.m_Line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(random.[Next](80) + 20))
            Next

            Me.m_Chart.Series.Add(Me.m_Line)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create smooth line (spline) charts.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"


		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_Line As Nevron.Nov.Chart.NLineSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NSmoothLineExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
