Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create inline elements with different formatting
	''' </summary>
	Public Class NFieldInlinesExample
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
            Nevron.Nov.Examples.Text.NFieldInlinesExample.NFieldInlinesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NFieldInlinesExample), NExampleBaseSchema)
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
            Dim updateAllFieldsButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Update All Fields")
            AddHandler updateAllFieldsButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnUpdateAllFieldsButtonClick)
            stack.Add(updateAllFieldsButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to add different field inlines as well as how to use a range visitor delegate to update all fields in a block tree.</p>
<p>Press the ""Update All Fields"" button to update all fields.</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NFieldInlinesExample.GetDescriptionBlock("Field Inlines", "The example shows how to use field inlines.", 1))
            section.Blocks.Add(Nevron.Nov.Examples.Text.NFieldInlinesExample.GetNoteBlock("Not all field values are always available. Please check the documentation for more information.", 1))

			' add numeric fields
			section.Blocks.Add(Nevron.Nov.Examples.Text.NFieldInlinesExample.GetTitleParagraph("Numeric Fields", 2))
            Dim numericFields As Nevron.Nov.Text.ENNumericFieldName() = Nevron.Nov.NEnum.GetValues(Of Nevron.Nov.Text.ENNumericFieldName)()
            Dim numericFieldNames As String() = Nevron.Nov.NEnum.GetNames(Of Nevron.Nov.Text.ENNumericFieldName)()

            For i As Integer = 0 To numericFieldNames.Length - 1
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(numericFieldNames(i) & " ["))
                Dim fieldInline As Nevron.Nov.Text.NFieldInline = New Nevron.Nov.Text.NFieldInline()
                fieldInline.Value = New Nevron.Nov.Text.NNumericFieldValue(numericFields(i))
                fieldInline.Text = "Not Updated"
                paragraph.Inlines.Add(fieldInline)
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("]"))
                section.Blocks.Add(paragraph)
            Next

			' add date time fields
			section.Blocks.Add(Nevron.Nov.Examples.Text.NFieldInlinesExample.GetTitleParagraph("Date/Time Fields", 2))
            Dim dateTimeFields As Nevron.Nov.Text.ENDateTimeFieldName() = Nevron.Nov.NEnum.GetValues(Of Nevron.Nov.Text.ENDateTimeFieldName)()
            Dim dateTimecFieldNames As String() = Nevron.Nov.NEnum.GetNames(Of Nevron.Nov.Text.ENDateTimeFieldName)()

            For i As Integer = 0 To dateTimecFieldNames.Length - 1
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(dateTimecFieldNames(i) & " ["))
                Dim fieldInline As Nevron.Nov.Text.NFieldInline = New Nevron.Nov.Text.NFieldInline()
                fieldInline.Value = New Nevron.Nov.Text.NDateTimeFieldValue(dateTimeFields(i))
                fieldInline.Text = "Not Updated"
                paragraph.Inlines.Add(fieldInline)
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("]"))
                section.Blocks.Add(paragraph)
            Next

			' add string fields
			section.Blocks.Add(Nevron.Nov.Examples.Text.NFieldInlinesExample.GetTitleParagraph("String Fields", 2))
            Dim stringFields As Nevron.Nov.Text.ENStringFieldName() = Nevron.Nov.NEnum.GetValues(Of Nevron.Nov.Text.ENStringFieldName)()
            Dim stringcFieldNames As String() = Nevron.Nov.NEnum.GetNames(Of Nevron.Nov.Text.ENStringFieldName)()

            For i As Integer = 0 To stringcFieldNames.Length - 1
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(stringcFieldNames(i) & " ["))
                Dim fieldInline As Nevron.Nov.Text.NFieldInline = New Nevron.Nov.Text.NFieldInline()
                fieldInline.Value = New Nevron.Nov.Text.NStringFieldValue(stringFields(i))
                fieldInline.Text = "Not Updated"
                paragraph.Inlines.Add(fieldInline)
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("]"))
                section.Blocks.Add(paragraph)
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"

		''' <summary>
		''' 
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnUpdateAllFieldsButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_RichText.Content.VisitRanges(Sub(ByVal range As Nevron.Nov.Text.NRangeTextElement)
                                                  Dim field As Nevron.Nov.Text.NFieldInline = TryCast(range, Nevron.Nov.Text.NFieldInline)

                                                  If field IsNot Nothing Then
                                                      field.Update()
                                                  End If
                                              End Sub)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NFieldInlinesExampleSchema As Nevron.Nov.Dom.NSchema

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
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NFieldInlinesExample.GetTitleParagraphNoBorder(text, level)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.Border = Nevron.Nov.Examples.Text.NFieldInlinesExample.CreateLeftTagBorder(color)
            paragraph.BorderThickness = Nevron.Nov.Examples.Text.NFieldInlinesExample.defaultBorderThickness
            Return paragraph
        End Function

        Private Shared Function GetNoteBlock(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Red
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NFieldInlinesExample.GetTitleParagraphNoBorder("Note", level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NFieldInlinesExample.GetDescriptionParagraph(text))
            groupBlock.Border = Nevron.Nov.Examples.Text.NFieldInlinesExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NFieldInlinesExample.defaultBorderThickness
            Return groupBlock
        End Function

        Private Shared Function GetDescriptionBlock(ByVal title As String, ByVal description As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NFieldInlinesExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NFieldInlinesExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NFieldInlinesExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NFieldInlinesExample.defaultBorderThickness
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
