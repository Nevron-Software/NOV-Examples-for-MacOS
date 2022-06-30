Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.Editors
Imports Nevron.Nov.DataStructures

Namespace Nevron.Nov.Examples.UI
    Public Class NLuminanceColorBarExample
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
            Nevron.Nov.Examples.UI.NLuminanceColorBarExample.NLuminanceColorBarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NLuminanceColorBarExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_LuminanceColorBar = New Nevron.Nov.UI.NLuminanceColorBar()
            Me.m_LuminanceColorBar.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_LuminanceColorBar.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_LuminanceColorBar.SelectedValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnLuminanceColorBarSelectedValueChanged)
            Return Me.m_LuminanceColorBar
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_LuminanceColorBar), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_LuminanceColorBar, Nevron.Nov.UI.NLuminanceColorBar.UpdateWhileDraggingProperty, Nevron.Nov.UI.NLuminanceColorBar.BaseColorProperty, Nevron.Nov.UI.NLuminanceColorBar.SelectedValueProperty, Nevron.Nov.UI.NLuminanceColorBar.OrientationProperty, Nevron.Nov.UI.NLuminanceColorBar.ValueSelectorExtendPercentProperty)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            Dim i As Integer = 0, editorsCount As Integer = editors.Count

            While i < editorsCount
                stack.Add(editors(i))
                i += 1
            End While

			' Modify the properties of the selected value property editor
			Dim selectedValueEditor As Nevron.Nov.Editors.NSinglePropertyEditor = CType(editors(2), Nevron.Nov.Editors.NSinglePropertyEditor)
            selectedValueEditor.Minimum = 0
            selectedValueEditor.Maximum = 1
            selectedValueEditor.[Step] = 0.01

			' Create an events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a luminance color bar. The luminance color bar allows the user to select darker or lighter variants of a given base color.
	He can modify the luminance of the <b>BaseColor</b> value by dragging a color selector. The currently selected luminance is stored in the
	<b>SelectedValue</b> property and can be in the range [0,1].
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnLuminanceColorBarSelectedValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_EventsLog.LogEvent("Selected Luminance: " & args.NewValue.ToString())
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_LuminanceColorBar As Nevron.Nov.UI.NLuminanceColorBar
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NLuminanceColorBarExample.
		''' </summary>
		Public Shared ReadOnly NLuminanceColorBarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
