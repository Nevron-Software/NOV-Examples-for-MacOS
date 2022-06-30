Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NPortsGlueExample
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
            Nevron.Nov.Examples.Diagram.NPortsGlueExample.NPortsGlueExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NPortsGlueExample), NExampleBaseSchema)
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
    Demonstrates the ports glue. Move the rectangle close to the Connector or the Line to test the port direction that is syncrhonized with the geometry tangenta at this point.
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
            activePage.Interaction.Enable1DShapeSplitting = False
            activePage.Interaction.AutoConnectToBeginPoints = False
            activePage.Interaction.AutoConnectToEndPoints = False

            ' hide the grid
            drawing.ScreenVisibility.ShowGrid = False
            Dim basicShapes As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()

            ' Port Glue To Geometry Contour
            If True Then
                ' create a connector with begin, end and middle ports
                ' start and end ports are offset with absolute values
                Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
                activePage.Items.Add(connector)
                connector.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(50, 50))
                connector.SetEndPoint(New Nevron.Nov.Graphics.NPoint(250, 250))
                Dim beginPort As Nevron.Nov.Diagram.NPort = New Nevron.Nov.Diagram.NPort()
                connector.Ports.Add(beginPort)
                beginPort.GlueToGeometryContour(0, 10, True, Nevron.Nov.NAngle.Zero)
                Dim endPort As Nevron.Nov.Diagram.NPort = New Nevron.Nov.Diagram.NPort()
                connector.Ports.Add(endPort)
                endPort.GlueToGeometryContour(1, -10, True, Nevron.Nov.NAngle.Zero)
                Dim middlePort As Nevron.Nov.Diagram.NPort = New Nevron.Nov.Diagram.NPort()
                connector.Ports.Add(middlePort)
                middlePort.GlueToGeometryContour(0.5, 0, True, Nevron.Nov.NAngle.Zero)
            End If

            ' Port Glue To Shape Line
            If True Then
                Dim lineShape As Nevron.Nov.Diagram.NShape = Nevron.Nov.Diagram.NShape.CreateLineShape()
                activePage.Items.Add(lineShape)
                lineShape.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(350, 50))
                lineShape.SetEndPoint(New Nevron.Nov.Graphics.NPoint(550, 250))
                Dim middlePort As Nevron.Nov.Diagram.NPort = New Nevron.Nov.Diagram.NPort()
                lineShape.Ports.Add(middlePort)
                middlePort.GlueToGeometryContour(0.5, 0, True, Nevron.Nov.NAngle.Zero)
            End If

            ' create a rectangle shape with 
            If True Then
                Dim rectShape As Nevron.Nov.Diagram.NShape = Nevron.Nov.Diagram.NShape.CreateRectangle()
                rectShape.SetBounds(300, 300, 100, 100)
                rectShape.Text = "Test Port Direction with Me"
                Dim port As Nevron.Nov.Diagram.NPort = New Nevron.Nov.Diagram.NPort(0.5, 0, True)
                port.DirectionMode = Nevron.Nov.Diagram.ENPortDirectionMode.AutoCenter
                port.GlueMode = Nevron.Nov.Diagram.ENPortGlueMode.Outward
                rectShape.Ports.Add(port)
                activePage.Items.Add(rectShape)
            End If
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NPortsGlueExample.
        ''' </summary>
        Public Shared ReadOnly NPortsGlueExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
