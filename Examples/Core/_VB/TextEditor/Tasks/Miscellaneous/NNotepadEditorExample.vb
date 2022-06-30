Imports Nevron.Nov.Dom
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to create a simple "Notepad" like editor.
	''' </summary>
	Public Class NNotepadEditorExample
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
            Nevron.Nov.Examples.Text.NNotepadEditorExample.NNotepadEditorExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NNotepadEditorExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the rich text
			Me.m_RichText = New Nevron.Nov.Text.NRichTextView()
            Me.m_RichText.AcceptsTab = True

			' make sure line breaks behave like a normal text box
			Me.m_RichText.ViewSettings.ExtendLineBreakWithSpaces = False
            Me.m_RichText.Content.Sections.Clear()

			' set the content to web and allow only text copy paste
			Me.m_RichText.Content.Layout = Nevron.Nov.Text.ENTextLayout.Web
			' m_RichText.Content.Selection.ClipboardTextFormats = new NClipboardTextFormat[] { NTextClipboardTextFormat.Instance };

			' add some content
			Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()

            For i As Integer = 0 To 10 - 1
                Dim text As String = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborumstring."
                section.Blocks.Add(New Nevron.Nov.Text.NParagraph(text))
            Next

            Me.m_RichText.Content.Sections.Add(section)
            Me.m_RichText.HRuler.Visibility = Nevron.Nov.UI.ENVisibility.Visible
            Me.m_RichText.VRuler.Visibility = Nevron.Nov.UI.ENVisibility.Visible
            Return Me.m_RichText
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_WordWrapCheckBox = New Nevron.Nov.UI.NCheckBox("Word Wrap")
            AddHandler Me.m_WordWrapCheckBox.CheckedChanged, AddressOf Me.OnWordWrapCheckBoxCheckedChanged
            stack.Add(Me.m_WordWrapCheckBox)
            Me.m_WordWrapCheckBox.Checked = False
            Me.OnWordWrapCheckBoxCheckedChanged(Nothing)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to create a simple ""Notepad"" like editor.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnWordWrapCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RichText.Content.WrapMinWidth = Me.m_WordWrapCheckBox.Checked
            Me.m_RichText.Content.WrapDesiredWidth = Me.m_WordWrapCheckBox.Checked
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_WordWrapCheckBox As Nevron.Nov.UI.NCheckBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NNotepadEditorExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
