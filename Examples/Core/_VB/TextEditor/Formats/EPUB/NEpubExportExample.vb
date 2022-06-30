Imports Nevron.Nov.Dom
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Formats
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NEpubExportExample
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
            Nevron.Nov.Examples.Text.NEpubExportExample.NEpubExportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NEpubExportExample), NExampleBaseSchema)
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
            Dim exportToEpubButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Export to EPUB...")
            AddHandler exportToEpubButton.Click, AddressOf Me.OnExportToEpubButtonClick
            stack.Add(exportToEpubButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to import Electronic Publications (EPUB files) in Nevron Rich Text Editor.
</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim documentBlock As Nevron.Nov.Text.NDocumentBlock = Me.m_RichText.Content
            Dim heading1Style As Nevron.Nov.Text.NRichTextStyle = documentBlock.Styles.FindStyleByTypeAndId(Nevron.Nov.Text.ENRichTextStyleType.Paragraph, "Heading1")

			' Add chapter 1
			Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            documentBlock.Sections.Add(section)
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Chapter 1: EPUB Import")
            section.Blocks.Add(paragraph)
            heading1Style.Apply(paragraph)
            paragraph = New Nevron.Nov.Text.NParagraph("NOV Rich Text Editor lets you import Electronic Publications. " & "Thus you can use it to read e-books on your PC or MAC.")
            section.Blocks.Add(paragraph)
            paragraph = New Nevron.Nov.Text.NParagraph("You can also use it to import and edit existing Electronic Publications and books.")
            section.Blocks.Add(paragraph)

			' Add chapter 2
			section = New Nevron.Nov.Text.NSection()
            section.BreakType = Nevron.Nov.Text.ENSectionBreakType.NextPage
            documentBlock.Sections.Add(section)
            paragraph = New Nevron.Nov.Text.NParagraph("Chapter 2: EPUB Export")
            section.Blocks.Add(paragraph)
            heading1Style.Apply(paragraph)
            paragraph = New Nevron.Nov.Text.NParagraph("NOV Rich Text Editor lets you export a rich text document to an Electronic Publication. " & "Thus you can use it to create e-books on your PC or MAC.")
            section.Blocks.Add(paragraph)
            paragraph = New Nevron.Nov.Text.NParagraph("You can also use it to import and edit existing Electronic Publications and books.")
            section.Blocks.Add(paragraph)
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnExportToEpubButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' Create and show a save file dialog
			Dim saveDialog As Nevron.Nov.UI.NSaveFileDialog = New Nevron.Nov.UI.NSaveFileDialog()
            saveDialog.Title = "Export to EPUB"
            saveDialog.DefaultFileName = "MyBook.epub"
            saveDialog.FileTypes = New Nevron.Nov.UI.NFileDialogFileType() {New Nevron.Nov.UI.NFileDialogFileType(Nevron.Nov.Text.Formats.NTextFormat.Epub)}
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
		''' Schema associated with NEpubExportExample.
		''' </summary>
		Public Shared ReadOnly NEpubExportExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
