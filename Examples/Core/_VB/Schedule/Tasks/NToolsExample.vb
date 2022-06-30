Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NToolsExample
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
            Nevron.Nov.Examples.Schedule.NToolsExample.NToolsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NToolsExample), NExampleBaseSchema)
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
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

			' Add a check box for each tool of the schedule view
			Dim toolsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To Me.m_ScheduleView.Interactor.Count - 1
                Dim tool As Nevron.Nov.UI.NTool = Me.m_ScheduleView.Interactor(i)
                Dim checkBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox(tool.ToString(), tool.Enabled)
                checkBox.Tag = tool
                AddHandler checkBox.CheckedChanged, AddressOf Me.OnCheckBoxCheckedChanged
                toolsStack.Add(checkBox)
            Next

            stack.Add(New Nevron.Nov.UI.NGroupBox("Tools", toolsStack))

			' Add a setting for the mouse button event of the Click Select tool
			Dim clickSelectStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim clickSelectTool As Nevron.Nov.Schedule.NScheduleClickSelectTool = CType(Me.m_ScheduleView.Interactor.GetTool(Nevron.Nov.Schedule.NScheduleClickSelectTool.NScheduleClickSelectToolSchema), Nevron.Nov.Schedule.NScheduleClickSelectTool)
            clickSelectStack.Add(Nevron.Nov.Editors.NDesigner.GetDesigner(CType((clickSelectTool), Nevron.Nov.Dom.NNode)).CreatePropertyEditor(clickSelectTool, Nevron.Nov.Schedule.NScheduleClickSelectTool.MouseButtonEventProperty))
            stack.Add(New Nevron.Nov.UI.NGroupBox("Click Select Tool", clickSelectStack))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    This example demonstrates how to configure, enable and disable schedule view tools.
	Use the widgets on the right to control the tools of the schedule view.
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

		#Region"Event Handlers"

		Private Sub OnCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
			' Enable/Disable the tool based on the new value of the check box's Check property
			Dim tool As Nevron.Nov.UI.NTool = CType(arg.CurrentTargetNode.Tag, Nevron.Nov.UI.NTool)
            tool.Enabled = CBool(arg.NewValue)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NToolsExample.
		''' </summary>
		Public Shared ReadOnly NToolsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
