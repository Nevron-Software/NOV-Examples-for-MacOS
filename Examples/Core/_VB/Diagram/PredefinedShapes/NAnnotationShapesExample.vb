Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Expressions
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NAnnotationShapesExample
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
            Nevron.Nov.Examples.Diagram.NAnnotationShapesExample.NAnnotationShapesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NAnnotationShapesExample), NExampleBaseSchema)
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
    This example demonstrates the annotation shapes, which are created by the NAnnotationShapeFactory.
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

			' Hide grid and ports
			drawing.ScreenVisibility.ShowGrid = False
            drawing.ScreenVisibility.ShowPorts = False

			' Create all shapes
			Dim factory As Nevron.Nov.Diagram.Shapes.NAnnotationShapeFactory = New Nevron.Nov.Diagram.Shapes.NAnnotationShapeFactory()
            factory.DefaultSize = New Nevron.Nov.Graphics.NSize(60, 60)
            Dim row As Integer = 0, col As Integer = 0
            Dim cellWidth As Double = 0' 180;
            Dim cellHeight As Double = 150
            Dim is1D As Boolean = False
            Dim i As Integer = 0

            While i < factory.ShapeCount
                Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(i)
                Dim tempShape As Nevron.Nov.Diagram.NShape
                shape.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                shape.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center

                If i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.Text) OrElse i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.FiveRuledColumn) OrElse i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.InfoLine) OrElse i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.NorthArrow5) OrElse i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.NoteSymbol) OrElse i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.ReferenceTriangle) OrElse i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.ReferenceRectangle) OrElse i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.ReferenceHexagon) OrElse i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.ReferenceCircle) OrElse i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.ReferenceOval) Then
                    Dim group As Nevron.Nov.Diagram.NGroup = New Nevron.Nov.Diagram.NGroup()
                    group.Width = shape.Width
                    group.Height = shape.Height

                    If i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.Text) Then
                        shape.PinX = 0
                        shape.SetFx(Nevron.Nov.Diagram.NShape.PinYProperty, "Height")
                    Else
                        shape.SetFx(Nevron.Nov.Diagram.NShape.PinXProperty, "Width / 2")
                        shape.SetFx(Nevron.Nov.Diagram.NShape.PinYProperty, "Height / 2")
                    End If

                    group.TextBlock = New Nevron.Nov.Diagram.NTextBlock(factory.GetShapeInfo(CInt((i))).Name)
                    shape.SetFx(Nevron.Nov.Diagram.NShape.WidthProperty, "$ParentSheet.Width")
                    shape.SetFx(Nevron.Nov.Diagram.NShape.HeightProperty, "$ParentSheet.Height")
                    Me.MoveTextBelowShape(group)
                    group.Shapes.Add(shape)
                    activePage.Items.Add(group)
                    tempShape = group
                Else

                    If i <> CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.Benchmark) Then
                        shape.Text = factory.GetShapeInfo(CInt((i))).Name
                        Me.MoveTextBelowShape(shape)

                        If i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.ReferenceCallout1) Then
                            shape.TextBlock.PinX = 40
                            shape.TextBlock.PinY = 10
                        End If

                        If i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.ReferenceCallout2) Then
                            shape.TextBlock.Angle = New Nevron.Nov.NAngle(0)
                            shape.TextBlock.PinY = 100
                        End If
                    End If

                    activePage.Items.Add(shape)
                    tempShape = shape
                End If

                If col >= 5 Then
                    row += 1
                    col = 0
                    cellWidth = 0
                    is1D = False
                End If

                Dim widthGap As Integer = If(is1D, 150, 100)
                is1D = shape.ShapeType = Nevron.Nov.Diagram.ENShapeType.Shape1D
                Dim beginPoint As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(widthGap + cellWidth, 50 + row * cellHeight)

                If is1D Then
                    Dim endPoint As Nevron.Nov.Graphics.NPoint = beginPoint + New Nevron.Nov.Graphics.NPoint(0, cellHeight - 60)

                    If i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.ReferenceCallout1) OrElse i = CInt(Nevron.Nov.Diagram.Shapes.ENAnnotationShape.ReferenceCallout2) Then
                        tempShape.SetBeginPoint(beginPoint)
                        tempShape.SetEndPoint(endPoint)
                    Else
                        tempShape.SetBeginPoint(endPoint)
                        tempShape.SetEndPoint(beginPoint)
                    End If
                Else
                    tempShape.SetBounds(beginPoint.X, beginPoint.Y, shape.Width, shape.Height)
                End If

                cellWidth += widthGap + tempShape.Width
                i += 1
                col += 1
            End While

			' size page to content
			activePage.Layout.ContentPadding = New Nevron.Nov.Graphics.NMargins(40)
            activePage.SizeToContent()
        End Sub

        Private Sub MoveTextBelowShape(ByVal shape As Nevron.Nov.Diagram.NShape)
            If shape.ShapeType = Nevron.Nov.Diagram.ENShapeType.Shape2D Then
                shape.MoveTextBlockBelowShape()
                Return
            End If

			' if the shape is 1D put the text block on the left part of the shape and rotate it on 90 degrees.
			Dim textBlock As Nevron.Nov.Diagram.NTextBlock = shape.GetTextBlock()
            textBlock.Padding = New Nevron.Nov.Graphics.NMargins(5, 0, 0, 0)
            textBlock.ResizeMode = Nevron.Nov.Diagram.ENTextBlockResizeMode.TextSize
            textBlock.PinX = shape.BeginX
            textBlock.SetFx(Nevron.Nov.Diagram.NTextBlock.PinYProperty, New Nevron.Nov.Diagram.Expressions.NShapeHeightFactorFx(0))
            textBlock.LocPinY = 0
            textBlock.Angle = New Nevron.Nov.NAngle(90)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NAnnotationShapesExample.
		''' </summary>
		Public Shared ReadOnly NAnnotationShapesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
