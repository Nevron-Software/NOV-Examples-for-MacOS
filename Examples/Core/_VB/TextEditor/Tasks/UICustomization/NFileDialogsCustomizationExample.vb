Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Commands
Imports Nevron.Nov.Text.Formats
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NFileDialogsCustomizationExample
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
            Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.NFileDialogsCustomizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the rich text
			Dim richTextWithRibbon As Nevron.Nov.Text.NRichTextViewWithRibbon = New Nevron.Nov.Text.NRichTextViewWithRibbon()
            Me.m_RichText = richTextWithRibbon.View
            Me.m_RichText.AcceptsTab = True
            Me.m_RichText.Content.Sections.Clear()

			' Replace the "Open", "Save As" and "Insert Image" command actions with the custom ones
			Me.m_RichText.Commander.ReplaceCommandAction(New Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.CustomOpenCommandAction())
            Me.m_RichText.Commander.ReplaceCommandAction(New Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.CustomSaveAsCommandAction())
            Me.m_RichText.Commander.ReplaceCommandAction(New Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.CustomInsertImageCommandAction())

			' Populate the rich text
			Me.PopulateRichText()
            Return richTextWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to customize the NOV Rich Text Open, Save As and Insert Image file dialogs.</p>"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.GetDescriptionBlock("Ribbon Customization", "This example demonstrates how to customize the NOV rich text Open, Save As and Insert Image file dialogs.", 1))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NFileDialogsCustomizationExample.
		''' </summary>
		Public Shared ReadOnly NFileDialogsCustomizationExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.CreateLeftTagBorder(color)
            paragraph.BorderThickness = Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.DefaultBorderThickness
            Return paragraph
        End Function

        Private Shared Function GetNoteBlock(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Red
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.GetTitleParagraphNoBorder("Note", level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.GetDescriptionParagraph(text))
            groupBlock.Border = Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.DefaultBorderThickness
            Return groupBlock
        End Function

        Private Shared Function GetDescriptionBlock(ByVal title As String, ByVal description As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.DefaultBorderThickness
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

		Private Shared ReadOnly DefaultBorderThickness As Nevron.Nov.Graphics.NMargins = New Nevron.Nov.Graphics.NMargins(5.0, 0.0, 0.0, 0.0)

		#EndRegion

		#Region"Nested Types"

		Private Class CustomOpenCommandAction
            Inherits Nevron.Nov.Text.Commands.NOpenCommandAction

            Public Sub New()
            End Sub

            Shared Sub New()
                Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.CustomOpenCommandAction.CustomOpenCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.CustomOpenCommandAction), Nevron.Nov.Text.Commands.NOpenCommandAction.NOpenCommandActionSchema)
            End Sub

			''' <summary>
			''' Executes the command action.
			''' </summary>
			''' <paramname="target"></param>
			''' <paramname="parameter"></param>
			Public Overrides Sub Execute(ByVal target As Nevron.Nov.Dom.NNode, ByVal parameter As Object)
                If MyBase.IsEnabled(target) = False Then Return

				' Get the drawing view
				Dim view As Nevron.Nov.Text.NRichTextView = TryCast(MyBase.GetRichTextView(target), Nevron.Nov.Text.NRichTextView)
                If view Is Nothing Then Return
                Dim registry As Nevron.Nov.Text.Formats.NTextFormatRegistry = New Nevron.Nov.Text.Formats.NTextFormatRegistry()
                registry.DocumentFormats = New Nevron.Nov.Text.Formats.NTextFormat() {Nevron.Nov.Text.Formats.NTextFormat.Docx, Nevron.Nov.Text.Formats.NTextFormat.Rtf}
                view.OpenFile(Nothing, Nothing, True, True)
            End Sub

            Public Shared ReadOnly CustomOpenCommandActionSchema As Nevron.Nov.Dom.NSchema
        End Class

        Private Class CustomSaveAsCommandAction
            Inherits Nevron.Nov.Text.Commands.NSaveAsCommandAction

            Public Sub New()
            End Sub

            Shared Sub New()
                Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.CustomSaveAsCommandAction.CustomSaveAsCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.CustomSaveAsCommandAction), Nevron.Nov.Text.Commands.NSaveAsCommandAction.NSaveAsCommandActionSchema)
            End Sub

			''' <summary>
			''' Executes this command action.
			''' </summary>
			''' <paramname="target"></param>
			''' <paramname="parameter"></param>
			Public Overrides Sub Execute(ByVal target As Nevron.Nov.Dom.NNode, ByVal parameter As Object)
                If MyBase.IsEnabled(target) = False Then Return

				' Get the drawing view
				Dim view As Nevron.Nov.Text.NRichTextView = TryCast(MyBase.GetRichTextView(target), Nevron.Nov.Text.NRichTextView)
                If view Is Nothing Then Return
                Dim registry As Nevron.Nov.Text.Formats.NTextFormatRegistry = New Nevron.Nov.Text.Formats.NTextFormatRegistry()
                registry.DocumentFormats = New Nevron.Nov.Text.Formats.NTextFormat() {Nevron.Nov.Text.Formats.NTextFormat.Docx, Nevron.Nov.Text.Formats.NTextFormat.Rtf}
                view.SaveAs(Nevron.Nov.Text.Formats.NTextFormat.NevronBinary, registry, True)
            End Sub

            Public Shared ReadOnly CustomSaveAsCommandActionSchema As Nevron.Nov.Dom.NSchema
        End Class

        Private Class CustomInsertImageCommandAction
            Inherits Nevron.Nov.Text.Commands.NInsertImageCommandAction

            Public Sub New()
            End Sub

            Shared Sub New()
                Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.CustomInsertImageCommandAction.CustomInsertImageCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NFileDialogsCustomizationExample.CustomInsertImageCommandAction), Nevron.Nov.Text.Commands.NInsertImageCommandAction.NInsertImageCommandActionSchema)
            End Sub

            Protected Overrides Function GetFormats() As Nevron.Nov.Graphics.NImageFormat()
                Return New Nevron.Nov.Graphics.NImageFormat() {Nevron.Nov.Graphics.NImageFormat.Jpeg, Nevron.Nov.Graphics.NImageFormat.Png}
            End Function

            Public Shared ReadOnly CustomInsertImageCommandActionSchema As Nevron.Nov.Dom.NSchema
        End Class

		#EndRegion
	End Class
End Namespace
