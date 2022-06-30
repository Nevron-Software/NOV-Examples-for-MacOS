Imports System
Imports System.IO
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NDocxImportExample
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
            Nevron.Nov.Examples.Text.NDocxImportExample.NDocxImportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NDocxImportExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the rich text
			Dim richTextWithRibbon As Nevron.Nov.Text.NRichTextViewWithRibbon = New Nevron.Nov.Text.NRichTextViewWithRibbon()
            Me.m_RichText = richTextWithRibbon.View
            Me.m_RichText.AcceptsTab = True
            Me.m_RichText.Content.Sections.Clear()
            Return richTextWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim predefinedDocumentGroupBox As Nevron.Nov.UI.NGroupBox = Me.CreatePredefinedDocumentGroupBox()
            predefinedDocumentGroupBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Return predefinedDocumentGroupBox
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates the DOCX import capabilities of Nevron Text control.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreatePredefinedDocumentGroupBox() As Nevron.Nov.UI.NGroupBox
            Const DocxSuffix As String = "_docx"
            Dim testListBox As Nevron.Nov.UI.NListBox = New Nevron.Nov.UI.NListBox()
            Dim resourceName As String() = Nevron.Nov.Text.NResources.Instance.GetResourceNames()
            Dim i As Integer = 0, count As Integer = resourceName.Length

            While i < count
                Dim resName As String = resourceName(i)

                If resName.EndsWith(DocxSuffix, System.StringComparison.Ordinal) Then
					' The current resource is a DOCX document, so add it to the list box
					Dim testName As String = resName.Substring(0, resName.Length - DocxSuffix.Length)
                    testName = testName.Substring(testName.LastIndexOf("_"c) + 1)
                    Dim item As Nevron.Nov.UI.NListBoxItem = New Nevron.Nov.UI.NListBoxItem(Nevron.Nov.NStringHelpers.InsertSpacesBeforeUppersAndDigits(testName))
                    item.Tag = resName
                    testListBox.Items.Add(item)
                End If

                i += 1
            End While

            AddHandler testListBox.Selection.Selected, AddressOf Me.OnListBoxItemSelected
            testListBox.Selection.SingleSelect(testListBox.Items(1))
            Return New Nevron.Nov.UI.NGroupBox("Predefined DOCX documents", testListBox)
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnListBoxItemSelected(ByVal arg1 As Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))
            If arg1.TargetNode Is Nothing Then Return

			' Determine the full name of the selected resource
			Dim resName As String = CStr(arg1.Item.Tag)

			' Read the stream
			Using stream As System.IO.Stream = Nevron.Nov.Text.NResources.Instance.GetResourceStream(resName)
                Me.m_RichText.LoadFromStream(stream)
            End Using
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NDocxImportExample.
		''' </summary>
		Public Shared ReadOnly NDocxImportExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
