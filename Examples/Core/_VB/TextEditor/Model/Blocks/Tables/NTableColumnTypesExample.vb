Imports Nevron.Nov.Dom
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI
Imports Nevron.Nov.Graphics

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create paragraphs with differnt inline formatting
	''' </summary>
	Public Class NTableColumnTypesExample
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
            Nevron.Nov.Examples.Text.NTableColumnTypesExample.NTableColumnTypesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NTableColumnTypesExample), NExampleBaseSchema)
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
            Return "<p>This example demonstrates how to set the table column preferred width.</p>"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NTableColumnTypesExample.GetDescriptionBlock("Table Column Types Example", "This example shows how to set the table column's preferred width.", 1))

            If True Then
				' create the table
				Dim table As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable()
                table.AllowSpacingBetweenCells = False
                Dim columnCount As Integer = 5
                Dim rowCount As Integer = 5

                For row As Integer = 0 To rowCount - 1
                    Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                    table.Rows.Add(tableRow)

                    For col As Integer = 0 To columnCount - 1
                        Dim paragraph As Nevron.Nov.Text.NParagraph

                        If row = 0 Then
							' set table column preferred width
							Dim headerText As String = String.Empty
                            Dim tableColumn As Nevron.Nov.Text.NTableColumn = New Nevron.Nov.Text.NTableColumn()

                            If col Mod 2 = 0 Then
                                tableColumn.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightGray)
                            Else
                                tableColumn.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige)
                            End If

                            Select Case col
                                Case 0 ' Fixed column
                                    tableColumn.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 80)
                                    headerText = "Fixed [80dips]"
                                Case 1 ' Auto
                                    headerText = "Automatic"
                                Case 2 ' Percentage
                                    tableColumn.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Percentage, 20)
                                    headerText = "Percentage [20%]"
                                Case 3 ' Fixed
                                    tableColumn.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 160)
                                    headerText = "Fixed [160dips]"
                                Case 4 ' Percentage
                                    tableColumn.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Percentage, 30)
                                    headerText = "Percentage [30%]"
                            End Select

                            table.Columns.Add(tableColumn)
                            paragraph = New Nevron.Nov.Text.NParagraph(headerText)
                        Else
                            paragraph = New Nevron.Nov.Text.NParagraph("Cell")
                        End If

						' by default cells contain a single paragraph
						Dim tableCell As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                        tableCell.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
                        tableCell.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
                        tableCell.Blocks.Add(paragraph)
                        tableRow.Cells.Add(tableCell)
                    Next
                Next

                section.Blocks.Add(table)
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NTableColumnTypesExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NTableColumnTypesExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NTableColumnTypesExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NTableColumnTypesExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NTableColumnTypesExample.defaultBorderThickness
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
