Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Point and Figure Example
	''' </summary>
	Public Class NPointAndFigureExample
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
            Nevron.Nov.Examples.Chart.NPointAndFigureExample.NPointAndFigureExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NPointAndFigureExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Point And Figure"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' setup X axis
			Dim priceScale As Nevron.Nov.Chart.NPriceTimeScale = New Nevron.Nov.Chart.NPriceTimeScale()
            priceScale.InnerMajorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(0.0, Nevron.Nov.Graphics.NColor.Black)
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale = priceScale
            Const nInitialBoxSize As Integer = 5

			' setup Y axis
			Dim scaleY As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            scaleY.MajorTickMode = Nevron.Nov.Chart.ENMajorTickMode.CustomStep
            scaleY.CustomStep = nInitialBoxSize
            scaleY.OuterMajorTicks.Width = 0
            scaleY.InnerMajorTicks.Width = 0
            scaleY.AutoMinorTicks = True
            scaleY.MinorTickCount = 1
            scaleY.InflateViewRangeBegin = False
            scaleY.InflateViewRangeEnd = False
            scaleY.MajorGridLines.Stroke = New Nevron.Nov.Graphics.NStroke(0, Nevron.Nov.Graphics.NColor.Black)
            scaleY.MinorGridLines.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Black)
            Dim highValues As Single() = New Single(19) {21.3F, 42.4F, 11.2F, 65.7F, 38.0F, 71.3F, 49.54F, 83.7F, 13.9F, 56.12F, 27.43F, 23.1F, 31.0F, 75.4F, 9.3F, 39.12F, 10.0F, 44.23F, 21.76F, 49.2F}
            Dim lowValues As Single() = New Single(19) {12.1F, 14.32F, 8.43F, 36.0F, 13.5F, 47.34F, 24.54F, 68.11F, 6.87F, 23.3F, 12.12F, 14.54F, 25.0F, 37.2F, 3.9F, 23.11F, 1.9F, 14.0F, 8.23F, 34.21F}

			' setup Point & Figure series
			Me.m_PointAndFigure = New Nevron.Nov.Chart.NPointAndFigureSeries()
            Me.m_PointAndFigure.UseXValues = True
            chart.Series.Add(Me.m_PointAndFigure)
            Dim dt As System.DateTime = System.DateTime.Now

			' fill data
			Dim count As Integer = highValues.Length

            For i As Integer = 0 To count - 1
                Me.m_PointAndFigure.DataPoints.Add(New Nevron.Nov.Chart.NPointAndFigureDataPoint(Nevron.Nov.NDateTimeHelpers.ToOADate(dt), highValues(i), lowValues(i)))
                dt = dt.AddDays(1)
            Next

            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim boxSizeUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            boxSizeUpDown.Minimum = 1
            boxSizeUpDown.Maximum = 100
            boxSizeUpDown.Value = Me.m_PointAndFigure.BoxSize
            AddHandler boxSizeUpDown.ValueChanged, AddressOf Me.OnBoxSizeUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Box Size:", boxSizeUpDown))
            Dim reversalAmountUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            reversalAmountUpDown.Minimum = 1
            reversalAmountUpDown.Maximum = 100
            reversalAmountUpDown.Value = Me.m_PointAndFigure.ReversalAmount
            AddHandler reversalAmountUpDown.ValueChanged, AddressOf Me.OnReversalAmountUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Reversal Amount:", reversalAmountUpDown))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates the functionality of the point and figure series.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnReversalAmountUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_PointAndFigure.ReversalAmount = CInt(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnBoxSizeUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_PointAndFigure.BoxSize = CDbl(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_PointAndFigure As Nevron.Nov.Chart.NPointAndFigureSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NPointAndFigureExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
