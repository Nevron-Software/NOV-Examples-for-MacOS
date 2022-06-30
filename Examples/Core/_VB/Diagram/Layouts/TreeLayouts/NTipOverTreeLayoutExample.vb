Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NTipOverTreeLayoutExample
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
            Nevron.Nov.Examples.Diagram.NTipOverTreeLayoutExample.NTipOverTreeLayoutExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NTipOverTreeLayoutExample), NExampleBaseSchema)
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
    The tip-over tree layout is a tree layout, which uses shapes provided data to determine
    whether the vertex children should be placed in a row, column or 2 columns. It produces
    orthogonal tree drawings. The layout satisfies to the following aesthetic criteria:
    <ul>
        <li><b>No edge crossings</b> - edges do not cross each other.</li>
        <li><b>No vertex-vertex overlaps</b> - vertices do not overlap each other.</li>
        <li><b>No vertex-edge overlaps</b> - vertices do not overlap edges.</li>
        <li><b>Compactness</b> - you can optimize the compactness of the drawing in both
            the breadth and depth dimensions of the tree by setting the <b>Compact</b>
            property to true.</li>
    </ul>
</p>
    You can change the way the children and the leafs are placed using the corresponding
    properties. You can also set the children placement for the children of each vertex
    individually using the <b>TipOverChildrenPlacement</b> property in the LayoutData
    collection of the shape.
<p>
    This type of layout is typically used in Org Charts, but can also be used in all cases
    where classical tree layouts (e.g. layouts with uniform parent placement) do not produce
    width/height ratio compact results.
</p>
<p>
    In this example the nodes whose children are arranged in cols are highlighted with orange.
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
            Dim activePage As Nevron.Nov.Diagram.NPage = drawingDocument.Content.ActivePage

            ' We will be using basic shapes with default size of 120, 60
            Dim basicShapesFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            basicShapesFactory.DefaultSize = New Nevron.Nov.Graphics.NSize(120, 60)

            ' Create the president
            Dim president As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            president.Text = "President"
            activePage.Items.Add(president)

            ' Create the VPs.
            ' NOTE: The child nodes of the VPs are layed out in cols
            Dim vpMarketing As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            vpMarketing.Text = "VP Marketing"
            vpMarketing.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(1, New Nevron.Nov.Graphics.NColor(68, 90, 108))
            activePage.Items.Add(vpMarketing)
            Dim vpSales As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            vpSales.Text = "VP Sales"
            vpSales.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(1, New Nevron.Nov.Graphics.NColor(68, 90, 108))
            activePage.Items.Add(vpSales)
            Dim vpProduction As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            vpProduction.Text = "VP Production"
            vpProduction.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(1, New Nevron.Nov.Graphics.NColor(68, 90, 108))
            activePage.Items.Add(vpProduction)

            ' Connect president with VP
            Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            activePage.Items.Add(connector)
            connector.GlueBeginToShape(president)
            connector.GlueEndToShape(vpMarketing)
            connector = New Nevron.Nov.Diagram.NRoutableConnector()
            activePage.Items.Add(connector)
            connector.GlueBeginToShape(president)
            connector.GlueEndToShape(vpSales)
            connector = New Nevron.Nov.Diagram.NRoutableConnector()
            activePage.Items.Add(connector)
            connector.GlueBeginToShape(president)
            connector.GlueEndToShape(vpProduction)

            ' Create the marketing managers
            Dim marketingManager1 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            marketingManager1.Text = "Manager1"
            activePage.Items.Add(marketingManager1)
            Dim marketingManager2 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            marketingManager2.Text = "Manager2"
            activePage.Items.Add(marketingManager2)

            ' Connect the marketing manager with the marketing VP
            connector = New Nevron.Nov.Diagram.NRoutableConnector()
            activePage.Items.Add(connector)
            connector.GlueBeginToShape(vpMarketing)
            connector.GlueEndToShape(marketingManager1)
            connector = New Nevron.Nov.Diagram.NRoutableConnector()
            activePage.Items.Add(connector)
            connector.GlueBeginToShape(vpMarketing)
            connector.GlueEndToShape(marketingManager2)

            ' Create the sales managers
            Dim salesManager1 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            salesManager1.Text = "Manager1"
            activePage.Items.Add(salesManager1)
            Dim salesManager2 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            salesManager2.Text = "Manager2"
            activePage.Items.Add(salesManager2)

            ' Connect the sales manager with the sales VP
            connector = New Nevron.Nov.Diagram.NRoutableConnector()
            activePage.Items.Add(connector)
            connector.GlueBeginToShape(vpSales)
            connector.GlueEndToShape(salesManager1)
            connector = New Nevron.Nov.Diagram.NRoutableConnector()
            activePage.Items.Add(connector)
            connector.GlueBeginToShape(vpSales)
            connector.GlueEndToShape(salesManager2)

            ' Create the production managers
            Dim productionManager1 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            productionManager1.Text = "Manager1"
            activePage.Items.Add(productionManager1)
            Dim productionManager2 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            productionManager2.Text = "Manager2"
            activePage.Items.Add(productionManager2)

            ' Connect the production manager with the production VP
            connector = New Nevron.Nov.Diagram.NRoutableConnector()
            activePage.Items.Add(connector)
            connector.GlueBeginToShape(vpProduction)
            connector.GlueEndToShape(productionManager1)
            connector = New Nevron.Nov.Diagram.NRoutableConnector()
            activePage.Items.Add(connector)
            connector.GlueBeginToShape(vpProduction)
            connector.GlueEndToShape(productionManager2)

            ' Arrange diagram
            Me.ArrangeDiagram(drawingDocument)

            ' Fit active page
            drawingDocument.Content.ActivePage.ZoomMode = Nevron.Nov.UI.ENZoomMode.Fit
        End Sub

        #EndRegion

		#Region"Implementation"

		Private Sub CreateTreeDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument, ByVal levels As Integer, ByVal branchNodes As Integer)
			' Create a random tree
			Dim tree As NGenericTreeTemplate = New NGenericTreeTemplate()
            tree.EdgesUserClass = "Connector"
            tree.Balanced = False
            tree.Levels = levels
            tree.BranchNodes = branchNodes
            tree.HorizontalSpacing = 10
            tree.VerticalSpacing = 10
            tree.VerticesShape = Nevron.Nov.Examples.Diagram.NTipOverTreeLayoutExample.VertexShape
            tree.VerticesSize = Nevron.Nov.Examples.Diagram.NTipOverTreeLayoutExample.VertexSize
            tree.VertexSizeDeviation = 1
            tree.Create(drawingDocument)

			' Randomly set the children placement of ten shapes to col
			Dim shapes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = drawingDocument.Content.ActivePage.GetShapes(False, Nevron.Nov.Diagram.NDiagramFilters.ShapeType2D)
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To shapes.Count / 2 - 1
                Dim index As Integer = random.[Next](shapes.Count)
                Dim shape As Nevron.Nov.Diagram.NShape = shapes(index)
                shape.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Orange)
                shape.LayoutData.TipOverChildrenPlacement = Nevron.Nov.Diagram.Layout.ENTipOverChildrenPlacement.ColRight
            Next
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

        Private Sub OnArrangeButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        Private Sub OnRandomTree1ButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_DrawingView.ActivePage.Items.Clear()

            ' create a random tree
			Me.CreateTreeDiagram(Me.m_DrawingView.Document, 6, 3)

            ' layout the tree
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        Private Sub OnRandomTree2ButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_DrawingView.ActivePage.Items.Clear()

            ' create a random tree
			Me.CreateTreeDiagram(Me.m_DrawingView.Document, 8, 2)

            ' layout the tree
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_Layout As Nevron.Nov.Diagram.Layout.NTipOverTreeLayout = New Nevron.Nov.Diagram.Layout.NTipOverTreeLayout()

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NTipOverTreeLayoutExample.
        ''' </summary>
        Public Shared ReadOnly NTipOverTreeLayoutExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Constants"

        Private Const VertexShape As Nevron.Nov.Diagram.Shapes.ENBasicShape = Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle
        Private Shared ReadOnly VertexSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(60, 60)

        #EndRegion
    End Class
End Namespace
