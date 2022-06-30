Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Wafer Chart Example
	''' </summary>
	Public Class NWaferChartExample
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
            Nevron.Nov.Examples.Chart.NWaferChartExample.NWaferChartExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NWaferChartExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Standard Heat Maps"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)
            Dim heatMap As Nevron.Nov.Chart.NHeatMapSeries = New Nevron.Nov.Chart.NHeatMapSeries()
            chart.Series.Add(heatMap)
            Dim data As Nevron.Nov.Chart.NGridData = heatMap.Data
            heatMap.Palette = New Nevron.Nov.Chart.NTwoColorPalette(Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.Graphics.NColor.Red)
            Dim gridSizeX As Integer = 100
            Dim gridSizeY As Integer = 100
            data.Size = New Nevron.Nov.Graphics.NSizeI(gridSizeX, gridSizeY)
            Dim centerX As Integer = gridSizeX / 2
            Dim centerY As Integer = gridSizeY / 2
            Dim radius As Integer = gridSizeX / 2
            Dim rand As System.Random = New System.Random()

            For y As Integer = 0 To gridSizeY - 1

                For x As Integer = 0 To gridSizeX - 1
                    Dim dx As Integer = x - centerX
                    Dim dy As Integer = y - centerY
                    Dim pointDistance As Double = System.Math.Sqrt(dx * dx + dy * dy)

                    If pointDistance < radius Then
						' assign value
						data.SetValue(x, y, pointDistance + rand.[Next](20))
                    Else
                        data.SetValue(x, y, Double.NaN)
                    End If
                Next
            Next

            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard bar chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		#EndRegion

		#Region"Fields"


		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NWaferChartExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
