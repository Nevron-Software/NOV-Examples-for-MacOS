Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NBalloonTreeLayoutExample
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
            Nevron.Nov.Examples.Diagram.NBalloonTreeLayoutExample.NBalloonTreeLayoutExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NBalloonTreeLayoutExample), NExampleBaseSchema)
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

            ' NOTE: For Tree layouts we provide the user with the ability to generate random tree diagrams so that he/she can test the layouts
            Dim randomTree1Button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random Tree (max 6 levels, max 3 branch nodes)")
            AddHandler randomTree1Button.Click, AddressOf Me.OnRandomTree1ButtonClick
            itemsStack.Add(randomTree1Button)
            Dim randomTree2Button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random Tree (max 8 levels, max 2 branch nodes)")
            AddHandler randomTree2Button.Click, AddressOf Me.OnRandomTree2ButtonClick
            itemsStack.Add(randomTree2Button)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Items", itemsStack))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    The balloon tree layout tries to compact the drawing area of the tree 
    by placing the vertices in balloons around the tree root.
    It produces straight line tree drawings. 
</p>
<p>        
    Following is a brief description of its properties:
</p>
<ul>
	<li>
		<b>ParentChildSpacing</b> - the preferred spacing between a parent and a child
		vertex in the layout direction. The real spacing may be different for some nodes,
		because the layout does not allow overlapping.
	</li>
	<li>
		<b>VertexSpacing</b> - the minimal spacing between 2 nodes in the layout.
		If set to 0, the nodes may touch each other.
	</li>
	<li>
		<b>ChildWedge</b> - the sector angle (measured in degrees) for the children
		of each vertex.
	</li>
	<li>
		<b>RootWedge</b> - the sector angle (measured in degrees) for the children
		of the root vertex.
	</li>
	<li>
		<b>StartAngle</b> - the start angle for the children of the root vertex, measured in
		degrees anticlockwise from the x-axis.
	</li>
</ul>
<p>
	To experiment with the layout just change its properties from the property grid and click the <b>Layout</b> button.
</p>            
            "
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' Hide ports
            drawingDocument.Content.ScreenVisibility.ShowPorts = False

            ' Create a template graph
            Dim tree As NGenericTreeTemplate = New NGenericTreeTemplate()
            tree.EdgesUserClass = "Connector"
            tree.Levels = 4
            tree.BranchNodes = 4
            tree.HorizontalSpacing = 10
            tree.VerticalSpacing = 10
            tree.ConnectorType = Nevron.Nov.Diagram.Shapes.ENConnectorShape.RoutableConnector
            tree.VerticesShape = Nevron.Nov.Examples.Diagram.NBalloonTreeLayoutExample.VertexShape
            tree.VerticesSize = Nevron.Nov.Examples.Diagram.NBalloonTreeLayoutExample.VertexSize
            tree.Create(drawingDocument)

            ' Arrange diagram
            Me.ArrangeDiagram(drawingDocument)

            ' Fit active page
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

        Private Sub OnRandomTree1ButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim drawingDocument As Nevron.Nov.Diagram.NDrawingDocument = Me.m_DrawingView.Document
            drawingDocument.StartHistoryTransaction("Create Random Tree 1")

            Try
                drawingDocument.Content.ActivePage.Items.Clear()

                ' create a random tree
                Dim tree As NGenericTreeTemplate = New NGenericTreeTemplate()
                tree.EdgesUserClass = "Connector"
                tree.Levels = 6
                tree.BranchNodes = 3
                tree.HorizontalSpacing = 10
                tree.VerticalSpacing = 10
                tree.VerticesShape = Nevron.Nov.Examples.Diagram.NBalloonTreeLayoutExample.VertexShape
                tree.VerticesSize = Nevron.Nov.Examples.Diagram.NBalloonTreeLayoutExample.VertexSize
                tree.Balanced = True
                tree.VertexSizeDeviation = 0
                tree.Create(drawingDocument)

                ' layout the tree
                Me.ArrangeDiagram(drawingDocument)
            Finally
                drawingDocument.CommitHistoryTransaction()
            End Try
        End Sub

        Private Sub OnRandomTree2ButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim drawingDocument As Nevron.Nov.Diagram.NDrawingDocument = Me.m_DrawingView.Document
            drawingDocument.StartHistoryTransaction("Create Random Tree 2")

            Try
                drawingDocument.Content.ActivePage.Items.Clear()

                ' create a random tree
                Dim tree As NGenericTreeTemplate = New NGenericTreeTemplate()
                tree.EdgesUserClass = "Connector"
                tree.Levels = 8
                tree.BranchNodes = 2
                tree.HorizontalSpacing = 10
                tree.VerticalSpacing = 10
                tree.VerticesShape = Nevron.Nov.Examples.Diagram.NBalloonTreeLayoutExample.VertexShape
                tree.VerticesSize = Nevron.Nov.Examples.Diagram.NBalloonTreeLayoutExample.VertexSize
                tree.Balanced = True
                tree.VertexSizeDeviation = 0
                tree.Create(drawingDocument)

                ' layout the tree
                Me.ArrangeDiagram(drawingDocument)
            Finally
                drawingDocument.CommitHistoryTransaction()
            End Try
        End Sub

        Private Sub OnLayoutChanged(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        Private Sub OnArrangeButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_Layout As Nevron.Nov.Diagram.Layout.NBalloonTreeLayout = New Nevron.Nov.Diagram.Layout.NBalloonTreeLayout()

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NBalloonTreeLayoutExample.
        ''' </summary>
        Public Shared ReadOnly NBalloonTreeLayoutExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Constants"

        Private Const VertexShape As Nevron.Nov.Diagram.Shapes.ENBasicShape = Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle
        Private Shared ReadOnly VertexSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(60, 60)

        #EndRegion
    End Class
End Namespace
