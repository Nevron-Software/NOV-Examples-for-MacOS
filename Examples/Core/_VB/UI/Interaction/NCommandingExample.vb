Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NCommandingExample
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
            Nevron.Nov.Examples.UI.NCommandingExample.NCommandingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NCommandingExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
This example demonstrates how to create auto sizable and auto centered windows with expressions.
</p>
"
        End Function

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCommandingExample.
		''' </summary>
		Public Shared ReadOnly NCommandingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		Public Class MyCommandableWidget
            Inherits Nevron.Nov.UI.NWidget

            Public Sub New()
				' TODO: initialize commander here
			End Sub

            Shared Sub New()
                Nevron.Nov.Examples.UI.NCommandingExample.MyCommandableWidget.MyCommandableWidgetSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NCommandingExample.MyCommandableWidget), Nevron.Nov.UI.NWidget.NWidgetSchema)
				' create a command that is associated with the Ctrl+T shortcut
				Nevron.Nov.Examples.UI.NCommandingExample.MyCommandableWidget.MyActionCommand = Nevron.Nov.UI.NCommand.Create(GetType(Nevron.Nov.Examples.UI.NCommandingExample.MyCommandableWidget), "MyActionCommand", "Sets a contstant text", New Nevron.Nov.UI.NShortcut(New Nevron.Nov.UI.NKey(Nevron.Nov.UI.ENKeyCode.T), Nevron.Nov.UI.ENModifierKeys.Control))
                Nevron.Nov.Examples.UI.NCommandingExample.MyCommandableWidget.MyToggleCommand = Nevron.Nov.UI.NCommand.Create(GetType(Nevron.Nov.Examples.UI.NCommandingExample.MyCommandableWidget), "MyToggleCommand", "Toggles the text fill", New Nevron.Nov.UI.NShortcut(New Nevron.Nov.UI.NKey(Nevron.Nov.UI.ENKeyCode.R), Nevron.Nov.UI.ENModifierKeys.Control))
            End Sub

            Public Shared ReadOnly MyCommandableWidgetSchema As Nevron.Nov.Dom.NSchema
            Public Shared ReadOnly MyActionCommand As Nevron.Nov.UI.NCommand
            Public Shared ReadOnly MyToggleCommand As Nevron.Nov.UI.NCommand
        End Class
    End Class
End Namespace
