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
    Public Class NCustomMasterDetailsExample
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
            Nevron.Nov.Examples.Grid.NCustomMasterDetailsExample.NCustomMasterDetailsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NCustomMasterDetailsExample), NExampleBase.NExampleBaseSchema)
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
                                                             Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(image, theColumn.Title, Nevron.Nov.UI.ENPairBoxRelation.Box1BeforeBox2)
                                                             pairBox.Spacing = 2
                                                             Return pairBox
                                                         End Function

                dataColumn.UpdateHeaderContent()
            Next

            ' create the custom detail that creates a widget displaying information about the row.
            ' NOTE: The widget is created by the OnCustomDetailCreateWidget event handler.
            Dim masterDetails As Nevron.Nov.Grid.NMasterDetails = grid.MasterDetails
            Dim customDetail As Nevron.Nov.Grid.NCustomDetail = New Nevron.Nov.Grid.NCustomDetail()
            masterDetails.Details.Add(customDetail)
            customDetail.CreateWidgetDelegate = Function(ByVal arg As Nevron.Nov.Grid.NCustomDetailCreateWidgetArgs)
                ' get information about the data source row
                Dim name As String = CStr(arg.DataSource.GetValue(arg.RowIndex, "Name"))
                                                    Dim gender As ENGender = CType(arg.DataSource.GetValue(arg.RowIndex, "Gender"), ENGender)
                                                    Dim birthday As System.DateTime = CDate(arg.DataSource.GetValue(arg.RowIndex, "Birthday"))
                                                    Dim country As ENCountry = CType(arg.DataSource.GetValue(arg.RowIndex, "Country"), ENCountry)
                                                    Dim phone As String = CStr(arg.DataSource.GetValue(arg.RowIndex, "Phone"))
                                                    Dim email As String = CStr(arg.DataSource.GetValue(arg.RowIndex, "Email"))

                ' display the information as a widget
                Dim namePair As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox("Name:", name)
                                                    Dim genderPair As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox("Gender:", gender.ToString())
                                                    Dim birthdayPair As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox("Birthday:", birthday.ToString())
                                                    Dim countryPair As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox("Country:", country.ToString())
                                                    Dim phonePair As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox("Phone:", phone.ToString())
                                                    Dim emailPair As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox("Email:", email.ToString())
                                                    Dim image As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox()

                                                    Select Case gender
                                                        Case ENGender.Male
                                                            image.Image = Nevron.Nov.Grid.NResources.Image__256x256_MaleIcon_jpg
                                                        Case ENGender.Female
                                                            image.Image = Nevron.Nov.Grid.NResources.Image__256x256_FemaleIcon_jpg
                                                        Case Else
                                                    End Select

                                                    Dim infoStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                                                    infoStack.VerticalSpacing = 2.0R
                                                    infoStack.Add(namePair)
                                                    infoStack.Add(genderPair)
                                                    infoStack.Add(birthdayPair)
                                                    infoStack.Add(countryPair)
                                                    infoStack.Add(phonePair)
                                                    infoStack.Add(emailPair)
                                                    Dim dock As Nevron.Nov.UI.NDockPanel = New Nevron.Nov.UI.NDockPanel()
                                                    dock.Add(image, Nevron.Nov.Layout.ENDockArea.Left)
                                                    dock.Add(infoStack, Nevron.Nov.Layout.ENDockArea.Center)

                ' assign the widget to the event arguments.
                Return dock
                                                End Function

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
    Demonstrates how to implement custom master-details. Expand the master table rows to see details about each person.
</p>
<p>
    Custom master details are widgets that you can create in response to an event. 
    The event holds information about the data source and the row index for which a widget is needed.
    In this scenario of master-details it is up to the user to provide the widget.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_View As Nevron.Nov.Grid.NTableGridView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NCustomMasterDetailsExample.
        ''' </summary>
        Public Shared ReadOnly NCustomMasterDetailsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
