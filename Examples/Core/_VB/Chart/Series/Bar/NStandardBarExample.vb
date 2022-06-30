Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard Bar Example
	''' </summary>
	Public Class NStandardBarExample
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
            Nevron.Nov.Examples.Chart.NStandardBarExample.NStandardBarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStandardBarExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Standard Bar"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

            ' add interlace stripe
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            'linearScale.Strips.Add(strip);

			' setup a bar series
			Me.m_Bar = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Bar.Name = "Bar Series"
            Me.m_Bar.InflateMargins = True
            Me.m_Bar.UseXValues = False
            Me.m_Bar.Shadow = New Nevron.Nov.Graphics.NShadow(Nevron.Nov.Graphics.NColor.LightGray, 2, 2)

			' add some data to the bar series
			Me.m_Bar.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(18, "C++"))
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(15, "Ruby"))
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(21, "Python"))
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(23, "Java"))
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(27, "Javascript"))
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(29, "C#"))
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(26, "PHP"))
            chart.Series.Add(Me.m_Bar)
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim originModeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            originModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENSeriesOriginMode)()
            AddHandler originModeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnOriginModeComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Origin Mode: ", originModeComboBox))
            Dim customOriginUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler customOriginUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnCustomOriginUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Custom Origin: ", customOriginUpDown))
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard bar chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCustomOriginUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Bar.CustomOrigin = CDbl(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnOriginModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Bar.OriginMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENSeriesOriginMode)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Bar As Nevron.Nov.Chart.NBarSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStandardBarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
