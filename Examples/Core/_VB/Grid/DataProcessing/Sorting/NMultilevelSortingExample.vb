Imports Nevron.Nov.Dom
Imports Nevron.Nov.Grid
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NMultilevelSortingExample
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
            Nevron.Nov.Examples.Grid.NMultilevelSortingExample.NMultilevelSortingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NMultilevelSortingExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a view and get its grid
            Dim view As Nevron.Nov.Grid.NTableGridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = view.Grid

            ' bind the grid to the data source
            grid.DataSource = NDummyDataSource.CreateCompanySalesDataSource()

            ' create a sorting rule that sorts by the company column first
            Dim companyColumn As Nevron.Nov.Grid.NColumn = grid.Columns.GetColumnByFieldName("Company")
            grid.SortingRules.Add(New Nevron.Nov.Grid.NSortingRule(companyColumn, Nevron.Nov.Grid.ENSortingDirection.Ascending))

            ' create a sorting rule that sorts by the sales column next
            Dim salesColumn As Nevron.Nov.Grid.NColumn = grid.Columns.GetColumnByFieldName("Sales")
            grid.SortingRules.Add(New Nevron.Nov.Grid.NSortingRule(salesColumn, Nevron.Nov.Grid.ENSortingDirection.Ascending))
            Return view
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates multilevel grid sorting by columns.
</p>
<p>
    In this example we have sorted in <b>Ascending</b> order first by the <b>Company</b> field and then by the <b>Sales</b> field.
</p>
"
        End Function

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NColumnSortingExample.
        ''' </summary>
        Public Shared ReadOnly NMultilevelSortingExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
