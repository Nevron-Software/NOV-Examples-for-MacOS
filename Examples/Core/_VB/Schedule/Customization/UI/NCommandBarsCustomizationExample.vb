Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.Schedule.Commands
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NCommandBarsCustomizationExample
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
            Nevron.Nov.Examples.Schedule.NCommandBarsCustomizationExample.NCommandBarsCustomizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NCommandBarsCustomizationExample), NExampleBaseSchema)
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
			Me.m_ScheduleView.Commander.Add(New Nevron.Nov.Examples.Schedule.NCommandBarsCustomizationExample.CustomCommandAction())

			' Create and configure command bars UI builder
			' Remove the "Edit" menu and insert a custom one
			Dim commandBarBuilder As Nevron.Nov.Schedule.NScheduleCommandBarBuilder = New Nevron.Nov.Schedule.NScheduleCommandBarBuilder()
            commandBarBuilder.MenuDropDownBuilders.Remove(Nevron.Nov.Schedule.NScheduleCommandBarBuilder.MenuEditName)
            commandBarBuilder.MenuDropDownBuilders.Insert(1, New Nevron.Nov.Examples.Schedule.NCommandBarsCustomizationExample.CustomMenuBuilder())

			' Remove the "Standard" toolbar and insert a custom one
			commandBarBuilder.ToolBarBuilders.Remove(Nevron.Nov.Schedule.NScheduleCommandBarBuilder.ToolbarStandardName)
            commandBarBuilder.ToolBarBuilders.Insert(0, New Nevron.Nov.Examples.Schedule.NCommandBarsCustomizationExample.CustomToolBarBuilder())
            Return commandBarBuilder.CreateUI(Me.m_ScheduleView)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to customize the NOV schedule command bars (menus and toolbars).</p>
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
		''' Schema associated with NCommandBarsCustomizationExample.
		''' </summary>
		Public Shared ReadOnly NCommandBarsCustomizationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Public Shared ReadOnly CustomCommand As Nevron.Nov.UI.NCommand = Nevron.Nov.UI.NCommand.Create(GetType(Nevron.Nov.Examples.Schedule.NCommandBarsCustomizationExample), "CustomCommand", "Custom Command")

		#EndRegion

		#Region"Nested Types"

		Public Class CustomMenuBuilder
            Inherits Nevron.Nov.UI.NMenuDropDownBuilder

            Public Sub New()
                MyBase.New("Custom Menu")
            End Sub

            Protected Overrides Sub AddItems(ByVal items As Nevron.Nov.UI.NMenuItemCollection)
				' Add the "Add Appointment" menu item
				items.Add(MyBase.CreateMenuItem(Nevron.Nov.Schedule.NResources.Image_Edit_AddAppointment_png, Nevron.Nov.Schedule.NScheduleView.AddAppointmentCommand))

				' Add the custom command menu item
				items.Add(CreateMenuItem(Nevron.Nov.Schedule.NResources.Image_Ribbon_16x16_smiley_png, Nevron.Nov.Examples.Schedule.NCommandBarsCustomizationExample.CustomCommand))
            End Sub
        End Class

        Public Class CustomToolBarBuilder
            Inherits Nevron.Nov.UI.NToolBarBuilder

            Public Sub New()
                MyBase.New("Custom Toolbar")
            End Sub

            Protected Overrides Sub AddItems(ByVal items As Nevron.Nov.UI.NCommandBarItemCollection)
				' Add the "Add Appointment" button
				items.Add(MyBase.CreateButton(Nevron.Nov.Schedule.NResources.Image_Edit_AddAppointment_png, Nevron.Nov.Schedule.NScheduleView.AddAppointmentCommand))

				' Add the custom command button
				items.Add(CreateButton(Nevron.Nov.Schedule.NResources.Image_Ribbon_16x16_smiley_png, Nevron.Nov.Examples.Schedule.NCommandBarsCustomizationExample.CustomCommand))
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
                Nevron.Nov.Examples.Schedule.NCommandBarsCustomizationExample.CustomCommandAction.CustomCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NCommandBarsCustomizationExample.CustomCommandAction), Nevron.Nov.Schedule.Commands.NScheduleCommandAction.NScheduleCommandActionSchema)
            End Sub

			#EndRegion

			#Region"Public Overrides"

			''' <summary>
			''' Gets the command associated with this command action.
			''' </summary>
			''' <returns></returns>
			Public Overrides Function GetCommand() As Nevron.Nov.UI.NCommand
                Return Nevron.Nov.Examples.Schedule.NCommandBarsCustomizationExample.CustomCommand
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
