Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NTreeViewItemsManipulationExample
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
            Nevron.Nov.Examples.UI.NTreeViewItemsManipulationExample.NTreeViewItemsManipulationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTreeViewItemsManipulationExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the tree view
			Me.m_TreeView = New Nevron.Nov.UI.NTreeView()
            Me.m_TreeView.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
			' Check whether the application is in touch mode and set the width of the tree view.
			Dim touchMode As Boolean = Nevron.Nov.NApplication.Desktop.TouchMode
            Me.m_TreeView.PreferredWidth = If(touchMode, 300, 200)

			' Add some items
			For i As Integer = 0 To 32 - 1
                Dim checkBox As Boolean = (i >= 8 AndAlso i < 16) OrElse i >= 24
                Dim image As Boolean = i >= 16
                Dim l1Item As Nevron.Nov.UI.NTreeViewItem = Me.CreateTreeViewItem(System.[String].Format("Item {0}", i))
                Me.m_TreeView.Items.Add(l1Item)

                For j As Integer = 0 To 8 - 1
                    Dim l2Item As Nevron.Nov.UI.NTreeViewItem = Me.CreateTreeViewItem(System.[String].Format("Item {0}.{1}", i, j))
                    l1Item.Items.Add(l2Item)

                    For k As Integer = 0 To 2 - 1
                        Dim l3Item As Nevron.Nov.UI.NTreeViewItem = Me.CreateTreeViewItem(System.[String].Format("Item {0}.{1}.{2}", i, j, k))
                        l2Item.Items.Add(l3Item)
                    Next
                Next
            Next

			' Hook to the tree view events
			AddHandler Me.m_TreeView.SelectedPathChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnTreeViewSelectedPathChanged)
            Return Me.m_TreeView
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
	This example demonstrates how to create a simple tree view with text only items and how to add/remove items.
	You can use the buttons on the right to add/remove items from the tree view.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateTreeViewItem(ByVal text As String) As Nevron.Nov.UI.NTreeViewItem
            Dim item As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem(text)
            item.Tag = text
            AddHandler item.ExpandedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnTreeViewItemExpandedChanged)
            Return item
        End Function

        Private Function CreateCommandsGroupBox() As Nevron.Nov.UI.NGroupBox
            Dim commandsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

			' Create the command buttons
			Me.m_AddButton = New Nevron.Nov.UI.NButton("Add Child Item")
            AddHandler Me.m_AddButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAddButtonClicked)
            commandsStack.Add(Me.m_AddButton)
            Me.m_InsertBeforeButton = New Nevron.Nov.UI.NButton("Insert Item Before")
            AddHandler Me.m_InsertBeforeButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAddButtonClicked)
            commandsStack.Add(Me.m_InsertBeforeButton)
            Me.m_InsertAfterButton = New Nevron.Nov.UI.NButton("Insert Item After")
            AddHandler Me.m_InsertAfterButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAddButtonClicked)
            commandsStack.Add(Me.m_InsertAfterButton)
            Dim expandAllButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Expand All")
            expandAllButton.Margins = New Nevron.Nov.Graphics.NMargins(0, 15, 0, 0)
            AddHandler expandAllButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnExpandAllClicked)
            commandsStack.Add(expandAllButton)
            Dim collapseAllButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Collapse All")
            collapseAllButton.Margins = New Nevron.Nov.Graphics.NMargins(0, 0, 0, 15)
            AddHandler collapseAllButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnCollapseAllClicked)
            commandsStack.Add(collapseAllButton)
            Me.m_RemoveSelectedButton = New Nevron.Nov.UI.NButton("Remove Selected")
            AddHandler Me.m_RemoveSelectedButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRemoveSelectedButtonClicked)
            commandsStack.Add(Me.m_RemoveSelectedButton)
            Me.m_RemoveChildrenButton = New Nevron.Nov.UI.NButton("Remove Children")
            AddHandler Me.m_RemoveChildrenButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRemoveChildrenButtonClicked)
            commandsStack.Add(Me.m_RemoveChildrenButton)
            Me.m_RemoveAllButton = New Nevron.Nov.UI.NButton("Remove All")
            AddHandler Me.m_RemoveAllButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRemoveAllButtonClicked)
            commandsStack.Add(Me.m_RemoveAllButton)

			' Create the commands group box
			Dim commmandsGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Commands")
            commmandsGroupBox.Content = commandsStack
            Me.UpdateButtonsState()
            Return commmandsGroupBox
        End Function

        Private Sub UpdateButtonsState()
            Dim selectedItem As Nevron.Nov.UI.NTreeViewItem = Me.m_TreeView.SelectedItem

            If selectedItem Is Nothing Then
                Me.m_InsertBeforeButton.Enabled = False
                Me.m_InsertAfterButton.Enabled = False
                Me.m_RemoveSelectedButton.Enabled = False
                Me.m_RemoveChildrenButton.Enabled = False
            Else
                Me.m_InsertBeforeButton.Enabled = True
                Me.m_InsertAfterButton.Enabled = True
                Me.m_RemoveSelectedButton.Enabled = True
                Me.m_RemoveChildrenButton.Enabled = selectedItem.Items.Count > 0
            End If
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnAddButtonClicked(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim selItem As Nevron.Nov.UI.NTreeViewItem = Me.m_TreeView.SelectedItem
            Dim text As String = If(selItem Is Nothing, "Item " & Me.m_TreeView.Items.Count.ToString(), selItem.Tag.ToString())

            If args.TargetNode Is Me.m_AddButton Then
                If selItem Is Nothing Then
					' Add the item as a last item in the tree view
					Me.m_TreeView.Items.Add(Me.CreateTreeViewItem(text))
                Else
					' Add the item as a last item in the selected item
					text += "." & selItem.Items.Count.ToString()
                    selItem.Items.Add(Me.CreateTreeViewItem(text))
                End If
            ElseIf args.TargetNode Is Me.m_InsertBeforeButton Then
				' Insert the item before the selected one
				Dim items As Nevron.Nov.UI.NTreeViewItemCollection = CType(selItem.ParentNode, Nevron.Nov.UI.NTreeViewItemCollection)
                text += ".Before"
                items.Insert(selItem.IndexInParent, Me.CreateTreeViewItem(text))
            ElseIf args.TargetNode Is Me.m_InsertAfterButton Then
				' Insert the item after the selected one
				Dim items As Nevron.Nov.UI.NTreeViewItemCollection = CType(selItem.ParentNode, Nevron.Nov.UI.NTreeViewItemCollection)
                text += ".After"
                items.Insert(selItem.IndexInParent + 1, Me.CreateTreeViewItem(text))
            End If

            If Me.m_TreeView.Items.Count = 1 Then
                Me.m_RemoveAllButton.Enabled = True
            End If
        End Sub

        Private Sub OnRemoveSelectedButtonClicked(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim selectedItem As Nevron.Nov.UI.NTreeViewItem = Me.m_TreeView.SelectedItem
            If selectedItem Is Nothing Then Return
            selectedItem.ParentNode.RemoveChild(selectedItem)
        End Sub

        Private Sub OnRemoveChildrenButtonClicked(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Dim selectedItem As Nevron.Nov.UI.NTreeViewItem = Me.m_TreeView.SelectedItem
            If selectedItem Is Nothing Then Return
            selectedItem.Items.RemoveAllChildren()
            Me.m_RemoveChildrenButton.Enabled = False
        End Sub

        Private Sub OnRemoveAllButtonClicked(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_TreeView.Items.Clear()
            Me.m_RemoveAllButton.Enabled = False
        End Sub

        Private Sub OnExpandAllClicked(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim treeIterator As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.Dom.NNode) = Me.m_TreeView.GetSubtreeIterator(Nevron.Nov.Dom.ENTreeTraversalOrder.DepthFirstPreOrder, Nevron.Nov.DataStructures.NIsFilter(Of Nevron.Nov.Dom.NNode, Nevron.Nov.UI.NTreeViewItem).Instance)
            Dim itemIterator As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.UI.NTreeViewItem) = New Nevron.Nov.DataStructures.NAsIterator(Of Nevron.Nov.Dom.NNode, Nevron.Nov.UI.NTreeViewItem)(treeIterator)

            While itemIterator.MoveNext()
                itemIterator.Current.Expanded = True
            End While
        End Sub

        Private Sub OnCollapseAllClicked(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim treeIterator As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.Dom.NNode) = Me.m_TreeView.GetSubtreeIterator(Nevron.Nov.Dom.ENTreeTraversalOrder.DepthFirstPreOrder, Nevron.Nov.DataStructures.NIsFilter(Of Nevron.Nov.Dom.NNode, Nevron.Nov.UI.NTreeViewItem).Instance)
            Dim itemIterator As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.UI.NTreeViewItem) = New Nevron.Nov.DataStructures.NAsIterator(Of Nevron.Nov.Dom.NNode, Nevron.Nov.UI.NTreeViewItem)(treeIterator)

            While itemIterator.MoveNext()
                itemIterator.Current.Expanded = False
            End While
        End Sub

        Private Sub OnTreeViewSelectedPathChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.UpdateButtonsState()
            Dim selectedItem As Nevron.Nov.UI.NTreeViewItem = Me.m_TreeView.SelectedItem

            If selectedItem IsNot Nothing Then
                Me.m_EventsLog.LogEvent("Selected: " & selectedItem.Tag.ToString())
            End If
        End Sub

        Private Sub OnTreeViewItemExpandedChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim item As Nevron.Nov.UI.NTreeViewItem = CType(args.TargetNode, Nevron.Nov.UI.NTreeViewItem)

            If item.Expanded Then
                Me.m_EventsLog.LogEvent("Expanded: " & item.Tag.ToString())
            Else
                Me.m_EventsLog.LogEvent("Collapsed: " & item.Tag.ToString())
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_AddButton As Nevron.Nov.UI.NButton
        Private m_InsertBeforeButton As Nevron.Nov.UI.NButton
        Private m_InsertAfterButton As Nevron.Nov.UI.NButton
        Private m_RemoveSelectedButton As Nevron.Nov.UI.NButton
        Private m_RemoveChildrenButton As Nevron.Nov.UI.NButton
        Private m_RemoveAllButton As Nevron.Nov.UI.NButton
        Private m_TreeView As Nevron.Nov.UI.NTreeView
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NTreeViewItemsManipulationExample.
		''' </summary>
		Public Shared ReadOnly NTreeViewItemsManipulationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
