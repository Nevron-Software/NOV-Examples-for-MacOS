Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NFixedTimeIndicatorsExample
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
            Nevron.Nov.Examples.Schedule.NFixedTimeIndicatorsExample.NFixedTimeIndicatorsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NFixedTimeIndicatorsExample), NExampleBaseSchema)
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
            Dim timeIndicators As Nevron.Nov.Schedule.NFixedTimeIndicatorCollection = Me.m_ScheduleView.Content.TimeIndicators

            For i As Integer = 0 To timeIndicators.Count - 1
                Dim timeIndicator As Nevron.Nov.Schedule.NTimeIndicator = timeIndicators(i)
                Dim propertyEditors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((timeIndicator), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(timeIndicator, Nevron.Nov.Schedule.NTimeIndicator.VisibleProperty, Nevron.Nov.Schedule.NTimeIndicator.FillProperty, Nevron.Nov.Schedule.NTimeIndicator.ThicknessProperty, Nevron.Nov.Schedule.NFixedTimeIndicator.TimeProperty)
                Dim currentStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

                For j As Integer = 0 To propertyEditors.Count - 1
                    currentStack.Add(propertyEditors(j))
                Next

                stack.Add(New Nevron.Nov.UI.NGroupBox($"Time Indicator {i + 1}", New Nevron.Nov.UI.NUniSizeBoxGroup(currentStack)))
            Next

            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    This example demonstrates how to show fixed time indicator in the schedule. Fixed schedule time indicators mark specific points
	in time on the schedule. The following properties control the appearance and position of the fixed time indicators:
</p>
<ul>
	<li><b>Time</b> - the point in time to paint the fixed time indicator at.</li>
	<li><b>Visible</b> - determines whether to show the time indicator.</li>
	<li><b>Fill</b> - determines the fill style of the time indicator. For best results set it to a semi-transparent color fill,
		for example - new NColorFill(new NColor(192, 0, 0, 160)).</li>
	<li><b>Thickness</b> - determines the thickness of the time indicator in DIPs. By default set to 2.</li>
</ul>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
            Dim today As System.DateTime = System.DateTime.Today
            schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Day

			' Add some appointments
			schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Travel to Work", today.AddHours(6.5), today.AddHours(7.5)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Meeting with John", today.AddHours(8), today.AddHours(10)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Conference", today.AddHours(10.5), today.AddHours(11.5)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Lunch", today.AddHours(12), today.AddHours(14)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("News Reading", today.AddHours(12.5), today.AddHours(13.5)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Video Presentation", today.AddHours(14.5), today.AddHours(15.5)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Web Meeting", today.AddHours(16), today.AddHours(17)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Travel back home", today.AddHours(17.5), today.AddHours(19)))
            schedule.Appointments.Add(New Nevron.Nov.Schedule.NAppointment("Family Dinner", today.AddHours(20), today.AddHours(21)))

			' Add two fixed time indicators
			schedule.TimeIndicators.Add(New Nevron.Nov.Schedule.NFixedTimeIndicator(System.DateTime.Now.AddHours(-1), New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.MediumBlue, 160)))
            schedule.TimeIndicators.Add(New Nevron.Nov.Schedule.NFixedTimeIndicator(System.DateTime.Now.AddHours(1), New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.DarkGreen, 160)))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NFixedTimeIndicatorsExample.
		''' </summary>
		Public Shared ReadOnly NFixedTimeIndicatorsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
