Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NInlineStylesExample
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
            Nevron.Nov.Examples.Text.NInlineStylesExample.NInlineStylesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NInlineStylesExample), NExampleBaseSchema)
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
            Return "<p>This example demonstrates how to create and apply inline styles.</p>"
        End Function

        Private Sub PopulateRichText()
            Dim documentBlock As Nevron.Nov.Text.NDocumentBlock = Me.m_RichText.Content
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            documentBlock.Sections.Add(section)
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            section.Blocks.Add(paragraph)

			' Create the first inline
			Dim inline1 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("This is the first inline. ")
            paragraph.Inlines.Add(inline1)

			' Create and apply an inline style
			Dim style1 As Nevron.Nov.Text.NInlineStyle = New Nevron.Nov.Text.NInlineStyle("MyRedStyle")
            style1.Rule = New Nevron.Nov.Text.NInlineRule(Nevron.Nov.Graphics.NColor.Red)
            style1.Rule.FontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold
            style1.Apply(inline1)

			' Create the second inline
			Dim inline2 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("This is the second inline.")
            paragraph.Inlines.Add(inline2)

			' Create and apply an inline style
			Dim style2 As Nevron.Nov.Text.NInlineStyle = New Nevron.Nov.Text.NInlineStyle("MyBlueStyle")
            style2.Rule = New Nevron.Nov.Text.NInlineRule(Nevron.Nov.Graphics.NColor.Blue)
            style2.Rule.FontStyle = Nevron.Nov.Graphics.ENFontStyle.Italic
            style2.Apply(inline2)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NInlineStylesExample.
		''' </summary>
		Public Shared ReadOnly NInlineStylesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
