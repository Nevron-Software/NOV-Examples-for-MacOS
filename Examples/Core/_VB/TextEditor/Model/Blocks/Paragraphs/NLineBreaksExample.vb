Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create paragraphs with line breaks
	''' </summary>
	Public Class NLineBreaksExample
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
            Nevron.Nov.Examples.Text.NLineBreaksExample.NLineBreaksExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NLineBreaksExample), NExampleBaseSchema)
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
	This example demonstrates how to programmatically add line breaks to paragraphs.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NLineBreaksExample.GetDescriptionBlock("Paragraphs can contain explicit line breaks", "This example shows how to programmatically add line breaks to paragraphs", 2))
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("This text appears on the first line."))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("This text appears on the second line (after the line break)."))
            section.Blocks.Add(paragraph)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_RichText As Nevron.Nov.Text.NRichTextView

        #EndRegion

        #Region"Schema"

        Public Shared ReadOnly NLineBreaksExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NLineBreaksExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NLineBreaksExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NLineBreaksExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NLineBreaksExample.defaultBorderThickness
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
