Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create paragraphs with differnt inline formatting
	''' </summary>
	Public Class NSelectionExample
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
            Nevron.Nov.Examples.Text.NSelectionExample.NSelectionExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NSelectionExample), NExampleBaseSchema)
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
            Dim groupsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

			' caret navigation
			If True Then
                Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Caret Navigation")
                groupsStack.Add(groupBox)
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                groupBox.Content = stack
                Dim moveToNextWordButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Move Caret to Next Word")
                AddHandler moveToNextWordButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnMoveToNextWordButtonClick)
                stack.Add(moveToNextWordButton)
                Dim moveToPrevWordButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Move Caret to Prev Word")
                AddHandler moveToPrevWordButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnMoveToPrevWordButtonClick)
                stack.Add(moveToPrevWordButton)
            End If

            If True Then
                Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Range Selection")
                groupsStack.Add(groupBox)
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                groupBox.Content = stack
                Dim selectCurrentParagraphButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Select Current Paragraph")
                AddHandler selectCurrentParagraphButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSelectCurrentParagraphButtonClick)
                stack.Add(selectCurrentParagraphButton)
                Dim deleteSelectedTextButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Delete Selected Text")
                AddHandler deleteSelectedTextButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnDeleteSelectedTextButtonClick)
                stack.Add(deleteSelectedTextButton)
            End If

            If True Then
                Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Copy / Paste")
                groupsStack.Add(groupBox)
                Dim pasteOptionsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                groupBox.Content = pasteOptionsStack

                If True Then
                    Dim copyButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Copy")
                    AddHandler copyButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnCopyButtonClick)
                    pasteOptionsStack.Add(copyButton)
                    Dim pasteButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Paste")
                    AddHandler pasteButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnPasteButtonClick)
                    pasteOptionsStack.Add(pasteButton)
                    Dim allowImagesCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Allow Images")
                    AddHandler allowImagesCheckBox.CheckedChanged, AddressOf Me.OnAllowImagesCheckBoxCheckedChanged
                    allowImagesCheckBox.Checked = True
                    pasteOptionsStack.Add(allowImagesCheckBox)
                    Dim allowTablesCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Allow Tables")
                    AddHandler allowTablesCheckBox.CheckedChanged, AddressOf Me.OnAllowTablesCheckBoxCheckedChanged
                    allowTablesCheckBox.Checked = True
                    pasteOptionsStack.Add(allowTablesCheckBox)
                    Dim allowSectionsCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Allow Sections")
                    AddHandler allowSectionsCheckBox.CheckedChanged, AddressOf Me.OnAllowSectionsCheckBoxCheckedChanged
                    allowSectionsCheckBox.Checked = True
                    pasteOptionsStack.Add(allowSectionsCheckBox)
                End If
            End If

            If True Then
                Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Inline Formatting")
                groupsStack.Add(groupBox)
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                groupBox.Content = stack
                Dim setBoldStyleButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Set Bold Style")
                AddHandler setBoldStyleButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSetBoldStyleButtonClick)
                stack.Add(setBoldStyleButton)
                Dim setItalicStyleButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Set Italic Style")
                AddHandler setItalicStyleButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSetItalicStyleButtonClick)
                stack.Add(setItalicStyleButton)
                Dim clearStyleButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Clear Style")
                AddHandler clearStyleButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnClearStyleButtonClick)
                stack.Add(clearStyleButton)
            End If

            Return groupsStack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to use the selection object in order to select text as well as how to modify selected text appearance</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NSelectionExample.GetDescriptionBlock("Working With the Selection Object", "The example demonstrates how to work with the selection object.", 1))

            For i As Integer = 0 To 10 - 1
                section.Blocks.Add(New Nevron.Nov.Text.NParagraph("Now it so happened that on one occasion the princess's golden ball did not fall into the little hand which she was holding up for it, but on to the ground beyond, and rolled straight into the water.  The king's daughter followed it with her eyes, but it vanished, and the well was deep, so deep that the bottom could not be seen.  At this she began to cry, and cried louder and louder, and could not be comforted.  And as she thus lamented someone said to her, ""What ails you, king's daughter?  You weep so that even a stone would show pity."""))
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"


		Private Sub OnClearStyleButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_RichText.Selection.ClearFontStyleFromSelectedInlines(Nevron.Nov.Graphics.ENFontStyle.Bold Or Nevron.Nov.Graphics.ENFontStyle.Italic Or Nevron.Nov.Graphics.ENFontStyle.Strikethrough Or Nevron.Nov.Graphics.ENFontStyle.Underline)
            Me.m_RichText.Focus()
        End Sub

        Private Sub OnSetItalicStyleButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_RichText.Selection.AddFontStyleToSelectedInlines(Nevron.Nov.Graphics.ENFontStyle.Italic)
            Me.m_RichText.Focus()
        End Sub

        Private Sub OnSetBoldStyleButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_RichText.Selection.AddFontStyleToSelectedInlines(Nevron.Nov.Graphics.ENFontStyle.Bold)
            Me.m_RichText.Focus()
        End Sub

        Private Sub OnPasteButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_RichText.Selection.Paste()
            Me.m_RichText.Focus()
        End Sub

        Private Sub OnCopyButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_RichText.Selection.SelectAll()
            Me.m_RichText.Selection.Copy()
            Me.m_RichText.Focus()
        End Sub

        Private Sub OnDeleteSelectedTextButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_RichText.Selection.Delete()
            Me.m_RichText.Focus()
        End Sub

        Private Sub OnAllowTablesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RichText.Selection.PasteOptions.AllowTables = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnAllowSectionsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RichText.Selection.PasteOptions.AllowSections = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnAllowImagesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RichText.Selection.PasteOptions.AllowImages = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnSelectCurrentParagraphButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim inline As Nevron.Nov.Text.NInline = Me.m_RichText.Selection.CaretInline
            If inline Is Nothing Then Return
            Dim currentParagraph As Nevron.Nov.Text.NParagraph = TryCast(inline.ParentBlock, Nevron.Nov.Text.NParagraph)

            If currentParagraph IsNot Nothing Then
                Me.m_RichText.Selection.SelectRange(currentParagraph.Range)
            End If
        End Sub

        Private Sub OnMoveToNextWordButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_RichText.Selection.MoveCaret(Nevron.Nov.Text.ENCaretMoveDirection.NextWord, False)
            Me.m_RichText.Focus()
        End Sub

        Private Sub OnMoveToPrevWordButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_RichText.Selection.MoveCaret(Nevron.Nov.Text.ENCaretMoveDirection.PrevWord, False)
            Me.m_RichText.Focus()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NSelectionExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NSelectionExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Text.NSelectionExample.CreateLeftTagBorder(color)
            paragraph.BorderThickness = Nevron.Nov.Examples.Text.NSelectionExample.defaultBorderThickness
            Return paragraph
        End Function

        Private Shared Function GetNoteBlock(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Red
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NSelectionExample.GetTitleParagraphNoBorder("Note", level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NSelectionExample.GetDescriptionParagraph(text))
            groupBlock.Border = Nevron.Nov.Examples.Text.NSelectionExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NSelectionExample.defaultBorderThickness
            Return groupBlock
        End Function

        Private Shared Function GetDescriptionBlock(ByVal title As String, ByVal description As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NSelectionExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NSelectionExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NSelectionExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NSelectionExample.defaultBorderThickness
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
