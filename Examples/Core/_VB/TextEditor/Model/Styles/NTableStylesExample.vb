Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NTableStylesExample
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
            Nevron.Nov.Examples.Text.NTableStylesExample.NTableStylesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NTableStylesExample), NExampleBaseSchema)
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
            Return "<p>This example demonstrates how to create and apply table styles.</p>"
        End Function

        Private Sub PopulateRichText()
            Dim documentBlock As Nevron.Nov.Text.NDocumentBlock = Me.m_RichText.Content
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            documentBlock.Sections.Add(section)

			' Create the first table
			section.Blocks.Add(New Nevron.Nov.Text.NParagraph("Table with predefined table style:"))
            Dim table1 As Nevron.Nov.Text.NTable = Me.CreateTable()
            section.Blocks.Add(table1)

			' Apply a predefined table style
			Dim predefinedStyle As Nevron.Nov.Text.NRichTextStyle = documentBlock.Styles.FindStyleByTypeAndId(Nevron.Nov.Text.ENRichTextStyleType.Table, "GridTable2Blue")
            predefinedStyle.Apply(table1)

			' Create the second table
			Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Table with custom table style:")
            paragraph.MarginTop = 30
            section.Blocks.Add(paragraph)
            Dim table2 As Nevron.Nov.Text.NTable = Me.CreateTable()
            section.Blocks.Add(table2)

			' Create a custom table style and apply it
			Dim customStyle As Nevron.Nov.Text.NTableStyle = New Nevron.Nov.Text.NTableStyle("CustomTableStyle")
            customStyle.WholeTable = New Nevron.Nov.Text.NTablePartStyle()
            customStyle.WholeTable.BorderRule = New Nevron.Nov.Text.NBorderRule(Nevron.Nov.UI.ENPredefinedBorderStyle.Solid, Nevron.Nov.Graphics.NColor.DarkRed, New Nevron.Nov.Graphics.NMargins(1))
            customStyle.WholeTable.BorderRule.InsideHSides = New Nevron.Nov.Text.NBorderSideRule(Nevron.Nov.UI.ENPredefinedBorderStyle.Solid, Nevron.Nov.Graphics.NColor.DarkRed, 1)
            customStyle.WholeTable.BorderRule.InsideVSides = New Nevron.Nov.Text.NBorderSideRule(Nevron.Nov.UI.ENPredefinedBorderStyle.Solid, Nevron.Nov.Graphics.NColor.DarkRed, 1)
            customStyle.FirstRow = New Nevron.Nov.Text.NTablePartStyle()
            customStyle.FirstRow.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkRed)
            customStyle.FirstRow.InlineRule = New Nevron.Nov.Text.NInlineRule(Nevron.Nov.Graphics.NColor.White)
            customStyle.FirstRow.InlineRule.FontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold
            customStyle.Apply(table2)
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Function CreateTable() As Nevron.Nov.Text.NTable
            Dim table As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable()
            table.AllowSpacingBetweenCells = False
            Dim rowCount As Integer = 3
            Dim colCount As Integer = 3

			' first create the columns
			For i As Integer = 0 To colCount - 1
                table.Columns.Add(New Nevron.Nov.Text.NTableColumn())
            Next

			' then add rows with cells count matching the number of columns
			For row As Integer = 0 To rowCount - 1
                Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                table.Rows.Add(tableRow)

                For col As Integer = 0 To colCount - 1
                    Dim tableCell As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                    tableRow.Cells.Add(tableCell)
                    tableCell.Margins = New Nevron.Nov.Graphics.NMargins(4)
                    tableCell.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
                    tableCell.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
                    Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("This is a table cell [" & row.ToString() & ", " & col.ToString() & "]")
                    tableCell.Blocks.Add(paragraph)
                Next
            Next

            Return table
        End Function

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NTableStylesExample.
		''' </summary>
		Public Shared ReadOnly NTableStylesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
