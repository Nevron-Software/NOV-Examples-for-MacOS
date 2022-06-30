Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NInteractiveContextPopupExample
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
            Nevron.Nov.Examples.UI.NInteractiveContextPopupExample.NInteractiveContextPopupExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NInteractiveContextPopupExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_Label = New Nevron.Nov.UI.NLabel("Click me with the right mouse button")
            Me.m_Label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Me.m_Label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Me.m_Label.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 10, Nevron.Nov.Graphics.ENFontStyle.Regular)
            Me.m_Label.TextFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black)
            Dim widget As Nevron.Nov.UI.NContentHolder = New Nevron.Nov.UI.NContentHolder(Me.m_Label)
            widget.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            widget.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            widget.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.PapayaWhip)
            widget.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            widget.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            widget.PreferredSize = New Nevron.Nov.Graphics.NSize(300, 100)
            AddHandler widget.MouseDown, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NMouseButtonEventArgs)(AddressOf Me.OnTargetWidgetMouseDown)
            Return widget
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create an interactive context popup. All you have to do is to create
	a popup window and show it using the <b>NPopupWindow.OpenInContext(...)</b> method when the user right
	clicks the widget the context is designed for.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateToggleButton(ByVal text As String, ByVal isChecked As Boolean) As Nevron.Nov.UI.NToggleButton
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(text)
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Dim button As Nevron.Nov.UI.NToggleButton = New Nevron.Nov.UI.NToggleButton(label)
            button.Checked = isChecked
            button.PreferredWidth = 20
            AddHandler button.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnToggleButtonCheckedChanged)
            Return button
        End Function

        Private Function CreatePopupContent() As Nevron.Nov.UI.NWidget
			' Create the first tool bar
			Dim toolBar1 As Nevron.Nov.UI.NToolBar = New Nevron.Nov.UI.NToolBar()
            toolBar1.Gripper.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
            toolBar1.Pendant.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
            toolBar1.Items.HorizontalSpacing = 3
            Dim fontComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            Dim i As Integer = 0, fontCount As Integer = Nevron.Nov.Examples.UI.NInteractiveContextPopupExample.Fonts.Length

            While i < fontCount
                Dim fontName As String = Nevron.Nov.Examples.UI.NInteractiveContextPopupExample.Fonts(i)
                Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(fontName)
                label.Font = New Nevron.Nov.Graphics.NFont(fontName, 8, Nevron.Nov.Graphics.ENFontStyle.Regular)
                fontComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem(label))

                If Equals(fontName, Me.m_Label.Font.Name) Then
					' Update the selected index
					fontComboBox.SelectedIndex = i
                End If

                i += 1
            End While

            AddHandler fontComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontComboBoxSelectedIndexChanged)
            toolBar1.Items.Add(fontComboBox)
            Dim fontSizeNumericUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            fontSizeNumericUpDown.Minimum = 6
            fontSizeNumericUpDown.Maximum = 32
            fontSizeNumericUpDown.Value = Me.m_Label.Font.Size
            AddHandler fontSizeNumericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontSizeNumericUpDownValueChanged)
            toolBar1.Items.Add(fontSizeNumericUpDown)

			' Create the second tool bar
			Dim toolBar2 As Nevron.Nov.UI.NToolBar = New Nevron.Nov.UI.NToolBar()
            toolBar2.Gripper.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
            toolBar2.Pendant.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
            Dim boldButton As Nevron.Nov.UI.NToggleButton = Me.CreateToggleButton("B", (Me.m_Label.Font.Style And Nevron.Nov.Graphics.ENFontStyle.Bold) = Nevron.Nov.Graphics.ENFontStyle.Bold)
            toolBar2.Items.Add(boldButton)
            Dim italicButton As Nevron.Nov.UI.NToggleButton = Me.CreateToggleButton("I", (Me.m_Label.Font.Style And Nevron.Nov.Graphics.ENFontStyle.Italic) = Nevron.Nov.Graphics.ENFontStyle.Italic)
            toolBar2.Items.Add(italicButton)
            Dim underlineButton As Nevron.Nov.UI.NToggleButton = Me.CreateToggleButton("U", (Me.m_Label.Font.Style And Nevron.Nov.Graphics.ENFontStyle.Underline) = Nevron.Nov.Graphics.ENFontStyle.Underline)
            toolBar2.Items.Add(underlineButton)
            Dim fillButton As Nevron.Nov.UI.NFillSplitButton = New Nevron.Nov.UI.NFillSplitButton()
            fillButton.SelectedValue = New Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.Graphics.NFill)(False, CType(Me.m_Label.TextFill.DeepClone(), Nevron.Nov.Graphics.NFill))
            AddHandler fillButton.SelectedValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFillButtonSelectedValueChanged)
            toolBar2.Items.Add(fillButton)

			' Add the tool bars in a stack
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(toolBar1)
            stack.Add(toolBar2)
            Return stack
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnTargetWidgetMouseDown(ByVal args As Nevron.Nov.UI.NMouseButtonEventArgs)
            If args.Button <> Nevron.Nov.UI.ENMouseButtons.Right Then Return

			' Mark the event as handled
			args.Cancel = True

			' Create and show the popup
			Dim popupContent As Nevron.Nov.UI.NWidget = Me.CreatePopupContent()
            Dim popupWindow As Nevron.Nov.UI.NPopupWindow = New Nevron.Nov.UI.NPopupWindow(popupContent)
            Call Nevron.Nov.UI.NPopupWindow.OpenInContext(popupWindow, args.CurrentTargetNode, args.ScreenPosition)
        End Sub

        Private Sub OnFontComboBoxSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim index As Integer = CInt(args.NewValue)
            Me.m_Label.Font.Name = Nevron.Nov.Examples.UI.NInteractiveContextPopupExample.Fonts(index)
        End Sub

        Private Sub OnToggleButtonCheckedChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim toggleButton As Nevron.Nov.UI.NToggleButton = CType(args.CurrentTargetNode, Nevron.Nov.UI.NToggleButton)
            Dim label As Nevron.Nov.UI.NLabel = CType(toggleButton.Content, Nevron.Nov.UI.NLabel)

            Select Case label.Text
                Case "B"
                    Me.m_Label.Font.Style = Me.m_Label.Font.Style Xor Nevron.Nov.Graphics.ENFontStyle.Bold
                Case "I"
                    Me.m_Label.Font.Style = Me.m_Label.Font.Style Xor Nevron.Nov.Graphics.ENFontStyle.Italic
                Case "U"
                    Me.m_Label.Font.Style = Me.m_Label.Font.Style Xor Nevron.Nov.Graphics.ENFontStyle.Underline
            End Select
        End Sub

        Private Sub OnFontSizeNumericUpDownValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Label.Font.Size = CDbl(args.NewValue)
        End Sub

        Private Sub OnFillButtonSelectedValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim newValue As Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.Graphics.NFill) = CType(args.NewValue, Nevron.Nov.Dom.NAutomaticValue(Of Nevron.Nov.Graphics.NFill))
            Dim fill As Nevron.Nov.Graphics.NFill = newValue.Value
            Me.m_Label.TextFill = CType(fill.DeepClone(), Nevron.Nov.Graphics.NFill)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Label As Nevron.Nov.UI.NLabel

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NInteractiveContextPopupExample.
		''' </summary>
		Public Shared ReadOnly NInteractiveContextPopupExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly Fonts As String() = New String() {Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, Nevron.Nov.Graphics.NFontDescriptor.DefaultSerifFamilyName, Nevron.Nov.Graphics.NFontDescriptor.DefaultMonoFamilyName}

		#EndRegion
	End Class
End Namespace
