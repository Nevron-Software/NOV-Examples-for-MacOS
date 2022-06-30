Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NSBColorPickerExample
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
            Nevron.Nov.Examples.UI.NSBColorPickerExample.NSBColorPickerExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NSBColorPickerExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the SB color box
			Me.m_ColorPicker = New Nevron.Nov.UI.NSBColorPicker()
            Me.m_ColorPicker.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_ColorPicker.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_ColorPicker.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSelectedColorChanged)
            Return Me.m_ColorPicker
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ColorPicker), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_ColorPicker, Nevron.Nov.UI.NSBColorPicker.UpdateWhileDraggingProperty, Nevron.Nov.UI.NSBColorPicker.SelectedColorProperty, Nevron.Nov.UI.NSBColorPicker.SBSelectorRadiusPercentProperty)
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
	This example demonstrates how to create a Saturation-Brightness color box. The SB color box lets the user modify
	the saturation and brightness of a color with a specified hue component. The hue component can be controlled through
	the <b>Hue</b> property and the currently selected color is stored in the <b>SelectedColor</b> property.
	The <b>SBSelectorRadiusPercent</b> property determines the size of the circular color selector and the 
	<b>UpdateWhileDragging</b> property specifies whether the selected color should be updated while the user drags
	the color selector or only when the user drops it. You can modify the values of all these properties using the controls
	on the rights.
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

		Private m_ColorPicker As Nevron.Nov.UI.NSBColorPicker
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NSBColorPickerExample.
		''' </summary>
		Public Shared ReadOnly NSBColorPickerExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
