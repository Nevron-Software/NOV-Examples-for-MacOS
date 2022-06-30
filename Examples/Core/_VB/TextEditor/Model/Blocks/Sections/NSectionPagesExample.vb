Imports System.Text
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create paragraphs with differnt formatting
	''' </summary>
	Public Class NSectionPagesExample
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
            Nevron.Nov.Examples.Text.NSectionPagesExample.NSectionPagesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NSectionPagesExample), NExampleBaseSchema)
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
	This example demonstrates how to set different page properties, like page size, page orientation, and page border.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Me.m_RichText.Content.Layout = Nevron.Nov.Text.ENTextLayout.Print
            Me.m_RichText.Content.ZoomFactor = 0.5

            For sectionIndex As Integer = 0 To 4 - 1
                Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
                section.Margins = Nevron.Nov.Graphics.NMargins.Zero
                section.Padding = Nevron.Nov.Graphics.NMargins.Zero
                Dim sectionText As String = String.Empty

                Select Case sectionIndex
                    Case 0
                        sectionText = "Paper size A4."
                        section.PageSize = New Nevron.Nov.Graphics.NPaperSize(Nevron.Nov.Graphics.ENPaperKind.A4)
                        section.BreakType = Nevron.Nov.Text.ENSectionBreakType.NextPage
                        section.Blocks.Add(Nevron.Nov.Examples.Text.NSectionPagesExample.GetDescriptionBlock("Section Pages", "This example shows how to set different page properties, like page size, page orientation, and page border", 1))
                    Case 1
                        sectionText = "Paper size A5."
                        section.PageSize = New Nevron.Nov.Graphics.NPaperSize(Nevron.Nov.Graphics.ENPaperKind.A5)
                        section.BreakType = Nevron.Nov.Text.ENSectionBreakType.NextPage
                    Case 2
                        sectionText = "Paper size A4, paper orientation portrait."
                        section.PageOrientation = Nevron.Nov.Graphics.ENPageOrientation.Landscape
                        section.PageSize = New Nevron.Nov.Graphics.NPaperSize(Nevron.Nov.Graphics.ENPaperKind.A4)
                        section.BreakType = Nevron.Nov.Text.ENSectionBreakType.NextPage
                    Case 3
                        sectionText = "Paper size A4, page border solid 10dip."
                        section.PageBorder = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
                        section.PageBorderThickness = New Nevron.Nov.Graphics.NMargins(10)
                        section.PageSize = New Nevron.Nov.Graphics.NPaperSize(Nevron.Nov.Graphics.ENPaperKind.A4)
                        section.BreakType = Nevron.Nov.Text.ENSectionBreakType.NextPage
                End Select

                Me.m_RichText.Content.Sections.Add(section)

				' add some content
				Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(sectionText)
                section.Blocks.Add(paragraph)
            Next
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Function CreateHeaderFooter(ByVal text As String) As Nevron.Nov.Text.NHeaderFooter
            Dim headerFooter As Nevron.Nov.Text.NHeaderFooter = New Nevron.Nov.Text.NHeaderFooter()
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(text))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Page "))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NFieldInline(Nevron.Nov.Text.ENNumericFieldName.PageNumber))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(" of "))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NFieldInline(Nevron.Nov.Text.ENNumericFieldName.PageCount))
            headerFooter.Blocks.Add(paragraph)
            Return headerFooter
        End Function

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NSectionPagesExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NSectionPagesExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NSectionPagesExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NSectionPagesExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NSectionPagesExample.defaultBorderThickness
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
