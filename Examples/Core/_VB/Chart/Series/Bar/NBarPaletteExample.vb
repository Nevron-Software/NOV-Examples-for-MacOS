Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' This example demonstrates how to associate a palette with a bar series
	''' </summary>
	Public Class NBarPaletteExample
        Inherits NExampleBase
        #Region"Constructors"

        ''' <summary>
        ''' 
        ''' </summary>
        Public Sub New()
        End Sub
        ''' <summary>
        ''' 
        ''' </summary>
        Shared Sub New()
            Nevron.Nov.Examples.Chart.NBarPaletteExample.NBarPaletteExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NBarPaletteExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)
            AddHandler chartView.Registered, AddressOf Me.OnChartViewRegistered
            AddHandler chartView.Unregistered, AddressOf Me.OnChartViewUnregistered

            ' configure title
            chartView.Surface.Titles(CInt((0))).Text = "Bar Palette"

            ' configure chart
            Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

            ' add interlace stripe
            Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            'linearScale.Strips.Add(strip);

            ' setup a bar series
            Me.m_Bar = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Bar.Name = "Bar Series"
            Me.m_Bar.InflateMargins = True
            Me.m_Bar.UseXValues = False
            Me.m_Bar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_Bar.Palette = New Nevron.Nov.Chart.NColorValuePalette(New Nevron.Nov.Chart.NColorValuePair() {New Nevron.Nov.Chart.NColorValuePair(0, Nevron.Nov.Graphics.NColor.Green), New Nevron.Nov.Chart.NColorValuePair(60, Nevron.Nov.Graphics.NColor.Yellow), New Nevron.Nov.Chart.NColorValuePair(120, Nevron.Nov.Graphics.NColor.Red)})
            Me.m_AxisRange = New Nevron.Nov.Graphics.NRange(0, 130)

			' limit the axis range to 0, 130
			Dim yAxis As Nevron.Nov.Chart.NCartesianAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryY)
            yAxis.ViewRangeMode = Nevron.Nov.Chart.ENAxisViewRangeMode.FixedRange
            yAxis.MinViewRangeValue = Me.m_AxisRange.Begin
            yAxis.MaxViewRangeValue = Me.m_AxisRange.[End]
            Me.m_Chart.Series.Add(Me.m_Bar)
            Dim indicatorCount As Integer = 10
            Me.m_IndicatorPhase = New Double(indicatorCount - 1) {}

            ' add some data to the bar series
            For i As Integer = 0 To indicatorCount - 1
                Me.m_IndicatorPhase(i) = i * 30
                Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(0))
            Next

            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim toggleTimerButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Stop Timer")
            AddHandler toggleTimerButton.Click, AddressOf Me.OnToggleTimerButtonClick
            toggleTimerButton.Tag = 0
            stack.Add(toggleTimerButton)
            Dim paletteColorModeCombo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            paletteColorModeCombo.FillFromEnum(Of Nevron.Nov.Chart.ENPaletteColorMode)()
            AddHandler paletteColorModeCombo.SelectedIndexChanged, AddressOf Me.OnPaletteColorModeComboSelectedIndexChanged
            paletteColorModeCombo.SelectedIndex = CInt(Nevron.Nov.Chart.ENPaletteColorMode.Spread)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Palette Color Mode:", paletteColorModeCombo))
            Dim invertScaleCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Invert Scale")
            AddHandler invertScaleCheckBox.CheckedChanged, AddressOf Me.OnInvertScaleCheckBoxCheckedChanged
            invertScaleCheckBox.Checked = False
            stack.Add(invertScaleCheckBox)
            Dim smoothPaletteCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Smooth Palette")
            AddHandler smoothPaletteCheckBox.CheckedChanged, AddressOf Me.OnSmoothPaletteCheckBoxCheckedChanged
            smoothPaletteCheckBox.Checked = True
            stack.Add(smoothPaletteCheckBox)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to associate a palette with a bar series.</p>"
        End Function

		#EndRegion

		#Region"Override"

		Private Sub OnChartViewRegistered(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Timer = New Nevron.Nov.NTimer()
            AddHandler Me.m_Timer.Tick, AddressOf Me.OnTimerTick
            Me.m_Timer.Start()
        End Sub

        Private Sub OnChartViewUnregistered(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Timer.[Stop]()
            RemoveHandler Me.m_Timer.Tick, AddressOf Me.OnTimerTick
            Me.m_Timer = Nothing
        End Sub

		#EndRegion
		
		#Region"Event Handlers"

		Private Sub OnInvertScaleCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Invert = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnPaletteColorModeComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim paletteColorMode As Nevron.Nov.Chart.ENPaletteColorMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENPaletteColorMode)
            Me.m_Bar.PaletteColorMode = paletteColorMode
        End Sub

        Private Sub OnToggleTimerButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim button As Nevron.Nov.UI.NButton = CType(arg.TargetNode, Nevron.Nov.UI.NButton)

            If CInt(button.Tag) = 0 Then
                Me.m_Timer.[Stop]()
                button.Content = New Nevron.Nov.UI.NLabel("Start Timer")
                button.Tag = 1
            Else
                Me.m_Timer.Start()
                button.Content = New Nevron.Nov.UI.NLabel("Stop Timer")
                button.Tag = 0
            End If
        End Sub

        Private Sub OnTimerTick()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To Me.m_Bar.DataPoints.Count - 1
                Dim value As Double = (Me.m_AxisRange.Begin + Me.m_AxisRange.[End]) / 2.0 + System.Math.Sin(Me.m_IndicatorPhase(i) * Nevron.Nov.NAngle.Degree2Rad) * Me.m_AxisRange.GetLength() / 2 + random.[Next](20)
                value = Me.m_AxisRange.GetValueInRange(value)
                Me.m_Bar.DataPoints(CInt((i))).Value = value
                Me.m_IndicatorPhase(i) += 10
            Next
        End Sub

        Private Sub OnSmoothPaletteCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim smoothPalette As Boolean = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
            Me.m_Bar.Palette.SmoothColors = smoothPalette
        End Sub

		#EndRegion

		#Region"Fields"

        Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_Bar As Nevron.Nov.Chart.NBarSeries
        Private m_Timer As Nevron.Nov.NTimer
        Private m_IndicatorPhase As Double()
        Private m_AxisRange As Nevron.Nov.Graphics.NRange

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NBarPaletteExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
