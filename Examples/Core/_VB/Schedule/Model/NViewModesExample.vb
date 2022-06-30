Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NViewModesExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor.
		''' </summary>
		Public Sub New()
            Me.m_bViewModeChanging = False
        End Sub

		''' <summary>
		''' Static constructor.
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Schedule.NViewModesExample.NViewModesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NViewModesExample), NExampleBaseSchema)
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
            Dim schedule As Nevron.Nov.Schedule.NSchedule = Me.m_ScheduleView.Content
            AddHandler schedule.ViewModeChanged, AddressOf Me.OnScheduleViewModeChanged

			' Add the view mode property editor, i.e. a combo box for selecting the schedule's active view mode
			Dim propertyEditor As Nevron.Nov.Editors.NPropertyEditor = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((schedule), Nevron.Nov.Dom.NNode)).CreatePropertyEditor(schedule, Nevron.Nov.Schedule.NSchedule.ViewModeProperty)
            propertyEditor.Margins = New Nevron.Nov.Graphics.NMargins(0, Nevron.Nov.NDesign.VerticalSpacing, 0, 0)
            stack.Add(propertyEditor)
		
			' Add a width numeric up down
			Me.m_WidthUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_WidthUpDown.Value = schedule.Width
            Me.m_WidthUpDown.Minimum = 100
            Me.m_WidthUpDown.[Step] = 10
            AddHandler Me.m_WidthUpDown.ValueChanged, AddressOf Me.OnWidthUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Width", Me.m_WidthUpDown))

			' Add a height numeric up down
			Me.m_HeightUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_HeightUpDown.Value = schedule.Height
            Me.m_HeightUpDown.Minimum = 100
            Me.m_HeightUpDown.[Step] = 10
            AddHandler Me.m_HeightUpDown.ValueChanged, AddressOf Me.OnHeightUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Height", Me.m_HeightUpDown))

			' Add a duration numeric up down
			Me.m_DurationUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_DurationUpDown.Value = schedule.ActiveViewMode.Duration
            Me.m_DurationUpDown.Minimum = 1
            Me.m_DurationUpDown.Enabled = False
            AddHandler Me.m_DurationUpDown.ValueChanged, AddressOf Me.OnDurationUpDownValueChanged
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(Me.m_DurationUpDown, "days")
            pairBox.Spacing = Nevron.Nov.NDesign.HorizontalSpacing
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Duration", pairBox))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    This example demonstrates how to change the active view mode of a schedule and how to modify the size of any view mode.
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

		Private Sub OnScheduleViewModeChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_bViewModeChanging = True
            Dim schedule As Nevron.Nov.Schedule.NSchedule = Me.m_ScheduleView.Content
            Me.m_WidthUpDown.Value = schedule.Width
            Me.m_HeightUpDown.Value = schedule.Height
            Me.m_DurationUpDown.Value = schedule.ActiveViewMode.Duration

			' Enable the duration numeric up down only when the timeline view mode is selected
			Me.m_DurationUpDown.Enabled = CType(arg.NewValue, Nevron.Nov.Schedule.ENScheduleViewMode) = Nevron.Nov.Schedule.ENScheduleViewMode.Timeline
            Me.m_bViewModeChanging = False
        End Sub

        Private Sub OnWidthUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Not Me.m_bViewModeChanging Then
                Me.m_ScheduleView.Content.ActiveViewMode.Width = CDbl(arg.NewValue)
            End If
        End Sub

        Private Sub OnHeightUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Not Me.m_bViewModeChanging Then
                Me.m_ScheduleView.Content.ActiveViewMode.Height = CDbl(arg.NewValue)
            End If
        End Sub

        Private Sub OnDurationUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Not Me.m_bViewModeChanging Then
                Dim timelineViewMode As Nevron.Nov.Schedule.NTimelineViewMode = CType(Me.m_ScheduleView.Content.ActiveViewMode, Nevron.Nov.Schedule.NTimelineViewMode)
                Dim durationInDays As Integer = CInt(CDbl(arg.NewValue))
                timelineViewMode.SetDuration(durationInDays)
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_bViewModeChanging As Boolean
        Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView
        Private m_WidthUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_HeightUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_DurationUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NViewModesExample.
		''' </summary>
		Public Shared ReadOnly NViewModesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
