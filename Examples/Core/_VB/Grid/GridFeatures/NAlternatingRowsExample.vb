Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Grid
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NAlternatingRowsExample
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
            Nevron.Nov.Examples.Grid.NAlternatingRowsExample.NAlternatingRowsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NAlternatingRowsExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_GridView = New Nevron.Nov.Grid.NTableGridView()
            Me.m_GridView.Grid.DataSource = NDummyDataSource.CreatePersonsOrdersDataSource()
            Me.m_GridView.Grid.AlternatingRows = True
            Return Me.m_GridView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_GridView.Grid), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_GridView.Grid, Nevron.Nov.Grid.NGrid.AlternatingRowsProperty, Nevron.Nov.Grid.NGrid.AlternatingRowsIntervalProperty, Nevron.Nov.Grid.NGrid.AlternatingRowsLengthProperty, Nevron.Nov.Grid.NGrid.AlternatingRowBackgroundFillProperty)

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates <b>Alternating Rows</b>.
</p>
<p>
    Use the controls on the right side to alter the properties that affect the <b>Alternating Rows</b> feature.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_GridView As Nevron.Nov.Grid.NTableGridView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NAlternatingRowsExample.
        ''' </summary>
        Public Shared ReadOnly NAlternatingRowsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
