Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NCheckBoxExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NCheckBoxExample.NCheckBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NCheckBoxExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_CheckBox = New Nevron.Nov.UI.NCheckBox("Check Box")
            Me.m_CheckBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_CheckBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_CheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnCheckedChanged)
            Return Me.m_CheckBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' properties
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_CheckBox), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_CheckBox, Nevron.Nov.UI.NCheckBox.EnabledProperty, Nevron.Nov.UI.NCheckBox.CheckedProperty, Nevron.Nov.UI.NCheckBox.IndeterminateProperty, Nevron.Nov.UI.NCheckBox.SymbolContentRelationProperty)
            Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To editors.Count - 1
                propertiesStack.Add(editors(i))
            Next

            stack.Add(New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack)))

			' events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates NOV check boxes. Use the controls to the right to enable/disable the check box and to change its state and direction.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCheckedChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim checkBox As Nevron.Nov.UI.NCheckBox = CType(args.TargetNode, Nevron.Nov.UI.NCheckBox)
            Me.m_EventsLog.LogEvent("Check box " & (If(checkBox.Checked, " checked", " unchecked")))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_CheckBox As Nevron.Nov.UI.NCheckBox
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NCheckBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
