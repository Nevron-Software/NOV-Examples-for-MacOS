Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Gauge
	''' <summary>
	''' This example demonstrates how to configure the gauge so that the user can drag gauge indicators
	''' </summary>
	Public Class NDraggingGaugeIndicatorsExample
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
            Nevron.Nov.Examples.Gauge.NDraggingGaugeIndicatorsExample.NDraggingGaugeIndicatorsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NDraggingGaugeIndicatorsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim controlStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(controlStack)

			' create the radial gauge
			Dim radialGauge As Nevron.Nov.Chart.NRadialGauge = New Nevron.Nov.Chart.NRadialGauge()
            controlStack.Add(radialGauge)
            radialGauge.Dial = New Nevron.Nov.Chart.NDial(Nevron.Nov.Chart.ENDialShape.Circle, New Nevron.Nov.Chart.NEdgeDialRim())
            radialGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NDraggingGaugeIndicatorsExample.defaultRadialGaugeSize
            radialGauge.Dial.BackgroundFill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.DarkGray, Nevron.Nov.Graphics.NColor.Black)
            radialGauge.CapEffect = New Nevron.Nov.Chart.NGlassCapEffect()

			' configure the axis
			Dim axis As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            radialGauge.Axes.Add(axis)
            Dim scale As Nevron.Nov.Chart.NStandardScale = CType(axis.Scale, Nevron.Nov.Chart.NStandardScale)
            scale.SetPredefinedScale(Nevron.Nov.Chart.ENPredefinedScaleStyle.Scientific)
            scale.Labels.Style.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            scale.Labels.Style.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Tinos", 10, Nevron.Nov.Graphics.ENFontStyle.Italic Or Nevron.Nov.Graphics.ENFontStyle.Bold)
            scale.OuterMajorTicks.Stroke.Color = Nevron.Nov.Graphics.NColor.White
            scale.OuterMajorTicks.Length = 6
            scale.OuterMinorTicks.Stroke.Color = Nevron.Nov.Graphics.NColor.White
            scale.OuterMinorTicks.Length = 4
            scale.Ruler.Stroke.Color = Nevron.Nov.Graphics.NColor.White
            scale.MinorTickCount = 4

			' add some indicators
			Me.m_RangeIndicator = New Nevron.Nov.Chart.NRangeIndicator()
            Me.m_RangeIndicator.Value = 50
            Me.m_RangeIndicator.Palette = New Nevron.Nov.Chart.NTwoColorPalette(Nevron.Nov.Graphics.NColor.DarkBlue, Nevron.Nov.Graphics.NColor.LightBlue)
            Me.m_RangeIndicator.Stroke = Nothing
            Me.m_RangeIndicator.EndWidth = 20
            Me.m_RangeIndicator.AllowDragging = True
            radialGauge.Indicators.Add(Me.m_RangeIndicator)
            Me.m_NeedleIndicator = New Nevron.Nov.Chart.NNeedleValueIndicator()
            Me.m_NeedleIndicator.Value = 79
            Me.m_NeedleIndicator.AllowDragging = True
            radialGauge.Indicators.Add(Me.m_NeedleIndicator)
            radialGauge.SweepAngle = New Nevron.Nov.NAngle(270, Nevron.Nov.NUnit.Degree)
            Me.m_MarkerIndicator = New Nevron.Nov.Chart.NMarkerValueIndicator()
            Me.m_MarkerIndicator.Value = 90
            Me.m_MarkerIndicator.AllowDragging = True
            Me.m_MarkerIndicator.OffsetOriginMode = Nevron.Nov.Chart.ENIndicatorOffsetOriginMode.ScaleEnd
            Me.m_MarkerIndicator.OffsetFromScale = 0.0
            radialGauge.Indicators.Add(Me.m_MarkerIndicator)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))
            Me.m_IndicatorSnapModeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_IndicatorSnapModeComboBox.FillFromArray(New String() {"None", "Ruler", "Major ticks", "Minor ticks", "Ruler Min/Max", "Numeric"})
            Me.m_IndicatorSnapModeComboBox.SelectedIndex = 0
            AddHandler Me.m_IndicatorSnapModeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUdpdateIndicatorValueSnapper)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Indicator Snap Mode:", Me.m_IndicatorSnapModeComboBox, True))
            Me.m_StepNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_StepNumericUpDown.Enabled = False
            Me.m_StepNumericUpDown.Value = 5.0
            AddHandler Me.m_StepNumericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUdpdateIndicatorValueSnapper)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Step:", Me.m_StepNumericUpDown, True))
            Me.m_AllowDraggingRangeIndicator = New Nevron.Nov.UI.NCheckBox("Allow Dragging Range")
            Me.m_AllowDraggingRangeIndicator.Checked = Me.m_RangeIndicator.AllowDragging
            AddHandler Me.m_AllowDraggingRangeIndicator.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnAllowDraggingRangeIndicator)
            propertyStack.Add(Me.m_AllowDraggingRangeIndicator)
            Me.m_AllowDraggingNeedleIndicator = New Nevron.Nov.UI.NCheckBox("Allow Dragging Needle")
            Me.m_AllowDraggingNeedleIndicator.Checked = Me.m_NeedleIndicator.AllowDragging
            AddHandler Me.m_AllowDraggingNeedleIndicator.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnAllowDraggingNeedleIndicator)
            propertyStack.Add(Me.m_AllowDraggingNeedleIndicator)
            Me.m_AllowDraggingMarkerIndicator = New Nevron.Nov.UI.NCheckBox("Allow Dragging Marker")
            Me.m_AllowDraggingMarkerIndicator.Checked = Me.m_MarkerIndicator.AllowDragging
            AddHandler Me.m_AllowDraggingMarkerIndicator.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnAllowDraggingMarkerIndicator)
            propertyStack.Add(Me.m_AllowDraggingMarkerIndicator)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to enable dragging of gauge indicators.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		''' <summary>
		''' Creates an indicator value snapper
		''' </summary>
		''' <returns></returns>
		Private Function CreateValueSnapper() As Nevron.Nov.Chart.NValueSnapper
            Select Case Me.m_IndicatorSnapModeComboBox.SelectedIndex
                Case 0 'None, snapping is disabled
                    Return Nothing
                Case 1 ' Ruler, values are constrained to the ruler begin and end values.
                    Return New Nevron.Nov.Chart.NAxisRulerClampSnapper()
                Case 2 ' Major ticks, values are snapped to axis major ticks
                    Return New Nevron.Nov.Chart.NAxisMajorTickSnapper()
                Case 3 ' Minor ticks, values are snapped to axis minor ticks
                    Return New Nevron.Nov.Chart.NAxisMinorTickSnapper()
                Case 4 ' Ruler Min Max, values are snapped to the ruler min and max values
                    Return New Nevron.Nov.Chart.NAxisRulerMinMaxSnapper()
                Case 5
                    Return New Nevron.Nov.Chart.NNumericValueSnapper(0.0, Me.m_StepNumericUpDown.Value)
                Case Else
                    Return Nothing
            End Select
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnAllowDraggingMarkerIndicator(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_MarkerIndicator.AllowDragging = Me.m_AllowDraggingMarkerIndicator.Checked
        End Sub

        Private Sub OnAllowDraggingNeedleIndicator(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_NeedleIndicator.AllowDragging = Me.m_AllowDraggingNeedleIndicator.Checked
        End Sub

        Private Sub OnAllowDraggingRangeIndicator(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RangeIndicator.AllowDragging = Me.m_AllowDraggingRangeIndicator.Checked
        End Sub

        Private Sub OnUdpdateIndicatorValueSnapper(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RangeIndicator.ValueSnapper = Me.CreateValueSnapper()
            Me.m_NeedleIndicator.ValueSnapper = Me.CreateValueSnapper()
            Me.m_MarkerIndicator.ValueSnapper = Me.CreateValueSnapper()
        End Sub

        Private Sub OnAllowDragRangeIndicatorsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
			' m_Indicator1.AllowDragging = m_AllowDragRangeIndicatorsCheckBox.Checked;
		End Sub

		#EndRegion

		#Region"Fields"

		Private m_RangeIndicator As Nevron.Nov.Chart.NRangeIndicator
        Private m_NeedleIndicator As Nevron.Nov.Chart.NNeedleValueIndicator
        Private m_MarkerIndicator As Nevron.Nov.Chart.NMarkerValueIndicator
        Private m_StepNumericUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_IndicatorSnapModeComboBox As Nevron.Nov.UI.NComboBox
        Private m_AllowDraggingRangeIndicator As Nevron.Nov.UI.NCheckBox
        Private m_AllowDraggingNeedleIndicator As Nevron.Nov.UI.NCheckBox
        Private m_AllowDraggingMarkerIndicator As Nevron.Nov.UI.NCheckBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NDraggingGaugeIndicatorsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultRadialGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(300, 300)

		#EndRegion
	End Class
End Namespace
