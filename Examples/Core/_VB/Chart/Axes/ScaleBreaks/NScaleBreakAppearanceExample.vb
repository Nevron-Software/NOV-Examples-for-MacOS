Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Scale Break Appearance Examples
	''' </summary>
	Public Class NScaleBreakAppearanceExample
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
            Nevron.Nov.Examples.Chart.NScaleBreakAppearanceExample.NScaleBreakAppearanceExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NScaleBreakAppearanceExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Scale Breaks Appearance"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.Padding = New Nevron.Nov.Graphics.NMargins(20)

			' configure axes
			Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)
            Me.m_Chart.PlotFill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.White, Nevron.Nov.Graphics.NColor.DarkGray)

			' configure scale
			Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            yScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
            Me.m_ScaleBreak = New Nevron.Nov.Chart.NAutoScaleBreak(0.4F)
            Me.m_ScaleBreak.PositionMode = Nevron.Nov.Chart.ENScaleBreakPositionMode.Percent
            yScale.ScaleBreaks.Add(Me.m_ScaleBreak)

			' add an interlaced strip to the Y axis
			Dim interlacedStrip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            interlacedStrip.Interlaced = True
            interlacedStrip.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige)
            yScale.Strips.Add(interlacedStrip)

			' Create some data with a peak in it
			Dim bar As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Chart.Series.Add(bar)
            bar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)

			' fill in some data so that it contains several peaks of data
			Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 8 - 1
                bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](30)))
            Next

            For i As Integer = 0 To 5 - 1
                bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(300 + random.[Next](50)))
            Next

            For i As Integer = 0 To 8 - 1
                bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](30)))
            Next

            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim styleComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            styleComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENScaleBreakStyle)()
            AddHandler styleComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnStyleComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Style:", styleComboBox))
            styleComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENScaleBreakStyle.Wave)
            Dim patternComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            patternComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENScaleBreakPattern)()
            AddHandler patternComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnPatternComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Pattern:", patternComboBox))
            Dim horzStepUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler horzStepUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnHorzStepUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Horz Step:", horzStepUpDown))
            horzStepUpDown.Value = 20
            Dim vertStepUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler vertStepUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnVertStepUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Vert Step:", vertStepUpDown))
            vertStepUpDown.Value = 10
            Dim lengthUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler lengthUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnLengthUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Length:", lengthUpDown))
            lengthUpDown.Value = 10
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to change the appearance of scale breaks.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnLengthUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ScaleBreak.Length = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnVertStepUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ScaleBreak.VertStep = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnHorzStepUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ScaleBreak.HorzStep = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnPatternComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ScaleBreak.Pattern = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENScaleBreakPattern)
        End Sub

        Private Sub OnStyleComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ScaleBreak.Style = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENScaleBreakStyle)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_ScaleBreak As Nevron.Nov.Chart.NScaleBreak

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NScaleBreakAppearanceExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
