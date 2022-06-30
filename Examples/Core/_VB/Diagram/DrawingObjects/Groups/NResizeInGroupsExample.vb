Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NResizeInGroupsExample
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
            Nevron.Nov.Examples.Diagram.NResizeInGroupsExample.NResizeInGroupsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NResizeInGroupsExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' Create a simple drawing
            Dim drawingViewWithRibbon As Nevron.Nov.Diagram.NDrawingViewWithRibbon = New Nevron.Nov.Diagram.NDrawingViewWithRibbon()
            Me.m_DrawingView = drawingViewWithRibbon.View
            Me.m_DrawingView.Document.HistoryService.Pause()

            Try
                Me.InitDiagram(Me.m_DrawingView.Document)
            Finally
                Me.m_DrawingView.Document.HistoryService.[Resume]()
            End Try

            Return drawingViewWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates how 2D and 1D shapes can be resized when they are placed in groups.
</p>
<p>
    When a 2D shape whose ResizeInGroup property is set to ScaleAndReposition is resized inside a group, 
    NOV Diagram will express the shape size as a fraction of the group Width and Height, 
    and bind the shape pin point to a relative position inside group coordinate system.
    When the group is resized, the 2D shape will scale and reposition with the group.
</p>
<p>
    When a 2D shape whose ResizeInGroup property is set to RepositionOnly is resized inside a group, 
    NOV Diagram will set a constant size to the shape and bind the shape pin point 
    to a relative position inside group coordinate system.
    When the group is resized, the 2D shape will only reposition itself.
</p>
<p>
    When you move the Begin or End points of a 1D shape inside a group, 
    NOV Diagram will bind the respective end-point to a relative position inside group coordinate system.
    When the group is resized, the 1D shape will get stretched.
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' create all shapes
            Dim basicShapes As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim connectorShapes As Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory()

            ' create the group
            Dim group As Nevron.Nov.Diagram.NGroup = New Nevron.Nov.Diagram.NGroup()

            ' make some background for the group
            Dim drawRect As Nevron.Nov.Diagram.NDrawRectangle = New Nevron.Nov.Diagram.NDrawRectangle(0, 0, 1, 1)
            drawRect.Relative = True
            group.Geometry.Add(drawRect)
            group.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightCoral)
            group.SetBounds(New Nevron.Nov.Graphics.NRectangle(50, 50, 230, 330))

            ' create a rectangle that is scaled and repositioned
            Dim rect1 As Nevron.Nov.Diagram.NShape = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            rect1.Text = "Scale and Reposition"
            group.Shapes.Add(rect1)
            rect1.ResizeInGroup = Nevron.Nov.Diagram.ENResizeInGroup.ScaleAndReposition
            rect1.SetBounds(New Nevron.Nov.Graphics.NRectangle(10, 10, 100, 100))

            ' create a rectangle that is only repositioned
            Dim rect2 As Nevron.Nov.Diagram.NShape = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            rect2.Text = "Reposition Only"
            group.Shapes.Add(rect2)
            rect2.ResizeInGroup = Nevron.Nov.Diagram.ENResizeInGroup.RepositionOnly
            rect2.SetBounds(New Nevron.Nov.Graphics.NRectangle(120, 120, 100, 100))

            ' create a 1D shape
			Dim arrow As Nevron.Nov.Diagram.NShape = connectorShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Single45DegreesArrow)
            arrow.Text = "1D Shape"
            group.Shapes.Add(arrow)
            arrow.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(10, 250))
            arrow.SetEndPoint(New Nevron.Nov.Graphics.NPoint(220, 290))

            ' add the group
            drawingDocument.Content.ActivePage.Items.Add(group)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NResizeInGroupsExample.
        ''' </summary>
        Public Shared ReadOnly NResizeInGroupsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
