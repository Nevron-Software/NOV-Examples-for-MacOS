Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Interactive Legend Example
	''' </summary>
	Public Class NInteractiveLegendExample
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
            Nevron.Nov.Examples.Chart.NInteractiveLegendExample.NInteractiveLegendExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NInteractiveLegendExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Interactive Legend"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' add interlace stripe
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            linearScale.Strips.Add(stripStyle)

			' add the first bar
            Dim bar1 As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar1.Name = "Bar1"
            bar1.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Series
            bar1.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.SeriesVisibility
            chart.Series.Add(bar1)

			' add the second bar
            Dim bar2 As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar2.Name = "Bar2"
            bar2.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Stacked
            bar2.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.SeriesVisibility
            chart.Series.Add(bar2)

			' add the third bar
            Dim bar3 As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar3.Name = "Bar3"
            bar3.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Stacked
            bar3.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.SeriesVisibility
            chart.Series.Add(bar3)

			' setup value formatting
			bar1.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            bar2.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            bar3.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")

			' position data labels in the center of the bars
			bar1.DataLabelStyle = Me.CreateDataLabelStyle()
            bar2.DataLabelStyle = Me.CreateDataLabelStyle()
            bar3.DataLabelStyle = Me.CreateDataLabelStyle()
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))

			' pass some random data
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 12 - 1
                bar1.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](90) + 10))
                bar2.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](90) + 10))
                bar3.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](90) + 10))
            Next

            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create an interactive legend.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		''' <summary>
		''' Creates a new data label style object
		''' </summary>
		''' <returns></returns>
		Private Function CreateDataLabelStyle() As Nevron.Nov.Chart.NDataLabelStyle
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.VertAlign = Nevron.Nov.ENVerticalAlignment.Center
            dataLabelStyle.ArrowLength = 0
            Return dataLabelStyle
        End Function

		#EndRegion

		#Region"Event Handlers"


		#EndRegion

		#Region"Fields"


		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NInteractiveLegendExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
