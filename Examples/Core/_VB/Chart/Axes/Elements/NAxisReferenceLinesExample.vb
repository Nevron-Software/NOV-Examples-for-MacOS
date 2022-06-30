Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis docking example
	''' </summary>
	Public Class NAxisReferenceLinesExample
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
            Nevron.Nov.Examples.Chart.NAxisReferenceLinesExample.NAxisReferenceLinesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisReferenceLinesExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Axis Reference Lines"
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' configure the y scale
			Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            yScale.Strips.Add(stripStyle)
            yScale.MajorGridLines.Visible = True

			' Create a point series
			Dim point As Nevron.Nov.Chart.NPointSeries = New Nevron.Nov.Chart.NPointSeries()
            point.InflateMargins = True
            point.UseXValues = True
            point.Name = "Series 1"
            chart.Series.Add(point)
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Visible = False
            point.DataLabelStyle = dataLabelStyle

			' Add some data
			Dim yValues As Double() = New Double() {31, 67, 12, 84, 90}
            Dim xValues As Double() = New Double() {5, 36, 51, 76, 93}

            For i As Integer = 0 To yValues.Length - 1
                point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(xValues(i), yValues(i)))
            Next

			' Add a constline for the left axis
			Me.m_XReferenceLine = New Nevron.Nov.Chart.NAxisReferenceLine()
            Me.m_XReferenceLine.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.SteelBlue)
            Me.m_XReferenceLine.Value = 40
            Me.m_XReferenceLine.Text = "X Reference Line"
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).ReferenceLines.Add(Me.m_XReferenceLine)

			' Add a constline for the bottom axis
			Me.m_YReferenceLine = New Nevron.Nov.Chart.NAxisReferenceLine()
            Me.m_YReferenceLine.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.SteelBlue)
            Me.m_YReferenceLine.Value = 60
            Me.m_YReferenceLine.Text = "Y Reference Line"
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).ReferenceLines.Add(Me.m_YReferenceLine)

			' apply style sheet
			chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            stack.Add(New Nevron.Nov.UI.NLabel("Y Axis Reference Line"))
            Dim yReferenceLineValueUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler yReferenceLineValueUpDown.ValueChanged, AddressOf Me.OnYReferenceLineValueUpDownValueChanged
            yReferenceLineValueUpDown.Value = Me.m_YReferenceLine.Value
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Value:", yReferenceLineValueUpDown))
            Dim yReferenceLineIncludeInAxisRangeCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Include Value in Axis Range")
            yReferenceLineIncludeInAxisRangeCheckBox.Checked = Me.m_YReferenceLine.IncludeValueInAxisRange
            AddHandler yReferenceLineIncludeInAxisRangeCheckBox.CheckedChanged, AddressOf Me.OnYReferenceLineIncludeInAxisRangeCheckBoxCheckedChanged
            stack.Add(yReferenceLineIncludeInAxisRangeCheckBox)
            Dim yReferenceLinePaintAfterSeriesCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Paint After Series")
            yReferenceLinePaintAfterSeriesCheckBox.Checked = Me.m_YReferenceLine.PaintAfterChartContent
            AddHandler yReferenceLinePaintAfterSeriesCheckBox.CheckedChanged, AddressOf Me.OnYReferenceLinePaintAfterSeriesCheckBoxCheckedChanged
            stack.Add(yReferenceLinePaintAfterSeriesCheckBox)
            Dim yTextAlignmentComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            yTextAlignmentComboBox.FillFromEnum(Of Nevron.Nov.ENContentAlignment)()
            yTextAlignmentComboBox.SelectedIndex = CInt(Me.m_YReferenceLine.TextAlignment)
            AddHandler yTextAlignmentComboBox.SelectedIndexChanged, AddressOf Me.OnYTextAlignmentComboBoxSelectedIndexChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Text Alignment:", yTextAlignmentComboBox))
            stack.Add(New Nevron.Nov.UI.NLabel("X Axis Reference Line"))
            Dim xReferenceLineValueUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            xReferenceLineValueUpDown.Value = Me.m_XReferenceLine.Value
            AddHandler xReferenceLineValueUpDown.ValueChanged, AddressOf Me.OnXReferenceLineValueUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Value:", xReferenceLineValueUpDown))
            Dim xReferenceLinePaintAfterSeriesCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Paint After Series")
            xReferenceLinePaintAfterSeriesCheckBox.Checked = Me.m_XReferenceLine.PaintAfterChartContent
            AddHandler xReferenceLinePaintAfterSeriesCheckBox.CheckedChanged, AddressOf Me.OnXReferenceLinePaintAfterSeriesCheckBoxCheckedChanged
            stack.Add(xReferenceLinePaintAfterSeriesCheckBox)
            Dim xReferenceLineIncludeInAxisRangeCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Include Value in Axis Range")
            xReferenceLineIncludeInAxisRangeCheckBox.Checked = Me.m_YReferenceLine.IncludeValueInAxisRange
            AddHandler xReferenceLineIncludeInAxisRangeCheckBox.CheckedChanged, AddressOf Me.OnXReferenceLineIncludeInAxisRangeCheckBoxCheckedChanged
            stack.Add(xReferenceLineIncludeInAxisRangeCheckBox)
            Dim xTextAlignmentComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            xTextAlignmentComboBox.FillFromEnum(Of Nevron.Nov.ENContentAlignment)()
            xTextAlignmentComboBox.SelectedIndex = CInt(Me.m_XReferenceLine.TextAlignment)
            AddHandler xTextAlignmentComboBox.SelectedIndexChanged, AddressOf Me.OnXTextAlignmentComboBoxSelectedIndexChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Text Alignment:", xTextAlignmentComboBox))
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to add axis reference lines.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnXReferenceLineValueUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_XReferenceLine.Value = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnXReferenceLineIncludeInAxisRangeCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_XReferenceLine.IncludeValueInAxisRange = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnXReferenceLinePaintAfterSeriesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_XReferenceLine.PaintAfterChartContent = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnXTextAlignmentComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_XReferenceLine.TextAlignment = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.ENContentAlignment)
        End Sub

        Private Sub OnYReferenceLineValueUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_YReferenceLine.Value = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnYReferenceLineIncludeInAxisRangeCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_YReferenceLine.IncludeValueInAxisRange = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnYReferenceLinePaintAfterSeriesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_YReferenceLine.PaintAfterChartContent = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnYTextAlignmentComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_YReferenceLine.TextAlignment = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.ENContentAlignment)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_XReferenceLine As Nevron.Nov.Chart.NAxisReferenceLine
        Private m_YReferenceLine As Nevron.Nov.Chart.NAxisReferenceLine

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisReferenceLinesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
