Imports System
Imports Nevron.Nov.Grid
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Data
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NFieldGroupingExample
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
            Nevron.Nov.Examples.Grid.NFieldGroupingExample.NFieldGroupingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NFieldGroupingExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a view and get its grid
            Dim view As Nevron.Nov.Grid.NTableGridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = view.Grid

            ' customize the grid
            grid.AllowEdit = False

            ' create the dummy persons data source - we will use it to obtain person names from person ids from it.
            Me.m_PersonsDataSource = NDummyDataSource.CreatePersonsDataSource()

            ' bind to data source, but exclude the "PersonId" field from binding 
            AddHandler grid.AutoCreateColumn, Sub(ByVal arg As Nevron.Nov.Grid.NAutoCreateColumnEventArgs)
                                                  If Equals(arg.FieldInfo.Name, "PersonId") Then
                                                      arg.DataColumn = Nothing
                                                  End If
                                              End Sub

            grid.DataSource = NDummyDataSource.CreatePersonsOrdersDataSource()

            ' create a grouping rule that groups by the PersonId field
            Dim groupingRule As Nevron.Nov.Grid.NGroupingRule = New Nevron.Nov.Grid.NGroupingRule()
            groupingRule.RowValue = New Nevron.Nov.Grid.NFieldRowValue("PersonId")

            ' create a custom grouping header named "Person"
            groupingRule.CreateGroupingHeaderContentDelegate = Function(ByVal theGroupingRule As Nevron.Nov.Grid.NGroupingRule) New Nevron.Nov.UI.NLabel("Person")

            ' create custom group row cells that display the person Name and number of orders 
            groupingRule.CreateGroupRowCellsDelegate = Function(ByVal arg As Nevron.Nov.Grid.NGroupingRuleCreateGroupRowCellsArgs)
                ' get the person id from the row for which we create row cells.
                Dim personId As Integer = CInt(arg.GroupRow.GroupValue)

                ' get the person name that corresponds to that person id.
                Dim idField As Integer = Me.m_PersonsDataSource.GetFieldIndex("Id")
                                                           Dim rs As Nevron.Nov.Data.NRecordset = Me.m_PersonsDataSource.GetOrCreateIndex(CInt((idField))).GetRecordsForValue(personId)
                                                           Dim personName As String = CStr(Me.m_PersonsDataSource.GetValue(rs(0), "Name"))

                ' create the group row cells
                Dim personNameCell As Nevron.Nov.Grid.NGroupRowCell = New Nevron.Nov.Grid.NGroupRowCell(personName)
                                                           personNameCell.EndXPosition.Mode = Nevron.Nov.Grid.ENSpanCellEndXPositionMode.NextCellBeginX
                                                           Dim ordersCountCell As Nevron.Nov.Grid.NGroupRowCell = New Nevron.Nov.Grid.NGroupRowCell("Orders Count:" & arg.GroupRow.Recordset.Count)
                                                           ordersCountCell.EndXPosition.Mode = Nevron.Nov.Grid.ENSpanCellEndXPositionMode.RowEndX
                                                           ordersCountCell.BeginXPosition.Mode = Nevron.Nov.Grid.ENSpanCellBeginXPositionMode.AnchorToEndX
                                                           Return New Nevron.Nov.Grid.NGroupRowCell() {personNameCell, ordersCountCell}
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
    Demonstrates fields grouping, custom group header content and custom group row cells.
</p>
<p>
    In this example we group the <b>PersonSales</b> data source by the <b>PersonId</b> field, which is intentionally hidden from display in the grid.
</p>
<p>
    Because the <b>PersonId</b> field is not very informative for the user, we create custom GroupRow cells, that pull the person <b>Name</b> by <b>Id</b> from the <b>Persons</b> data source.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_PersonsDataSource As Nevron.Nov.Data.NDataSource

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NFieldGroupingExample.
        ''' </summary>
        Public Shared ReadOnly NFieldGroupingExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
