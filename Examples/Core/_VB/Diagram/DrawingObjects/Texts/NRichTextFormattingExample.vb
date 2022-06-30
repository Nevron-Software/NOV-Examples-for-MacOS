Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NRichTextFormattingExample
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
            Nevron.Nov.Examples.Diagram.NRichTextFormattingExample.NRichTextFormattingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NRichTextFormattingExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a simple drawing
			Dim drawingViewWithRibbon As Nevron.Nov.Diagram.NDrawingViewWithRibbon = New Nevron.Nov.Diagram.NDrawingViewWithRibbon()
            Me.m_DrawingView = drawingViewWithRibbon.View
            Me.m_DrawingView.Document.HistoryService.Pause()

            Try
                Me.InitDiagram(Me.m_DrawingView.Document)
            Finally
                Me.m_DrawingView.Document.HistoryService.[Resume]()
            End Try

            Return drawingViewWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>Demonstrates how to apply rich text formatting to texts.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content

            ' hide the grid
            drawing.ScreenVisibility.ShowGrid = False
            Dim basicShapesFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim shape1 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            shape1.SetBounds(10, 10, 381, 600)
            Dim textBlock1 As Nevron.Nov.Diagram.NTextBlock = New Nevron.Nov.Diagram.NTextBlock()
            shape1.TextBlock = textBlock1
            textBlock1.Content.Blocks.Clear()
            Me.AddFormattedTextToContent(textBlock1.Content)
            drawing.ActivePage.Items.Add(shape1)
            Dim shape2 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            shape2.SetBounds(401, 10, 381, 600)
            Dim textBlock2 As Nevron.Nov.Diagram.NTextBlock = New Nevron.Nov.Diagram.NTextBlock()
            shape2.TextBlock = textBlock2
            textBlock2.Content.Blocks.Clear()
            Me.AddFormattedTextWithImagesToContent(textBlock2.Content)
            drawing.ActivePage.Items.Add(shape2)
        End Sub

		#EndRegion

		#Region"Implementation "

		''' <summary>
		''' Adds rich formatted text to the specified text block content
		''' </summary>
		''' <paramname="content"></param>
		Private Sub AddFormattedTextToContent(ByVal content As Nevron.Nov.Diagram.NTextBlockContent)
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
                content.Blocks.Add(paragraph)
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
                content.Blocks.Add(paragraph)
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
                content.Blocks.Add(paragraph)
            End If

            If True Then
				' tabs
				Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                Dim tabInline As Nevron.Nov.Text.NTabInline = New Nevron.Nov.Text.NTabInline()
                paragraph.Inlines.Add(tabInline)
                Dim textInline1 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("(Tabs) are not supported by HTML, however, they are essential when importing text documents.")
                paragraph.Inlines.Add(textInline1)
                content.Blocks.Add(paragraph)
            End If
        End Sub
		''' <summary>
		''' Adds formatted text with image elements to the specified text block content
		''' </summary>
		''' <paramname="content"></param>
		Private Sub AddFormattedTextWithImagesToContent(ByVal content As Nevron.Nov.Diagram.NTextBlockContent)
			' adding a raster image with automatic size
			If True Then
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Raster image in its original size (125x100):"))
                paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                Dim imageInline As Nevron.Nov.Text.NImageInline = New Nevron.Nov.Text.NImageInline()
                imageInline.Image = NResources.Image_Artistic_FishBowl_jpg
                paragraph.Inlines.Add(imageInline)
                content.Blocks.Add(paragraph)
            End If


			' adding a raster image with specified preferred width and height
			If True Then
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Raster image with preferred width and height (80x60):"))
                paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                Dim imageInline As Nevron.Nov.Text.NImageInline = New Nevron.Nov.Text.NImageInline()
                imageInline.Image = NResources.Image_Artistic_FishBowl_jpg
                imageInline.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 80)
                imageInline.PreferredHeight = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 60)
                paragraph.Inlines.Add(imageInline)
                content.Blocks.Add(paragraph)
            End If

			' adding a metafile image with preferred width and height
			If True Then
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Metafile image with preferred width and height (125x100):"))
                paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                Dim imageInline As Nevron.Nov.Text.NImageInline = New Nevron.Nov.Text.NImageInline()
                imageInline.Image = NResources.Image_FishBowl_wmf
                imageInline.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 125)
                imageInline.PreferredHeight = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 100)
                paragraph.Inlines.Add(imageInline)
                content.Blocks.Add(paragraph)
            End If


			' adding a metafile image with preferred width and height
			If True Then
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Metafile image with preferred width and height (80x60):"))
                paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                Dim imageInline As Nevron.Nov.Text.NImageInline = New Nevron.Nov.Text.NImageInline()
                imageInline.Image = NResources.Image_FishBowl_wmf
                imageInline.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 80)
                imageInline.PreferredHeight = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 60)
                paragraph.Inlines.Add(imageInline)
                content.Blocks.Add(paragraph)
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRichTextFormattingExample.
		''' </summary>
		Public Shared ReadOnly NRichTextFormattingExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
