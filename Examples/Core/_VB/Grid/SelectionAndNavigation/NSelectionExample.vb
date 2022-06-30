Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Grid
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NSelectionExample
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
            Nevron.Nov.Examples.Grid.NSelectionExample.NSelectionExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NSelectionExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_TableView = New Nevron.Nov.Grid.NTableGridView()
            Me.m_TableView.Grid.DataSource = NDummyDataSource.CreateCompanySalesDataSource()
            Me.m_TableView.Grid.AllowEdit = True
            Return Me.m_TableView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            
            ' create the row headers properties
            If True Then
                Dim selectionStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Dim designer As Nevron.Nov.Editors.NDesigner = Nevron.Nov.Editors.NDesigner.GetDesigner(Nevron.Nov.Grid.NGridSelection.NGridSelectionSchema)
                Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = designer.CreatePropertyEditors(Me.m_TableView.Grid.Selection, Nevron.Nov.Grid.NGridSelection.ModeProperty, Nevron.Nov.Grid.NGridSelection.AllowCurrentCellProperty, Nevron.Nov.Grid.NGridSelection.BeginEditCellOnClickProperty, Nevron.Nov.Grid.NGridSelection.BeginEditCellOnDoubleClickProperty, Nevron.Nov.Grid.NGridSelection.BeginEditCellOnBecomeCurrentProperty)

                For i As Integer = 0 To editors.Count - 1
                    selectionStack.Add(editors(i))
                Next

                Dim selectionGroup As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Selection Properties", selectionStack)
                stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(selectionGroup))
            End If

            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates the grid selection modes and various properties that affect the current cell and its editing behavior.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_TableView As Nevron.Nov.Grid.NTableGridView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NSelectionExample.
        ''' </summary>
        Public Shared ReadOnly NSelectionExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
