Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NOccurrencesModificationExample
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
            Nevron.Nov.Examples.Schedule.NOccurrencesModificationExample.NOccurrencesModificationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NOccurrencesModificationExample), NExampleBaseSchema)
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
    This example demonstrates how to create recurring appointments, i.e. appointments, which occur multiple
	times. Recurring appointments are created in the same way as ordinary appointments with the only difference
	that they have a recurrence rule assigned to their <b>RecurrenceRule</b> property and can be easily
	recognized by the circular arrows symbol at their top left corner.
</p>
<p>
	This examples also shows how to change the properties or delete an occurrence of a recurring appointment.
	By default occurrences of recurring appointments inherit the property values of the recurring appointment,
	but you can easily change any of these properties for a specific occurrence. All changes to such occurrences
	are remembered, so even if you move to the previous or the next range of the schedule, when you get back
	to the current range, you'll be able to see all these changes again.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
            Dim today As System.DateTime = System.DateTime.Today

			' Create a recurring appointment, which occurs every day
			Dim appointment As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment("Appointment", today.AddHours(12), today.AddHours(14))
            Dim rule As Nevron.Nov.Schedule.NRecurrenceDailyRule = New Nevron.Nov.Schedule.NRecurrenceDailyRule()
            rule.StartDate = New System.DateTime(2015, 1, 1)
            appointment.RecurrenceRule = rule

			' Add the recurring appointment to the schedule
			schedule.Appointments.Add(appointment)

			' Change the time of the first appointment in the current week
			Dim appChild As Nevron.Nov.Schedule.NAppointmentBase = appointment.Occurrences(0)
            appChild.Start = appChild.Start.AddHours(-2)
            appChild.[End] = appChild.[End].AddHours(-2)

			' Change the subject of the second appointment
			appChild = appointment.Occurrences(1)
            appChild.Subject = "Custom Subject"

			' Change the category of the third appointment
			appChild = appointment.Occurrences(2)
            appChild.Category = "Red"

			' Delete the fourth appointment
			appointment.Occurrences.RemoveAt(3)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NOccurrencesModificationExample.
		''' </summary>
		Public Shared ReadOnly NOccurrencesModificationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
