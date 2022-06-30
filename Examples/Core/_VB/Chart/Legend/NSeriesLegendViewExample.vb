Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Series Legend Modes Example
	''' </summary>
	Public Class NSeriesLegendViewExample
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
            Nevron.Nov.Examples.Chart.NSeriesLegendViewExample.NSeriesLegendViewExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NSeriesLegendViewExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

            ' configure title
            chartView.Surface.Titles(CInt((0))).Text = "Series Legend View"

            ' configure chart
            Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

            ' add interlace stripe
            Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            linearScale.Strips.Add(strip)

            ' add a bar series
            Dim bar1 As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar1.Name = "Bar1"
            bar1.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Series
            bar1.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            bar1.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            Me.m_Chart.Series.Add(bar1)

            ' add another bar series
            Dim bar2 As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar2.Name = "Bar2"
            bar2.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Clustered
            bar2.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            bar2.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            Me.m_Chart.Series.Add(bar2)

            ' add another bar series
            Dim bar3 As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar3.Name = "Bar2"
            bar3.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Clustered
            bar3.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            bar3.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            Me.m_Chart.Series.Add(bar3)
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 5 - 1
                bar1.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 100)))
                bar2.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 100)))
                bar3.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 100)))
            Next

            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim seriesLegendModeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            seriesLegendModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENSeriesLegendMode)()
            AddHandler seriesLegendModeComboBox.SelectedIndexChanged, AddressOf Me.OnSeriesLegendModeComboBoxSelectedIndexChanged
            seriesLegendModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENSeriesLegendMode.Series)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Legend Mode: ", seriesLegendModeComboBox))
            Dim seriesLegendOrderComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            seriesLegendOrderComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENSeriesLegendOrder)()
            AddHandler seriesLegendOrderComboBox.SelectedIndexChanged, AddressOf Me.OnSeriesLegendOrderComboBoxSelectedIndexChanged
            seriesLegendOrderComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENSeriesLegendOrder.Append)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Legend Order: ", seriesLegendOrderComboBox))
            Dim markSizeUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler markSizeUpDown.ValueChanged, AddressOf Me.OnMarkSizeUpDownValueChanged
            markSizeUpDown.Value = 10
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Mark Size: ", markSizeUpDown))
            Dim fontSizeUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler fontSizeUpDown.ValueChanged, AddressOf Me.OnFontSizeUpDownValueChanged
            fontSizeUpDown.Value = 10
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Font Size: ", fontSizeUpDown))
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates the effect of different series legend view settings.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

        Private Sub OnSeriesLegendModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim seriesLegendMode As Nevron.Nov.Chart.ENSeriesLegendMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENSeriesLegendMode)

            For i As Integer = 0 To Me.m_Chart.Series.Count - 1
                Me.m_Chart.Series(CInt((i))).LegendView.Mode = seriesLegendMode
            Next
        End Sub

        Private Sub OnSeriesLegendOrderComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim seriesLegendOrder As Nevron.Nov.Chart.ENSeriesLegendOrder = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENSeriesLegendOrder)

            For i As Integer = 0 To Me.m_Chart.Series.Count - 1
                Me.m_Chart.Series(CInt((i))).LegendView.Order = seriesLegendOrder
            Next
        End Sub

        Private Sub OnFontSizeUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim fontSize As Double = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value

            For i As Integer = 0 To Me.m_Chart.Series.Count - 1
                Me.m_Chart.Series(CInt((i))).LegendView.TextStyle.Font.Size = fontSize
            Next
        End Sub

        Private Sub OnMarkSizeUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim markSize As Double = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value

            For i As Integer = 0 To Me.m_Chart.Series.Count - 1
                Me.m_Chart.Series(CInt((i))).LegendView.MarkSize = New Nevron.Nov.Graphics.NSize(markSize, markSize)
            Next
        End Sub

		#EndRegion

        #Region"Fields"

        Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NSeriesLegendViewExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
