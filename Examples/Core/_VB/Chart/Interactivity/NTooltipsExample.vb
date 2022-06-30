Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Tooltip Example
	''' </summary>
	Public Class NTooltipsExample
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
            Nevron.Nov.Examples.Chart.NTooltipsExample.NTooltipsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NTooltipsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NTooltipsExample.CreatePieChartView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Tooltips"

			' configure chart
			Me.m_PieChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NPieChart)
            Me.m_PieSeries = New Nevron.Nov.Chart.NPieSeries()
            Me.m_PieChart.Series.Add(Me.m_PieSeries)
            Me.m_PieChart.DockSpiderLabelsToSides = True
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.ArrowLength = 15
            dataLabelStyle.ArrowPointerLength = 0
            Me.m_PieSeries.DataLabelStyle = dataLabelStyle
            Me.m_PieSeries.LabelMode = Nevron.Nov.Chart.ENPieLabelMode.Spider
            Me.m_PieSeries.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
            Me.m_PieSeries.LegendView.Format = "<label> <percent>"
            Me.m_PieSeries.DataPoints.Add(Me.CreateDataPoint(24, "Cars"))
            Me.m_PieSeries.DataPoints.Add(Me.CreateDataPoint(18, "Airplanes"))
            Me.m_PieSeries.DataPoints.Add(Me.CreateDataPoint(32, "Trains"))
            Me.m_PieSeries.DataPoints.Add(Me.CreateDataPoint(23, "Ships"))
            Me.m_PieSeries.DataPoints.Add(Me.CreateDataPoint(19, "Buses"))



			' detach airplanes
			Me.m_PieSeries.DataPoints(CInt((1))).DetachmentPercent = 10
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, True))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim beginAngleUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            beginAngleUpDown.Value = Me.m_PieChart.BeginAngle
            AddHandler beginAngleUpDown.ValueChanged, AddressOf Me.OnBeginAngleUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin Angle:", beginAngleUpDown))
            Dim sweepAngleUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            sweepAngleUpDown.Value = Me.m_PieChart.SweepAngle
            sweepAngleUpDown.Minimum = -360
            sweepAngleUpDown.Maximum = 360
            AddHandler sweepAngleUpDown.ValueChanged, AddressOf Me.OnSweepAngleUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Sweep Angle:", sweepAngleUpDown))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create tooltips attached to chart objects.</p>"
        End Function

		#EndRegion

        #Region"Implementation"

        Private Function CreateDataPoint(ByVal value As Double, ByVal text As String) As Nevron.Nov.Chart.NPieDataPoint
            Dim pieDataPoint As Nevron.Nov.Chart.NPieDataPoint = New Nevron.Nov.Chart.NPieDataPoint(value, text)
            pieDataPoint.Tooltip = New Nevron.Nov.UI.NTooltip(text & " [" & value.ToString() & "]")
            Return pieDataPoint
        End Function

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnBeginAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_PieChart.BeginAngle = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnSweepAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_PieChart.SweepAngle = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnPieLabelModeSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_PieSeries.LabelMode = CType((CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex), Nevron.Nov.Chart.ENPieLabelMode)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_PieSeries As Nevron.Nov.Chart.NPieSeries
        Private m_PieChart As Nevron.Nov.Chart.NPieChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NTooltipsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreatePieChartView() As Nevron.Nov.Chart.NChartView
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Pie)
            Return chartView
        End Function

		#EndRegion
	End Class
End Namespace
