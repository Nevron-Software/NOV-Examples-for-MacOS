Imports Nevron.Nov.Barcode
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NBarcodesInRichTextExample
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
            Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.NBarcodesInRichTextExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NBarcodesInRichTextExample), NExampleBaseSchema)
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
	This example demonstrates how to embed barcodes in rich text documents as widget inlines.
	If you right click the barcode widget you will be able to edit its content and appearance
	through the ""Edit Barcode..."" option.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim documentBlock As Nevron.Nov.Text.NDocumentBlock = Me.m_RichText.Content
            documentBlock.Layout = Nevron.Nov.Text.ENTextLayout.Print
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            documentBlock.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.GetDescriptionBlock("Barcode Widget Inlines", "Nevron Open Vision lets you easily insert barcodes in text documents as widget inlines.", 1))

			' Create a table
			Dim table As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable(2, 2)
            section.Blocks.Add(table)

			' Create a linear barcode
			Dim linearBarcode As Nevron.Nov.Barcode.NLinearBarcode = New Nevron.Nov.Barcode.NLinearBarcode(Nevron.Nov.Barcode.ENLinearBarcodeSymbology.EAN13, "0123456789012")
            Dim widgetInline As Nevron.Nov.Text.NWidgetInline = New Nevron.Nov.Text.NWidgetInline(linearBarcode)

			' Create a paragraph to host the linear barcode widget inline
			Dim cell As Nevron.Nov.Text.NTableCell = table.Rows(CInt((0))).Cells(0)
            cell.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Center
            Dim paragraph As Nevron.Nov.Text.NParagraph = CType(cell.Blocks(0), Nevron.Nov.Text.NParagraph)
            paragraph.Inlines.Add(widgetInline)

			' Create a paragraph to the right with some text
			cell = table.Rows(CInt((0))).Cells(1)
            paragraph = CType(cell.Blocks(0), Nevron.Nov.Text.NParagraph)
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("The linear barcode to the left uses the EAN13 symbology."))

			' Create a QR code widget inline
			Dim qrCode As Nevron.Nov.Barcode.NMatrixBarcode = New Nevron.Nov.Barcode.NMatrixBarcode(Nevron.Nov.Barcode.ENMatrixBarcodeSymbology.QrCode, "https://www.nevron.com")
            widgetInline = New Nevron.Nov.Text.NWidgetInline(qrCode)

			' Create a paragraph to host the QR code widget inline
			cell = table.Rows(CInt((1))).Cells(0)
            cell.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Center
            paragraph = CType(cell.Blocks(0), Nevron.Nov.Text.NParagraph)
            paragraph.Inlines.Add(widgetInline)

			' Create a paragraph to the right with some text
			cell = table.Rows(CInt((1))).Cells(1)
            paragraph = CType(cell.Blocks(0), Nevron.Nov.Text.NParagraph)
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("The QR code to the left contains a link to "))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NHyperlinkInline("https://www.nevron.com", "https://www.nevron.com"))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("."))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NBarcodesInRichTextExample.
		''' </summary>
		Public Shared ReadOnly NBarcodesInRichTextExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.CreateLeftTagBorder(color)
            paragraph.BorderThickness = Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.defaultBorderThickness
            Return paragraph
        End Function

        Private Shared Function GetNoteBlock(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Red
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.GetTitleParagraphNoBorder("Note", level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.GetDescriptionParagraph(text))
            groupBlock.Border = Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.defaultBorderThickness
            Return groupBlock
        End Function

        Private Shared Function GetDescriptionBlock(ByVal title As String, ByVal description As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NBarcodesInRichTextExample.defaultBorderThickness
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

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultBorderThickness As Nevron.Nov.Graphics.NMargins = New Nevron.Nov.Graphics.NMargins(5.0, 0.0, 0.0, 0.0)

		#EndRegion
	End Class
End Namespace
