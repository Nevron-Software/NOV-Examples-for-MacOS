Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NPaletteColorPickerExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NPaletteColorPickerExample.NPaletteColorPickerExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NPaletteColorPickerExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Protected Overrides - Examples Content"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_PaletteColorPicker = New Nevron.Nov.UI.NPaletteColorPicker()
            Me.m_PaletteColorPicker.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_PaletteColorPicker.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_PaletteColorPicker.SelectedIndexChanged, AddressOf Me.OnSelectedIndexChanged
            stack.Add(Me.m_PaletteColorPicker)
            Return stack
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
            paletteComboBox.SelectedIndex = 0
            AddHandler paletteComboBox.SelectedIndexChanged, AddressOf Me.OnPaletteComboBoxSelectedIndexChanged
            stack.Add(New Nevron.Nov.UI.NPairBox("Palette:", paletteComboBox, True))

			' add some property editors
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_PaletteColorPicker), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_PaletteColorPicker, Nevron.Nov.UI.NPaletteColorPicker.EnabledProperty, Nevron.Nov.UI.NPaletteColorPicker.CyclicKeyboardNavigationProperty, Nevron.Nov.UI.NPaletteColorPicker.SelectedIndexProperty)

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
	This example demonstrates how to create a palette color picker. The palette color picker is table picker
	that lets the user pick a color from a palette of colors. The palette to use may be passed as a parameter
	to the palette color picker constructor or may be assigned to its <b>Palette</b> property. You can use the
	controls on the right to change the currently used color palette.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnPaletteComboBoxSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim cbPalette As Nevron.Nov.UI.NComboBox = CType(args.TargetNode, Nevron.Nov.UI.NComboBox)

            Select Case cbPalette.SelectedIndex
                Case 0 ' "MS Paint"
                    Me.m_PaletteColorPicker.Palette = New Nevron.Nov.UI.NColorPalette(Nevron.Nov.UI.ENColorPaletteType.MicrosoftPaint)
                Case 1 ' "Office 2003":
                    Me.m_PaletteColorPicker.Palette = New Nevron.Nov.UI.NColorPalette(Nevron.Nov.UI.ENColorPaletteType.MicrosoftOffice2003)
                Case 2 ' "Office 2007":
                    Me.m_PaletteColorPicker.Palette = New Nevron.Nov.UI.NColorPalette(Nevron.Nov.UI.ENColorPaletteType.MicrosoftOffice2007)
                Case 3 ' "Web Safe":
                    Me.m_PaletteColorPicker.Palette = New Nevron.Nov.UI.NColorPalette(Nevron.Nov.UI.ENColorPaletteType.WebSafe)
            End Select
        End Sub

        Private Sub OnSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim colorPicker As Nevron.Nov.UI.NPaletteColorPicker = CType(args.TargetNode, Nevron.Nov.UI.NPaletteColorPicker)
            Dim selectedColor As Nevron.Nov.Graphics.NColor = colorPicker.SelectedColor
            Me.m_EventsLog.LogEvent(selectedColor.GetHEX().ToUpper())
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_PaletteColorPicker As Nevron.Nov.UI.NPaletteColorPicker
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NPaletteColorPickerExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
