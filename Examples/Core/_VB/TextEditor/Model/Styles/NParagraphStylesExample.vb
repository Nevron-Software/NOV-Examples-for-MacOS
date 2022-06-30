Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NParagraphStylesExample
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
            Nevron.Nov.Examples.Text.NParagraphStylesExample.NParagraphStylesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NParagraphStylesExample), NExampleBaseSchema)
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
            Return "<p>This example demonstrates how to create and apply paragraph styles.</p>"
        End Function

        Private Sub PopulateRichText()
            Dim documentBlock As Nevron.Nov.Text.NDocumentBlock = Me.m_RichText.Content
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            documentBlock.Sections.Add(section)

			' Create the first paragraph
			Dim paragraph1 As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("This paragraph is styled with a predefined paragraph style.")
            section.Blocks.Add(paragraph1)

			' Apply a predefined paragraph style
			Dim predefinedStyle As Nevron.Nov.Text.NRichTextStyle = documentBlock.Styles.FindStyleByTypeAndId(Nevron.Nov.Text.ENRichTextStyleType.Paragraph, "Heading2")
            predefinedStyle.Apply(paragraph1)

			' Create the second paragraph
			Dim paragraph2 As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("This paragraph is styled with a custom paragraph style.")
            section.Blocks.Add(paragraph2)

			' Create a custom paragraph style and apply it
			Dim customStyle As Nevron.Nov.Text.NParagraphStyle = New Nevron.Nov.Text.NParagraphStyle("CustomStyle")
            customStyle.ParagraphRule = New Nevron.Nov.Text.NParagraphRule()
            customStyle.ParagraphRule.BorderRule = New Nevron.Nov.Text.NBorderRule(Nevron.Nov.UI.ENPredefinedBorderStyle.Dash, Nevron.Nov.Graphics.NColor.Red, New Nevron.Nov.Graphics.NMargins(1))
            customStyle.ParagraphRule.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Center
            customStyle.ParagraphRule.Padding = New Nevron.Nov.Graphics.NMargins(20)
            customStyle.InlineRule = New Nevron.Nov.Text.NInlineRule()
            customStyle.InlineRule.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Blue)
            customStyle.Apply(paragraph2)
            paragraph2.MarginTop = 30
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NParagraphStylesExample.
		''' </summary>
		Public Shared ReadOnly NParagraphStylesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
