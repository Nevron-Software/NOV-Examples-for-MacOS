Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard Vector Example
	''' </summary>
	Public Class NStandardVectorExample
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
            Nevron.Nov.Examples.Chart.NStandardVectorExample.NStandardVectorExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStandardVectorExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Standard Vector"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' setup X axis
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale = linearScale

			' setup Y axis
			linearScale = New Nevron.Nov.Chart.NLinearScale()
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale = linearScale

			' setup shape series
			Dim vectorSeries As Nevron.Nov.Chart.NVectorSeries = New Nevron.Nov.Chart.NVectorSeries()
            Me.m_Chart.Series.Add(vectorSeries)
            vectorSeries.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            vectorSeries.InflateMargins = False
            vectorSeries.UseXValues = True
            vectorSeries.MinArrowheadSize = New Nevron.Nov.Graphics.NSize(2, 3)
            vectorSeries.MaxArrowheadSize = New Nevron.Nov.Graphics.NSize(4, 6)

			' fill data
			Me.FillData(vectorSeries)
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard 2D vector chart.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub FillData(ByVal vectorSeries As Nevron.Nov.Chart.NVectorSeries)
            Dim x As Double = 0, y As Double = 0
            Dim k As Integer = 0

            For i As Integer = 0 To 10 - 1
                x = 1
                y += 1

                For j As Integer = 0 To 10 - 1
                    x += 1
                    Dim dx As Double = System.Math.Sin(x / 3.0) * System.Math.Cos((x - y) / 4.0)
                    Dim dy As Double = System.Math.Cos(y / 8.0) * System.Math.Cos(y / 4.0)
                    Dim color As Nevron.Nov.Graphics.NColor = Me.ColorFromVector(dx, dy)
                    vectorSeries.DataPoints.Add(New Nevron.Nov.Chart.NVectorDataPoint(x, y, x + dx, y + dy, New Nevron.Nov.Graphics.NColorFill(color), New Nevron.Nov.Graphics.NStroke(1, color)))
                    k += 1
                Next
            Next
        End Sub

        Private Function ColorFromVector(ByVal dx As Double, ByVal dy As Double) As Nevron.Nov.Graphics.NColor
            Dim length As Double = System.Math.Sqrt(dx * dx + dy * dy)
            Dim sq2 As Double = System.Math.Sqrt(2)
            Dim r As Integer = CInt(((255 / sq2) * length))
            Dim g As Integer = 20
            Dim b As Integer = CInt(((255 / sq2) * (sq2 - length)))
            Return Nevron.Nov.Graphics.NColor.FromRGB(CByte(r), CByte(g), CByte(b))
        End Function

		#EndRegion

		#Region"Event Handlers"


		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStandardVectorExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
