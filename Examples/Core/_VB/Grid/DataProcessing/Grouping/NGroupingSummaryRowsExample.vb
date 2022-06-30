Imports System
Imports Nevron.Nov.Data
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Grid
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NGroupingSummaryRowsExample
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
            Nevron.Nov.Examples.Grid.NGroupingSummaryRowsExample.NGroupingSummaryRowsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NGroupingSummaryRowsExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a view and get its grid
            Dim view As Nevron.Nov.Grid.NTableGridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = view.Grid

            ' customize the grid
            grid.AllowEdit = False

            ' bind to data source, but exclude the "PersonId" field from binding 
            AddHandler grid.AutoCreateColumn, Sub(ByVal arg As Nevron.Nov.Grid.NAutoCreateColumnEventArgs)
                                                  If Equals(arg.FieldInfo.Name, "PersonId") Then
                                                      arg.DataColumn = Nothing
                                                  End If
                                              End Sub

            grid.DataSource = NDummyDataSource.CreatePersonsOrdersDataSource()

            ' create a calculated Total column
            Dim totalColumn As Nevron.Nov.Grid.NCustomCalculatedColumn(Of Double) = New Nevron.Nov.Grid.NCustomCalculatedColumn(Of Double)()
            totalColumn.Title = "Total"
            totalColumn.GetRowValueDelegate = Function(ByVal args As Nevron.Nov.Grid.NCustomCalculatedColumnGetRowValueArgs(Of Double))
                                                  Dim price As Double = System.Convert.ToDouble(args.DataSource.GetValue(args.RowIndex, "Price"))
                                                  Dim quantity As Integer = System.Convert.ToInt32(args.DataSource.GetValue(args.RowIndex, "Quantity"))
                                                  Return CDbl((price * quantity))
                                              End Function

            grid.Columns.Add(totalColumn)

            ' create a grouping rule that groups by the Product Name column
            Dim groupingRule As Nevron.Nov.Grid.NGroupingRule = New Nevron.Nov.Grid.NGroupingRule(grid.Columns.GetColumnByFieldName("Product Name"))
            
            ' create a footer summary row for the total total
            groupingRule.CreateFooterSummaryRowsDelegate = Function(ByVal args As Nevron.Nov.Grid.NGroupingRuleCreateSummaryRowsArgs)
                ' get the recordset for the group
                Dim recordset As Nevron.Nov.Data.NRecordset = args.GroupRow.Recordset

                ' calculate the sum of totals
                Dim total As Double = 0

                                                               For i As Integer = 0 To recordset.Count - 1
                                                                   total += System.Convert.ToDouble(totalColumn.GetRowValue(recordset(i)))
                                                               Next

                ' create the total summary row
                Dim totalRow As Nevron.Nov.Grid.NSummaryRow = New Nevron.Nov.Grid.NSummaryRow()
                                                               totalRow.Cells = New Nevron.Nov.Grid.NSummaryCellCollection()
                                                               Dim cell As Nevron.Nov.Grid.NSummaryCell = New Nevron.Nov.Grid.NSummaryCell()
                                                               cell.BeginXPosition.Mode = Nevron.Nov.Grid.ENSpanCellBeginXPositionMode.AnchorToEndX
                                                               cell.EndXPosition.Mode = Nevron.Nov.Grid.ENSpanCellEndXPositionMode.RowEndX
                                                               cell.Content = New Nevron.Nov.UI.NLabel("Grand Total: " & total.ToString("0.00"))
                                                               totalRow.Cells.Add(cell)
                                                               Return New Nevron.Nov.Grid.NSummaryRow() {totalRow}
                                                           End Function

            grid.GroupingRules.Add(groupingRule)
            Return view
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates groups' summary rows and calculated columns.
</p>
<p>
    In this example we have created the <b>Total</b> <b>NCustomCalculatedColumn</b> that shows the <b>Price</b> * <b>Quantity</b> calculation.
</p>
<p>
    Additionally the we have grouped the records by <b>Product Name</b> and created a summary row for each of the groups that displays the <b>Grand Total</b> of the totals contained in the group.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NGroupingSummaryRowsExample.
        ''' </summary>
        Public Shared ReadOnly NGroupingSummaryRowsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
