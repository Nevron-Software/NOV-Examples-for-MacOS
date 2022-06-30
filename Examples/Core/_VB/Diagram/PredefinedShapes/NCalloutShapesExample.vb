Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NCalloutShapesExample
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
            Nevron.Nov.Examples.Diagram.NCalloutShapesExample.NCalloutShapesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NCalloutShapesExample), NExampleBaseSchema)
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
		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
		Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    This example demonstrates the callout shapes, which are created by the NCalloutShapeFactory.
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Const XStep As Double = 150
            Const YStep As Double = 100
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

			' Hide grid and ports
			drawing.ScreenVisibility.ShowGrid = False
            drawing.ScreenVisibility.ShowPorts = False

			' Create all shapes
			Dim factory As Nevron.Nov.Diagram.Shapes.NCalloutShapeFactory = New Nevron.Nov.Diagram.Shapes.NCalloutShapeFactory()
            factory.DefaultSize = New Nevron.Nov.Graphics.NSize(70, 70)
            Dim x As Double = 0
            Dim y As Double = 0

            For i As Integer = 0 To factory.ShapeCount - 1
                Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(i)
                shape.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                shape.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                shape.Tooltip = New Nevron.Nov.UI.NTooltip(factory.GetShapeInfo(CInt((i))).Name)
                activePage.Items.Add(shape)

                If shape.ShapeType = Nevron.Nov.Diagram.ENShapeType.Shape1D Then
                    shape.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(x, y))
                    shape.SetEndPoint(New Nevron.Nov.Graphics.NPoint(x + shape.Width, y + shape.Height))
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
			activePage.Layout.ContentPadding = New Nevron.Nov.Graphics.NMargins(70, 60, 70, 60)
            activePage.SizeToContent()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCalloutShapesExample.
		''' </summary>
		Public Shared ReadOnly NCalloutShapesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
