Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NTableResizeModeExample
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
            Nevron.Nov.Examples.Diagram.NTableResizeModeExample.NTableResizeModeExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NTableResizeModeExample), NExampleBaseSchema)
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
            Return "<p>
				Demonstrates the table resize modes. Table support three resize modes:<br/>
				AutoSize - The table desired size determines the size of the table containing shape<br/>
				AutoHeight - The table containing shape defines the table witdth, while the table contents defines the table containing shape height<br/>
				Stretch - The table is stretched to fit the bounds specified by its containing shape. If the table cannot fit it will resize the table containing shape automatically.<br/>
			</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

            ' hide the grid
            drawing.ScreenVisibility.ShowGrid = False
            Dim tableDescription As String() = New String() {"Table in Auto Size Mode", "Table in Auto Height Mode", "Table in Fit To Shape Mode"}
            Dim tableBlockResizeMode As Nevron.Nov.Diagram.ENTableBlockResizeMode() = New Nevron.Nov.Diagram.ENTableBlockResizeMode() {Nevron.Nov.Diagram.ENTableBlockResizeMode.AutoSize, Nevron.Nov.Diagram.ENTableBlockResizeMode.AutoHeight, Nevron.Nov.Diagram.ENTableBlockResizeMode.FitToShape}
            Dim y As Double = 100

            For i As Integer = 0 To 3 - 1
                Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
                shape.SetBounds(New Nevron.Nov.Graphics.NRectangle(100, y, 300, 300))
                y += 200
			
				' create table
				Dim tableBlock As Nevron.Nov.Diagram.NTableBlock = Me.CreateTableBlock(tableDescription(i))
                tableBlock.Content.AllowSpacingBetweenCells = False
                tableBlock.ResizeMode = tableBlockResizeMode(i)
                shape.TextBlock = tableBlock
                activePage.Items.AddChild(shape)
            Next
        End Sub

        Private Function CreateTableBlock(ByVal description As String) As Nevron.Nov.Diagram.NTableBlock
            Dim tableBlock As Nevron.Nov.Diagram.NTableBlock = New Nevron.Nov.Diagram.NTableBlock(4, 3, Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black), New Nevron.Nov.Graphics.NMargins(1))
            Dim tableBlockContent As Nevron.Nov.Diagram.NTableBlockContent = tableBlock.Content
            Dim tableCell As Nevron.Nov.Text.NTableCell = tableBlock.Content.Rows(CInt((0))).Cells(0)
            tableCell.ColSpan = Integer.MaxValue
            tableCell.Blocks.Clear()
            Dim par As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(description)
            par.FontStyleBold = True
            tableCell.Blocks.Add(par)

            For rowIndex As Integer = 1 To tableBlockContent.Rows.Count - 1
                Dim row As Nevron.Nov.Text.NTableRow = tableBlockContent.Rows(rowIndex)

                For colIndex As Integer = 0 To tableBlockContent.Columns.Count - 1
                    Dim cell As Nevron.Nov.Text.NTableCell = row.Cells(colIndex)
                    cell.Blocks.Clear()
                    cell.Blocks.Add(New Nevron.Nov.Text.NParagraph("This is a table cell [" & rowIndex.ToString() & ", " & colIndex.ToString() & "]"))
                Next
            Next

            Dim iter As Nevron.Nov.Text.NTableCellIterator = New Nevron.Nov.Text.NTableCellIterator(tableBlockContent)

            While iter.MoveNext()
                iter.Current.VerticalAlignment = Nevron.Nov.Text.ENVAlign.Center
                iter.Current.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Center
            End While

			' make sure all columns are percentage based
			Dim percent As Double = 100 / tableBlockContent.Columns.Count

            For i As Integer = 0 To tableBlockContent.Columns.Count - 1
                tableBlockContent.Columns(CInt((i))).PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Percentage, percent)
            Next

            Return tableBlock
        End Function

		#EndRegion

		#Region"Implementation "

		''' <summary>
		''' Adds rich formatted text to the specified text block content
		''' </summary>
		''' <paramname="content"></param>
		Private Sub AddFormattedTextToContent(ByVal content As Nevron.Nov.Diagram.NTextBlockContent)
            content.Blocks.Add(Nevron.Nov.Examples.Diagram.NTableResizeModeExample.GetTitleParagraph("Bullet lists allow you to apply automatic numbering on paragraphs or groups of blocks.", 1))

			' setting bullet list template type
			If True Then
                content.Blocks.Add(Nevron.Nov.Examples.Diagram.NTableResizeModeExample.GetTitleParagraph("Following are bullet lists with different formatting", 2))
                Dim values As Nevron.Nov.Text.ENBulletListTemplateType() = Nevron.Nov.NEnum.GetValues(Of Nevron.Nov.Text.ENBulletListTemplateType)()
                Dim names As String() = Nevron.Nov.NEnum.GetNames(Of Nevron.Nov.Text.ENBulletListTemplateType)()
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
                content.Blocks.Add(Nevron.Nov.Examples.Diagram.NTableResizeModeExample.GetTitleParagraph("Following is an example of bullets with different bullet level", 2))
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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Diagram.NTableResizeModeExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Diagram.NTableResizeModeExample.CreateLeftTagBorder(color)
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
		''' Schema associated with NTableResizeModeExample.
		''' </summary>
		Public Shared ReadOnly NTableResizeModeExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
