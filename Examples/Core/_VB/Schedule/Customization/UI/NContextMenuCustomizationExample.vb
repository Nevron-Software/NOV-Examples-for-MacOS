Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.Schedule.Commands
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NContextMenuCustomizationExample
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
            Nevron.Nov.Examples.Schedule.NContextMenuCustomizationExample.NContextMenuCustomizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NContextMenuCustomizationExample), NExampleBaseSchema)
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

			' Add the custom command action to the schedule view's commander
			Me.m_ScheduleView.Commander.Add(New Nevron.Nov.Examples.Schedule.NContextMenuCustomizationExample.CustomCommandAction())

			' Change the context menu factory to the custom one
			Me.m_ScheduleView.ContextMenu = New Nevron.Nov.Examples.Schedule.NContextMenuCustomizationExample.CustomContextMenu()

			' Return the commanding widget
			Return scheduleViewWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to customize the NOV schedule context menu. A custom command is added
at the end of the context menu.</p>
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
		''' Schema associated with NContextMenuCustomizationExample.
		''' </summary>
		Public Shared ReadOnly NContextMenuCustomizationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Public Shared ReadOnly CustomCommand As Nevron.Nov.UI.NCommand = Nevron.Nov.UI.NCommand.Create(GetType(Nevron.Nov.Examples.Schedule.NContextMenuCustomizationExample), "CustomCommand", "Custom Command")

		#EndRegion

		#Region"Nested Types"

		Public Class CustomContextMenu
            Inherits Nevron.Nov.Schedule.NScheduleContextMenu
			''' <summary>
			''' Default constructor.
			''' </summary>
			Public Sub New()
            End Sub
			''' <summary>
			''' Static constructor.
			''' </summary>
			Shared Sub New()
                Nevron.Nov.Examples.Schedule.NContextMenuCustomizationExample.CustomContextMenu.CustomContextMenuSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NContextMenuCustomizationExample.CustomContextMenu), Nevron.Nov.Schedule.NScheduleContextMenu.NScheduleContextMenuSchema)
            End Sub

            Protected Overrides Sub CreateCustomCommands(ByVal menu As Nevron.Nov.UI.NMenu)
                MyBase.CreateCustomCommands(menu)

				' Create a context menu builder
				Dim builder As Nevron.Nov.UI.NContextMenuBuilder = New Nevron.Nov.UI.NContextMenuBuilder()

				' Add a custom command
				builder.AddMenuItem(menu, Nevron.Nov.Schedule.NResources.Image_Ribbon_16x16_smiley_png, Nevron.Nov.Examples.Schedule.NContextMenuCustomizationExample.CustomCommand)
            End Sub

			''' <summary>
			''' Schema associated with CustomContextMenu.
			''' </summary>
			Public Shared ReadOnly CustomContextMenuSchema As Nevron.Nov.Dom.NSchema
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
                Nevron.Nov.Examples.Schedule.NContextMenuCustomizationExample.CustomCommandAction.CustomCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NContextMenuCustomizationExample.CustomCommandAction), Nevron.Nov.Schedule.Commands.NScheduleCommandAction.NScheduleCommandActionSchema)
            End Sub

			#EndRegion

			#Region"Public Overrides"

			''' <summary>
			''' Gets the command associated with this command action.
			''' </summary>
			''' <returns></returns>
			Public Overrides Function GetCommand() As Nevron.Nov.UI.NCommand
                Return Nevron.Nov.Examples.Schedule.NContextMenuCustomizationExample.CustomCommand
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
