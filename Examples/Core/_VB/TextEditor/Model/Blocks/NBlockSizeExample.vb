Imports System.Text
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create paragraphs with differnt inline formatting
	''' </summary>
	Public Class NBlockSizeExample
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
            Nevron.Nov.Examples.Text.NBlockSizeExample.NBlockSizeExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NBlockSizeExample), NExampleBaseSchema)
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
	This example demonstrates how to control the size of text blocks using the preferred, minimum and maximum width and height properties of each block element.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockSizeExample.GetDescriptionBlock("Block Size", "Every block in the document can have a specified Preferred, Minimum and Maximum size.", 1))
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockSizeExample.GetDescriptionBlock("Preferred Width and Height", "The following paragraph has specified Preferred Width and Height.", 2))
            Dim paragraph1 As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Paragraph with Preferred Width 50% and Preferred Height of 200dips.")
            paragraph1.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightGray)
            paragraph1.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Percentage, 50)
            paragraph1.PreferredHeight = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 200)
            section.Blocks.Add(paragraph1)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockSizeExample.GetDescriptionBlock("Minimum and Maximum Width and Height", "The following paragraph has specified Minimum and Maximum Width.", 2))
            Dim paragraph2 As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Paragraph with Preferred Width 50% and Preferred Height of 200dips and Minimum Width of 200 dips and Maximum Width 300 dips.")
            paragraph2.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightGray)
            paragraph2.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Percentage, 50)
            paragraph2.PreferredHeight = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 200)
            paragraph2.MinWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 200)
            paragraph2.MaxWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 300)
            paragraph2.WrapDesiredWidth = False
            paragraph2.WrapMinWidth = False
            section.Blocks.Add(paragraph2)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockSizeExample.GetDescriptionBlock("Desired Width Wrapping", "The following paragraph has disabled desired width wrapping, resulting in a paragraph that does not introduce any hard line breaks.", 2))
            Dim paragraph3 As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Paragraph with WrapDesiredWidth set to false. Note that the paragraph will not introduce any hard breaks. The content of this paragraph is intentionally made long to illustrate this feature.")
            paragraph3.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightGray)
            paragraph3.WrapDesiredWidth = False
            paragraph3.WrapMinWidth = False
            section.Blocks.Add(paragraph3)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NBlockSizeExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NBlockSizeExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NBlockSizeExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NBlockSizeExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NBlockSizeExample.defaultBorderThickness
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
