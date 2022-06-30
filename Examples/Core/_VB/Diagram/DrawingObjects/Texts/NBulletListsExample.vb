Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NBulletListsExample
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
            Nevron.Nov.Examples.Diagram.NBulletListsExample.NBulletListsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NBulletListsExample), NExampleBaseSchema)
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
            Return "<p>Demonstrates how to add bullets lists in text content.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content

            ' hide the grid
            drawing.ScreenVisibility.ShowGrid = False
            Dim basicShapesFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim shape1 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            shape1.SetBounds(10, 10, 600, 1000)
            Dim textBlock As Nevron.Nov.Diagram.NTextBlock = New Nevron.Nov.Diagram.NTextBlock()
            shape1.TextBlock = textBlock
            textBlock.Padding = New Nevron.Nov.Graphics.NMargins(20)
            textBlock.Content.Blocks.Clear()
            textBlock.Content.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            Call Nevron.Nov.Examples.Diagram.NBulletListsExample.AddFormattedTextToContent(textBlock.Content)
            drawing.ActivePage.Items.Add(shape1)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NBulletListsExample.
		''' </summary>
		Public Shared ReadOnly NBulletListsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		''' <summary>
		''' Adds rich formatted text to the specified text block content
		''' </summary>
		''' <paramname="content"></param>
		Private Shared Sub AddFormattedTextToContent(ByVal content As Nevron.Nov.Diagram.NTextBlockContent)
            content.Blocks.Add(Nevron.Nov.Examples.Diagram.NBulletListsExample.GetTitleParagraph("Bullet lists allow you to apply automatic numbering on paragraphs or groups of blocks.", 1))

			' setting bullet list template type
			If True Then
                content.Blocks.Add(Nevron.Nov.Examples.Diagram.NBulletListsExample.GetTitleParagraph("Following are bullet lists with different formatting", 2))
                Dim values As Nevron.Nov.Text.ENBulletListTemplateType() = Nevron.Nov.NEnum.GetValues(Of Nevron.Nov.Text.ENBulletListTemplateType)()
                Dim itemText As String = "Bullet List Item"

                For i As Integer = 0 To values.Length - 1 - 1
                    Dim group As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
                    content.Blocks.Add(group)
                    group.MarginTop = 10
                    group.MarginBottom = 10
                    Dim bulletList As Nevron.Nov.Text.NBulletList = New Nevron.Nov.Text.NBulletList(values(i))
                    content.BulletLists.Add(bulletList)

                    For j As Integer = 0 To 3 - 1
                        Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(itemText & j.ToString())
                        Dim bullet As Nevron.Nov.Text.NBulletInline = New Nevron.Nov.Text.NBulletInline()
                        bullet.List = bulletList
                        paragraph.Bullet = bullet
                        group.Blocks.Add(paragraph)
                    Next
                Next
            End If

			' nested bullet lists
			If True Then
                content.Blocks.Add(Nevron.Nov.Examples.Diagram.NBulletListsExample.GetTitleParagraph("Following is an example of bullets with different bullet level", 2))
                Dim bulletList As Nevron.Nov.Text.NBulletList = New Nevron.Nov.Text.NBulletList(Nevron.Nov.Text.ENBulletListTemplateType.[Decimal])
                content.BulletLists.Add(bulletList)
                Dim group As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
                content.Blocks.Add(group)

                For i As Integer = 0 To 3 - 1
                    Dim par As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Bullet List Item" & i.ToString(), bulletList, 0)
                    group.Blocks.Add(par)

                    For j As Integer = 0 To 2 - 1
                        Dim nestedPar As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Nested Bullet List Item" & i.ToString(), bulletList, 1)
                        nestedPar.MarginLeft = 10
                        group.Blocks.Add(nestedPar)
                    Next
                Next
            End If
        End Sub

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Diagram.NBulletListsExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Diagram.NBulletListsExample.CreateLeftTagBorder(color)
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
	End Class
End Namespace
