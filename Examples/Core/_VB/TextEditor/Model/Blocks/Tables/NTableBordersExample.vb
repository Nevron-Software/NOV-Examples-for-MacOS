Imports Nevron.Nov.Dom
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI
Imports Nevron.Nov.Graphics
Imports System.Text

Namespace Nevron.Nov.Examples.Text
    ''' <summary>
    ''' The example demonstrates how to modify the table borders, spacing etc.
    ''' </summary>
    Public Class NTableBordersExample
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
            Nevron.Nov.Examples.Text.NTableBordersExample.NTableBordersExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NTableBordersExample), NExampleBaseSchema)
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
	This example demonstrates how table borders behave when the table allows or not table cells spacing.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)

            If True Then
                section.Blocks.Add(Nevron.Nov.Examples.Text.NTableBordersExample.GetDescriptionBlock("Table Borders Example", "This example shows how table borders behave when the table allows or does not table cell spacing.", 1))

                ' first create the table
                Dim table As Nevron.Nov.Text.NTable = Nevron.Nov.Examples.Text.NTableBordersExample.CreateTable(2, 3)
                table.AllowSpacingBetweenCells = True
                table.TableCellHorizontalSpacing = 3
                table.TableCellVerticalSpacing = 3

                For col As Integer = 0 To table.Columns.Count - 1

                    For row As Integer = 0 To table.Rows.Count - 1
                        Dim cell As Nevron.Nov.Text.NTableCell = table.Rows(CInt((row))).Cells(col)

                        Select Case col Mod 3
                            Case 0
                                cell.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
                                cell.Margins = New Nevron.Nov.Graphics.NMargins(3)
                                cell.Padding = New Nevron.Nov.Graphics.NMargins(3)
                                cell.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
                            Case 1
                                cell.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Green)
                                cell.Margins = New Nevron.Nov.Graphics.NMargins(5)
                                cell.Padding = New Nevron.Nov.Graphics.NMargins(5)
                                cell.BorderThickness = New Nevron.Nov.Graphics.NMargins(3)
                            Case 2
                                cell.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Blue)
                                cell.Margins = New Nevron.Nov.Graphics.NMargins(7)
                                cell.Padding = New Nevron.Nov.Graphics.NMargins(7)
                                cell.BorderThickness = New Nevron.Nov.Graphics.NMargins(2)
                        End Select
                    Next
                Next

                table.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
                table.BorderThickness = New Nevron.Nov.Graphics.NMargins(2, 2, 2, 2)
                section.Blocks.Add(table)
                section.Blocks.Add(New Nevron.Nov.Text.NParagraph("This is a 2x3 table, with borders in collapsed table border mode:"))

                ' first create the table
                table = Nevron.Nov.Examples.Text.NTableBordersExample.CreateTable(2, 3)
                table.AllowSpacingBetweenCells = False

                For col As Integer = 0 To table.Columns.Count - 1

                    For row As Integer = 0 To table.Rows.Count - 1
                        Dim cell As Nevron.Nov.Text.NTableCell = table.Rows(CInt((row))).Cells(col)

                        Select Case col Mod 3
                            Case 0
                                cell.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
                                cell.Margins = New Nevron.Nov.Graphics.NMargins(3)
                                cell.Padding = New Nevron.Nov.Graphics.NMargins(3)
                                cell.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
                            Case 1
                                cell.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Green)
                                cell.Margins = New Nevron.Nov.Graphics.NMargins(5)
                                cell.Padding = New Nevron.Nov.Graphics.NMargins(5)
                                cell.BorderThickness = New Nevron.Nov.Graphics.NMargins(3)
                            Case 2
                                cell.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Blue)
                                cell.Margins = New Nevron.Nov.Graphics.NMargins(7)
                                cell.Padding = New Nevron.Nov.Graphics.NMargins(7)
                                cell.BorderThickness = New Nevron.Nov.Graphics.NMargins(2)
                        End Select
                    Next
                Next

                table.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
                section.Blocks.Add(table)
            End If
        End Sub

		#EndRegion

        #Region"Fields"

        Private m_RichText As Nevron.Nov.Text.NRichTextView

        #EndRegion

        #Region"Schema"

        Public Shared ReadOnly NTableBordersExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

		#Region"Static Methods"

		Private Shared Function CreateTable(ByVal rowCount As Integer, ByVal colCount As Integer) As Nevron.Nov.Text.NTable
            ' first create the table
            Dim table As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable(rowCount, colCount)
            table.AllowSpacingBetweenCells = True
            table.TableCellHorizontalSpacing = 3
            table.TableCellVerticalSpacing = 3

            For col As Integer = 0 To table.Columns.Count - 1
                ' set table column preferred width
                Dim headerText As String = String.Empty

                Select Case col
                    Case 0 ' Fixed column
                        table.Columns(CInt((col))).PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 80)
                        headerText = "Fixed [80dips]"
                    Case 1 ' Auto
                        headerText = "Automatic"
                    Case 2 ' Percentage
                        table.Columns(CInt((col))).PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Percentage, 20)
                        headerText = "Percentage [20%]"
                    Case 3 ' Fixed
                        table.Columns(CInt((col))).PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 160)
                        headerText = "Fixed [160dips]"
                    Case 4 ' Percentage
                        table.Columns(CInt((col))).PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Percentage, 30)
                        headerText = "Percentage [30%]"
                End Select

                For row As Integer = 0 To table.Rows.Count - 1
                    Dim paragraph As Nevron.Nov.Text.NParagraph

                    If row = 0 Then
                        paragraph = New Nevron.Nov.Text.NParagraph(headerText)
                    Else
                        paragraph = New Nevron.Nov.Text.NParagraph("Cell")
                    End If

                    Dim cell As Nevron.Nov.Text.NTableCell = table.Rows(CInt((row))).Cells(col)
                    cell.Blocks.Clear()
                    cell.Blocks.Add(paragraph)
                Next
            Next

            Return table
        End Function

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NTableBordersExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NTableBordersExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NTableBordersExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NTableBordersExample.defaultBorderThickness
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
