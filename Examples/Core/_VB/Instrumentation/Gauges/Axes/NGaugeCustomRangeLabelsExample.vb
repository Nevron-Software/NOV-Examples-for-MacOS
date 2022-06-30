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
	Public Class NGaugeCustomRangeLabelsExample
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
            Nevron.Nov.Examples.Gauge.NGaugeCustomRangeLabelsExample.NGaugeCustomRangeLabelsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NGaugeCustomRangeLabelsExample), NExampleBaseSchema)
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
            Me.m_LinearGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NGaugeCustomRangeLabelsExample.defaultLinearVerticalGaugeSize
            Me.m_LinearGauge.CapEffect = New Nevron.Nov.Chart.NGelCapEffect()
            Me.m_LinearGauge.Border = Me.CreateBorder()
            Me.m_LinearGauge.Padding = New Nevron.Nov.Graphics.NMargins(20)
            Me.m_LinearGauge.BorderThickness = New Nevron.Nov.Graphics.NMargins(6)
            controlStack.Add(Me.m_LinearGauge)

			' create the background panel
			Dim advGradient As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
            advGradient.BackgroundColor = Nevron.Nov.Graphics.NColor.Black
            advGradient.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.LightGray, New Nevron.Nov.NAngle(10, Nevron.Nov.NUnit.Degree), 0.1F, 0, 1.0F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
            Me.m_LinearGauge.BackgroundFill = advGradient
            Me.m_LinearGauge.Axes.Clear()
            Dim axis As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            Me.m_LinearGauge.Axes.Add(axis)
            axis.Anchor = New Nevron.Nov.Chart.NModelGaugeAxisAnchor(24.0, Nevron.Nov.ENVerticalAlignment.Center, Nevron.Nov.Chart.ENScaleOrientation.Left)
            Me.ConfigureScale(CType(axis.Scale, Nevron.Nov.Chart.NLinearScale))

			' add some indicators
			Me.m_LinearGauge.Indicators.Add(New Nevron.Nov.Chart.NMarkerValueIndicator(60))

			' create the radial gauge
			Me.m_RadialGauge = New Nevron.Nov.Chart.NRadialGauge()
            controlStack.Add(Me.m_RadialGauge)
            Me.m_RadialGauge.CapEffect = New Nevron.Nov.Chart.NGlassCapEffect()
            Me.m_RadialGauge.Dial = New Nevron.Nov.Chart.NDial(Nevron.Nov.Chart.ENDialShape.Circle, New Nevron.Nov.Chart.NEdgeDialRim())

			' set some background
			advGradient = New Nevron.Nov.Graphics.NAdvancedGradientFill()
            advGradient.BackgroundColor = Nevron.Nov.Graphics.NColor.Black
            advGradient.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.LightGray, New Nevron.Nov.NAngle(10, Nevron.Nov.NUnit.Degree), 0.1F, 0, 1.0F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
            Me.m_RadialGauge.Dial.BackgroundFill = advGradient
            Me.m_RadialGauge.CapEffect = New Nevron.Nov.Chart.NGlassCapEffect(Nevron.Nov.Chart.ENCapEffectShape.Ellipse)

			' create the radial gauge
			Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(270, Nevron.Nov.NUnit.Degree)
            Me.m_RadialGauge.BeginAngle = New Nevron.Nov.NAngle(-90, Nevron.Nov.NUnit.Degree)

			' FIX remove
			axis.Scale.Title.Text = "Axis Title"
            Dim scale As Nevron.Nov.Chart.NStandardScale = TryCast(axis.Scale, Nevron.Nov.Chart.NStandardScale)
            scale.MajorTickMode = Nevron.Nov.Chart.ENMajorTickMode.AutoMinDistance
            scale.MinTickDistance = 50

			' configure the axis
			Me.m_RadialGauge.Axes.Clear()
            axis = New Nevron.Nov.Chart.NGaugeAxis()
            axis.Range = New Nevron.Nov.Graphics.NRange(0, 100)
            axis.Anchor.ScaleOrientation = Nevron.Nov.Chart.ENScaleOrientation.Right
            axis.Anchor = New Nevron.Nov.Chart.NDockGaugeAxisAnchor(Nevron.Nov.Chart.ENGaugeAxisDockZone.Top, True, Nevron.Nov.Chart.ENScaleOrientation.Right, 0, 100)
            Me.m_RadialGauge.Axes.Add(axis)
            Me.ConfigureScale(CType(axis.Scale, Nevron.Nov.Chart.NLinearScale))

			' add some indicators
			Dim needle As Nevron.Nov.Chart.NNeedleValueIndicator = New Nevron.Nov.Chart.NNeedleValueIndicator(60)
            needle.OffsetOriginMode = Nevron.Nov.Chart.ENIndicatorOffsetOriginMode.ScaleMiddle
            needle.OffsetFromScale = 15
            Me.m_RadialGauge.Indicators.Add(needle)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))

			' begin angle scroll
			Me.m_BeginAngleScrollBar = New Nevron.Nov.UI.NHScrollBar()
            Me.m_BeginAngleScrollBar.Minimum = -360
            Me.m_BeginAngleScrollBar.Maximum = 360
            Me.m_BeginAngleScrollBar.Value = Me.m_RadialGauge.BeginAngle.ToDegrees()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Begin Angle:", Me.m_BeginAngleScrollBar))

			' sweep angle scroll
			Me.m_SweepAngleScrollBar = New Nevron.Nov.UI.NHScrollBar()
            Me.m_SweepAngleScrollBar.Minimum = -360
            Me.m_SweepAngleScrollBar.Maximum = 360
            Me.m_SweepAngleScrollBar.Value = Me.m_RadialGauge.SweepAngle.ToDegrees()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Sweep Angle:", Me.m_SweepAngleScrollBar))

			' show max check box
			Me.m_ShowMinRangeCheckBox = New Nevron.Nov.UI.NCheckBox("Show Min Range")
            Me.m_ShowMinRangeCheckBox.Checked = True
            propertyStack.Add(Me.m_ShowMinRangeCheckBox)

			' show min check box
			Me.m_ShowMaxRangeCheckBox = New Nevron.Nov.UI.NCheckBox("Show Max Range")
            Me.m_ShowMaxRangeCheckBox.Checked = True
            propertyStack.Add(Me.m_ShowMaxRangeCheckBox)
            AddHandler Me.m_BeginAngleScrollBar.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnBeginAngleScrollBarValueChanged)
            AddHandler Me.m_SweepAngleScrollBar.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSweepAngleScrollBarValueChanged)
            AddHandler Me.m_ShowMinRangeCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnShowMinRangeCheckBoxCheckedChanged)
            AddHandler Me.m_ShowMaxRangeCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnShowMaxRangeCheckBoxCheckedChanged)
            Me.UpdateAxisRanges()
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to create range labels on the gauge scale.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub ConfigureScale(ByVal scale As Nevron.Nov.Chart.NLinearScale)
            scale.SetPredefinedScale(Nevron.Nov.Chart.ENPredefinedScaleStyle.PresentationNoStroke)
            scale.Labels.OverlapResolveLayouts = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.Chart.ENLevelLabelsLayout)()
            scale.MinorTickCount = 3
            scale.Ruler.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.White, 0.4F))
            scale.OuterMajorTicks.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Orange)
            scale.Labels.Style.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 10.0, Nevron.Nov.Graphics.ENFontStyle.Bold)
            scale.Labels.Style.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
        End Sub

        Private Sub UpdateAxisRanges()
            Dim linearGaugeScale As Nevron.Nov.Chart.NLinearScale = TryCast(CType(Me.m_LinearGauge.Axes(CInt((0))), Nevron.Nov.Chart.NGaugeAxis).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim radialGaugeScale As Nevron.Nov.Chart.NLinearScale = TryCast(CType(Me.m_RadialGauge.Axes(CInt((0))), Nevron.Nov.Chart.NGaugeAxis).Scale, Nevron.Nov.Chart.NLinearScale)
            linearGaugeScale.CustomLabels.Clear()
            linearGaugeScale.Sections.Clear()
            radialGaugeScale.CustomLabels.Clear()
            radialGaugeScale.Sections.Clear()

            If Me.m_ShowMinRangeCheckBox.Checked Then
                Me.ApplyScaleSectionToAxis(linearGaugeScale, "Min", New Nevron.Nov.Graphics.NRange(0, 20), Nevron.Nov.Graphics.NColor.LightBlue)
                Me.ApplyScaleSectionToAxis(radialGaugeScale, "Min", New Nevron.Nov.Graphics.NRange(0, 20), Nevron.Nov.Graphics.NColor.LightBlue)
            End If

            If Me.m_ShowMaxRangeCheckBox.Checked Then
                Me.ApplyScaleSectionToAxis(linearGaugeScale, "Max", New Nevron.Nov.Graphics.NRange(80, 100), Nevron.Nov.Graphics.NColor.Red)
                Me.ApplyScaleSectionToAxis(radialGaugeScale, "Max", New Nevron.Nov.Graphics.NRange(80, 100), Nevron.Nov.Graphics.NColor.Red)
            End If
        End Sub

        Private Sub ApplyScaleSectionToAxis(ByVal scale As Nevron.Nov.Chart.NLinearScale, ByVal text As String, ByVal range As Nevron.Nov.Graphics.NRange, ByVal color As Nevron.Nov.Graphics.NColor)
            Dim scaleSection As Nevron.Nov.Chart.NScaleSection = New Nevron.Nov.Chart.NScaleSection()
            scaleSection.Range = range
            scaleSection.LabelTextStyle = New Nevron.Nov.UI.NTextStyle()
            scaleSection.LabelTextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(color)
            scaleSection.LabelTextStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 10, Nevron.Nov.Graphics.ENFontStyle.Bold Or Nevron.Nov.Graphics.ENFontStyle.Italic)
            scaleSection.MajorTickStroke = New Nevron.Nov.Graphics.NStroke(color)
            scale.Sections.Add(scaleSection)
            Dim rangeLabel As Nevron.Nov.Chart.NCustomRangeLabel = New Nevron.Nov.Chart.NCustomRangeLabel(range, text)
            rangeLabel.LabelStyle.AlwaysInsideScale = False
            rangeLabel.LabelStyle.VisibilityMode = Nevron.Nov.Chart.ENScaleLabelVisibilityMode.TextInRuler
            rangeLabel.LabelStyle.Stroke.Color = color
            rangeLabel.LabelStyle.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            rangeLabel.LabelStyle.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 0)
            rangeLabel.LabelStyle.TickMode = Nevron.Nov.Chart.ENRangeLabelTickMode.Center
            scale.CustomLabels.Add(rangeLabel)
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnShowMaxRangeCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.UpdateAxisRanges()
        End Sub

        Private Sub OnShowMinRangeCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.UpdateAxisRanges()
        End Sub

        Private Sub OnSweepAngleScrollBarValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(Me.m_SweepAngleScrollBar.Value, Nevron.Nov.NUnit.Degree)
        End Sub

        Private Sub OnBeginAngleScrollBarValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RadialGauge.BeginAngle = New Nevron.Nov.NAngle(Me.m_BeginAngleScrollBar.Value, Nevron.Nov.NUnit.Degree)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RadialGauge As Nevron.Nov.Chart.NRadialGauge
        Private m_LinearGauge As Nevron.Nov.Chart.NLinearGauge
        Private m_BeginAngleScrollBar As Nevron.Nov.UI.NHScrollBar
        Private m_SweepAngleScrollBar As Nevron.Nov.UI.NHScrollBar
        Private m_ShowMinRangeCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_ShowMaxRangeCheckBox As Nevron.Nov.UI.NCheckBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NGaugeCustomRangeLabelsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Protected Function CreateBorder() As Nevron.Nov.UI.NBorder
            Return Nevron.Nov.UI.NBorder.CreateThreeColorBorder(Nevron.Nov.Graphics.NColor.LightGray, Nevron.Nov.Graphics.NColor.White, Nevron.Nov.Graphics.NColor.DarkGray, 10, 10)
        End Function

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultLinearVerticalGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(100, 300)

		#EndRegion
	End Class
End Namespace
