Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Commands
Imports Nevron.Nov.Text.UI
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NRibbonCustomizationExample
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
            Nevron.Nov.Examples.Text.NRibbonCustomizationExample.NRibbonCustomizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NRibbonCustomizationExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_RichText = New Nevron.Nov.Text.NRichTextView()
            Me.m_RichText.AcceptsTab = True
            Me.m_RichText.Content.Sections.Clear()

			' Populate the rich text
			Me.PopulateRichText()

			' Add the custom command action to the rich text view's commander
			Me.m_RichText.Commander.Add(New Nevron.Nov.Examples.Text.NRibbonCustomizationExample.CustomCommandAction())
            Me.m_RibbonBuilder = New Nevron.Nov.Text.UI.NRichTextRibbonBuilder()

			' Rename the "Home" ribbon tab page
			Dim homeTabBuilder As Nevron.Nov.UI.NRibbonTabPageBuilder = Me.m_RibbonBuilder.TabPageBuilders(Nevron.Nov.Text.UI.NRichTextRibbonBuilder.TabPageHomeName)
            homeTabBuilder.Name = "Start"

			' Rename the "Font" ribbon group of the "Home" tab page
			Dim fontGroupBuilder As Nevron.Nov.UI.NRibbonGroupBuilder = homeTabBuilder.RibbonGroupBuilders(Nevron.Nov.Text.UI.NHomeTabPageBuilder.GroupFontName)
            fontGroupBuilder.Name = "Text"

			' Remove the "Clipboard" ribbon group of the "Home" tab page
			homeTabBuilder.RibbonGroupBuilders.Remove(Nevron.Nov.Text.UI.NHomeTabPageBuilder.GroupClipboardName)

			' Insert the custom ribbon group at the beginning of the home tab page
			homeTabBuilder.RibbonGroupBuilders.Insert(0, New Nevron.Nov.Examples.Text.NRibbonCustomizationExample.CustomRibbonGroup())

			' Create the ribbon commanding UI
			Return Me.m_RibbonBuilder.CreateUI(Me.m_RichText)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to customize the NOV rich text ribbon.</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NRibbonCustomizationExample.GetDescriptionBlock("Ribbon Customization", "This example demonstrates how to customize the NOV rich text ribbon.", 1))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_RibbonBuilder As Nevron.Nov.Text.UI.NRichTextRibbonBuilder

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRibbonCustomizationExample.
		''' </summary>
		Public Shared ReadOnly NRibbonCustomizationExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NRibbonCustomizationExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Text.NRibbonCustomizationExample.CreateLeftTagBorder(color)
            paragraph.BorderThickness = Nevron.Nov.Examples.Text.NRibbonCustomizationExample.DefaultBorderThickness
            Return paragraph
        End Function

        Private Shared Function GetNoteBlock(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Red
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NRibbonCustomizationExample.GetTitleParagraphNoBorder("Note", level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NRibbonCustomizationExample.GetDescriptionParagraph(text))
            groupBlock.Border = Nevron.Nov.Examples.Text.NRibbonCustomizationExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NRibbonCustomizationExample.DefaultBorderThickness
            Return groupBlock
        End Function

        Private Shared Function GetDescriptionBlock(ByVal title As String, ByVal description As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NRibbonCustomizationExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NRibbonCustomizationExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NRibbonCustomizationExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NRibbonCustomizationExample.DefaultBorderThickness
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
        Public Shared ReadOnly CustomCommand As Nevron.Nov.UI.NCommand = Nevron.Nov.UI.NCommand.Create(GetType(Nevron.Nov.Examples.Text.NRibbonCustomizationExample), "CustomCommand", "Custom Command")

		#EndRegion

		#Region"Nested Types"

		Public Class CustomRibbonGroup
            Inherits Nevron.Nov.UI.NRibbonGroupBuilder

            Public Sub New()
                MyBase.New("Custom Group", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_smiley_png)
            End Sub

            Protected Overrides Sub AddRibbonGroupItems(ByVal items As Nevron.Nov.UI.NRibbonGroupItemCollection)
				' Add the copy command
				items.Add(CreateRibbonButton(Nevron.Nov.Text.NResources.Image_Ribbon_32x32_clipboard_copy_png, Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, Nevron.Nov.Text.NRichTextView.CopyCommand))

				' Add the custom command
				items.Add(CreateRibbonButton(Nevron.Nov.Text.NResources.Image_Ribbon_32x32_smiley_png, Nevron.Nov.Text.NResources.Image_Ribbon_16x16_smiley_png, Nevron.Nov.Examples.Text.NRibbonCustomizationExample.CustomCommand))
            End Sub
        End Class

        Public Class CustomCommandAction
            Inherits Nevron.Nov.Text.Commands.NTextCommandAction
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
                Nevron.Nov.Examples.Text.NRibbonCustomizationExample.CustomCommandAction.CustomCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NRibbonCustomizationExample.CustomCommandAction), Nevron.Nov.Text.Commands.NTextCommandAction.NTextCommandActionSchema)
            End Sub

			#EndRegion

			#Region"Public Overrides"

			''' <summary>
			''' Gets the command associated with this command action.
			''' </summary>
			''' <returns></returns>
			Public Overrides Function GetCommand() As Nevron.Nov.UI.NCommand
                Return Nevron.Nov.Examples.Text.NRibbonCustomizationExample.CustomCommand
            End Function
			''' <summary>
			''' Executes the command action.
			''' </summary>
			''' <paramname="target"></param>
			''' <paramname="parameter"></param>
			Public Overrides Sub Execute(ByVal target As Nevron.Nov.Dom.NNode, ByVal parameter As Object)
                Dim richTextView As Nevron.Nov.Text.INRichTextView = MyBase.GetRichTextView(target)
                Call Nevron.Nov.UI.NMessageBox.Show("Rich Text Custom Command executed!", "Custom Command", Nevron.Nov.UI.ENMessageBoxButtons.OK, Nevron.Nov.UI.ENMessageBoxIcon.Information)
            End Sub

			#EndRegion

			#Region"Schema"

			''' <summary>
			''' Schema associated with CustomCommandAction.
			''' </summary>
			Public Shared ReadOnly CustomCommandActionSchema As Nevron.Nov.Dom.NSchema

			#EndRegion
		End Class

		#EndRegion
	End Class
End Namespace
