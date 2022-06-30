Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NRibbonAndCommandBarsExample
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
            Nevron.Nov.Examples.Schedule.NRibbonAndCommandBarsExample.NRibbonAndCommandBarsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NRibbonAndCommandBarsExample), NExampleBaseSchema)
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

			' Create and execute a ribbon UI builder
			Me.m_RibbonBuilder = New Nevron.Nov.Schedule.NScheduleRibbonBuilder()
            Return Me.m_RibbonBuilder.CreateUI(Me.m_ScheduleView)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

			' Switch UI button
			Dim switchUIButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton(Nevron.Nov.Examples.Schedule.NRibbonAndCommandBarsExample.SwitchToCommandBars)
            AddHandler switchUIButton.Click, AddressOf Me.OnSwitchUIButtonClick
            stack.Add(switchUIButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to switch the NOV Schedule commanding interface between ribbon and command bars.</p>"
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

        Private Sub SetUI(ByVal oldUiHolder As Nevron.Nov.UI.NCommandUIHolder, ByVal widget As Nevron.Nov.UI.NWidget)
            If TypeOf oldUiHolder.ParentNode Is Nevron.Nov.UI.NTabPage Then
                CType(oldUiHolder.ParentNode, Nevron.Nov.UI.NTabPage).Content = widget
            ElseIf TypeOf oldUiHolder.ParentNode Is Nevron.Nov.UI.NPairBox Then
                CType(oldUiHolder.ParentNode, Nevron.Nov.UI.NPairBox).Box1 = widget
            End If
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSwitchUIButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim switchUIButton As Nevron.Nov.UI.NButton = CType(arg.TargetNode, Nevron.Nov.UI.NButton)
            Dim label As Nevron.Nov.UI.NLabel = CType(switchUIButton.Content, Nevron.Nov.UI.NLabel)

			' Remove the schedule view from its parent
			Dim uiHolder As Nevron.Nov.UI.NCommandUIHolder = Me.m_ScheduleView.GetFirstAncestor(Of Nevron.Nov.UI.NCommandUIHolder)()
            Me.m_ScheduleView.ParentNode.RemoveChild(Me.m_ScheduleView)

            If Equals(label.Text, Nevron.Nov.Examples.Schedule.NRibbonAndCommandBarsExample.SwitchToRibbon) Then
				' We are in "Command Bars" mode, so switch to "Ribbon"
				label.Text = Nevron.Nov.Examples.Schedule.NRibbonAndCommandBarsExample.SwitchToCommandBars

				' Create the ribbon
				Me.SetUI(uiHolder, Me.m_RibbonBuilder.CreateUI(Me.m_ScheduleView))
            Else
				' We are in "Ribbon" mode, so switch to "Command Bars"
				label.Text = Nevron.Nov.Examples.Schedule.NRibbonAndCommandBarsExample.SwitchToRibbon

				' Create the command bars
				If Me.m_CommandBarBuilder Is Nothing Then
                    Me.m_CommandBarBuilder = New Nevron.Nov.Schedule.NScheduleCommandBarBuilder()
                End If

                Me.SetUI(uiHolder, Me.m_CommandBarBuilder.CreateUI(Me.m_ScheduleView))
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView
        Private m_RibbonBuilder As Nevron.Nov.Schedule.NScheduleRibbonBuilder
        Private m_CommandBarBuilder As Nevron.Nov.Schedule.NScheduleCommandBarBuilder

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRibbonAndCommandBarsSwitchingExample.
		''' </summary>
		Public Shared ReadOnly NRibbonAndCommandBarsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Const SwitchToCommandBars As String = "Switch to Command Bars"
        Private Const SwitchToRibbon As String = "Switch to Ribbon"

		#EndRegion
	End Class
End Namespace
