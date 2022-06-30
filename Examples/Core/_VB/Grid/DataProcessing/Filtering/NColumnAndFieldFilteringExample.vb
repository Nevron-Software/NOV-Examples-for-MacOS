Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Grid
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NColumnAndFieldFilteringExample
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
            Nevron.Nov.Examples.Grid.NColumnAndFieldFilteringExample.NColumnAndFieldFilteringExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NColumnAndFieldFilteringExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim view As Nevron.Nov.Grid.NTableGridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = view.Grid

            ' bind the grid to the data source, but exclude the "PersonId" column.
            AddHandler grid.AutoCreateColumn, Sub(ByVal arg As Nevron.Nov.Grid.NAutoCreateColumnEventArgs)
                                                  If Equals(arg.FieldInfo.Name, "PersonId") Then
                                                      arg.DataColumn = Nothing
                                                  End If
                                              End Sub

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

            ' show only records where PersonId = "0"
            ' NOTE: when the rule is not associated with a column, you must explicitly specify a row value for the row condition.
            Dim personIdFilterRule As Nevron.Nov.Grid.NFilteringRule = New Nevron.Nov.Grid.NFilteringRule()
            Dim rowCondition As Nevron.Nov.Grid.NOperatorRowCondition = New Nevron.Nov.Grid.NOperatorRowCondition(Nevron.Nov.Grid.ENRowConditionOperator.Equals, "0")
            rowCondition.RowValue = New Nevron.Nov.Grid.NFieldRowValue("PersonId")
            personIdFilterRule.RowCondition = rowCondition
            grid.FilteringRules.Add(personIdFilterRule)

            ' show only records for which the total column is larger than 150
            ' NOTE: when the rule is associated with a column, by default the row condition operates on the column values.
            Dim companyFilterRule As Nevron.Nov.Grid.NFilteringRule = New Nevron.Nov.Grid.NFilteringRule()
            companyFilterRule.Column = totalColumn
            companyFilterRule.RowCondition = New Nevron.Nov.Grid.NOperatorRowCondition(Nevron.Nov.Grid.ENRowConditionOperator.GreaterThan, "150")
            grid.FilteringRules.Add(companyFilterRule)
            
            ' customize the grid
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
    Demonstrates filtering by field or column values as well as multiple filters.
</p>
<p>
    NOV Grid allows you to create filter rules that work on column and field provided row values. 
    This makes it possible to create filter rules that work on calculated columns (i.e. columns that do not have an associated field in the data source) 
    as well as create filter rules that work on data source fields, regardless of whether they are represented by columns in the grid.
</p>
<p>
    In this example we have create two filter rules.
</br>
    The first filter rule filters records for which the <b>PersonId</b> field is equal to 0. This filter rule is not associated with a column, 
    since the <b>PersonId</b> field is not represented by a column in the grid.
</br>
    The second filter rule filters records for which the <b>Total</b> calculated column is greater than 150. This filter rule is associated with a column,
    which is at the same time row value provider for this filter rule row condition. Note that the <b>Total</b> column does not have an associated field in the data source.
</p>"
        End Function

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NColumnAndFieldFilteringExample.
        ''' </summary>
        Public Shared ReadOnly NColumnAndFieldFilteringExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
