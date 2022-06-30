Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Doughnut Pie Example
	''' </summary>
	Public Class NDoughnutPieExample
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
            Nevron.Nov.Examples.Chart.NDoughnutPieExample.NDoughnutPieExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NDoughnutPieExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NDoughnutPieExample.CreatePieChartView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Doughnut Pie"

			' configure chart
			Me.m_PieChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NPieChart)
            Me.m_PieChart.BeginRadiusPercent = 20
            Me.m_PieChart.LabelLayout.EnableInitialPositioning = False
            Dim labels As String() = New String() {"Ships", "Trains", "Automobiles", "Airplanes"}
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 4 - 1
                Dim pieSeries As Nevron.Nov.Chart.NPieSeries = New Nevron.Nov.Chart.NPieSeries()
				
				' create a small detachment between pie rings
				pieSeries.BeginRadiusPercent = 10
                Me.m_PieChart.Series.Add(pieSeries)
                Me.m_PieChart.DockSpiderLabelsToSides = True
                Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
                dataLabelStyle.ArrowLength = 0
                dataLabelStyle.ArrowPointerLength = 0
                dataLabelStyle.Format = "<percent>"
                dataLabelStyle.TextStyle.HorzAlign = Nevron.Nov.Graphics.ENTextHorzAlign.Center
                dataLabelStyle.TextStyle.VertAlign = Nevron.Nov.Graphics.ENTextVertAlign.Center
                pieSeries.DataLabelStyle = dataLabelStyle

                If i = 0 Then
                    pieSeries.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
                    pieSeries.LegendView.Format = "<label>"
                Else
                    pieSeries.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.None
                End If

                pieSeries.LabelMode = Nevron.Nov.Chart.ENPieLabelMode.Center

                For j As Integer = 0 To labels.Length - 1
                    pieSeries.DataPoints.Add(New Nevron.Nov.Chart.NPieDataPoint(20 + random.[Next](100), labels(j)))
                Next
            Next

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
            Dim enableLabelAdjustmentCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enable Label Adjustment")
            AddHandler enableLabelAdjustmentCheckBox.CheckedChanged, AddressOf Me.OnEnableLabelAdjustmentCheckBoxCheckedChanged
            enableLabelAdjustmentCheckBox.Checked = True
            stack.Add(enableLabelAdjustmentCheckBox)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a doughnut pie chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnBeginAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_PieChart.BeginAngle = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnSweepAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_PieChart.SweepAngle = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnEnableLabelAdjustmentCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_PieChart.LabelLayout.EnableLabelAdjustment = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub


		#EndRegion

		#Region"Fields"

		Private m_PieChart As Nevron.Nov.Chart.NPieChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NDoughnutPieExampleSchema As Nevron.Nov.Dom.NSchema

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
