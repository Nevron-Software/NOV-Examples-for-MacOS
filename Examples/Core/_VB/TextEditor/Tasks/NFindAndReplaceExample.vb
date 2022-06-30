Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create paragraphs with different inline formatting.
	''' </summary>
	Public Class NFindAndReplaceExample
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
            Nevron.Nov.Examples.Text.NFindAndReplaceExample.NFindAndReplaceExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NFindAndReplaceExample), NExampleBaseSchema)
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
            Me.m_ReplaceTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_ReplaceTextBox.Text = "queen"
            stack.Add(New Nevron.Nov.UI.NPairBox(New Nevron.Nov.UI.NLabel("Replace:"), Me.m_ReplaceTextBox, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2))
            Dim findAllButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Find All")
            AddHandler findAllButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnFindAllButtonClick)
            stack.Add(findAllButton)
            Dim replaceAllButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Replace All")
            AddHandler replaceAllButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnReplaceAllButtonClick)
            stack.Add(replaceAllButton)
            Dim clearHighlightButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Clear Highlight")
            AddHandler clearHighlightButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnClearHighlightButtonClick)
            stack.Add(clearHighlightButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to find and replace text.</p>
<p>Press the ""Find All"" button to highlight all occurrences of ""Find"".</p>
<p>Press the ""Replace All"" button to replace and highlight all occurrences of ""Find"" with ""Replace""</p>
<p>Press the ""Clear Highlight"" button to clear all highlighting</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NFindAndReplaceExample.GetDescriptionBlock("Find and Replace Text", "The example demonstrates how to work find and replace text.", 1))

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
		Private Sub OnFindAllButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' init find settings
			Dim settings As Nevron.Nov.Text.NFindSettings = New Nevron.Nov.Text.NFindSettings()
            settings.FindWhat = Me.m_FindTextBox.Text
            settings.SearchDirection = Nevron.Nov.Text.ENSearchDirection.Forward

			' loop through all occurances
			Dim textRange As Nevron.Nov.Graphics.NRangeI = Nevron.Nov.Graphics.NRangeI.Zero

            While Me.m_RichText.EditingRoot.FindNext(settings, textRange)
                Me.m_RichText.Selection.SelectRange(textRange)
                Me.m_RichText.Selection.SetHighlightFillToSelectedInlines(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Red))
            End While

			' move caret to beginning of document
			Me.m_RichText.Selection.MoveCaret(Nevron.Nov.Text.ENCaretMoveDirection.DocumentBegin, False)
        End Sub
		''' <summary>
		''' Called when the user presses the replace all button
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnReplaceAllButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' init find settings
			Dim settings As Nevron.Nov.Text.NFindSettings = New Nevron.Nov.Text.NFindSettings()
            settings.FindWhat = Me.m_FindTextBox.Text
            settings.SearchDirection = Nevron.Nov.Text.ENSearchDirection.Forward

			' find all occurances 
			Dim textRange As Nevron.Nov.Graphics.NRangeI = Nevron.Nov.Graphics.NRangeI.Zero
            Dim selection As Nevron.Nov.Text.NSelection = Me.m_RichText.EditingRoot.Selection

            While Me.m_RichText.EditingRoot.FindNext(settings, textRange)
				' replace dog with cat
                selection.SelectRange(textRange)
                selection.InsertText(Me.m_ReplaceTextBox.Text)

                If Me.m_ReplaceTextBox.Text.Length > 0 Then
                    selection.SelectRange(New Nevron.Nov.Graphics.NRangeI(textRange.Begin, textRange.Begin + Me.m_ReplaceTextBox.Text.Length - 1))
                    selection.SetHighlightFillToSelectedInlines(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.LimeGreen))
                End If
            End While

			' move caret to beginning of document
            selection.MoveCaret(Nevron.Nov.Text.ENCaretMoveDirection.DocumentBegin, False)
        End Sub
		''' <summary>
		''' Called when the user presses clear highlight button
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnClearHighlightButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            CType(Me.m_RichText.EditingRoot, Nevron.Nov.Text.NBlock).VisitRanges(Sub(ByVal range As Nevron.Nov.Text.NRangeTextElement)
                                                                                     Dim inline As Nevron.Nov.Text.NInline = TryCast(range, Nevron.Nov.Text.NInline)

                                                                                     If inline IsNot Nothing Then
                                                                                         inline.ClearLocalValue(Nevron.Nov.Text.NInline.HighlightFillProperty)
                                                                                     End If
                                                                                 End Sub)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_FindTextBox As Nevron.Nov.UI.NTextBox
        Private m_ReplaceTextBox As Nevron.Nov.UI.NTextBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NFindAndReplaceExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NFindAndReplaceExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NFindAndReplaceExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NFindAndReplaceExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NFindAndReplaceExample.defaultBorderThickness
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
