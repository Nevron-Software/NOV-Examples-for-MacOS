Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Gauge
	''' <summary>
	''' This example demonstrates how to associate a palette with an indicator
	''' </summary>
	Public Class NIndicatorPaletteExample
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
            Nevron.Nov.Examples.Gauge.NIndicatorPaletteExample.NIndicatorPaletteExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NIndicatorPaletteExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_AxisRange = New Nevron.Nov.Graphics.NRange(80, 130)
            Dim indicatorCount As Integer = 4
            Me.m_IndicatorPhase = New Double(indicatorCount - 1) {}

			' create gauges
			Me.CreateLinearGauge()
            Me.CreateRadialGauge()

			' add to stack
			stack.Add(Me.m_LinearGauge)
            stack.Add(Me.m_RadialGauge)

			' add axes
			Me.m_LinearGauge.Axes.Add(Me.CreateGaugeAxis())
            Me.m_RadialGauge.Axes.Add(Me.CreateGaugeAxis())
            Dim offset As Double = 10

			' now add two indicators
			For i As Integer = 0 To indicatorCount - 1
                Me.m_IndicatorPhase(i) = i * 30
                Me.m_LinearGauge.Indicators.Add(Me.CreateRangeIndicator(offset))
                offset += 20
            Next

            Me.m_RadialGauge.Indicators.Add(Me.CreateRangeIndicator(0))
            AddHandler stack.Registered, AddressOf Me.OnStackRegistered
            AddHandler stack.Unregistered, AddressOf Me.OnStackUnregistered
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))
            Dim toggleTimerButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Stop Timer")
            AddHandler toggleTimerButton.Click, AddressOf Me.OnToggleTimerButtonClick
            toggleTimerButton.Tag = 0
            stack.Add(toggleTimerButton)
            Dim rangeIndicatorShapeCombo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            rangeIndicatorShapeCombo.FillFromEnum(Of Nevron.Nov.Chart.ENRangeIndicatorShape)()
            AddHandler rangeIndicatorShapeCombo.SelectedIndexChanged, AddressOf Me.OnRangeIndicatorShapeComboSelectedIndexChanged
            rangeIndicatorShapeCombo.SelectedIndex = CInt(Nevron.Nov.Chart.ENRangeIndicatorShape.Bar)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Shape:", rangeIndicatorShapeCombo))
            Dim paletteColorModeCombo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            paletteColorModeCombo.FillFromEnum(Of Nevron.Nov.Chart.ENPaletteColorMode)()
            AddHandler paletteColorModeCombo.SelectedIndexChanged, AddressOf Me.OnPaletteColorModeComboSelectedIndexChanged
            paletteColorModeCombo.SelectedIndex = CInt(Nevron.Nov.Chart.ENPaletteColorMode.Spread)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Palette Color Mode:", paletteColorModeCombo))
            Dim orientationCombo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            orientationCombo.FillFromEnum(Of Nevron.Nov.Chart.ENLinearGaugeOrientation)()
            AddHandler orientationCombo.SelectedIndexChanged, AddressOf Me.OnOrientationComboSelectedIndexChanged
            orientationCombo.SelectedIndex = CInt(Nevron.Nov.Chart.ENLinearGaugeOrientation.Vertical)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Orientation", orientationCombo))
            Dim beginAngleUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            beginAngleUpDown.Value = Me.m_RadialGauge.BeginAngle.Value
            AddHandler beginAngleUpDown.ValueChanged, AddressOf Me.OnBeginAngleUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin Angle:", beginAngleUpDown))
            Dim sweepAngleUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            sweepAngleUpDown.Value = Me.m_RadialGauge.BeginAngle.Value
            AddHandler sweepAngleUpDown.ValueChanged, AddressOf Me.OnSweepAngleUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Sweep Angle:", sweepAngleUpDown))
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
            Return "<p>This example demonstrates how to associate a palette with an indicator.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub CreateRadialGauge()
            Me.m_RadialGauge = New Nevron.Nov.Chart.NRadialGauge()
            Me.m_RadialGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NIndicatorPaletteExample.defaultRadialGaugeSize
            Me.m_RadialGauge.CapEffect = New Nevron.Nov.Chart.NGelCapEffect()
            Me.m_RadialGauge.Border = Nevron.Nov.Examples.Gauge.NIndicatorPaletteExample.CreateBorder()
            Me.m_RadialGauge.BorderThickness = New Nevron.Nov.Graphics.NMargins(6)
            Me.m_RadialGauge.BackgroundFill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.DarkGray, Nevron.Nov.Graphics.NColor.Black)
            Me.m_RadialGauge.NeedleCap.Visible = False
            Me.m_RadialGauge.PreferredWidth = 200
            Me.m_RadialGauge.PreferredHeight = 200
            Me.m_RadialGauge.Padding = New Nevron.Nov.Graphics.NMargins(5)
        End Sub

        Private Sub CreateLinearGauge()
            Me.m_LinearGauge = New Nevron.Nov.Chart.NLinearGauge()
            Me.m_LinearGauge.Orientation = Nevron.Nov.Chart.ENLinearGaugeOrientation.Vertical
            Me.m_LinearGauge.PreferredWidth = 200
            Me.m_LinearGauge.PreferredHeight = 300
            Me.m_LinearGauge.CapEffect = New Nevron.Nov.Chart.NGlassCapEffect()
            Me.m_LinearGauge.Border = Nevron.Nov.Examples.Gauge.NIndicatorPaletteExample.CreateBorder()
            Me.m_LinearGauge.Padding = New Nevron.Nov.Graphics.NMargins(20)
            Me.m_LinearGauge.BorderThickness = New Nevron.Nov.Graphics.NMargins(6)
            Me.m_LinearGauge.BackgroundFill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.DarkGray, Nevron.Nov.Graphics.NColor.Black)
            Me.m_LinearGauge.Axes.Clear()
        End Sub

        Private Function CreateGaugeAxis() As Nevron.Nov.Chart.NGaugeAxis
            Dim axis As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            axis.Range = Me.m_AxisRange
            axis.Anchor = New Nevron.Nov.Chart.NDockGaugeAxisAnchor(Nevron.Nov.Chart.ENGaugeAxisDockZone.Top, True, Nevron.Nov.Chart.ENScaleOrientation.Left, 0, 100)
            axis.Scale.SetColor(Nevron.Nov.Graphics.NColor.White)
            CType(axis.Scale, Nevron.Nov.Chart.NLinearScale).Labels.Style.TextStyle.Font.Style = Nevron.Nov.Graphics.ENFontStyle.Bold
            Return axis
        End Function

        Private Function CreateRangeIndicator(ByVal offsetFromScale As Double) As Nevron.Nov.Chart.NRangeIndicator
            Dim rangeIndicator As Nevron.Nov.Chart.NRangeIndicator = New Nevron.Nov.Chart.NRangeIndicator()
            rangeIndicator.Value = 0
            rangeIndicator.Stroke = Nothing
            rangeIndicator.OffsetFromScale = offsetFromScale
            rangeIndicator.BeginWidth = 10
            rangeIndicator.EndWidth = 10
            rangeIndicator.BeginWidth = 10
            rangeIndicator.EndWidth = 10

			' assign palette to the indicator
			Dim palette As Nevron.Nov.Chart.NColorValuePalette = New Nevron.Nov.Chart.NColorValuePalette(New Nevron.Nov.Chart.NColorValuePair() {New Nevron.Nov.Chart.NColorValuePair(80, Nevron.Nov.Graphics.NColor.Green), New Nevron.Nov.Chart.NColorValuePair(100, Nevron.Nov.Graphics.NColor.Yellow), New Nevron.Nov.Chart.NColorValuePair(120, Nevron.Nov.Graphics.NColor.Red)})
            rangeIndicator.Palette = palette
            Return rangeIndicator
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnStackRegistered(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Timer = New Nevron.Nov.NTimer()
            AddHandler Me.m_Timer.Tick, AddressOf Me.OnTimerTick
            Me.m_Timer.Start()
        End Sub

        Private Sub OnStackUnregistered(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Timer.[Stop]()
            RemoveHandler Me.m_Timer.Tick, AddressOf Me.OnTimerTick
            Me.m_Timer = Nothing
        End Sub

        Private Sub OnSweepAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnBeginAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RadialGauge.BeginAngle = New Nevron.Nov.NAngle(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnInvertScaleCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_LinearGauge.Axes(CInt((0))).Scale.Invert = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
            Me.m_RadialGauge.Axes(CInt((0))).Scale.Invert = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnPaletteColorModeComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim paletteColorMode As Nevron.Nov.Chart.ENPaletteColorMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENPaletteColorMode)

            For i As Integer = 0 To Me.m_LinearGauge.Indicators.Count - 1
                CType(Me.m_LinearGauge.Indicators(CInt((i))), Nevron.Nov.Chart.NRangeIndicator).PaletteColorMode = paletteColorMode
            Next

            CType(Me.m_RadialGauge.Indicators(CInt((0))), Nevron.Nov.Chart.NRangeIndicator).PaletteColorMode = paletteColorMode
        End Sub

        Private Sub OnOrientationComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_LinearGauge.Orientation = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENLinearGaugeOrientation)

            Select Case Me.m_LinearGauge.Orientation
                Case Nevron.Nov.Chart.ENLinearGaugeOrientation.Horizontal
                    Me.m_LinearGauge.PreferredWidth = 300
                    Me.m_LinearGauge.PreferredHeight = 200
                Case Nevron.Nov.Chart.ENLinearGaugeOrientation.Vertical
                    Me.m_LinearGauge.PreferredWidth = 200
                    Me.m_LinearGauge.PreferredHeight = 300
            End Select
        End Sub

        Private Sub OnRangeIndicatorShapeComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim shape As Nevron.Nov.Chart.ENRangeIndicatorShape = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENRangeIndicatorShape)

            For i As Integer = 0 To Me.m_LinearGauge.Indicators.Count - 1
                CType(Me.m_LinearGauge.Indicators(CInt((i))), Nevron.Nov.Chart.NRangeIndicator).Shape = shape
            Next

            CType(Me.m_RadialGauge.Indicators(CInt((0))), Nevron.Nov.Chart.NRangeIndicator).Shape = shape
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

            For i As Integer = 0 To Me.m_LinearGauge.Indicators.Count - 1
                Dim value As Double = (Me.m_AxisRange.Begin + Me.m_AxisRange.[End]) / 2.0 + System.Math.Sin(Me.m_IndicatorPhase(i) * Nevron.Nov.NAngle.Degree2Rad) * Me.m_AxisRange.GetLength() / 2 + random.[Next](20)
                value = Me.m_AxisRange.GetValueInRange(value)
                CType(Me.m_LinearGauge.Indicators(CInt((i))), Nevron.Nov.Chart.NRangeIndicator).Value = value
                Me.m_IndicatorPhase(i) += 10
            Next

            CType(Me.m_RadialGauge.Indicators(CInt((0))), Nevron.Nov.Chart.NRangeIndicator).Value = CType(Me.m_LinearGauge.Indicators(CInt((0))), Nevron.Nov.Chart.NRangeIndicator).Value
        End Sub

        Private Sub OnSmoothPaletteCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim smoothPalette As Boolean = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked

            For i As Integer = 0 To Me.m_LinearGauge.Indicators.Count - 1
                Me.m_LinearGauge.Indicators(CInt((i))).Palette.SmoothColors = smoothPalette
            Next

            Me.m_RadialGauge.Indicators(CInt((0))).Palette.SmoothColors = smoothPalette
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_LinearGauge As Nevron.Nov.Chart.NLinearGauge
        Private m_RadialGauge As Nevron.Nov.Chart.NRadialGauge
        Private m_Timer As Nevron.Nov.NTimer
        Private m_IndicatorPhase As Double()
        Private m_AxisRange As Nevron.Nov.Graphics.NRange

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NIndicatorPaletteExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateBorder() As Nevron.Nov.UI.NBorder
            Return Nevron.Nov.UI.NBorder.CreateThreeColorBorder(Nevron.Nov.Graphics.NColor.LightGray, Nevron.Nov.Graphics.NColor.White, Nevron.Nov.Graphics.NColor.DarkGray, 10, 10)
        End Function

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultRadialGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(300, 300)

		#EndRegion
	End Class
End Namespace
