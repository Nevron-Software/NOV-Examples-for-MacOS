Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors

Namespace Nevron.Nov.Examples.UI
    Public Class NListBoxFirstLookExample
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
            Nevron.Nov.Examples.UI.NListBoxFirstLookExample.NListBoxFirstLookExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NListBoxFirstLookExample), NExampleBase.NExampleBaseSchema)
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
			stack.Add(Me.CreatePropertiesGroupBox())

			' Create the events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a simple list box with text only items. You can use the controls
	on the right to modify various properties of the list box.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreatePropertiesGroupBox() As Nevron.Nov.UI.NGroupBox
            Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ListBox), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_ListBox, Nevron.Nov.UI.NListBox.EnabledProperty, Nevron.Nov.UI.NListBox.HorizontalPlacementProperty, Nevron.Nov.UI.NListBox.VerticalPlacementProperty, Nevron.Nov.UI.NListBox.HScrollModeProperty, Nevron.Nov.UI.NListBox.VScrollModeProperty, Nevron.Nov.UI.NListBox.NoScrollHAlignProperty, Nevron.Nov.UI.NListBox.NoScrollVAlignProperty, Nevron.Nov.UI.NListBox.IntegralVScrollProperty)
            Dim i As Integer = 0, count As Integer = editors.Count

            While i < count
                propertiesStack.Add(editors(i))
                i += 1
            End While

            Dim propertiesGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack))
            Return propertiesGroupBox
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
		''' Schema associated with NListBoxFirstLookExample.
		''' </summary>
		Public Shared ReadOnly NListBoxFirstLookExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
