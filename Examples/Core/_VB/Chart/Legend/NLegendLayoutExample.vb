Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Legend Layout Example
	''' </summary>
	Public Class NLegendLayoutExample
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
            Nevron.Nov.Examples.Chart.NLegendLayoutExample.NLegendLayoutExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NLegendLayoutExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ChartView = New Nevron.Nov.Chart.NChartView()
            Me.m_ChartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

            ' configure title
            Me.m_ChartView.Surface.Titles(CInt((0))).Text = "Legend Layout"

            ' configure chart
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

            ' add interlace stripe
            Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            linearScale.Strips.Add(strip)

            ' add a bar series
            Dim bar1 As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar1.Name = "Bar1"
            bar1.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Series
            bar1.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
            bar1.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            bar1.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            chart.Series.Add(bar1)

            ' add another bar series
            Dim bar2 As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar2.Name = "Bar2"
            bar2.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Clustered
            bar2.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
            bar2.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            bar2.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            chart.Series.Add(bar2)

            ' add another bar series
            Dim bar3 As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar3.Name = "Bar2"
            bar3.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Clustered
            bar3.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
            bar3.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            bar3.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            chart.Series.Add(bar3)
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 5 - 1
                bar1.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 100)))
                bar2.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 100)))
                bar3.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 100)))
            Next

            Me.m_ChartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return Me.m_ChartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim legendExpandModeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            legendExpandModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENLegendExpandMode)()
            AddHandler legendExpandModeComboBox.SelectedIndexChanged, AddressOf Me.OnLegendExpandModeComboBoxSelectedIndexChanged
            legendExpandModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENLegendExpandMode.RowsOnly)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Expand Mode: ", legendExpandModeComboBox))
            Me.m_RowCountUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_RowCountUpDown.Enabled = False
            Me.m_RowCountUpDown.Minimum = 1
            Me.m_RowCountUpDown.Value = 1
            AddHandler Me.m_RowCountUpDown.ValueChanged, AddressOf Me.OnRowCountUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Row Count: ", Me.m_RowCountUpDown))
            Me.m_ColCountUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_ColCountUpDown.Enabled = False
            Me.m_ColCountUpDown.Minimum = 1
            Me.m_ColCountUpDown.Value = 1
            AddHandler Me.m_ColCountUpDown.ValueChanged, AddressOf Me.OnColCountUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Col Count: ", Me.m_ColCountUpDown))
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates different settings related to legend layout.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

        Private Sub OnRowCountUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ChartView.Surface.Legends(CInt((0))).RowCount = CInt(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnColCountUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ChartView.Surface.Legends(CInt((0))).ColCount = CInt(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnLegendExpandModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ChartView.Surface.Legends(CInt((0))).ExpandMode = CType((CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex), Nevron.Nov.Chart.ENLegendExpandMode)

            Select Case Me.m_ChartView.Surface.Legends(CInt((0))).ExpandMode
                Case Nevron.Nov.Chart.ENLegendExpandMode.ColsFixed
                    Me.m_ColCountUpDown.Enabled = True
                    Me.m_RowCountUpDown.Enabled = False
                Case Nevron.Nov.Chart.ENLegendExpandMode.RowsFixed
                    Me.m_ColCountUpDown.Enabled = False
                    Me.m_RowCountUpDown.Enabled = True
                Case Else
                    Me.m_ColCountUpDown.Enabled = False
                    Me.m_RowCountUpDown.Enabled = False
            End Select
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_ChartView As Nevron.Nov.Chart.NChartView
        Private m_ColCountUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_RowCountUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NLegendLayoutExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
