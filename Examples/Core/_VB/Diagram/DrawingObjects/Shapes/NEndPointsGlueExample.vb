Imports System
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NEndPointsGlueExample
        Inherits NExampleBase
        #Region"Constructors"

        ''' <summary>
        ''' Default constructor.
        ''' </summary>
        Public Sub New()
            AddHandler Me.m_Timer.Tick, AddressOf Me.OnTimerTick
        End Sub
        ''' <summary>
        ''' Static constructor.
        ''' </summary>
        Shared Sub New()
            Nevron.Nov.Examples.Diagram.NEndPointsGlueExample.NEndPointsGlueExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NEndPointsGlueExample), NExampleBaseSchema)
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

            AddHandler Me.m_DrawingView.Registered, AddressOf Me.OnDrawingViewRegistered
            AddHandler Me.m_DrawingView.Unregistered, AddressOf Me.OnDrawingViewUnregistered
            Return drawingViewWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last

            ' selection mode
            If True Then
                Me.m_RadioGroup = New Nevron.Nov.UI.NRadioButtonGroup()
                Dim radioStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Me.m_RadioGroup.Content = radioStack
                radioStack.Add(New Nevron.Nov.UI.NRadioButton("Glue To Nearest Port"))
                radioStack.Add(New Nevron.Nov.UI.NRadioButton("Glue To Shape Box Intersection"))
                radioStack.Add(New Nevron.Nov.UI.NRadioButton("Glue To Geometry Intersection"))
                radioStack.Add(New Nevron.Nov.UI.NRadioButton("Glue To Shape Box"))
                radioStack.Add(New Nevron.Nov.UI.NRadioButton("Glue To Geometry Contour"))
                radioStack.Add(New Nevron.Nov.UI.NRadioButton("Glue To Port"))
                stack.Add(New Nevron.Nov.UI.NGroupBox("Select Glue Mode", Me.m_RadioGroup))
            End If

            ' glue properties
            If True Then
                Dim holdersStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                stack.Add(holdersStack)
                Me.m_BeginGlueHolder = New Nevron.Nov.UI.NGroupBox("Begin Glue Properties")
                holdersStack.Add(Me.m_BeginGlueHolder)
                Me.m_EndGlueHolder = New Nevron.Nov.UI.NGroupBox("End Glue Properties")
                holdersStack.Add(Me.m_EndGlueHolder)
            End If

            ' timer
            If True Then
                Dim timerStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Dim startRotationButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Start Rotation")
                AddHandler startRotationButton.Click, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) Me.m_Timer.Start()
                timerStack.Add(startRotationButton)
                Dim stopRotationButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Stop Rotation")
                AddHandler stopRotationButton.Click, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) Me.m_Timer.[Stop]()
                timerStack.Add(stopRotationButton)
                stack.Add(timerStack)
            End If

            ' select the first glue mode
            AddHandler Me.m_RadioGroup.SelectedIndexChanged, AddressOf Me.OnRadioGroupSelectedIndexChanged
            Me.m_RadioGroup.SelectedIndex = 0
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates the End-Points glue and the API you can use to glue the begin and end points.
</p>
<p>
    The begin or end point of a 1D shape can be glued in the following ways:
    <ul>
        <li>
            <b>Glue To Nearest Port</b> The begin or end point is glued to the nearest port in respect to the other end-point.
        </li>
        <li>
            <b>Glue To Shape Box Intersection</b> The begin or end point is glued to the intersection of the shape box and the line formed by the shape center and the other end-point.
        </li>
        <li>
            <b>Glue To Geometry Intersection</b> The begin or end point is glued to the intersection of the shape geometry and the line formed by the shape center and the other end-point.
        </li>
        <li>
            <b>Glue To Shape Box</b> The begin or end point is glued to a point in the shape Width-Height box. The point is defined in relative coordinates.
        </li>
        <li>
            <b>Glue To Geometry Contour</b> The begin or end point is glued to a point along the shape geometry contour (outline). The point is defined a factor - 0 is the contour begin, 1 is the contour end.
        </li>
        <li>
            <b>Glue To Port</b> The begin or end point is glued to a port of the target shape. The port is defined by its index.
        </li>
    </ul>
</p>
            "
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content

            ' Hide the grid
            drawing.ScreenVisibility.ShowGrid = False

            ' Create two shapes and a line connector between them
            Dim basicShapes As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim connectorShapes As Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory()
            Me.m_BeginShape = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Ellipse)
            Me.m_BeginShape.Size = New Nevron.Nov.Graphics.NSize(150, 100)
            drawing.ActivePage.Items.Add(Me.m_BeginShape)
            Me.m_EndShape = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Pentagram)
            Me.m_EndShape.Size = New Nevron.Nov.Graphics.NSize(100, 100)
            drawing.ActivePage.Items.Add(Me.m_EndShape)
            Me.m_Connector = connectorShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
            Me.m_Connector.GlueBeginToNearestPort(Me.m_BeginShape)
            Me.m_Connector.GlueEndToNearestPort(Me.m_EndShape)
            drawing.ActivePage.Items.Add(Me.m_Connector)

            ' Perform inital layout of shapes
            Me.OnTimerTick()
        End Sub
        
        #EndRegion

        #Region"Event Handlers"

        Private Sub OnDrawingViewRegistered(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Timer.Start()
        End Sub

        Private Sub OnDrawingViewUnregistered(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Timer.[Stop]()
        End Sub

        Private Sub OnTimerTick()
            Dim centerOfRotation As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(Me.m_DrawingView.ActivePage.Bounds.CenterX, 300)
            Const radius As Double = 150
            Dim beginCenter As Nevron.Nov.Graphics.NPoint = centerOfRotation + New Nevron.Nov.Graphics.NPoint(System.Math.Cos(Me.m_dAngle) * radius, System.Math.Sin(Me.m_dAngle) * radius)
            Me.m_BeginShape.SetBounds(Nevron.Nov.Graphics.NRectangle.FromCenterAndSize(beginCenter, Me.m_BeginShape.Width, Me.m_BeginShape.Height))
            Dim endCenter As Nevron.Nov.Graphics.NPoint = centerOfRotation + New Nevron.Nov.Graphics.NPoint(System.Math.Cos(Me.m_dAngle + Nevron.Nov.NMath.PI) * radius, System.Math.Sin(Me.m_dAngle + Nevron.Nov.NMath.PI) * radius)
            Me.m_EndShape.SetBounds(Nevron.Nov.Graphics.NRectangle.FromCenterAndSize(endCenter, Me.m_EndShape.Width, Me.m_EndShape.Height))
            Me.m_dAngle += Nevron.Nov.NMath.PI / 180
        End Sub

        Private Sub OnRadioGroupSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Select Case Me.m_RadioGroup.SelectedIndex
                Case 0 ' glue to nearest port
                    Me.m_Connector.GlueBeginToNearestPort(Me.m_BeginShape)
                    Me.m_Connector.GlueEndToNearestPort(Me.m_EndShape)
                Case 1 ' glue to box intersection
                    Me.m_Connector.GlueBeginToShapeBoxIntersection(Me.m_BeginShape)
                    Me.m_Connector.GlueEndToShapeBoxIntersection(Me.m_EndShape)
                Case 2 ' glue to box intersection
                    Me.m_Connector.GlueBeginToGeometryIntersection(Me.m_BeginShape)
                    Me.m_Connector.GlueEndToGeometryIntersection(Me.m_EndShape)
                Case 3 ' glue to box location
                    Me.m_Connector.GlueBeginToShapeBox(Me.m_BeginShape, 0.3R, 0.3R)
                    Me.m_Connector.GlueEndToShapeBox(Me.m_EndShape, 0.3R, 0.3R)
                Case 4 ' glue to geometry contour
                    Me.m_Connector.GlueBeginToGeometryContour(Me.m_BeginShape, 0.5R)
                    Me.m_Connector.GlueEndToGeometryContour(Me.m_EndShape, 0.5R)
                Case 5 ' glue to port
                    Me.m_Connector.GlueBeginToPort(Me.m_BeginShape.Ports(0))
                    Me.m_Connector.GlueEndToPort(Me.m_EndShape.Ports(0))
            End Select

            ' update the begin point glue properties
            If Me.m_Connector.BeginPointGlue Is Nothing Then
                Me.m_BeginGlueHolder.Content = Nothing
                Me.m_BeginGlueHolder.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
            Else
                Me.m_BeginGlueHolder.Visibility = Nevron.Nov.UI.ENVisibility.Visible
                Me.m_BeginGlueHolder.Content = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Connector.BeginPointGlue), Nevron.Nov.Dom.NNode)).CreateInstanceEditor(Me.m_Connector.BeginPointGlue)
            End If

            ' update the end point glue properties
            If Me.m_Connector.EndPointGlue Is Nothing Then
                Me.m_EndGlueHolder.Content = Nothing
                Me.m_EndGlueHolder.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
            Else
                Me.m_EndGlueHolder.Visibility = Nevron.Nov.UI.ENVisibility.Visible
                Me.m_EndGlueHolder.Content = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Connector.EndPointGlue), Nevron.Nov.Dom.NNode)).CreateInstanceEditor(Me.m_Connector.EndPointGlue)
            End If
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_BeginShape As Nevron.Nov.Diagram.NShape
        Private m_EndShape As Nevron.Nov.Diagram.NShape
        Private m_Connector As Nevron.Nov.Diagram.NShape
        Private m_Timer As Nevron.Nov.NTimer = New Nevron.Nov.NTimer(50)
        Private m_RadioGroup As Nevron.Nov.UI.NRadioButtonGroup
        Private m_BeginGlueHolder As Nevron.Nov.UI.NGroupBox
        Private m_EndGlueHolder As Nevron.Nov.UI.NGroupBox
        Private m_dAngle As Double = 0

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NEndPointsGlueExample.
        ''' </summary>
        Public Shared ReadOnly NEndPointsGlueExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
