Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Value Timeline Scale Example
	''' </summary>
	Public Class NValueTimelineScaleExample
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
            Nevron.Nov.Examples.Chart.NValueTimelineScaleExample.NValueTimelineScaleExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NValueTimelineScaleExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Value Timeline Scale"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)

			' configure axes
			Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' setup X axis
			Dim axis As Nevron.Nov.Chart.NCartesianAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryX)
            Me.m_TimeLineScale = New Nevron.Nov.Chart.NValueTimelineScale()
            axis.Scale = Me.m_TimeLineScale

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
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Me.OnWeeklyDataButtonClick(Nothing)
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim firstRowVisibleCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("First Row Visible")
            AddHandler firstRowVisibleCheckBox.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnFirstRowVisibleCheckBoxClick)
            stack.Add(firstRowVisibleCheckBox)
            Dim secondRowVisibleCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Second Row Visible")
            AddHandler secondRowVisibleCheckBox.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSecondRowVisibleCheckBoxClick)
            stack.Add(secondRowVisibleCheckBox)
            Dim thirdRowVisibleCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Third Row Visible")
            AddHandler thirdRowVisibleCheckBox.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnThirdRowVisibleCheckBoxClick)
            stack.Add(thirdRowVisibleCheckBox)
            Dim dailyDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Daily Data")
            AddHandler dailyDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnDailyDataButtonClick)
            stack.Add(dailyDataButton)
            Dim weeklyDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Weekly Data")
            AddHandler weeklyDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnWeeklyDataButtonClick)
            stack.Add(weeklyDataButton)
            Dim monthlyDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Monthly Data")
            AddHandler monthlyDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnMonthlyDataButtonClick)
            stack.Add(monthlyDataButton)
            Dim yearlyDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Yearly Data")
            AddHandler yearlyDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnYearlyDataButtonClick)
            stack.Add(yearlyDataButton)
            firstRowVisibleCheckBox.Checked = True
            secondRowVisibleCheckBox.Checked = True
            thirdRowVisibleCheckBox.Checked = True
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a value timeline scale.</p>"
        End Function

		#EndRegion

		#Region"Implementation"
		
		Private Sub GenerateData(ByVal dtStart As System.DateTime, ByVal dtEnd As System.DateTime, ByVal span As Nevron.Nov.NDateTimeSpan)
            Dim count As Long = span.GetSpanCountInRange(New Nevron.Nov.NDateTimeRange(dtStart, dtEnd))
            Dim open, high, low, close As Double
            Me.m_Stock.DataPoints.Clear()
            Dim random As System.Random = New System.Random()
            Dim dtNow As System.DateTime = dtStart
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

		Private Sub OnThirdRowVisibleCheckBoxClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_TimeLineScale.FirstRow.Visible = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnSecondRowVisibleCheckBoxClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_TimeLineScale.SecondRow.Visible = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnFirstRowVisibleCheckBoxClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_TimeLineScale.ThirdRow.Visible = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnYearlyDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' generate data for 30 years
			Dim dtNow As System.DateTime = System.DateTime.Now
            Dim dtEnd As System.DateTime = New System.DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 7, 0, 0, 0)
            Dim dtStart As System.DateTime = Nevron.Nov.NDateTimeUnit.Year.Add(dtEnd, -30)
            Me.GenerateData(dtStart, dtEnd, New Nevron.Nov.NDateTimeSpan(1, Nevron.Nov.NDateTimeUnit.Month))
        End Sub

        Private Sub OnMonthlyDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' generate data for 30 months 
			Dim dtNow As System.DateTime = System.DateTime.Now
            Dim dtEnd As System.DateTime = New System.DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 7, 0, 0, 0)
            Dim dtStart As System.DateTime = Nevron.Nov.NDateTimeUnit.Month.Add(dtEnd, -30)
            Me.GenerateData(dtStart, dtEnd, New Nevron.Nov.NDateTimeSpan(1, Nevron.Nov.NDateTimeUnit.Week))
        End Sub

        Private Sub OnWeeklyDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' generate data for 30 weeks
			Dim dtNow As System.DateTime = System.DateTime.Now
            Dim dtEnd As System.DateTime = New System.DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 7, 0, 0, 0)
            Dim dtStart As System.DateTime = Nevron.Nov.NDateTimeUnit.Week.Add(dtEnd, -30)
            Me.GenerateData(dtStart, dtEnd, New Nevron.Nov.NDateTimeSpan(1, Nevron.Nov.NDateTimeUnit.Day))
        End Sub

        Private Sub OnDailyDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' generate data for 30 days
			Dim dtNow As System.DateTime = System.DateTime.Now
            Dim dtEnd As System.DateTime = New System.DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 17, 0, 0, 0)
            Dim dtStart As System.DateTime = New System.DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 7, 0, 0, 0)
            Me.GenerateData(dtStart, dtEnd, New Nevron.Nov.NDateTimeSpan(5, Nevron.Nov.NDateTimeUnit.Minute))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_Stock As Nevron.Nov.Chart.NStockSeries
        Private m_TimeLineScale As Nevron.Nov.Chart.NValueTimelineScale

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NValueTimelineScaleExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
