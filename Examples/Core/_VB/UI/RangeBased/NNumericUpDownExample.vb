Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NNumericUpDownExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NNumericUpDownExample.NNumericUpDownExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NNumericUpDownExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_NumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_NumericUpDown.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_NumericUpDown.Minimum = 0
            Me.m_NumericUpDown.Maximum = 100
            Me.m_NumericUpDown.Value = 50
            Me.m_NumericUpDown.[Step] = 1
            Me.m_NumericUpDown.DecimalPlaces = 2
            AddHandler Me.m_NumericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnValueChanged)
            stack.Add(Me.m_NumericUpDown)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_NumericUpDown), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_NumericUpDown, Nevron.Nov.UI.NNumericUpDown.EnabledProperty, Nevron.Nov.UI.NNumericUpDown.ValueProperty, Nevron.Nov.UI.NNumericUpDown.MinimumProperty, Nevron.Nov.UI.NNumericUpDown.MaximumProperty, Nevron.Nov.UI.NNumericUpDown.StepProperty, Nevron.Nov.UI.NNumericUpDown.DecimalPlacesProperty, Nevron.Nov.UI.NNumericUpDown.WheelIncDecModeProperty, Nevron.Nov.UI.NNumericUpDown.TextSelectionModeProperty)
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
	This example demonstrates how to create and use numeric up downs. The numeric up down widget
	consists of a text box and a spinner with up and down buttons. The text box shows and also
	can be used to edit the current numeric value and accepts only digits and the decimal separator.
	The current value is stored in the <b>Value</b> property. The <b>Step</b> property defines how
	much to increase/decrease the current value when the user clicks on the increase/decrease spinner
	button or presses the up/down key from the keyboard while the numeric up down text box is focused.
	The <b>TextSelectionMode</b> controls how the text selection mode is changed when the user presses 
	the numeric up-down spin buttons.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_EventsLog.LogEvent("New value: " & args.NewValue.ToString())
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_NumericUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NNumericUpDownExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
