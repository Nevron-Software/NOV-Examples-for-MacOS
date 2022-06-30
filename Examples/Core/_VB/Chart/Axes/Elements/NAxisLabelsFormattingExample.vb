Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis labels orientation example
	''' </summary>
	Public Class NAxisLabelsFormattingExample
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
            Nevron.Nov.Examples.Chart.NAxisLabelsFormattingExample.NAxisLabelsFormattingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisLabelsFormattingExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Axis Labels Formatting"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' add interlace stripe
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
			'linearScale.Strips.Add(strip);

			' setup a bar series
			Dim bar As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar.Name = "Bar Series"
            bar.InflateMargins = True
            bar.UseXValues = False
            bar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)

			' add some data to the bar series
			bar.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(18, "C++"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(15, "Ruby"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(21, "Python"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(23, "Java"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(27, "Javascript"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(29, "C#"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(26, "PHP"))
            Me.m_Chart.Series.Add(bar)
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim yAxisDecimalPlaces As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            yAxisDecimalPlaces.Minimum = 0
            AddHandler yAxisDecimalPlaces.ValueChanged, AddressOf Me.OnYAxisDecimalPlacesValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Y Axis Decimal Places:", yAxisDecimalPlaces))
            Dim useCustomXAxisLabels As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Use Custom X Axis Labels")
            AddHandler useCustomXAxisLabels.CheckedChanged, AddressOf Me.OnUseCustomXAxisLabelsCheckedChanged
            stack.Add(useCustomXAxisLabels)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to apply different formatting to axis labels.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnUseCustomXAxisLabelsCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim ordinalScale As Nevron.Nov.Chart.NOrdinalScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale)

            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                ordinalScale.Labels.TextProvider = New Nevron.Nov.Chart.NOrdinalScaleLabelTextProvider(New String() {"C++", "Ruby", "Python", "Java", "Javascript", "C#", "PHP"})
            Else
                ordinalScale.Labels.TextProvider = New Nevron.Nov.Chart.NFormattedScaleLabelTextProvider(New Nevron.Nov.Dom.NNumericValueFormatter(Nevron.Nov.ENNumericValueFormat.Arabic))
            End If
        End Sub

        Private Sub OnYAxisDecimalPlacesValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim decimalPlaces As Integer = CInt(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
            Dim format As String = "."

            For i As Integer = 0 To decimalPlaces - 1
                format += "0"
            Next

            Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            linearScale.Labels.TextProvider = New Nevron.Nov.Chart.NFormattedScaleLabelTextProvider(New Nevron.Nov.Dom.NNumericValueFormatter(format))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
		
		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisLabelsFormattingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
