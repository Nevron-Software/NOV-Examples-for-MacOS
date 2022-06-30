Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Batches
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NBarycenterGraphLayoutExample
        Inherits NExampleBase
        #Region"Constructors"

        ''' <summary>
        ''' Default constructor.
        ''' </summary>
        Public Sub New()
            Me.m_Layout.FixedVertexPlacement.Mode = Nevron.Nov.Diagram.Layout.ENFixedVertexPlacementMode.AutomaticEllipseRim
            Me.m_Layout.FixedVertexPlacement.PredefinedEllipse = New Nevron.Nov.Graphics.NRectangle(0, 0, 500, 500)
        End Sub

        ''' <summary>
        ''' Static constructor.
        ''' </summary>
        Shared Sub New()
            Nevron.Nov.Examples.Diagram.NBarycenterGraphLayoutExample.NBarycenterGraphLayoutExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NBarycenterGraphLayoutExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>     
    The barycenter layout method splits the input graph into a set of fixed and free vertices. 
    Fixed vertices are nailed to the corners of a strictly convex polygon,           
    while free vertices are placed in the barycenter of their neighbors. 
    The barycenter force accessible from the <b>BarycenterForce</b> property of the layout is 
    responsible for attracting the vertices to their barycenter.
</p>
<p>
	In case there are no fixed vertices this will place all vertices at a single point, 
	which is obviously not a good graph drawing. That is why the barycenter layout needs 
	at least three fixed vertices.
</p>
<p>
	The minimal amount of fixed vertices is specified by the <b>MinFixedVerticesCount</b> property. 
	If the input graph does not have that many fixed vertices, the layout will automatically 
	fulfill this requirement. This is done by fixing the vertices with the smallest degree.
</p>
<p>
	In this example the fixed vertices are highlighted in orange.
</p>
"
        End Function

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
            AddHandler Me.m_Layout.Changed, AddressOf Me.OnLayoutChanged
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            ' property editor
            Dim editor As Nevron.Nov.Editors.NEditor = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Layout), Nevron.Nov.Dom.NNode)).CreateInstanceEditor(Me.m_Layout)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Properties", editor))
            Dim arrangeButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Arrange Diagram")
            AddHandler arrangeButton.Click, AddressOf Me.OnArrangeButtonClick
            stack.Add(arrangeButton)

            ' items stack
            Dim itemsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim triangularGrid6 As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Create Triangular Grid (levels 6)")
            AddHandler triangularGrid6.Click, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) Me.CreateTriangularGridDiagram(6)
            stack.AddChild(triangularGrid6)
            Dim triangularGrid8 As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Create Triangular Grid (levels 8)")
            AddHandler triangularGrid8.Click, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) Me.CreateTriangularGridDiagram(8)
            stack.AddChild(triangularGrid8)
            Dim random10 As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random (fixed 10, free 10)")
            AddHandler random10.Click, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) Me.CreateRandomBarycenterDiagram(10, 10)
            stack.AddChild(random10)
            Dim random15 As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random (fixed 15, free 15)")
            AddHandler random15.Click, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) Me.CreateRandomBarycenterDiagram(15, 15)
            stack.AddChild(random15)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Items", itemsStack))
            Return stack
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' Hide ports
            drawingDocument.Content.ScreenVisibility.ShowPorts = False

            ' Create a random diagram 
            Me.CreateRandomBarycenterDiagram(8, 10)

            ' Arrange the diagram
            Me.ArrangeDiagram(drawingDocument)

            ' Fit active page
            drawingDocument.Content.ActivePage.ZoomMode = Nevron.Nov.UI.ENZoomMode.Fit
        End Sub

        #EndRegion

        #Region"Implementation"

        ''' <summary>
        ''' Creates a random barycenter diagram with the specified settings
        ''' </summary>
        ''' <paramname="fixedCount">number of fixed vertices (must be larger than 3)</param>
        ''' <paramname="freeCount">number of free vertices</param>
        Private Sub CreateRandomBarycenterDiagram(ByVal fixedCount As Integer, ByVal freeCount As Integer)
            If fixedCount < 3 Then Throw New System.ArgumentException("Needs at least three fixed vertices")

            ' clean up the active page
            Dim activePage As Nevron.Nov.Diagram.NPage = Me.m_DrawingView.ActivePage
            activePage.Items.Clear()

            ' we will be using basic circle shapes with default size of (30, 30)
            Dim basicShapesFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            basicShapesFactory.DefaultSize = New Nevron.Nov.Graphics.NSize(30, 30)

			' create the fixed vertices
			Dim fixedShapes As Nevron.Nov.Diagram.NShape() = New Nevron.Nov.Diagram.NShape(fixedCount - 1) {}

            For i As Integer = 0 To fixedCount - 1
                fixedShapes(i) = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle)

                '((NDynamicPort)fixedShapes[i].Ports.GetChildByName("Center", -1)).GlueMode = DynamicPortGlueMode.GlueToLocation;
                fixedShapes(CInt((i))).Geometry.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Horizontal, Nevron.Nov.Graphics.ENGradientVariant.Variant3, New Nevron.Nov.Graphics.NColor(251, 203, 156), New Nevron.Nov.Graphics.NColor(247, 150, 56))
                fixedShapes(CInt((i))).Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(1, New Nevron.Nov.Graphics.NColor(68, 90, 108))

                ' setting the ForceXMoveable and ForceYMoveable properties to false
                ' specifies that the layout cannot move the shape in both X and Y directions
                Call Nevron.Nov.Diagram.Layout.NForceDirectedGraphLayout.SetXMoveable(fixedShapes(i), False)
                Call Nevron.Nov.Diagram.Layout.NForceDirectedGraphLayout.SetYMoveable(fixedShapes(i), False)
                activePage.Items.AddChild(fixedShapes(i))
            Next

            ' create the free vertices
            Dim freeShapes As Nevron.Nov.Diagram.NShape() = New Nevron.Nov.Diagram.NShape(freeCount - 1) {}

            For i As Integer = 0 To freeCount - 1
                freeShapes(i) = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle)
                freeShapes(CInt((i))).Geometry.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Horizontal, Nevron.Nov.Graphics.ENGradientVariant.Variant3, New Nevron.Nov.Graphics.NColor(192, 194, 194), New Nevron.Nov.Graphics.NColor(129, 133, 133))
                freeShapes(CInt((i))).Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(1, New Nevron.Nov.Graphics.NColor(68, 90, 108))
                activePage.Items.AddChild(freeShapes(i))
            Next

            ' link the fixed shapes in a circle
            For i As Integer = 0 To fixedCount - 1
                Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
                connector.MakeLine()
                activePage.Items.AddChild(connector)

                If i = 0 Then
                    connector.GlueBeginToShape(fixedShapes(fixedCount - 1))
                    connector.GlueEndToShape(fixedShapes(0))
                Else
                    connector.GlueBeginToShape(fixedShapes(i - 1))
                    connector.GlueEndToShape(fixedShapes(i))
                End If
            Next

            ' link each free shape with two different random fixed shapes
            Dim rnd As System.Random = New System.Random()

            For i As Integer = 0 To freeCount - 1
                Dim firstFixed As Integer = rnd.[Next](fixedCount)
                Dim secondFixed As Integer = (firstFixed + rnd.[Next](fixedCount / 3) + 1) Mod fixedCount

                ' link with first fixed
                Dim lineShape As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
                lineShape.MakeLine()
                activePage.Items.AddChild(lineShape)
                lineShape.GlueBeginToShape(freeShapes(i))
                lineShape.GlueEndToShape(fixedShapes(firstFixed))

                ' link with second fixed
                lineShape = New Nevron.Nov.Diagram.NRoutableConnector()
                lineShape.MakeLine()
                activePage.Items.AddChild(lineShape)
                lineShape.GlueBeginToShape(freeShapes(i))
                lineShape.GlueEndToShape(fixedShapes(secondFixed))
            Next

            ' link each free shape with another free shape
            For i As Integer = 1 To freeCount - 1
                Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
                connector.MakeLine()
                activePage.Items.AddChild(connector)
                connector.GlueBeginToShape(freeShapes(i - 1))
                connector.GlueEndToShape(freeShapes(i))
            Next

            ' send all edges to back
            Dim batchReorder As Nevron.Nov.Diagram.Batches.NBatchReorder = New Nevron.Nov.Diagram.Batches.NBatchReorder(Me.m_DrawingView.Document)
            batchReorder.Build(activePage.GetShapes(False, Nevron.Nov.Diagram.NDiagramFilters.ShapeType1D).CastAll(Of Nevron.Nov.Diagram.NDiagramItem)())
            batchReorder.SendToBack(activePage)

            ' arrange the elements
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub
        ''' <summary>
        ''' Creates a triangular grid diagram with the specified count of levels
        ''' </summary>
        ''' <paramname="levels"></param>
        Private Sub CreateTriangularGridDiagram(ByVal levels As Integer)
            ' clean up the active page
            Dim activePage As Nevron.Nov.Diagram.NPage = Me.m_DrawingView.ActivePage
            activePage.Items.Clear()

            ' we will be using basic circle shapes with default size of (30, 30)
            Dim basicShapesFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            basicShapesFactory.DefaultSize = New Nevron.Nov.Graphics.NSize(30, 30)
            Dim prev As Nevron.Nov.Diagram.NShape = Nothing
            Dim prevRowShapes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = Nothing

            For level As Integer = 1 To levels - 1
                Dim curRowShapes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape)()

                For i As Integer = 0 To level - 1
                    Dim cur As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle)
                    cur.Geometry.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Horizontal, Nevron.Nov.Graphics.ENGradientVariant.Variant3, New Nevron.Nov.Graphics.NColor(192, 194, 194), New Nevron.Nov.Graphics.NColor(129, 133, 133))
                    cur.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(1, New Nevron.Nov.Graphics.NColor(68, 90, 108))
                    activePage.Items.Add(cur)
                    Dim edge As Nevron.Nov.Diagram.NRoutableConnector
					' connect with prev
					If i > 0 Then
                        edge = New Nevron.Nov.Diagram.NRoutableConnector()
                        edge.MakeLine()
                        activePage.Items.Add(edge)
                        edge.GlueBeginToShape(prev)
                        edge.GlueEndToShape(cur)
                    End If

					' connect with ancestors
					If level > 1 Then
                        If i < prevRowShapes.Count Then
                            edge = New Nevron.Nov.Diagram.NRoutableConnector()
                            edge.MakeLine()
                            activePage.Items.Add(edge)
                            edge.GlueBeginToShape(prevRowShapes(i))
                            edge.GlueEndToShape(cur)
                        End If

                        If i > 0 Then
                            edge = New Nevron.Nov.Diagram.NRoutableConnector()
                            edge.MakeLine()
                            activePage.Items.Add(edge)
                            edge.GlueBeginToShape(prevRowShapes(i - 1))
                            edge.GlueEndToShape(cur)
                        End If
                    End If

                    ' fix the three corner vertices
                    If level = 1 OrElse (level = levels - 1 AndAlso (i = 0 OrElse i = level - 1)) Then
                        Call Nevron.Nov.Diagram.Layout.NForceDirectedGraphLayout.SetXMoveable(cur, False)
                        Call Nevron.Nov.Diagram.Layout.NForceDirectedGraphLayout.SetYMoveable(cur, False)
                        cur.Geometry.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Horizontal, Nevron.Nov.Graphics.ENGradientVariant.Variant3, New Nevron.Nov.Graphics.NColor(251, 203, 156), New Nevron.Nov.Graphics.NColor(247, 150, 56))
                        cur.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(1, New Nevron.Nov.Graphics.NColor(68, 90, 108))
                    End If

                    curRowShapes.Add(cur)
                    prev = cur
                Next

                prevRowShapes = curRowShapes
            Next

            ' send all edges to back
            Dim batchReorder As Nevron.Nov.Diagram.Batches.NBatchReorder = New Nevron.Nov.Diagram.Batches.NBatchReorder(Me.m_DrawingView.Document)
            batchReorder.Build(activePage.GetShapes(False, Nevron.Nov.Diagram.NDiagramFilters.ShapeType1D).CastAll(Of Nevron.Nov.Diagram.NDiagramItem)())
            batchReorder.SendToBack(activePage)

            ' arrange the elements
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub
        ''' <summary>
        ''' Arranges the shapes in the active page.
        ''' </summary>
        ''' <paramname="drawingDocument"></param>
        Private Sub ArrangeDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' get all top-level shapes that reside in the active page
            Dim activePage As Nevron.Nov.Diagram.NPage = drawingDocument.Content.ActivePage
            Dim shapes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = activePage.GetShapes(False)

            ' create a layout context and use it to arrange the shapes using the current layout
            Dim layoutContext As Nevron.Nov.Diagram.Layout.NDrawingLayoutContext = New Nevron.Nov.Diagram.Layout.NDrawingLayoutContext(drawingDocument, activePage)
            Me.m_Layout.Arrange(shapes.CastAll(Of Object)(), layoutContext)

            ' size the page to the content size
            activePage.SizeToContent()
        End Sub

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnLayoutChanged(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        Protected Overridable Sub OnArrangeButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_Layout As Nevron.Nov.Diagram.Layout.NBarycenterGraphLayout = New Nevron.Nov.Diagram.Layout.NBarycenterGraphLayout()

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NBarycenterGraphLayoutExample.
        ''' </summary>
        Public Shared ReadOnly NBarycenterGraphLayoutExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
