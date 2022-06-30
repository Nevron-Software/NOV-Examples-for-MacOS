Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard Bubble Example
	''' </summary>
	Public Class NStandardBubbleExample
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
            Nevron.Nov.Examples.Chart.NStandardBubbleExample.NStandardBubbleExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStandardBubbleExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Standard Bubble"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' configure the chart
			Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            yScale.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            yScale.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot

			' add interlace stripe
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            yScale.Strips.Add(strip)
            Dim xScale As Nevron.Nov.Chart.NOrdinalScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale)
            xScale.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            xScale.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot

			' add a bubble series
			Me.m_Bubble = New Nevron.Nov.Chart.NBubbleSeries()
            Me.m_Bubble.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            Me.m_Bubble.DataLabelStyle.VertAlign = Nevron.Nov.ENVerticalAlignment.Center
            Me.m_Bubble.DataLabelStyle.Visible = False
            Me.m_Bubble.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
            Me.m_Bubble.MinSize = 20
            Me.m_Bubble.MaxSize = 100
            Me.m_Bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(10, 10, "Company 1"))
            Me.m_Bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(15, 20, "Company 2"))
            Me.m_Bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(12, 25, "Company 3"))
            Me.m_Bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(8, 15, "Company 4"))
            Me.m_Bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(14, 17, "Company 5"))
            Me.m_Bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(11, 12, "Company 6"))
            Me.m_Chart.Series.Add(Me.m_Bubble)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, True))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim bubbleShapeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            bubbleShapeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENPointShape)()
            AddHandler bubbleShapeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnBubbleShapeComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Bubble Shape: ", bubbleShapeComboBox))
            bubbleShapeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENPointShape.Ellipse)
            Dim minBubbleSizeUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler minBubbleSizeUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMinBubbleSizeUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Min Bubble Size:", minBubbleSizeUpDown))
            minBubbleSizeUpDown.Value = 50
            Dim maxBubbleSizeUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler maxBubbleSizeUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMaxBubbleSizeUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Max Bubble Size:", maxBubbleSizeUpDown))
            maxBubbleSizeUpDown.Value = 200
            Dim inflateMarginsCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox()
            AddHandler inflateMarginsCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnInflateMarginsCheckBoxCheckedChanged)
            inflateMarginsCheckBox.Checked = True
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Inflate Margins: ", inflateMarginsCheckBox))
            Dim legendFormatComboBox As Nevron.Nov.UI.NComboBox = Me.CreateLegendFormatCombo()
            AddHandler legendFormatComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnLegendFormatComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Legend Format:", legendFormatComboBox))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard bubble chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnLegendFormatComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Bubble.LegendView.Format = CStr(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedItem.Tag)
        End Sub

        Private Sub OnBubbleShapeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Bubble.Shape = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENPointShape)
        End Sub

        Private Sub OnInflateMarginsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Bubble.InflateMargins = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnMaxBubbleSizeUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Bubble.MaxSize = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnMinBubbleSizeUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Bubble.MinSize = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Function CreateLegendFormatCombo() As Nevron.Nov.UI.NComboBox
            Dim comboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            Dim item As Nevron.Nov.UI.NComboBoxItem = New Nevron.Nov.UI.NComboBoxItem("Value and Label")
            item.Tag = "<value> <label>"
            comboBox.Items.Add(item)
            item = New Nevron.Nov.UI.NComboBoxItem("Value")
            item.Tag = "<value>"
            comboBox.Items.Add(item)
            item = New Nevron.Nov.UI.NComboBoxItem("Label")
            item.Tag = "<label>"
            comboBox.Items.Add(item)
            item = New Nevron.Nov.UI.NComboBoxItem("Size")
            item.Tag = "<size>"
            comboBox.Items.Add(item)
            Return comboBox
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Bubble As Nevron.Nov.Chart.NBubbleSeries
        Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStandardBubbleExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
