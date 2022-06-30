Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NRecurrenceRulesExample
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
            Nevron.Nov.Examples.Schedule.NRecurrenceRulesExample.NRecurrenceRulesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NRecurrenceRulesExample), NExampleBaseSchema)
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
            stack.Add(New Nevron.Nov.UI.NRadioButton("No recurrence"))
            stack.Add(New Nevron.Nov.UI.NRadioButton("Every 3 hours"))
            stack.Add(New Nevron.Nov.UI.NRadioButton("Every day"))
            stack.Add(New Nevron.Nov.UI.NRadioButton("Every week on Monday and Friday"))
            stack.Add(New Nevron.Nov.UI.NRadioButton("The third day of every other month"))
            stack.Add(New Nevron.Nov.UI.NRadioButton("The last Sunday of every month"))
            stack.Add(New Nevron.Nov.UI.NRadioButton("May 7 and July 7 of every year"))
            stack.Add(New Nevron.Nov.UI.NRadioButton("The first Monday and Tuesday of" & Global.Microsoft.VisualBasic.Constants.vbLf & "May, June and August"))
            stack.Add(New Nevron.Nov.UI.NRadioButton("Every second day for 1 month from today"))
            stack.Add(New Nevron.Nov.UI.NRadioButton("Every day for 5 occurrences from today"))
            Dim group As Nevron.Nov.UI.NRadioButtonGroup = New Nevron.Nov.UI.NRadioButtonGroup(stack)
            group.SelectedIndex = 2
            AddHandler group.SelectedIndexChanged, AddressOf Me.OnRadioGroupSelectedIndexChanged
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    This example demonstrates how to create recurring appointments, i.e. appointments, which occur multiple
	times. Recurring appointments are created in the same way as ordinary appointments with the only difference
	that they have a recurrence rule assigned to their <b>RecurrenceRule</b> property. This recurring rule
	defines when the appointment occurs. Recurring appointments can be easily recognized by the circular arrows
	symbol at their top left corner.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
            Dim today As System.DateTime = System.DateTime.Today
            Dim appointment As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment("Appoinment", today.AddHours(12), today.AddHours(14))
            appointment.RecurrenceRule = Me.CreateDailyRule()
            schedule.Appointments.Add(appointment)
        End Sub

        Private Function CreateHourlyRule() As Nevron.Nov.Schedule.NRecurrenceHourlyRule
			' Create a rule, which occurs every 3 hours
			Dim rule As Nevron.Nov.Schedule.NRecurrenceHourlyRule = New Nevron.Nov.Schedule.NRecurrenceHourlyRule()
            rule.StartDate = Nevron.Nov.Examples.Schedule.NRecurrenceRulesExample.RuleStart
            rule.Interval = 3
            Return rule
        End Function

        Private Function CreateDailyRule() As Nevron.Nov.Schedule.NRecurrenceDailyRule
			' Create a rule, which occurs every day
			Dim rule As Nevron.Nov.Schedule.NRecurrenceDailyRule = New Nevron.Nov.Schedule.NRecurrenceDailyRule()
            rule.StartDate = Nevron.Nov.Examples.Schedule.NRecurrenceRulesExample.RuleStart
            Return rule
        End Function

        Private Function CreateWeeklyRule() As Nevron.Nov.Schedule.NRecurrenceWeeklyRule
			' Create a rule, which occurs every week on Monday and Friday
			Dim rule As Nevron.Nov.Schedule.NRecurrenceWeeklyRule = New Nevron.Nov.Schedule.NRecurrenceWeeklyRule(Nevron.Nov.Schedule.ENDayOfWeek.Monday Or Nevron.Nov.Schedule.ENDayOfWeek.Friday)
            rule.StartDate = Nevron.Nov.Examples.Schedule.NRecurrenceRulesExample.RuleStart
            Return rule
        End Function

        Private Function CreateAbsoluteMonthlyRule() As Nevron.Nov.Schedule.NRecurrenceMonthlyRule
			' Create a rule, which occurs on the third day of every other month
			' Use negative values for the last days of the month, for example -1 refers to the last day of the month
			Dim rule As Nevron.Nov.Schedule.NRecurrenceMonthlyRule = New Nevron.Nov.Schedule.NRecurrenceMonthlyRule(3)
            rule.StartDate = Nevron.Nov.Examples.Schedule.NRecurrenceRulesExample.RuleStart
            rule.Interval = 2
            Return rule
        End Function

        Private Function CreateRelativeMonthlyRule() As Nevron.Nov.Schedule.NRecurrenceMonthlyRule
			' Create a rule, which occurs on the last Sunday of every month
			Dim rule As Nevron.Nov.Schedule.NRecurrenceMonthlyRule = New Nevron.Nov.Schedule.NRecurrenceMonthlyRule(Nevron.Nov.Schedule.ENDayOrdinal.Last, Nevron.Nov.Schedule.ENDayOfWeek.Sunday)
            rule.StartDate = Nevron.Nov.Examples.Schedule.NRecurrenceRulesExample.RuleStart
            Return rule
        End Function

        Private Function CreateAbsoluteYearlyRule() As Nevron.Nov.Schedule.NRecurrenceYearlyRule
			' Create a rule, which occurs on every May 7 and July 7, every year
			Dim rule As Nevron.Nov.Schedule.NRecurrenceYearlyRule = New Nevron.Nov.Schedule.NRecurrenceYearlyRule(7, Nevron.Nov.Schedule.ENMonth.May Or Nevron.Nov.Schedule.ENMonth.July)
            rule.StartDate = Nevron.Nov.Examples.Schedule.NRecurrenceRulesExample.RuleStart
            Return rule
        End Function

        Private Function CreateRelativeYearlyRule() As Nevron.Nov.Schedule.NRecurrenceYearlyRule
			' Create a rule, which occurs on the first Monday and Tuesday of May, June and August
			Dim rule As Nevron.Nov.Schedule.NRecurrenceYearlyRule = New Nevron.Nov.Schedule.NRecurrenceYearlyRule(Nevron.Nov.Schedule.ENDayOrdinal.First, Nevron.Nov.Schedule.ENDayOfWeek.Monday Or Nevron.Nov.Schedule.ENDayOfWeek.Tuesday, Nevron.Nov.Schedule.ENMonth.May Or Nevron.Nov.Schedule.ENMonth.June Or Nevron.Nov.Schedule.ENMonth.August)
            rule.StartDate = Nevron.Nov.Examples.Schedule.NRecurrenceRulesExample.RuleStart
            Return rule
        End Function

        Private Function CreateDailyRuleForOneMonth() As Nevron.Nov.Schedule.NRecurrenceDailyRule
            Dim rule As Nevron.Nov.Schedule.NRecurrenceDailyRule = New Nevron.Nov.Schedule.NRecurrenceDailyRule()
            rule.EndMode = Nevron.Nov.Schedule.ENRecurrenceEndMode.ByDate
            rule.EndDate = System.DateTime.Today.AddMonths(1)
            rule.Interval = 2
            Return rule
        End Function

        Private Function CreateDailyRuleForFiveOccurrences() As Nevron.Nov.Schedule.NRecurrenceDailyRule
            Dim rule As Nevron.Nov.Schedule.NRecurrenceDailyRule = New Nevron.Nov.Schedule.NRecurrenceDailyRule()
            rule.EndMode = Nevron.Nov.Schedule.ENRecurrenceEndMode.AfterOccurrences
            rule.MaxOccurrences = 5
            Return rule
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnRadioGroupSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim schedule As Nevron.Nov.Schedule.NSchedule = Me.m_ScheduleView.Content
            Dim appointment As Nevron.Nov.Schedule.NAppointment = CType(schedule.Appointments(0), Nevron.Nov.Schedule.NAppointment)
            Dim selectedIndex As Integer = CInt(arg.NewValue)

            Select Case selectedIndex
                Case 0
                    appointment.RecurrenceRule = Nothing
                    schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Week
                Case 1
                    appointment.RecurrenceRule = Me.CreateHourlyRule()
                    schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Week
                Case 2
                    appointment.RecurrenceRule = Me.CreateDailyRule()
                    schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Week
                Case 3
                    appointment.RecurrenceRule = Me.CreateWeeklyRule()
                    schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Week
                Case 4
                    appointment.RecurrenceRule = Me.CreateAbsoluteMonthlyRule()
                    schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Month
                Case 5
                    appointment.RecurrenceRule = Me.CreateRelativeMonthlyRule()
                    schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Month
                Case 6
                    appointment.RecurrenceRule = Me.CreateAbsoluteYearlyRule()
                    schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Month
                Case 7
                    appointment.RecurrenceRule = Me.CreateRelativeYearlyRule()
                    schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Month
                Case 8
                    appointment.RecurrenceRule = Me.CreateDailyRuleForOneMonth()
                    schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Month
                Case 9
                    appointment.RecurrenceRule = Me.CreateDailyRuleForFiveOccurrences()
                    schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Month
            End Select
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRecurrenceRulesExample.
		''' </summary>
		Public Shared ReadOnly NRecurrenceRulesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		' The start date of the recurrence rules. By default rules start from the current day (today),
		' so if you want to change that, you should set their Start property to another date.
		Private Shared ReadOnly RuleStart As System.DateTime = New System.DateTime(2015, 1, 1)

		#EndRegion
	End Class
End Namespace
