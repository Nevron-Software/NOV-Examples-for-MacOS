Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Gauge
	''' <summary>
	''' This example demonstrates how to add tooltips to gauge indicators
	''' </summary>
	Public Class NGaugeTooltipsExample
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
            Nevron.Nov.Examples.Gauge.NGaugeTooltipsExample.NGaugeTooltipsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NGaugeTooltipsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim controlStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(controlStack)
            Dim radialGauge As Nevron.Nov.Chart.NRadialGauge = New Nevron.Nov.Chart.NRadialGauge()
            radialGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NGaugeTooltipsExample.defaultRadialGaugeSize
            radialGauge.CapEffect = New Nevron.Nov.Chart.NGlassCapEffect()
            radialGauge.Dial = New Nevron.Nov.Chart.NDial(Nevron.Nov.Chart.ENDialShape.Circle, New Nevron.Nov.Chart.NEdgeDialRim())
            radialGauge.Dial.BackgroundFill = Nevron.Nov.Graphics.NAdvancedGradientFill.Create(Nevron.Nov.Graphics.ENAdvancedGradientColorScheme.Ocean2, 0)

			' configure scale
			Dim axis As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            radialGauge.Axes.Add(axis)
            Dim scale As Nevron.Nov.Chart.NLinearScale = TryCast(axis.Scale, Nevron.Nov.Chart.NLinearScale)
            scale.SetPredefinedScale(Nevron.Nov.Chart.ENPredefinedScaleStyle.PresentationNoStroke)
            scale.Labels.OverlapResolveLayouts = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.Chart.ENLevelLabelsLayout)()
            scale.MinorTickCount = 3
            scale.Ruler.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.White, 0.4F))
            scale.OuterMajorTicks.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Orange)
            scale.Labels.Style.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 12.0, Nevron.Nov.Graphics.ENFontStyle.Bold Or Nevron.Nov.Graphics.ENFontStyle.Italic)
            scale.Labels.Style.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            Me.m_Axis = CType(radialGauge.Axes(0), Nevron.Nov.Chart.NGaugeAxis)
            controlStack.Add(radialGauge)
            Me.m_Indicator1 = New Nevron.Nov.Chart.NRangeIndicator()
            Me.m_Indicator1.Value = 50
            Me.m_Indicator1.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightBlue)
            Me.m_Indicator1.Stroke.Color = Nevron.Nov.Graphics.NColor.DarkBlue
            Me.m_Indicator1.EndWidth = 20
            Me.m_Indicator1.AllowDragging = True
            radialGauge.Indicators.Add(Me.m_Indicator1)
            Me.m_Indicator2 = New Nevron.Nov.Chart.NNeedleValueIndicator()
            Me.m_Indicator2.Value = 79
            Me.m_Indicator2.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Horizontal, Nevron.Nov.Graphics.ENGradientVariant.Variant1, Nevron.Nov.Graphics.NColor.White, Nevron.Nov.Graphics.NColor.Red)
            Me.m_Indicator2.Stroke.Color = Nevron.Nov.Graphics.NColor.Red
            Me.m_Indicator2.AllowDragging = True
            radialGauge.Indicators.Add(Me.m_Indicator2)
            Me.m_Indicator3 = New Nevron.Nov.Chart.NMarkerValueIndicator()
            Me.m_Indicator3.Value = 90
            Me.m_Indicator3.AllowDragging = True
            radialGauge.Indicators.Add(Me.m_Indicator3)
            radialGauge.SweepAngle = New Nevron.Nov.NAngle(270.0, Nevron.Nov.NUnit.Degree)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))
            Me.m_RangeTooltipTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_RangeTooltipTextBox.Text = "Range Tooltip"
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Range Tooltip:", Me.m_RangeTooltipTextBox, True))
            Me.m_NeedleTooltipTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_NeedleTooltipTextBox.Text = "Needle Tooltip"
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Needle Tooltip:", Me.m_NeedleTooltipTextBox, True))
            Me.m_MarkerTooltipTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_MarkerTooltipTextBox.Text = "Marker Tooltip"
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Marker Tooltip:", Me.m_MarkerTooltipTextBox, True))
            Me.m_ScaleTooltipTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_ScaleTooltipTextBox.Text = "Scale Tooltip"
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Scale Tooltip:", Me.m_ScaleTooltipTextBox, True))
            AddHandler Me.m_RangeTooltipTextBox.TextChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.UpdateTooltips)
            AddHandler Me.m_NeedleTooltipTextBox.TextChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.UpdateTooltips)
            AddHandler Me.m_MarkerTooltipTextBox.TextChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.UpdateTooltips)
            AddHandler Me.m_ScaleTooltipTextBox.TextChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.UpdateTooltips)
            Me.UpdateTooltips(Nothing)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to assign tooltips to different gauge elements.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub UpdateTooltips(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Me.m_Axis Is Nothing Then Return
            Me.m_Indicator1.Tooltip = New Nevron.Nov.UI.NTooltip(Me.m_RangeTooltipTextBox.Text)
            Me.m_Indicator2.Tooltip = New Nevron.Nov.UI.NTooltip(Me.m_NeedleTooltipTextBox.Text)
            Me.m_Indicator3.Tooltip = New Nevron.Nov.UI.NTooltip(Me.m_MarkerTooltipTextBox.Text)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Indicator1 As Nevron.Nov.Chart.NRangeIndicator
        Private m_Indicator2 As Nevron.Nov.Chart.NNeedleValueIndicator
        Private m_Indicator3 As Nevron.Nov.Chart.NMarkerValueIndicator
        Private m_Axis As Nevron.Nov.Chart.NGaugeAxis
        Private m_RangeTooltipTextBox As Nevron.Nov.UI.NTextBox
        Private m_NeedleTooltipTextBox As Nevron.Nov.UI.NTextBox
        Private m_MarkerTooltipTextBox As Nevron.Nov.UI.NTextBox
        Private m_ScaleTooltipTextBox As Nevron.Nov.UI.NTextBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NGaugeTooltipsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultRadialGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(300, 300)

		#EndRegion
	End Class
End Namespace
