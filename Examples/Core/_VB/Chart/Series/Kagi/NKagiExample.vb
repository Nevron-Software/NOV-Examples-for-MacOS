Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Kagi Example
	''' </summary>
	Public Class NKagiExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor
		''' </summary>s
		Public Sub New()
        End Sub
		''' <summary>
		''' Static constructor
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Chart.NKagiExample.NKagiExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NKagiExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Kagi"

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
			Me.m_KagiSeries = New Nevron.Nov.Chart.NKagiSeries()
            Me.m_KagiSeries.UseXValues = True
            chart.Series.Add(Me.m_KagiSeries)
            Me.GenerateData(Me.m_KagiSeries)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim reversalAmountUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            reversalAmountUpDown.Minimum = 1
            reversalAmountUpDown.Maximum = 100
            reversalAmountUpDown.Value = Me.m_KagiSeries.ReversalAmount
            AddHandler reversalAmountUpDown.ValueChanged, AddressOf Me.OnReversalAmountUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Reversal Amount:", reversalAmountUpDown))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates the functionality of the kagi series.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub GenerateData(ByVal KagiSeries As Nevron.Nov.Chart.NKagiSeries)
            Dim dataGenerator As NStockDataGenerator = New NStockDataGenerator(New Nevron.Nov.Graphics.NRange(50, 350), 0.002, 2)
            dataGenerator.Reset()
            Dim dt As System.DateTime = System.DateTime.Now

            For i As Integer = 0 To 100 - 1
                KagiSeries.DataPoints.Add(New Nevron.Nov.Chart.NKagiDataPoint(Nevron.Nov.NDateTimeHelpers.ToOADate(dt), dataGenerator.GetNextValue()))
                dt = dt.AddDays(1)
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnReversalAmountUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_KagiSeries.ReversalAmount = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_KagiSeries As Nevron.Nov.Chart.NKagiSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NKagiExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
