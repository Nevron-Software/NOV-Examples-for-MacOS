Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Editors

Namespace Nevron.Nov.Examples.Gauge
	''' <summary>
	''' This example demonstrates how to control the size of the gauge axes
	''' </summary>
	Public Class NGaugeScaleLabelsOrientationExample
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
            Nevron.Nov.Examples.Gauge.NGaugeScaleLabelsOrientationExample.NGaugeScaleLabelsOrientationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NGaugeScaleLabelsOrientationExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim controlStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            controlStack.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            stack.Add(controlStack)
            Me.m_LinearGauge = New Nevron.Nov.Chart.NLinearGauge()
            Me.m_LinearGauge.Orientation = Nevron.Nov.Chart.ENLinearGaugeOrientation.Vertical
            Me.m_LinearGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NGaugeScaleLabelsOrientationExample.defaultLinearVerticalGaugeSize
            Me.m_LinearGauge.CapEffect = New Nevron.Nov.Chart.NGelCapEffect()
            Me.m_LinearGauge.Border = Nevron.Nov.Examples.Gauge.NGaugeScaleLabelsOrientationExample.CreateBorder()
            Me.m_LinearGauge.Padding = New Nevron.Nov.Graphics.NMargins(20)
            Me.m_LinearGauge.BorderThickness = New Nevron.Nov.Graphics.NMargins(6)
            controlStack.Add(Me.m_LinearGauge)

			' create the background panel
			Dim advGradient As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
            advGradient.BackgroundColor = Nevron.Nov.Graphics.NColor.Black
            advGradient.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.LightGray, New Nevron.Nov.NAngle(10, Nevron.Nov.NUnit.Degree), 0.1F, 0, 1.0F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
            Me.m_LinearGauge.BackgroundFill = advGradient
			'          FIX m_LinearGauge.BorderStyle = new NEdgeBorderStyle(BorderShape.RoundedRect);

			Dim axis As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            Me.m_LinearGauge.Axes.Add(axis)
            axis.Anchor = New Nevron.Nov.Chart.NModelGaugeAxisAnchor(10, Nevron.Nov.ENVerticalAlignment.Center, Nevron.Nov.Chart.ENScaleOrientation.Left)
            Me.ConfigureScale(CType(axis.Scale, Nevron.Nov.Chart.NLinearScale))

			' add some indicators
			Me.AddRangeIndicatorToGauge(Me.m_LinearGauge)
            Me.m_LinearGauge.Indicators.Add(New Nevron.Nov.Chart.NMarkerValueIndicator(60))

			' create the radial gauge
			Me.m_RadialGauge = New Nevron.Nov.Chart.NRadialGauge()
            Me.m_RadialGauge.CapEffect = New Nevron.Nov.Chart.NGlassCapEffect()
            Me.m_RadialGauge.Dial = New Nevron.Nov.Chart.NDial(Nevron.Nov.Chart.ENDialShape.Circle, New Nevron.Nov.Chart.NEdgeDialRim())
            controlStack.Add(Me.m_RadialGauge)

			' create the radial gauge
			Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(270, Nevron.Nov.NUnit.Degree)
            Me.m_RadialGauge.BeginAngle = New Nevron.Nov.NAngle(-90, Nevron.Nov.NUnit.Degree)

			' set some background
			advGradient = New Nevron.Nov.Graphics.NAdvancedGradientFill()
            advGradient.BackgroundColor = Nevron.Nov.Graphics.NColor.Black
            advGradient.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.White, New Nevron.Nov.NAngle(10, Nevron.Nov.NUnit.Degree), 0.1F, 0, 1.0F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
            Me.m_RadialGauge.Dial = New Nevron.Nov.Chart.NDial(Nevron.Nov.Chart.ENDialShape.Circle, New Nevron.Nov.Chart.NEdgeDialRim())
            Me.m_RadialGauge.Dial.BackgroundFill = advGradient

			' configure the axis
			axis = New Nevron.Nov.Chart.NGaugeAxis()
            Me.m_RadialGauge.Axes.Add(axis)
            axis.Range = New Nevron.Nov.Graphics.NRange(0, 100)
            axis.Anchor.ScaleOrientation = Nevron.Nov.Chart.ENScaleOrientation.Right
            axis.Anchor = New Nevron.Nov.Chart.NDockGaugeAxisAnchor(Nevron.Nov.Chart.ENGaugeAxisDockZone.Top, True, Nevron.Nov.Chart.ENScaleOrientation.Right, 0.0F, 100.0F)
            Me.ConfigureScale(CType(axis.Scale, Nevron.Nov.Chart.NLinearScale))

			' add some indicators
			Me.AddRangeIndicatorToGauge(Me.m_RadialGauge)
            Dim needle As Nevron.Nov.Chart.NNeedleValueIndicator = New Nevron.Nov.Chart.NNeedleValueIndicator(60)
            needle.OffsetOriginMode = Nevron.Nov.Chart.ENIndicatorOffsetOriginMode.ScaleMiddle
            needle.OffsetFromScale = 15.0
            Me.m_RadialGauge.Indicators.Add(needle)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))
            Me.m_AngleModeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_AngleModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENScaleLabelAngleMode)()
            AddHandler Me.m_AngleModeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.UpdateScaleLabelAngle)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Angle Mode:", Me.m_AngleModeComboBox, True))
            Me.m_CustomAngleNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CustomAngleNumericUpDown.Minimum = -360
            Me.m_CustomAngleNumericUpDown.Maximum = 360
            AddHandler Me.m_CustomAngleNumericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.UpdateScaleLabelAngle)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Custom Angle:", Me.m_CustomAngleNumericUpDown, True))
            Me.m_AllowTextFlipCheckBox = New Nevron.Nov.UI.NCheckBox("Allow Text to Flip")
            AddHandler Me.m_AllowTextFlipCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.UpdateScaleLabelAngle)
            propertyStack.Add(Me.m_AllowTextFlipCheckBox)
            Me.m_BeginAngleScrollBar = New Nevron.Nov.UI.NHScrollBar()
            Me.m_BeginAngleScrollBar.Minimum = -360
            Me.m_BeginAngleScrollBar.Maximum = 360
            Me.m_BeginAngleScrollBar.Value = Me.m_RadialGauge.BeginAngle.ToDegrees()
            AddHandler Me.m_BeginAngleScrollBar.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnBeginAngleScrollBarValueChanged)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Begin Angle:", Me.m_BeginAngleScrollBar, True))
            Me.m_SweepAngleScrollBar = New Nevron.Nov.UI.NHScrollBar()
            Me.m_SweepAngleScrollBar.Minimum = -360.0
            Me.m_SweepAngleScrollBar.Maximum = 360.0
            Me.m_SweepAngleScrollBar.Value = Me.m_RadialGauge.SweepAngle.ToDegrees()
            AddHandler Me.m_SweepAngleScrollBar.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSweepAngleScrollBarValueChanged)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Sweep Angle:", Me.m_SweepAngleScrollBar, True))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to control the Gauge scale labels orientation.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub UpdateScaleLabelAngle(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim angle As Nevron.Nov.Chart.NScaleLabelAngle = New Nevron.Nov.Chart.NScaleLabelAngle(CType(Me.m_AngleModeComboBox.SelectedIndex, Nevron.Nov.Chart.ENScaleLabelAngleMode), CSng(Me.m_CustomAngleNumericUpDown.Value), Me.m_AllowTextFlipCheckBox.Checked)

			' apply angle to radial gauge axis
			Dim axis As Nevron.Nov.Chart.NGaugeAxis = CType(Me.m_RadialGauge.Axes(0), Nevron.Nov.Chart.NGaugeAxis)
            Dim scale As Nevron.Nov.Chart.NLinearScale = CType(axis.Scale, Nevron.Nov.Chart.NLinearScale)
            scale.Labels.Style.Angle = angle

			' apply angle to linear gauge axis
			axis = CType(Me.m_LinearGauge.Axes(0), Nevron.Nov.Chart.NGaugeAxis)
            scale = CType(axis.Scale, Nevron.Nov.Chart.NLinearScale)
            scale.Labels.Style.Angle = angle
        End Sub

        Private Sub OnBeginAngleScrollBarValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RadialGauge.BeginAngle = New Nevron.Nov.NAngle(Me.m_BeginAngleScrollBar.Value, Nevron.Nov.NUnit.Degree)
        End Sub

        Private Sub OnSweepAngleScrollBarValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(Me.m_SweepAngleScrollBar.Value, Nevron.Nov.NUnit.Degree)
        End Sub

        Private Sub OnAngleModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(Me.m_SweepAngleScrollBar.Value, Nevron.Nov.NUnit.Degree)
        End Sub

		#EndRegion

		#Region"Implementation"

		''' <summary>
		''' 
		''' </summary>
		''' <paramname="scale"></param>
		Private Sub ConfigureScale(ByVal scale As Nevron.Nov.Chart.NLinearScale)
            scale.SetPredefinedScale(Nevron.Nov.Chart.ENPredefinedScaleStyle.PresentationNoStroke)
            scale.Labels.OverlapResolveLayouts = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.Chart.ENLevelLabelsLayout)()
            scale.MinorTickCount = 3
            scale.Ruler.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.White, 0.4F))
            scale.OuterMajorTicks.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Orange)
            scale.Labels.Style.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 12, Nevron.Nov.Graphics.ENFontStyle.Bold)
            scale.Labels.Style.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
        End Sub
		''' <summary>
		''' 
		''' </summary>
		''' <paramname="gauge"></param>
		Private Sub AddRangeIndicatorToGauge(ByVal gauge As Nevron.Nov.Chart.NGauge)
			' add some indicators
			Dim rangeIndicator As Nevron.Nov.Chart.NRangeIndicator = New Nevron.Nov.Chart.NRangeIndicator(New Nevron.Nov.Graphics.NRange(75, 100))
            rangeIndicator.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Red)
            rangeIndicator.Stroke.Width = 0.0
            rangeIndicator.BeginWidth = 5.0
            rangeIndicator.EndWidth = 10.0
            rangeIndicator.PaintOrder = Nevron.Nov.Chart.ENIndicatorPaintOrder.BeforeScale
            gauge.Indicators.Add(rangeIndicator)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RadialGauge As Nevron.Nov.Chart.NRadialGauge
        Private m_LinearGauge As Nevron.Nov.Chart.NLinearGauge
        Private m_AngleModeComboBox As Nevron.Nov.UI.NComboBox
        Private m_CustomAngleNumericUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_AllowTextFlipCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_BeginAngleScrollBar As Nevron.Nov.UI.NHScrollBar
        Private m_SweepAngleScrollBar As Nevron.Nov.UI.NHScrollBar


		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NGaugeScaleLabelsOrientationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateBorder() As Nevron.Nov.UI.NBorder
            Return Nevron.Nov.UI.NBorder.CreateThreeColorBorder(Nevron.Nov.Graphics.NColor.LightGray, Nevron.Nov.Graphics.NColor.White, Nevron.Nov.Graphics.NColor.DarkGray, 10, 10)
        End Function

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultLinearVerticalGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(100, 300)

		#EndRegion
	End Class
End Namespace
