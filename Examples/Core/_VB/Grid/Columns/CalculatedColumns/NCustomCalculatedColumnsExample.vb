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
    Public Class NCustomCalculatedColumnsExample
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
            Nevron.Nov.Examples.Grid.NCustomCalculatedColumnsExample.NCustomCalculatedColumnsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NCustomCalculatedColumnsExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_GridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = Me.m_GridView.Grid

            ' bind the grid to the data source
            grid.DataSource = NDummyDataSource.CreatePersonsOrdersDataSource()

            ' add an event calculated column of type Double
            Dim totalColumn As Nevron.Nov.Grid.NCustomCalculatedColumn(Of System.[Double]) = New Nevron.Nov.Grid.NCustomCalculatedColumn(Of System.[Double])()
            totalColumn.Title = "Total"
            totalColumn.GetRowValueDelegate += Function(ByVal arg As Nevron.Nov.Grid.NCustomCalculatedColumnGetRowValueArgs(Of Double))
                ' calculate a RowValue for the RowIndex
                Dim price As Double = System.Convert.ToDouble(arg.DataSource.GetValue(arg.RowIndex, "Price"))
                                                   Dim quantity As Double = System.Convert.ToDouble(arg.DataSource.GetValue(arg.RowIndex, "Quantity"))
                                                   Return CDbl((price * quantity))
                                               End Function

            totalColumn.Format.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.SeaShell)
            grid.Columns.Add(totalColumn)
            Return Me.m_GridView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates custom calculated columns.
</p>
<p>
    Custom calculated columns are represented by the <b>NCustomCalculatedColumn</b> class. 
    It exposes a <b>GetRowValueDelegate</b> delegate, which is called whenever the column must provide a value for a specific row.
    Thus it is up to the user to provide a row value for a specific row.
</p>
<p>
    In the example the <b>Total</b> column is a custom calculated column that is calculated as {<b>Price</b>*<b>Quantity</b>}.
</p>"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_GridView As Nevron.Nov.Grid.NTableGridView
        

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NCustomCalculatedColumnsExample.
        ''' </summary>
        Public Shared ReadOnly NCustomCalculatedColumnsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
