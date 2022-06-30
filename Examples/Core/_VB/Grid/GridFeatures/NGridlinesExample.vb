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
    Public Class NGridlinesExample
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
            Nevron.Nov.Examples.Grid.NGridlinesExample.NGridlinesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NGridlinesExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_GridView = New Nevron.Nov.Grid.NTableGridView()
            Me.m_GridView.Grid.DataSource = NDummyDataSource.CreateCompanySalesDataSource()
            Return Me.m_GridView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            ' default gridlines
            If True Then
                Dim pstack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                pstack.VerticalSpacing = 2
                Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_GridView.Grid), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_GridView.Grid, Nevron.Nov.Grid.NGrid.HorizontalGridlinesStrokeProperty, Nevron.Nov.Grid.NGrid.VerticalGridlinesStrokeProperty)

                For i As Integer = 0 To editors.Count - 1
                    pstack.Add(editors(i))
                Next

                stack.Add(New Nevron.Nov.UI.NGroupBox("Grid Gridlines", New Nevron.Nov.UI.NUniSizeBoxGroup(pstack)))
            End If

            ' column gridlines
            If True Then
                Dim pstack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                pstack.VerticalSpacing = 2
                Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Nevron.Nov.Grid.NColumnCollection.NColumnCollectionSchema), Nevron.Nov.Dom.NSchema)).CreatePropertyEditors(Me.m_GridView.Grid.Columns, Nevron.Nov.Grid.NColumnCollection.VisibleProperty, Nevron.Nov.Grid.NColumnCollection.TopGridlineStrokeProperty, Nevron.Nov.Grid.NColumnCollection.BottomGridlineStrokeProperty, Nevron.Nov.Grid.NColumnCollection.VerticalGridlinesStrokeProperty)

                For i As Integer = 0 To editors.Count - 1
                    pstack.Add(editors(i))
                Next

                stack.Add(New Nevron.Nov.UI.NGroupBox("Columns Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(pstack)))
            End If

            ' row headers gridlines
            If True Then
                Dim pstack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                pstack.VerticalSpacing = 2
                Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Nevron.Nov.Grid.NRowHeaderCollection.NRowHeaderCollectionSchema), Nevron.Nov.Dom.NSchema)).CreatePropertyEditors(Me.m_GridView.Grid.RowHeaders, Nevron.Nov.Grid.NRowHeaderCollection.VisibleProperty, Nevron.Nov.Grid.NRowHeaderCollection.LeftGridlineStrokeProperty, Nevron.Nov.Grid.NRowHeaderCollection.RightGridlineStrokeProperty, Nevron.Nov.Grid.NRowHeaderCollection.HorizontalGridlinesStrokeProperty)

                For i As Integer = 0 To editors.Count - 1
                    pstack.Add(editors(i))
                Next

                stack.Add(New Nevron.Nov.UI.NGroupBox("Row Headers Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(pstack)))
            End If

            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates gridlines.
</p>
<p>
    NOV Grid for .NET features several types of gridlines that are displayed by the Grid cells, Columns and Row Headers.
</p>
<p>
    Use the controls on the right to change the appearance of the gridlines.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_GridView As Nevron.Nov.Grid.NTableGridView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NGridlinesExample.
        ''' </summary>
        Public Shared ReadOnly NGridlinesExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
