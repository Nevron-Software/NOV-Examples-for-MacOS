Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NTableOfContentsExample
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
            Nevron.Nov.Examples.Text.NTableOfContentsExample.NTableOfContentsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NTableOfContentsExample), NExampleBaseSchema)
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
<p>This example demonstrates how to programmatically create and add a table of contents block to a document.</p>
"
        End Function

        Private Sub PopulateRichText()
			' Get references to the heading styles
			Dim documentBlock As Nevron.Nov.Text.NDocumentBlock = Me.m_RichText.Content
            Dim heading1Style As Nevron.Nov.Text.NRichTextStyle = documentBlock.Styles.FindStyleByTypeAndId(Nevron.Nov.Text.ENRichTextStyleType.Paragraph, "Heading1")
            Dim heading2Style As Nevron.Nov.Text.NRichTextStyle = documentBlock.Styles.FindStyleByTypeAndId(Nevron.Nov.Text.ENRichTextStyleType.Paragraph, "Heading2")
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)

			' Add a table of contents block
			Dim tableOfContents As Nevron.Nov.Text.NTableOfContentsBlock = New Nevron.Nov.Text.NTableOfContentsBlock()
            section.Blocks.Add(tableOfContents)

			' Create chapter 1
			Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Chapter 1")
            section.Blocks.Add(paragraph)
            heading1Style.Apply(paragraph)
            paragraph = New Nevron.Nov.Text.NParagraph("Topic 1.1")
            section.Blocks.Add(paragraph)
            heading2Style.Apply(paragraph)
            Me.AddParagraphs(section, "This is a sample paragraph from the first topic of chapter one.", 20)
            paragraph = New Nevron.Nov.Text.NParagraph("Topic 1.2")
            section.Blocks.Add(paragraph)
            heading2Style.Apply(paragraph)
            Me.AddParagraphs(section, "This is a sample paragraph from the second topic of chapter one.", 20)

			' Create chapter 2
			paragraph = New Nevron.Nov.Text.NParagraph("Chapter 2")
            section.Blocks.Add(paragraph)
            heading1Style.Apply(paragraph)
            paragraph = New Nevron.Nov.Text.NParagraph("Topic 2.1")
            section.Blocks.Add(paragraph)
            heading2Style.Apply(paragraph)
            Me.AddParagraphs(section, "This is a sample paragraph from the first topic of chapter two.", 20)
            paragraph = New Nevron.Nov.Text.NParagraph("Topic 2.2")
            section.Blocks.Add(paragraph)
            heading2Style.Apply(paragraph)
            Me.AddParagraphs(section, "This is a sample paragraph from the second topic of chapter two.", 20)

			' Update the table of contents
			Me.m_RichText.Document.Evaluate()
            tableOfContents.Update()
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub AddParagraphs(ByVal section As Nevron.Nov.Text.NSection, ByVal text As String, ByVal count As Integer)
			' Duplicate the given text 5 times
			text = System.[String].Join(" ", New String() {text, text, text, text, text})

			' Create the given number of paragraphs with the text
			For i As Integer = 0 To count - 1
                section.Blocks.Add(New Nevron.Nov.Text.NParagraph(text))
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NTableOfContentsExample.
		''' </summary>
		Public Shared ReadOnly NTableOfContentsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
