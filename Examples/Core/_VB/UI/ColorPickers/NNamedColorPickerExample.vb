Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NNamedColorPickerExample
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
            Nevron.Nov.Examples.UI.NNamedColorPickerExample.NNamedColorPickerExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NNamedColorPickerExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_NamedColorPicker = New Nevron.Nov.UI.NNamedColorPicker()
            Me.m_NamedColorPicker.PreferredWidth = 300
            Me.m_NamedColorPicker.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_NamedColorPicker.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Fit
            AddHandler Me.m_NamedColorPicker.SelectedColorChanged, AddressOf Me.OnNamedColorPickerSelectedColorChanged
            Return Me.m_NamedColorPicker
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' add come property editors
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_NamedColorPicker), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_NamedColorPicker, Nevron.Nov.UI.NNamedColorPicker.EnabledProperty, Nevron.Nov.UI.NNamedColorPicker.HorizontalPlacementProperty, Nevron.Nov.UI.NNamedColorPicker.VerticalPlacementProperty)

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
	This example demonstrates how to create and use a NOV named color picker.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnNamedColorPickerSelectedColorChanged(ByVal arg As Nevron.Nov.Graphics.NColor)
            Me.m_EventsLog.LogEvent(Nevron.Nov.Graphics.NColor.GetNameOrHex(arg))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_NamedColorPicker As Nevron.Nov.UI.NNamedColorPicker
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NNamedColorPickerExample.
		''' </summary>
		Public Shared ReadOnly NNamedColorPickerExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
