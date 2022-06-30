Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NDfdShapesExample
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
            Nevron.Nov.Examples.Diagram.NDfdShapesExample.NDfdShapesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NDfdShapesExample), NExampleBaseSchema)
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
            Return "<p>This example demonstrates the data flow shapes, which are created by the NDfdShapeFactory.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Const XStep As Double = 150
            Const YStep As Double = 200
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

			' Hide grid and ports
			drawing.ScreenVisibility.ShowGrid = False
            drawing.ScreenVisibility.ShowPorts = False

			' Create all shapes
			Dim factory As Nevron.Nov.Diagram.Shapes.NDataFlowDiagramShapesFactory = New Nevron.Nov.Diagram.Shapes.NDataFlowDiagramShapesFactory()
            factory.DefaultSize = New Nevron.Nov.Graphics.NSize(80, 60)
            Dim x As Double = 0
            Dim y As Double = 0

            For i As Integer = 0 To factory.ShapeCount - 1
                Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(i)
                shape.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                shape.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                Dim name As String = factory.GetShapeInfo(CInt((i))).Name
                shape.Tooltip = New Nevron.Nov.UI.NTooltip(name)
                shape.Text = factory.GetShapeInfo(CInt((i))).Name
                shape.MoveTextBlockBelowShape()
                activePage.Items.Add(shape)

                If shape.ShapeType = Nevron.Nov.Diagram.ENShapeType.Shape1D Then
                    shape.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(x, y))
                    shape.SetEndPoint(New Nevron.Nov.Graphics.NPoint(x + shape.Width, y))
                Else
                    shape.SetBounds(x, y, shape.Width, shape.Height)
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
		''' Schema associated with NDfdShapesExample.
		''' </summary>
		Public Shared ReadOnly NDfdShapesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
