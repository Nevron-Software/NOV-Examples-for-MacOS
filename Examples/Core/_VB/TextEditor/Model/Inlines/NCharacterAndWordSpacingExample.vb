Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create inline elements with different formatting
	''' </summary>
	Public Class NCharacterAndWordSpacingExample
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
            Nevron.Nov.Examples.Text.NCharacterAndWordSpacingExample.NCharacterAndWordSpacingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NCharacterAndWordSpacingExample), NExampleBaseSchema)
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
            Me.m_CharacterSpacing = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CharacterSpacing.Value = 0
            AddHandler Me.m_CharacterSpacing.ValueChanged, AddressOf Me.OnCharacterSpacingFactorValueChanged
            Me.m_WordSpacing = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_WordSpacing.Value = 0
            AddHandler Me.m_WordSpacing.ValueChanged, AddressOf Me.OnWordSpacingFactorValueChanged
            stack.Add(New Nevron.Nov.UI.NPairBox(New Nevron.Nov.UI.NLabel("Character Spacing:"), Me.m_CharacterSpacing, Nevron.Nov.UI.ENPairBoxRelation.Box1BeforeBox2))
            stack.Add(New Nevron.Nov.UI.NPairBox(New Nevron.Nov.UI.NLabel("Word Spacing:"), Me.m_WordSpacing, Nevron.Nov.UI.ENPairBoxRelation.Box1BeforeBox2))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to apply character and word spacing.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)

            For i As Integer = 0 To 3 - 1
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                section.Blocks.Add(paragraph)
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("This example demonstrates the ability to apply ", Nevron.Nov.Graphics.ENFontStyle.Underline))

                If i = 1 Then
                    paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Character and Word (This is inline with modified character and word spacing) ", Nevron.Nov.Graphics.ENFontStyle.BoldItalic))
                End If

                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("spacing to inlines and individual blocks."))

                If i = 0 Then
                    paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("This paragraph has modified character and word spacing. Use the controls on the right to set different word and character spacing"))
                End If
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnWordSpacingFactorValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim textElements As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Text.NTextElement) = Me.CollectTextElements()

            For i As Integer = 0 To textElements.Count - 1
                textElements(CInt((i))).FontWordSpacing = Me.m_WordSpacing.Value
            Next
        End Sub

        Private Sub OnCharacterSpacingFactorValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim textElements As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Text.NTextElement) = Me.CollectTextElements()

            For i As Integer = 0 To textElements.Count - 1
                textElements(CInt((i))).FontCharacterSpacing = Me.m_CharacterSpacing.Value
            Next
        End Sub

		#EndRegion

		#Region"Implementation"

		''' <summary>
		''' Collects some text elements to apply character and word spacing to. You can apply 
		''' </summary>
		''' <returns></returns>
		Private Function CollectTextElements() As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Text.NTextElement)
            Dim targetElements As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Text.NTextElement) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Text.NTextElement)()
            Dim paragraphs As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Dom.NNode) = Me.m_RichText.Content.GetDescendants(Nevron.Nov.Text.NParagraph.NParagraphSchema)

            If paragraphs.Count > 0 Then
                targetElements.Add(CType(paragraphs(0), Nevron.Nov.Text.NTextElement))
            End If

            If paragraphs.Count > 1 Then
                If CType(paragraphs(CInt((1))), Nevron.Nov.Text.NParagraph).Inlines.Count > 1 Then
                    targetElements.Add(CType(paragraphs(CInt((1))), Nevron.Nov.Text.NParagraph).Inlines(1))
                End If
            End If

            Return targetElements
        End Function

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_CharacterSpacing As Nevron.Nov.UI.NNumericUpDown
        Private m_WordSpacing As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NCharacterAndWordSpacingExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NCharacterAndWordSpacingExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NCharacterAndWordSpacingExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NCharacterAndWordSpacingExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NCharacterAndWordSpacingExample.defaultBorderThickness
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
