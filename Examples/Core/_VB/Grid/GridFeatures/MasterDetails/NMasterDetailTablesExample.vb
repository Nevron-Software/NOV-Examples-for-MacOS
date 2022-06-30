Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Grid
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NMasterDetailTablesExample
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
            Nevron.Nov.Examples.Grid.NMasterDetailTablesExample.NMasterDetailTablesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NMasterDetailTablesExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a view and get its grid
            Me.m_View = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = Me.m_View.Grid

            ' bind the grid to the data source
            grid.DataSource = NDummyDataSource.CreatePersonsDataSource()

            ' configure the master grid
            grid.AllowEdit = False

            ' assign some icons to the columns
            For i As Integer = 0 To grid.Columns.Count - 1
                Dim dataColumn As Nevron.Nov.Grid.NDataColumn = TryCast(grid.Columns(i), Nevron.Nov.Grid.NDataColumn)
                If dataColumn Is Nothing Then Continue For
                Dim image As Nevron.Nov.Graphics.NImage = Nothing

                Select Case dataColumn.FieldName
                    Case "Name"
                        image = Nevron.Nov.Grid.NResources.Image__16x16_Contacts_png
                    Case "Gender"
                        image = Nevron.Nov.Grid.NResources.Image__16x16_Gender_png
                    Case "Birthday"
                        image = Nevron.Nov.Grid.NResources.Image__16x16_Birthday_png
                    Case "Country"
                        image = Nevron.Nov.Grid.NResources.Image__16x16_Globe_png
                    Case "Phone"
                        image = Nevron.Nov.Grid.NResources.Image__16x16_Phone_png
                    Case "Email"
                        image = Nevron.Nov.Grid.NResources.Image__16x16_Mail_png
                    Case Else
                        Continue For
                End Select

                ' NOTE: The CreateHeaderContentDelegate is invoked whenever the Title changes or the UpdateHeaderContent() is called.
                ' you can use this event to create custom column header content
                dataColumn.CreateHeaderContentDelegate = Function(ByVal theColumn As Nevron.Nov.Grid.NColumn)
                                                             Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(image, dataColumn.Title, Nevron.Nov.UI.ENPairBoxRelation.Box1BeforeBox2)
                                                             pairBox.Spacing = 2
                                                             Return pairBox
                                                         End Function

                dataColumn.UpdateHeaderContent()
            Next

            ' get the grid master details
            Dim masterDetails As Nevron.Nov.Grid.NMasterDetails = grid.MasterDetails

            ' creater the table grid detail. 
            ' NOTE: It shows information from the sales data source. the details are bound using field binding
            Dim detail As Nevron.Nov.Grid.NTableGridDetail = New Nevron.Nov.Grid.NTableGridDetail()
            masterDetails.Details.Add(detail)
            detail.DataSource = NDummyDataSource.CreatePersonsOrdersDataSource()

            ' configure the details grid
            detail.GridView.Grid.AllowEdit = False
            Dim masterBinding As Nevron.Nov.Grid.NRelationMasterBinding = New Nevron.Nov.Grid.NRelationMasterBinding()
            masterBinding.Relations.Add(New Nevron.Nov.Grid.NRelation("Id", "PersonId"))
            detail.MasterBinding = masterBinding
            Return Me.m_View
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_View.Grid), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_View.Grid, Nevron.Nov.Grid.NGrid.FrozenRowsProperty, Nevron.Nov.Grid.NGrid.IntegralVScrollProperty)

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates how to show other tables data that is related to the master table rows.
</p>
<p>
    <b>NTableGridDetail</b> and <b>NTreeGridDetail</b> are master-details that can display a table or tree grid that display information from a slave data source.
</p>
<p>
    In this example we have created an <b>NTableGridDetail</b> detail that display information about each specific person orders.
    The master grid shows the <b>Persons</b> data source.
    The detail for each person are extracted from the <b>PersonOrders</b> data source and displayed as a table grid again.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_View As Nevron.Nov.Grid.NTableGridView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NMasterDetailsExample.
        ''' </summary>
        Public Shared ReadOnly NMasterDetailTablesExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
