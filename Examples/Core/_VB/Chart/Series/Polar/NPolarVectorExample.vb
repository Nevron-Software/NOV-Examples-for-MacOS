Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Polar vector example
	''' </summary>
	Public Class NPolarVectorExample
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
            Nevron.Nov.Examples.Chart.NPolarVectorExample.NPolarVectorExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NPolarVectorExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NPolarVectorExample.CreatePolarChartView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Polar Vector"

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

			' create a polar line series
			Dim vectorSeries As Nevron.Nov.Chart.NPolarVectorSeries = New Nevron.Nov.Chart.NPolarVectorSeries()
            Me.m_Chart.Series.Add(vectorSeries)
            vectorSeries.Name = "Series 1"
            vectorSeries.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)

            For i As Integer = 0 To 360 - 1 Step 30

                For j As Integer = 10 To 100 Step 10
                    vectorSeries.DataPoints.Add(New Nevron.Nov.Chart.NPolarVectorDataPoint(i, j, i + j / 10, j, Nothing, New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Examples.Chart.NPolarVectorExample.ColorFromValue(j))))
                Next
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
            Return "<p>This example demonstrates how to create a polar line chart.</p>"
        End Function

		#EndRegion

		#Region"Implementation"


		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NPolarChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NPolarVectorExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreatePolarChartView() As Nevron.Nov.Chart.NChartView
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Polar)
            Return chartView
        End Function

        Private Shared Function ColorFromValue(ByVal value As Double) As Nevron.Nov.Graphics.NColor
            Return Nevron.Nov.Graphics.NColor.InterpolateColors(Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.Blue, value / 100.0)
        End Function

		#EndRegion
	End Class
End Namespace
