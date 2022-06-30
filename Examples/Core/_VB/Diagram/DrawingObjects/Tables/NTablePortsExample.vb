Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
	''' <summary>
	''' The example demonstrates how to connect table ports
	''' </summary>
	Public Class NTablePortsExample
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
            Nevron.Nov.Examples.Diagram.NTablePortsExample.NTablePortsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NTablePortsExample), NExampleBaseSchema)
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
            Return "<p>The example demonstrates how to connect table ports to other shapes.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

            ' hide the grid
            drawing.ScreenVisibility.ShowGrid = False
            Dim tableDescription As String() = New String() {"Table With Column Ports", "Table With Row Ports", "Table With Cell Ports", "Table With Grid Ports"}
            Dim tablePortDistributionModes As Nevron.Nov.Diagram.ENPortsDistributionMode() = New Nevron.Nov.Diagram.ENPortsDistributionMode() {Nevron.Nov.Diagram.ENPortsDistributionMode.ColumnsOnly, Nevron.Nov.Diagram.ENPortsDistributionMode.RowsOnly, Nevron.Nov.Diagram.ENPortsDistributionMode.CellBased, Nevron.Nov.Diagram.ENPortsDistributionMode.GridBased}
            Dim tableBlocks As Nevron.Nov.Diagram.NTableBlock() = New Nevron.Nov.Diagram.NTableBlock(tablePortDistributionModes.Length - 1) {}
            Dim margin As Double = 40
            Dim tableSize As Double = 200
            Dim gap As Double = activePage.Width - margin * 2 - tableSize * 2
            Dim yPos As Double = margin
            Dim portDistributionModeIndex As Integer = 0

            For y As Integer = 0 To 2 - 1
                Dim xPos As Double = margin

                For x As Integer = 0 To 2 - 1
                    Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
                    shape.SetBounds(New Nevron.Nov.Graphics.NRectangle(xPos, yPos, tableSize, tableSize))
                    xPos += tableSize + gap

					' create table
					Dim tableBlock As Nevron.Nov.Diagram.NTableBlock = Me.CreateTableBlock(tableDescription(portDistributionModeIndex))

					' collect the block to connect it to the center shape
					tableBlocks(portDistributionModeIndex) = tableBlock
                    tableBlock.Content.AllowSpacingBetweenCells = False
                    tableBlock.ResizeMode = Nevron.Nov.Diagram.ENTableBlockResizeMode.FitToShape
                    tableBlock.PortsDistributionMode = tablePortDistributionModes(System.Math.Min(System.Threading.Interlocked.Increment(portDistributionModeIndex), portDistributionModeIndex - 1))
                    shape.TextBlock = tableBlock
                    drawing.ActivePage.Items.AddChild(shape)
                Next

                yPos += tableSize + gap
            Next

            Dim centerShape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory().CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            centerShape.Text = "Table Ports allow you to connect tables to other shapes"
            centerShape.TextBlock.FontStyleBold = True
            CType(centerShape.TextBlock, Nevron.Nov.Diagram.NTextBlock).VerticalAlignment = Nevron.Nov.ENVerticalAlignment.Center
            CType(centerShape.TextBlock, Nevron.Nov.Diagram.NTextBlock).HorizontalAlignment = Nevron.Nov.Text.ENAlign.Center
            activePage.Items.AddChild(centerShape)
            Dim center As Double = drawing.ActivePage.Width / 2.0
            Dim shapeSize As Double = 100
            centerShape.SetBounds(New Nevron.Nov.Graphics.NRectangle(center - shapeSize / 2.0, center - shapeSize / 2.0, shapeSize, shapeSize))

			' get the column port for the first column on the bottom side
			Dim columnPort As Nevron.Nov.Diagram.NTableColumnPort

            If tableBlocks(CInt((0))).TryGetColumnPort(0, False, columnPort) Then
                Me.Connect(columnPort, centerShape.GetPortByName("Left"))
            End If

			' get the row port for the second row on the left side
			Dim rowPort As Nevron.Nov.Diagram.NTableRowPort

            If tableBlocks(CInt((1))).TryGetRowPort(1, True, rowPort) Then
                Me.Connect(rowPort, centerShape.GetPortByName("Top"))
            End If

			' get the cell port of the first cell on the top side
			Dim cellPort As Nevron.Nov.Diagram.NTableCellPort

            If tableBlocks(CInt((2))).TryGetCellPort(0, 0, Nevron.Nov.Diagram.ENTableCellPortDirection.Top, cellPort) Then
                Me.Connect(cellPort, centerShape.GetPortByName("Bottom"))
            End If

			' get the cell port of the first row on the left side
			Dim rowPort1 As Nevron.Nov.Diagram.NTableRowPort

            If tableBlocks(CInt((3))).TryGetRowPort(0, True, rowPort1) Then
                Me.Connect(rowPort1, centerShape.GetPortByName("Right"))
            End If
        End Sub

        Private Sub Connect(ByVal beginPort As Nevron.Nov.Diagram.NPort, ByVal endPort As Nevron.Nov.Diagram.NPort)
            Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            connector.RerouteMode = Nevron.Nov.Diagram.ENRoutableConnectorRerouteMode.Always
            Me.m_DrawingView.ActivePage.Items.AddChild(connector)
            connector.GlueBeginToPort(beginPort)
            connector.GlueEndToPort(endPort)
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
            content.Blocks.Add(Nevron.Nov.Examples.Diagram.NTablePortsExample.GetTitleParagraph("Bullet lists allow you to apply automatic numbering on paragraphs or groups of blocks.", 1))

			' setting bullet list template type
			If True Then
                content.Blocks.Add(Nevron.Nov.Examples.Diagram.NTablePortsExample.GetTitleParagraph("Following are bullet lists with different formatting", 2))
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
                content.Blocks.Add(Nevron.Nov.Examples.Diagram.NTablePortsExample.GetTitleParagraph("Following is an example of bullets with different bullet level", 2))
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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Diagram.NTablePortsExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Diagram.NTablePortsExample.CreateLeftTagBorder(color)
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
		''' Schema associated with NTablePortsExample.
		''' </summary>
		Public Shared ReadOnly NTablePortsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
