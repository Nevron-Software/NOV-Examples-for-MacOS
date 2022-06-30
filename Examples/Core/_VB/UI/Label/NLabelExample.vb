Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NLabelExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NLabelExample.NLabelExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NLabelExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_Label = New Nevron.Nov.UI.NLabel()
            Me.m_Label.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
            Me.m_Label.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            Return Me.m_Label
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_SampleTextComboBox = New Nevron.Nov.UI.NComboBox()

            For i As Integer = 0 To 2 - 1
                Me.m_SampleTextComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Sample " & i.ToString()))
            Next

            AddHandler Me.m_SampleTextComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSampleTextChanged)
            Me.m_SampleTextComboBox.SelectedIndex = 0
            stack.Add(New Nevron.Nov.UI.NLabel("Sample Text:"))
            stack.Add(Me.m_SampleTextComboBox)

			' font families
			stack.Add(New Nevron.Nov.UI.NLabel("Font Family:"))
            Me.m_FontFamiliesComboBox = New Nevron.Nov.UI.NComboBox()
            Dim fontFamilies As String() = Nevron.Nov.NApplication.FontService.InstalledFontsMap.FontFamilies
            Dim selectedIndex As Integer = 0

            For i As Integer = 0 To fontFamilies.Length - 1
                Me.m_FontFamiliesComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem(fontFamilies(i)))

                If Equals(fontFamilies(i), Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName) Then
                    selectedIndex = i
                End If
            Next

            Me.m_FontFamiliesComboBox.SelectedIndex = selectedIndex
            AddHandler Me.m_FontFamiliesComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontStyleChanged)
            stack.Add(Me.m_FontFamiliesComboBox)

			' font sizes
			stack.Add(New Nevron.Nov.UI.NLabel("Font Size:"))
            Me.m_FontSizeComboBox = New Nevron.Nov.UI.NComboBox()

            For i As Integer = 5 To 72 - 1
                Me.m_FontSizeComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem(i.ToString()))
            Next

            Me.m_FontSizeComboBox.SelectedIndex = 4
            AddHandler Me.m_FontSizeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontStyleChanged)
            stack.Add(Me.m_FontSizeComboBox)

			' add style controls
			Me.m_BoldCheckBox = New Nevron.Nov.UI.NCheckBox()
            Me.m_BoldCheckBox.Content = New Nevron.Nov.UI.NLabel("Bold")
            AddHandler Me.m_BoldCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontStyleChanged)
            stack.Add(Me.m_BoldCheckBox)
            Me.m_ItalicCheckBox = New Nevron.Nov.UI.NCheckBox()
            Me.m_ItalicCheckBox.Content = New Nevron.Nov.UI.NLabel("Italic")
            AddHandler Me.m_ItalicCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontStyleChanged)
            stack.Add(Me.m_ItalicCheckBox)
            Me.m_UnderlineCheckBox = New Nevron.Nov.UI.NCheckBox()
            Me.m_UnderlineCheckBox.Content = New Nevron.Nov.UI.NLabel("Underline")
            AddHandler Me.m_UnderlineCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontStyleChanged)
            stack.Add(Me.m_UnderlineCheckBox)
            Me.m_StrikeThroughCheckBox = New Nevron.Nov.UI.NCheckBox()
            Me.m_StrikeThroughCheckBox.Content = New Nevron.Nov.UI.NLabel("Strikethrough")
            AddHandler Me.m_StrikeThroughCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontStyleChanged)
            stack.Add(Me.m_StrikeThroughCheckBox)

			' properties
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Label), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_Label, Nevron.Nov.UI.NLabel.EnabledProperty, Nevron.Nov.UI.NLabel.HorizontalPlacementProperty, Nevron.Nov.UI.NLabel.VerticalPlacementProperty, Nevron.Nov.UI.NLabel.TextAlignmentProperty, Nevron.Nov.UI.NLabel.TextWrapModeProperty)
            Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To editors.Count - 1
                propertiesStack.Add(editors(i))
            Next

			' make sure font style is updated
			Me.OnFontStyleChanged(Nothing)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack)))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use labels. The controls on the right let you
	change the label's font, alignment, placement, etc.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSampleTextChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Select Case Me.m_SampleTextComboBox.SelectedIndex
                Case 0
                    Me.m_Label.Text = "The quick brown fox jumps over the lazy dog."
                Case 1
                    Me.m_Label.Text = "This is the first line of a multi line label." & Global.Microsoft.VisualBasic.Constants.vbLf & "This is the second line."
            End Select
        End Sub

        Private Sub OnFontStyleChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim fontFamily As String = TryCast(Me.m_FontFamiliesComboBox.SelectedItem.Content, Nevron.Nov.UI.NLabel).Text
            Dim fontSize As Integer = System.Int32.Parse(TryCast(Me.m_FontSizeComboBox.SelectedItem.Content, Nevron.Nov.UI.NLabel).Text)
            Dim fontStyle As Nevron.Nov.Graphics.ENFontStyle = Nevron.Nov.Graphics.ENFontStyle.Regular

            If Me.m_BoldCheckBox.Checked Then
                fontStyle = fontStyle Or Nevron.Nov.Graphics.ENFontStyle.Bold
            End If

            If Me.m_ItalicCheckBox.Checked Then
                fontStyle = fontStyle Or Nevron.Nov.Graphics.ENFontStyle.Italic
            End If

            If Me.m_UnderlineCheckBox.Checked Then
                fontStyle = fontStyle Or Nevron.Nov.Graphics.ENFontStyle.Underline
            End If

            If Me.m_StrikeThroughCheckBox.Checked Then
                fontStyle = fontStyle Or Nevron.Nov.Graphics.ENFontStyle.Strikethrough
            End If

            Me.m_Label.Font = New Nevron.Nov.Graphics.NFont(fontFamily, fontSize, fontStyle, Nevron.Nov.Graphics.ENFontRasterizationMode.Antialiased)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Label As Nevron.Nov.UI.NLabel
        Private m_SampleTextComboBox As Nevron.Nov.UI.NComboBox
        Private m_FontFamiliesComboBox As Nevron.Nov.UI.NComboBox
        Private m_FontSizeComboBox As Nevron.Nov.UI.NComboBox
        Private m_BoldCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_ItalicCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_UnderlineCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_StrikeThroughCheckBox As Nevron.Nov.UI.NCheckBox

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NLabelExample.
		''' </summary>
		Public Shared ReadOnly NLabelExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
