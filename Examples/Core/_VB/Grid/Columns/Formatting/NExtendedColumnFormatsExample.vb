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
    Public Class NExtendedColumnFormatsExample
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
            Nevron.Nov.Examples.Grid.NExtendedColumnFormatsExample.NExtendedColumnFormatsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NExtendedColumnFormatsExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a view and get its grid
            Dim view As Nevron.Nov.Grid.NTableGridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = view.Grid

            ' create persons order data source
            Dim personOrders As Nevron.Nov.Data.NDataSource = NDummyDataSource.CreatePersonsOrdersDataSource()

            ' get the min and max price. We will use it in the progress bars.
            Dim min, max As Object
            personOrders.TryGetMin("Price", min)
            personOrders.TryGetMax("Price", max)
            AddHandler grid.AutoCreateColumn, Sub(ByVal args As Nevron.Nov.Grid.NAutoCreateColumnEventArgs)
                                                  If Equals(args.FieldInfo.Name, "Price") Then
                    ' create a progress bar column format for the Price field
                    Dim progressBarColumnFormat As Nevron.Nov.Grid.NProgressBarColumnFormat = New Nevron.Nov.Grid.NProgressBarColumnFormat()
                                                      progressBarColumnFormat.Minimum = System.Convert.ToDouble(min)
                                                      progressBarColumnFormat.Maximum = System.Convert.ToDouble(max)
                                                      args.DataColumn.Format = progressBarColumnFormat
                                                  End If
                                              End Sub

            grid.DataSource = NDummyDataSource.CreatePersonsOrdersDataSource()
            Return view
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates the extended column formats.
</p>
<p>
    Extended column formats are such column formats that the grid does not use by default. 
    It is up to the developer to manually assign the extended column format to specific columns, as the grid will not automatically assign them.
</p>
<p>
    In this example the Price column is displayed by the <b>NProgressBarColumnFormat</b>, that is an extended column format.
<p>
"
        End Function

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NExtendedColumnFormatsExample.
        ''' </summary>
        Public Shared ReadOnly NExtendedColumnFormatsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
