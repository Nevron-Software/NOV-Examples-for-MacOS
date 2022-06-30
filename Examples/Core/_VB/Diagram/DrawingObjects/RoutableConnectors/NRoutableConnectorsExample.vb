Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
	''' <summary>
	''' Summary description for NRoutableConnectors.
	''' </summary>
	Public Class NRoutableConnectorsExample
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
            Nevron.Nov.Examples.Diagram.NRoutableConnectorsExample.NRoutableConnectorsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NRoutableConnectorsExample), NExampleBaseSchema)
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
	This example demonstrates routable connectors and routing.
</p>
<p>
	Routing is the process of finding a path between two points, which strives not 
	to cross any obstacles and also tries to obey certain aesthetic criteria (such 
	as minimal number of turns, port orientation etc.).
</p>
<p>
    Routing works with three corner stone objects: routable connector, obstacle shapes and router. 
    A routable connector tries to avoid the current set of obstacle shapes (residing in the page) by obtaining routing points from the router. 
    The router is responsible for creating and maintaining a routing graph for the current set of obstacle shapes existing in the page.    
</p>
<p>
	A routable connector can be automatically rerouted in three modes:
	<ul>
		<li>
			<b>Never</b> - the connector is never automatically rerouted. You can still reroute the 
			route by executing the Reroute command (from the context menu or from code).
		</li>
		<li>
			<b>Always</b> - the connector is automatically rerouted when any of the obstacles have 
			changed (i.e. there is a possibility for the route to be rerouted in a better way).
		</li>
		<li>
			<b>When Needed</b> - the connector is automatically rerouted when an obstacle is placed on it 
			(i.e. the route needs to be rerouted cause it crosses an obstacle).
		</li>
	</ul>
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

            ' hide grid and ports
            drawing.ScreenVisibility.ShowGrid = False
            drawing.ScreenVisibility.ShowPorts = False

            ' create a stylesheet for styling the different bricks
            Dim styleSheet As Nevron.Nov.Dom.NStyleSheet = New Nevron.Nov.Dom.NStyleSheet()
            drawingDocument.StyleSheets.AddChild(styleSheet)

            ' the first rule fills brichs with UserClass BRICK1
            Dim ruleBrick1 As Nevron.Nov.Dom.NRule = New Nevron.Nov.Dom.NRule()
            styleSheet.Add(ruleBrick1)
            Dim sb As Nevron.Nov.Dom.NSelectorBuilder = ruleBrick1.GetSelectorBuilder()
            sb.Start()
            sb.Type(Nevron.Nov.Diagram.NGeometry.NGeometrySchema)
            sb.ChildOf()
            sb.UserClass("BRICK1")
            sb.[End]()
            ruleBrick1.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Graphics.NFill)(Nevron.Nov.Diagram.NGeometry.FillProperty, New Nevron.Nov.Graphics.NHatchFill(Nevron.Nov.Graphics.ENHatchStyle.HorizontalBrick, Nevron.Nov.Graphics.NColor.DarkOrange, Nevron.Nov.Graphics.NColor.Gold)))

            ' the second rule fills brichs with UserClass BRICK2
            Dim ruleBrick2 As Nevron.Nov.Dom.NRule = New Nevron.Nov.Dom.NRule()
            styleSheet.Add(ruleBrick2)
            sb = ruleBrick2.GetSelectorBuilder()
            sb.Start()
            sb.Type(Nevron.Nov.Diagram.NGeometry.NGeometrySchema)
            sb.ChildOf()
            sb.UserClass("BRICK2")
            sb.[End]()
            ruleBrick2.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Graphics.NFill)(Nevron.Nov.Diagram.NGeometry.FillProperty, New Nevron.Nov.Graphics.NHatchFill(Nevron.Nov.Graphics.ENHatchStyle.HorizontalBrick, Nevron.Nov.Graphics.NColor.DarkRed, Nevron.Nov.Graphics.NColor.Gold)))

            ' create all shapes
            ' create the maze frame
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(50, 0, 700, 50), "BRICK1")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(750, 0, 50, 800), "BRICK1")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(50, 750, 700, 50), "BRICK1")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(0, 0, 50, 800), "BRICK1")

            ' create the maze obstacles
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(100, 200, 200, 50), "BRICK2")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(300, 50, 50, 200), "BRICK2")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(450, 50, 50, 200), "BRICK2")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(500, 200, 200, 50), "BRICK2")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(50, 300, 250, 50), "BRICK2")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(500, 300, 250, 50), "BRICK2")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(350, 350, 100, 100), "BRICK2")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(50, 450, 250, 50), "BRICK2")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(500, 450, 250, 50), "BRICK2")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(100, 550, 200, 50), "BRICK2")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(300, 550, 50, 200), "BRICK2")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(450, 550, 50, 200), "BRICK2")
            Me.CreateBrick(New Nevron.Nov.Graphics.NRectangle(500, 550, 200, 50), "BRICK2")

            ' create the first set of start/end shapes
            Dim start As Nevron.Nov.Diagram.NShape = Me.CreateEllipse(New Nevron.Nov.Graphics.NRectangle(100, 100, 50, 50), "START")
            Dim [end] As Nevron.Nov.Diagram.NShape = Me.CreateEllipse(New Nevron.Nov.Graphics.NRectangle(650, 650, 50, 50), "END")

            ' connect them with a dynamic HV routable connector, 
            ' which is rerouted whenever the obstacles have changed
            Dim routableConnector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            routableConnector.RerouteMode = Nevron.Nov.Diagram.ENRoutableConnectorRerouteMode.Always
            routableConnector.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(3, Nevron.Nov.Graphics.NColor.Black)
            activePage.Items.Add(routableConnector)

            ' connect the start and end shapes
            routableConnector.GlueBeginToShape(start)
            routableConnector.GlueEndToShape([end])

            ' reroute the connector
            routableConnector.RequestReroute()

            ' size document to fit the maze
            activePage.SizeToContent()
        End Sub

        #EndRegion
        
        #Region"Implementation"

        ''' <summary>
        ''' Creates a brick shape (Rectangle) and applies the specified class to ut 
        ''' </summary>
        ''' <paramname="bounds"></param>
        ''' <paramname="userClass"></param>
        ''' <returns></returns>
        Private Function CreateBrick(ByVal bounds As Nevron.Nov.Graphics.NRectangle, ByVal userClass As String) As Nevron.Nov.Diagram.NShape
            Dim factory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            shape.SetBounds(bounds)
            shape.UserClass = userClass
            Me.m_DrawingView.ActivePage.Items.Add(shape)
            Return shape
        End Function
        ''' <summary>
        ''' Creates an ellipse shapeand applies the specified class to ut (used for Start and End shapes)
        ''' </summary>
        ''' <paramname="bounds"></param>
        ''' <paramname="userClass"></param>
        ''' <returns></returns>
        Private Function CreateEllipse(ByVal bounds As Nevron.Nov.Graphics.NRectangle, ByVal userClass As String) As Nevron.Nov.Diagram.NShape
            Dim factory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Ellipse)
            shape.SetBounds(bounds)
            shape.UserClass = userClass
            Me.m_DrawingView.ActivePage.Items.Add(shape)
            Return shape
        End Function

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NRoutableConnectorsExample.
        ''' </summary>
        Public Shared ReadOnly NRoutableConnectorsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
