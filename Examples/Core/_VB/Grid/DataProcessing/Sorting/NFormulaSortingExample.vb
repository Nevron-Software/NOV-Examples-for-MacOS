Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Grid
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NFormulaSortingExample
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
            Nevron.Nov.Examples.Grid.NFormulaSortingExample.NFormulaSortingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NFormulaSortingExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a view and get its grid
            Dim view As Nevron.Nov.Grid.NTableGridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = view.Grid

            ' bind the grid to the data source
            grid.DataSource = NDummyDataSource.CreatePersonsOrdersDataSource()

            ' add a custom calculated column of type Double that displays the Total (e.g. Price * Quantity)
            Dim totalColumn As Nevron.Nov.Grid.NCustomCalculatedColumn(Of Double) = New Nevron.Nov.Grid.NCustomCalculatedColumn(Of Double)()
            totalColumn.Title = "Total"
            totalColumn.GetRowValueDelegate += Function(ByVal arg As Nevron.Nov.Grid.NCustomCalculatedColumnGetRowValueArgs(Of Double))
                ' calculate a RowValue for the RowIndex
                Dim price As Double = System.Convert.ToDouble(arg.DataSource.GetValue(arg.RowIndex, "Price"))
                                                   Dim quantity As Double = System.Convert.ToDouble(arg.DataSource.GetValue(arg.RowIndex, "Quantity"))
                                                   Return CDbl((price * quantity))
                                               End Function

            totalColumn.Format.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.SeaShell)
            grid.Columns.Add(totalColumn)

            ' create the sales sorting rule.
            ' also subcribe for the create sorting rule event to recreate the rule when users 
            grid.SortingRules.Add(Me.CreateTotalSortingRule(grid))
            totalColumn.CreateSortingRuleDelegate = Function(ByVal theColumn As Nevron.Nov.Grid.NColumn) Me.CreateTotalSortingRule(grid)

            ' alter some view preferences
            grid.AllowSortColumns = True
            grid.AlternatingRows = True
            grid.RowHeaders.Visible = True
            Return view
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates expression sorting rules in combination with calculated columns.
</p>
<p>
    In this example we have created an <b>NCustomCalculatedColumn</b> that displays the <b>Price</b> * <b>Quantity</b> calculation (e.g. <b>Total</b>).
</p>
<p>
    We have also created a sorting rule that sorts by the <b>Total</b> calculated column in a different way - via a Formula Expression.
</p>
"
        End Function

        #EndRegion

        #Region"Implementation"

        Private Function CreateTotalSortingRule(ByVal grid As Nevron.Nov.Grid.NTableGrid) As Nevron.Nov.Grid.NSortingRule
            Dim salesColumn As Nevron.Nov.Grid.NColumn = grid.Columns.GetColumnByFieldName("Sales")
            Dim priceColumn As Nevron.Nov.Grid.NColumn = grid.Columns.GetColumnByFieldName("Price")

            ' create a sorting rule that sorts by the Total value
            Dim quantityFieldName As String = grid.CreateFormulaFieldName("Quantity")
            Dim priceFieldName As String = grid.CreateFormulaFieldName("Price")
            Dim sortingRule As Nevron.Nov.Grid.NSortingRule = Nevron.Nov.Grid.NSortingRule.FromFormula(Nothing, priceFieldName & "*" & quantityFieldName, Nevron.Nov.Grid.ENSortingDirection.Ascending)
            sortingRule.Column = salesColumn
            Return sortingRule
        End Function

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NFormulaSortingExample.
        ''' </summary>
        Public Shared ReadOnly NFormulaSortingExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
