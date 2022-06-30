Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NHexagonColorPickerExample
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
            Nevron.Nov.Examples.UI.NHexagonColorPickerExample.NHexagonColorPickerExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NHexagonColorPickerExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_HexagonColorPicker = New Nevron.Nov.UI.NHexagonColorPicker()
            Me.m_HexagonColorPicker.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_HexagonColorPicker.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_HexagonColorPicker.SelectedIndexChanged, AddressOf Me.OnHexagonColorPickerSelectedIndexChanged
            Return Me.m_HexagonColorPicker
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' Add some property editors
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_HexagonColorPicker), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_HexagonColorPicker, Nevron.Nov.UI.NHexagonColorPicker.EnabledProperty, Nevron.Nov.UI.NHexagonColorPicker.SelectedIndexProperty, Nevron.Nov.UI.NHexagonColorPicker.HorizontalPlacementProperty, Nevron.Nov.UI.NHexagonColorPicker.VerticalPlacementProperty)

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
	This example demonstrates how to create a hexagon color picker. The hexagon color picker is color picker
	that lets the user pick a color from a set of standard colors. The selected color can be get or set
	through the <b>SelectedColor</b> property of the picker.
</p>
<p>
	The desired size of the picker is determined by the desired size of a cell, which is controlled through
	the <b>CellSideLength</b> property. If the picker is larger or smaller than its desired size, its cells
	are scaled automatically to fill the available area.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnHexagonColorPickerSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim colorPicker As Nevron.Nov.UI.NHexagonColorPicker = CType(arg.TargetNode, Nevron.Nov.UI.NHexagonColorPicker)
            Dim selectedColor As Nevron.Nov.Graphics.NColor = colorPicker.SelectedColor
            Me.m_EventsLog.LogEvent(selectedColor.GetHEX().ToUpper())
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_HexagonColorPicker As Nevron.Nov.UI.NHexagonColorPicker
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NHexagonColorPickerExample.
		''' </summary>
		Public Shared ReadOnly NHexagonColorPickerExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
