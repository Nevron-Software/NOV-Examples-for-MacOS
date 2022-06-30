Imports System.Text
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    ''' <summary>
    ''' The example demonstrates how to programmatically create paragraphs with differnt formatting
    ''' </summary>
    Public Class NParagraphFormattingExample
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
            Nevron.Nov.Examples.Text.NParagraphFormattingExample.NParagraphFormattingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NParagraphFormattingExample), NExampleBaseSchema)
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
	This example demonstrates how to use different paragraph formatting properties.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)

            ' paragraphs with different horizontal alignment
			section.Blocks.Add(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetDescriptionBlock("Paragraph Formatting Example", "The following examples show different paragraph formatting properties.", 1))
            section.Blocks.Add(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetTitleParagraph("Paragraphs with different horizontal alignment", 2))
            Dim paragraph As Nevron.Nov.Text.NParagraph

            For i As Integer = 0 To 4 - 1
                paragraph = New Nevron.Nov.Text.NParagraph()

                Select Case i
                    Case 0
                        paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
                        paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetAlignedParagraphText("left")))
                    Case 1
                        paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Center
                        paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetAlignedParagraphText("center")))
                    Case 2
                        paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Right
                        paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetAlignedParagraphText("right")))
                    Case 3
                        paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Justify
                        paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetAlignedParagraphText("justify")))
                End Select

                section.Blocks.Add(paragraph)
            Next

            section.Blocks.Add(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetTitleParagraph("Paragraphs with Margins, Padding and Borders", 2))

            If True Then
                ' borders
                paragraph = New Nevron.Nov.Text.NParagraph()
                paragraph.BorderThickness = New Nevron.Nov.Graphics.NMargins(2, 2, 2, 2)
                paragraph.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
                paragraph.PreferredWidth = Nevron.Nov.NMultiLength.NewPercentage(50)
                paragraph.Margins = New Nevron.Nov.Graphics.NMargins(5, 5, 5, 5)
                paragraph.Padding = New Nevron.Nov.Graphics.NMargins(5, 5, 5, 5)
                paragraph.PreferredWidth = Nevron.Nov.NMultiLength.NewFixed(300)
                paragraph.PreferredHeight = Nevron.Nov.NMultiLength.NewFixed(100)
                Dim textInline1 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("Paragraphs can have border, margins and padding as well as preffered size")
                paragraph.Inlines.Add(textInline1)
                section.Blocks.Add(paragraph)
            End If

			' First line indent and hanging indent
			section.Blocks.Add(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetTitleParagraph("Paragraph with First Line Indent and Hanging Indent", 2))
            Dim paragraphWithIndents As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetRepeatingText("First line indent -10dip, hanging indent 15dip.", 5))
            paragraphWithIndents.FirstLineIndent = -10
            paragraphWithIndents.HangingIndent = 15
            section.Blocks.Add(paragraphWithIndents)

			' First line indent and hanging indent
			section.Blocks.Add(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetTitleParagraph("Line Spacing", 2))
            Dim paragraphWithMultipleLineSpacing As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetRepeatingText("Line space is two times bigger than normal", 10))
            paragraphWithMultipleLineSpacing.LineHeightMode = Nevron.Nov.Text.ENLineHeightMode.Multiple
            paragraphWithMultipleLineSpacing.LineHeightFactor = 2.0
            section.Blocks.Add(paragraphWithMultipleLineSpacing)
            Dim paragraphWithAtLeastLineSpacing As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetRepeatingText("Line space is at least 20 dips.", 10))
            paragraphWithAtLeastLineSpacing.LineHeightMode = Nevron.Nov.Text.ENLineHeightMode.AtLeast
            paragraphWithAtLeastLineSpacing.LineHeight = 20.0
            section.Blocks.Add(paragraphWithAtLeastLineSpacing)
            Dim paragraphWithExactLineSpacing As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetRepeatingText("Line space is exactly 20 dips.", 10))
            paragraphWithExactLineSpacing.LineHeightMode = Nevron.Nov.Text.ENLineHeightMode.Exactly
            paragraphWithExactLineSpacing.LineHeight = 20.0
            section.Blocks.Add(paragraphWithExactLineSpacing)

            ' BIDI formatting
			section.Blocks.Add(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetTitleParagraph("Paragraphs with BIDI text", 2))
            paragraph = New Nevron.Nov.Text.NParagraph()
            Dim latinText1 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("This is some text in English. Followed by Arabic:")
            Dim arabicText As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("أساسًا، تتعامل الحواسيب فقط مع الأرقام، وتقوم بتخزين الأحرف والمحارف الأخرى بعد أن تُعطي رقما معينا لكل واحد منها. وقبل اختراع ")
            Dim latinText2 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("This is some text in English.")
            paragraph.Inlines.Add(latinText1)
            paragraph.Inlines.Add(arabicText)
            paragraph.Inlines.Add(latinText2)
            section.Blocks.Add(paragraph)
        End Sub

        #EndRegion

        #Region"Implementation"

        ''' <summary>
        ''' Gets dummy text for aligned paragraphs
        ''' </summary>
        ''' <paramname="alignment"></param>
        ''' <returns></returns>
        Private Shared Function GetAlignedParagraphText(ByVal alignment As String) As String
            Dim text As String = String.Empty

            For i As Integer = 0 To 10 - 1

                If text.Length > 0 Then
                    text += " "
                End If

                text += "This is a " & alignment & " aligned paragraph."
            Next

            Return text
        End Function

        #EndRegion

        #Region"Fields"

        Private m_RichText As Nevron.Nov.Text.NRichTextView

        #EndRegion

        #Region"Schema"

        Public Shared ReadOnly NParagraphFormattingExampleSchema As Nevron.Nov.Dom.NSchema

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
		''' <summary>
		''' Gets a paragraph with title formatting
		''' </summary>
		''' <paramname="text"></param>
		''' <returns></returns>
		Private Shared Function GetTitleParagraph(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NParagraph
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Text.NParagraphFormattingExample.CreateLeftTagBorder(color)
            paragraph.BorderThickness = Nevron.Nov.Examples.Text.NParagraphFormattingExample.defaultBorderThickness
            Return paragraph
        End Function

        Private Shared Function GetDescriptionBlock(ByVal title As String, ByVal description As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NParagraphFormattingExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NParagraphFormattingExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NParagraphFormattingExample.defaultBorderThickness
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
		''' <summary>
		''' Gets the specified text repeated
		''' </summary>
		''' <paramname="text"></param>
		''' <paramname="count"></param>
		''' <returns></returns>
		Private Shared Function GetRepeatingText(ByVal text As String, ByVal count As Integer) As String
            Dim builder As System.Text.StringBuilder = New System.Text.StringBuilder()

            For i As Integer = 0 To count - 1

                If builder.Length > 0 Then
                    builder.Append(" ")
                End If

                builder.Append(text)
            Next

            Return builder.ToString()
        End Function

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultBorderThickness As Nevron.Nov.Graphics.NMargins = New Nevron.Nov.Graphics.NMargins(5.0, 0.0, 0.0, 0.0)

		#EndRegion
	End Class
End Namespace
