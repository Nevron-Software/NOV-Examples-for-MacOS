Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NRecurrenceStressTestExample
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
            Nevron.Nov.Examples.Schedule.NRecurrenceStressTestExample.NRecurrenceStressTestExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NRecurrenceStressTestExample), NExampleBaseSchema)
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
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    This example demonstrates how NOV Schedule deals with a large number of occurrences of recurring appointments.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
            Dim today As System.DateTime = System.DateTime.Today
            Dim startDate As System.DateTime = New System.DateTime(today.Year, 1, 1)

			' Create an appointment which occurs per 3 hours
			Dim appointment As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment("Meeting", today, today.AddHours(2))
            Dim rule As Nevron.Nov.Schedule.NRecurrenceHourlyRule = New Nevron.Nov.Schedule.NRecurrenceHourlyRule()
            rule.StartDate = startDate
            rule.Interval = 3
            appointment.RecurrenceRule = rule
            schedule.Appointments.Add(appointment)

			' Create an appointment which occurs every hour and categorize it
			appointment = New Nevron.Nov.Schedule.NAppointment("Talking", today, today.AddHours(0.5))
            rule = New Nevron.Nov.Schedule.NRecurrenceHourlyRule()
            rule.StartDate = startDate
            rule.Interval = 1
            appointment.RecurrenceRule = rule
            appointment.Category = "Red"
            schedule.Appointments.Add(appointment)

			' Swicth schedule to week view mode
			schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Week
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRecurrenceStressTestExample.
		''' </summary>
		Public Shared ReadOnly NRecurrenceStressTestExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
