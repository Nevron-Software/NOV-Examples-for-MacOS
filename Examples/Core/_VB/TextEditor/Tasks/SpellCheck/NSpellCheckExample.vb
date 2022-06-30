Imports System.Text
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NSpellCheckExample
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
            Nevron.Nov.Examples.Text.NSpellCheckExample.NSpellCheckExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NSpellCheckExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            stack.VerticalSpacing = 10
            Me.m_RichTextView = New Nevron.Nov.Text.NRichTextView()
            Me.m_RichTextView.Content.Sections.Clear()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichTextView.Content.Sections.Add(section)
            stack.Add(Me.m_RichTextView)
            section.Blocks.Add(New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Text.NSpellCheckExample.Text1))
            section.Blocks.Add(New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Text.NSpellCheckExample.Text2))
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim spellCheckButton As Nevron.Nov.UI.NToggleButton = New Nevron.Nov.UI.NToggleButton("Enable Spell Checking")
            AddHandler spellCheckButton.CheckedChanged, AddressOf Me.OnSpellCheckButtonCheckedChanged
            stack.Add(spellCheckButton)
            Dim suggetsionsButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Get Suggestions")
            AddHandler suggetsionsButton.Click, AddressOf Me.OnSuggetsionsButtonClick
            stack.Add(suggetsionsButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates the built in spell checker functionality.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSpellCheckButtonCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RichTextView.SpellChecker.Enabled = CBool(arg.NewValue)
        End Sub

        Private Sub OnSuggetsionsButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim paragraphs As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Text.NParagraph) = Me.m_RichTextView.Selection.GetSelectedParagraphs()
            If paragraphs.Count = 0 Then Return
            Dim paragraph As Nevron.Nov.Text.NParagraph = paragraphs(0)

			' Determine the current word
			Dim words As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Graphics.NRangeI) = paragraph.GetWordRanges()
            Dim index As Integer = Me.m_RichTextView.EditingRoot.Selection.Position.InsertIndex - paragraph.Range.Begin
            Dim hasWord As Boolean = False
            Dim wordRange As Nevron.Nov.Graphics.NRangeI = Nevron.Nov.Graphics.NRangeI.Zero

            For i As Integer = 0 To words.Count - 1

                If words(CInt((i))).Contains(index) Then
                    hasWord = True
                    wordRange = words(i)
                End If
            Next

            If Not hasWord Then
                Call Nevron.Nov.UI.NMessageBox.Show(Me.m_RichTextView.DisplayWindow, "You should click in a word first", "Warning", Nevron.Nov.UI.ENMessageBoxButtons.OK, Nevron.Nov.UI.ENMessageBoxIcon.Warning)
                Return
            End If

            Dim word As Char() = paragraph.Text.Substring(CInt((wordRange.Begin)), CInt((wordRange.GetLength() + 1))).ToCharArray()
            Dim title As String = "Suggestions for '" & New String(word) & "'"
            Dim content As String

            If Me.m_RichTextView.SpellChecker.IsCorrect(word, 0, word.Length - 1) = False Then
                Dim suggestions As Nevron.Nov.DataStructures.INIterator(Of Char()) = Me.m_RichTextView.SpellChecker.GetSuggestions(word, 0, word.Length - 1)
                Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

                While suggestions.MoveNext()

                    If sb.Length > 0 Then
                        sb.Append(Global.Microsoft.VisualBasic.Constants.vbLf)
                    End If

                    sb.Append(suggestions.Current)
                End While

                content = sb.ToString()
            Else
                content = "The word is correct."
            End If

            Call Nevron.Nov.UI.NMessageBox.Show(Me.m_RichTextView.DisplayWindow, content, title, Nevron.Nov.UI.ENMessageBoxButtons.OK)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichTextView As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NSpellCheckExample.
		''' </summary>
		Public Shared ReadOnly NSpellCheckExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Const Text1 As String = "Nevron Softuare is a globl leader in component based data vizualization technology " & "for a divrese range of Microsoft centric platforms. Built with perfectin, usability and enterprize level featurs " & "in mind, our components deliverr advanced digital dachboards and diagrams that are not to be matched."
        Private Const Text2 As String = "Tuday Nevron components are used by many Fortune 500 companis and thousands of developers " & "and IT profesionals worldwide."
        Private Const BytesInMB As Long = 1024 * 1024

		#EndRegion
	End Class
End Namespace
