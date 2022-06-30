Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NCompactDepthTreeLayoutExample
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
            Nevron.Nov.Examples.Diagram.NCompactDepthTreeLayoutExample.NCompactDepthTreeLayoutExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NCompactDepthTreeLayoutExample), NExampleBaseSchema)
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
    The compact depth tree layout represents a classical directed tree layout 
    (e.g. with uniform parent placement), which compacts the depth of the tree drawing area. 
    It produces both straight line and orthogonal tree drawings, which is controlled by the <b>OrthogonalEdgeRouting</b> property.    
	The <b>PlugSpacing</b> property controls the spacing between the plugs of a node.
	You can set a fixed amount of spacing or a proportional spacing, which means that the plugs
	are distributed along the whole side of the node.
    The layout satisfies to the following aesthetic criteria:
    <ul>
        <li>No edge crossings - edges do not cross each other.</li>
        <li>No vertex-vertex overlaps - vertices do not overlap each other.</li>
        <li>No vertex-edge overlaps - vertices do not overlap edges.</li>
        <li>Compactness - you can optimize the compactness of the drawing in the tree breadth dimension 
        by setting the <b>CompactBreadth</b> property to true. This type of layout is by default depth compact.</li>
    </ul>
</p>    
<p>
    This layout is very useful when arranging deep, unbalanced trees with different node sizes 
    (class diagrams being a perfect example). In cases like these the layout guarantees 
    that the drawing is with minimal depth.
</p>
<p>
	To experiment with the layout just change its properties from the property grid and click the <b>Layout</b> button. 
    To see the layout in action on a different trees, just click the <b>Random Tree</b> button. 
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' Hide ports
            drawingDocument.Content.ScreenVisibility.ShowPorts = False

            ' Create a random diagram 
            Dim template As NGenericTreeTemplate = New NGenericTreeTemplate()
            template.EdgesUserClass = "Connector"
            template.Balanced = False
            template.Levels = 6
            template.BranchNodes = 3
            template.HorizontalSpacing = 10
            template.VerticalSpacing = 10
            template.VerticesSize = New Nevron.Nov.Graphics.NSize(50, 50)
            template.VertexSizeDeviation = 1
            template.Create(drawingDocument)

            ' Arrange the diagram
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
                tree.VerticesShape = Nevron.Nov.Examples.Diagram.NCompactDepthTreeLayoutExample.VertexShape
                tree.VerticesSize = Nevron.Nov.Examples.Diagram.NCompactDepthTreeLayoutExample.VertexSize
                tree.Balanced = False
                tree.VertexSizeDeviation = 1
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
                tree.VerticesShape = Nevron.Nov.Examples.Diagram.NCompactDepthTreeLayoutExample.VertexShape
                tree.VerticesSize = Nevron.Nov.Examples.Diagram.NCompactDepthTreeLayoutExample.VertexSize
                tree.Balanced = False
                tree.VertexSizeDeviation = 1
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
        Private m_Layout As Nevron.Nov.Diagram.Layout.NCompactDepthTreeLayout = New Nevron.Nov.Diagram.Layout.NCompactDepthTreeLayout()

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NCompactDepthTreeLayoutExample.
        ''' </summary>
        Public Shared ReadOnly NCompactDepthTreeLayoutExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Constants"

        Private Const VertexShape As Nevron.Nov.Diagram.Shapes.ENBasicShape = Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle
        Private Shared ReadOnly VertexSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(60, 60)

        #EndRegion
    End Class
End Namespace
