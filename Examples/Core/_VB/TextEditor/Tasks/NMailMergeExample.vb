Imports System.Globalization
Imports System.IO
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.IO
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Data
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' Demonstrates how to use the mail merge functionality of the Nevron Rich Text control.
	''' </summary>
	Public Class NMailMergeExample
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
            Nevron.Nov.Examples.Text.NMailMergeExample.NMailMergeExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NMailMergeExample), NExampleBaseSchema)
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

            If Nevron.Nov.NApplication.IntegrationPlatform <> Nevron.Nov.ENIntegrationPlatform.Silverlight Then
                Dim mergeAndSaveToFolderButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Merge & Save to Folder")
                AddHandler mergeAndSaveToFolderButton.Click, AddressOf Me.OnMergeAndSaveToFolderButtonClick
                stack.Add(mergeAndSaveToFolderButton)
            End If

            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Dim previewMailMergeUri As Nevron.Nov.NDataUri = Nevron.Nov.NDataUri.FromImage(Nevron.Nov.Text.NResources.Image_Documentation_PreviewResults_png)
            Return "
<p>
	This example demonstrates how to use the mail merge functionality of the Nevron Rich Text control.
</p>
<p>
	Click the <b>Preview Mail Merge</b> button (&nbsp;<img src=""" & previewMailMergeUri.ToString() & """ />&nbsp;) from the <b>Mailings</b> ribbon tab to see the values for the currently selected
    mail merge record. When ready click the <b>Merge & Save</b> button to save all merged documents to a file.
</p>
<p>
	The <b>Merge & Save</b> button saves each of the individual documents result of the mail
	merge operation to a folder.	
</p>
"
        End Function

        Private Sub PopulateRichText()
			' Create some text
			Dim documentBlock As Nevron.Nov.Text.NDocumentBlock = Me.m_RichText.Content
            documentBlock.Layout = Nevron.Nov.Text.ENTextLayout.Print
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            documentBlock.Sections.Add(section)
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            paragraph.Inlines.Add(Nevron.Nov.Examples.Text.NMailMergeExample.CreateMailMergeField(New Nevron.Nov.Text.NGreetingLineFieldValue()))
            section.Blocks.Add(paragraph)
            paragraph = New Nevron.Nov.Text.NParagraph()
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("We would like to offer you a unique software component that will help you leverage multiple platforms with single code base. We believe that as a "))
            paragraph.Inlines.Add(Nevron.Nov.Examples.Text.NMailMergeExample.CreateMailMergeField(New Nevron.Nov.Text.NMailMergeSourceFieldValue("Title")))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(" in your company you will be very interested in this solution. If that's the case do not hesitate to contact us in order to arrange a meeting in "))
            paragraph.Inlines.Add(Nevron.Nov.Examples.Text.NMailMergeExample.CreateMailMergeField(New Nevron.Nov.Text.NMailMergePredefinedFieldValue(Nevron.Nov.Text.ENMailMergeDataField.City)))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("."))
            section.Blocks.Add(paragraph)
            paragraph = New Nevron.Nov.Text.NParagraph()
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Best Regards,"))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Nevron Software"))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
            paragraph.Inlines.Add(New Nevron.Nov.Text.NHyperlinkInline("www.nevron.com", "https://www.nevron.com"))
            section.Blocks.Add(paragraph)

			' Load a mail merge data source from resource
			Dim stream As System.IO.Stream = Nevron.Nov.Text.NResources.Instance.GetResourceStream("RSTR_Employees_csv")
            Dim dataSource As Nevron.Nov.Text.NMailMergeDataSource = Nevron.Nov.Text.Data.NDataSourceFormat.Csv.LoadFromStream(stream, New Nevron.Nov.Text.Data.NDataSourceLoadSettings(Nothing, Nothing, True))

			' Create the field mappings
            Dim fieldMap As Nevron.Nov.Text.NMailMergeFieldMap = New Nevron.Nov.Text.NMailMergeFieldMap()
            fieldMap.[Set](Nevron.Nov.Text.ENMailMergeDataField.CourtesyTitle, "TitleOfCourtesy")
            fieldMap.[Set](Nevron.Nov.Text.ENMailMergeDataField.FirstName, "FirstName")
            fieldMap.[Set](Nevron.Nov.Text.ENMailMergeDataField.LastName, "LastName")
            fieldMap.[Set](Nevron.Nov.Text.ENMailMergeDataField.City, "City")
            dataSource.FieldMap = fieldMap
            documentBlock.MailMerge.DataSource = dataSource
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub MergeAndSaveToFolder(ByVal targetPath As String)
            Dim folder As Nevron.Nov.IO.NFolder = Nevron.Nov.IO.NFileSystem.Current.GetFolder(targetPath)

            If folder Is Nothing Then
                Call Nevron.Nov.UI.NMessageBox.Show("The entered target path does not exist", "Error", Nevron.Nov.UI.ENMessageBoxButtons.OK, Nevron.Nov.UI.ENMessageBoxIcon.[Error])
                Return
            End If

			' Clone the rich text view
			Dim clonedRichTextView As Nevron.Nov.Text.NRichTextView = CType(Me.m_RichText.DeepClone(), Nevron.Nov.Text.NRichTextView)

			' Switch the mail merge of the cloned rich text view to preview mode
			Dim mailMerge As Nevron.Nov.Text.NMailMerge = clonedRichTextView.Content.MailMerge
            mailMerge.PreviewMailMerge = True

			' Loop through all mail merge records to save individual documents to file
			For i As Integer = 0 To mailMerge.DataRecordCount - 1
				' Move to the next data source record
				mailMerge.CurrentDataRecordIndex = i

				' Save the merged document to file
				Dim fileName As String = "Document" & i.ToString(System.Globalization.CultureInfo.InvariantCulture) & ".docx"
                clonedRichTextView.SaveToFile(Nevron.Nov.IO.NPath.Current.Combine(targetPath, fileName))
            Next

            Call Nevron.Nov.UI.NMessageBox.Show("Merged documents saved to """ & targetPath & """.", "Mail Merge Complete", Nevron.Nov.UI.ENMessageBoxButtons.OK, Nevron.Nov.UI.ENMessageBoxIcon.Information)
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnMergeAndSaveToFolderButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim textBox As Nevron.Nov.UI.NTextBox = New Nevron.Nov.UI.NTextBox()
            Dim buttonStrip As Nevron.Nov.UI.NButtonStrip = New Nevron.Nov.UI.NButtonStrip()
            buttonStrip.AddOKCancelButtons()
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(textBox, buttonStrip, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2)
            Dim dialog As Nevron.Nov.UI.NTopLevelWindow = Nevron.Nov.NApplication.CreateTopLevelWindow()
            dialog.SetupDialogWindow("Enter Folder Path", False)
            dialog.Content = pairBox
            AddHandler dialog.Closed, AddressOf Me.OnEnterFolderDialogClosed
            dialog.Open()
        End Sub

        Private Sub OnEnterFolderDialogClosed(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim dialog As Nevron.Nov.UI.NTopLevelWindow = CType(arg.TargetNode, Nevron.Nov.UI.NTopLevelWindow)

            If dialog.Result = Nevron.Nov.UI.ENWindowResult.OK Then
                Dim textBox As Nevron.Nov.UI.NTextBox = CType(dialog.Content.GetFirstDescendant(Nevron.Nov.UI.NTextBox.NTextBoxSchema), Nevron.Nov.UI.NTextBox)
                Me.MergeAndSaveToFolder(textBox.Text)
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NMailMergeExample.
		''' </summary>
		Public Shared ReadOnly NMailMergeExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateMailMergeField(ByVal value As Nevron.Nov.Text.NMailMergeFieldValue) As Nevron.Nov.Text.NFieldInline
            Dim fieldInline As Nevron.Nov.Text.NFieldInline = New Nevron.Nov.Text.NFieldInline(value)
            fieldInline.FontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold
            Return fieldInline
        End Function

		#EndRegion
	End Class
End Namespace
