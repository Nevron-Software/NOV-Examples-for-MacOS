Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NConnectorWithMultipleLabelsExample
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
            Nevron.Nov.Examples.Diagram.NConnectorWithMultipleLabelsExample.NConnectorWithMultipleLabelsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NConnectorWithMultipleLabelsExample), NExampleBaseSchema)
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
<p>This example demonstrates how to create a connector with multiple labels. This can be done by adding
outward ports to a connector and then gluing shapes that only have text to these outward ports.</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

			' 1. Create some shape factories
			Dim basicShapesFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim connectorShapesFactory As Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory()

			' 2. Create and add some shapes
			Dim shape1 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            shape1.SetBounds(New Nevron.Nov.Graphics.NRectangle(50, 50, 100, 100))
            activePage.Items.Add(shape1)
            Dim shape2 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            shape2.SetBounds(New Nevron.Nov.Graphics.NRectangle(400, 50, 100, 100))
            activePage.Items.Add(shape2)

			' 3. Connect the shapes
			Dim connector As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
            activePage.Items.Add(connector)
            connector.GlueBeginToShape(shape1)
            connector.GlueEndToShape(shape2)

			' Add 2 outward ports to the connector
			Dim port1 As Nevron.Nov.Diagram.NPort = New Nevron.Nov.Diagram.NPort(0.3, 0.3, True)
            port1.GlueMode = Nevron.Nov.Diagram.ENPortGlueMode.Outward
            connector.Ports.Add(port1)
            Dim port2 As Nevron.Nov.Diagram.NPort = New Nevron.Nov.Diagram.NPort(0.7, 0.7, True)
            port2.GlueMode = Nevron.Nov.Diagram.ENPortGlueMode.Outward
            connector.Ports.Add(port2)

			' Attach label shapes to the outward ports of the connector
			Dim labelShape1 As Nevron.Nov.Diagram.NShape = Me.CreateLabelShape("Label 1")
            activePage.Items.Add(labelShape1)
            labelShape1.GlueMasterPortToPort(labelShape1.Ports(0), port1)
            Dim labelShape2 As Nevron.Nov.Diagram.NShape = Me.CreateLabelShape("Label 2")
            activePage.Items.Add(labelShape2)
            labelShape2.GlueMasterPortToPort(labelShape2.Ports(0), port2)
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Function CreateLabelShape(ByVal text As String) As Nevron.Nov.Diagram.NShape
            Dim labelShape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()

			' Configure shape
			labelShape.SetBounds(0, 0, 100, 30)
            labelShape.SetProtectionMask(Nevron.Nov.Diagram.ENDiagramItemOperationMask.All)
            labelShape.CanSplit = False
            labelShape.GraphPart = False
            labelShape.RouteThroughHorizontally = True
            labelShape.RouteThroughVertically = True

			' Set text and make text block resize to text
			labelShape.Text = text
            CType(labelShape.TextBlock, Nevron.Nov.Diagram.NTextBlock).ResizeMode = Nevron.Nov.Diagram.ENTextBlockResizeMode.TextSize

			' Set text background and border
			labelShape.TextBlock.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            labelShape.TextBlock.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            labelShape.TextBlock.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            labelShape.TextBlock.Padding = New Nevron.Nov.Graphics.NMargins(2)

			' Add a port to the shape
			labelShape.Ports.Add(New Nevron.Nov.Diagram.NPort(0.5, 0.5, True))
            Return labelShape
        End Function

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NConnectorWithMultipleLabelsExample.
		''' </summary>
		Public Shared ReadOnly NConnectorWithMultipleLabelsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
