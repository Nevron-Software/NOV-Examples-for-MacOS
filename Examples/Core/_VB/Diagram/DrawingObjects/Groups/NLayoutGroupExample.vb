Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NLayoutGroupExample
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
            Nevron.Nov.Examples.Diagram.NLayoutGroupExample.NLayoutGroupExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NLayoutGroupExample), NExampleBaseSchema)
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
            Return "Demonstrates how to layout the shapes in a group."
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
			' Create a group
			Dim group As Nevron.Nov.Diagram.NGroup = New Nevron.Nov.Diagram.NGroup()
            drawingDocument.Content.ActivePage.Items.Add(group)
            group.PinX = 300
            group.PinY = 300

			' Create some shapes and add them to the group
			Dim factory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim root As Nevron.Nov.Diagram.NShape = factory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            root.Text = "Root"
            group.Shapes.Add(root)
            Dim shape1 As Nevron.Nov.Diagram.NShape = factory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle)
            shape1.Text = "Circle 1"
            group.Shapes.Add(shape1)
            Dim shape2 As Nevron.Nov.Diagram.NShape = factory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle)
            shape2.Text = "Circle 2"
            group.Shapes.Add(shape2)
            Dim shape3 As Nevron.Nov.Diagram.NShape = factory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle)
            shape3.Text = "Circle 3"
            group.Shapes.Add(shape3)

			' Connect the shapes
			Me.ConnectShapes(root, shape1)
            Me.ConnectShapes(root, shape2)
            Me.ConnectShapes(root, shape3)

			' Update group bounds
			group.UpdateBounds()

			' Create a layout context and configure the area you want the group to be arranged in.
			' The layout area is in page coordinates
			Dim layoutContext As Nevron.Nov.Diagram.Layout.NDrawingLayoutContext = New Nevron.Nov.Diagram.Layout.NDrawingLayoutContext(drawingDocument, group)
            layoutContext.LayoutArea = New Nevron.Nov.Graphics.NRectangle(100, 100, 200, 200)

			' Layout the shapes in the group
			Dim layout As Nevron.Nov.Diagram.Layout.NRadialGraphLayout = New Nevron.Nov.Diagram.Layout.NRadialGraphLayout()
            layout.Arrange(group.Shapes.ToArray(), layoutContext)

			' Update the group bounds
			group.UpdateBounds()
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub ConnectShapes(ByVal shape1 As Nevron.Nov.Diagram.NShape, ByVal shape2 As Nevron.Nov.Diagram.NShape)
            Dim group As Nevron.Nov.Diagram.NGroup = shape1.OwnerGroup
            Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            connector.UserClass = Nevron.Nov.Diagram.NDR.StyleSheetNameConnectors
            group.Shapes.Add(connector)
            connector.GlueBeginToShape(shape1)
            connector.GlueEndToShape(shape2)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NLayoutGroupExample.
		''' </summary>
		Public Shared ReadOnly NLayoutGroupExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
