Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NConnectorShapesExample
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
            Nevron.Nov.Examples.Diagram.NConnectorShapesExample.NConnectorShapesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NConnectorShapesExample), NExampleBaseSchema)
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
    This example demonstrates the connector shapes, which are created by the NConnectorShapeFactory.
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage
            drawing.ScreenVisibility.ShowGrid = False

            ' create all shapes
            Dim factory As Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory()
            factory.DefaultSize = New Nevron.Nov.Graphics.NSize(120, 90)
            Dim row As Integer = 0, col As Integer = 0
            Dim cellWidth As Double = 300
            Dim cellHeight As Double = 200
            Dim i As Integer = 0

            While i < factory.ShapeCount
                Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(i)
                shape.Text = factory.GetShapeInfo(CInt((i))).Name
                activePage.Items.Add(shape)

                If col >= 4 Then
                    row += 1
                    col = 0
                End If

                Dim beginPoint As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(50 + col * cellWidth, 50 + row * cellHeight)
                Dim endPoint As Nevron.Nov.Graphics.NPoint = beginPoint + New Nevron.Nov.Graphics.NPoint(cellWidth - 50, cellHeight - 50)
                shape.SetBeginPoint(beginPoint)
                shape.SetEndPoint(endPoint)
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
        ''' Schema associated with NConnectorShapesExample.
        ''' </summary>
        Public Shared ReadOnly NConnectorShapesExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
