Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NHSBWheelColorPickerExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NHSBWheelColorPickerExample.NHSBWheelColorPickerExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NHSBWheelColorPickerExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the HSB color picker
			Me.m_ColorPicker = New Nevron.Nov.UI.NHsbWheelColorPicker()
            Me.m_ColorPicker.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_ColorPicker.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_ColorPicker.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSelectedColorChanged)
            Return Me.m_ColorPicker
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ColorPicker), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_ColorPicker, Nevron.Nov.UI.NHsbWheelColorPicker.UpdateWhileDraggingProperty, Nevron.Nov.UI.NHsbWheelColorPicker.SelectedColorProperty, Nevron.Nov.UI.NHsbWheelColorPicker.HueSelectorSectorAngleProperty, Nevron.Nov.UI.NHsbWheelColorPicker.HueSelectorExtendPercentProperty, Nevron.Nov.UI.NHsbWheelColorPicker.HueWheelWidthPercentProperty, Nevron.Nov.UI.NHsbWheelColorPicker.SBSelectorRadiusPercentProperty, Nevron.Nov.UI.NHsbWheelColorPicker.SBTriangleMarginsPercentProperty)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create, configure and use an HSB Wheel Color Picker. The HSB Wheel Color Picker
	is a color picker that consists of a hue color wheel and a Saturation-Brightness triangle. The controls on the right
	let you control the appearance and the behavior of the picker.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSelectedColorChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim selectedColor As Nevron.Nov.Graphics.NColor = CType(args.NewValue, Nevron.Nov.Graphics.NColor)
            Me.m_EventsLog.LogEvent(Nevron.Nov.Graphics.NColor.GetNameOrHex(selectedColor))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ColorPicker As Nevron.Nov.UI.NHsbWheelColorPicker
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NHSBWheelColorPickerExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
