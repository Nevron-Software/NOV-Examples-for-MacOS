Imports System
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
	Public Class NScaleSectionsExample
        Inherits NExampleBase
        #Region"Constructors"

        ''' <summary>
        ''' Default constructor
        ''' </summary>
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Static constructor
        ''' </summary>
        Shared Sub New()
            Nevron.Nov.Examples.Gauge.NScaleSectionsExample.NScaleSectionsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NScaleSectionsExample), NExampleBaseSchema)
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
            controlStack.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            stack.Add(controlStack)
            Me.m_LinearGauge = New Nevron.Nov.Chart.NLinearGauge()
            Me.m_LinearGauge.Orientation = Nevron.Nov.Chart.ENLinearGaugeOrientation.Vertical
            Me.m_LinearGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NScaleSectionsExample.defaultLinearVerticalGaugeSize
            Me.m_LinearGauge.BackgroundFill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.DarkGray, Nevron.Nov.Graphics.NColor.Black)
            Me.m_LinearGauge.CapEffect = New Nevron.Nov.Chart.NGelCapEffect()
            Me.m_LinearGauge.Border = Me.CreateBorder()
            Me.m_LinearGauge.Padding = New Nevron.Nov.Graphics.NMargins(20)
            Me.m_LinearGauge.BorderThickness = New Nevron.Nov.Graphics.NMargins(6)
            controlStack.Add(Me.m_LinearGauge)
            Dim markerIndicator As Nevron.Nov.Chart.NMarkerValueIndicator = New Nevron.Nov.Chart.NMarkerValueIndicator()
            Me.m_LinearGauge.Indicators.Add(markerIndicator)
            Me.InitSections(Me.m_LinearGauge)

			' create the radial gauge
			Me.m_RadialGauge = New Nevron.Nov.Chart.NRadialGauge()
            Me.m_RadialGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NScaleSectionsExample.defaultRadialGaugeSize
            Dim dialRim As Nevron.Nov.Chart.NEdgeDialRim = New Nevron.Nov.Chart.NEdgeDialRim()
            dialRim.OuterBevelWidth = 2.0
            dialRim.InnerBevelWidth = 2.0
            dialRim.MiddleBevelWidth = 2.0
            Me.m_RadialGauge.Dial = New Nevron.Nov.Chart.NDial(Nevron.Nov.Chart.ENDialShape.CutCircle, dialRim)
            Me.m_RadialGauge.Dial.BackgroundFill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.DarkGray, Nevron.Nov.Graphics.NColor.Black)
            Me.m_RadialGauge.InnerRadius = 15
            Dim glassCapEffect As Nevron.Nov.Chart.NGlassCapEffect = New Nevron.Nov.Chart.NGlassCapEffect()
            glassCapEffect.LightDirection = New Nevron.Nov.NAngle(130, Nevron.Nov.NUnit.Degree)
            glassCapEffect.EdgeOffset = 0
            glassCapEffect.EdgeDepth = 0.30
            Me.m_RadialGauge.CapEffect = glassCapEffect
            controlStack.Add(Me.m_RadialGauge)
            Dim needleIndicator As Nevron.Nov.Chart.NNeedleValueIndicator = New Nevron.Nov.Chart.NNeedleValueIndicator()
            Me.m_RadialGauge.Indicators.Add(needleIndicator)
            Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(180, Nevron.Nov.NUnit.Degree)
            Me.InitSections(Me.m_RadialGauge)
            Me.m_DataFeedTimer = New Nevron.Nov.NTimer()
            AddHandler Me.m_DataFeedTimer.Tick, New Nevron.Nov.[Function](AddressOf Me.OnDataFeedTimerTick)
            Me.m_DataFeedTimer.Start()
            Return stack
        End Function
		''' <summary>
		''' 
		''' </summary>
		Protected Overrides Sub OnUnregistered()
            MyBase.OnUnregistered()
            Me.m_DataFeedTimer.[Stop]()
        End Sub
		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))
            Me.m_BlueSectionBeginUpDown = Me.CreateUpDown(0.0)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Begin:", Me.m_BlueSectionBeginUpDown, True))
            Me.m_BlueSectionEndUpDown = Me.CreateUpDown(20.0)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("End:", Me.m_BlueSectionEndUpDown, True))
            Me.m_RedSectionBeginUpDown = Me.CreateUpDown(80.0)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Begin:", Me.m_RedSectionBeginUpDown, True))
            Me.m_RedSectionEndUpDown = Me.CreateUpDown(100.0)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("End:", Me.m_RedSectionEndUpDown, True))
            Me.m_StopStartTimerButton = New Nevron.Nov.UI.NButton("Stop Timer")
            propertyStack.Add(Me.m_StopStartTimerButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to create scale sections. Scale sections allow you to modify the appearance of scale elements if they fall in certain range.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		''' <summary>
		''' 
		''' </summary>
		Private Sub OnDataFeedTimerTick()
			' update linear gauge
			Dim gauges As Nevron.Nov.Chart.NGauge() = New Nevron.Nov.Chart.NGauge() {Me.m_RadialGauge, Me.m_LinearGauge}

            For i As Integer = 0 To gauges.Length - 1
                Dim gauge As Nevron.Nov.Chart.NGauge = gauges(i)
                Dim valueIndicator As Nevron.Nov.Chart.NValueIndicator = CType(gauge.Indicators(0), Nevron.Nov.Chart.NValueIndicator)
                Dim scale As Nevron.Nov.Chart.NStandardScale = CType(gauge.Axes(CInt((0))).Scale, Nevron.Nov.Chart.NStandardScale)
                Dim blueSection As Nevron.Nov.Chart.NScaleSection = CType(scale.Sections(0), Nevron.Nov.Chart.NScaleSection)
                Dim redSection As Nevron.Nov.Chart.NScaleSection = CType(scale.Sections(1), Nevron.Nov.Chart.NScaleSection)
                Me.m_FirstIndicatorAngle += 0.02
                valueIndicator.Value = 50.0 - System.Math.Cos(Me.m_FirstIndicatorAngle) * 50.0

				' FIX: Smart Shapes
				'					valueIndicator.Shape.FillStyle = new NStockGradientFill(ENGradientStyle.Horizontal, ENGradientVariant.Variant1, NColor.White, NColor.Blue);
'					valueIndicator.Shape.StrokeStyle = new NStrokeStyle(Color.Blue);
				If blueSection.Range.Contains(valueIndicator.Value) Then
'					valueIndicator.Shape.FillStyle = new NGradientFillStyle(GradientStyle.Horizontal, GradientVariant.Variant1, Color.White, Color.Red);
'					valueIndicator.Shape.StrokeStyle = new NStrokeStyle(Color.Red);
				ElseIf redSection.Range.Contains(valueIndicator.Value) Then
                Else
'					valueIndicator.Shape.FillStyle = new NColorFillStyle(Color.LightGreen);
'					valueIndicator.Shape.StrokeStyle = new NStrokeStyle(Color.DarkGreen);
				End If
            Next
        End Sub
		''' <summary>
		''' 
		''' </summary>
		''' <paramname="sender"></param>
		''' <paramname="e"></param>
		Private Sub OnStopStartTimerButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
            If Me.m_DataFeedTimer.IsStarted Then
                Me.m_DataFeedTimer.[Stop]()
                Me.m_StopStartTimerButton.Content = New Nevron.Nov.UI.NLabel("Start Timer")
            Else
                Me.m_DataFeedTimer.Start()
                Me.m_StopStartTimerButton.Content = New Nevron.Nov.UI.NLabel("Stop Timer")
            End If
        End Sub
		''' <summary>
		''' 
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub UpdateSections(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim gauges As Nevron.Nov.Chart.NGauge() = New Nevron.Nov.Chart.NGauge() {Me.m_RadialGauge, Me.m_LinearGauge}

            For i As Integer = 0 To gauges.Length - 1
                Dim gauge As Nevron.Nov.Chart.NGauge = gauges(i)
                Dim axis As Nevron.Nov.Chart.NGaugeAxis = CType(gauge.Axes(0), Nevron.Nov.Chart.NGaugeAxis)
                Dim scale As Nevron.Nov.Chart.NStandardScale = CType(axis.Scale, Nevron.Nov.Chart.NStandardScale)

                If scale.Sections.Count = 2 Then
                    Dim blueSection As Nevron.Nov.Chart.NScaleSection = CType(scale.Sections(0), Nevron.Nov.Chart.NScaleSection)
                    blueSection.Range = New Nevron.Nov.Graphics.NRange(Me.m_BlueSectionBeginUpDown.Value, Me.m_BlueSectionEndUpDown.Value)
                    Dim redSection As Nevron.Nov.Chart.NScaleSection = CType(scale.Sections(1), Nevron.Nov.Chart.NScaleSection)
                    redSection.Range = New Nevron.Nov.Graphics.NRange(Me.m_RedSectionBeginUpDown.Value, Me.m_RedSectionEndUpDown.Value)
                End If
            Next
        End Sub
	
		#EndRegion

		#Region"Implementation"

		''' <summary>
		''' 
		''' </summary>
		''' <paramname="value"></param>
		''' <returns></returns>
		Private Function CreateUpDown(ByVal value As Double) As Nevron.Nov.UI.NNumericUpDown
            Dim numericUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            numericUpDown.Minimum = 0.0
            numericUpDown.Maximum = 100.0
            numericUpDown.Value = value
            AddHandler numericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.UpdateSections)
            Return numericUpDown
        End Function
		''' <summary>
		''' 
		''' </summary>
		''' <paramname="gauge"></param>
		Private Sub InitSections(ByVal gauge As Nevron.Nov.Chart.NGauge)
            gauge.Axes.Clear()
            Dim axis As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            gauge.Axes.Add(axis)
            axis.Anchor = New Nevron.Nov.Chart.NDockGaugeAxisAnchor(Nevron.Nov.Chart.ENGaugeAxisDockZone.Top)
            Dim scale As Nevron.Nov.Chart.NStandardScale = CType(axis.Scale, Nevron.Nov.Chart.NStandardScale)

			' init text style for regular labels
			scale.Labels.Style.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            scale.Labels.Style.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 10, Nevron.Nov.Graphics.ENFontStyle.Bold)

			' init ticks
			scale.MajorGridLines.Visible = True
            scale.MinTickDistance = 25
            scale.MinorTickCount = 1
            scale.SetPredefinedScale(Nevron.Nov.Chart.ENPredefinedScaleStyle.Scientific)

			' create sections
			Dim blueSection As Nevron.Nov.Chart.NScaleSection = New Nevron.Nov.Chart.NScaleSection()
            blueSection.Range = New Nevron.Nov.Graphics.NRange(0, 20)
            blueSection.RangeFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.Blue, 0.5F))
            blueSection.MajorGridStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Blue)
            blueSection.MajorTickStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkBlue)
            blueSection.MinorTickStroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.Graphics.ENDashStyle.Dot)
            Dim labelStyle As Nevron.Nov.UI.NTextStyle = New Nevron.Nov.UI.NTextStyle()
            labelStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Blue)
            labelStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 10, Nevron.Nov.Graphics.ENFontStyle.Bold)
            blueSection.LabelTextStyle = labelStyle
            scale.Sections.Add(blueSection)
            Dim redSection As Nevron.Nov.Chart.NScaleSection = New Nevron.Nov.Chart.NScaleSection()
            redSection.Range = New Nevron.Nov.Graphics.NRange(80, 100)
            redSection.RangeFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.Red, 0.5F))
            redSection.MajorGridStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Red)
            redSection.MajorTickStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkRed)
            redSection.MinorTickStroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.ENDashStyle.Dot)
            labelStyle = New Nevron.Nov.UI.NTextStyle()
            labelStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Red)
            labelStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 10.0, Nevron.Nov.Graphics.ENFontStyle.Bold)
            redSection.LabelTextStyle = labelStyle
            scale.Sections.Add(redSection)
        End Sub

		#EndRegion

		#Region"Fields"


		Private m_BlueSectionBeginUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_BlueSectionEndUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_RedSectionBeginUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_RedSectionEndUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_RadialGauge As Nevron.Nov.Chart.NRadialGauge
        Private m_LinearGauge As Nevron.Nov.Chart.NLinearGauge
        Private m_DataFeedTimer As Nevron.Nov.NTimer
        Private m_StopStartTimerButton As Nevron.Nov.UI.NButton
        Private m_FirstIndicatorAngle As Double

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NScaleSectionsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Protected Function CreateBorder() As Nevron.Nov.UI.NBorder
            Return Nevron.Nov.UI.NBorder.CreateThreeColorBorder(Nevron.Nov.Graphics.NColor.LightGray, Nevron.Nov.Graphics.NColor.White, Nevron.Nov.Graphics.NColor.DarkGray, 10, 10)
        End Function

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultRadialGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(300, 300)
        Private Shared ReadOnly defaultLinearVerticalGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(100, 300)

		#EndRegion
	End Class
End Namespace
