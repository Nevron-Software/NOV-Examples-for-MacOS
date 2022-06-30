Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NInteractivityExample
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
            Nevron.Nov.Examples.Schedule.NInteractivityExample.NReadOnlyOrDisabledScheduleViewExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NInteractivityExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Examples"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a simple schedule
			Dim scheduleViewWithRibbon As Nevron.Nov.Schedule.NScheduleViewWithRibbon = New Nevron.Nov.Schedule.NScheduleViewWithRibbon()
            Me.m_ScheduleView = scheduleViewWithRibbon.View
            AddHandler Me.m_ScheduleView.EnabledChanged, AddressOf Me.OnScheduleViewEnabledOrReadOnlyChanged
            AddHandler Me.m_ScheduleView.ReadOnlyChanged, AddressOf Me.OnScheduleViewEnabledOrReadOnlyChanged
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

			' Add schedule view property editors
			Dim propertyEditors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ScheduleView), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_ScheduleView, Nevron.Nov.Schedule.NScheduleView.EnabledProperty, Nevron.Nov.Schedule.NScheduleView.ReadOnlyProperty)
            Dim scheduleViewStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To propertyEditors.Count - 1
                scheduleViewStack.Add(propertyEditors(i))
            Next

            stack.Add(New Nevron.Nov.UI.NGroupBox("Schedule View", New Nevron.Nov.UI.NUniSizeBoxGroup(scheduleViewStack)))

			' Add schedule property editors
			Dim infoLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Taken into account only if the schedule view is enabled and not read only")
            infoLabel.TextWrapMode = Nevron.Nov.Graphics.ENTextWrapMode.WordWrap
            Dim schedule As Nevron.Nov.Schedule.NSchedule = Me.m_ScheduleView.Content
            Me.m_InteractivityEditor = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((schedule), Nevron.Nov.Dom.NNode)).CreatePropertyEditor(schedule, Nevron.Nov.Schedule.NSchedule.InteractivityProperty)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Schedule - Interactivity", New Nevron.Nov.UI.NPairBox(infoLabel, Me.m_InteractivityEditor, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2)))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    This example demonstrates how a read only or disabled schedule view behaves. Use the check boxes on the right
	to enable/disable or make the schedule view (NScheduleView) read only.
</p>
<p>
	If the schedule view is enabled and not read only, you can use the check boxes in the ""Interactivity"" group box
	on the right to enable/disable individual schedule interactivity features. These check boxes modify the <b>Interactivity</b>
	property of the schedule (NSchedule). The schedule interactivity can also be controlled from the ""Interactivity"" group of the
	schedule's ""Settings"" ribbon tab.
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

		Private Sub OnScheduleViewEnabledOrReadOnlyChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Me.m_InteractivityEditor IsNot Nothing Then
                Dim scheduleView As Nevron.Nov.Schedule.NScheduleView = CType(arg.CurrentTargetNode, Nevron.Nov.Schedule.NScheduleView)
                Me.m_InteractivityEditor.Enabled = scheduleView.Enabled AndAlso Not scheduleView.[ReadOnly]
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView
        Private m_InteractivityEditor As Nevron.Nov.Editors.NPropertyEditor

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NReadOnlyOrDisabledScheduleViewExample.
		''' </summary>
		Public Shared ReadOnly NReadOnlyOrDisabledScheduleViewExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
