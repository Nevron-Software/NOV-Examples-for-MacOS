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
    Public Class NRowHeadersExample
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
            Nevron.Nov.Examples.Grid.NRowHeadersExample.NRowHeadersExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NRowHeadersExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_TableView = New Nevron.Nov.Grid.NTableGridView()
            Me.m_TableView.Grid.DataSource = NDummyDataSource.CreateCompanySalesDataSource()

            ' show the row headers
            Me.m_TableView.Grid.RowHeaders.Visible = True
            Return Me.m_TableView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            
            ' create the row headers properties
            If True Then
                Dim rowHeadersStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Dim designer As Nevron.Nov.Editors.NDesigner = Nevron.Nov.Editors.NDesigner.GetDesigner(Nevron.Nov.Grid.NRowHeaderCollection.NRowHeaderCollectionSchema)
                Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = designer.CreatePropertyEditors(Me.m_TableView.Grid.RowHeaders, Nevron.Nov.Grid.NRowHeaderCollection.VisibleProperty, Nevron.Nov.Grid.NRowHeaderCollection.ShowRowNumbersProperty, Nevron.Nov.Grid.NRowHeaderCollection.ShowRowSymbolProperty)

                For i As Integer = 0 To editors.Count - 1
                    rowHeadersStack.Add(editors(i))
                Next

                Dim rowHeadersGroup As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Row Headers Properties", rowHeadersStack)
                stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(rowHeadersGroup))
            End If

            ' create the grid properties
            If True Then
                Dim gridStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_TableView.Grid), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_TableView.Grid, Nevron.Nov.Grid.NGrid.FrozenRowsProperty, Nevron.Nov.Grid.NGrid.IntegralVScrollProperty)

                For i As Integer = 0 To editors.Count - 1
                    gridStack.Add(editors(i))
                Next

                Dim gridGroup As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Grid Properties", gridStack)
                stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(gridGroup))
            End If

            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates the row headers.
</p>
<p>
    Row headers are small button-like elements that you can use to select rows. 
    Besides for selection, row headers indicate the row state (e.g. current or editing status) and can be configured to show the row ordinal in the data source.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_TableView As Nevron.Nov.Grid.NTableGridView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NRowHeadersExample.
        ''' </summary>
        Public Shared ReadOnly NRowHeadersExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
