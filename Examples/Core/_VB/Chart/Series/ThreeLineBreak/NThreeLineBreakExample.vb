Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI
Imports System

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Three Line Break Example
	''' </summary>
	Public Class NThreeLineBreakExample
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
            Nevron.Nov.Examples.Chart.NThreeLineBreakExample.NThreeLineBreakExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NThreeLineBreakExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Three Line Break"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)
            Dim scaleY As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)

			' add interlace stripe
			Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            scaleY.Strips.Add(stripStyle)

			' setup X axis
			Dim priceScale As Nevron.Nov.Chart.NPriceTimeScale = New Nevron.Nov.Chart.NPriceTimeScale()
            priceScale.InnerMajorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(0.0, Nevron.Nov.Graphics.NColor.Black)
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale = priceScale

			' setup line break series
			Me.m_ThreeLineBreak = New Nevron.Nov.Chart.NThreeLineBreakSeries()
            Me.m_ThreeLineBreak.UseXValues = True
            chart.Series.Add(Me.m_ThreeLineBreak)
            Me.GenerateData(Me.m_ThreeLineBreak)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim boxWidthPercentUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            boxWidthPercentUpDown.Minimum = 0
            boxWidthPercentUpDown.Maximum = 100
            boxWidthPercentUpDown.Value = Me.m_ThreeLineBreak.BoxWidthPercent
            AddHandler boxWidthPercentUpDown.ValueChanged, AddressOf Me.OnBoxWidthPercentUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Box Width Percent:", boxWidthPercentUpDown))
            Dim numberOfLinesToBreakUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            numberOfLinesToBreakUpDown.Minimum = 1
            numberOfLinesToBreakUpDown.Maximum = 100
            numberOfLinesToBreakUpDown.Value = Me.m_ThreeLineBreak.NumberOfLinesToBreak
            AddHandler numberOfLinesToBreakUpDown.ValueChanged, AddressOf Me.OnNumberOfLinesToBreakUpDownValueChanged
            numberOfLinesToBreakUpDown.DecimalPlaces = 0
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Number of Lines to Break:", numberOfLinesToBreakUpDown))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates the functionality of the three line break series.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub GenerateData(ByVal threeLineBreak As Nevron.Nov.Chart.NThreeLineBreakSeries)
            Dim dataGenerator As NStockDataGenerator = New NStockDataGenerator(New Nevron.Nov.Graphics.NRange(50, 350), 0.002, 2)
            dataGenerator.Reset()
            Dim dt As System.DateTime = System.DateTime.Now

            For i As Integer = 0 To 100 - 1
                threeLineBreak.DataPoints.Add(New Nevron.Nov.Chart.NThreeLineBreakDataPoint(Nevron.Nov.NDateTimeHelpers.ToOADate(dt), dataGenerator.GetNextValue()))
                dt = dt.AddDays(1)
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnBoxWidthPercentUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ThreeLineBreak.BoxWidthPercent = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnNumberOfLinesToBreakUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ThreeLineBreak.NumberOfLinesToBreak = CInt(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ThreeLineBreak As Nevron.Nov.Chart.NThreeLineBreakSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NThreeLineBreakExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
