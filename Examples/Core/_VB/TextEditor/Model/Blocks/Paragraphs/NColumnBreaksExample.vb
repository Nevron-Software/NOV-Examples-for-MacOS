Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to use column breaks to alter the flow of section columns
	''' </summary>
	Public Class NColumnBreaksExample
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
            Nevron.Nov.Examples.Text.NColumnBreaksExample.NColumnBreaksExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NColumnBreaksExample), NExampleBaseSchema)
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
		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
		Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to programmatically add column breaks inlines to paragraphs.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            section.ColumnCount = 2
            Me.m_RichText.Content.Sections.Add(section)
            Me.m_RichText.Content.Layout = Nevron.Nov.Text.ENTextLayout.Print
            section.Blocks.Add(Nevron.Nov.Examples.Text.NColumnBreaksExample.GetDescriptionBlock("Column Breaks", "The example shows how to add column break inlines.", 1))

            ' paragraphs with different horizontal alignment
            section.Blocks.Add(Nevron.Nov.Examples.Text.NColumnBreaksExample.GetTitleParagraph("Paragraphs can contain explicit column breaks", 2))

            For i As Integer = 0 To 23 - 1

                If i Mod 10 = 0 Then
                    section.Blocks.Add(Nevron.Nov.Examples.Text.NColumnBreaksExample.GetParagraphWithColumnBreak())
                Else
                    section.Blocks.Add(Nevron.Nov.Examples.Text.NColumnBreaksExample.GetParagraphWithoutColumnBreak())
                End If
            Next

            section.Blocks.Add(Nevron.Nov.Examples.Text.NColumnBreaksExample.GetTitleParagraph("Tables can also contain column breaks", 2))
            Dim table As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable(3, 3)

            For row As Integer = 0 To table.Rows.Count - 1

                For col As Integer = 0 To table.Columns.Count - 1
                    ' by default cells contain a single paragraph
					table.Rows(CInt((row))).Cells(CInt((col))).Blocks.Clear()
                    table.Rows(CInt((row))).Cells(CInt((col))).Blocks.Add(Nevron.Nov.Examples.Text.NColumnBreaksExample.GetParagraphWithoutColumnBreak())
                Next
            Next

            table.Rows(CInt((1))).Cells(CInt((1))).Blocks.Clear()
            table.Rows(CInt((1))).Cells(CInt((1))).Blocks.Add(Nevron.Nov.Examples.Text.NColumnBreaksExample.GetParagraphWithColumnBreak())
            section.Blocks.Add(table)
        End Sub

        #EndRegion

        #Region"Implementation"

        ''' <summary>
        ''' Gets a paragraph without an explicit column break
        ''' </summary>
        ''' <paramname="alignment"></param>
        ''' <returns></returns>
        Private Shared Function GetParagraphWithoutColumnBreak() As Nevron.Nov.Text.NParagraph
            Dim text As String = String.Empty

            For i As Integer = 0 To 10 - 1

                If text.Length > 0 Then
                    text += " "
                End If

                text += "This is a paragraph without an explicit column break."
            Next

            Return New Nevron.Nov.Text.NParagraph(text)
        End Function
        ''' <summary>
        ''' Gets a paragraph with an explicit column break
        ''' </summary>
        ''' <paramname="alignment"></param>
        ''' <returns></returns>
        Private Shared Function GetParagraphWithColumnBreak() As Nevron.Nov.Text.NParagraph
            Dim text As String = String.Empty
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()

            For i As Integer = 0 To 5 - 1

                If text.Length > 0 Then
                    text += " "
                End If

                text += "This is a paragraph with an explicit column break."
            Next

            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(text))
            Dim inline As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("Column break appears here!")
            inline.FontStyle = inline.FontStyle Or Nevron.Nov.Graphics.ENFontStyle.Bold
            paragraph.Inlines.Add(inline)
            paragraph.Inlines.Add(New Nevron.Nov.Text.NColumnBreakInline())
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(text))
            Return paragraph
        End Function

        #EndRegion

        #Region"Fields"

        Private m_RichText As Nevron.Nov.Text.NRichTextView

        #EndRegion

        #Region"Schema"

        Public Shared ReadOnly NColumnBreaksExampleSchema As Nevron.Nov.Dom.NSchema

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
		''' <summary>
		''' Gets a paragraph with title formatting
		''' </summary>
		''' <paramname="text"></param>
		''' <returns></returns>
		Private Shared Function GetTitleParagraph(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NParagraph
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NColumnBreaksExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Text.NColumnBreaksExample.CreateLeftTagBorder(color)
            paragraph.BorderThickness = Nevron.Nov.Examples.Text.NColumnBreaksExample.defaultBorderThickness
            Return paragraph
        End Function

        Private Shared Function GetDescriptionBlock(ByVal title As String, ByVal description As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NColumnBreaksExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NColumnBreaksExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NColumnBreaksExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NColumnBreaksExample.defaultBorderThickness
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
