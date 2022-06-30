Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NListBoxSelectionExample
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
            Nevron.Nov.Examples.UI.NListBoxSelectionExample.NListBoxSelectionExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NListBoxSelectionExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a list box
			Me.m_ListBox = New Nevron.Nov.UI.NListBox()
            Me.m_ListBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_ListBox.PreferredSize = New Nevron.Nov.Graphics.NSize(200, 400)

			' Add some items
			For i As Integer = 0 To 100 - 1
                Me.m_ListBox.Items.Add(New Nevron.Nov.UI.NListBoxItem("Item " & i.ToString()))
            Next

			' Hook to list box selection events
			AddHandler Me.m_ListBox.Selection.Selected, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))(AddressOf Me.OnListBoxItemSelected)
            AddHandler Me.m_ListBox.Selection.Deselected, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))(AddressOf Me.OnListBoxItemDeselected)
            Return Me.m_ListBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' Create the properties group box
			Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ListBox), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_ListBox, Nevron.Nov.UI.NListBox.EnabledProperty, Nevron.Nov.UI.NListBox.HorizontalPlacementProperty, Nevron.Nov.UI.NListBox.VerticalPlacementProperty)
            Dim i As Integer = 0, count As Integer = editors.Count

            While i < count
                propertiesStack.Add(editors(i))
                i += 1
            End While

            Dim editor As Nevron.Nov.Editors.NPropertyEditor = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ListBox.Selection), Nevron.Nov.Dom.NNode)).CreatePropertyEditor(Me.m_ListBox.Selection, Nevron.Nov.UI.NListBoxSelection.ModeProperty)
            Dim label As Nevron.Nov.UI.NLabel = CType(editor.GetDescendants(New Nevron.Nov.Dom.NInstanceOfSchemaFilter(Nevron.Nov.UI.NLabel.NLabelSchema))(0), Nevron.Nov.UI.NLabel)
            label.Text = "Selection Mode:"
            propertiesStack.Add(editor)
            Dim propertiesGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack))
            stack.Add(propertiesGroupBox)

			' Create the events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to work with list box selection. You can change the selection mode
	using the ""Selection Mode"" combo box on the right. When in <b>Multiple</b> selection mode you
	can hold &lt;Shift&gt; or &lt;Ctrl&gt; to select multiple items from the list box.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnListBoxItemSelected(ByVal args As Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))
            Dim item As Nevron.Nov.UI.NListBoxItem = args.Item
            Dim index As Integer = item.GetAggregationInfo().Index
            Me.m_EventsLog.LogEvent("Selected Item: " & index.ToString())
        End Sub

        Private Sub OnListBoxItemDeselected(ByVal args As Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))
            Dim item As Nevron.Nov.UI.NListBoxItem = args.Item
            Dim index As Integer = item.GetAggregationInfo().Index
            Me.m_EventsLog.LogEvent("Deselected Item: " & index.ToString())
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ListBox As Nevron.Nov.UI.NListBox
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NListBoxSelectionExample.
		''' </summary>
		Public Shared ReadOnly NListBoxSelectionExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
