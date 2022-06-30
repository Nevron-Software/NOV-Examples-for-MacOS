Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Gauge
    ''' <summary>
	''' This example demonstrates how to control the size of the gauge axes
    ''' </summary>
	Public Class NAxisDockingExample
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
            Nevron.Nov.Examples.Gauge.NAxisDockingExample.NAxisDockingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NAxisDockingExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim controlStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            controlStack.Padding = New Nevron.Nov.Graphics.NMargins(20)
            stack.Add(controlStack)
            Me.m_LinearGauge = New Nevron.Nov.Chart.NLinearGauge()
            Me.m_LinearGauge.Padding = New Nevron.Nov.Graphics.NMargins(30)
            Me.m_LinearGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NAxisDockingExample.defaultLinearHorizontalGaugeSize
            controlStack.Add(Me.m_LinearGauge)
            Dim axis As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            Me.m_LinearGauge.Axes.Add(axis)
            axis.Anchor = New Nevron.Nov.Chart.NDockGaugeAxisAnchor(Nevron.Nov.Chart.ENGaugeAxisDockZone.Top, True, Nevron.Nov.Chart.ENScaleOrientation.Left, 0, 100)
            axis = New Nevron.Nov.Chart.NGaugeAxis()
            Me.m_LinearGauge.Axes.Add(axis)
            axis.Anchor = New Nevron.Nov.Chart.NDockGaugeAxisAnchor(Nevron.Nov.Chart.ENGaugeAxisDockZone.Top, True, Nevron.Nov.Chart.ENScaleOrientation.Left, 0, 50)

            ' create the radial gauge
			Me.m_RadialGauge = New Nevron.Nov.Chart.NRadialGauge()
            Me.m_RadialGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NAxisDockingExample.defaultRadialGaugeSize
            Me.m_RadialGauge.NeedleCap.Visible = False
            controlStack.Add(Me.m_RadialGauge)

            ' create the radial gauge
            Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(270, Nevron.Nov.NUnit.Degree)
            Me.m_RadialGauge.BeginAngle = New Nevron.Nov.NAngle(-90, Nevron.Nov.NUnit.Degree)
            Me.m_RadialGauge.PreferredHeight = 400

            ' configure the axis
            axis = New Nevron.Nov.Chart.NGaugeAxis()
            Me.m_RadialGauge.Axes.Add(axis)
            axis.Range = New Nevron.Nov.Graphics.NRange(0, 100)
            axis.Anchor = New Nevron.Nov.Chart.NDockGaugeAxisAnchor(Nevron.Nov.Chart.ENGaugeAxisDockZone.Top, True, Nevron.Nov.Chart.ENScaleOrientation.Left, 0.0F, 100.0F)

			' configure the axis
			axis = New Nevron.Nov.Chart.NGaugeAxis()
            Me.m_RadialGauge.Axes.Add(axis)
            axis.Range = New Nevron.Nov.Graphics.NRange(0, 100)
            axis.Anchor = New Nevron.Nov.Chart.NDockGaugeAxisAnchor(Nevron.Nov.Chart.ENGaugeAxisDockZone.Top, True, Nevron.Nov.Chart.ENScaleOrientation.Left, 0.0F, 50.0F)
            Dim indicator As Nevron.Nov.Chart.NNeedleValueIndicator = New Nevron.Nov.Chart.NNeedleValueIndicator()
            indicator.ScaleAxis = axis
            indicator.OffsetFromScale = 20
            Me.m_RadialGauge.Indicators.Add(indicator)
            Dim markerValueIndicator As Nevron.Nov.Chart.NMarkerValueIndicator = New Nevron.Nov.Chart.NMarkerValueIndicator(10)
            markerValueIndicator.Shape = Nevron.Nov.Chart.ENScaleValueMarkerShape.Bar
            Me.m_RadialGauge.Indicators.Add(New Nevron.Nov.Chart.NMarkerValueIndicator(10))
            Me.m_RadialGauge.Indicators.Add(New Nevron.Nov.Chart.NMarkerValueIndicator(90))
            Dim needle As Nevron.Nov.Chart.NNeedleValueIndicator = New Nevron.Nov.Chart.NNeedleValueIndicator()
            needle.Value = 10
            needle.Shape = Nevron.Nov.Chart.ENNeedleShape.Needle4
            needle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkGreen)
            needle.Stroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkGreen)

'			radialGauge.Indicators.Add(needle);

			markerValueIndicator.Width = 20
            markerValueIndicator.Height = 20
            markerValueIndicator.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkGreen)
            markerValueIndicator.Stroke = New Nevron.Nov.Graphics.NStroke(1.0, Nevron.Nov.Graphics.NColor.DarkGreen)
            Return stack
        End Function
		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to control the gauge axis position using gauge axis dock anchors.</p>"
        End Function

		#EndRegion

		#Region"Fields"

		Private m_RadialGauge As Nevron.Nov.Chart.NRadialGauge
        Private m_LinearGauge As Nevron.Nov.Chart.NLinearGauge

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisDockingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultLinearHorizontalGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(300, 100)
        Private Shared ReadOnly defaultRadialGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(300, 300)

		#EndRegion
	End Class
End Namespace
