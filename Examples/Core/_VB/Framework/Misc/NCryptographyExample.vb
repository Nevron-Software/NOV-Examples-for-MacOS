Imports Nevron.Nov.Cryptography
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NCryptographyExample
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
            Nevron.Nov.Examples.Framework.NCryptographyExample.NCryptographyExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NCryptographyExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_DecryptedTextBox = New Nevron.Nov.UI.NTextBox("This is some text to encrypt.")
            Me.m_DecryptedTextBox.PreferredWidth = 200
            Me.m_DecryptedTextBox.Multiline = True
            Dim groupBox1 As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Decrypted Text", Me.m_DecryptedTextBox)
            groupBox1.Padding = New Nevron.Nov.Graphics.NMargins(Nevron.Nov.NDesign.HorizontalSpacing, Nevron.Nov.NDesign.VerticalSpacing)
            Me.m_EncryptedTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_EncryptedTextBox.PreferredWidth = 200
            Me.m_EncryptedTextBox.Multiline = True
            Dim groupBox2 As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Encrypted Text", Me.m_EncryptedTextBox)
            groupBox2.Padding = New Nevron.Nov.Graphics.NMargins(Nevron.Nov.NDesign.HorizontalSpacing, Nevron.Nov.NDesign.VerticalSpacing)
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(groupBox1, groupBox2, Nevron.Nov.UI.ENPairBoxRelation.Box1BeforeBox2)
            pairBox.FitMode = Nevron.Nov.Layout.ENStackFitMode.Equal
            pairBox.FillMode = Nevron.Nov.Layout.ENStackFillMode.Equal
            pairBox.Spacing = Nevron.Nov.NDesign.HorizontalSpacing
            pairBox.PreferredHeight = 300
            pairBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Return pairBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_PasswordTextBox = New Nevron.Nov.UI.NTextBox("password")
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Password", Me.m_PasswordTextBox))
            Dim encryptButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Encrypt")
            AddHandler encryptButton.Click, AddressOf Me.OnEncryptButtonClick
            stack.Add(encryptButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates how to encrypt and decrypt messages using the PK ZIP Classic encryption algorithm. Enter some text in the <b>Decrypted Text</b> text box and
    click the <b>Encrypt</b> button on the right to enrypt it. Then click the <b>Decrypt Button</b> to decrypt it.
</p>
"
        End Function

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnEncryptButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim button As Nevron.Nov.UI.NButton = CType(arg.CurrentTargetNode, Nevron.Nov.UI.NButton)
            Dim label As Nevron.Nov.UI.NLabel = CType(button.Content, Nevron.Nov.UI.NLabel)

            If Equals(label.Text, "Encrypt") Then
                Me.m_EncryptedTextBox.Text = Nevron.Nov.Cryptography.NCryptography.EncryptPkzipClassic(Me.m_DecryptedTextBox.Text, Me.m_PasswordTextBox.Text)
                Me.m_DecryptedTextBox.Text = Nothing
                label.Text = "Decrypt"
            Else
                Me.m_DecryptedTextBox.Text = Nevron.Nov.Cryptography.NCryptography.DecryptPkzipClassic(Me.m_EncryptedTextBox.Text, Me.m_PasswordTextBox.Text)
                Me.m_EncryptedTextBox.Text = Nothing
                label.Text = "Encrypt"
            End If
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DecryptedTextBox As Nevron.Nov.UI.NTextBox
        Private m_PasswordTextBox As Nevron.Nov.UI.NTextBox
        Private m_EncryptedTextBox As Nevron.Nov.UI.NTextBox

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NCryptographyExample.
        ''' </summary>
        Public Shared ReadOnly NCryptographyExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
