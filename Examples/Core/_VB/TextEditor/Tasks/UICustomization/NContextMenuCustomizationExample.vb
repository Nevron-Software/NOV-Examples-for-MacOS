Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Commands
Imports Nevron.Nov.Text.UI
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NContextMenuCustomizationExample
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
            Nevron.Nov.Examples.Text.NContextMenuCustomizationExample.NContextMenuCustomizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NContextMenuCustomizationExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim richTextWithRibbon As Nevron.Nov.Text.NRichTextViewWithRibbon = New Nevron.Nov.Text.NRichTextViewWithRibbon()
            Me.m_RichText = richTextWithRibbon.View
            Me.m_RichText.AcceptsTab = True
            Me.m_RichText.Content.Sections.Clear()

			' Populate the rich text
			Me.PopulateRichText()

			' Add the custom command action to the rich text view's commander
			Me.m_RichText.Commander.Add(New Nevron.Nov.Examples.Text.NContextMenuCustomizationExample.CustomCommandAction())

			' Get the context menu builder
			Dim builder As Nevron.Nov.Text.UI.NRichTextContextMenuBuilder = Me.m_RichText.ContextMenuBuilder

			' Remove the common menu group, which contains commands such as Cut, Copy, Paste, etc.
			builder.Groups.RemoveByName(Nevron.Nov.Text.UI.NRichTextMenuGroup.Common)

			' Add the custom context menu group
			builder.Groups.Add(New Nevron.Nov.Examples.Text.NContextMenuCustomizationExample.CustomMenuGroup())
            Return richTextWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to customize the NOV rich text context menu. The common menu group,
which contains command such as Cut, Copy, Paste, etc. is removed and a custom menu group which contains
only the Copy command and a custom command is added to the context menu builder of the rich text view's
selection.</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NContextMenuCustomizationExample.GetDescriptionBlock("Context Menu Customization", "The example demonstrates how to customize the NOV rich text context menu.", 1))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NContextMenuCustomizationExample.
		''' </summary>
		Public Shared ReadOnly NContextMenuCustomizationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Public Shared ReadOnly CustomCommand As Nevron.Nov.UI.NCommand = Nevron.Nov.UI.NCommand.Create(GetType(Nevron.Nov.Examples.Text.NContextMenuCustomizationExample), "CustomCommand", "Custom Command")

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NContextMenuCustomizationExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NContextMenuCustomizationExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NContextMenuCustomizationExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NContextMenuCustomizationExample.defaultBorderThickness
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

		#Region"Nested Types"

		''' <summary>
		''' A custom menu group, which applies to every text element and adds only a Copy menu item
		''' and a custom command menu item.
		''' </summary>
		Public Class CustomMenuGroup
            Inherits Nevron.Nov.Text.UI.NRichTextMenuGroup
			''' <summary>
			''' Default constructor.
			''' </summary>
			Public Sub New()
                MyBase.New("Custom")
            End Sub

			''' <summary>
			''' Gets whether this context menu group should be shown for the given
			''' text element. Overriden to return true for every text element.
			''' </summary>
			''' <paramname="textElement"></param>
			''' <returns></returns>
			Public Overrides Function AppliesTo(ByVal textElement As Nevron.Nov.Text.NTextElement) As Boolean
                Return True
            End Function
			''' <summary>
			''' Appends the context menu action items from this group to the given menu.
			''' </summary>
			''' <paramname="menu">The menu to append to.</param>
			''' <paramname="textElement">The text element this menu is built for.</param>
			Public Overrides Sub AppendActionsTo(ByVal menu As Nevron.Nov.UI.NMenu, ByVal textElement As Nevron.Nov.Text.NTextElement)
				' Add the "Copy" command
				menu.Items.Add(MyBase.CreateMenuItem(Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, Nevron.Nov.Text.NRichTextView.CopyCommand))

				' Add the custom command
				menu.Items.Add(CreateMenuItem(Nevron.Nov.Text.NResources.Image_Ribbon_16x16_smiley_png, Nevron.Nov.Examples.Text.NContextMenuCustomizationExample.CustomCommand))
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
                Nevron.Nov.Examples.Text.NContextMenuCustomizationExample.CustomCommandAction.CustomCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NContextMenuCustomizationExample.CustomCommandAction), Nevron.Nov.Text.Commands.NTextCommandAction.NTextCommandActionSchema)
            End Sub

			#EndRegion

			#Region"Public Overrides"

			''' <summary>
			''' Gets the command associated with this command action.
			''' </summary>
			''' <returns></returns>
			Public Overrides Function GetCommand() As Nevron.Nov.UI.NCommand
                Return Nevron.Nov.Examples.Text.NContextMenuCustomizationExample.CustomCommand
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
