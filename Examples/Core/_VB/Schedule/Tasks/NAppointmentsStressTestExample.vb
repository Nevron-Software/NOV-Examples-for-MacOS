Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Globalization
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NAppointmentsStressTestExample
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
            Nevron.Nov.Examples.Schedule.NAppointmentsStressTestExample.NAppointmentsStressTestExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NAppointmentsStressTestExample), NExampleBaseSchema)
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
            Return System.[String].Format(Nevron.Nov.Globalization.NCultureInfo.EnglishUS, "
<p>
    This example demonstrates how NOV Schedule handles {0:N0} appointments.
</p>
", Nevron.Nov.Examples.Schedule.NAppointmentsStressTestExample.TotalAppointments)
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
            Me.m_Random = New System.Random()
            Dim totalDays As Integer = Nevron.Nov.Examples.Schedule.NAppointmentsStressTestExample.TotalAppointments / Nevron.Nov.Examples.Schedule.NAppointmentsStressTestExample.AppointmentsPerDay
            Dim [date] As System.DateTime = System.DateTime.Today.AddDays(-totalDays / 2)

			' Generate the random appointments
			Dim appointments As Nevron.Nov.Schedule.NAppointmentCollection = schedule.Appointments

            For i As Integer = 0 To totalDays - 1
                Me.AddRandomAppointments(appointments, [date])
                [date] = [date].AddDays(1)
            Next

			' Switch the schedule to week view
			schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Week
        End Sub
		''' <summary>
		''' Adds some random appointments for the given date.
		''' </summary>
		''' <paramname="appointments"></param>
		''' <paramname="date"></param>
		Private Sub AddRandomAppointments(ByVal appointments As Nevron.Nov.Schedule.NAppointmentCollection, ByVal [date] As System.DateTime)
            For i As Integer = 0 To Nevron.Nov.Examples.Schedule.NAppointmentsStressTestExample.AppointmentsPerDay - 1
				' Generate random subject
				Dim subject As String = Nevron.Nov.Examples.Schedule.NAppointmentsStressTestExample.AppointmentSubjects(Me.m_Random.[Next](0, Nevron.Nov.Examples.Schedule.NAppointmentsStressTestExample.AppointmentSubjects.Length))

				' Generate random start hour from 0 to 24
				Dim startHour As Double = Me.m_Random.NextDouble() * 24

				' Generate random duration from 0.5 to 2.5 hours
				Dim duration As Double = 0.5 + Me.m_Random.NextDouble() * 2

				' Create and add the appointment
				Dim appointment As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment(subject, [date].AddHours(startHour), [date].AddHours(startHour + duration))
                appointments.Add(appointment)
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView
        Private m_Random As System.Random

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NAppointmentsStressTestExample.
		''' </summary>
		Public Shared ReadOnly NAppointmentsStressTestExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Const AppointmentsPerDay As Integer = 20
        Private Const TotalAppointments As Integer = 100000
        Private Shared ReadOnly AppointmentSubjects As String() = New String() {"Travel to Work", "Meeting with John", "Conference", "Lunch", "News Reading", "Video Presentation", "Web Meeting", "Travel back home", "Family Dinner"}

		#EndRegion
	End Class
End Namespace
