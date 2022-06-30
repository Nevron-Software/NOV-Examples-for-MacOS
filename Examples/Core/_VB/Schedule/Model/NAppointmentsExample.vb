Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NAppointmentsExample
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
            Nevron.Nov.Examples.Schedule.NAppointmentsExample.NAppointmentsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NAppointmentsExample), NExampleBaseSchema)
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
    This example demonstrates how to create and add appointments to a schedule.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
            Dim today As System.DateTime = System.DateTime.Today
            schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Day
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Travel to Work", today.AddHours(6.5), today.AddHours(7.5)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Meeting with John", today.AddHours(8), today.AddHours(10)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Conference", today.AddHours(10.5), today.AddHours(11.5)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Lunch", today.AddHours(12), today.AddHours(14)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("News Reading", today.AddHours(12.5), today.AddHours(13.5)))
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
		''' Schema associated with NAppointmentsExample.
		''' </summary>
		Public Shared ReadOnly NAppointmentsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
