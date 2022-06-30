Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create paragraphs with different inline formatting.
	''' </summary>
	Public Class NMultiRangeSelectionExample
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
            Nevron.Nov.Examples.Text.NMultiRangeSelectionExample.NMultiRangeSelectionExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NMultiRangeSelectionExample), NExampleBaseSchema)
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
            Me.m_FindTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_FindTextBox.Text = "princess"
            stack.Add(New Nevron.Nov.UI.NPairBox(New Nevron.Nov.UI.NLabel("Find:"), Me.m_FindTextBox, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2))
            Dim selectAll As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Select All")
            AddHandler selectAll.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSelectAllButtonClick)
            stack.Add(selectAll)
            Dim deleteAllButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Delete All")
            AddHandler deleteAllButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnDeleteAllButtonClick)
            stack.Add(deleteAllButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates the ability of the control to select multiple ranges of text.</p>
<p>Press the ""Mark All"" button to select all occurrences of ""Find"".</p>
<p>Press the ""Delete All"" button to delete all occurrences of ""Find""</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NMultiRangeSelectionExample.GetDescriptionBlock("Multiple Range Selection", "The example demonstrates how to use multiple range selection.", 1))

            For i As Integer = 0 To 10 - 1
                section.Blocks.Add(New Nevron.Nov.Text.NParagraph("Now it so happened that on one occasion the princess's golden ball did not fall into the little hand which she was holding up for it, but on to the ground beyond, and rolled straight into the water.  She followed it with her eyes, but it vanished, and the well was deep, so deep that the bottom could not be seen.  At this she began to cry, and cried louder and louder, and could not be comforted.  And as she thus lamented someone said to her, ""What ails you?  You weep so that even a stone would show pity."""))
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"

		''' <summary>
		''' Called when the user presses the find all button
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnSelectAllButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' init find settings
			Dim settings As Nevron.Nov.Text.NFindSettings = New Nevron.Nov.Text.NFindSettings()
            settings.FindWhat = Me.m_FindTextBox.Text
            settings.SearchDirection = Nevron.Nov.Text.ENSearchDirection.Forward

			' loop through all occurances
			Dim textRange As Nevron.Nov.Graphics.NRangeI = Nevron.Nov.Graphics.NRangeI.Zero

			' move caret to beginning of document
			Me.m_RichText.Selection.MoveCaret(Nevron.Nov.Text.ENCaretMoveDirection.DocumentBegin, False)

            While Me.m_RichText.EditingRoot.FindNext(settings, textRange)
                Me.m_RichText.Selection.SelectRange(textRange, True)
            End While
        End Sub
		''' <summary>
		''' Called when the user presses the replace all button
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnDeleteAllButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' delete the selection
			Me.m_RichText.EditingRoot.Selection.Delete()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_FindTextBox As Nevron.Nov.UI.NTextBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NMultiRangeSelectionExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NMultiRangeSelectionExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NMultiRangeSelectionExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NMultiRangeSelectionExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NMultiRangeSelectionExample.defaultBorderThickness
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
