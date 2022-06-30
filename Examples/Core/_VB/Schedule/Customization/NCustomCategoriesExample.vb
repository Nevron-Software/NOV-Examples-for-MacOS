Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NCustomCategoriesExample
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
            Nevron.Nov.Examples.Schedule.NCustomCategoriesExample.NCustomCategoriesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NCustomCategoriesExample), NExampleBaseSchema)
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
            Return "<p>This example demonstrates how to create and use custom categories.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
			' Rename the first predefined category
			schedule.Categories(CInt((0))).Name = Nevron.Nov.NLoc.[Get]("Renamed Category")

			' Create and add some custom categories
			Dim category1 As Nevron.Nov.Schedule.NCategory = New Nevron.Nov.Schedule.NCategory(Nevron.Nov.NLoc.[Get]("Custom Category 1"), Nevron.Nov.Graphics.NColor.Khaki)
            schedule.Categories.Add(category1)
            Dim category2 As Nevron.Nov.Schedule.NCategory = New Nevron.Nov.Schedule.NCategory(Nevron.Nov.NLoc.[Get]("Custom Category 2"), New Nevron.Nov.Graphics.NHatchFill(Nevron.Nov.Graphics.ENHatchStyle.DiagonalBrick, Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.Orange))
            schedule.Categories.Add(category2)

			' Remove the second category
			schedule.Categories.RemoveAt(1)

			' Remove the category called "Green"
			Dim categoryToRemove As Nevron.Nov.Schedule.NCategory = schedule.Categories.FindByName(Nevron.Nov.NLoc.[Get]("Green"))

            If categoryToRemove IsNot Nothing Then
                schedule.Categories.Remove(categoryToRemove)
            End If

			' Create and add some appointments
			Dim start As System.DateTime = System.DateTime.Now
            Dim appointment1 As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment("Meeting 1", start, start.AddHours(2))
            appointment1.Category = category1.Name
            schedule.Appointments.Add(appointment1)
            Dim appointment2 As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment("Meeting 2", appointment1.[End].AddHours(0.5), appointment1.[End].AddHours(2.5))
            appointment2.Category = category2.Name
            schedule.Appointments.Add(appointment2)

			' Scroll the schedule to the start of the first appointment
			schedule.ScrollToTime(start.TimeOfDay)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCustomCategoriesExample.
		''' </summary>
		Public Shared ReadOnly NCustomCategoriesExampleSchema As Nevron.Nov.Dom.NSchema


		#EndRegion
	End Class
End Namespace
