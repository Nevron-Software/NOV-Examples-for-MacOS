Imports System.Text
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    ''' <summary>
    ''' The example demonstrates how to programmatically create paragraphs with differnt formatting
    ''' </summary>
    Public Class NHeaderAndFooterExample
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
            Nevron.Nov.Examples.Text.NHeaderAndFooterExample.NHeaderAndFooterExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NHeaderAndFooterExample), NExampleBaseSchema)
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
	This example demonstrates how create sections with different header and footer. Each section has three different properties for header and footer that allow you to specify distinct header and footer for first, left and right pages.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Me.m_RichText.Content.Layout = Nevron.Nov.Text.ENTextLayout.Print

            For sectionIndex As Integer = 0 To 3 - 1
                Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
                Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Empty
                Dim sectionText As String = String.Empty

                Select Case sectionIndex
                    Case 0
                        sectionText = "Red Section (uniform header and footer)."
                        color = Nevron.Nov.Graphics.NColor.Red
                        section.Blocks.Add(Nevron.Nov.Examples.Text.NHeaderAndFooterExample.GetDescriptionBlock("Section Headers and Footers", "This example shows how to create sections with different header and footer settings.", 1))
                        section.DifferentFirstHeaderAndFooter = False
                        section.DifferentLeftRightHeadersAndFooters = False
                        Dim header As Nevron.Nov.Text.NHeaderFooter = Me.CreateHeaderFooter("Uniform Header.")
                        header.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(color)
                        section.Header = header
                        Dim footer As Nevron.Nov.Text.NHeaderFooter = Me.CreateHeaderFooter("Uniform Footer.")
                        footer.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(color)
                        section.Footer = footer
                    Case 1
                        sectionText = "Green Section (different first header and footer)."
                        color = Nevron.Nov.Graphics.NColor.Green
                        section.BreakType = Nevron.Nov.Text.ENSectionBreakType.NextPage
                        section.DifferentFirstHeaderAndFooter = True
                        section.DifferentLeftRightHeadersAndFooters = False
                        Dim headerFirst As Nevron.Nov.Text.NHeaderFooter = Me.CreateHeaderFooter("First Page Header.")
                        headerFirst.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(color)
                        section.HeaderFirst = headerFirst
                        Dim footerFirst As Nevron.Nov.Text.NHeaderFooter = Me.CreateHeaderFooter("First Page Footer.")
                        footerFirst.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(color)
                        section.FooterFirst = footerFirst
                    Case 2
                        sectionText = "Blue Section (different left and right page header and footer)."
                        color = Nevron.Nov.Graphics.NColor.Blue
                        section.BreakType = Nevron.Nov.Text.ENSectionBreakType.NextPage
                        section.DifferentFirstHeaderAndFooter = False
                        section.DifferentLeftRightHeadersAndFooters = True
                        Dim headerLeft As Nevron.Nov.Text.NHeaderFooter = Me.CreateHeaderFooter("Left Page Header.")
                        headerLeft.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(color)
                        section.HeaderLeft = headerLeft
                        Dim headerRight As Nevron.Nov.Text.NHeaderFooter = Me.CreateHeaderFooter("Right Page Header.")
                        headerRight.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(color)
                        section.HeaderRight = headerRight
                        Dim footerLeft As Nevron.Nov.Text.NHeaderFooter = Me.CreateHeaderFooter("Left Page Footer.")
                        footerLeft.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(color)
                        section.FooterLeft = footerLeft
                        Dim footerRight As Nevron.Nov.Text.NHeaderFooter = Me.CreateHeaderFooter("Right Page Footer.")
                        footerRight.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(color)
                        section.FooterRight = footerRight
                End Select

                Me.m_RichText.Content.Sections.Add(section)

				' add some section contentparagraphs with some simple text
				For i As Integer = 0 To 20 - 1
                    Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                    Dim textInline As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline(Nevron.Nov.Examples.Text.NHeaderAndFooterExample.GetRepeatingText(sectionText, 5))
                    textInline.Fill = New Nevron.Nov.Graphics.NColorFill(color)
                    paragraph.Inlines.Add(textInline)
                    section.Blocks.Add(paragraph)
                Next
            Next
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Function CreateHeaderFooter(ByVal text As String) As Nevron.Nov.Text.NHeaderFooter
            Dim headerFooter As Nevron.Nov.Text.NHeaderFooter = New Nevron.Nov.Text.NHeaderFooter()
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(text))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(". Page "))
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

		Public Shared ReadOnly NHeaderAndFooterExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NHeaderAndFooterExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Text.NHeaderAndFooterExample.CreateLeftTagBorder(color)
            paragraph.BorderThickness = Nevron.Nov.Examples.Text.NHeaderAndFooterExample.defaultBorderThickness
            Return paragraph
        End Function

        Private Shared Function GetNoteBlock(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Red
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NHeaderAndFooterExample.GetTitleParagraphNoBorder("Note", level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NHeaderAndFooterExample.GetDescriptionParagraph(text))
            groupBlock.Border = Nevron.Nov.Examples.Text.NHeaderAndFooterExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NHeaderAndFooterExample.defaultBorderThickness
            Return groupBlock
        End Function

        Private Shared Function GetDescriptionBlock(ByVal title As String, ByVal description As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NHeaderAndFooterExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NHeaderAndFooterExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NHeaderAndFooterExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NHeaderAndFooterExample.defaultBorderThickness
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

        Private Shared Function GetLoremIpsumParagraph() As Nevron.Nov.Text.NParagraph
            Return New Nevron.Nov.Text.NParagraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum placerat in tortor nec tincidunt. Sed sagittis in sem ac auctor. Donec scelerisque molestie eros, a dictum leo fringilla eu. Vivamus porta urna non ullamcorper commodo. Nulla posuere sodales pellentesque. Donec a erat et tortor viverra euismod non et erat. Donec dictum ante eu mauris porta, eget suscipit mi ultrices. Nunc convallis adipiscing ligula, non pharetra dolor egestas at. Etiam in condimentum sapien. Praesent sagittis pulvinar metus, a posuere mauris aliquam eget.")
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
