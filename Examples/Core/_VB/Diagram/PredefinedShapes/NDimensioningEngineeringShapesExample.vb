Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NDimensioningEngineeringShapesExample
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
            Nevron.Nov.Examples.Diagram.NDimensioningEngineeringShapesExample.NDimensioningEngineeringShapesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NDimensioningEngineeringShapesExample), NExampleBaseSchema)
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
    This example demonstrates the dimensioning engineering shapes, which are created by the NDimensioningEngineeringShapeFactory.
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Const XStep As Double = 150
            Const YStep As Double = 200
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

			' Hide grid and ports
			drawing.ScreenVisibility.ShowGrid = False
            drawing.ScreenVisibility.ShowPorts = False

			' create all shapes
			Dim factory As Nevron.Nov.Diagram.Shapes.NDimensioningEngineeringShapeFactory = New Nevron.Nov.Diagram.Shapes.NDimensioningEngineeringShapeFactory()
            factory.DefaultSize = New Nevron.Nov.Graphics.NSize(90, 90)
            Dim x As Double = 0
            Dim y As Double = 0

            For i As Integer = 0 To factory.ShapeCount - 1
                Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(i)
                shape.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                shape.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                shape.Tooltip = New Nevron.Nov.UI.NTooltip(factory.GetShapeInfo(CInt((i))).Name)
                activePage.Items.Add(shape)

                If shape.ShapeType = Nevron.Nov.Diagram.ENShapeType.Shape1D Then
                    Dim shapeType As Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes = CType(i, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes)

                    Select Case shapeType
                        Case Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.VerticalBaseline, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.Vertical, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.VerticalOutside, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.OrdinateVertical, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.OrdinateVerticalMultiple
                            shape.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(x + shape.Width, y + shape.Height))
                            shape.SetEndPoint(New Nevron.Nov.Graphics.NPoint(x + shape.Width, y))
                        Case Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.OrdinateHorizontalMultiple, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.OrdinateHorizontal
                            shape.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(x, y))
                            shape.SetEndPoint(New Nevron.Nov.Graphics.NPoint(x + shape.Width, y))
                        Case Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.Radius, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.RadiusOutside, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.ArcRadius, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.Diameter, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.DiameterOutside
                            shape.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(x, y + shape.Height / 2))
                            shape.SetEndPoint(New Nevron.Nov.Graphics.NPoint(x + shape.Width, y - shape.Height / 2))
                        Case Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.AngleCenter, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.AngleEven, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.AngleOutside, Nevron.Nov.Diagram.Shapes.ENDimensioningEngineeringShapes.AngleUneven
                            shape.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(x, y + shape.Width / 2))
                            shape.SetEndPoint(New Nevron.Nov.Graphics.NPoint(x + shape.Width, y + shape.Width / 2))
                        Case Else
                            shape.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(x, y))
                            shape.SetEndPoint(New Nevron.Nov.Graphics.NPoint(x + shape.Width, y + shape.Height))
                    End Select
                Else
                    shape.SetBounds(x, y, shape.Width, shape.Height)
                    shape.LocPinY = 1
                End If

                x += XStep

                If x > activePage.Width Then
                    x = 0
                    y += YStep
                End If
            Next

			' size page to content
			activePage.Layout.ContentPadding = New Nevron.Nov.Graphics.NMargins(50)
            activePage.SizeToContent()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NDimensioningEngineeringShapesExample.
		''' </summary>
		Public Shared ReadOnly NDimensioningEngineeringShapesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
