Imports Nevron.Nov.Dom
Imports Nevron.Nov.Grid
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NFormulaGroupingExample
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
            Nevron.Nov.Examples.Grid.NFormulaGroupingExample.NFormulaGroupingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NFormulaGroupingExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim gridView As Nevron.Nov.Grid.NTableGridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = gridView.Grid

            ' bind the grid to the data source
            grid.DataSource = NDummyDataSource.CreateCompanySalesDataSource()

            ' create a grouping rule that groups by the company field value first
            ' note that in order to indicate the grouping in the grouping panel, the rule must reference the respective column
            Dim companyColumn As Nevron.Nov.Grid.NColumn = grid.Columns.GetColumnByFieldName("Company")
            Dim fx1 As String = grid.CreateFormulaFieldName("Company")
            Dim fxRowValue As Nevron.Nov.Grid.NFormulaRowValue = New Nevron.Nov.Grid.NFormulaRowValue(fx1)
            Dim groupingRule1 As Nevron.Nov.Grid.NGroupingRule = New Nevron.Nov.Grid.NGroupingRule(companyColumn, fxRowValue, Nevron.Nov.Grid.ENSortingDirection.Ascending)
            grid.GroupingRules.Add(groupingRule1)

            ' create a grouping rule that groups by sales larger than 1000 next
            ' note that in order to indicate the grouping in the grouping panel, the rule must reference the respective column
            Dim fx2 As String = grid.CreateFormulaFieldName("Sales") & ">1000"
            Dim salesColumn As Nevron.Nov.Grid.NColumn = grid.Columns.GetColumnByFieldName("Sales")
            Dim groupingRule2 As Nevron.Nov.Grid.NGroupingRule = Nevron.Nov.Grid.NGroupingRule.FromFormula(salesColumn, fx2)
            groupingRule2.CreateGroupRowCellsDelegate += Function(ByVal arg As Nevron.Nov.Grid.NGroupingRuleCreateGroupRowCellsArgs)
                                                             Dim groupValue As Boolean = CBool(CType(arg.GroupRow.GroupValue, Nevron.Nov.NVariant))
                                                             Dim text As String = If(groupValue, "Sales greater than 1000", "Sales less than or equal to 1000")
                                                             Return New Nevron.Nov.Grid.NGroupRowCell() {New Nevron.Nov.Grid.NGroupRowCell(text)}
                                                         End Function

            grid.GroupingRules.Add(groupingRule2)
            
            ' alter some view preferences
            grid.AllowSortColumns = True
            grid.AlternatingRows = True
            grid.RowHeaders.Visible = True
            Return gridView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates formula row values in combination with grouping rules.
</p>
<p>
    In this example we have created two grouping rules, one that groups by the <b>Company</b> field and another one that groups by the <b>Sales</b> larger than 1000.
    Both conditions have been expressed with instances of the <b>NFormulaRowValue</b>. 
</p>
<p>
    Furthermore the example creates custom group row cells that display the <b>""Sales greater than 1000""</b> or <b>""Sales less than or equal to 1000""</b> for the second grouping rule rows.
</p>
"
        End Function

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NFormulaGroupingExample.
        ''' </summary>
        Public Shared ReadOnly NFormulaGroupingExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
