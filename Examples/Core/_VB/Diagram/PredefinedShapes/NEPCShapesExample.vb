Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NEPCShapesExample
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
            Nevron.Nov.Examples.Diagram.NEPCShapesExample.NEPCShapesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NEPCShapesExample), NExampleBaseSchema)
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
    This example demonstrates the EPC shapes, which are created by the NEpcShapeFactory.
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
			Dim factory As Nevron.Nov.Diagram.Shapes.NEPCDiagramShapesFactory = New Nevron.Nov.Diagram.Shapes.NEPCDiagramShapesFactory()
            factory.DefaultSize = New Nevron.Nov.Graphics.NSize(80, 60)
            Dim row As Integer = 0, col As Integer = 0
            Dim cellWidth As Double = 180
            Dim cellHeight As Double = 120
            Dim i As Integer = 0

            While i < factory.ShapeCount
                Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(i)
                Dim tempShape As Nevron.Nov.Diagram.NShape
                shape.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                shape.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center

                If i = CInt(Nevron.Nov.Diagram.Shapes.ENEpcShape.[AND]) OrElse i = CInt(Nevron.Nov.Diagram.Shapes.ENEpcShape.[OR]) OrElse i = CInt(Nevron.Nov.Diagram.Shapes.ENEpcShape.[XOR]) Then
                    Dim group As Nevron.Nov.Diagram.NGroup = New Nevron.Nov.Diagram.NGroup()
                    group.Width = shape.Width
                    group.Height = shape.Height
                    group.Shapes.Add(shape)
                    group.TextBlock = New Nevron.Nov.Diagram.NTextBlock(factory.GetShapeInfo(CInt((i))).Name)
                    shape.SetFx(Nevron.Nov.Diagram.NShape.PinXProperty, "Width / 2")
                    shape.SetFx(Nevron.Nov.Diagram.NShape.PinYProperty, "Height / 2")
                    shape.SetFx(Nevron.Nov.Diagram.NShape.WidthProperty, "$ParentSheet.Width")
                    shape.SetFx(Nevron.Nov.Diagram.NShape.HeightProperty, "$ParentSheet.Height")
                    group.MoveTextBlockBelowShape()
                    activePage.Items.Add(group)
                    tempShape = group
                Else
                    shape.Text = factory.GetShapeInfo(CInt((i))).Name

                    If i = CInt(Nevron.Nov.Diagram.Shapes.ENEpcShape.InformationMaterial) Then
                        shape.TextBlock.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black)
                    End If

                    shape.MoveTextBlockBelowShape()
                    activePage.Items.Add(shape)
                    tempShape = shape
                End If

                If col >= 4 Then
                    row += 1
                    col = 0
                End If

                Dim beginPoint As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(50 + col * cellWidth, 50 + row * cellHeight)

                If shape.ShapeType = Nevron.Nov.Diagram.ENShapeType.Shape1D Then
                    Dim endPoint As Nevron.Nov.Graphics.NPoint = beginPoint + New Nevron.Nov.Graphics.NPoint(cellWidth - 100, cellHeight - 100)
                    tempShape.SetBeginPoint(beginPoint)
                    tempShape.SetEndPoint(endPoint)
                Else
                    tempShape.SetBounds(beginPoint.X, beginPoint.Y, shape.Width, shape.Height)
                End If

                i += 1
                col += 1
            End While

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
		''' Schema associated with NEPCShapesExample.
		''' </summary>
		Public Shared ReadOnly NEPCShapesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
