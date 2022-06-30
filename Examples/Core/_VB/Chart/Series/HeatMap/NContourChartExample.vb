Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Controur Chart Example
	''' </summary>
	Public Class NContourChartExample
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
            Nevron.Nov.Examples.Chart.NContourChartExample.NContourChartExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NContourChartExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Contour Chart"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)
            Me.m_HeatMap = New Nevron.Nov.Chart.NHeatMapSeries()
            chart.Series.Add(Me.m_HeatMap)
            Me.m_HeatMap.Palette = New Nevron.Nov.Chart.NColorValuePalette(New Nevron.Nov.Chart.NColorValuePair() {New Nevron.Nov.Chart.NColorValuePair(0.0, Nevron.Nov.Graphics.NColor.Purple), New Nevron.Nov.Chart.NColorValuePair(1.5, Nevron.Nov.Graphics.NColor.MediumSlateBlue), New Nevron.Nov.Chart.NColorValuePair(3.0, Nevron.Nov.Graphics.NColor.CornflowerBlue), New Nevron.Nov.Chart.NColorValuePair(4.5, Nevron.Nov.Graphics.NColor.LimeGreen), New Nevron.Nov.Chart.NColorValuePair(6.0, Nevron.Nov.Graphics.NColor.LightGreen), New Nevron.Nov.Chart.NColorValuePair(7.5, Nevron.Nov.Graphics.NColor.Yellow), New Nevron.Nov.Chart.NColorValuePair(9.0, Nevron.Nov.Graphics.NColor.Orange), New Nevron.Nov.Chart.NColorValuePair(10.5, Nevron.Nov.Graphics.NColor.Red)})

' 			m_HeatMap.Palette = new NColorValuePalette(new NColorValuePair[] { new NColorValuePair(-5.0, NColor.Purple),
' new NColorValuePair(1, NColor.MediumSlateBlue),
' new NColorValuePair(10.0, NColor.CornflowerBlue) });

			Me.m_HeatMap.ContourDisplayMode = Nevron.Nov.Chart.ENContourDisplayMode.Contour
            Me.m_HeatMap.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.SeriesLogic
            Me.m_HeatMap.LegendView.Format = "<level_value>"
            Me.GenerateData()
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim originXUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler originXUpDown.ValueChanged, AddressOf Me.OnOriginXUpDownValueChanged
            originXUpDown.Value = 0
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Origin X:", originXUpDown))
            Dim originYUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler originYUpDown.ValueChanged, AddressOf Me.OnOriginYUpDownValueChanged
            originYUpDown.Value = 0
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Origin Y:", originYUpDown))
            Dim GridStepXUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler GridStepXUpDown.ValueChanged, AddressOf Me.OnGridStepXUpDownValueChanged
            GridStepXUpDown.Value = 1.0
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Grid Step X:", GridStepXUpDown))
            Dim GridStepYUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler GridStepYUpDown.ValueChanged, AddressOf Me.OnGridStepYUpDownValueChanged
            GridStepYUpDown.Value = 1.0
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Grid Step Y:", GridStepYUpDown))
            Dim contourDisplayModeCombo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            contourDisplayModeCombo.FillFromEnum(Of Nevron.Nov.Chart.ENContourDisplayMode)()
            AddHandler contourDisplayModeCombo.SelectedIndexChanged, AddressOf Me.OnContourDisplayModeComboSelectedIndexChanged
            contourDisplayModeCombo.SelectedIndex = CInt(Nevron.Nov.Chart.ENContourDisplayMode.Contour)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Contour Display Mode:", contourDisplayModeCombo))
            Dim contourColorModeCombo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            contourColorModeCombo.FillFromEnum(Of Nevron.Nov.Chart.ENContourColorMode)()
            AddHandler contourColorModeCombo.SelectedIndexChanged, AddressOf Me.OnContourColorModeComboSelectedIndexChanged
            contourColorModeCombo.SelectedIndex = CInt(Nevron.Nov.Chart.ENContourColorMode.Uniform)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Contour Color Mode:", contourColorModeCombo))
            Dim showFillCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Fill")
            AddHandler showFillCheckBox.CheckedChanged, AddressOf Me.OnShowFillCheckBoxCheckedChanged
            showFillCheckBox.Checked = True
            stack.Add(showFillCheckBox)
            Dim smoothPaletteCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Smooth Palette")
            AddHandler smoothPaletteCheckBox.CheckedChanged, AddressOf Me.OnSmoothPaletteCheckBoxCheckedChanged
            smoothPaletteCheckBox.Checked = True
            stack.Add(smoothPaletteCheckBox)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a contour chart.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

' 		private void GenerateData()
' 		{
' 			NGridData data = m_HeatMap.Data;
' 
' int GridStepX = 10;
' int GridStepY = 10;
' 			data.Size = new NSizeI(GridStepX, GridStepY);
' 
' 			for (int row = 0; row < GridStepX; row++)
' 			{
' 				for (int col = 0; col < GridStepY; col++)
' 				{
' 					int dx = 2 - row;
' 					int dy = 2 - col;
' 					double distance = Math.Sqrt(dx * dx + dy * dy);
' 
' 					data.SetValue(row, col, 5 - distance);
' 				}
' 			}
' 		}

		Private Sub GenerateData()
            Dim data As Nevron.Nov.Chart.NGridData = Me.m_HeatMap.Data
            Dim GridStepX As Integer = 300
            Dim GridStepY As Integer = 300
            data.Size = New Nevron.Nov.Graphics.NSizeI(GridStepX, GridStepY)
            Const dIntervalX As Double = 10.0
            Const dIntervalZ As Double = 10.0
            Dim dIncrementX As Double = (dIntervalX / GridStepX)
            Dim dIncrementZ As Double = (dIntervalZ / GridStepY)
            Dim y, x, z As Double
            z = -(dIntervalZ / 2)
            Dim col As Integer = 0

            While col < GridStepX
                x = -(dIntervalX / 2)
                Dim row As Integer = 0

                While row < GridStepY
                    y = 10 - System.Math.Sqrt((x * x) + (z * z) + 2)
                    y += 3.0 * System.Math.Sin(x) * System.Math.Cos(z)
                    data.SetValue(row, col, y)
                    row += 1
                    x += dIncrementX
                End While

                col += 1
                z += dIncrementZ
            End While
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnOriginYUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_HeatMap.Data.OriginY = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnOriginXUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_HeatMap.Data.OriginX = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnGridStepYUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_HeatMap.Data.StepY = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnGridStepXUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_HeatMap.Data.StepX = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnShowFillCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_HeatMap.ShowFill = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnContourColorModeComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_HeatMap.ContourColorMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENContourColorMode)
        End Sub

        Private Sub OnContourDisplayModeComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_HeatMap.ContourDisplayMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENContourDisplayMode)
        End Sub

        Private Sub OnSmoothPaletteCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_HeatMap.Palette.SmoothColors = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_HeatMap As Nevron.Nov.Chart.NHeatMapSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NContourChartExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
