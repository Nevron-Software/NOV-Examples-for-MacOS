Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NOrthogonalGraphLayoutExample
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
            Nevron.Nov.Examples.Diagram.NOrthogonalGraphLayoutExample.NOrthogonalGraphLayoutExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NOrthogonalGraphLayoutExample), NExampleBaseSchema)
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

			' NOTE: For Graph layouts we provide the user with the ability to generate random graph diagrams so that he/she can test the layouts
			Dim randomGraph1Button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random Graph (10 vertices, 15 edges)")
            AddHandler randomGraph1Button.Click, AddressOf Me.OnRandomGraph1ButtonClick
            itemsStack.Add(randomGraph1Button)
            Dim randomGraph2Button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random Graph (20 vertices, 30 edges)")
            AddHandler randomGraph2Button.Click, AddressOf Me.OnRandomGraph2ButtonClick
            itemsStack.Add(randomGraph2Button)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Items", itemsStack))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    The orthogonal graph layout produces orthogonal graph drawings of all types of graphs
    (including those with self-loops and duplicate edges). It tries to compact the graph
    drawing area and also to minimize the number of edge crossings and bends.
</p>
<p>
	The most important properties are:
	<ul>
		<li>
			<b>CellSpacing</b> - determines the distance between 2 grid cells. For example if a grid
			cell is calculated to have a size of 100 x 100 and the CellSpacing property is set to
			10, then the cell size will be 120 x 120. Note that the node is always placed in the
			middle of the cell.
		</li>
		<li>
			<b>GridCellSizeMode</b> - this property is an enum with 2 possible values: GridCellSizeMode.
			GridBased and GridCellSizeMode.CellBased. If set to the first the maximal size of a
			node in the graph is determined and all cells are scaled to that size. More area
			efficient is the second value - it causes the dimensions of each column and row
			dimensions to be determined according to the size of the cells they contain.
		</li>
		<li>
			<b>Compact</b> - if set to true, a compaction algorithm will be applied to the embedded
			graph. This will decrease the total area of the drawing with 20 to 50 % (in the average
			case) at the cost of some additional time needed for the calculations.
		</li>
		<li>
			<b>PlugSpacing</b> - determines the spacing between the plugs of a node.
			You can set a fixed amount of spacing or a proportional spacing, which means that the plugs
			are distributed along the whole side of the node.
		</li>
	</ul>
</p>
<p>
	To experiment with the layout just change its properties from the property grid and click the <b>Layout</b> button. 
    To see the layout in action on a different graph, just click the <b>Random Graph</b> button. 
</p>
            "
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Const width As Double = 40
            Const height As Double = 40
            Const distance As Double = 80

			' Hide ports
			drawingDocument.Content.ScreenVisibility.ShowPorts = False
            Dim basicShapes As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim activePage As Nevron.Nov.Diagram.NPage = drawingDocument.Content.ActivePage
            Dim from As Integer() = New Integer() {1, 1, 1, 2, 2, 2, 3, 3, 4, 4, 4, 5, 5, 6}
            Dim [to] As Integer() = New Integer() {2, 3, 4, 4, 5, 8, 6, 7, 5, 8, 10, 8, 9, 10}
            Dim shapes As Nevron.Nov.Diagram.NShape() = New Nevron.Nov.Diagram.NShape(9) {}
            Dim vertexCount As Integer = shapes.Length
            Dim edgeCount As Integer = from.Length
            Dim count As Integer = vertexCount + edgeCount

            For i As Integer = 0 To count - 1

                If i < vertexCount Then
                    Dim j As Integer = If(vertexCount Mod 2 = 0, i, i + 1)
                    shapes(i) = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)

                    If vertexCount Mod 2 <> 0 AndAlso i = 0 Then
                        shapes(CInt((i))).SetBounds(New Nevron.Nov.Graphics.NRectangle((width + (distance * 1.5)) / 2, distance + (j / 2) * (distance * 1.5), width, height))
                    Else
                        shapes(CInt((i))).SetBounds(New Nevron.Nov.Graphics.NRectangle(width / 2 + (j Mod 2) * (distance * 1.5), height + (j / 2) * (distance * 1.5), width, height))
                    End If

                    activePage.Items.Add(shapes(i))
                Else
                    Dim edge As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
                    edge.UserClass = "Connector"
                    activePage.Items.Add(edge)
                    edge.GlueBeginToShape(shapes(from(i - vertexCount) - 1))
                    edge.GlueEndToShape(shapes([to](i - vertexCount) - 1))
                End If
            Next

            ' arrange diagram
            Me.ArrangeDiagram(drawingDocument)

            ' fit active page
            drawingDocument.Content.ActivePage.ZoomMode = Nevron.Nov.UI.ENZoomMode.Fit
        End Sub

		#EndRegion

		#Region"Implementation"

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

		Private Sub OnRandomGraph1ButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim drawingDocument As Nevron.Nov.Diagram.NDrawingDocument = Me.m_DrawingView.Document
            drawingDocument.StartHistoryTransaction("Create Random Graph 1")

            Try
                Me.m_DrawingView.ActivePage.Items.Clear()

				' create a test tree
				Dim graph As NRandomGraphTemplate = New NRandomGraphTemplate()
                graph.EdgesUserClass = "Connector"
                graph.VertexCount = 10
                graph.EdgeCount = 15
                graph.VerticesShape = Nevron.Nov.Examples.Diagram.NOrthogonalGraphLayoutExample.VertexShape
                graph.VerticesSize = Nevron.Nov.Examples.Diagram.NOrthogonalGraphLayoutExample.VertexSize
                graph.Create(drawingDocument)

				' layout the tree
				Me.ArrangeDiagram(drawingDocument)
            Finally
                drawingDocument.CommitHistoryTransaction()
            End Try
        End Sub

        Private Sub OnRandomGraph2ButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim drawingDocument As Nevron.Nov.Diagram.NDrawingDocument = Me.m_DrawingView.Document
            drawingDocument.StartHistoryTransaction("Create Random Graph 2")

            Try
                Me.m_DrawingView.ActivePage.Items.Clear()

				' create a test tree
				Dim graph As NRandomGraphTemplate = New NRandomGraphTemplate()
                graph.EdgesUserClass = "Connector"
                graph.VertexCount = 20
                graph.EdgeCount = 30
                graph.VerticesShape = Nevron.Nov.Examples.Diagram.NOrthogonalGraphLayoutExample.VertexShape
                graph.VerticesSize = Nevron.Nov.Examples.Diagram.NOrthogonalGraphLayoutExample.VertexSize
                graph.Create(drawingDocument)

				' layout the tree
				Me.ArrangeDiagram(drawingDocument)
            Finally
                drawingDocument.CommitHistoryTransaction()
            End Try
        End Sub

        Private Sub OnLayoutChanged(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        Protected Overridable Sub OnArrangeButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_Layout As Nevron.Nov.Diagram.Layout.NOrthogonalGraphLayout = New Nevron.Nov.Diagram.Layout.NOrthogonalGraphLayout()

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NOrthogonalGraphLayoutExample.
        ''' </summary>
        Public Shared ReadOnly NOrthogonalGraphLayoutExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Const VertexShape As Nevron.Nov.Diagram.Shapes.ENBasicShape = Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle
        Private Shared ReadOnly VertexSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(50, 50)

		#EndRegion
	End Class
End Namespace
