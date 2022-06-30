Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NLinkLabelExample
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
            Nevron.Nov.Examples.UI.NLinkLabelExample.NLinkLabelExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NLinkLabelExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Padding = New Nevron.Nov.Graphics.NMargins(Nevron.Nov.NDesign.HorizontalSpacing, Nevron.Nov.NDesign.VerticalSpacing)
            Dim webLinkLabel As Nevron.Nov.UI.NLinkLabel = New Nevron.Nov.UI.NLinkLabel("Nevron Website", "https://www.nevron.com/")
            stack.Add(webLinkLabel)
            Dim emailLinkLabel As Nevron.Nov.UI.NLinkLabel = New Nevron.Nov.UI.NLinkLabel("Nevron Support Email", "mailto:support@nevron.com")
            stack.Add(emailLinkLabel)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use link labels. The first label leads to Nevron's website and the second one
	should open your default email client when clicked. When a link label is clicked, it is automatically marked as visited by
	setting its <b>IsVisited</b> property to true.
</p>
"
        End Function

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NLinkLabelExample.
		''' </summary>
		Public Shared ReadOnly NLinkLabelExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
