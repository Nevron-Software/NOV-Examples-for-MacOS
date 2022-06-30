Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create inline elements with different formatting
	''' </summary>
	Public Class NInlineFormattingExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' 
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' 
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Text.NInlineFormattingExample.NInlineFormattingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NInlineFormattingExample), NExampleBaseSchema)
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
	This example demonstrates how to modify the text style and the appearance settings of inline elements as well as how to add line breaks and tabs to paragraphs.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NInlineFormattingExample.GetDescriptionBlock("Inline Formatting", "The example shows how to add inlines with different formatting to a paragraph.", 1))

            If True Then
				' font style control
				Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                Dim textInline1 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("This paragraph contains text inlines with altered ")
                paragraph.Inlines.Add(textInline1)
                Dim textInline2 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("Font Name, ")
                textInline2.FontName = "Tahoma"
                paragraph.Inlines.Add(textInline2)
                Dim textInline3 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("Font Size, ")
                textInline3.FontSize = 14
                paragraph.Inlines.Add(textInline3)
                Dim textInline4 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("Font Style (Bold), ")
                textInline4.FontStyle = textInline4.FontStyle Or Nevron.Nov.Graphics.ENFontStyle.Bold
                paragraph.Inlines.Add(textInline4)
                Dim textInline5 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("Font Style (Italic), ")
                textInline5.FontStyle = textInline5.FontStyle Or Nevron.Nov.Graphics.ENFontStyle.Italic
                paragraph.Inlines.Add(textInline5)
                Dim textInline6 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("Font Style (Underline), ")
                textInline6.FontStyle = textInline6.FontStyle Or Nevron.Nov.Graphics.ENFontStyle.Underline
                paragraph.Inlines.Add(textInline6)
                Dim textInline7 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("Font Style (StrikeTrough) ")
                textInline7.FontStyle = textInline7.FontStyle Or Nevron.Nov.Graphics.ENFontStyle.Strikethrough
                paragraph.Inlines.Add(textInline7)
                Dim textInline8 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline(", and Font Style All.")
                textInline8.FontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold Or Nevron.Nov.Graphics.ENFontStyle.Italic Or Nevron.Nov.Graphics.ENFontStyle.Underline Or Nevron.Nov.Graphics.ENFontStyle.Strikethrough
                paragraph.Inlines.Add(textInline8)
                section.Blocks.Add(paragraph)
            End If

            If True Then
				' appearance control
				Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                Dim textInline1 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("Each text inline element can contain text with different fill and background. ")
                paragraph.Inlines.Add(textInline1)
                Dim textInline2 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("Fill (Red), Background Fill Inherit. ")
                textInline2.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Red)
                paragraph.Inlines.Add(textInline2)
                Dim textInline3 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("Fill inherit, Background Fill (Green).")
                textInline3.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Green)
                paragraph.Inlines.Add(textInline3)
                section.Blocks.Add(paragraph)
            End If

            If True Then
				' line breaks
				' appearance control
				Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                Dim textInline1 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("Line breaks allow you to break...")
                paragraph.Inlines.Add(textInline1)
                Dim lineBreak As Nevron.Nov.Text.NLineBreakInline = New Nevron.Nov.Text.NLineBreakInline()
                paragraph.Inlines.Add(lineBreak)
                Dim textInline2 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("the current line in the paragraph.")
                paragraph.Inlines.Add(textInline2)
                section.Blocks.Add(paragraph)
            End If

            If True Then
				' tabs
				Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                Dim tabInline As Nevron.Nov.Text.NTabInline = New Nevron.Nov.Text.NTabInline()
                paragraph.Inlines.Add(tabInline)
                Dim textInline1 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("(Tabs) are not supported by HTML, however, they are essential when importing text documents.")
                paragraph.Inlines.Add(textInline1)
                section.Blocks.Add(paragraph)
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NInlineFormattingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function GetDescriptionParagraph(ByVal text As String) As Nevron.Nov.Text.NParagraph
            Return New Nevron.Nov.Text.NParagraph(text)
        End Function

        Private Shared Function GetTitleParagraphNoBorder(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NParagraph
            Dim fontSize As Double = 10
            Dim fontStyle As Nevron.Nov.Graphics.ENFontStyle = Nevron.Nov.Graphics.ENFontStyle.Regular

            Select Case level
                Case 1
                    fontSize = 16
                    fontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold
                Case 2
                    fontSize = 10
                    fontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold
            End Select

            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.FontSize = fontSize
            paragraph.FontStyle = fontStyle
            Dim textInline As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline(text)
            textInline.FontStyle = fontStyle
            textInline.FontSize = fontSize
            paragraph.Inlines.Add(textInline)
            Return paragraph
        End Function

        Private Shared Function GetDescriptionBlock(ByVal title As String, ByVal description As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NInlineFormattingExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NInlineFormattingExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NInlineFormattingExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NInlineFormattingExample.defaultBorderThickness
            Return groupBlock
        End Function
		''' <summary>
		''' Creates a left tag border with the specified border
		''' </summary>
		''' <paramname="color"></param>
		''' <returns></returns>
		Private Shared Function CreateLeftTagBorder(ByVal color As Nevron.Nov.Graphics.NColor) As Nevron.Nov.UI.NBorder
            Dim border As Nevron.Nov.UI.NBorder = New Nevron.Nov.UI.NBorder()
            border.LeftSide = New Nevron.Nov.UI.NBorderSide()
            border.LeftSide.Fill = New Nevron.Nov.Graphics.NColorFill(color)
            Return border
        End Function

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultBorderThickness As Nevron.Nov.Graphics.NMargins = New Nevron.Nov.Graphics.NMargins(5.0, 0.0, 0.0, 0.0)

		#EndRegion
	End Class
End Namespace
