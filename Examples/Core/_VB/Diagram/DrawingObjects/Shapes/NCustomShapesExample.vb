Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Batches
Imports Nevron.Nov.Diagram.Expressions
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NCustomShapesExample
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
            Nevron.Nov.Examples.Diagram.NCustomShapesExample.NCustomShapesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NCustomShapesExample), NExampleBaseSchema)
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
    Demonstrates how to create custom shapes by combining shapes and also how to create custom formula shapes with control points.
</p>
<p>
    The Coffee Cup shape is created by grouping shapes with different fill styles inside a group. 
    By using this approach you can create shapes that mix different fill and stroke styles.
</p>
<p>
    The Trapezoid Shape is a replica of the Visio Trapezoid Smart Shape. 
    It demonstrates that with NOV Diagram you can replicate the Shape Sheet behavior of almost any Visio shape.
    Select the shape and move its control point to modify the trapezoid strip width. 
    Note that the geometry, left and right ports and XControl point behavior of this shape are driven by formula expressions.
</p>
            "
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            AddHandler Me.m_DrawingView.ActivePage.Interaction.GluingShapes, AddressOf Me.Interaction_GluingShapes

            ' create the coffee cup
            Dim coffeeCup As Nevron.Nov.Diagram.NShape = Me.CreateCoffeeCupShape()
            coffeeCup.SetBounds(New Nevron.Nov.Graphics.NRectangle(50, 50, 100, 200))
            drawingDocument.Content.ActivePage.Items.Add(coffeeCup)
            Dim trapedzoid As Nevron.Nov.Diagram.NShape = Me.CreateTrapedzoidShape()
            trapedzoid.SetBounds(New Nevron.Nov.Graphics.NRectangle(200, 150, 100, 100))
            drawingDocument.Content.ActivePage.Items.Add(trapedzoid)
        End Sub

        ''' <summary>
        ''' Creates a custom shape that is essentially a group consisting of three other shapes each with different filling.
        ''' You need to use groups to have shapes that mix different fill, or stroke styles.
        ''' </summary>
        ''' <returns></returns>
        Private Function CreateCoffeeCupShape() As Nevron.Nov.Diagram.NShape
            ' create the points and paths from which the shape consits
            Dim cupPoints As Nevron.Nov.Graphics.NPoint() = New Nevron.Nov.Graphics.NPoint() {New Nevron.Nov.Graphics.NPoint(45, 268), New Nevron.Nov.Graphics.NPoint(63, 331), New Nevron.Nov.Graphics.NPoint(121, 331), New Nevron.Nov.Graphics.NPoint(140, 268)}
            Dim handleGraphicsPath As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            handleGraphicsPath.AddClosedCurve(New Nevron.Nov.Graphics.NPoint() {New Nevron.Nov.Graphics.NPoint(175, 295), New Nevron.Nov.Graphics.NPoint(171, 278), New Nevron.Nov.Graphics.NPoint(140, 283), New Nevron.Nov.Graphics.NPoint(170, 290), New Nevron.Nov.Graphics.NPoint(128, 323)}, 1)
            Dim steamGraphicsPath As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            steamGraphicsPath.AddCubicBeziers(New Nevron.Nov.Graphics.NPoint() {New Nevron.Nov.Graphics.NPoint(92, 270), New Nevron.Nov.Graphics.NPoint(53, 163), New Nevron.Nov.Graphics.NPoint(145, 160), New Nevron.Nov.Graphics.NPoint(86, 50), New Nevron.Nov.Graphics.NPoint(138, 194), New Nevron.Nov.Graphics.NPoint(45, 145), New Nevron.Nov.Graphics.NPoint(92, 270)})
            steamGraphicsPath.CloseFigure()

            ' calculate some bounds
            Dim handleBounds As Nevron.Nov.Graphics.NRectangle = handleGraphicsPath.ExactBounds
            Dim cupBounds As Nevron.Nov.Graphics.NRectangle = Nevron.Nov.Graphics.NGeometry2D.GetBounds(cupPoints)
            Dim steamBounds As Nevron.Nov.Graphics.NRectangle = steamGraphicsPath.ExactBounds
            Dim geometryBounds As Nevron.Nov.Graphics.NRectangle = Nevron.Nov.Graphics.NRectangle.Union(cupBounds, handleBounds, steamBounds)

            ' normalize the points and paths by transforming them to relative coordinates
            Dim normalRect As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(0, 0, 1, 1)
            Dim transform As Nevron.Nov.Graphics.NMatrix = Nevron.Nov.Graphics.NMatrix.CreateBoundsStretchMatrix(geometryBounds, normalRect)
            transform.TransformPoints(cupPoints)
            handleGraphicsPath.Transform(transform)
            steamGraphicsPath.Transform(transform)

            ' create the cup shape
            Dim cupPolygon As Nevron.Nov.Diagram.NDrawPolygon = New Nevron.Nov.Diagram.NDrawPolygon(normalRect, cupPoints)
            cupPolygon.Relative = True
            Dim cupShape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            cupShape.Init2DShape()
            cupShape.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Brown)
            cupShape.Geometry.Add(cupPolygon)
            cupShape.SetBounds(geometryBounds)
            
            ' create the cup handle
            Dim handlePath As Nevron.Nov.Diagram.NDrawPath = New Nevron.Nov.Diagram.NDrawPath(normalRect, handleGraphicsPath)
            handlePath.Relative = True
            Dim handleShape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            handleShape.Init2DShape()
            handleShape.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightSalmon)
            handleShape.Geometry.Add(handlePath)
            handleShape.SetBounds(geometryBounds)
            
            ' create the steam
            Dim steamPath As Nevron.Nov.Diagram.NDrawPath = New Nevron.Nov.Diagram.NDrawPath(steamGraphicsPath.Bounds, steamGraphicsPath)
            steamPath.Relative = True
            Dim steamShape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            steamShape.Init2DShape()
            steamShape.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(50, 122, 122, 122))
            steamShape.Geometry.Add(steamPath)
            steamShape.SetBounds(geometryBounds)
            
            ' group the shapes as a single group
            Dim group As Nevron.Nov.Diagram.NGroup
            Dim batch As Nevron.Nov.Diagram.Batches.NBatchGroup = New Nevron.Nov.Diagram.Batches.NBatchGroup(Me.m_DrawingView.Document)
            batch.Build(cupShape, handleShape, steamShape)
            batch.Group(Nothing, group)

            ' alter some properties of the group
            group.SelectionMode = Nevron.Nov.Diagram.ENGroupSelectionMode.GroupOnly
            group.SnapToShapes = False
            Return group
        End Function
        ''' <summary>
        ''' Creates a custom shape that is a replica of the Visio Trapedzoid shape. With NOV diagram you can replicate the smart behavior of any Visio smart shape.
        ''' </summary>
        ''' <returns></returns>
        Private Function CreateTrapedzoidShape() As Nevron.Nov.Diagram.NShape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Init2DShape()

            ' add controls
            Dim control As Nevron.Nov.Diagram.NControl = New Nevron.Nov.Diagram.NControl()
            control.SetFx(Nevron.Nov.Diagram.NControl.XProperty, New Nevron.Nov.Diagram.Expressions.NShapeWidthFactorFx(0.3))
            control.Y = 0.0R
            control.SetFx(Nevron.Nov.Diagram.NControl.XBehaviorProperty, String.Format("IF(X<Width/2,{0},{1})", (CInt(Nevron.Nov.Diagram.ENCoordinateBehavior.OffsetFromMin)), (CInt(Nevron.Nov.Diagram.ENCoordinateBehavior.OffsetFromMax))))
            control.YBehavior = Nevron.Nov.Diagram.ENCoordinateBehavior.Locked
            control.Tooltip = "Modify strip width"
            shape.Controls.Add(control)

            ' add a geometry
            Dim geometry1 As Nevron.Nov.Diagram.NGeometry = shape.Geometry

            If True Then
                Dim plotFigure As Nevron.Nov.Diagram.NMoveTo = geometry1.MoveTo("MIN(Controls.0.X,Width-Controls.0.X)", 0.0R)
                geometry1.LineTo("Width-Geometry.0.X", 0.0R)
                geometry1.LineTo("Width", "Height")
                geometry1.LineTo(0.0R, "Height")
                geometry1.LineTo("Geometry.0.X", "Geometry.0.Y")
                plotFigure.CloseFigure = True
            End If

            ' add ports
            For i As Integer = 0 To 4 - 1
                Dim port As Nevron.Nov.Diagram.NPort = New Nevron.Nov.Diagram.NPort()
                shape.Ports.Add(port)

                Select Case i
                    Case 0 ' top
                        port.Relative = True
                        port.X = 0.5
                        port.Y = 0.0R
                        port.SetDirection(Nevron.Nov.ENBoxDirection.Up)
                    Case 1 ' right
                        port.SetFx(Nevron.Nov.Diagram.NPort.XProperty, "(Geometry.1.X + Geometry.2.X) / 2")
                        port.SetFx(Nevron.Nov.Diagram.NPort.YProperty, New Nevron.Nov.Diagram.Expressions.NShapeHeightFactorFx(0.5))
                        port.SetDirection(Nevron.Nov.ENBoxDirection.Right)
                    Case 2 ' bottom
                        port.Relative = True
                        port.X = 0.5
                        port.Y = 1.0R
                        port.SetDirection(Nevron.Nov.ENBoxDirection.Down)
                    Case 3 ' left
                        port.SetFx(Nevron.Nov.Diagram.NPort.XProperty, "(Geometry.0.X + Geometry.3.X) / 2")
                        port.SetFx(Nevron.Nov.Diagram.NPort.YProperty, New Nevron.Nov.Diagram.Expressions.NShapeHeightFactorFx(0.5))
                        port.SetDirection(Nevron.Nov.ENBoxDirection.Left)
                End Select
            Next

            Return shape
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub Interaction_GluingShapes(ByVal args As Nevron.Nov.Diagram.NGlueShapesEventArgs)
            ' safely get the ports collection
            Dim ports As Nevron.Nov.Diagram.NPortCollection = CType(args.Shape2D.GetChild(Nevron.Nov.Diagram.NShape.PortsChild, False), Nevron.Nov.Diagram.NPortCollection)
            If ports Is Nothing AndAlso ports.Count = 0 Then Return

            ' get the anchor point in page coordinates
            Dim anchorInPage As Nevron.Nov.Graphics.NPoint = If(args.ConnectBegin, args.Shape1D.GetEndPointInPage(), args.Shape1D.GetBeginPointInPage())

            ' get the nearest port
            Dim neartestPort As Nevron.Nov.Diagram.NPort = ports(0)
            Dim neartestDistance As Double = Nevron.Nov.Graphics.NGeometry2D.PointsDistance(anchorInPage, neartestPort.GetLocationInPage())

            For i As Integer = 1 To ports.Count - 1
                Dim curPort As Nevron.Nov.Diagram.NPort = ports(i)
                Dim curDistance As Double = Nevron.Nov.Graphics.NGeometry2D.PointsDistance(anchorInPage, curPort.GetLocationInPage())

                If curDistance < neartestDistance Then
                    neartestDistance = curDistance
                    neartestPort = curPort
                End If
            Next

            ' connect begin or end 
            If args.ConnectBegin Then
                args.Shape1D.GlueBeginToPort(neartestPort)
            Else
                args.Shape1D.GlueEndToPort(neartestPort)
            End If

            ' cancel the event so that the diagram does not perform default connection
            args.Cancel = True
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NCustomShapesExample.
        ''' </summary>
        Public Shared ReadOnly NCustomShapesExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
