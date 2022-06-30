Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NListBoxItemsManipulationExample
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
            Nevron.Nov.Examples.UI.NListBoxItemsManipulationExample.NListBoxItemsManipulationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NListBoxItemsManipulationExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a list box
			Me.m_ListBox = New Nevron.Nov.UI.NListBox()
            Me.m_ListBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_ListBox.PreferredSize = New Nevron.Nov.Graphics.NSize(200, 400)

			' Add some items
			For i As Integer = 0 To 10 - 1
                Dim item As Nevron.Nov.UI.NListBoxItem = New Nevron.Nov.UI.NListBoxItem("Item " & i.ToString())
                item.Tag = i
                Me.m_ListBox.Items.Add(item)
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

			' Create the commands group box
			stack.Add(Me.CreateCommandsGroupBox())

			' Create the events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a simple list box with text only list box items and how to add/remove items.
	You can use the buttons on the right to add/remove items from the list box's <b>Items</b> collection.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateCommandsGroupBox() As Nevron.Nov.UI.NGroupBox
            Dim commandsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

			' Create the command buttons
			Me.m_AddButton = New Nevron.Nov.UI.NButton("Add Item")
            AddHandler Me.m_AddButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAddButtonClick)
            commandsStack.Add(Me.m_AddButton)
            Me.m_RemoveSelectedButton = New Nevron.Nov.UI.NButton("Remove Selected")
            Me.m_RemoveSelectedButton.Enabled = False
            AddHandler Me.m_RemoveSelectedButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRemoveSelectedButtonClick)
            commandsStack.Add(Me.m_RemoveSelectedButton)
            Me.m_RemoveAllButton = New Nevron.Nov.UI.NButton("Remove All")
            AddHandler Me.m_RemoveAllButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRemoveAllButtonClick)
            commandsStack.Add(Me.m_RemoveAllButton)

			' Create the commands group box
			Dim commmandsGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Commands")
            commmandsGroupBox.Content = commandsStack
            Return commmandsGroupBox
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnListBoxItemSelected(ByVal args As Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))
            Me.m_RemoveSelectedButton.Enabled = True
            Dim item As Nevron.Nov.UI.NListBoxItem = args.Item
            Me.m_EventsLog.LogEvent("Selected Item: " & item.Tag.ToString())
        End Sub

        Private Sub OnListBoxItemDeselected(ByVal args As Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))
            Dim item As Nevron.Nov.UI.NListBoxItem = args.Item
            Me.m_EventsLog.LogEvent("Deselected Item: " & item.Tag.ToString())
        End Sub

        Private Sub OnAddButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim index As Integer
            Dim value As String = "0"

            If Me.m_ListBox.Items.Count > 0 Then
                Dim lastItem As Nevron.Nov.UI.NListBoxItem = Me.m_ListBox.Items(Me.m_ListBox.Items.Count - 1)
                Dim label As Nevron.Nov.UI.NLabel = CType(lastItem.GetDescendants(New Nevron.Nov.Dom.NInstanceOfSchemaFilter(Nevron.Nov.UI.NLabel.NLabelSchema))(0), Nevron.Nov.UI.NLabel)
                value = label.Text
                value = value.Remove(0, value.LastIndexOf(" "c) + 1)
            End If

			' Add an item with the calculated index
			index = System.Int32.Parse(value) + 1
            Dim item As Nevron.Nov.UI.NListBoxItem = New Nevron.Nov.UI.NListBoxItem("Item " & index)
            item.Tag = index
            Me.m_ListBox.Items.Add(item)

            If Me.m_ListBox.Items.Count = 1 Then
                Me.m_RemoveAllButton.Enabled = True
            End If
        End Sub

        Private Sub OnRemoveSelectedButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim selected As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.UI.NListBoxItem) = Me.m_ListBox.Selection.SelectedItems

            For i As Integer = 0 To selected.Count - 1
                Me.m_ListBox.Items.Remove(selected(i))
            Next

            If Me.m_ListBox.Items.Count = 0 Then
                Me.m_RemoveAllButton.Enabled = False
                Me.m_RemoveSelectedButton.Enabled = False
            End If
        End Sub

        Private Sub OnRemoveAllButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_ListBox.Items.Clear()
            Me.m_RemoveAllButton.Enabled = False
            Me.m_RemoveSelectedButton.Enabled = False
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ListBox As Nevron.Nov.UI.NListBox
        Private m_EventsLog As NExampleEventsLog
        Private m_AddButton As Nevron.Nov.UI.NButton
        Private m_RemoveSelectedButton As Nevron.Nov.UI.NButton
        Private m_RemoveAllButton As Nevron.Nov.UI.NButton

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NListBoxItemsManipulationExample.
		''' </summary>
		Public Shared ReadOnly NListBoxItemsManipulationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
