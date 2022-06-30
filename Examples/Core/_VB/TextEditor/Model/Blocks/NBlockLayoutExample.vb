Imports Nevron.Nov.Dom
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI
Imports Nevron.Nov.Graphics
Imports System.Text

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create paragraphs with differnt inline formatting
	''' </summary>
	Public Class NBlockLayoutExample
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
            Nevron.Nov.Examples.Text.NBlockLayoutExample.NBlockLayoutExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NBlockLayoutExample), NExampleBaseSchema)
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
            Return "<p>This example demonstrates how to use margins, padding and borders as well as how to create floating blocks.</p>"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.GetDescriptionBlock("Block Layout", "Every block in the document follows the HTML block formatting model", 1))
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.GetDescriptionBlock("Block Margins, Padding, and Border Thickness", "Every block in the document has margins, border thickness, and padding.", 2))
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateSampleParagraph1())
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateSampleParagraph1())
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.GetNoteBlock("The distance between the above two paragraphs is 10 dips as the margins collapse", 2))
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.GetDescriptionBlock("Floating blocks", "Floating blocks can be positioned on the left of right of the the parent containing block", 2))
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateFloatingParagraph(Nevron.Nov.Text.ENFloatMode.Left))
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateNormalParagraph())
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateNormalParagraph())
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateFloatingParagraph(Nevron.Nov.Text.ENFloatMode.Right))
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateNormalParagraph())
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateNormalParagraph())
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.GetDescriptionBlock("Clear Mode", "Clear mode allows you to position blocks at a space not occupied by other blocks", 2))
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateFloatingParagraph(Nevron.Nov.Text.ENFloatMode.Left))
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateNormalParagraph())
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateNormalParagraph()
            paragraph.ClearMode = Nevron.Nov.Text.ENClearMode.Left
            section.Blocks.Add(paragraph)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.GetNoteBlock("The second paragraph has ClearMode set to the left and is not obscured by the floating block.", 2))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NBlockLayoutExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateFloatingParagraph(ByVal floatMode As Nevron.Nov.Text.ENFloatMode) As Nevron.Nov.Text.NParagraph
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(floatMode.ToString() & " flow paragraph.")
            paragraph.FloatMode = floatMode
            paragraph.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 100)
            paragraph.PreferredHeight = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 100)
            paragraph.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            paragraph.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            Return paragraph
        End Function

        Private Shared Function CreateNormalParagraph() As Nevron.Nov.Text.NParagraph
            Return New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Text.NBlockLayoutExample.GetRepeatingText("Normal flow paragraph.", 10))
        End Function

        Private Shared Function CreateSampleParagraph1() As Nevron.Nov.Text.NParagraph
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("This paragraph has margins, border thickness, and padding of 10dips.")
            paragraph.Margins = New Nevron.Nov.Graphics.NMargins(10)
            paragraph.BorderThickness = New Nevron.Nov.Graphics.NMargins(10)
            paragraph.Padding = New Nevron.Nov.Graphics.NMargins(10)
            paragraph.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
            paragraph.BackgroundFill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.White, Nevron.Nov.Graphics.NColor.LightYellow)
            Return paragraph
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
		''' <summary>
		''' Gets a paragraph with title formatting
		''' </summary>
		''' <paramname="text"></param>
		''' <returns></returns>
		Private Shared Function GetTitleParagraph(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NParagraph
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NBlockLayoutExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateLeftTagBorder(color)
            paragraph.BorderThickness = Nevron.Nov.Examples.Text.NBlockLayoutExample.defaultBorderThickness
            Return paragraph
        End Function

        Private Shared Function GetNoteBlock(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Red
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NBlockLayoutExample.GetTitleParagraphNoBorder("Note", level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.GetDescriptionParagraph(text))
            groupBlock.Border = Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NBlockLayoutExample.defaultBorderThickness
            Return groupBlock
        End Function

        Private Shared Function GetDescriptionBlock(ByVal title As String, ByVal description As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NBlockLayoutExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NBlockLayoutExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NBlockLayoutExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NBlockLayoutExample.defaultBorderThickness
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

        Private Shared Function GetLoremIpsumParagraph() As Nevron.Nov.Text.NParagraph
            Return New Nevron.Nov.Text.NParagraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum placerat in tortor nec tincidunt. Sed sagittis in sem ac auctor. Donec scelerisque molestie eros, a dictum leo fringilla eu. Vivamus porta urna non ullamcorper commodo. Nulla posuere sodales pellentesque. Donec a erat et tortor viverra euismod non et erat. Donec dictum ante eu mauris porta, eget suscipit mi ultrices. Nunc convallis adipiscing ligula, non pharetra dolor egestas at. Etiam in condimentum sapien. Praesent sagittis pulvinar metus, a posuere mauris aliquam eget.")
        End Function
		''' <summary>
		''' Gets the specified text repeated
		''' </summary>
		''' <paramname="text"></param>
		''' <paramname="count"></param>
		''' <returns></returns>
		Private Shared Function GetRepeatingText(ByVal text As String, ByVal count As Integer) As String
            Dim builder As System.Text.StringBuilder = New System.Text.StringBuilder()

            For i As Integer = 0 To count - 1

                If builder.Length > 0 Then
                    builder.Append(" ")
                End If

                builder.Append(text)
            Next

            Return builder.ToString()
        End Function

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultBorderThickness As Nevron.Nov.Graphics.NMargins = New Nevron.Nov.Graphics.NMargins(5.0, 0.0, 0.0, 0.0)

		#EndRegion
	End Class
End Namespace
