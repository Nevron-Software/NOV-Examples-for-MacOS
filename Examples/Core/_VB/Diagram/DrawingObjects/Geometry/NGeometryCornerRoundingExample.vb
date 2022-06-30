Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    ''' <summary>
    ''' 
    ''' </summary>
    Public Class NGeometryCornerRoundingExample
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
            Nevron.Nov.Examples.Diagram.NGeometryCornerRoundingExample.NGeometryCornerRoundingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NGeometryCornerRoundingExample), NExampleBaseSchema)
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
    Demonstrates the geometry corner rounding.
</p>
<p>
    In NOV diagram each geometry can be easily modified to have rounded corners.
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

            ' hide the grid
            drawing.ScreenVisibility.ShowGrid = False

            ' plotter commands
            Dim basicShapes As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim connectorFactory As Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory()

            ' create a rounded rect
            Dim rectShape As Nevron.Nov.Diagram.NShape = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            rectShape.DefaultShapeGlue = Nevron.Nov.Diagram.ENDefaultShapeGlue.GlueToGeometryIntersection
            rectShape.Geometry.CornerRounding = 10
            rectShape.SetBounds(50, 50, 100, 100)
            activePage.Items.Add(rectShape)

            ' create a rounded pentagram
            Dim pentagramShape As Nevron.Nov.Diagram.NShape = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Pentagram)
            pentagramShape.DefaultShapeGlue = Nevron.Nov.Diagram.ENDefaultShapeGlue.GlueToGeometryIntersection
            pentagramShape.Geometry.CornerRounding = 20
            pentagramShape.SetBounds(310, 310, 100, 100)
            activePage.Items.Add(pentagramShape)

            ' create a rounded routable connector
            Dim connector As Nevron.Nov.Diagram.NShape = connectorFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.RoutableConnector)
            connector.Geometry.CornerRounding = 30
            connector.GlueBeginToShape(rectShape)
            connector.GlueEndToShape(pentagramShape)
            activePage.Items.Add(connector)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NGeometryCornerRoundingExample.
        ''' </summary>
        Public Shared ReadOnly NGeometryCornerRoundingExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
