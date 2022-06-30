Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.Schedule.Commands
Imports Nevron.Nov.Schedule.UI
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NRibbonCustomizationExample
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
            Nevron.Nov.Examples.Schedule.NRibbonCustomizationExample.NRibbonCustomizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NRibbonCustomizationExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a simple schedule
			Me.m_ScheduleView = New Nevron.Nov.Schedule.NScheduleView()
            Me.m_ScheduleView.Document.PauseHistoryService()

            Try
                Me.InitSchedule(Me.m_ScheduleView.Content)
            Finally
                Me.m_ScheduleView.Document.ResumeHistoryService()
            End Try

			' Add the custom command action to the schedule view's commander
			Me.m_ScheduleView.Commander.Add(New Nevron.Nov.Examples.Schedule.NRibbonCustomizationExample.CustomCommandAction())

			' Create and configure a ribbon UI builder
			Dim ribbonBuilder As Nevron.Nov.Schedule.NScheduleRibbonBuilder = New Nevron.Nov.Schedule.NScheduleRibbonBuilder()

			' Rename the "Home" ribbon tab page
			Dim homeTabBuilder As Nevron.Nov.UI.NRibbonTabPageBuilder = ribbonBuilder.TabPageBuilders(Nevron.Nov.Schedule.NScheduleRibbonBuilder.TabPageHomeName)
            homeTabBuilder.Name = "Start"

			' Rename the "Font" ribbon group of the "Home" tab page
			Dim fontGroupBuilder As Nevron.Nov.UI.NRibbonGroupBuilder = homeTabBuilder.RibbonGroupBuilders(Nevron.Nov.Schedule.UI.NHomeTabPageBuilder.GroupViewName)
            fontGroupBuilder.Name = "Look"

			' Remove the "Editing" ribbon group from the "Home" tab page
			homeTabBuilder.RibbonGroupBuilders.Remove(Nevron.Nov.Schedule.UI.NHomeTabPageBuilder.GroupEditingName)

			' Insert the custom ribbon group at the beginning of the home tab page
			homeTabBuilder.RibbonGroupBuilders.Insert(0, New Nevron.Nov.Examples.Schedule.NRibbonCustomizationExample.CustomRibbonGroupBuilder())
            Return ribbonBuilder.CreateUI(Me.m_ScheduleView)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to customize the NOV schedule ribbon.</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
            Dim start As System.DateTime = System.DateTime.Now

			' Create an appointment
			Dim appointment As Nevron.Nov.Schedule.NAppointment = New Nevron.Nov.Schedule.NAppointment("Meeting", start, start.AddHours(2))
            schedule.Appointments.Add(appointment)
            schedule.ScrollToTime(start.TimeOfDay)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRibbonCustomizationExample.
		''' </summary>
		Public Shared ReadOnly NRibbonCustomizationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Public Shared ReadOnly CustomCommand As Nevron.Nov.UI.NCommand = Nevron.Nov.UI.NCommand.Create(GetType(Nevron.Nov.Examples.Schedule.NRibbonCustomizationExample), "CustomCommand", "Custom Command")

		#EndRegion

		#Region"Nested Types"

		Public Class CustomRibbonGroupBuilder
            Inherits Nevron.Nov.UI.NRibbonGroupBuilder

            Public Sub New()
                MyBase.New("Custom Group", Nevron.Nov.Schedule.NResources.Image_Ribbon_16x16_smiley_png)
            End Sub

            Protected Overrides Sub AddRibbonGroupItems(ByVal items As Nevron.Nov.UI.NRibbonGroupItemCollection)
				' Add the "Add Appointment" command
				items.Add(MyBase.CreateRibbonButton(Nevron.Nov.Schedule.NResources.Image_Ribbon_32x32_AddAppointment_png, Nevron.Nov.Schedule.NResources.Image_Edit_AddAppointment_png, Nevron.Nov.Schedule.NScheduleView.AddAppointmentCommand))

				' Add the custom command
				items.Add(CreateRibbonButton(Nevron.Nov.Schedule.NResources.Image_Ribbon_32x32_smiley_png, Nevron.Nov.Schedule.NResources.Image_Ribbon_16x16_smiley_png, Nevron.Nov.Examples.Schedule.NRibbonCustomizationExample.CustomCommand))
            End Sub
        End Class

        Public Class CustomCommandAction
            Inherits Nevron.Nov.Schedule.Commands.NScheduleCommandAction
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
                Nevron.Nov.Examples.Schedule.NRibbonCustomizationExample.CustomCommandAction.CustomCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NRibbonCustomizationExample.CustomCommandAction), Nevron.Nov.Schedule.Commands.NScheduleCommandAction.NScheduleCommandActionSchema)
            End Sub

			#EndRegion

			#Region"Public Overrides"

			''' <summary>
			''' Gets the command associated with this command action.
			''' </summary>
			''' <returns></returns>
			Public Overrides Function GetCommand() As Nevron.Nov.UI.NCommand
                Return Nevron.Nov.Examples.Schedule.NRibbonCustomizationExample.CustomCommand
            End Function
			''' <summary>
			''' Executes the command action.
			''' </summary>
			''' <paramname="target"></param>
			''' <paramname="parameter"></param>
			Public Overrides Sub Execute(ByVal target As Nevron.Nov.Dom.NNode, ByVal parameter As Object)
                Dim scheduleView As Nevron.Nov.Schedule.NScheduleView = MyBase.GetView(target)
                Call Nevron.Nov.UI.NMessageBox.Show("Schedule Custom Command executed!", "Custom Command", Nevron.Nov.UI.ENMessageBoxButtons.OK, Nevron.Nov.UI.ENMessageBoxIcon.Information)
            End Sub

			#EndRegion

			#Region"Schema"

			''' <summary>
			''' Schema associated with CustomCommandAction.
			''' </summary>
			Public Shared ReadOnly CustomCommandActionSchema As Nevron.Nov.Dom.NSchema

			#EndRegion
		End Class

		#EndRegion
	End Class
End Namespace
