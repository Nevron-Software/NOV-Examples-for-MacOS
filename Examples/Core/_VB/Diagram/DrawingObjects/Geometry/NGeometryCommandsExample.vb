Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
	''' <summary>
	''' 
	''' </summary>
	Public Class NGeometryCommandsExample
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
            Nevron.Nov.Examples.Diagram.NGeometryCommandsExample.NGeometryCommandsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NGeometryCommandsExample), NExampleBaseSchema)
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
    Demonstrates the geometry commands that you can use to construct the shapes geometry. 
</p>
<p>
    On the first row you can see the different types of geometry commands that you can use to plot geometry. 
    Plotter commands are designed to be placed in a sequence so that you can create arbitrary geometry by combining 
    MoveTo, LineTo, CubicBezierTo, ArcTo, CircularActTo and EllipticalArcTo commands.
</p>
<p>
    On the second row you can see the different types of draw box commands. 
    These commands are used when you want to output more vertices or generally draw more complex clipart shapes with single geometry commands.
</p>
<p>
    The active page is switched to geometry edit mode, so when you select shapes you can see handles you can move to modify the geometry.
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content

            ' switch selected edit mode to geometry
            ' this instructs the diagram to show geometry handles for the selected shapes.
            drawingDocument.Content.ActivePage.SelectionEditMode = Nevron.Nov.Diagram.ENSelectionEditMode.Geometry
            drawing.ScreenVisibility.ShowGrid = False

            ' plotter commands
            Me.CreateDescriptionPair(0, 0, Me.CreateLineTo(), "Line To")
            Me.CreateDescriptionPair(0, 1, Me.CreateArcTo(), "Arc To")
            Me.CreateDescriptionPair(0, 2, Me.CreateCubicBezierTo(), "Cubic Bezier To")
            Me.CreateDescriptionPair(0, 3, Me.CreateCircularArcTo(), "Circular Arc To")
            Me.CreateDescriptionPair(0, 4, Me.CreateEllipticalArcTo(), "Elliptical Arc To")

            ' draw box commands
            Me.CreateDescriptionPair(1, 0, Me.CreateDrawRectangle(), "Draw Rectangle")
            Me.CreateDescriptionPair(1, 1, Me.CreateDrawEllipse(), "Draw Ellipse")
            Me.CreateDescriptionPair(1, 2, Me.CreateDrawPolygon(0), "Draw Polygon")
            Me.CreateDescriptionPair(1, 3, Me.CreateDrawPolyline(0), "Draw Polyline")
            Me.CreateDescriptionPair(1, 4, Me.CreateDrawPolygon(1), "Draw Polygon With Tension")
            Me.CreateDescriptionPair(1, 5, Me.CreateDrawPolyline(1), "Draw Polyline With Tension")
            Me.CreateDescriptionPair(1, 6, Me.CreateDrawPath(), "Draw Path")
        End Sub

        Private Function CreateLineTo() As Nevron.Nov.Diagram.NShape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Init2DShape()

            ' the LineTo command draws a line from the prev plotter command to the command location
            If True Then
                Dim plotFigure As Nevron.Nov.Diagram.NMoveTo = shape.Geometry.RelMoveTo(0, 0)
                shape.Geometry.RelLineTo(1, 1)
                plotFigure.ShowFill = False
            End If

            Return shape
        End Function

        Private Function CreateArcTo() As Nevron.Nov.Diagram.NShape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Init2DShape()

            ' the ArcTo command draws a circular arc from the prev plotter command to the command location.
            ' the ArcTo Bow parameter defines the distance of the arc from the line formed by previous command location and the command location
            If True Then
                Dim plotFigure As Nevron.Nov.Diagram.NMoveTo = shape.Geometry.RelMoveTo(0, 0)
                shape.Geometry.RelArcTo(1, 1, 30)
                plotFigure.ShowFill = False
            End If

            Return shape
        End Function

        Private Function CreateCubicBezierTo() As Nevron.Nov.Diagram.NShape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Init2DShape()

            ' the CubicBezierTo command draws a cubic bezier from the prev plotter command to the command location.
            ' the cubic bezier curve is controled by two control points.
            If True Then
                Dim plotFigure As Nevron.Nov.Diagram.NMoveTo = shape.Geometry.RelMoveTo(0, 0)
                shape.Geometry.RelCubicBezierTo(1, 1, 1, 0, 0, 1)
                plotFigure.ShowFill = False
            End If

            Return shape
        End Function

        Private Function CreateCircularArcTo() As Nevron.Nov.Diagram.NShape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Init2DShape()

            ' the CircularAcrTo command draws a circular arc from the prev plotter command to the command location.
            ' the circular acr curve is controled by a control point which defines the circle trough which the arc passes.
            If True Then
                Dim plotFigure As Nevron.Nov.Diagram.NMoveTo = shape.Geometry.RelMoveTo(0, 0)
                shape.Geometry.RelCircularArcTo(1, 1, 1, 0)
                plotFigure.ShowFill = False
            End If

            Return shape
        End Function

        Private Function CreateEllipticalArcTo() As Nevron.Nov.Diagram.NShape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Init2DShape()

            ' the EllipticalArcTo command draws an elliptical arc from the prev plotter command to the command location.
            ' the elliptical acr curve is controled by a control point which defines the ellipse trough which the arc passes, 
            ' the angle of the ellipse and the ratio between the ellipse radiuses.
            If True Then
                Dim plotFigure As Nevron.Nov.Diagram.NMoveTo = shape.Geometry.RelMoveTo(0, 0)
                shape.Geometry.RelEllipticalArcTo(1, 1, 1, 0, New Nevron.Nov.NAngle(0, Nevron.Nov.NUnit.Degree), 0.5)
                plotFigure.ShowFill = False
            End If

            Return shape
        End Function

        Private Function CreateDrawRectangle() As Nevron.Nov.Diagram.NShape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Init2DShape()

            ' the draw rectangle command draws a rect inside a relative or absolute rect inside the shape coordinate system. 
            ' The following draws a rect that fills the shape.
            Dim drawRectangle As Nevron.Nov.Diagram.NDrawRectangle = New Nevron.Nov.Diagram.NDrawRectangle(0, 0, 1, 1)
            shape.Geometry.AddRelative(drawRectangle)
            Return shape
        End Function

        Private Function CreateDrawEllipse() As Nevron.Nov.Diagram.NShape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Init2DShape()

            ' the draw ellipse command draws an ellipse inside a relative or absolute rect inside the shape coordinate system. 
            ' The following draws an ellipse that fills the shape.
            Dim drawEllipse As Nevron.Nov.Diagram.NDrawEllipse = New Nevron.Nov.Diagram.NDrawEllipse(0, 0, 1, 1)
            shape.Geometry.AddRelative(drawEllipse)
            Return shape
        End Function

        Private Function CreateDrawPolygon(ByVal tension As Double) As Nevron.Nov.Diagram.NShape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Init2DShape()
            Dim ngon As Nevron.Nov.Graphics.NGenericNGram = New Nevron.Nov.Graphics.NGenericNGram(4, 0, 0.5, 0.1, New Nevron.Nov.Graphics.NPoint(0.5, 0.5))
            Dim points As Nevron.Nov.Graphics.NPoint() = ngon.CreateVertices()

            ' the draw ellipse command draws an ellipse inside a relative or absolute rect inside the shape coordinate system. 
            ' The following draws an ellipse that fills the shape.
            Dim drawPolygon As Nevron.Nov.Diagram.NDrawPolygon = New Nevron.Nov.Diagram.NDrawPolygon(0, 0, 1, 1, points)
            drawPolygon.Tension = tension
            shape.Geometry.AddRelative(drawPolygon)
            Return shape
        End Function

        Private Function CreateDrawPolyline(ByVal tension As Double) As Nevron.Nov.Diagram.NShape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Init2DShape()
            Dim points As Nevron.Nov.Graphics.NPoint() = New Nevron.Nov.Graphics.NPoint() {New Nevron.Nov.Graphics.NPoint(0, 0), New Nevron.Nov.Graphics.NPoint(0.25, 1), New Nevron.Nov.Graphics.NPoint(0.50, 0), New Nevron.Nov.Graphics.NPoint(0.75, 1), New Nevron.Nov.Graphics.NPoint(1, 0)}

            ' the draw ellipse command draws an ellipse inside a relative or absolute rect inside the shape coordinate system. 
            ' The following draws an ellipse that fills the shape.
            Dim drawPolyline As Nevron.Nov.Diagram.NDrawPolyline = New Nevron.Nov.Diagram.NDrawPolyline(0, 0, 1, 1, points)
            drawPolyline.Tension = tension
            drawPolyline.ShowFill = False
            shape.Geometry.AddRelative(drawPolyline)
            Return shape
        End Function

        Private Function CreateDrawPath() As Nevron.Nov.Diagram.NShape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Init2DShape()
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddRectangle(0, 0, 0.5, 0.5)
            path.AddEllipse(0.5, 0.5, 0.5, 0.5)

            ' the draw path command draws a path inside a relative or absolute rect inside the shape coordinate system. 
            ' The following draws a path that contains a rectangle and an ellipse that fills the shape.
            Dim drawPath As Nevron.Nov.Diagram.NDrawPath = New Nevron.Nov.Diagram.NDrawPath(0, 0, 1, 1, path)
            shape.Geometry.AddRelative(drawPath)
            Return shape
        End Function

        Private Sub CreateDescriptionPair(ByVal row As Integer, ByVal col As Integer, ByVal shape As Nevron.Nov.Diagram.NShape, ByVal text As String)
            Const startX As Double = 20
            Const startY As Double = 100
            Const width As Double = 80
            Const height As Double = 100
            Const spacing As Double = 20
            Me.m_DrawingView.ActivePage.Items.Add(shape)
            shape.SetBounds(New Nevron.Nov.Graphics.NRectangle(startX + col * (width + spacing), startY + row * (height + spacing), width, height / 2))
            Dim textShape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            textShape.Init2DShape()
            textShape.Text = text
            textShape.SetBounds(New Nevron.Nov.Graphics.NRectangle(startX + col * (width + spacing), startY + row * (height + spacing) + height / 2, width, height / 2))
            Me.m_DrawingView.ActivePage.Items.Add(textShape)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NGeometryCommandsExample.
        ''' </summary>
        Public Shared ReadOnly NGeometryCommandsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
