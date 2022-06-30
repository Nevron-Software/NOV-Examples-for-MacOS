Imports Nevron.Nov.Data
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Grid
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NFormulaFilteringExample
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
            Nevron.Nov.Examples.Grid.NFormulaFilteringExample.NFormulaFilteringExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NFormulaFilteringExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a view and get its grid
            Dim view As Nevron.Nov.Grid.NTableGridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = view.Grid
            
            ' bind to sales data source
            Dim dataSource As Nevron.Nov.Data.NDataSource = NDummyDataSource.CreateCompanySalesDataSource()
            grid.DataSource = dataSource

            ' create an expression filter rule that matches records for which Company is equal to Leka
            Dim companyFxName As String = dataSource.CreateFormulaFieldName("Company")
            Dim expression1 As String = companyFxName & "==""" & NDummyDataSource.RandomCompanyName() & """"
            grid.FilteringRules.Add(New Nevron.Nov.Grid.NFilteringRule(New Nevron.Nov.Grid.NFormulaRowCondition(expression1)))

            ' create an expression filter rule that matches records for which Sales is larger than 1000
            Dim salesFxName As String = dataSource.CreateFormulaFieldName("Sales")
            Dim expression2 As String = salesFxName & ">1000"
            grid.FilteringRules.Add(New Nevron.Nov.Grid.NFilteringRule(New Nevron.Nov.Grid.NFormulaRowCondition(expression2)))
            grid.AllowSortColumns = True
            grid.AlternatingRows = True
            grid.RowHeaders.Visible = True
            Return view
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>
Demonstrates formula filtering as well as multiple filter conditions.
</p>
<p>
In this example we have used formula row conditions to specify the predicates that pass only records for a specific Company, for which the <b>Sales</b> are greater than 1000.
You can use formula filtering to define complex filter rules.
</p>
"
        End Function

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NFormulaFilteringExampleSchema.
        ''' </summary>
        Public Shared ReadOnly NFormulaFilteringExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
