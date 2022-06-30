Imports System.Globalization
Imports Nevron.Nov.Data
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Grid
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NEmbeddedResourcesExample
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
            Nevron.Nov.Examples.Framework.NEmbeddedResourcesExample.NEmbeddedResourcesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NEmbeddedResourcesExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_TreeView = New Nevron.Nov.UI.NTreeView()
            AddHandler Me.m_TreeView.SelectedPathChanged, AddressOf Me.OnTreeViewSelectedPathChanged
            Me.m_ResourcesMap = New Nevron.Nov.DataStructures.NMap(Of Nevron.Nov.UI.NTreeViewItem, Nevron.Nov.NEmbeddedResourceContainer)()
            Me.m_TreeView.Items.Add(Me.CreateRootItem(Nevron.Nov.Presentation.NResources.Instance))
            Me.m_TreeView.Items.Add(Me.CreateRootItem(Nevron.Nov.Diagram.NResources.Instance))
            Me.m_TreeView.Items.Add(Me.CreateRootItem(Nevron.Nov.Text.NResources.Instance))
            Me.m_TreeView.Items.Add(Me.CreateRootItem(Nevron.Nov.Schedule.NResources.Instance))
            Me.m_TreeView.Items.Add(Me.CreateRootItem(Nevron.Nov.Grid.NResources.Instance))

            ' Create a data table
            Me.m_DataTable = New Nevron.Nov.Data.NMemoryDataTable()
            Me.m_DataTable.AddField(New Nevron.Nov.Data.NFieldInfo("Image", GetType(Nevron.Nov.Graphics.NImage)))
            Me.m_DataTable.AddField(New Nevron.Nov.Data.NFieldInfo("Name", GetType(String)))
            Me.m_DataTable.AddField(New Nevron.Nov.Data.NFieldInfo("Size", GetType(String)))
            Me.m_DataTable.AddField(New Nevron.Nov.Data.NFieldInfo("Action", GetType(String)))

            ' Create a grid view
            Me.m_GridView = New Nevron.Nov.Grid.NTableGridView()
            Me.m_GridView.GroupingPanel.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
            Me.m_GridView.[ReadOnly] = True
            Dim tableGrid As Nevron.Nov.Grid.NTableGrid = Me.m_GridView.Grid
            tableGrid.AlternatingRows = False
            tableGrid.RowHeaders.Visible = False
            AddHandler tableGrid.AutoCreateColumn, AddressOf Me.OnGridAutoCreateColumn
            tableGrid.DataSource = New Nevron.Nov.Data.NDataSource(Me.m_DataTable)
            Dim splitter As Nevron.Nov.UI.NSplitter = New Nevron.Nov.UI.NSplitter(Me.m_TreeView, Me.m_GridView, Nevron.Nov.UI.ENSplitterSplitMode.OffsetFromNearSide, 200)
            Return splitter
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	Browser for all resources embedded in the NOV assemblies. Select a category from the tree view
	with resources and then click the <b>Copy Code</b> button in the grid next to the image resource
	you are interested in to copy the code for using it to the clipboard.
</p>
"
        End Function

        #EndRegion

        #Region"Implementation"

        Private Function CreateRootItem(ByVal resourceContainer As Nevron.Nov.NEmbeddedResourceContainer) As Nevron.Nov.UI.NTreeViewItem
            Dim nspace As String = resourceContainer.[GetType]().[Namespace]
            Dim name As String = nspace.Substring("Nevron.Nov.".Length)
            Dim rootItem As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem(name)
            Me.m_ResourcesMap.[Set](rootItem, resourceContainer)
            rootItem.Expanded = True
            Dim names As String() = resourceContainer.GetResourceNames()

            For i As Integer = 0 To names.Length - 1
                Dim tokens As String() = names(CInt((i))).Split("_"c)
                If Not Equals(tokens(0), "RIMG") Then Continue For

                ' Navigate to the path of the current image resource in the tree view
                Dim item As Nevron.Nov.UI.NTreeViewItem = rootItem

                For j As Integer = 1 To tokens.Length - 2 - 1
                    item = Me.GetOrCreateItem(item.Items, tokens(j))
                Next

                ' Add the image resource to the path
                Dim images As Nevron.Nov.DataStructures.NList(Of String) = Me.GetImageNames(item)

                If images Is Nothing Then
                    images = New Nevron.Nov.DataStructures.NList(Of String)()
                    item.Tag = images
                End If

                images.Add(names(i))
            Next

            Return rootItem
        End Function

        Private Function GetOrCreateItem(ByVal items As Nevron.Nov.UI.NTreeViewItemCollection, ByVal name As String) As Nevron.Nov.UI.NTreeViewItem
            Dim i As Integer = 0, count As Integer = items.Count

            While i < count
                Dim label As Nevron.Nov.UI.NLabel = CType(items(CInt((i))).Header.GetFirstDescendant(Nevron.Nov.UI.NLabel.NLabelSchema), Nevron.Nov.UI.NLabel)
                If Equals(label.Text, name) Then Return items(i)
                i += 1
            End While

            Dim item As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem(Nevron.Nov.UI.NPairBox.Create(Nevron.Nov.Grid.NResources.Image__16x16_Folders_png, name))
            items.Add(item)
            Return item
        End Function

        Private Function GetResourceContainer(ByVal item As Nevron.Nov.UI.NTreeViewItem) As Nevron.Nov.NEmbeddedResourceContainer
            ' Find the root item of the given item
            While item.ParentItem IsNot Nothing
                item = item.ParentItem
            End While

            ' Return the resource container represented by this root item
            Return Me.m_ResourcesMap(item)
        End Function

        Private Function GetImageNames(ByVal item As Nevron.Nov.UI.NTreeViewItem) As Nevron.Nov.DataStructures.NList(Of String)
            Return CType(item.Tag, Nevron.Nov.DataStructures.NList(Of String))
        End Function

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnTreeViewSelectedPathChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_DataTable.RemoveAllRows()
            Me.m_GridView.Grid.Update()
            Dim treeView As Nevron.Nov.UI.NTreeView = CType(arg.CurrentTargetNode, Nevron.Nov.UI.NTreeView)
            Dim selectedItem As Nevron.Nov.UI.NTreeViewItem = treeView.SelectedItem
            If selectedItem Is Nothing Then Return

            ' Get the resource container and the images for the selected item
            Dim resourceContainer As Nevron.Nov.NEmbeddedResourceContainer = Me.GetResourceContainer(selectedItem)
            Dim images As Nevron.Nov.DataStructures.NList(Of String) = Me.GetImageNames(selectedItem)

            ' Populate the stack with the images in the selected resources folder
            Dim containerType As String = resourceContainer.[GetType]().FullName

            For i As Integer = 0 To images.Count - 1
                Dim resourceName As String = images(i)
                Dim imageName As String = resourceName.Replace("RIMG", "Image")
                Dim image As Nevron.Nov.Graphics.NImage = Nevron.Nov.Graphics.NImage.FromResource(resourceContainer.GetResource(resourceName))
                Dim imageSize As String = image.Width.ToString(System.Globalization.CultureInfo.InvariantCulture) & " x " & image.Height.ToString(System.Globalization.CultureInfo.InvariantCulture)
                Dim code As String = containerType & "." & imageName
                Me.m_DataTable.AddRow(image, imageName, imageSize, code)
            Next
        End Sub

        Private Sub OnGridAutoCreateColumn(ByVal arg As Nevron.Nov.Grid.NAutoCreateColumnEventArgs)
            Dim dataColumn As Nevron.Nov.Grid.NDataColumn = arg.DataColumn

            If Equals(dataColumn.FieldName, "Action") Then
                dataColumn.Format = New Nevron.Nov.Examples.Framework.NEmbeddedResourcesExample.NButtonColumnFormat()
            End If
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_TreeView As Nevron.Nov.UI.NTreeView
        Private m_GridView As Nevron.Nov.Grid.NTableGridView
        Private m_DataTable As Nevron.Nov.Data.NMemoryDataTable
        Private m_ResourcesMap As Nevron.Nov.DataStructures.NMap(Of Nevron.Nov.UI.NTreeViewItem, Nevron.Nov.NEmbeddedResourceContainer)

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NEmbeddedResourcesExample.
        ''' </summary>
        Public Shared ReadOnly NEmbeddedResourcesExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Nested Types"

        Private Class NButtonColumnFormat
            Inherits Nevron.Nov.Grid.NColumnFormat
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
                Nevron.Nov.Examples.Framework.NEmbeddedResourcesExample.NButtonColumnFormat.NButtonColumnFormatSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NEmbeddedResourcesExample.NButtonColumnFormat), Nevron.Nov.Grid.NColumnFormat.NColumnFormatSchema)
            End Sub

            #EndRegion

            #Region"Overrides"

            Public Overrides Sub FormatDefaultDataCell(ByVal dataCell As Nevron.Nov.Grid.NDataCell)
            End Sub

            Protected Overrides Function CreateValueDataCellView(ByVal dataCell As Nevron.Nov.Grid.NDataCell, ByVal rowValue As Object) As Nevron.Nov.UI.NWidget
                Dim button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Copy Code")
                button.Tag = rowValue
                AddHandler button.Click, AddressOf Me.OnButtonClick
                Return button
            End Function

            Protected Overrides Function GetAutomaticHorizontalAlignment(ByVal dataCell As Nevron.Nov.Grid.NDataCell, ByVal rowValue As Object) As Nevron.Nov.Layout.ENHorizontalPlacement
                Return Nevron.Nov.Layout.ENHorizontalPlacement.Center
            End Function

            #EndRegion

            #Region"Event Handlers"

            Private Sub OnButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
                Call Nevron.Nov.UI.NClipboard.SetText(CStr(arg.CurrentTargetNode.Tag))
            End Sub

            #EndRegion

            #Region"Schema"

            ''' <summary>
            ''' Schema associated with NButtonColumnFormat.
            ''' </summary>
            Public Shared ReadOnly NButtonColumnFormatSchema As Nevron.Nov.Dom.NSchema

            #EndRegion
        End Class

        #EndRegion
    End Class
End Namespace
