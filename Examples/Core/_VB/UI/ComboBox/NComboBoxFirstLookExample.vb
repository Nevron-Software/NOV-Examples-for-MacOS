Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NComboBoxFirstLookExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NComboBoxFirstLookExample.NComboBoxFirstLookExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NComboBoxFirstLookExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' create the combo box
			Me.m_ComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_ComboBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_ComboBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_ComboBox.DropDownStyle = Nevron.Nov.UI.ENComboBoxStyle.DropDownList

			' add a few items
			For i As Integer = 0 To 10 - 1
                Me.m_ComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Item " & i.ToString()))
            Next

			' select the first item
			Me.m_ComboBox.SelectedIndex = 0

			' hook combo box selection events
			AddHandler Me.m_ComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnComboBoxSelectedIndexChanged)
            Return Me.m_ComboBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ComboBox), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_ComboBox, Nevron.Nov.UI.NComboBox.EnabledProperty, Nevron.Nov.UI.NComboBox.HorizontalPlacementProperty, Nevron.Nov.UI.NComboBox.VerticalPlacementProperty, Nevron.Nov.UI.NComboBox.DropDownButtonPositionProperty, Nevron.Nov.UI.NComboBox.SelectedIndexProperty, Nevron.Nov.UI.NComboBox.DropDownStyleProperty, Nevron.Nov.UI.NComboBox.WheelNavigationModeProperty)

            For i As Integer = 0 To editors.Count - 1
                propertiesStack.Add(editors(i))
            Next

            stack.Add(New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack)))

			' create the events list box
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a simple combo box with text only items. You can use the controls
	on the right to modify various properties of the combo box.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnComboBoxSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim comboBox As Nevron.Nov.UI.NComboBox = CType(args.TargetNode, Nevron.Nov.UI.NComboBox)
            Me.m_EventsLog.LogEvent("Selected Index: " & comboBox.SelectedIndex.ToString())
        End Sub

        Private Sub OnShowDesignerButtonClicked(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim editor As Nevron.Nov.Editors.NEditor = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ComboBox), Nevron.Nov.Dom.NNode)).CreateInstanceEditor(Me.m_ComboBox)
            Dim window As Nevron.Nov.Editors.NEditorWindow = Nevron.Nov.NApplication.CreateTopLevelWindow(Of Nevron.Nov.Editors.NEditorWindow)()
            window.Editor = editor
            window.Open()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog
        Private m_ComboBox As Nevron.Nov.UI.NComboBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NComboBoxFirstLookExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
