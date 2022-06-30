Imports System
Imports Nevron.Nov.Grid
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Data
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NColumnProtectionExample
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
            Nevron.Nov.Examples.Grid.NColumnProtectionExample.NColumnProtectionExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NColumnProtectionExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_GridView = New Nevron.Nov.Grid.NTableGridView()
            Me.m_GridView.Grid.DataSource = NDummyDataSource.CreateProductsDataSource()
            Me.m_GridView.Grid.AllowSortColumns = True
            Return Me.m_GridView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            ' create the columns combo box
            If True Then
                stack.Add(New Nevron.Nov.UI.NLabel("Select Column:"))
                Me.m_ColumnsComboBox = New Nevron.Nov.UI.NComboBox()
                stack.Add(Me.m_ColumnsComboBox)

                For i As Integer = 0 To Me.m_GridView.Grid.Columns.Count - 1
                    Dim column As Nevron.Nov.Grid.NColumn = Me.m_GridView.Grid.Columns(i)
                    Dim item As Nevron.Nov.UI.NComboBoxItem = New Nevron.Nov.UI.NComboBoxItem(column.Title)
                    item.Tag = column
                    Me.m_ColumnsComboBox.Items.Add(item)
                Next

                AddHandler Me.m_ColumnsComboBox.SelectedIndexChanged, AddressOf Me.OnColumnsComboBoxSelectedIndexChanged

                ' create the columns 
                Me.m_ColumnPropertiesHolder = New Nevron.Nov.UI.NContentHolder()
                stack.Add(New Nevron.Nov.UI.NGroupBox("Selected Column Properties", Me.m_ColumnPropertiesHolder))
            End If

            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates the column properties that affect the operations, which the user can perform with the column (e.g column protection).
<p>"
        End Function

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnColumnsComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim item As Nevron.Nov.UI.NComboBoxItem = Me.m_ColumnsComboBox.SelectedItem

            If item Is Nothing Then
                Me.m_ColumnPropertiesHolder.Content = Nothing
                Return
            End If

            Dim column As Nevron.Nov.Grid.NColumn = CType(item.Tag, Nevron.Nov.Grid.NColumn)
            Dim columnPropertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_ColumnPropertiesHolder.Content = New Nevron.Nov.UI.NUniSizeBoxGroup(columnPropertiesStack)
            Dim designer As Nevron.Nov.Editors.NDesigner = Nevron.Nov.Editors.NDesigner.GetDesigner(column)
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = designer.CreatePropertyEditors(column, Nevron.Nov.Grid.NColumn.AllowFilterProperty, Nevron.Nov.Grid.NColumn.AllowSortProperty, Nevron.Nov.Grid.NColumn.AllowGroupProperty, Nevron.Nov.Grid.NColumn.AllowFormatProperty, Nevron.Nov.Grid.NColumn.AllowEditProperty, Nevron.Nov.Grid.NColumn.AllowReorderProperty, Nevron.Nov.Grid.NColumn.AllowResizeProperty)

            For i As Integer = 0 To editors.Count - 1
                columnPropertiesStack.Add(editors(i))
            Next
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_GridView As Nevron.Nov.Grid.NTableGridView
        Private m_ColumnsComboBox As Nevron.Nov.UI.NComboBox
        Private m_ColumnPropertiesHolder As Nevron.Nov.UI.NContentHolder

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NColumnProtectionExample.
        ''' </summary>
        Public Shared ReadOnly NColumnProtectionExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
