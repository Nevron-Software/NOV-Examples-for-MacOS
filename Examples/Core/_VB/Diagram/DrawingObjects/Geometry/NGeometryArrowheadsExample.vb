Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NGeometryArrowheadsExample
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
            Nevron.Nov.Examples.Diagram.NGeometryArrowheadsExample.NGeometryArrowheadsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NGeometryArrowheadsExample), NExampleBaseSchema)
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
            Return "<p>Demonstrates the arrowhead styles included in NOV Diagram.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage
            activePage.Layout.ContentPadding = New Nevron.Nov.Graphics.NMargins(20)

            ' switch selected edit mode to geometry
            ' this instructs the diagram to show geometry handles for the selected shapes.
            drawing.ScreenVisibility.ShowGrid = False
            Dim connectorShapes As Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory()
            Dim arrowheadShapes As Nevron.Nov.Diagram.ENArrowheadShape() = Nevron.Nov.NEnum.GetValues(Of Nevron.Nov.Diagram.ENArrowheadShape)()
            Dim x As Double = 20
            Dim y As Double = 0

            For i As Integer = 1 To arrowheadShapes.Length - 1
                Dim arrowheadShape As Nevron.Nov.Diagram.ENArrowheadShape = arrowheadShapes(i)
                Dim shape As Nevron.Nov.Diagram.NShape = connectorShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
                drawing.ActivePage.Items.Add(shape)

                ' create geometry arrowheads
                shape.Geometry.BeginArrowhead = New Nevron.Nov.Diagram.NArrowhead(arrowheadShape)
                shape.Geometry.EndArrowhead = New Nevron.Nov.Diagram.NArrowhead(arrowheadShape)
                shape.Text = Nevron.Nov.NEnum.GetLocalizedString(arrowheadShape)
                shape.SetBeginPoint(New Nevron.Nov.Graphics.NPoint(x, y))
                shape.SetEndPoint(New Nevron.Nov.Graphics.NPoint(x + 350, y))
                y += 30

                If i = arrowheadShapes.Length / 2 Then
					' Begin a second column of shapes
					x += 400
                    y = 0
                End If
            Next

            activePage.SizeToContent()
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NGeometryArrowheadsExample.
        ''' </summary>
        Public Shared ReadOnly NGeometryArrowheadsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
