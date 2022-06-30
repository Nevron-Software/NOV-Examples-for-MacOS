Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NMaskedTextBoxExample
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
            Nevron.Nov.Examples.UI.NMaskedTextBoxExample.NMaskedTextBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NMaskedTextBoxExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_PhoneNumberTextBox = New Nevron.Nov.UI.NMaskedTextBox()
            Me.m_PhoneNumberTextBox.Mask = "(999) 000-0000"
            AddHandler Me.m_PhoneNumberTextBox.MaskInputRejected, AddressOf Me.OnMaskInputRejected
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Phone Number:", Me.m_PhoneNumberTextBox))
            Me.m_CreditCardNumberTextBox = New Nevron.Nov.UI.NMaskedTextBox()
            Me.m_CreditCardNumberTextBox.Mask = "0000 0000 0000 0000"
            AddHandler Me.m_CreditCardNumberTextBox.MaskInputRejected, AddressOf Me.OnMaskInputRejected
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Credit Card Number:", Me.m_CreditCardNumberTextBox))
            Me.m_SocialSecurityNumberTextBox = New Nevron.Nov.UI.NMaskedTextBox()
            Me.m_SocialSecurityNumberTextBox.Mask = "000-00-0000"
            AddHandler Me.m_SocialSecurityNumberTextBox.MaskInputRejected, AddressOf Me.OnMaskInputRejected
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Social Security Number:", Me.m_SocialSecurityNumberTextBox))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            Dim promptCharTextBox As Nevron.Nov.UI.NTextBox = New Nevron.Nov.UI.NTextBox("_")
            promptCharTextBox.MaxLength = 1
            promptCharTextBox.SelectAllOnFocus = True
            promptCharTextBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            AddHandler promptCharTextBox.TextChanged, AddressOf Me.OnPromptCharTextBoxTextChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Prompt char: ", promptCharTextBox))
            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    This example demonstrates how to create, configure and use masked text boxes. The text box on the right lets you configure
    the prompt character (i.e. the <b>PromptChar</b> property) of the sample masked text boxes.
</p>
"
        End Function

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnMaskInputRejected(ByVal arg As Nevron.Nov.UI.NMaskInputRejectedEventArgs)
            Me.m_EventsLog.LogEvent("Rejected Character: '" & arg.Character & "', reason: '" & Nevron.Nov.NStringHelpers.InsertSpacesBeforeUppersAndDigits(arg.Reason.ToString()) & "'")
        End Sub

        Private Sub OnPromptCharTextBoxTextChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim text As String = CStr(arg.NewValue)

            If Not Equals(text, Nothing) AndAlso text.Length = 1 Then
                Dim promptChar As Char = text(0)
                Me.m_PhoneNumberTextBox.PromptChar = promptChar
                Me.m_CreditCardNumberTextBox.PromptChar = promptChar
                Me.m_SocialSecurityNumberTextBox.PromptChar = promptChar
            End If
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_PhoneNumberTextBox As Nevron.Nov.UI.NMaskedTextBox
        Private m_CreditCardNumberTextBox As Nevron.Nov.UI.NMaskedTextBox
        Private m_SocialSecurityNumberTextBox As Nevron.Nov.UI.NMaskedTextBox
        Private m_EventsLog As NExampleEventsLog

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NMaskedTextBoxExample.
        ''' </summary>
        Public Shared ReadOnly NMaskedTextBoxExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
