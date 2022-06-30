Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NBasicShapesExample
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
            Nevron.Nov.Examples.Diagram.NBasicShapesExample.NBasicShapesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NBasicShapesExample), NExampleBaseSchema)
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
            Return "<p>This example demonstrates the basic shapes, which are created by the NBasicShapeFactory.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

			' Hide grid and ports
			drawing.ScreenVisibility.ShowGrid = False
            drawing.ScreenVisibility.ShowPorts = False

			' create all shapes
			Dim factory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            factory.DefaultSize = New Nevron.Nov.Graphics.NSize(120, 90)
            Dim row As Integer = 0, col As Integer = 0
            Dim cellWidth As Double = 240
            Dim cellHeight As Double = 150
            Dim i As Integer = 0

            While i < factory.ShapeCount
                Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(i)
                shape.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                shape.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                shape.Text = factory.GetShapeInfo(CInt((i))).Name
                shape.MoveTextBlockBelowShape()

                If i = CInt(Nevron.Nov.Diagram.Shapes.ENBasicShape.ThreeDBox) Then
                    shape.TextBlock.Padding = New Nevron.Nov.Graphics.NMargins(0, 15, 0, 0)
                ElseIf i = CInt(Nevron.Nov.Diagram.Shapes.ENBasicShape.Concentric) Then
                    shape.TextBlock.Angle = Nevron.Nov.NAngle.Zero
                End If

                activePage.Items.Add(shape)

                If col >= 4 Then
                    row += 1
                    col = 0
                End If

                Dim beginPoint As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(50 + col * cellWidth, 50 + row * cellHeight)

                If shape.ShapeType = Nevron.Nov.Diagram.ENShapeType.Shape1D Then
                    Dim endPoint As Nevron.Nov.Graphics.NPoint = beginPoint + New Nevron.Nov.Graphics.NPoint(cellWidth - 50, cellHeight - 50)

                    If i = CInt(Nevron.Nov.Diagram.Shapes.ENBasicShape.CenterDragCircle) Then
                        beginPoint.Translate(cellWidth / 3, cellHeight / 3)
                        endPoint.Translate(-cellWidth / 3, -cellHeight / 3)
                    End If

                    shape.SetBeginPoint(beginPoint)
                    shape.SetEndPoint(endPoint)
                Else
                    shape.SetBounds(beginPoint.X, beginPoint.Y, shape.Width, shape.Height)
                End If

                i += 1
                col += 1
            End While

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
		''' Schema associated with NBasicShapesExample.
		''' </summary>
		Public Shared ReadOnly NBasicShapesExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
