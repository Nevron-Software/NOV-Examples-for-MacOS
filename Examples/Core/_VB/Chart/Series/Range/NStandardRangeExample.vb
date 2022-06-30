Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard Range Example
	''' </summary>
	Public Class NStandardRangeExample
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
            Nevron.Nov.Examples.Chart.NStandardRangeExample.NStandardRangeExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStandardRangeExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Standard Range"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' setup X axis
			Dim xScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            xScale.MajorGridLines.Visible = True

			' setup Y axis
			Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            yScale.MajorGridLines.Visible = True
			
			' add interlaced stripe
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            yScale.Strips.Add(strip)

			' setup shape series
			Me.m_Range = New Nevron.Nov.Chart.NRangeSeries()
            Me.m_Chart.Series.Add(Me.m_Range)
            Me.m_Range.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_Range.UseXValues = True
            Me.m_Range.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkOrange)
            Me.m_Range.Stroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkRed)

			' fill data
			Dim intervals As Double() = New Double() {5, 5, 5, 5, 5, 5, 5, 5, 5, 15, 30, 60}
            Dim values As Double() = New Double() {4180, 13687, 18618, 19634, 17981, 7190, 16369, 3212, 4122, 9200, 6461, 3435}
            Dim count As Integer = System.Math.Min(intervals.Length, values.Length)
            Dim x As Double = 0

            For i As Integer = 0 To count - 1
                Dim interval As Double = intervals(i)
                Dim value As Double = values(i)
                Dim x1 As Double = x
                Dim y1 As Double = 0
                x += interval
                Dim x2 As Double = x
                Dim y2 As Double = value / interval
                Me.m_Range.DataPoints.Add(New Nevron.Nov.Chart.NRangeDataPoint(x1, y1, x2, y2))
            Next

            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim rangeShapeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            rangeShapeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENBarShape)()
            AddHandler rangeShapeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRangeShapeComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Range Shape: ", rangeShapeComboBox))
            Dim showDataLabels As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Data Labels")
            AddHandler showDataLabels.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnShowDataLabelsCheckedChanged)
            stack.Add(showDataLabels)
            rangeShapeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENBarShape.Rectangle)
            showDataLabels.Checked = False
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard range chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnShowDataLabelsCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If TryCast(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                Me.m_Range.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(True)
                Me.m_Range.DataLabelStyle.Format = "<y2>"
            Else
                Me.m_Range.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            End If
        End Sub

        Private Sub OnRangeShapeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Range.Shape = CType(TryCast(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENBarShape)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Range As Nevron.Nov.Chart.NRangeSeries
        Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStandardRangeExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
