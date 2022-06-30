Imports System
Imports Nevron.Nov.Data
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Grid
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NTreeGridViewExample
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
            Nevron.Nov.Examples.Grid.NTreeGridViewExample.NTreeGridViewExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NTreeGridViewExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a hieararchical data table that represents a simple organization.
            Dim dataTable As Nevron.Nov.Data.NMemoryDataTable = New Nevron.Nov.Data.NMemoryDataTable(New Nevron.Nov.Data.NFieldInfo() {New Nevron.Nov.Data.NFieldInfo("Id", GetType(System.Int32)), New Nevron.Nov.Data.NFieldInfo("ParentId", GetType(System.Int32)), New Nevron.Nov.Data.NFieldInfo("Name", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Job Title", GetType(ENJobTitle)), New Nevron.Nov.Data.NFieldInfo("Company", GetType(String))})
            Dim i As Integer = 0

            ' company 1
            dataTable.AddRow(0, -1, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.President, NDummyDataSource.CompanyNames(0))
            dataTable.AddRow(1, 0, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.VicePresident, NDummyDataSource.CompanyNames(0))
            dataTable.AddRow(2, 1, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.SalesManager, NDummyDataSource.CompanyNames(0))
            dataTable.AddRow(3, 2, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.SalesRepresentative, NDummyDataSource.CompanyNames(0))
            dataTable.AddRow(4, 2, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.SalesRepresentative, NDummyDataSource.CompanyNames(0))
            dataTable.AddRow(5, 1, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.LeadDevelop, NDummyDataSource.CompanyNames(0))
            dataTable.AddRow(6, 5, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.SeniorDeveloper, NDummyDataSource.CompanyNames(0))
            dataTable.AddRow(7, 5, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.SeniorDeveloper, NDummyDataSource.CompanyNames(0))

            ' company 2
            dataTable.AddRow(8, -1, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.President, NDummyDataSource.CompanyNames(1))
            dataTable.AddRow(9, 8, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.VicePresident, NDummyDataSource.CompanyNames(1))
            dataTable.AddRow(10, 9, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.SalesManager, NDummyDataSource.CompanyNames(1))
            dataTable.AddRow(11, 10, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.SalesRepresentative, NDummyDataSource.CompanyNames(1))
            dataTable.AddRow(12, 10, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.SalesRepresentative, NDummyDataSource.CompanyNames(1))
            dataTable.AddRow(13, 10, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.SalesRepresentative, NDummyDataSource.CompanyNames(1))
            dataTable.AddRow(14, 9, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.LeadDevelop, NDummyDataSource.CompanyNames(1))
            dataTable.AddRow(15, 14, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.SeniorDeveloper, NDummyDataSource.CompanyNames(1))
            dataTable.AddRow(16, 14, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.SeniorDeveloper, NDummyDataSource.CompanyNames(1))
            dataTable.AddRow(17, 14, NDummyDataSource.PersonInfos(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)).Name, ENJobTitle.SeniorDeveloper, NDummyDataSource.CompanyNames(1))

            ' create a tree grid view
            ' records are identified by the Id field.
            ' the parent of each record is specified by the ParentId field.
            Me.m_TreeGridView = New Nevron.Nov.Grid.NTreeGridView()
            Me.m_TreeGridView.Grid.IdFieldName = "Id"
            Me.m_TreeGridView.Grid.ParentIdFieldName = "ParentId"
            Me.m_TreeGridView.Grid.DataSource = New Nevron.Nov.Data.NDataSource(dataTable)
            Return Me.m_TreeGridView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_TreeGridView.Grid), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_TreeGridView.Grid, Nevron.Nov.Grid.NGrid.FrozenRowsProperty, Nevron.Nov.Grid.NGrid.IntegralVScrollProperty)

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates the <b>NTreeGridView</b> and <b>NTreeGrid</b> 
</p>
<p>
    The <b>NTreeGridView</b> represents a grid that displays hierarchical data from the data source.
</p>
<p>
The hierarchy is encoded in the data source with the help of two service fields:
    <br/>
    <b>Id</b> - uniquely identifies the records in the data source.
    <br/>
    <b>ParentId</b> - identifies the parent record of a specific record by Id.
</p>
<p>
Usually the service fields of the data source are not displayed by the <b>Tree Grid</b> as is the case in this example.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_TreeGridView As Nevron.Nov.Grid.NTreeGridView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NSelfReferencingExample.
        ''' </summary>
        Public Shared ReadOnly NTreeGridViewExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
