Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Stacked Area Example
	''' </summary>
	Public Class NStackedAreaExample
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
            Nevron.Nov.Examples.Chart.NStackedAreaExample.NStackedAreaExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStackedAreaExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ChartView = New Nevron.Nov.Chart.NChartView()
            Me.m_ChartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			Me.m_ChartView.Surface.Titles(CInt((0))).Text = "Stacked Area"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' setup X axis
			Dim scaleX As Nevron.Nov.Chart.NOrdinalScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale)
            scaleX.InflateContentRange = False

			' add interlaced stripe for Y axis
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            Dim scaleY As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            scaleY.Strips.Add(strip)

			' add the first area
			Me.m_Area1 = New Nevron.Nov.Chart.NAreaSeries()
            Me.m_Area1.MultiAreaMode = Nevron.Nov.Chart.ENMultiAreaMode.Series
            Me.m_Area1.Name = "Product A"
            Me.m_Area1.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            chart.Series.Add(Me.m_Area1)
            Me.SetupDataLabels(Me.m_Area1)

			' add the second Area
			Me.m_Area2 = New Nevron.Nov.Chart.NAreaSeries()
            Me.m_Area2.MultiAreaMode = Nevron.Nov.Chart.ENMultiAreaMode.Stacked
            Me.m_Area2.Name = "Product B"
            Me.m_Area2.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            chart.Series.Add(Me.m_Area2)
            Me.SetupDataLabels(Me.m_Area2)

			' add the third Area
			Me.m_Area3 = New Nevron.Nov.Chart.NAreaSeries()
            Me.m_Area3.MultiAreaMode = Nevron.Nov.Chart.ENMultiAreaMode.Stacked
            Me.m_Area3.Name = "Product C"
            Me.m_Area3.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            chart.Series.Add(Me.m_Area3)
            Me.SetupDataLabels(Me.m_Area3)

			' fill with random data
			Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 10 - 1
                Me.m_Area1.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(random.[Next](20, 50)))
                Me.m_Area2.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(random.[Next](20, 50)))
                Me.m_Area3.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(random.[Next](20, 50)))
            Next

            Return Me.m_ChartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim stackModeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            stackModeComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Stacked"))
            stackModeComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Stacked Percent"))
            stackModeComboBox.SelectedIndex = 0
            AddHandler stackModeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnStackModeComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Stack Mode: ", stackModeComboBox))
            Dim showDataLabelsCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Data Labels")
            AddHandler showDataLabelsCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnShowDataLabelsCheckedChanged)
            stack.Add(showDataLabelsCheckBox)
            Me.m_Area1LabelFormatCombox = Me.CreateLabelsCombo()
            AddHandler Me.m_Area1LabelFormatCombox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnArea1LabelFormatComboxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Area 1 Label Format: ", Me.m_Area1LabelFormatCombox))
            Me.m_Area2LabelFormatCombox = Me.CreateLabelsCombo()
            AddHandler Me.m_Area2LabelFormatCombox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnArea2LabelFormatComboxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Area 2 Label Format: ", Me.m_Area2LabelFormatCombox))
            Me.m_Area3LabelFormatCombox = Me.CreateLabelsCombo()
            AddHandler Me.m_Area3LabelFormatCombox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnArea3LabelFormatComboxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Area 3 Label Format: ", Me.m_Area3LabelFormatCombox))
            showDataLabelsCheckBox.Checked = True
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a stacked area chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnStackModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Dim scale As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)

            Select Case CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex
                Case 0
                    Me.m_Area2.MultiAreaMode = Nevron.Nov.Chart.ENMultiAreaMode.Stacked
                    Me.m_Area3.MultiAreaMode = Nevron.Nov.Chart.ENMultiAreaMode.Stacked
                    scale.Labels.TextProvider = New Nevron.Nov.Chart.NFormattedScaleLabelTextProvider(New Nevron.Nov.Dom.NNumericValueFormatter(Nevron.Nov.ENNumericValueFormat.General))
                Case 1
                    Me.m_Area2.MultiAreaMode = Nevron.Nov.Chart.ENMultiAreaMode.StackedPercent
                    Me.m_Area3.MultiAreaMode = Nevron.Nov.Chart.ENMultiAreaMode.StackedPercent
                    scale.Labels.TextProvider = New Nevron.Nov.Chart.NFormattedScaleLabelTextProvider(New Nevron.Nov.Dom.NNumericValueFormatter(Nevron.Nov.ENNumericValueFormat.Percentage))
            End Select
        End Sub

        Private Sub OnShowDataLabelsCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim showDataLabelsCheckBox As Nevron.Nov.UI.NCheckBox = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox)
            Me.m_Area1.DataLabelStyle.Visible = showDataLabelsCheckBox.Checked
            Me.m_Area2.DataLabelStyle.Visible = showDataLabelsCheckBox.Checked
            Me.m_Area3.DataLabelStyle.Visible = showDataLabelsCheckBox.Checked
            Me.m_Area1LabelFormatCombox.Enabled = showDataLabelsCheckBox.Checked
            Me.m_Area2LabelFormatCombox.Enabled = showDataLabelsCheckBox.Checked
            Me.m_Area3LabelFormatCombox.Enabled = showDataLabelsCheckBox.Checked
        End Sub

        Private Sub OnArea1LabelFormatComboxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Area1.DataLabelStyle.Format = Me.GetFormatStringFromIndex(Me.m_Area1LabelFormatCombox.SelectedIndex)
        End Sub

        Private Sub OnArea2LabelFormatComboxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Area2.DataLabelStyle.Format = Me.GetFormatStringFromIndex(Me.m_Area2LabelFormatCombox.SelectedIndex)
        End Sub

        Private Sub OnArea3LabelFormatComboxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Area3.DataLabelStyle.Format = Me.GetFormatStringFromIndex(Me.m_Area3LabelFormatCombox.SelectedIndex)
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Function GetFormatStringFromIndex(ByVal index As Integer) As String
            Select Case index
                Case 0
                    Return "<value>"
                Case 1
                    Return "<total>"
                Case 2
                    Return "<cumulative>"
                Case 3
                    Return "<percent>"
                Case Else
                    Return ""
            End Select
        End Function

        Private Function CreateLabelsCombo() As Nevron.Nov.UI.NComboBox
            Dim comboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Value"))
            comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Total"))
            comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Cumulative"))
            comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Percent"))
            comboBox.SelectedIndex = 0
            Return comboBox
        End Function

        Private Sub SetupDataLabels(ByVal area As Nevron.Nov.Chart.NAreaSeries)
            Dim dataLabel As Nevron.Nov.Chart.NDataLabelStyle = area.DataLabelStyle
            dataLabel.ArrowLength = 0
            dataLabel.VertAlign = Nevron.Nov.ENVerticalAlignment.Center
            dataLabel.TextStyle.Background.Padding = New Nevron.Nov.Graphics.NMargins(5)
            dataLabel.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arial", 8, Nevron.Nov.Graphics.ENFontStyle.Bold)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ChartView As Nevron.Nov.Chart.NChartView
        Private m_Area1 As Nevron.Nov.Chart.NAreaSeries
        Private m_Area2 As Nevron.Nov.Chart.NAreaSeries
        Private m_Area3 As Nevron.Nov.Chart.NAreaSeries
        Private m_Area1LabelFormatCombox As Nevron.Nov.UI.NComboBox
        Private m_Area2LabelFormatCombox As Nevron.Nov.UI.NComboBox
        Private m_Area3LabelFormatCombox As Nevron.Nov.UI.NComboBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStackedAreaExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
