Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Polar range example
	''' </summary>
	Public Class NPolarRangeExample
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
            Nevron.Nov.Examples.Chart.NPolarRangeExample.NPolarRangeExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NPolarRangeExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NPolarRangeExample.CreatePolarChartView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Polar Range"

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
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.Gray, 100))
            strip.Interlaced = True
            linearScale.Strips.Add(strip)
            linearScale.MajorGridLines.Visible = True

			' setup polar angle axis			
			Dim angleAxis As Nevron.Nov.Chart.NPolarAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.PrimaryAngle)
            Dim ordinalScale As Nevron.Nov.Chart.NOrdinalScale = New Nevron.Nov.Chart.NOrdinalScale()
            strip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.DarkGray, 100))
            strip.Interlaced = True
            ordinalScale.Strips.Add(strip)
            ordinalScale.InflateContentRange = False
            ordinalScale.MajorTickMode = Nevron.Nov.Chart.ENMajorTickMode.CustomTicks
            ordinalScale.DisplayDataPointsBetweenTicks = False
            ordinalScale.MajorTickMode = Nevron.Nov.Chart.ENMajorTickMode.CustomStep
            ordinalScale.CustomStep = 1
            Dim labels As String() = New String() {"E", "NE", "N", "NW", "W", "SW", "S", "SE"}
            ordinalScale.Labels.TextProvider = New Nevron.Nov.Chart.NOrdinalScaleLabelTextProvider(labels)
            ordinalScale.Labels.DisplayLast = False
            angleAxis.Scale = ordinalScale
            angleAxis.ViewRangeMode = Nevron.Nov.Chart.ENAxisViewRangeMode.FixedRange
            angleAxis.MinViewRangeValue = 0
            angleAxis.MaxViewRangeValue = 8
            Dim polarRange As Nevron.Nov.Chart.NPolarRangeSeries = New Nevron.Nov.Chart.NPolarRangeSeries()
            polarRange.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_Chart.Series.Add(polarRange)
            Dim rand As System.Random = New System.Random()

            For i As Integer = 0 To 8 - 1
                polarRange.DataPoints.Add(New Nevron.Nov.Chart.NPolarRangeDataPoint(i - 0.4, 0.0, i + 0.4, rand.[Next](80) + 20.0))
            Next

            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a polar range chart.</p>"
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

		Public Shared ReadOnly NPolarRangeExampleSchema As Nevron.Nov.Dom.NSchema

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
