Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NNotificationsExample
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
            Nevron.Nov.Examples.Schedule.NNotificationsExample.NNotificationsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NNotificationsExample), NExampleBaseSchema)
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
    This example demonstrates how to assign notifications to appointments and how to configure NOV Schedule to
	show notification messages.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
            schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Day

			' Create an old appointment
			Dim oldStart As System.DateTime = System.DateTime.Now.AddHours(-3)
            Dim oldAppointment As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment("Old Meeting", oldStart, oldStart.AddHours(2))
            oldAppointment.Notification = System.TimeSpan.Zero
            schedule.Appointments.Add(oldAppointment)

			' Create an appointment and assign a notification 10 minutes before its start
			Dim newStart As System.DateTime = System.DateTime.Now.AddMinutes(10)
            Dim newAppointment As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment("New Meeting", newStart, newStart.AddHours(2))
            newAppointment.Notification = System.TimeSpan.FromMinutes(10)
            schedule.Appointments.Add(newAppointment)

			' Scroll the schedule to the current hour
			schedule.ScrollToTime(System.TimeSpan.FromHours(System.Math.Floor(CDbl(oldStart.Hour))))

			' Configure the schedule view to check for pending notifications every 60 seconds
			Me.m_ScheduleView.NotificationCheckInterval = 60
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NNotificationsExample.
		''' </summary>
		Public Shared ReadOnly NNotificationsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
