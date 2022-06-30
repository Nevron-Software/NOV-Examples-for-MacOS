Imports System.Text
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create paragraphs with differnt inline formatting
	''' </summary>
	Public Class NBulletListsExample
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
            Nevron.Nov.Examples.Text.NBulletListsExample.NBulletListsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NBulletListsExample), NExampleBaseSchema)
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
	This example demonstrates how to set different bullet list templates to bullet lists as well as how to create nested (multilevel) bullet lists.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBulletListsExample.GetDescriptionBlock("Bullet Lists", "Bullet lists allow you to apply automatic numbering on paragraphs or groups of blocks.", 1))
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBulletListsExample.GetDescriptionBlock("Simple bullet list", "Following is a bullet list with default formatting.", 2))
            Me.CreateSampleBulletList(section, Nevron.Nov.Text.ENBulletListTemplateType.Bullet, 3, "Bullet List Item")

			' setting bullet list template type
			If True Then
                section.Blocks.Add(Nevron.Nov.Examples.Text.NBulletListsExample.GetDescriptionBlock("Bullet Lists with Different Template", "Following are bullet lists with different formatting", 2))
                Dim values As Nevron.Nov.Text.ENBulletListTemplateType() = Nevron.Nov.NEnum.GetValues(Of Nevron.Nov.Text.ENBulletListTemplateType)()
                Dim names As String() = Nevron.Nov.NEnum.GetNames(Of Nevron.Nov.Text.ENBulletListTemplateType)()

                For i As Integer = 0 To values.Length - 1 - 1
                    Me.CreateSampleBulletList(section, values(i), 3, names(i) & " bullet list item ")
                Next
            End If

			' nested bullet lists
			If True Then
                section.Blocks.Add(Nevron.Nov.Examples.Text.NBulletListsExample.GetDescriptionBlock("Bullet List Levels", "Following is an example of bullet list levels", 2))
                Dim bulletList As Nevron.Nov.Text.NBulletList = New Nevron.Nov.Text.NBulletList(Nevron.Nov.Text.ENBulletListTemplateType.[Decimal])
                Me.m_RichText.Content.BulletLists.Add(bulletList)

                For i As Integer = 0 To 3 - 1
                    Dim par1 As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Bullet List Item" & i.ToString())
                    par1.SetBulletList(bulletList, 0)
                    section.Blocks.Add(par1)

                    For j As Integer = 0 To 2 - 1
                        Dim par2 As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Nested Bullet List Item" & i.ToString())
                        par2.SetBulletList(bulletList, 1)
                        par2.MarginLeft = 20
                        section.Blocks.Add(par2)
                    Next
                Next
            End If
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub CreateSampleBulletList(ByVal section As Nevron.Nov.Text.NSection, ByVal bulletListType As Nevron.Nov.Text.ENBulletListTemplateType, ByVal items As Integer, ByVal itemText As String)
            Dim bulletList As Nevron.Nov.Text.NBulletList = New Nevron.Nov.Text.NBulletList(bulletListType)
            Me.m_RichText.Content.BulletLists.Add(bulletList)

            For i As Integer = 0 To items - 1
                Dim par As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(itemText & i.ToString())
                par.SetBulletList(bulletList, 0)
                section.Blocks.Add(par)
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NBulletListsExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NBulletListsExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NBulletListsExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NBulletListsExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NBulletListsExample.defaultBorderThickness
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
