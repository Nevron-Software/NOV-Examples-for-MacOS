Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NInputAndOutputPortsExample
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
            Nevron.Nov.Examples.Diagram.NInputAndOutputPortsExample.NInputAndOutputPortsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NInputAndOutputPortsExample), NExampleBaseSchema)
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
    Demonstrates the behavior of input and output ports. Try to connect the AND shape ports with lines.
</p>
<p> 
    In NOV Diagram each port FlowMode can be set to Input, Output or InputAndOutput (the default). 
</p>
<p>
    <b>Input</b> ports can accept connections with End points of 1D shapes and output ports of 2D shapes.
    Input ports are painted in green color.
</p>
<p>
    <b>Output</b> ports can accept connections with Begin points of 1D shapes and input ports of 2D shapes.
    Output ports are painted in red color.
</p>
<p>
    <b>InputAndOutput</b> ports behave as both input and output ports (the default).
    InputAndOutput ports are painted in blue color.
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

            ' hide the grid
            drawing.ScreenVisibility.ShowGrid = False

            ' create a AND shape
            Dim andShape As Nevron.Nov.Diagram.NShape = Me.CreateAndShape()
            andShape.SetBounds(300, 100, 150, 100)
            activePage.Items.Add(andShape)
        End Sub

        #EndRegion

        #Region"Implementation "

        Private Function CreateAndShape() As Nevron.Nov.Diagram.NShape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Init2DShape()
            Dim normalSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(1, 1)
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()

            ' create input lines
            Dim x1 As Double = 0
            Dim y1 As Double = normalSize.Height / 3
            path.StartFigure(x1, y1)
            path.LineTo(normalSize.Width / 4, y1)
            Dim y2 As Double = normalSize.Height * 2 / 3
            Dim x2 As Double = 0
            path.StartFigure(x2, y2)
            path.LineTo(normalSize.Width / 4, y2)

            ' create body
            path.StartFigure(normalSize.Width / 4, 0)
            path.LineTo(normalSize.Width / 4, 1)
            Dim ellipseCenter As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(normalSize.Width / 4, 0.5)
            path.AddEllipseSegment(Nevron.Nov.Graphics.NRectangle.FromCenterAndSize(ellipseCenter, normalSize.Width, normalSize.Height), Nevron.Nov.NMath.PIHalf, -Nevron.Nov.NMath.PI)
            path.CloseFigure()

            ' create output
            Dim y3 As Double = normalSize.Height / 2
            Dim x3 As Double = normalSize.Width
            path.StartFigure(normalSize.Width * 3 / 4, y3)
            path.LineTo(x3, y3)
            shape.Geometry.AddRelative(New Nevron.Nov.Diagram.NDrawPath(New Nevron.Nov.Graphics.NRectangle(0, 0, 1, 1), path))

            ' create ports
            Dim input1 As Nevron.Nov.Diagram.NPort = New Nevron.Nov.Diagram.NPort()
            input1.X = x1
            input1.Y = y1
            input1.Relative = True
            input1.SetDirection(Nevron.Nov.ENBoxDirection.Left)
            input1.FlowMode = Nevron.Nov.Diagram.ENPortFlowMode.Input
            shape.Ports.Add(input1)
            Dim input2 As Nevron.Nov.Diagram.NPort = New Nevron.Nov.Diagram.NPort()
            input2.X = x2
            input2.Y = y2
            input2.Relative = True
            input2.SetDirection(Nevron.Nov.ENBoxDirection.Left)
            input2.FlowMode = Nevron.Nov.Diagram.ENPortFlowMode.Input
            shape.Ports.Add(input2)
            Dim output1 As Nevron.Nov.Diagram.NPort = New Nevron.Nov.Diagram.NPort()
            output1.X = x3
            output1.Y = y3
            output1.Relative = True
            output1.SetDirection(Nevron.Nov.ENBoxDirection.Right)
            output1.FlowMode = Nevron.Nov.Diagram.ENPortFlowMode.Output
            shape.Ports.Add(output1)

            ' by default this shape does not accept shape-to-shape connections
            shape.DefaultShapeGlue = Nevron.Nov.Diagram.ENDefaultShapeGlue.None

            ' set text
            shape.Text = "AND"
            Return shape
        End Function

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NInputAndOutputPortsExample.
        ''' </summary>
        Public Shared ReadOnly NInputAndOutputPortsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
