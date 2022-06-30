Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NInwardAndOutwardPortsExample
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
            Nevron.Nov.Examples.Diagram.NInwardAndOutwardPortsExample.NInwardAndOutwardPortsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NInwardAndOutwardPortsExample), NExampleBaseSchema)
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
    Demonstrates the behavior of inward and outward ports. Move the rectangle close to the Star to see the rectangle rotate to match the snapped ports direction.
</p>
<p> 
    In NOV Diagram each port GlueMode can be set to Inward, Outward or InwardAndOutward. 
</p>
<p>
    <b>Inward</b> ports can accept connections with Begin and End points of 1D shapes as well as other shapes Outward ports.
    Most of the ports are only Inward ports.
</p>
<p>
    <b>Outward</b> ports can be connected only to other shapes Inward ports. When a shape with outward ports
	is moved closed a shape with inward ports. The two shapes are glued in master-slave relation. The shape
	to which the outward port belongs is rotated and translated so that its outward port location matches
	the inward port location and so that the ports directions form a line.
</p>
<p>
    <b>InwardAndOutward</b> ports behave as both inward and outward.
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
           
            ' create a rectangle with an outward port
            Dim rectShape As Nevron.Nov.Diagram.NShape = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            rectShape.SetBounds(50, 50, 100, 100)
            rectShape.Text = "Move me close to the star"
            rectShape.GetPortByName(CStr(("Top"))).GlueMode = Nevron.Nov.Diagram.ENPortGlueMode.Outward
            activePage.Items.Add(rectShape)

            ' create a pentagram
            Dim pentagramShape As Nevron.Nov.Diagram.NShape = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Pentagram)
            pentagramShape.SetBounds(310, 310, 100, 100)
            activePage.Items.Add(pentagramShape)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NInwardAndOutwardPortsExample.
        ''' </summary>
        Public Shared ReadOnly NInwardAndOutwardPortsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
