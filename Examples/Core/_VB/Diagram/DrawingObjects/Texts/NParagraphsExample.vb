Imports System.Text
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NParagraphsExample
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
            Nevron.Nov.Examples.Diagram.NParagraphsExample.NParagraphsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NParagraphsExample), NExampleBaseSchema)
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
            Return "<p>Demonstrates how to modify different aspects of the rich text paragraphs.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content

            ' hide the grid
            drawing.ScreenVisibility.ShowGrid = False
            Dim basicShapesFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim shape1 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            shape1.SetBounds(10, 10, 600, 900)
            Dim textBlock As Nevron.Nov.Diagram.NTextBlock = New Nevron.Nov.Diagram.NTextBlock()
            shape1.TextBlock = textBlock
            textBlock.Padding = New Nevron.Nov.Graphics.NMargins(20)
            textBlock.Content.Blocks.Clear()
            Me.AddFormattedTextToContent(textBlock.Content)
            drawing.ActivePage.Items.Add(shape1)
        End Sub

		#EndRegion

		#Region"Implementation "

		''' <summary>
		''' Adds rich formatted text to the specified text block content
		''' </summary>
		''' <paramname="content"></param>
		Private Sub AddFormattedTextToContent(ByVal content As Nevron.Nov.Diagram.NTextBlockContent)
			' paragraphs with different horizontal alignment

			Dim paragraph As Nevron.Nov.Text.NParagraph

            For i As Integer = 0 To 4 - 1
                paragraph = New Nevron.Nov.Text.NParagraph()

                Select Case i
                    Case 0
                        paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
                        paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(Nevron.Nov.Examples.Diagram.NParagraphsExample.GetAlignedParagraphText("left")))
                    Case 1
                        paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Center
                        paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(Nevron.Nov.Examples.Diagram.NParagraphsExample.GetAlignedParagraphText("center")))
                    Case 2
                        paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Right
                        paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(Nevron.Nov.Examples.Diagram.NParagraphsExample.GetAlignedParagraphText("right")))
                    Case 3
                        paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Justify
                        paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(Nevron.Nov.Examples.Diagram.NParagraphsExample.GetAlignedParagraphText("justify")))
                End Select

                content.Blocks.Add(paragraph)
            Next

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
                content.Blocks.Add(paragraph)
            End If

			' First line indent and hanging indent
			content.Blocks.Add(Nevron.Nov.Examples.Diagram.NParagraphsExample.GetTitleParagraph("Paragraph with First Line Indent and Hanging Indent", 2))
            Dim paragraphWithIndents As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Diagram.NParagraphsExample.GetRepeatingText("First line indent -10dip, hanging indent 15dip.", 5))
            paragraphWithIndents.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraphWithIndents.FirstLineIndent = -10
            paragraphWithIndents.HangingIndent = 15
            content.Blocks.Add(paragraphWithIndents)

			' First line indent and hanging indent
			content.Blocks.Add(Nevron.Nov.Examples.Diagram.NParagraphsExample.GetTitleParagraph("Line Spacing", 2))
            Dim paragraphWithMultipleLineSpacing As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Diagram.NParagraphsExample.GetRepeatingText("Line space is two times bigger than normal", 10))
            paragraphWithMultipleLineSpacing.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraphWithMultipleLineSpacing.LineHeightMode = Nevron.Nov.Text.ENLineHeightMode.Multiple
            paragraphWithMultipleLineSpacing.LineHeightFactor = 2.0
            content.Blocks.Add(paragraphWithMultipleLineSpacing)
            Dim paragraphWithAtLeastLineSpacing As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Diagram.NParagraphsExample.GetRepeatingText("Line space is at least 20 dips.", 10))
            paragraphWithAtLeastLineSpacing.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraphWithAtLeastLineSpacing.LineHeightMode = Nevron.Nov.Text.ENLineHeightMode.AtLeast
            paragraphWithAtLeastLineSpacing.LineHeight = 20.0
            content.Blocks.Add(paragraphWithAtLeastLineSpacing)
            Dim paragraphWithExactLineSpacing As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Diagram.NParagraphsExample.GetRepeatingText("Line space is exactly 20 dips.", 10))
            paragraphWithExactLineSpacing.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraphWithExactLineSpacing.LineHeightMode = Nevron.Nov.Text.ENLineHeightMode.Exactly
            paragraphWithExactLineSpacing.LineHeight = 20.0
            content.Blocks.Add(paragraphWithExactLineSpacing)

			' BIDI formatting
			content.Blocks.Add(Nevron.Nov.Examples.Diagram.NParagraphsExample.GetTitleParagraph("Paragraphs with BIDI text", 2))
            paragraph = New Nevron.Nov.Text.NParagraph()
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            Dim latinText1 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("This is some text in English. Followed by Arabic:")
            Dim arabicText As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("أساسًا، تتعامل الحواسيب فقط مع الأرقام، وتقوم بتخزين الأحرف والمحارف الأخرى بعد أن تُعطي رقما معينا لكل واحد منها. وقبل اختراع ")
            Dim latinText2 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("This is some text in English.")
            paragraph.Inlines.Add(latinText1)
            paragraph.Inlines.Add(arabicText)
            paragraph.Inlines.Add(latinText2)
            content.Blocks.Add(paragraph)
        End Sub
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

                text += "This is " & alignment & " aligned paragraph."
            Next

            Return text
        End Function
		''' <summary>
		''' Gets the specified text repeated
		''' </summary>
		''' <paramname="text"></param>
		''' <paramname="count"></param>
		''' <returns></returns>
		Friend Shared Function GetRepeatingText(ByVal text As String, ByVal count As Integer) As String
            Dim builder As System.Text.StringBuilder = New System.Text.StringBuilder()

            For i As Integer = 0 To count - 1

                If builder.Length > 0 Then
                    builder.Append(" ")
                End If

                builder.Append(text)
            Next

            Return builder.ToString()
        End Function
		''' <summary>
		''' 
		''' </summary>
		''' <paramname="text"></param>
		''' <paramname="level"></param>
		''' <returns></returns>
		Friend Shared Function GetTitleParagraphNoBorder(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NParagraph
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
		Friend Shared Function GetTitleParagraph(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NParagraph
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Diagram.NParagraphsExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Diagram.NParagraphsExample.CreateLeftTagBorder(color)
            paragraph.BorderThickness = New Nevron.Nov.Graphics.NMargins(5.0, 0.0, 0.0, 0.0)
            Return paragraph
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

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NParagraphsExample.
		''' </summary>
		Public Shared ReadOnly NParagraphsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
