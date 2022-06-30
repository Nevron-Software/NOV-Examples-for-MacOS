Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Expressions
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NDrawingToolShapesExample
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
            Nevron.Nov.Examples.Diagram.NDrawingToolShapesExample.NDrawingToolShapesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NDrawingToolShapesExample), NExampleBaseSchema)
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
    This example demonstrates the drawing tool shapes, which are created by the NDrawingToolShapeFactory.
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
			Dim factory As Nevron.Nov.Diagram.Shapes.NDrawingToolShapeFactory = New Nevron.Nov.Diagram.Shapes.NDrawingToolShapeFactory()
            factory.DefaultSize = New Nevron.Nov.Graphics.NSize(90, 90)
            Dim x As Double = 0
            Dim y As Double = 0

            For i As Integer = 0 To factory.ShapeCount - 1
                Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(i)
                shape.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                shape.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                shape.Tooltip = New Nevron.Nov.UI.NTooltip(factory.GetShapeInfo(CInt((i))).Name)

                If i <> CInt(Nevron.Nov.Diagram.Shapes.ENDrawingToolShapes.SectorNumeric) AndAlso i <> CInt(Nevron.Nov.Diagram.Shapes.ENDrawingToolShapes.ArcNumeric) AndAlso i <> CInt(Nevron.Nov.Diagram.Shapes.ENDrawingToolShapes.RightTriangle) Then
                    shape.Text = factory.GetShapeInfo(CInt((i))).Name
                    Me.MoveTextBelowShape(shape)
                End If

                activePage.Items.Add(shape)

                If shape.ShapeType = Nevron.Nov.Diagram.ENShapeType.Shape1D Then
                    If i = CInt(Nevron.Nov.Diagram.Shapes.ENDrawingToolShapes.CircleRadius) Then
                        shape.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(x + shape.Width / 2, y))
                    Else
                        shape.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(x, y))
                    End If

                    Dim width As Double = shape.Width

                    If i = CInt(Nevron.Nov.Diagram.Shapes.ENDrawingToolShapes.MultigonEdge) Then
                        width = 90
                    ElseIf i = CInt(Nevron.Nov.Diagram.Shapes.ENDrawingToolShapes.MultigonCenter) Then
                        width = 30
                    End If

                    shape.SetEndPoint(New Nevron.Nov.Graphics.NPoint(x + width, y + shape.Height))
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

        Private Sub MoveTextBelowShape(ByVal shape As Nevron.Nov.Diagram.NShape)
            If shape.ShapeType = Nevron.Nov.Diagram.ENShapeType.Shape1D Then
                Dim textBlock As Nevron.Nov.Diagram.NTextBlock = CType(shape.TextBlock, Nevron.Nov.Diagram.NTextBlock)
                textBlock.Padding = New Nevron.Nov.Graphics.NMargins(0, 5, 0, 0)
                textBlock.ResizeMode = Nevron.Nov.Diagram.ENTextBlockResizeMode.TextSize
                textBlock.SetFx(Nevron.Nov.Diagram.NTextBlock.PinYProperty, New Nevron.Nov.Diagram.Expressions.NShapeHeightFactorFx(1.0))
                textBlock.LocPinY = -2
            Else
                shape.MoveTextBlockBelowShape()
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NBasicShapesExample.
		''' </summary>
		Public Shared ReadOnly NDrawingToolShapesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
