Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Polar area example
	''' </summary>
	Public Class NPolarLineExample
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
            Nevron.Nov.Examples.Chart.NPolarLineExample.NPolarLineExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NPolarLineExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NPolarLineExample.CreatePolarChartView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Polar Line"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NPolarChart)
            Me.m_Chart.SetPredefinedPolarAxes(Nevron.Nov.Chart.ENPredefinedPolarAxes.AngleValue)

			' setup polar axis
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryValue), Nevron.Nov.Chart.ENPolarAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            linearScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
            linearScale.InflateViewRangeBegin = True
            linearScale.InflateViewRangeEnd = True
            linearScale.MajorGridLines.Visible = True
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.Beige, 125))
            strip.Interlaced = True
            linearScale.Strips.Add(strip)

			' setup polar angle axis
			Dim angularScale As Nevron.Nov.Chart.NAngularScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryAngle), Nevron.Nov.Chart.ENPolarAxis)).Scale, Nevron.Nov.Chart.NAngularScale)
            strip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromRGBA(192, 192, 192, 125))
            strip.Interlaced = True
            angularScale.Strips.Add(strip)

			' add a const line
			Dim line As Nevron.Nov.Chart.NAxisReferenceLine = New Nevron.Nov.Chart.NAxisReferenceLine()
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryValue), Nevron.Nov.Chart.ENPolarAxis)).ReferenceLines.Add(line)
            line.Value = 50
            line.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.SlateBlue)

			' create a polar line series
			Dim series1 As Nevron.Nov.Chart.NPolarLineSeries = New Nevron.Nov.Chart.NPolarLineSeries()
            Me.m_Chart.Series.Add(series1)
            series1.Name = "Series 1"
            series1.CloseContour = True
            series1.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Call Nevron.Nov.Examples.Chart.NPolarLineExample.Curve1(series1, 50)

			' create a polar line series
			Dim series2 As Nevron.Nov.Chart.NPolarLineSeries = New Nevron.Nov.Chart.NPolarLineSeries()
            Me.m_Chart.Series.Add(series2)
            series2.Name = "Series 2"
            series2.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            series2.CloseContour = True
            Call Nevron.Nov.Examples.Chart.NPolarLineExample.Curve2(series2, 100)

			' create a polar line series
			Dim series3 As Nevron.Nov.Chart.NPolarLineSeries = New Nevron.Nov.Chart.NPolarLineSeries()
            Me.m_Chart.Series.Add(series3)
            series3.Name = "Series 3"
            series3.CloseContour = False
            series3.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Call Nevron.Nov.Examples.Chart.NPolarLineExample.Curve3(series3, 100)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a polar line chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"



		#EndRegion

		#Region"Implementation"

		''' <summary>
		''' 
		''' </summary>
		''' <paramname="series"></param>
		''' <paramname="count"></param>
		Friend Shared Sub Curve1(ByVal series As Nevron.Nov.Chart.NPolarLineSeries, ByVal count As Integer)
            series.DataPoints.Clear()
            Dim angleStep As Double = 2 * System.Math.PI / count

            For i As Integer = 0 To count - 1
                Dim angle As Double = i * angleStep
                Dim radius As Double = 1 + System.Math.Cos(angle)
                series.DataPoints.Add(New Nevron.Nov.Chart.NPolarLineDataPoint(angle * 180 / System.Math.PI, radius))
            Next
        End Sub
		''' <summary>
		''' 
		''' </summary>
		''' <paramname="series"></param>
		''' <paramname="count"></param>
		Friend Shared Sub Curve2(ByVal series As Nevron.Nov.Chart.NPolarLineSeries, ByVal count As Integer)
            series.DataPoints.Clear()
            Dim angleStep As Double = 2 * System.Math.PI / count

            For i As Integer = 0 To count - 1
                Dim angle As Double = i * angleStep
                Dim radius As Double = 0.2 + 1.7 * System.Math.Sin(2 * angle) + 1.7 * System.Math.Cos(2 * angle)
                radius = System.Math.Abs(radius)
                series.DataPoints.Add(New Nevron.Nov.Chart.NPolarLineDataPoint(angle * 180 / System.Math.PI, radius))
            Next
        End Sub
		''' <summary>
		''' 
		''' </summary>
		''' <paramname="series"></param>
		''' <paramname="count"></param>
		Friend Shared Sub Curve3(ByVal series As Nevron.Nov.Chart.NPolarLineSeries, ByVal count As Integer)
            series.DataPoints.Clear()
            Dim angleStep As Double = 4 * System.Math.PI / count

            For i As Integer = 0 To count - 1
                Dim angle As Double = i * angleStep
                Dim radius As Double = 0.2 + angle / 5.0
                series.DataPoints.Add(New Nevron.Nov.Chart.NPolarLineDataPoint(angle * 180 / System.Math.PI, radius))
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NPolarChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NPolarLineExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreatePolarChartView() As Nevron.Nov.Chart.NChartView
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Polar)
            Return chartView
        End Function

		#EndRegion
	End Class
End Namespace
