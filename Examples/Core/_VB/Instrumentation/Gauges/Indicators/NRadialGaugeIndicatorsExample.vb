Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Gauge
    ''' <summary>
	''' This example demonstrates how to add indicators to a radial gauge 
    ''' </summary>
	Public Class NRadialGaugeIndicatorsExample
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
            Nevron.Nov.Examples.Gauge.NRadialGaugeIndicatorsExample.NRadialGaugeIndicatorsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NRadialGaugeIndicatorsExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Dim controlStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(controlStack)

			' create the radial gauge
			Me.m_RadialGauge = New Nevron.Nov.Chart.NRadialGauge()
            Me.m_RadialGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NRadialGaugeIndicatorsExample.defaultRadialGaugeSize
            Me.m_RadialGauge.CapEffect = New Nevron.Nov.Chart.NGlassCapEffect()
            Me.m_RadialGauge.Dial = New Nevron.Nov.Chart.NDial(Nevron.Nov.Chart.ENDialShape.CutCircle, New Nevron.Nov.Chart.NEdgeDialRim())
            Me.m_RadialGauge.Dial.BackgroundFill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.DarkGray, Nevron.Nov.Graphics.NColor.Black)

			' configure scale
			Dim axis As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            Me.m_RadialGauge.Axes.Add(axis)
            Dim scale As Nevron.Nov.Chart.NLinearScale = CType(axis.Scale, Nevron.Nov.Chart.NLinearScale)
            scale.SetPredefinedScale(Nevron.Nov.Chart.ENPredefinedScaleStyle.Presentation)
            scale.Labels.Style.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 10, Nevron.Nov.Graphics.ENFontStyle.Bold)
            scale.Labels.Style.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            scale.Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 90.0)
            scale.MinorTickCount = 4
            scale.Ruler.Stroke.Width = 0
            scale.Ruler.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkGray)

			' add radial gauge indicators
			Me.m_RangeIndicator = New Nevron.Nov.Chart.NRangeIndicator()
            Me.m_RangeIndicator.Value = 20
            Me.m_RangeIndicator.Palette = New Nevron.Nov.Chart.NTwoColorPalette(Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.Graphics.NColor.Red)
            Me.m_RangeIndicator.Stroke = Nothing
            Me.m_RangeIndicator.EndWidth = 20
            Me.m_RadialGauge.Indicators.Add(Me.m_RangeIndicator)
            Me.m_ValueIndicator = New Nevron.Nov.Chart.NNeedleValueIndicator()
            Me.m_ValueIndicator.Value = 79
            Me.m_ValueIndicator.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Horizontal, Nevron.Nov.Graphics.ENGradientVariant.Variant1, Nevron.Nov.Graphics.NColor.White, Nevron.Nov.Graphics.NColor.Red)
            Me.m_ValueIndicator.Stroke.Color = Nevron.Nov.Graphics.NColor.Red
            Me.m_ValueIndicator.OffsetFromCenter = -20
            Me.m_RadialGauge.Indicators.Add(Me.m_ValueIndicator)
            Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(270.0, Nevron.Nov.NUnit.Degree)

			' add radial gauge
			controlStack.Add(Me.m_RadialGauge)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))

			' value indicator properties
			Dim valueIndicatorGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Value")
            propertyStack.Add(valueIndicatorGroupBox)
            Dim valueIndicatorGroupBoxContent As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            valueIndicatorGroupBox.Content = New Nevron.Nov.UI.NUniSizeBoxGroup(valueIndicatorGroupBoxContent)
            Dim markerValueIndicator As Nevron.Nov.Chart.NMarkerValueIndicator = New Nevron.Nov.Chart.NMarkerValueIndicator()
            markerValueIndicator.Value = 10
            Me.m_RadialGauge.Indicators.Add(markerValueIndicator)
            Me.m_ValueIndicatorUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_ValueIndicatorUpDown.Value = Me.m_ValueIndicator.Value
            AddHandler Me.m_ValueIndicatorUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnValueIndicatorUpDownValueChanged)
            valueIndicatorGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Value:", Me.m_ValueIndicatorUpDown, True))
            Me.m_ValueIndicatorWidthUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_ValueIndicatorWidthUpDown.Value = Me.m_ValueIndicator.Width
            AddHandler Me.m_ValueIndicatorWidthUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnValueIndicatorWidthUpDownValueChanged)
            valueIndicatorGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Width:", Me.m_ValueIndicatorWidthUpDown, True))
            Me.m_ValueIndicatorOffsetFromCenterUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_ValueIndicatorOffsetFromCenterUpDown.Value = Me.m_ValueIndicator.OffsetFromCenter
            AddHandler Me.m_ValueIndicatorOffsetFromCenterUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnValueIndicatorOffsetFromCenterUpDownValueChanged)
            valueIndicatorGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Offset From Center:", Me.m_ValueIndicatorOffsetFromCenterUpDown, True))
            Me.m_ValueIndicatorShapeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_ValueIndicatorShapeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENNeedleShape)()
            Me.m_ValueIndicatorShapeComboBox.SelectedIndex = CInt(Me.m_ValueIndicator.Shape)
            AddHandler Me.m_ValueIndicatorShapeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnValueIndicatorShapeComboBoxSelectedIndexChanged)
            valueIndicatorGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Shape:", Me.m_ValueIndicatorShapeComboBox, True))

			' Range indicator properties
			Dim rangeIndicatorGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Range")
            propertyStack.Add(rangeIndicatorGroupBox)
            Dim rangeIndicatorGroupBoxContent As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            rangeIndicatorGroupBox.Content = New Nevron.Nov.UI.NUniSizeBoxGroup(rangeIndicatorGroupBoxContent)
            Me.m_RangeIndicatorOriginModeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_RangeIndicatorOriginModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENRangeIndicatorOriginMode)()
            Me.m_RangeIndicatorOriginModeComboBox.SelectedIndex = CInt(Me.m_RangeIndicator.OriginMode)
            rangeIndicatorGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Origin Mode:", Me.m_RangeIndicatorOriginModeComboBox, True))
            Me.m_RangeIndicatorOriginUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_RangeIndicatorOriginUpDown.Value = Me.m_RangeIndicator.Origin
            AddHandler Me.m_RangeIndicatorOriginUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRangeIndicatorOriginUpDownValueChanged)
            rangeIndicatorGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Origin:", Me.m_RangeIndicatorOriginUpDown, True))
            Me.m_RangeIndicatorValueUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_RangeIndicatorValueUpDown.Value = Me.m_RangeIndicator.Value
            AddHandler Me.m_RangeIndicatorValueUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRangeIndicatorValueUpDownValueChanged)
            rangeIndicatorGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Value:", Me.m_RangeIndicatorValueUpDown, True))
            Me.m_BeginAngleUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_BeginAngleUpDown.Maximum = 360
            Me.m_BeginAngleUpDown.Minimum = -360
            Me.m_BeginAngleUpDown.Value = Me.m_RadialGauge.BeginAngle.ToDegrees()
            AddHandler Me.m_BeginAngleUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnBeginAngleUpDownValueChanged)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Begin Angle:", Me.m_BeginAngleUpDown, True))
            Me.m_SweepAngleUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_SweepAngleUpDown.Maximum = 360
            Me.m_SweepAngleUpDown.Minimum = -360
            Me.m_SweepAngleUpDown.Value = Me.m_RadialGauge.SweepAngle.ToDegrees()
            AddHandler Me.m_SweepAngleUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSweepAngleUpDownValueChanged)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Sweep Angle:", Me.m_SweepAngleUpDown, True))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to create range and needle gauge indicators.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnValueIndicatorWidthUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ValueIndicator.Width = Me.m_ValueIndicatorWidthUpDown.Value
        End Sub

        Private Sub OnValueIndicatorOffsetFromCenterUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ValueIndicator.OffsetFromCenter = Me.m_ValueIndicatorOffsetFromCenterUpDown.Value
        End Sub

        Private Sub OnValueIndicatorShapeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ValueIndicator.Shape = CType(Me.m_ValueIndicatorShapeComboBox.SelectedIndex, Nevron.Nov.Chart.ENNeedleShape)
        End Sub

        Private Sub OnValueIndicatorUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ValueIndicator.Value = Me.m_ValueIndicatorUpDown.Value
        End Sub

        Private Sub OnRangeIndicatorValueUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RangeIndicator.Value = Me.m_RangeIndicatorValueUpDown.Value
        End Sub

        Private Sub OnRangeIndicatorOriginUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RangeIndicator.Origin = Me.m_RangeIndicatorOriginUpDown.Value
        End Sub

        Private Sub OnSweepAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(Me.m_SweepAngleUpDown.Value, Nevron.Nov.NUnit.Degree)
        End Sub

        Private Sub OnBeginAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RadialGauge.BeginAngle = New Nevron.Nov.NAngle(Me.m_BeginAngleUpDown.Value, Nevron.Nov.NUnit.Degree)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RadialGauge As Nevron.Nov.Chart.NRadialGauge
        Private m_RangeIndicator As Nevron.Nov.Chart.NRangeIndicator
        Private m_ValueIndicator As Nevron.Nov.Chart.NNeedleValueIndicator
        Private m_RangeIndicatorOriginModeComboBox As Nevron.Nov.UI.NComboBox
        Private m_RangeIndicatorOriginUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_RangeIndicatorValueUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_ValueIndicatorUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_ValueIndicatorWidthUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_ValueIndicatorOffsetFromCenterUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_ValueIndicatorShapeComboBox As Nevron.Nov.UI.NComboBox
        Private m_BeginAngleUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_SweepAngleUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NRadialGaugeIndicatorsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultRadialGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(300, 300)

		#EndRegion
	End Class
End Namespace
