Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to apply css like styles to the text document
	''' </summary>
	Public Class NCssStylingExample
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
            Nevron.Nov.Examples.Text.NCssStylingExample.NCssStylingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NCssStylingExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the rich text
			Me.m_RichTextWithRibbon = New Nevron.Nov.Text.NRichTextViewWithRibbon()
            Me.m_RichTextWithRibbon.View.Content.Sections.Clear()

			' set the content to web and allow only text copy paste
			Me.m_RichTextWithRibbon.View.Content.Layout = Nevron.Nov.Text.ENTextLayout.Web

			' add some content
			Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()

            For i As Integer = 0 To 2 - 1
                Dim text As String = "Paragraph block contained in a section block. The paragraph will appear with a red background."
                section.Blocks.Add(New Nevron.Nov.Text.NParagraph(text))
            Next

            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            section.Blocks.Add(groupBlock)

            For i As Integer = 0 To 2 - 1
                Dim text As String = "Paragraph block contained in a group block. This paragraph will appear with a green background, italic font style, and altered font size and indentation."
                groupBlock.Blocks.Add(New Nevron.Nov.Text.NParagraph(text))
            Next

            Dim table As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable(2, 2)
            table.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            table.BorderThickness = New Nevron.Nov.Graphics.NMargins(2, 2, 2, 2)
            table.AllowSpacingBetweenCells = False
            section.Blocks.Add(table)

            For rowIndex As Integer = 0 To table.Rows.Count - 1

                For colIndex As Integer = 0 To table.Columns.Count - 1
                    Dim text As String = "Paragraph block contained in a table cell. This paragraph with appear in blue."
                    table.Rows(CInt((rowIndex))).Cells(CInt((colIndex))).Blocks.Add(New Nevron.Nov.Text.NParagraph(text))
                Next
            Next

            Me.m_RichTextWithRibbon.View.Content.Sections.Add(section)

			' create a CSS style sheet that applies different background and font depending on the parent of the paragraph
			Dim sheet As Nevron.Nov.Dom.NStyleSheet = New Nevron.Nov.Dom.NStyleSheet()
            Me.m_RichTextWithRibbon.View.Document.StyleSheets.Add(sheet)

			' for all paragraphs that are directly contained in a section
			If True Then
                Dim rule1 As Nevron.Nov.Dom.NRule = New Nevron.Nov.Dom.NRule()
                rule1.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Graphics.NFill)("BackgroundFill", New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightCoral)))
                sheet.Add(rule1)
                Dim selector1 As Nevron.Nov.Dom.NSelector = New Nevron.Nov.Dom.NSelector()
                rule1.Selectors.Add(selector1)

				' select all paragraph
				selector1.Conditions.Add(New Nevron.Nov.Dom.NTypeCondition(Nevron.Nov.Text.NParagraph.NParagraphSchema))

				' that are children of range collection
				Dim childOfBlockCollection As Nevron.Nov.Dom.NChildCombinator = New Nevron.Nov.Dom.NChildCombinator()
                selector1.Combinators.Add(childOfBlockCollection)

				' that are direct descendants of section
				Dim childOfSection As Nevron.Nov.Dom.NChildCombinator = New Nevron.Nov.Dom.NChildCombinator()
                childOfSection.Conditions.Add(New Nevron.Nov.Dom.NTypeCondition(Nevron.Nov.Text.NSection.NSectionSchema))
                selector1.Combinators.Add(childOfSection)
            End If

			' then add a rule for blocks contained inside a group block
			If True Then
                If True Then
                    Dim rule2 As Nevron.Nov.Dom.NRule = New Nevron.Nov.Dom.NRule()
                    rule2.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Graphics.NFill)("BackgroundFill", New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightGreen)))
                    rule2.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Double)("FontSize", 12.0R))
                    rule2.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Double)("FirstLineIndent", 10.0R))
                    rule2.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Boolean)("FontStyleItalic", True))
                    sheet.Add(rule2)
                    Dim builder As Nevron.Nov.Dom.NSelectorBuilder = New Nevron.Nov.Dom.NSelectorBuilder(rule2)
                    builder.Start()
                    builder.Type(Nevron.Nov.Text.NParagraph.NParagraphSchema)
                    builder.ChildOf()
                    builder.ChildOf()
                    builder.Type(Nevron.Nov.Text.NGroupBlock.NGroupBlockSchema)
                    builder.ChildOf()
                    builder.ChildOf()
                    builder.Type(Nevron.Nov.Text.NSection.NSectionSchema)
                    builder.[End]()
                End If

                If True Then
                    Dim rule3 As Nevron.Nov.Dom.NRule = New Nevron.Nov.Dom.NRule()
                    rule3.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Graphics.NFill)("BackgroundFill", New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightBlue)))
                    sheet.Add(rule3)
                    Dim builder As Nevron.Nov.Dom.NSelectorBuilder = New Nevron.Nov.Dom.NSelectorBuilder(rule3)
                    builder.Start()
                    builder.Type(Nevron.Nov.Text.NParagraph.NParagraphSchema)
                    builder.ChildOf()
                    builder.ChildOf()
                    builder.Type(Nevron.Nov.Text.NGroupBlock.NGroupBlockSchema)
                    builder.ChildOf()
                    builder.ChildOf()
                    builder.Type(Nevron.Nov.Text.NTableRow.NTableRowSchema)
                    builder.[End]()
                End If
            End If

            Return Me.m_RichTextWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to apply css like styles to the text document.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		#EndRegion

		#Region"Fields"

		Private m_RichTextWithRibbon As Nevron.Nov.Text.NRichTextViewWithRibbon

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NCssStylingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
