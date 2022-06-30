Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Gauge
	''' <summary>
	''' This example demonstrates how to control the size of the gauge axes
	''' </summary>
	Public Class NAxisSizeExample
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
            Nevron.Nov.Examples.Gauge.NAxisSizeExample.NAxisSizeExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NAxisSizeExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim controlStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(controlStack)

			' create the radial gauge
			Me.m_RadialGauge = New Nevron.Nov.Chart.NRadialGauge()
            Me.m_RadialGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NAxisSizeExample.defaultRadialGaugeSize
            controlStack.Add(Me.m_RadialGauge)
            Me.m_RadialGauge.Dial = New Nevron.Nov.Chart.NDial(Nevron.Nov.Chart.ENDialShape.CutCircle, New Nevron.Nov.Chart.NEdgeDialRim())
            Me.m_RadialGauge.Dial.BackgroundFill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.DarkGray, Nevron.Nov.Graphics.NColor.Black)
            Dim gelEffect As Nevron.Nov.Chart.NGelCapEffect = New Nevron.Nov.Chart.NGelCapEffect(Nevron.Nov.Chart.ENCapEffectShape.Ellipse)
            gelEffect.Margins = New Nevron.Nov.Graphics.NMargins(0, 0, 0, 0.5)
            Me.m_RadialGauge.Axes.Clear()

			' create the first axis
			Dim axis1 As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            axis1.Anchor = New Nevron.Nov.Chart.NDockGaugeAxisAnchor(Nevron.Nov.Chart.ENGaugeAxisDockZone.Top, True, 0, 70)
            Dim scale1 As Nevron.Nov.Chart.NStandardScale = CType(axis1.Scale, Nevron.Nov.Chart.NStandardScale)
            scale1.SetPredefinedScale(Nevron.Nov.Chart.ENPredefinedScaleStyle.PresentationNoStroke)
            scale1.MinorTickCount = 3
            scale1.Ruler.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.White, 0.4F))
            scale1.OuterMajorTicks.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Orange)
            scale1.Labels.Style.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 12, Nevron.Nov.Graphics.ENFontStyle.Bold)
            scale1.Labels.Style.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            scale1.Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 0)
            Me.m_RadialGauge.Axes.Add(axis1)

			' create the second axis
			Dim axis2 As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            axis2.Anchor = New Nevron.Nov.Chart.NDockGaugeAxisAnchor(Nevron.Nov.Chart.ENGaugeAxisDockZone.Top, False, 75, 95)
            Dim scale2 As Nevron.Nov.Chart.NStandardScale = CType(axis2.Scale, Nevron.Nov.Chart.NStandardScale)
            scale2.SetPredefinedScale(Nevron.Nov.Chart.ENPredefinedScaleStyle.PresentationNoStroke)
            scale2.MinorTickCount = 3
            scale2.Ruler.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.White, 0.4F))
            scale2.OuterMajorTicks.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Blue)
            scale2.Labels.Style.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 12, Nevron.Nov.Graphics.ENFontStyle.Bold)
            scale2.Labels.Style.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            scale2.Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 0)
            Me.m_RadialGauge.Axes.Add(axis2)

			' add indicators
			Dim rangeIndicator As Nevron.Nov.Chart.NRangeIndicator = New Nevron.Nov.Chart.NRangeIndicator()
            rangeIndicator.Value = 50
            rangeIndicator.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.Orange, Nevron.Nov.Graphics.NColor.Red)
            rangeIndicator.Stroke.Width = 0
            rangeIndicator.OffsetFromScale = 3
            rangeIndicator.BeginWidth = 6
            rangeIndicator.EndWidth = 12
            Me.m_RadialGauge.Indicators.Add(rangeIndicator)
            Dim needleValueIndicator1 As Nevron.Nov.Chart.NNeedleValueIndicator = New Nevron.Nov.Chart.NNeedleValueIndicator()
            needleValueIndicator1.Value = 79
'			needleValueIndicator1.Shape.FillStyle = new NGradientFillStyle(GradientStyle.Vertical, GradientVariant.Variant2, Color.White, Color.Red);
'			needleValueIndicator1.Shape.StrokeStyle.Color = Color.Red;
			needleValueIndicator1.ScaleAxis = axis1
            needleValueIndicator1.OffsetFromScale = 2
            Me.m_RadialGauge.Indicators.Add(needleValueIndicator1)
            Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(360, Nevron.Nov.NUnit.Degree)
            Dim needleValueIndicator2 As Nevron.Nov.Chart.NNeedleValueIndicator = New Nevron.Nov.Chart.NNeedleValueIndicator()
            needleValueIndicator2.Value = 79
'			needleValueIndicator2.Shape.FillStyle = new NGradientFillStyle(GradientStyle.Vertical, GradientVariant.Variant2, Color.White, Color.Blue);
'			needleValueIndicator2.Shape.StrokeStyle.Color = Color.Blue;
			needleValueIndicator2.ScaleAxis = axis2
            needleValueIndicator2.OffsetFromScale = 2
            Me.m_RadialGauge.Indicators.Add(needleValueIndicator2)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))
            Me.m_ScrollBar = New Nevron.Nov.UI.NHScrollBar()
            Me.m_ScrollBar.Value = 70.0
            AddHandler Me.m_ScrollBar.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnScrollBarValueChanged)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Percent:", Me.m_ScrollBar, True))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how use the begin and end percent properties of the anchor in order to change the gauge axis size.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnScrollBarValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim axis1 As Nevron.Nov.Chart.NGaugeAxis = CType(Me.m_RadialGauge.Axes(0), Nevron.Nov.Chart.NGaugeAxis)
            Dim axis2 As Nevron.Nov.Chart.NGaugeAxis = CType(Me.m_RadialGauge.Axes(1), Nevron.Nov.Chart.NGaugeAxis)
            axis1.Anchor = New Nevron.Nov.Chart.NDockGaugeAxisAnchor(Nevron.Nov.Chart.ENGaugeAxisDockZone.Top, True, 0, CSng((Me.m_ScrollBar.Value - 5)))
            axis2.Anchor = New Nevron.Nov.Chart.NDockGaugeAxisAnchor(Nevron.Nov.Chart.ENGaugeAxisDockZone.Top, False, CSng(Me.m_ScrollBar.Value), 95)
'			 RedAxisTextBox.Text = m_ScrollBar.Value.ToString();
		End Sub

		#EndRegion

		#Region"Fields"

		Private m_RadialGauge As Nevron.Nov.Chart.NRadialGauge
        Private m_ScrollBar As Nevron.Nov.UI.NHScrollBar

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisSizeExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultRadialGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(300, 300)

		#EndRegion
	End Class
End Namespace
