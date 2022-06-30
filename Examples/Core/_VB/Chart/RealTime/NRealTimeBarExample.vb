Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Real Time Bar Example.
	''' </summary>
	Public Class NRealTimeBarExample
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
            Nevron.Nov.Examples.Chart.NRealTimeBarExample.NRealTimeBarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NRealTimeBarExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)
            AddHandler chartView.Registered, AddressOf Me.OnChartViewRegistered
            AddHandler chartView.Unregistered, AddressOf Me.OnChartViewUnregistered

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Real Time Bar"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)
            Dim scaleY As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            scaleY.InflateViewRangeBegin = False
            scaleY.InflateViewRangeEnd = False

			' add interlaced stripe to the Y axis
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Strips.Add(strip)
            Me.m_Random = New System.Random()
            Me.m_Bar = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Bar.Name = "Line Series"
            Me.m_Bar.InflateMargins = True
            Me.m_Bar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_Bar.UseXValues = True
            Me.m_Bar.WidthMode = Nevron.Nov.Chart.ENBarWidthMode.ScaleWidth
            Me.m_Bar.Width = 0.5
            Me.m_CurXValue = 0
            Me.m_Chart.Series.Add(Me.m_Bar)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim toggleTimerButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Stop Timer")
            AddHandler toggleTimerButton.Click, AddressOf Me.OnToggleTimerButtonClick
            toggleTimerButton.Tag = 0
            stack.Add(toggleTimerButton)
            Dim resetButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Reset Data")
            AddHandler resetButton.Click, AddressOf Me.OnResetButtonClick
            stack.Add(resetButton)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a bar chart that updates in real time.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnChartViewRegistered(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Timer = New Nevron.Nov.NTimer()
            AddHandler Me.m_Timer.Tick, AddressOf Me.OnTimerTick
            Me.m_Timer.Start()
        End Sub

        Private Sub OnChartViewUnregistered(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Timer.[Stop]()
            RemoveHandler Me.m_Timer.Tick, AddressOf Me.OnTimerTick
            Me.m_Timer = Nothing
        End Sub

        Private Sub OnTimerTick()
            Const dataPointCount As Integer = 40

            If Me.m_Bar.DataPoints.Count < dataPointCount Then
                Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(System.Math.Min(System.Threading.Interlocked.Increment(Me.m_CurXValue), Me.m_CurXValue - 1), Me.m_Random.[Next](80) + 20))
            Else
                Me.m_Bar.DataPoints(CInt((Me.m_Bar.OriginIndex))).X = System.Math.Min(System.Threading.Interlocked.Increment(Me.m_CurXValue), Me.m_CurXValue - 1)
                Me.m_Bar.DataPoints(CInt((Me.m_Bar.OriginIndex))).Value = Me.m_Random.[Next](80) + 20
                Me.m_Bar.OriginIndex += 1

                If Me.m_Bar.OriginIndex >= Me.m_Bar.DataPoints.Count Then
                    Me.m_Bar.OriginIndex = 0
                End If
            End If
        End Sub

        Private Sub OnResetButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Bar.DataPoints.Clear()
            Me.m_Bar.OriginIndex = 0
            Me.m_CurXValue = 0
        End Sub

        Private Sub OnToggleTimerButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim button As Nevron.Nov.UI.NButton = CType(arg.TargetNode, Nevron.Nov.UI.NButton)

            If CInt(button.Tag) = 0 Then
                Me.m_Timer.[Stop]()
                button.Content = New Nevron.Nov.UI.NLabel("Start Timer")
                button.Tag = 1
            Else
                Me.m_Timer.Start()
                button.Content = New Nevron.Nov.UI.NLabel("Stop Timer")
                button.Tag = 0
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_Bar As Nevron.Nov.Chart.NBarSeries
        Private m_Random As System.Random
        Private m_Timer As Nevron.Nov.NTimer
        Private m_CurXValue As Integer

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NRealTimeBarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
