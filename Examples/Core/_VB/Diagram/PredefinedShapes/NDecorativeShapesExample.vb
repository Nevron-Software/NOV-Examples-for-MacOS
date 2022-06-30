Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NDecorativeShapesExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor.
		''' </summary>
		Public Sub New()
        End Sub

		''' <summary>
		''' Static constructor
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Diagram.NDecorativeShapesExample.NDecorativeShapesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NDecorativeShapesExample), NExampleBaseSchema)
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
    This example demonstrates the decorative shapes, which are created by the NDecorativeShapeFactory.
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

			' Hide grid and ports
			drawing.ScreenVisibility.ShowGrid = False
            drawing.ScreenVisibility.ShowPorts = False

			' create all shapes
			Dim factory As Nevron.Nov.Diagram.Shapes.NDecorativeShapeFactory = New Nevron.Nov.Diagram.Shapes.NDecorativeShapeFactory()
            factory.DefaultSize = New Nevron.Nov.Graphics.NSize(80, 60)

            For i As Integer = 0 To factory.ShapeCount - 1
                Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(i)
                shape.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                shape.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                shape.Text = factory.GetShapeInfo(CInt((i))).Name
                shape.MoveTextBlockBelowShape()
                activePage.Items.Add(shape)
            Next

			' arrange them
			Dim shapes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = activePage.GetShapes(False)
            Dim layoutContext As Nevron.Nov.Layout.NLayoutContext = New Nevron.Nov.Layout.NLayoutContext()
            layoutContext.BodyAdapter = New Nevron.Nov.Diagram.Layout.NShapeBodyAdapter(drawingDocument)
            layoutContext.GraphAdapter = New Nevron.Nov.Diagram.Layout.NShapeGraphAdapter()
            layoutContext.LayoutArea = activePage.Bounds
            Dim flowLayout As Nevron.Nov.Layout.NTableFlowLayout = New Nevron.Nov.Layout.NTableFlowLayout()
            flowLayout.HorizontalSpacing = 30
            flowLayout.VerticalSpacing = 50
            flowLayout.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            flowLayout.MaxOrdinal = 5
            flowLayout.Arrange(shapes.CastAll(Of Object)(), layoutContext)

			' size page to content
			activePage.Layout.ContentPadding = New Nevron.Nov.Graphics.NMargins(40)
            activePage.SizeToContent()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NDecorativeShapesExample
		''' </summary>
		Public Shared ReadOnly NDecorativeShapesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
