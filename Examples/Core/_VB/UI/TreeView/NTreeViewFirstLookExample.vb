Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NTreeViewFirstLookExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NTreeViewFirstLookExample.NTreeViewFirstLookExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTreeViewFirstLookExample), NExampleBase.NExampleBaseSchema)
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

			' Hook to tree view events
			AddHandler Me.m_TreeView.SelectedPathChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnTreeViewSelectedPathChanged)
            Return Me.m_TreeView
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
	This example demonstrates how to create a tree view with text only items. You can use the controls
	on the right to modify various properties of the tree box.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreatePropertiesGroupBox() As Nevron.Nov.UI.NGroupBox
            Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_TreeView), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_TreeView, Nevron.Nov.UI.NTreeView.EnabledProperty, Nevron.Nov.UI.NTreeView.HorizontalPlacementProperty, Nevron.Nov.UI.NTreeView.VerticalPlacementProperty, Nevron.Nov.UI.NTreeView.NoScrollHAlignProperty, Nevron.Nov.UI.NTreeView.NoScrollVAlignProperty, Nevron.Nov.UI.NTreeView.HScrollModeProperty, Nevron.Nov.UI.NTreeView.VScrollModeProperty, Nevron.Nov.UI.NTreeView.IntegralVScrollProperty)
            Dim i As Integer = 0, count As Integer = editors.Count

            While i < count
                propertiesStack.Add(editors(i))
                i += 1
            End While

            Dim propertiesGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack))
            Return propertiesGroupBox
        End Function

        Private Function CreateTreeViewItem(ByVal text As String) As Nevron.Nov.UI.NTreeViewItem
            Dim item As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem(text)
            item.Tag = text
            AddHandler item.ExpandedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnTreeViewItemExpandedChanged)
            Return item
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnTreeViewSelectedPathChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_EventsLog.LogEvent("Selected: " & Me.m_TreeView.SelectedItem.Tag.ToString())
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

		Private m_TreeView As Nevron.Nov.UI.NTreeView
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NTreeViewFirstLookExample.
		''' </summary>
		Public Shared ReadOnly NTreeViewFirstLookExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
