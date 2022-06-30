Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Gauge
	''' <summary>
	''' This example demonstrates how to control the paint order of gauge indicators
	''' </summary>
	Public Class NIndicatorPaintOrderExample
        Inherits NExampleBase
        #Region"Constructors"

        ''' <summary>
        ''' 	Initializer constructor
        ''' </summary>
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Static constructor
        ''' </summary>
        Shared Sub New()
            Nevron.Nov.Examples.Gauge.NIndicatorPaintOrderExample.NIndicatorPaintOrderExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NIndicatorPaintOrderExample), NExampleBaseSchema)
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
            Me.m_RadialGauge.SweepAngle = New Nevron.Nov.NAngle(270, Nevron.Nov.NUnit.Degree)
            Me.m_RadialGauge.BeginAngle = New Nevron.Nov.NAngle(-225, Nevron.Nov.NUnit.Degree)
            Me.m_RadialGauge.PreferredSize = Nevron.Nov.Examples.Gauge.NIndicatorPaintOrderExample.defaultRadialGaugeSize
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
			Me.m_ValueIndicator = New Nevron.Nov.Chart.NNeedleValueIndicator()
            Me.m_ValueIndicator.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Horizontal, Nevron.Nov.Graphics.ENGradientVariant.Variant1, Nevron.Nov.Graphics.NColor.White, Nevron.Nov.Graphics.NColor.Red)
            Me.m_ValueIndicator.Stroke.Color = Nevron.Nov.Graphics.NColor.Red
            Me.m_ValueIndicator.Width = 15
            Me.m_ValueIndicator.OffsetFromScale = -10
            Me.m_RadialGauge.Indicators.Add(Me.m_ValueIndicator)
            Dim verticalStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            verticalStack.Direction = Nevron.Nov.Layout.ENHVDirection.TopToBottom
            verticalStack.Padding = New Nevron.Nov.Graphics.NMargins(80, 200, 80, 0)
            Me.m_NumericLedDisplay = New Nevron.Nov.Chart.NNumericLedDisplay()
            Me.m_NumericLedDisplay.Value = 0.0
            Me.m_NumericLedDisplay.CellCountMode = Nevron.Nov.Chart.ENDisplayCellCountMode.Fixed
            Me.m_NumericLedDisplay.CellCount = 7
            Me.m_NumericLedDisplay.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black)
            Me.m_NumericLedDisplay.Border = Nevron.Nov.UI.NBorder.CreateSunken3DBorder(New Nevron.Nov.UI.NUIThemeColorMap(Nevron.Nov.UI.ENUIThemeScheme.WindowsClassic))
            Me.m_NumericLedDisplay.BorderThickness = New Nevron.Nov.Graphics.NMargins(6)
            Me.m_NumericLedDisplay.Margins = New Nevron.Nov.Graphics.NMargins(5)
            Me.m_NumericLedDisplay.Padding = New Nevron.Nov.Graphics.NMargins(5)
            Dim gelCap As Nevron.Nov.Chart.NGelCapEffect = New Nevron.Nov.Chart.NGelCapEffect()
            gelCap.Shape = Nevron.Nov.Chart.ENCapEffectShape.RoundedRect
            Me.m_NumericLedDisplay.CapEffect = gelCap
            Me.m_NumericLedDisplay.PreferredHeight = 60
            verticalStack.Add(Me.m_NumericLedDisplay)
            Me.m_RadialGauge.Content = verticalStack

			' add radial gauge
			controlStack.Add(Me.m_RadialGauge)
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

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))
            propertyStack.Add(New Nevron.Nov.UI.NLabel("Paint Order:"))
            Me.m_PaintOrderComboBox = New Nevron.Nov.UI.NComboBox()
            propertyStack.Add(Me.m_PaintOrderComboBox)
            Me.m_PaintOrderComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENIndicatorPaintOrder)()
            AddHandler Me.m_PaintOrderComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnPaintOrderComboBoxSelectedIndexChanged)
            Me.m_PaintOrderComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENIndicatorPaintOrder.AfterScale)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to control the paint order of gauge indicators.</p>"
        End Function


		#EndRegion

		#Region"Event Handlers"

		''' <summary>
		''' 
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnPaintOrderComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
			' set the paint order
			Me.m_ValueIndicator.PaintOrder = CType(Me.m_PaintOrderComboBox.SelectedIndex, Nevron.Nov.Chart.ENIndicatorPaintOrder)
        End Sub
		''' <summary>
		''' 
		''' </summary>
		Private Sub OnDataFeedTimerTick()
			' update the indicator and the numeric led display
			Me.m_FirstIndicatorAngle += 0.02
            Dim value As Double = 50.0 - System.Math.Cos(Me.m_FirstIndicatorAngle) * 50.0
            Me.m_ValueIndicator.Value = value
            Me.m_NumericLedDisplay.Value = value
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RadialGauge As Nevron.Nov.Chart.NRadialGauge
        Private m_ValueIndicator As Nevron.Nov.Chart.NNeedleValueIndicator
        Private m_NumericLedDisplay As Nevron.Nov.Chart.NNumericLedDisplay
        Private m_DataFeedTimer As Nevron.Nov.NTimer
        Private m_FirstIndicatorAngle As Double
        Private m_PaintOrderComboBox As Nevron.Nov.UI.NComboBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NIndicatorPaintOrderExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultRadialGaugeSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(300, 300)

		#EndRegion
	End Class
End Namespace
