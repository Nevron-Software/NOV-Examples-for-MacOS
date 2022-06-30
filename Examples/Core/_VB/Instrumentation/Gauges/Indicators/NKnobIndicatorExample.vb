Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Gauge
    ''' <summary>
	''' This example demonstrates how to add create a knob indicator
    ''' </summary>
	Public Class NKnobIndicatorExample
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
            Nevron.Nov.Examples.Gauge.NKnobIndicatorExample.NKnobIndicatorExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NKnobIndicatorExample), NExampleBaseSchema)
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

			'			radialGauge.PreferredSize = new NSize(0, 50);
			'			radialGauge.ContentAlignment = ContentAlignment.MiddleCenter;
			Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(270, Nevron.Nov.NUnit.Degree)
            Me.m_RadialGauge.BeginAngle = New Nevron.Nov.NAngle(-225, Nevron.Nov.NUnit.Degree)
            Me.m_RadialGauge.NeedleCap.Visible = False
            Me.m_RadialGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NKnobIndicatorExample.defaultRadialGaugeSize

			' configure scale
			Dim axis As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            Me.m_RadialGauge.Axes.Add(axis)
            Dim scale As Nevron.Nov.Chart.NStandardScale = CType(axis.Scale, Nevron.Nov.Chart.NStandardScale)
            scale.SetPredefinedScale(Nevron.Nov.Chart.ENPredefinedScaleStyle.PresentationNoStroke)
            scale.Labels.Style.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 12.0, Nevron.Nov.Graphics.ENFontStyle.Italic)
            scale.Labels.Style.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black)
            scale.Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 0.0)
            scale.MinorTickCount = 4
            scale.Ruler.Stroke.Width = 0
            scale.Ruler.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkGray)

			' create the knob indicator
			Me.m_KnobIndicator = New Nevron.Nov.Chart.NKnobIndicator()
            Me.m_KnobIndicator.OffsetFromScale = -3
            Me.m_KnobIndicator.AllowDragging = True

			' apply fill style to the marker
			Dim advancedGradientFill As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
            advancedGradientFill.BackgroundColor = Nevron.Nov.Graphics.NColor.Red
            advancedGradientFill.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.White, New Nevron.Nov.NAngle(20, Nevron.Nov.NUnit.Degree), 20, 0, 100, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
            Me.m_KnobIndicator.Fill = advancedGradientFill
            AddHandler Me.m_KnobIndicator.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnKnobValueChanged)
            Me.m_RadialGauge.Indicators.Add(Me.m_KnobIndicator)

			' create the numeric display
			Me.m_NumericDisplay = New Nevron.Nov.Chart.NNumericLedDisplay()
            Me.m_NumericDisplay.PreferredSize = New Nevron.Nov.Graphics.NSize(0, 60)
            Me.m_NumericDisplay.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black)
            Me.m_NumericDisplay.Border = Nevron.Nov.UI.NBorder.CreateSunken3DBorder(New Nevron.Nov.UI.NUIThemeColorMap(Nevron.Nov.UI.ENUIThemeScheme.WindowsClassic))
            Me.m_NumericDisplay.BorderThickness = New Nevron.Nov.Graphics.NMargins(6)
            Me.m_NumericDisplay.ContentAlignment = Nevron.Nov.ENContentAlignment.MiddleCenter
            Me.m_NumericDisplay.Margins = New Nevron.Nov.Graphics.NMargins(5)
            Me.m_NumericDisplay.Padding = New Nevron.Nov.Graphics.NMargins(5)
            Me.m_NumericDisplay.CapEffect = New Nevron.Nov.Chart.NGelCapEffect()
            controlStack.Add(Me.m_RadialGauge)
            controlStack.Add(Me.m_NumericDisplay)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))

			' marker properties
			Dim markerGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Marker")
            propertyStack.Add(markerGroupBox)
            Dim markerGroupBoxContent As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            markerGroupBox.Content = New Nevron.Nov.UI.NUniSizeBoxGroup(markerGroupBoxContent)

			' fill the marker shape combo
			Me.m_MarkerShapeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_MarkerShapeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENScaleValueMarkerShape)()
            Me.m_MarkerShapeComboBox.SelectedIndex = CInt(Me.m_KnobIndicator.Shape)
            AddHandler Me.m_MarkerShapeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnKnobAppearanceChanged)
            markerGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Shape:", Me.m_MarkerShapeComboBox, True))
            Me.m_MarkerOffsetUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_MarkerOffsetUpDown.Value = Me.m_KnobIndicator.OffsetFromScale
            AddHandler Me.m_MarkerOffsetUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnKnobAppearanceChanged)
            markerGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Offset:", Me.m_MarkerOffsetUpDown, True))
            Me.m_MarkerPaintOrderComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_MarkerPaintOrderComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENKnobMarkerPaintOrder)()
            Me.m_MarkerPaintOrderComboBox.SelectedIndex = CInt(Me.m_KnobIndicator.MarkerPaintOrder)
            AddHandler Me.m_MarkerPaintOrderComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnKnobAppearanceChanged)
            markerGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Paint Order:", Me.m_MarkerPaintOrderComboBox, True))

			' outer rim properties
			Dim outerRimGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Outer Rim")
            propertyStack.Add(outerRimGroupBox)
            Dim outerRimGroupBoxContent As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            outerRimGroupBox.Content = New Nevron.Nov.UI.NUniSizeBoxGroup(outerRimGroupBoxContent)
            Me.m_OuterRimPatternComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_OuterRimPatternComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENCircularRimPattern)()
            Me.m_OuterRimPatternComboBox.SelectedIndex = CInt(Me.m_KnobIndicator.OuterRim.Pattern)
            AddHandler Me.m_OuterRimPatternComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnKnobAppearanceChanged)
            outerRimGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Pattern", Me.m_OuterRimPatternComboBox, True))
            Me.m_OuterRimPatternRepeatCountUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_OuterRimPatternRepeatCountUpDown.Value = Me.m_KnobIndicator.OuterRim.PatternRepeatCount
            AddHandler Me.m_OuterRimPatternRepeatCountUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnKnobAppearanceChanged)
            outerRimGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Repeat Count:", Me.m_OuterRimPatternRepeatCountUpDown, True))
            Me.m_OuterRimRadiusOffsetUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_OuterRimRadiusOffsetUpDown.Value = Me.m_KnobIndicator.OuterRim.Offset
            AddHandler Me.m_OuterRimRadiusOffsetUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnKnobAppearanceChanged)
            outerRimGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Radius Offset:", Me.m_OuterRimRadiusOffsetUpDown, True))

			' inner rim properties
			Dim innerRimGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Inner Rim")
            propertyStack.Add(innerRimGroupBox)
            Dim innerRimGroupBoxContent As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            innerRimGroupBox.Content = New Nevron.Nov.UI.NUniSizeBoxGroup(innerRimGroupBoxContent)
            Me.m_InnerRimPatternComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_InnerRimPatternComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENCircularRimPattern)()
            Me.m_InnerRimPatternComboBox.SelectedIndex = CInt(Me.m_KnobIndicator.InnerRim.Pattern)
            AddHandler Me.m_InnerRimPatternComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnKnobAppearanceChanged)
            innerRimGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Pattern", Me.m_InnerRimPatternComboBox, True))
            Me.m_InnerRimPatternRepeatCountUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_InnerRimPatternRepeatCountUpDown.Value = Me.m_KnobIndicator.InnerRim.PatternRepeatCount
            AddHandler Me.m_InnerRimPatternRepeatCountUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnKnobAppearanceChanged)
            innerRimGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Repeat Count:", Me.m_InnerRimPatternRepeatCountUpDown, True))
            Me.m_InnerRimRadiusOffsetUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_InnerRimRadiusOffsetUpDown.Value = Me.m_KnobIndicator.InnerRim.Offset
            AddHandler Me.m_InnerRimRadiusOffsetUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnKnobAppearanceChanged)
            innerRimGroupBoxContent.Add(New Nevron.Nov.UI.NPairBox("Radius Offset:", Me.m_InnerRimRadiusOffsetUpDown, True))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates the properties of the knob indicator.</p>"
        End Function

		#EndRegion

		#Region"Implementation"
		
		#EndRegion

		#Region"Event Handlers"

		Private Sub OnKnobValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Me.m_RadialGauge Is Nothing Then Return
            Me.m_NumericDisplay.Value = CType(Me.m_RadialGauge.Indicators(CInt((0))), Nevron.Nov.Chart.NIndicator).Value
        End Sub

        Private Sub OnKnobAppearanceChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
			' update the knob marker shape
			Me.m_KnobIndicator.Shape = CType(Me.m_MarkerShapeComboBox.SelectedIndex, Nevron.Nov.Chart.ENScaleValueMarkerShape)
            Me.m_KnobIndicator.OffsetFromScale = Me.m_MarkerOffsetUpDown.Value
            Me.m_KnobIndicator.MarkerPaintOrder = CType(Me.m_MarkerPaintOrderComboBox.SelectedIndex, Nevron.Nov.Chart.ENKnobMarkerPaintOrder)

			 ' update the outer rim style
			Me.m_KnobIndicator.OuterRim.Pattern = CType(Me.m_OuterRimPatternComboBox.SelectedIndex, Nevron.Nov.Chart.ENCircularRimPattern)
            Me.m_KnobIndicator.OuterRim.PatternRepeatCount = CInt(Me.m_OuterRimPatternRepeatCountUpDown.Value)
            Me.m_KnobIndicator.OuterRim.Offset = Me.m_OuterRimRadiusOffsetUpDown.Value

			' update the inner rim style
			Me.m_KnobIndicator.InnerRim.Pattern = CType(Me.m_InnerRimPatternComboBox.SelectedIndex, Nevron.Nov.Chart.ENCircularRimPattern)
            Me.m_KnobIndicator.InnerRim.PatternRepeatCount = CInt(Me.m_InnerRimPatternRepeatCountUpDown.Value)
            Me.m_KnobIndicator.InnerRim.Offset = Me.m_InnerRimRadiusOffsetUpDown.Value
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RadialGauge As Nevron.Nov.Chart.NRadialGauge
        Private m_NumericDisplay As Nevron.Nov.Chart.NNumericLedDisplay
        Private m_KnobIndicator As Nevron.Nov.Chart.NKnobIndicator
        Private m_MarkerOffsetUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_MarkerPaintOrderComboBox As Nevron.Nov.UI.NComboBox
        Private m_MarkerShapeComboBox As Nevron.Nov.UI.NComboBox
        Private m_OuterRimPatternComboBox As Nevron.Nov.UI.NComboBox
        Private m_OuterRimPatternRepeatCountUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_OuterRimRadiusOffsetUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_InnerRimRadiusOffsetUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_InnerRimPatternRepeatCountUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_InnerRimPatternComboBox As Nevron.Nov.UI.NComboBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NKnobIndicatorExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultRadialGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(300, 300)

		#EndRegion
	End Class
End Namespace
