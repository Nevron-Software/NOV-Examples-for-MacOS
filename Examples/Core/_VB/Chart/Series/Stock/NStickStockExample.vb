Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Stick Stock Example
	''' </summary>
	Public Class NStickStockExample
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
            Nevron.Nov.Examples.Chart.NStickStockExample.NStickStockExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStickStockExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Stick Stock"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' setup X axis
			Dim axis As Nevron.Nov.Chart.NCartesianAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryX)
            axis.Scale = New Nevron.Nov.Chart.NValueTimelineScale()

			' setup primary Y axis
			axis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryY)
            Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(axis.Scale, Nevron.Nov.Chart.NLinearScale)

			' configure ticks and grid lines
			linearScale.MajorGridLines.Stroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.LightGray)
            linearScale.InnerMajorTicks.Visible = False

			' add interlaced stripe 
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige)
            strip.Interlaced = True
            linearScale.Strips.Add(strip)

			' Setup the stock series
			Me.m_Stock = New Nevron.Nov.Chart.NStockSeries()
            Me.m_Chart.Series.Add(Me.m_Stock)
            Me.m_Stock.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_Stock.CandleShape = Nevron.Nov.Chart.ENCandleShape.Stick
            Me.m_Stock.CandleWidth = 4
            Me.m_Stock.UseXValues = True
            Me.GenerateData()
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a stick stock chart.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub GenerateData()
			' generate data for 30 weeks
			Dim dtNow As System.DateTime = System.DateTime.Now
            Dim dtEnd As System.DateTime = New System.DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 7, 0, 0, 0)
            Dim dtStart As System.DateTime = Nevron.Nov.NDateTimeUnit.Week.Add(dtEnd, -30)
            Dim span As Nevron.Nov.NDateTimeSpan = New Nevron.Nov.NDateTimeSpan(1, Nevron.Nov.NDateTimeUnit.Day)
            Dim count As Long = span.GetSpanCountInRange(New Nevron.Nov.NDateTimeRange(dtStart, dtEnd))
            Dim open, high, low, close As Double
            Me.m_Stock.DataPoints.Clear()
            Dim random As System.Random = New System.Random()
            Dim prevClose As Double = 100

            For nIndex As Integer = 0 To count - 1
                open = prevClose

                If prevClose < 25 OrElse random.NextDouble() > 0.5 Then
					' upward price change
					close = open + (2 + (random.NextDouble() * 20))
                    high = close + (random.NextDouble() * 10)
                    low = open - (random.NextDouble() * 10)
                Else
					' downward price change
					close = open - (2 + (random.NextDouble() * 20))
                    high = open + (random.NextDouble() * 10)
                    low = close - (random.NextDouble() * 10)
                End If

                If low < 1 Then
                    low = 1
                End If

                prevClose = close
                Me.m_Stock.DataPoints.Add(New Nevron.Nov.Chart.NStockDataPoint(Nevron.Nov.NDateTimeHelpers.ToOADate(dtNow), open, close, high, low))
                dtNow = span.Add(dtNow)
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"


		#EndRegion

		#Region"Fields"

		Private m_Stock As Nevron.Nov.Chart.NStockSeries
        Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStickStockExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
