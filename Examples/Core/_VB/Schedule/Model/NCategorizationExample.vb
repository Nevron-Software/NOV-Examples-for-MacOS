Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NCategorizationExample
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
            Nevron.Nov.Examples.Schedule.NCategorizationExample.NCategorizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NCategorizationExample), NExampleBaseSchema)
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
    This example demonstrates how to create and add appointments to a schedule and how to categorize them through code.
	To categorize an appointment means to assign a category and/or a time marker to it. The schedule contains a collection
	of default categories and time markers, but you can easily modify them when needed.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
            Dim today As System.DateTime = System.DateTime.Today
            schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Day

			' The categories of the schedule are stored in its Categories collections and by default are:
			' "Orange", "Red", "Blue", "Green", "Purple" and "Yellow"
			' You can either use these names (case sensitive), or obtain them from the Categories collection of the schedule

			' Create an appointment and associate it with the "Red" category
			Dim appointment As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment("Travel to Work", today.AddHours(6.5), today.AddHours(7.5))
            appointment.Category = "Red"
            schedule.Appointments.Add(appointment)

			' Create an appointment and associate it with the first category of the schedule
			appointment = New Nevron.Nov.Schedule.NAppointment("Meeting with John", today.AddHours(8), today.AddHours(10))
            appointment.Category = schedule.Categories(CInt((0))).Name
            schedule.Appointments.Add(appointment)

			' Time markers are similar to categories with the diference that they only color the header of an appointment.
			' The time markers of a schedule are stored in its TimeMarkers collection and by default are:
			' "Free", "Tentative", "Busy" and "Out of Office"
			' You can either use these names (case sensitive), or obtain them from the TmieMarkers collection of the schedule

			' Create an appointment and assign the "Busy" time marker to it
			appointment = New Nevron.Nov.Schedule.NAppointment("Conference", today.AddHours(10.5), today.AddHours(11.5))
            appointment.TimeMarker = "Busy"
            schedule.Appointments.Add(appointment)

			' Create an appointment and assign the first time marker of the schedule to it
			appointment = New Nevron.Nov.Schedule.NAppointment("Lunch", today.AddHours(12), today.AddHours(14))
            appointment.TimeMarker = schedule.TimeMarkers(CInt((0))).Name
            schedule.Appointments.Add(appointment)

			' Create an appointment and assign both a category and a time marker to it
			appointment = New Nevron.Nov.Schedule.NAppointment("News Reading", today.AddHours(12.5), today.AddHours(13.5))
            appointment.Category = "Yellow"
            appointment.TimeMarker = "Tentative"
            schedule.Appointments.Add(appointment)

			' Add some more appointments
			schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Video Presentation", today.AddHours(14.5), today.AddHours(15.5)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Web Meeting", today.AddHours(16), today.AddHours(17)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Travel back home", today.AddHours(17.5), today.AddHours(19)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Family Dinner", today.AddHours(20), today.AddHours(21)))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCategorizationExample.
		''' </summary>
		Public Shared ReadOnly NCategorizationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
