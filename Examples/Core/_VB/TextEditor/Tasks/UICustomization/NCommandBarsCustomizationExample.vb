Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Commands
Imports Nevron.Nov.Text.UI
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NCommandBarsCustomizationExample
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
            Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.NCommandBarsCustomizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample), NExampleBaseSchema)
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
			Me.m_RichText.Commander.Add(New Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.CustomCommandAction())

			' Remove the "Edit" menu and insert a custom one
			Me.m_CommandBarBuilder = New Nevron.Nov.Text.UI.NRichTextCommandBarBuilder()
            Me.m_CommandBarBuilder.MenuDropDownBuilders.Remove(Nevron.Nov.NLoc.[Get]("Edit"))
            Me.m_CommandBarBuilder.MenuDropDownBuilders.Insert(1, New Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.CustomMenuBuilder())

			' Remove the "Standard" toolbar and insert a custom one
			Me.m_CommandBarBuilder.ToolBarBuilders.Remove(Nevron.Nov.NLoc.[Get]("Standard"))
            Me.m_CommandBarBuilder.ToolBarBuilders.Insert(0, New Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.CustomToolBarBuilder())

			' Create the command bars UI
			Return Me.m_CommandBarBuilder.CreateUI(Me.m_RichText)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to customize the NOV rich text command bars (menus and toolbars).</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.GetDescriptionBlock("Ribbon Customization", "This example demonstrates how to customize the NOV rich text command bars (menus and toolbars).", 1))
        End Sub

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCommandBarsCustomizationExample.
		''' </summary>
		Public Shared ReadOnly NCommandBarsCustomizationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_CommandBarBuilder As Nevron.Nov.Text.UI.NRichTextCommandBarBuilder

		#EndRegion

		#Region"Constants"

		Public Shared ReadOnly CustomCommand As Nevron.Nov.UI.NCommand = Nevron.Nov.UI.NCommand.Create(GetType(Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample), "CustomCommand", "Custom Command")

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.CreateLeftTagBorder(color)
            paragraph.BorderThickness = Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.defaultBorderThickness
            Return paragraph
        End Function

        Private Shared Function GetNoteBlock(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Red
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.GetTitleParagraphNoBorder("Note", level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.GetDescriptionParagraph(text))
            groupBlock.Border = Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.defaultBorderThickness
            Return groupBlock
        End Function

        Private Shared Function GetDescriptionBlock(ByVal title As String, ByVal description As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.defaultBorderThickness
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

		#Region"Nested Types"

		Public Class CustomMenuBuilder
            Inherits Nevron.Nov.UI.NMenuDropDownBuilder

            Public Sub New()
                MyBase.New("Custom Menu")
            End Sub

            Protected Overrides Sub AddItems(ByVal items As Nevron.Nov.UI.NMenuItemCollection)
				' Add the "Copy" menu item
				items.Add(MyBase.CreateMenuItem(Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, Nevron.Nov.Text.NRichTextView.CopyCommand))

				' Add the custom command menu item
				items.Add(CreateMenuItem(Nevron.Nov.Text.NResources.Image_Ribbon_16x16_smiley_png, Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.CustomCommand))
            End Sub
        End Class

        Public Class CustomToolBarBuilder
            Inherits Nevron.Nov.UI.NToolBarBuilder

            Public Sub New()
                MyBase.New("Custom Toolbar")
            End Sub

            Protected Overrides Sub AddItems(ByVal items As Nevron.Nov.UI.NCommandBarItemCollection)
				' Add the "Copy" button
				items.Add(MyBase.CreateButton(Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, Nevron.Nov.Text.NRichTextView.CopyCommand))

				' Add the custom command button
				items.Add(CreateButton(Nevron.Nov.Text.NResources.Image_Ribbon_16x16_smiley_png, Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.CustomCommand))
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
                Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.CustomCommandAction.CustomCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.CustomCommandAction), Nevron.Nov.Text.Commands.NTextCommandAction.NTextCommandActionSchema)
            End Sub

			#EndRegion

			#Region"Public Overrides"

			''' <summary>
			''' Gets the command associated with this command action.
			''' </summary>
			''' <returns></returns>
			Public Overrides Function GetCommand() As Nevron.Nov.UI.NCommand
                Return Nevron.Nov.Examples.Text.NCommandBarsCustomizationExample.CustomCommand
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
