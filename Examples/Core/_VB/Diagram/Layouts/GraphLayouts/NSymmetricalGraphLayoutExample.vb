Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NSymmetricalGraphLayoutExample
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
            Nevron.Nov.Examples.Diagram.NSymmetricalGraphLayoutExample.NSymmetricalGraphLayoutExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NSymmetricalGraphLayoutExample), NExampleBaseSchema)
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
    The symmetrical layout represents an implementation of the Fruchertman and Reingold force directed layout (with some modifications).
</p>
<p>
    It uses attractive and repulsive forces, which aim to produce a drawing with uniform distance between each set of connected vertices. Because of that the drawing tends to be symmetrical.
</p>
<p>
	The attractive and repulsive forces are coupled in an instance of the <b>NDesiredDistanceForce</b> class, accessible from the <b>DesiredDistanceForce</b> property. 
</p>
<p>
	To experiment with the layout behavior just change the properties of the layout in the property grid and click the <b>Layout</b> button.
</p>
            "
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
			' Hide ports
			drawingDocument.Content.ScreenVisibility.ShowPorts = False

			' Create random diagram
			Dim template As NGenericTreeTemplate = New NGenericTreeTemplate()
            template.Balanced = False
            template.Levels = 6
            template.BranchNodes = 3
            template.HorizontalSpacing = 10
            template.VerticalSpacing = 10
            template.VerticesShape = Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle
            template.VerticesSize = New Nevron.Nov.Graphics.NSize(50, 50)
            template.VertexSizeDeviation = 0
            template.Create(drawingDocument)

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
                graph.VerticesShape = Nevron.Nov.Examples.Diagram.NSymmetricalGraphLayoutExample.VertexShape
                graph.VerticesSize = Nevron.Nov.Examples.Diagram.NSymmetricalGraphLayoutExample.VertexSize
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
                graph.VerticesShape = Nevron.Nov.Examples.Diagram.NSymmetricalGraphLayoutExample.VertexShape
                graph.VerticesSize = Nevron.Nov.Examples.Diagram.NSymmetricalGraphLayoutExample.VertexSize
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
        Private m_Layout As Nevron.Nov.Diagram.Layout.NSymmetricalGraphLayout = New Nevron.Nov.Diagram.Layout.NSymmetricalGraphLayout()

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NSymmetricalGraphLayoutExample.
        ''' </summary>
        Public Shared ReadOnly NSymmetricalGraphLayoutExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Const VertexShape As Nevron.Nov.Diagram.Shapes.ENBasicShape = Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle
        Private Shared ReadOnly VertexSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(50, 50)

		#EndRegion
	End Class
End Namespace
