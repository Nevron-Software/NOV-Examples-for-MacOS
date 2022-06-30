Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard Bubble Example
	''' </summary>
	Public Class NXYScatterBubbleExample
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
            Nevron.Nov.Examples.Chart.NXYScatterBubbleExample.NXYScatterBubbleExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NXYScatterBubbleExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "XY Scatter Bubble"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' configure the chart
			Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            yScale.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            yScale.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot

			' add interlace stripe
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            yScale.Strips.Add(strip)
            Dim xScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            xScale.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            xScale.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot

			' add a bubble series
			Me.m_Bubble = New Nevron.Nov.Chart.NBubbleSeries()
            Me.m_Bubble = New Nevron.Nov.Chart.NBubbleSeries()
            Me.m_Bubble.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_Bubble.LegendView.Format = "<label>"
            Me.m_Bubble.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
            Me.m_Bubble.UseXValues = True
            Me.m_Bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(27, 51, 1147995904, "India"))
            Me.m_Bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(50, 67, 1321851888, "China"))
            Me.m_Bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(76, 22, 109955400, "Mexico"))
            Me.m_Bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(210, 9, 142008838, "Russia"))
            Me.m_Bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(360, 4, 305843000, "USA"))
            Me.m_Bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(470, 5, 33560000, "Canada"))
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
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Inflate Margins: ", inflateMarginsCheckBox))
            inflateMarginsCheckBox.Checked = True
            Dim changeYValuesButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Change Y Values")
            AddHandler changeYValuesButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnChangeYValuesButtonClick)
            stack.Add(changeYValuesButton)
            Dim changeXValuesButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Change X Values")
            AddHandler changeXValuesButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnChangeXValuesButtonClick)
            stack.Add(changeXValuesButton)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a xy scatter bubble chart.</p>"
        End Function

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

		#Region"Event Handlers"

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

        Private Sub OnChangeXValuesButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To Me.m_Bubble.DataPoints.Count - 1
                Me.m_Bubble.DataPoints(CInt((i))).X = random.[Next](-100, 100)
            Next
        End Sub

        Private Sub OnChangeYValuesButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To Me.m_Bubble.DataPoints.Count - 1
                Me.m_Bubble.DataPoints(CInt((i))).Value = random.[Next](-100, 100)
            Next
        End Sub


		#EndRegion

		#Region"Fields"

		Private m_Bubble As Nevron.Nov.Chart.NBubbleSeries
        Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NXYScatterBubbleExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
