Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NTimeSpanBoxExample
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
            Nevron.Nov.Examples.UI.NTimeSpanBoxExample.NTimeSpanBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTimeSpanBoxExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim timeSpanBox As Nevron.Nov.UI.NTimeSpanBox = New Nevron.Nov.UI.NTimeSpanBox()
            timeSpanBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            timeSpanBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top

			' Add some time spans to the box
			timeSpanBox.AddItem(System.TimeSpan.MinValue)
            timeSpanBox.AddItem(System.TimeSpan.Zero)
            timeSpanBox.AddItem(System.TimeSpan.FromMinutes(10))
            timeSpanBox.AddItem(System.TimeSpan.FromMinutes(15))
            timeSpanBox.AddItem(System.TimeSpan.FromMinutes(30))
            timeSpanBox.AddItem(System.TimeSpan.FromHours(1))
            timeSpanBox.AddItem(System.TimeSpan.FromHours(1.5))
            timeSpanBox.AddItem(System.TimeSpan.FromHours(2))
            timeSpanBox.AddItem(System.TimeSpan.FromHours(3))
            timeSpanBox.AddItem(System.TimeSpan.FromHours(6))
            timeSpanBox.AddItem(System.TimeSpan.FromHours(8))
            timeSpanBox.AddItem(System.TimeSpan.FromHours(12))
            timeSpanBox.AddItem(System.TimeSpan.FromDays(1))
            timeSpanBox.AddItem(System.TimeSpan.FromDays(1.5))
            timeSpanBox.AddItem(System.TimeSpan.FromDays(2))

			' Add the "Custom..." option
			timeSpanBox.AddCustomItem()

			' Select the first item
			timeSpanBox.SelectedIndex = 0

			' Subscribe to the SelectedIndex changed event
			AddHandler timeSpanBox.SelectedIndexChanged, AddressOf Me.OnTimeSpanBoxSelectedIndexChanged
            Return timeSpanBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Me.m_EventsLog = New NExampleEventsLog()
            Return Me.m_EventsLog
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and configure a time span box. Using the controls on the right you can
	modify various aspects of the time span box.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnTimeSpanBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim timeSpanBox As Nevron.Nov.UI.NTimeSpanBox = CType(arg.TargetNode, Nevron.Nov.UI.NTimeSpanBox)

			' Log the selected time span
			Dim timeSpan As System.TimeSpan = timeSpanBox.SelectedTimeSpan

            If timeSpan <> System.TimeSpan.MinValue Then
                Me.m_EventsLog.LogEvent("Selected time span: " & timeSpan.ToString())
            Else
                Me.m_EventsLog.LogEvent("Selected time span: none")
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NTimeSpanBoxExample.
		''' </summary>
		Public Shared ReadOnly NTimeSpanBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
