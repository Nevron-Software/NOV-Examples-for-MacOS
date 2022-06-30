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
    Public Class NFormulaCalculatedColumnsExample
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
            Nevron.Nov.Examples.Grid.NFormulaCalculatedColumnsExample.NFormulaCalculatedColumnsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NFormulaCalculatedColumnsExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_GridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = Me.m_GridView.Grid

            ' bind the grid to the data source
            grid.DataSource = NDummyDataSource.CreatePersonsOrdersDataSource()

            ' add a formula column
            Me.m_TotalColumn = New Nevron.Nov.Grid.NFormulaCalculatedColumn()
            Me.m_TotalColumn.Title = "Total"
            Dim fx As String = grid.CreateFormulaFieldName("Price") & "*" & grid.CreateFormulaFieldName("Quantity")
            Me.m_TotalColumn.Formula = fx
            Me.m_TotalColumn.Format.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.SeaShell)
            grid.Columns.Add(Me.m_TotalColumn)
            Return Me.m_GridView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_TotalColumn), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_TotalColumn, Nevron.Nov.Grid.NFormulaCalculatedColumn.FormulaProperty)

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates formula calculated columns.
</p>
<p>
    Formula calculated columns are columns whose row values are not obtained from the data source, 
    but are dynamically calculated via a user-specified formula, that can reference data source field values.
    Formula calculated columns are represented by the <b>NFormulaCalculatedColumn</b> class.
</p>
<p>
    In the example the <b>Total</b> column is a formula calculated column that is calculated via the {<b>Price</b>*<b>Quantity</b>} formula.
</p>"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_GridView As Nevron.Nov.Grid.NTableGridView
        Private m_TotalColumn As Nevron.Nov.Grid.NFormulaCalculatedColumn

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NFormulaCalculatedColumnsExample.
        ''' </summary>
        Public Shared ReadOnly NFormulaCalculatedColumnsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
