Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NCustomTimeMarkersExample
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
            Nevron.Nov.Examples.Schedule.NCustomTimeMarkersExample.NCustomTimeMarkersExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NCustomTimeMarkersExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_DefaultAppointmentHeaderThickness = Nevron.Nov.Schedule.NScheduleTheme.AppointmentHeaderThickness

			' Change the thickness of time markers from the default 5 DIPs to 20 DIPs
			Nevron.Nov.Schedule.NScheduleTheme.AppointmentHeaderThickness = 20

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
<p>This example demonstrates how to create and use custom time markers. It also shows how to use the
<b>NScheduleTheme.AppointmentHeaderThickness</b> static property to change the thickness of time markers.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
			' Rename the first predefined time marker
			schedule.TimeMarkers(CInt((0))).Name = Nevron.Nov.NLoc.[Get]("Renamed Time Marker")

			' Create and add some custom time markers
			Dim timeMarker1 As Nevron.Nov.Schedule.NTimeMarker = New Nevron.Nov.Schedule.NTimeMarker(Nevron.Nov.NLoc.[Get]("Custom Time Marker 1"), Nevron.Nov.Graphics.NColor.Khaki)
            schedule.TimeMarkers.Add(timeMarker1)
            Dim timeMarker2 As Nevron.Nov.Schedule.NTimeMarker = New Nevron.Nov.Schedule.NTimeMarker(Nevron.Nov.NLoc.[Get]("Custom Time Marker 2"), New Nevron.Nov.Graphics.NHatchFill(Nevron.Nov.Graphics.ENHatchStyle.DiagonalBrick, Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.Orange))
            schedule.TimeMarkers.Add(timeMarker2)

			' Remove the second time marker
			schedule.TimeMarkers.RemoveAt(1)

			' Remove the time marker called "Busy"
			Dim timeMarkerToRemove As Nevron.Nov.Schedule.NTimeMarker = schedule.TimeMarkers.FindByName(Nevron.Nov.NLoc.[Get]("Busy"))

            If timeMarkerToRemove IsNot Nothing Then
                schedule.TimeMarkers.Remove(timeMarkerToRemove)
            End If

			' Create and add some appointments
			Dim start As System.DateTime = System.DateTime.Now
            Dim appointment1 As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment("Meeting 1", start, start.AddHours(2))
            appointment1.TimeMarker = timeMarker1.Name
            schedule.Appointments.Add(appointment1)
            Dim appointment2 As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment("Meeting 2", appointment1.[End].AddHours(0.5), appointment1.[End].AddHours(2.5))
            appointment2.TimeMarker = timeMarker2.Name
            schedule.Appointments.Add(appointment2)

			' Scroll the schedule to the start of the first appointment
			schedule.ScrollToTime(start.TimeOfDay)
        End Sub

		#EndRegion

		#Region"Example Lifetime"

		Protected Friend Overrides Sub OnClosing()
            MyBase.OnClosing()

			' Restore appointment header thickness to its default value
			Nevron.Nov.Schedule.NScheduleTheme.AppointmentHeaderThickness = Me.m_DefaultAppointmentHeaderThickness
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView
        Private m_DefaultAppointmentHeaderThickness As Double

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCustomTimeMarkersExample.
		''' </summary>
		Public Shared ReadOnly NCustomTimeMarkersExampleSchema As Nevron.Nov.Dom.NSchema


		#EndRegion
	End Class
End Namespace
