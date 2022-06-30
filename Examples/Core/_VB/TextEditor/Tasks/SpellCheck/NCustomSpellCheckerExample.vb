Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.SpellCheck
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NCustomSpellCheckerExample
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
            Nevron.Nov.Examples.Text.NCustomSpellCheckerExample.NCustomSpellCheckerExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NCustomSpellCheckerExample), NExampleBase.NExampleBaseSchema)
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
            section.Blocks.Add(New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Text.NCustomSpellCheckerExample.Text1))
            section.Blocks.Add(New Nevron.Nov.Text.NParagraph(Nevron.Nov.Examples.Text.NCustomSpellCheckerExample.Text2))
            Me.m_RichTextView.SpellChecker = New Nevron.Nov.Examples.Text.NCustomSpellCheckerExample.CustomSpellchecker()

			' the following code shows how to disable the mini toolbar and leave only the text proofing context menu builder
			Me.m_RichTextView.ContextMenuBuilder.Groups.Clear()
            Me.m_RichTextView.ContextMenuBuilder.ShowMiniToolbar = False
            Me.m_RichTextView.ContextMenuBuilder.Groups.Add(New Nevron.Nov.Text.UI.NProofingMenuGroup())
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim spellCheckButton As Nevron.Nov.UI.NToggleButton = New Nevron.Nov.UI.NToggleButton("Enable Spell Checking")
            AddHandler spellCheckButton.CheckedChanged, AddressOf Me.OnSpellCheckButtonCheckedChanged
            stack.Add(spellCheckButton)
            spellCheckButton.Checked = True
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to implement custom spell check functionality. In this example the custom spellchecker searched for the word ""Nevron"" and when it finds it will underline it. This functionality also works when you type text in the editor.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSpellCheckButtonCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RichTextView.SpellChecker.Enabled = CBool(arg.NewValue)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichTextView As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCustomSpellCheckerExample.
		''' </summary>
		Public Shared ReadOnly NCustomSpellCheckerExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Const Text1 As String = "Nevron Softuare is a globl leader in component based data vizualization technology " & "for a divrese range of Microsoft centric platforms. Built with perfectin, usability and enterprize level featurs " & "in mind, our components deliverr advanced digital dachboards and diagrams that are not to be matched."
        Private Const Text2 As String = "Tuday Nevron components are used by many Fortune 500 companis and thousands of developers " & "and IT profesionals worldwide."
        Private Const BytesInMB As Long = 1024 * 1024

		#EndRegion

		#Region"Nested Types"

		''' <summary>
		''' Sample class that shows how to implement a custom spell checker
		''' </summary>
		Private Class CustomSpellchecker
            Inherits Nevron.Nov.Text.SpellCheck.NSpellChecker
			''' <summary>
			''' Default constructor (mandatory for all NOV DOM objects)
			''' </summary>
			Public Sub New()
            End Sub
			''' <summary>
			''' Static constructor (mandatory for all NOV DOM objects)
			''' </summary>
			Shared Sub New()
                Nevron.Nov.Examples.Text.NCustomSpellCheckerExample.CustomSpellchecker.CustomSpellcheckerSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NCustomSpellCheckerExample.CustomSpellchecker), Nevron.Nov.Text.SpellCheck.NSpellChecker.NSpellCheckerSchema)
            End Sub
			''' <summary>
			''' Gets a list of misspelled word ranges
			''' </summary>
			''' <paramname="chars"></param>
			''' <paramname="protectWordRange">when set to true the spellchecker must check the words that intersect the specified range as the user may be currently typing there.</param>
			''' <paramname="protectedWordRange">the protected word range</param>
			''' <returns></returns>
			Public Overrides Function GetMisspelledWordRanges(ByVal chars As Char(), ByVal protectWordRange As Boolean, ByVal protectedWordRange As Nevron.Nov.Graphics.NRangeI) As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Graphics.NRangeI)
                Dim text As String = New String(chars)
                Dim invalidWord As String = "Nevron"
                Dim index As Integer = 0
                Dim misspelledWordRanges As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Graphics.NRangeI) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Graphics.NRangeI)()

                While (CSharpImpl.__Assign(index, text.IndexOf(invalidWord, index))) <> -1
                    Dim currentRange As Nevron.Nov.Graphics.NRangeI = New Nevron.Nov.Graphics.NRangeI(index, index + invalidWord.Length - 1)
					' skip the currently protected spellcheck word, because the user is currently typing in there
					If Not protectWordRange OrElse Not protectedWordRange.Equals(currentRange) Then
                        misspelledWordRanges.Add(currentRange)
                    End If

                    index += invalidWord.Length
                End While

                Return misspelledWordRanges
            End Function

            Public Overrides Function GetSuggestions(ByVal chars As Char(), ByVal beginIndex As Integer, ByVal endIndex As Integer) As Nevron.Nov.DataStructures.INIterator(Of Char())
                Return MyBase.GetSuggestions(chars, beginIndex, endIndex)
            End Function

            Private Shared ReadOnly CustomSpellcheckerSchema As Nevron.Nov.Dom.NSchema

            Private Class CSharpImpl
                <System.Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
                Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                    target = value
                    Return value
                End Function
            End Class
        End Class
    End Class

	#EndRegion
End Namespace
