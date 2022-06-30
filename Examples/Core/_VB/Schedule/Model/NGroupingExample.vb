Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NGroupingExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor.
		''' </summary>
		Public Sub New()
        End Sub

		''' <summary>
		''' Static constructor.
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Schedule.NGroupingExample.NGroupingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NGroupingExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a simple schedule
			Dim scheduleViewWithRibbon As Nevron.Nov.Schedule.NScheduleViewWithRibbon = New Nevron.Nov.Schedule.NScheduleViewWithRibbon()
            Me.m_ScheduleView = scheduleViewWithRibbon.View
            Me.m_ScheduleView.Document.PauseHistoryService()

            Try
                Me.InitSchedule(Me.m_ScheduleView.Content)
            Finally
                Me.m_ScheduleView.Document.ResumeHistoryService()
            End Try

			' Return the commanding widget
			Return scheduleViewWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NRadioButton("Time grouping first"))
            stack.Add(New Nevron.Nov.UI.NRadioButton("Group grouping first"))
            Dim groupingOrderGroup As Nevron.Nov.UI.NRadioButtonGroup = New Nevron.Nov.UI.NRadioButtonGroup(stack)
            AddHandler groupingOrderGroup.SelectedIndexChanged, AddressOf Me.OnRadioGroupSelectedIndexChanged
            groupingOrderGroup.SelectedIndex = 0
            Return groupingOrderGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    This example demonstrates how to create schedule groups, how to associate a group to appointments and
	how to apply grouping to schedule view modes. Each view mode (except for ""Timeline"") has a default
	date related grouping, so you can either add a new grouping or insert it before the default one. This
	will affect the order in which the groupings are applied. Use the radio button on the right to control
	the grouping order.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
            Const ActivityGroup As String = "Activity"
            Const Work As String = "Work"
            Const Rest As String = "Rest"
            Const Travel As String = "Travel"
            schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Day

			' Add a schedule group
			schedule.Groups.Add(New Nevron.Nov.Schedule.NScheduleGroup(ActivityGroup, New String() {Work, Rest, Travel}))

			' Add a grouping to each of the view modes
			schedule.DayViewMode.Groupings.Add(New Nevron.Nov.Schedule.NGroupGrouping(ActivityGroup))
            schedule.WeekViewMode.Groupings.Add(New Nevron.Nov.Schedule.NGroupGrouping(ActivityGroup))
            schedule.MonthViewMode.Groupings.Add(New Nevron.Nov.Schedule.NGroupGrouping(ActivityGroup))
            schedule.TimelineViewMode.Groupings.Add(New Nevron.Nov.Schedule.NGroupGrouping(ActivityGroup))

			' Create the appointments
			Dim today As System.DateTime = System.DateTime.Today
            schedule.Appointments.Add(Me.CreateAppointment("Travel to Work", today.AddHours(6.5), today.AddHours(7.5), Travel))
            schedule.Appointments.Add(Me.CreateAppointment("Meeting with John", today.AddHours(8), today.AddHours(10), Work))
            schedule.Appointments.Add(Me.CreateAppointment("Conference", today.AddHours(10.5), today.AddHours(11.5), Work))
            schedule.Appointments.Add(Me.CreateAppointment("Lunch", today.AddHours(12), today.AddHours(14), Rest))
            schedule.Appointments.Add(Me.CreateAppointment("News Reading", today.AddHours(12.5), today.AddHours(13.5), Rest))
            schedule.Appointments.Add(Me.CreateAppointment("Video Presentation", today.AddHours(14.5), today.AddHours(15.5), Work))
            schedule.Appointments.Add(Me.CreateAppointment("Web Meeting", today.AddHours(16), today.AddHours(17), Work))
            schedule.Appointments.Add(Me.CreateAppointment("Travel back home", today.AddHours(17.5), today.AddHours(19), Travel))
            schedule.Appointments.Add(Me.CreateAppointment("Family Dinner", today.AddHours(20), today.AddHours(21), Rest))
        End Sub

        Private Function CreateAppointment(ByVal subject As String, ByVal start As System.DateTime, ByVal [end] As System.DateTime, ByVal groupItem As String) As Nevron.Nov.Schedule.NAppointment
            Dim appointment As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment(subject, start, [end])
            appointment.Groups = New Nevron.Nov.Schedule.NAppointmentGroupCollection()
            appointment.Groups.Add(New Nevron.Nov.Schedule.NAppointmentGroup("Activity", groupItem))
            Return appointment
        End Function

        Private Sub SwapGroupings(ByVal viewMode As Nevron.Nov.Schedule.NViewMode)
            Dim grouping1 As Nevron.Nov.Schedule.NGrouping = viewMode.Groupings(0)
            Dim grouping2 As Nevron.Nov.Schedule.NGrouping = viewMode.Groupings(1)
            viewMode.Groupings.Clear()
            viewMode.Groupings.Add(grouping2)
            viewMode.Groupings.Add(grouping1)
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnRadioGroupSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim selectedIndex As Integer = CInt(arg.NewValue)
            Dim schedule As Nevron.Nov.Schedule.NSchedule = Me.m_ScheduleView.Content

            If selectedIndex = 0 Then
				' Time grouping should be first
				If TypeOf schedule.DayViewMode.Groupings(0) Is Nevron.Nov.Schedule.NGroupGrouping Then
                    Me.SwapGroupings(schedule.DayViewMode)
                    Me.SwapGroupings(schedule.WeekViewMode)
                    Me.SwapGroupings(schedule.MonthViewMode)
                End If
            Else
				' Schedule group grouping should be first
				If TypeOf schedule.DayViewMode.Groupings(0) Is Nevron.Nov.Schedule.NDateRangeGrouping Then
                    Me.SwapGroupings(schedule.DayViewMode)
                    Me.SwapGroupings(schedule.WeekViewMode)
                    Me.SwapGroupings(schedule.MonthViewMode)
                End If
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NGroupingExample.
		''' </summary>
		Public Shared ReadOnly NGroupingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
