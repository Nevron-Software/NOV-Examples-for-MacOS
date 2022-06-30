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
    Public Class NExtendedColumnEditorsExample
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
            Nevron.Nov.Examples.Grid.NExtendedColumnEditorsExample.NExtendedColumnEditorsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NExtendedColumnEditorsExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a view and get its grid
            Dim view As Nevron.Nov.Grid.NTableGridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = view.Grid
            grid.AllowEdit = True

            ' create persons order data source
            Dim personOrders As Nevron.Nov.Data.NDataSource = NDummyDataSource.CreatePersonsOrdersDataSource()

            ' get the min and max price. We will use it in the progress bars.
            Dim min, max As Object
            personOrders.TryGetMin("Price", min)
            personOrders.TryGetMax("Price", max)
            AddHandler grid.AutoCreateColumn, Sub(ByVal args As Nevron.Nov.Grid.NAutoCreateColumnEventArgs)
                                                  If Equals(args.FieldInfo.Name, "Price") Then
                    ' create a progress bar column format for the Price field
                    Dim sliderColumnEditor As Nevron.Nov.Grid.NSliderColumnEditor = New Nevron.Nov.Grid.NSliderColumnEditor()
                                                      args.DataColumn.Editor = sliderColumnEditor
                                                      args.DataColumn.WidthMode = Nevron.Nov.Grid.ENColumnWidthMode.Fixed
                                                      args.DataColumn.FixedWidth = 150
                                                  End If
                                              End Sub

            grid.DataSource = personOrders
            Return view
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates the extended column editors. 
</p>
<p>
    Extended column editors are editors, which are not automatically assigned to data columns during data binding.
    Instead it is up to the user to assign these editors to specific columns.
</p>
<p>
    In this example we have assigned the <b>NSliderColumnEditor</b> to the <b>Price</b> column.
</p>
"
        End Function

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NExtendedColumnEditorsExample.
        ''' </summary>
        Public Shared ReadOnly NExtendedColumnEditorsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
