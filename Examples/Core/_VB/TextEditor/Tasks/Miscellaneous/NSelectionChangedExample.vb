Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically a sample report.
	''' </summary>
	Public Class NSelectionChangedExample
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
            Nevron.Nov.Examples.Text.NSelectionChangedExample.NSelectionChangedExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NSelectionChangedExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the rich text
			Me.m_RichText = New Nevron.Nov.Text.NRichTextView()
            Me.m_RichText.AcceptsTab = True
            Me.m_RichText.Content.Sections.Clear()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            section.Blocks.Add(New Nevron.Nov.Text.NParagraph("The example demonstrates how to track the selection changed event."))
            section.Blocks.Add(New Nevron.Nov.Text.NParagraph("It also shows how to identify different text elements depending on the current selection."))
            section.Blocks.Add(New Nevron.Nov.Text.NParagraph("Move the selection and the control will highlight the currently selected words as well as blocks inside the text document."))
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("You can also detect the inline elements "))
            Dim inline1 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("that have different")
            paragraph.Inlines.Add(inline1)
            Dim inline2 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline(" Font Style")
            inline2.FontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold
            paragraph.Inlines.Add(inline2)
            Dim inline3 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline(" Font Size")
            inline3.FontSize = 14
            paragraph.Inlines.Add(inline3)
            Dim inline4 As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline(" and / or other attributes")
            inline4.FontStyle = Nevron.Nov.Graphics.ENFontStyle.Italic Or Nevron.Nov.Graphics.ENFontStyle.Underline
            paragraph.Inlines.Add(inline4)
            section.Blocks.Add(paragraph)
            Me.m_RichText.Content.Sections.Add(section)
            Me.m_RichText.Content.Layout = Nevron.Nov.Text.ENTextLayout.Web
            AddHandler Me.m_RichText.Selection.SelectionChanged, AddressOf Me.Selection_SelectionChanged
            Return Me.m_RichText
        End Function

        Private Sub Selection_SelectionChanged(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.ClearHighlights()

			' highlight selected blocks
			Dim selectedBlocks As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Text.NBlock) = Me.m_RichText.Selection.GetSelectedBlocks()

            For i As Integer = 0 To selectedBlocks.Count - 1
                selectedBlocks(CInt((i))).BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightBlue)
            Next

            Dim selectedInlines As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Text.NInline) = Me.m_RichText.Selection.GetSelectedInlines(False)

            For i As Integer = 0 To selectedInlines.Count - 1
                selectedInlines(CInt((i))).HighlightFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.Yellow, 125))
            Next
        End Sub

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to track the selection changed event as well as how to query the selection object.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub ClearHighlights()
            Dim blocks As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Dom.NNode) = Me.m_RichText.Document.GetDescendants(Nevron.Nov.Text.NBlock.NBlockSchema)

            For i As Integer = 0 To blocks.Count - 1
                CType(blocks(CInt((i))), Nevron.Nov.Text.NBlock).BackgroundFill = Nothing
            Next

            Dim inlines As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Dom.NNode) = Me.m_RichText.Document.GetDescendants(Nevron.Nov.Text.NInline.NInlineSchema)

            For i As Integer = 0 To inlines.Count - 1
                CType(inlines(CInt((i))), Nevron.Nov.Text.NInline).HighlightFill = Nothing
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NSelectionChangedExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
