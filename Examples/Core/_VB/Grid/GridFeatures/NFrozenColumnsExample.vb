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
    Public Class NFrozenColumnsExample
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
            Nevron.Nov.Examples.Grid.NFrozenColumnsExample.NFrozenColumnsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NFrozenColumnsExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_GridView = New Nevron.Nov.Grid.NTableGridView()
            Me.m_GridView.Grid.DataSource = NDummyDataSource.CreatePersonsOrdersDataSource()

            ' create a total column that is pinned to the right
            ' add an event calculated column of type Double
            Dim totalColumn As Nevron.Nov.Grid.NCustomCalculatedColumn(Of System.[Double]) = New Nevron.Nov.Grid.NCustomCalculatedColumn(Of System.[Double])()
            totalColumn.Title = "Total"
            totalColumn.FreezeMode = Nevron.Nov.Grid.ENColumnFreezeMode.Right
            totalColumn.GetRowValueDelegate += Function(ByVal arg As Nevron.Nov.Grid.NCustomCalculatedColumnGetRowValueArgs(Of Double))
                ' calculate a RowValue for the RowIndex
                Dim price As Double = System.Convert.ToDouble(arg.DataSource.GetValue(arg.RowIndex, "Price"))
                                                   Dim quantity As Double = System.Convert.ToDouble(arg.DataSource.GetValue(arg.RowIndex, "Quantity"))
                                                   Return CDbl((price * quantity))
                                               End Function

            totalColumn.Format.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.SeaShell)
            Me.m_GridView.Grid.Columns.Add(totalColumn)

            ' freeze the pruduct name to the left
            Dim productNameColumn As Nevron.Nov.Grid.NColumn = Me.m_GridView.Grid.Columns.GetColumnByFieldName("Product Name")
            productNameColumn.Format.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.SeaShell)
            productNameColumn.FreezeMode = Nevron.Nov.Grid.ENColumnFreezeMode.Left
            Return Me.m_GridView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim pstack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Return pstack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates <b>Frozen Columns</b>.
</p>
<p>
    Columns can be frozen to the left or right side of the grid window area.
    In this example the <b>Total</b> column is frozen to the right side, while the <b>Product Name</b> column is frozen to the left side.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_GridView As Nevron.Nov.Grid.NTableGridView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NFrozenColumnsExample.
        ''' </summary>
        Public Shared ReadOnly NFrozenColumnsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
