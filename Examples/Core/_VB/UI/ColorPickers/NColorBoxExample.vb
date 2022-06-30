Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NColorBoxExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NColorBoxExample.NColorBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NColorBoxExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ColorBox = New Nevron.Nov.UI.NColorBox()
            Me.m_ColorBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_ColorBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_ColorBox.SelectedColorChanged, AddressOf Me.OnColorBoxSelectedColorChanged
            Return Me.m_ColorBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' create the palette select combo box
			Dim paletteComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            paletteComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem(Nevron.Nov.UI.ENColorPaletteType.MicrosoftPaint))
            paletteComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem(Nevron.Nov.UI.ENColorPaletteType.MicrosoftOffice2003))
            paletteComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem(Nevron.Nov.UI.ENColorPaletteType.MicrosoftOffice2007))
            paletteComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem(Nevron.Nov.UI.ENColorPaletteType.WebSafe))
            paletteComboBox.SelectedIndex = 2
            AddHandler paletteComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnPaletteComboBoxSelectedIndexChanged)
            stack.Add(New Nevron.Nov.UI.NPairBox("Palette:", paletteComboBox, True))

			' add come property editors
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ColorBox), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_ColorBox, Nevron.Nov.UI.NColorBox.EnabledProperty, Nevron.Nov.UI.NColorBox.ShowMoreColorsButtonProperty, Nevron.Nov.UI.NColorBox.ShowOpacitySliderInDialogProperty, Nevron.Nov.UI.NColorBox.SelectedColorProperty)

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

			' create the events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use a NOV color box (i.e. a drop down palette color picker). 
	The controls on the right let you change the palette of the picker, whether the drop down should have a
	""More Colors..."" button and which is the currently selected color.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnPaletteComboBoxSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim cbPalette As Nevron.Nov.UI.NComboBox = CType(args.TargetNode, Nevron.Nov.UI.NComboBox)

            Select Case cbPalette.SelectedIndex
                Case 0 ' "MS Paint"
                    Me.m_ColorBox.Palette = New Nevron.Nov.UI.NColorPalette(Nevron.Nov.UI.ENColorPaletteType.MicrosoftPaint)
                Case 1 ' "Office 2003":
                    Me.m_ColorBox.Palette = New Nevron.Nov.UI.NColorPalette(Nevron.Nov.UI.ENColorPaletteType.MicrosoftOffice2003)
                Case 2 ' "Office 2007":
                    Me.m_ColorBox.Palette = New Nevron.Nov.UI.NColorPalette(Nevron.Nov.UI.ENColorPaletteType.MicrosoftOffice2007)
                Case 3 ' "Web Safe":
                    Me.m_ColorBox.Palette = New Nevron.Nov.UI.NColorPalette(Nevron.Nov.UI.ENColorPaletteType.WebSafe)
            End Select
        End Sub

        Private Sub OnColorBoxSelectedColorChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim colorBox As Nevron.Nov.UI.NColorBox = CType(args.TargetNode, Nevron.Nov.UI.NColorBox)
            Me.m_EventsLog.LogEvent(Nevron.Nov.Graphics.NColor.GetNameOrHex(colorBox.SelectedColor))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ColorBox As Nevron.Nov.UI.NColorBox
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NColorBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
