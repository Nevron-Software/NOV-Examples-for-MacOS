Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create inline elements with different formatting
	''' </summary>
	Public Class NHyperlinkInlinesExample
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
            Nevron.Nov.Examples.Text.NHyperlinkInlinesExample.NHyperlinkInlinesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NHyperlinkInlinesExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Examples"

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
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim jumpToBookmarkButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Jump to Bookmark")
            AddHandler jumpToBookmarkButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnJumpToBookmarkButtonClick)
            stack.Add(jumpToBookmarkButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to add hyperlinks and bookmarks and how to create image hyperlinks.</p>
<p>Press the ""Jump to Bookmark"" button to position the caret to ""MyBookmark"" bookmark.</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NHyperlinkInlinesExample.GetDescriptionBlock("Hyperlink Inlines", "The example shows how to use hyperlinks inlines.", 1))

			' Hyperlink inline with a hyperlink to an URL
			If True Then
                Dim hyperlinkInline As Nevron.Nov.Text.NHyperlinkInline = New Nevron.Nov.Text.NHyperlinkInline()
                hyperlinkInline.Hyperlink = New Nevron.Nov.Text.NUrlHyperlink("http://www.nevron.com", Nevron.Nov.Text.ENUrlHyperlinkTarget.SameWindowSameFrame)
                hyperlinkInline.Text = "Jump to www.nevron.com"
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(hyperlinkInline)
                section.Blocks.Add(paragraph)
            End If

			' Image inline with a hyperlink to an URL
			If True Then
                Dim imageInline As Nevron.Nov.Text.NImageInline = New Nevron.Nov.Text.NImageInline()
                imageInline.Image = Nevron.Nov.Diagram.NResources.Image_MyDraw_Logos_MyDrawLogo_png
                imageInline.Hyperlink = New Nevron.Nov.Text.NUrlHyperlink("http://www.mydraw.com", Nevron.Nov.Text.ENUrlHyperlinkTarget.SameWindowSameFrame)
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(imageInline)
                section.Blocks.Add(paragraph)
            End If

            For i As Integer = 0 To 10 - 1
                section.Blocks.Add(New Nevron.Nov.Text.NParagraph("Some paragraph"))
            Next

			' Bookmark inline
			If True Then
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                Dim bookmark As Nevron.Nov.Text.NBookmarkInline = New Nevron.Nov.Text.NBookmarkInline()
                bookmark.Name = "MyBookmark"
                bookmark.Text = "This is a bookmark"
                bookmark.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Red)
                paragraph.Inlines.Add(bookmark)
                section.Blocks.Add(paragraph)
            End If
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnJumpToBookmarkButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_RichText.Content.[Goto](Nevron.Nov.Text.ENTextDocumentPart.Bookmark, "MyBookmark", True)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NHyperlinkInlinesExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NHyperlinkInlinesExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NHyperlinkInlinesExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NHyperlinkInlinesExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NHyperlinkInlinesExample.defaultBorderThickness
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
