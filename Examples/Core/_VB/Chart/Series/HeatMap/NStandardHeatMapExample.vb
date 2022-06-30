Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard HeatMap Example
	''' </summary>
	Public Class NStandardHeatMapExample
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
            Nevron.Nov.Examples.Chart.NStandardHeatMapExample.NStandardHeatMapExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStandardHeatMapExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Standard Heat Map"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)
            Me.m_HeatMap = New Nevron.Nov.Chart.NHeatMapSeries()
            chart.Series.Add(Me.m_HeatMap)
            Dim data As Nevron.Nov.Chart.NGridData = Me.m_HeatMap.Data
            Me.m_HeatMap.Palette = New Nevron.Nov.Chart.NColorValuePalette(New Nevron.Nov.Chart.NColorValuePair() {New Nevron.Nov.Chart.NColorValuePair(0.0, Nevron.Nov.Graphics.NColor.Purple), New Nevron.Nov.Chart.NColorValuePair(1.5, Nevron.Nov.Graphics.NColor.MediumSlateBlue), New Nevron.Nov.Chart.NColorValuePair(3.0, Nevron.Nov.Graphics.NColor.CornflowerBlue), New Nevron.Nov.Chart.NColorValuePair(4.5, Nevron.Nov.Graphics.NColor.LimeGreen), New Nevron.Nov.Chart.NColorValuePair(6.0, Nevron.Nov.Graphics.NColor.LightGreen), New Nevron.Nov.Chart.NColorValuePair(7.5, Nevron.Nov.Graphics.NColor.Yellow), New Nevron.Nov.Chart.NColorValuePair(9.0, Nevron.Nov.Graphics.NColor.Orange), New Nevron.Nov.Chart.NColorValuePair(10.5, Nevron.Nov.Graphics.NColor.Red)})
            Dim gridSizeX As Integer = 100
            Dim gridSizeY As Integer = 100
            data.Size = New Nevron.Nov.Graphics.NSizeI(gridSizeX, gridSizeY)
            Dim y, x, z As Double
            Const dIntervalX As Double = 10.0
            Const dIntervalZ As Double = 10.0
            Dim dIncrementX As Double = (dIntervalX / gridSizeX)
            Dim dIncrementZ As Double = (dIntervalZ / gridSizeY)
            z = -(dIntervalZ / 2)
            Dim j As Integer = 0

            While j < gridSizeY
                x = -(dIntervalX / 2)
                Dim i As Integer = 0

                While i < gridSizeX
                    y = 10 - System.Math.Sqrt((x * x) + (z * z) + 2)
                    y += 3.0 * System.Math.Sin(x) * System.Math.Cos(z)
                    data.SetValue(i, j, y)
                    i += 1
                    x += dIncrementX
                End While

                j += 1
                z += dIncrementZ
            End While

            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim smoothPaletteCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Smooth Palette")
            AddHandler smoothPaletteCheckBox.CheckedChanged, AddressOf Me.OnSmoothPaletteCheckBoxCheckedChanged
            smoothPaletteCheckBox.Checked = True
            stack.Add(smoothPaletteCheckBox)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard heat map chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

        Private Sub OnSmoothPaletteCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_HeatMap.Palette.SmoothColors = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

		#EndRegion

		#Region"Fields"

        Private m_HeatMap As Nevron.Nov.Chart.NHeatMapSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStandardHeatMapExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
