Imports Nevron.Nov.Chart
Imports Nevron.Nov.Chart.Tools
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI
Imports System

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Weekly Schdule Wrok Calendar Example
	''' </summary>
	Public Class NWeeklyScheduleWorkCalendarExample
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
            Nevron.Nov.Examples.Chart.NWeeklyScheduleWorkCalendarExample.NWeeklyScheduleWorkCalendarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NWeeklyScheduleWorkCalendarExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Weekly Schedule Work Calendar"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)
            Dim ranges As Nevron.Nov.Chart.NRangeSeries = New Nevron.Nov.Chart.NRangeSeries()
            Me.m_Chart.Series.Add(ranges)
            ranges.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            ranges.UseXValues = True
            Dim dt As System.DateTime = New System.DateTime(2014, 4, 14)
            Dim rand As System.Random = New System.Random()
            Dim rangeTimeline As Nevron.Nov.Chart.NRangeTimelineScale = New Nevron.Nov.Chart.NRangeTimelineScale()
            rangeTimeline.EnableCalendar = True
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale = rangeTimeline
            Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            yScale.MajorGridLines.Visible = True

			' add interlaced strip
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            yScale.Strips.Add(strip)
            yScale.Title.Text = "Weekly Workload in %"
            Dim workCalendar As Nevron.Nov.Chart.NWorkCalendar = rangeTimeline.Calendar

			' show only the working days on the scale
			Dim weekDayRule As Nevron.Nov.Chart.NWeekDayRule = New Nevron.Nov.Chart.NWeekDayRule(Nevron.Nov.Chart.ENWeekDayBit.Monday Or Nevron.Nov.Chart.ENWeekDayBit.Tuesday Or Nevron.Nov.Chart.ENWeekDayBit.Wednesday Or Nevron.Nov.Chart.ENWeekDayBit.Thursday Or Nevron.Nov.Chart.ENWeekDayBit.Friday)
            workCalendar.Rules.Add(weekDayRule)

			' generate data for week working days
			For i As Integer = 0 To 21 - 1

                If dt.DayOfWeek <> System.DayOfWeek.Saturday AndAlso dt.DayOfWeek <> System.DayOfWeek.Sunday Then
                    ranges.DataPoints.Add(New Nevron.Nov.Chart.NRangeDataPoint(Nevron.Nov.NDateTimeHelpers.ToOADate(dt), 0, Nevron.Nov.NDateTimeHelpers.ToOADate(dt + New System.TimeSpan(1, 0, 0, 0)), rand.NextDouble() * 70 + 30.0R))
                End If

                dt += New System.TimeSpan(1, 0, 0, 0)
            Next

            Me.ConfigureInteractivity(Me.m_Chart)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim enableWorkCalendarCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enable Work Calendar")
            AddHandler enableWorkCalendarCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnableWorkCalendarCheckBoxCheckedChanged)
            enableWorkCalendarCheckBox.Checked = True
            stack.Add(enableWorkCalendarCheckBox)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates the ability of the timeline and date/time scales to skip date time ranges, when it is expected that there is no data for them. Common applications of this feature are financial charts that usually display only working week days as stock markets are closed on weekends.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnEnableWorkCalendarCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NRangeTimelineScale).EnableCalendar = TryCast(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub ConfigureInteractivity(ByVal chart As Nevron.Nov.Chart.NChart)
            Dim interactor As Nevron.Nov.UI.NInteractor = New Nevron.Nov.UI.NInteractor()
            Dim rectangleZoomTool As Nevron.Nov.Chart.Tools.NRectangleZoomTool = New Nevron.Nov.Chart.Tools.NRectangleZoomTool()
            rectangleZoomTool.Enabled = True
            rectangleZoomTool.VerticalValueSnapper = New Nevron.Nov.Chart.NAxisRulerMinMaxSnapper()
            interactor.Add(rectangleZoomTool)
            Dim dataPanTool As Nevron.Nov.Chart.Tools.NDataPanTool = New Nevron.Nov.Chart.Tools.NDataPanTool()
            dataPanTool.StartMouseButtonEvent = Nevron.Nov.UI.ENMouseButtonEvent.RightButtonDown
            dataPanTool.EndMouseButtonEvent = Nevron.Nov.UI.ENMouseButtonEvent.RightButtonUp
            dataPanTool.Enabled = True
            interactor.Add(dataPanTool)
            chart.Interactor = interactor
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NWeeklyScheduleWorkCalendarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
