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
    Public Class NFrozenRowsExample
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
            Nevron.Nov.Examples.Grid.NFrozenRowsExample.NFrozenRowsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NFrozenRowsExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_GridView = New Nevron.Nov.Grid.NTableGridView()
            Me.m_GridView.Grid.DataSource = NDummyDataSource.CreatePersonsOrdersDataSource()
            Me.m_GridView.Grid.FrozenRows = 3
            Return Me.m_GridView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim pstack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            pstack.VerticalSpacing = 2
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_GridView.Grid), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_GridView.Grid, Nevron.Nov.Grid.NGrid.FrozenRowsProperty)

            For i As Integer = 0 To editors.Count - 1
                pstack.Add(editors(i))
            Next

            Return pstack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates <b>Frozen Rows</b>.
</p>
<p>
    Frozen rows are controlled by the <b>FrozenRows</b> grid property. 
    It specifies the count of rows from the top of the grid, that are non-scrollable. 
    Frozen rows are thus appearing pinned to the column headers.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_GridView As Nevron.Nov.Grid.NTableGridView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NFrozenRowsExample.
        ''' </summary>
        Public Shared ReadOnly NFrozenRowsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
