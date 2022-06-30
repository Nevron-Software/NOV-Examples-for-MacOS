Imports Nevron.Nov.Dom
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Formats
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NEpubImportExample
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
            Nevron.Nov.Examples.Text.NEpubImportExample.NEpubImportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NEpubImportExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the rich text
			Dim richTextWithRibbon As Nevron.Nov.Text.NRichTextViewWithRibbon = New Nevron.Nov.Text.NRichTextViewWithRibbon()
            Me.m_RichText = richTextWithRibbon.View
            Me.m_RichText.AcceptsTab = True
            Me.m_RichText.Content.Sections.Clear()

			' Populate the rich text
			Me.PopulateRichText()
            Return richTextWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to import Electronic Publications (EPUB files) in the Nevron Rich Text Editor.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Me.m_RichText.LoadFromResource(Nevron.Nov.Text.NResources.RBIN_EPUB_GeographyOfBliss_epub, Nevron.Nov.Text.Formats.NTextFormat.Epub)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NEpubImportExample.
		''' </summary>
		Public Shared ReadOnly NEpubImportExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
