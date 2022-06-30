Imports Nevron.Nov.Dom
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Formats
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NPdfExportExample
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
            Nevron.Nov.Examples.Text.NPdfExportExample.NPdfExportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NPdfExportExample), NExampleBaseSchema)
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
            Dim exportToPdfButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Export to PDF...")
            AddHandler exportToPdfButton.Click, AddressOf Me.OnExportToPdfButtonClick
            stack.Add(exportToPdfButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to export a Nevron Rich Text document to PDF.
</p>
"
        End Function

        Private Sub PopulateRichText()
			' Load a document from resource
			Me.m_RichText.LoadFromResource(Nevron.Nov.Text.NResources.RBIN_DOCX_ComplexDocument_docx)
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnExportToPdfButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' Create and show a save file dialog
			Dim saveDialog As Nevron.Nov.UI.NSaveFileDialog = New Nevron.Nov.UI.NSaveFileDialog()
            saveDialog.Title = "Export to PDF"
            saveDialog.DefaultFileName = "Document1.pdf"
            saveDialog.FileTypes = New Nevron.Nov.UI.NFileDialogFileType() {New Nevron.Nov.UI.NFileDialogFileType(Nevron.Nov.Text.Formats.NTextFormat.Pdf)}
            AddHandler saveDialog.Closed, AddressOf Me.OnSaveDialogClosed
            saveDialog.RequestShow()
        End Sub

        Private Sub OnSaveDialogClosed(ByVal arg As Nevron.Nov.UI.NSaveFileDialogResult)
            If arg.Result = Nevron.Nov.UI.ENCommonDialogResult.OK Then
                Me.m_RichText.SaveToFile(arg.File)
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NPdfExportExample.
		''' </summary>
		Public Shared ReadOnly NPdfExportExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
