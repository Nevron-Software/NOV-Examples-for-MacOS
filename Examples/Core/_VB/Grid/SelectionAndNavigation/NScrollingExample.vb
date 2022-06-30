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
    Public Class NScrollingExample
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
            Nevron.Nov.Examples.Grid.NScrollingExample.NScrollingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NScrollingExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_TableView = New Nevron.Nov.Grid.NTableGridView()

            ' create a dummy data source with many columns to demonstrate horizontal scrolling
                            ' person info
                                ' address info
                                ' product info
                Dim dataTable As Nevron.Nov.Data.NMemoryDataTable = New Nevron.Nov.Data.NMemoryDataTable(New Nevron.Nov.Data.NFieldInfo() {New Nevron.Nov.Data.NFieldInfo("Name-0", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Gender-1", GetType(ENGender)), New Nevron.Nov.Data.NFieldInfo("Birthday-2", GetType(System.DateTime)), New Nevron.Nov.Data.NFieldInfo("Phone-3", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Email-4", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Country-5", GetType(ENCountry)), New Nevron.Nov.Data.NFieldInfo("City-6", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Address-7", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Product Name-8", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Product Price-9", GetType(System.[Double])), New Nevron.Nov.Data.NFieldInfo("Product Quantity-10", GetType(System.Int32))})

            For i As Integer = 0 To 1000 - 1
                Dim personInfo As NDummyDataSource.NPersonInfo = NDummyDataSource.RandomPersonInfo()
                Dim addressInfo As NDummyDataSource.NAddressInfo = NDummyDataSource.RandomAddressInfo()
                Dim productInfo As NDummyDataSource.NProductInfo = NDummyDataSource.RandomProductInfo()
                    ' person info
                                        ' address
                                        ' product
                    dataTable.AddRow(personInfo.Name, personInfo.Gender, personInfo.Birthday, personInfo.Phone, personInfo.Email, addressInfo.Country, addressInfo.City, addressInfo.Address, productInfo.Name, productInfo.Price, NDummyDataSource.RandomInt32(1, 100))
            Next

            Me.m_TableView.Grid.DataSource = New Nevron.Nov.Data.NDataSource(dataTable)
            Return Me.m_TableView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            
            ' create the horizontal scrolling properties
            If True Then
                Dim hstack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Dim designer As Nevron.Nov.Editors.NDesigner = Nevron.Nov.Editors.NDesigner.GetDesigner(Nevron.Nov.Grid.NTableGrid.NTableGridSchema)
                Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = designer.CreatePropertyEditors(Me.m_TableView.Grid, Nevron.Nov.Grid.NTableGrid.HScrollModeProperty, Nevron.Nov.Grid.NTableGrid.IntegralHScrollProperty, Nevron.Nov.Grid.NTableGrid.SmallHScrollChangeProperty)

                For i As Integer = 0 To editors.Count - 1
                    hstack.Add(editors(i))
                Next

                Dim hgroup As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Horizontal Scrolling", hstack)
                stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(hgroup))
            End If

            ' create the vertical scrolling properties
            If True Then
                Dim vstack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Dim designer As Nevron.Nov.Editors.NDesigner = Nevron.Nov.Editors.NDesigner.GetDesigner(Nevron.Nov.Grid.NTableGrid.NTableGridSchema)
                Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = designer.CreatePropertyEditors(Me.m_TableView.Grid, Nevron.Nov.Grid.NTableGrid.VScrollModeProperty, Nevron.Nov.Grid.NTableGrid.IntegralVScrollProperty, Nevron.Nov.Grid.NTableGrid.SmallVScrollChangeProperty)

                For i As Integer = 0 To editors.Count - 1
                    vstack.Add(editors(i))
                Next

                Dim vgroup As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Vertical Scrolling", vstack)
                stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(vgroup))
            End If

            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates the properties that control the grid horizontal and vertical scrolling behavior.
</p>
<p>
    Note that the grid supports integral and pixel-wise scrolling in both the horizontal and vertical dimensions.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_TableView As Nevron.Nov.Grid.NTableGridView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NScrollingExample.
        ''' </summary>
        Public Shared ReadOnly NScrollingExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
