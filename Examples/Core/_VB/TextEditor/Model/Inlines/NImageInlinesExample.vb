Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create inline elements with different formatting
	''' </summary>
	Public Class NImageInlinesExample
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
            Nevron.Nov.Examples.Text.NImageInlinesExample.NImageInlinesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NImageInlinesExample), NExampleBaseSchema)
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
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim maintainImageAspect As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Maintain Image Aspect On Resize")
            AddHandler maintainImageAspect.CheckedChanged, AddressOf Me.OnMaintainImageAspectCheckedChanged
            maintainImageAspect.Checked = True
            stack.Add(maintainImageAspect)
            Return stack
        End Function

        Private Sub OnMaintainImageAspectCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim maintainImageAspect As Boolean = CBool(arg.NewValue)
            Dim imageInlines As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Dom.NNode) = Me.m_RichText.Content.GetDescendants(Nevron.Nov.Text.NImageInline.NImageInlineSchema)

            For i As Integer = 0 To imageInlines.Count - 1
                CType(imageInlines(CInt((i))), Nevron.Nov.Text.NImageInline).MaintainAspect = maintainImageAspect
            Next
        End Sub

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to add raster and metafile image inlines to text documents. Note that metafile images scale without loss of quality. On the right side, you can control whether images will maintain their aspect ratio when resized with the mouse.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NImageInlinesExample.GetDescriptionBlock("Image Inlines", "The example shows how to add image inlines with altered preferred width and height.", 1))

			' adding a raster image with automatic size
			If True Then
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Raster image in its original size (125x100):"))
                paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                Dim imageInline As Nevron.Nov.Text.NImageInline = New Nevron.Nov.Text.NImageInline()
                imageInline.Image = Nevron.Nov.Text.NResources.Image_Artistic_FishBowl_jpg
                paragraph.Inlines.Add(imageInline)
                section.Blocks.Add(paragraph)
            End If

			' adding a raster image with specified preferred width and height
			If True Then
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Raster image with preferred width and height (250x200):"))
                paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                Dim imageInline As Nevron.Nov.Text.NImageInline = New Nevron.Nov.Text.NImageInline()
                imageInline.Image = Nevron.Nov.Text.NResources.Image_Artistic_FishBowl_jpg
                imageInline.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 250)
                imageInline.PreferredHeight = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 200)
                paragraph.Inlines.Add(imageInline)
                section.Blocks.Add(paragraph)
            End If

			' adding a metafile image with preferred width and height
			If True Then
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Metafile image with preferred width and height (125x100):"))
                paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                Dim imageInline As Nevron.Nov.Text.NImageInline = New Nevron.Nov.Text.NImageInline()
                imageInline.Image = Nevron.Nov.Text.NResources.Image_FishBowl_wmf
                imageInline.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 125)
                imageInline.PreferredHeight = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 100)
                paragraph.Inlines.Add(imageInline)
                section.Blocks.Add(paragraph)
            End If

			' adding a metafile image with preferred width and height
			If True Then
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Metafile image with preferred width and height (250x200):"))
                paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                Dim imageInline As Nevron.Nov.Text.NImageInline = New Nevron.Nov.Text.NImageInline()
                imageInline.Image = Nevron.Nov.Text.NResources.Image_FishBowl_wmf
                imageInline.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 250)
                imageInline.PreferredHeight = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 200)
                paragraph.Inlines.Add(imageInline)
                section.Blocks.Add(paragraph)
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NImageInlinesExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NImageInlinesExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NImageInlinesExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NImageInlinesExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NImageInlinesExample.defaultBorderThickness
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
