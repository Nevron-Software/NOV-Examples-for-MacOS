Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Automatic Scale Breaks Example
	''' </summary>
	Public Class NAutomaticScaleBreaksExample
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
            Nevron.Nov.Examples.Chart.NAutomaticScaleBreaksExample.NAutomaticScaleBreaksExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAutomaticScaleBreaksExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Automatic Scale Breaks"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)

			' configure axes
			Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' configure scale
			Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            yScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
            yScale.MinTickDistance = 30

			' add an interlaced strip to the Y axis
			Dim interlacedStrip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            interlacedStrip.Interlaced = True
            interlacedStrip.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige)
            yScale.Strips.Add(interlacedStrip)
            Me.OnChangeDataButtonClick(Nothing)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Me.m_EnableScaleBreaksCheckBox = New Nevron.Nov.UI.NCheckBox("Enable Scale Breaks")
            AddHandler Me.m_EnableScaleBreaksCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnableScaleBreaksCheckBoxCheckedChanged)
            stack.Add(Me.m_EnableScaleBreaksCheckBox)
            Me.m_ThresholdUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_ThresholdUpDown.[Step] = 0.05
            Me.m_ThresholdUpDown.Maximum = 1
            Me.m_ThresholdUpDown.DecimalPlaces = 2
            AddHandler Me.m_ThresholdUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnThresholdUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Threshold", Me.m_ThresholdUpDown))
            Me.m_LengthUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler Me.m_LengthUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnLengthUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Length", Me.m_LengthUpDown))
            Me.m_MaxBreaksUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler Me.m_MaxBreaksUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMaxBreaksUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Max Breaks", Me.m_MaxBreaksUpDown))
            Me.m_PositionModeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_PositionModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENScaleBreakPositionMode)()
            AddHandler Me.m_PositionModeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnPositionModeComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Position Mode:", Me.m_PositionModeComboBox))
            Me.m_PositionPercentUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_PositionPercentUpDown.Minimum = 0
            Me.m_PositionPercentUpDown.Maximum = 100
            AddHandler Me.m_PositionPercentUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnPositionPercentUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Position Percent:", Me.m_PositionPercentUpDown))
            Dim changeDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Change Data")
            AddHandler changeDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnChangeDataButtonClick)
            stack.Add(changeDataButton)

			' update initial state
			Me.m_EnableScaleBreaksCheckBox.Checked = True
            Me.m_LengthUpDown.Value = 5
            Me.m_ThresholdUpDown.Value = 0.25 ' this is relatively low factor
            Me.m_MaxBreaksUpDown.Value = 1
            Me.m_PositionPercentUpDown.Value = 50
            Me.m_PositionModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENScaleBreakPositionMode.Content) ' use content mode by default
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to add automatic scale breaks.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnChangeDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Chart.Series.Clear()

            ' setup bar series
            Dim bar As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Chart.Series.Add(bar)
            bar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            bar.Stroke = New Nevron.Nov.Graphics.NStroke(1.5F, Nevron.Nov.Graphics.NColor.DarkGreen)

            ' fill in some data so that it contains several peaks of data
            Dim random As System.Random = New System.Random()
            Dim value As Double = 0

            For i As Integer = 0 To 25 - 1

                If i < 6 Then
                    value = 600 + random.[Next](30)
                ElseIf i < 11 Then
                    value = 200 + random.[Next](50)
                ElseIf i < 16 Then
                    value = 400 + random.[Next](50)
                ElseIf i < 21 Then
                    value = random.[Next](30)
                Else
                    value = random.[Next](50)
                End If

                Dim dataPoint As Nevron.Nov.Chart.NBarDataPoint = New Nevron.Nov.Chart.NBarDataPoint(value)
                dataPoint.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Examples.Chart.NAutomaticScaleBreaksExample.ColorFromValue(value), Nevron.Nov.Graphics.NColor.DarkSlateBlue)
                bar.DataPoints.Add(dataPoint)
            Next
        End Sub

        Private Sub OnPositionPercentUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.UpdateScaleBreak()
        End Sub

        Private Sub OnPositionModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.UpdateScaleBreak()
            Me.m_PositionPercentUpDown.Enabled = Me.m_PositionModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENScaleBreakPositionMode.Percent)
        End Sub

        Private Sub OnMaxBreaksUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.UpdateScaleBreak()
        End Sub

        Private Sub OnLengthUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.UpdateScaleBreak()
        End Sub

        Private Sub OnThresholdUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.UpdateScaleBreak()
        End Sub

        Private Sub OnEnableScaleBreaksCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.UpdateScaleBreak()
            Dim enableControls As Boolean = Me.m_EnableScaleBreaksCheckBox.Checked
            Me.m_ThresholdUpDown.Enabled = enableControls
            Me.m_LengthUpDown.Enabled = enableControls
            Me.m_MaxBreaksUpDown.Enabled = enableControls
            Me.m_PositionModeComboBox.Enabled = enableControls
            Me.m_PositionPercentUpDown.Enabled = enableControls AndAlso Me.m_PositionModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENScaleBreakPositionMode.Percent)
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub UpdateScaleBreak()
            Dim scale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            scale.ScaleBreaks.Clear()

            If Me.m_EnableScaleBreaksCheckBox.Checked Then
                Dim scaleBreak As Nevron.Nov.Chart.NAutoScaleBreak = New Nevron.Nov.Chart.NAutoScaleBreak(CSng(Me.m_ThresholdUpDown.Value))
                scaleBreak.Style = Nevron.Nov.Chart.ENScaleBreakStyle.Line
                scaleBreak.Length = Me.m_LengthUpDown.Value
                scaleBreak.MaxScaleBreakCount = CInt(Me.m_MaxBreaksUpDown.Value)
                scaleBreak.PositionMode = CType(Me.m_PositionModeComboBox.SelectedIndex, Nevron.Nov.Chart.ENScaleBreakPositionMode)
                scaleBreak.Percent = CSng(Me.m_PositionPercentUpDown.Value)
                scale.ScaleBreaks.Add(scaleBreak)
            End If
        End Sub

        Private Shared Function ColorFromValue(ByVal value As Double) As Nevron.Nov.Graphics.NColor
            Dim beginColor As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.LightBlue
            Dim endColor As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.DarkSlateBlue
            Dim factor As Single = CSng((value / 650.0F))
            Dim oneMinusFactor As Single = 1.0F - factor
            Return Nevron.Nov.Graphics.NColor.FromRGB(CByte((CSng(beginColor.R) * factor + CSng(endColor.R) * oneMinusFactor)), CByte((CSng(beginColor.G) * factor + CSng(endColor.G) * oneMinusFactor)), CByte((CSng(beginColor.B) * factor + CSng(endColor.B) * oneMinusFactor)))
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_EnableScaleBreaksCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_ThresholdUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_LengthUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_MaxBreaksUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_PositionModeComboBox As Nevron.Nov.UI.NComboBox
        Private m_PositionPercentUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAutomaticScaleBreaksExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
