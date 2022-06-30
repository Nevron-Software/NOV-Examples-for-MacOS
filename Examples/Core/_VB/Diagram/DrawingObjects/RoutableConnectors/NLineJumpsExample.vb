Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NLineJumpsExample
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
            Nevron.Nov.Examples.Diagram.NLineJumpsExample.NLineJumpsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NLineJumpsExample), NExampleBaseSchema)
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
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim activePage As Nevron.Nov.Diagram.NPage = Me.m_DrawingView.ActivePage
            Dim editor As Nevron.Nov.Editors.NEditor = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((activePage.LineJumps), Nevron.Nov.Dom.NNode)).CreateStateEditor(activePage.LineJumps)
            stack.Add(editor)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates the line jumps. Line jumps are shown at connector crossing points.</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage
            activePage.Items.Add(Me.CreateSampleLine1())
            activePage.Items.Add(Me.CreateSampleLine2())
            activePage.Items.Add(Me.CreateSamplePolyline1())
            activePage.Items.Add(Me.CreateSamplePolyline2())
            activePage.Items.Add(Me.CreateSampleLineDoubleBridge())
        End Sub

        #EndRegion

        #Region"Implementation"

        Private Function CreateSampleLine1() As Nevron.Nov.Diagram.NRoutableConnector
            Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            connector.MakeLine()
            connector.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.BlueViolet)
            connector.BeginX = 10
            connector.BeginY = 130
            connector.EndX = 250
            connector.EndY = 130
            Return connector
        End Function

        Private Function CreateSampleLine2() As Nevron.Nov.Diagram.NRoutableConnector
            Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            connector.MakeLine()
            connector.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Orange)
            connector.BeginX = 10
            connector.BeginY = 75
            connector.EndX = 280
            connector.EndY = 75
            Return connector
        End Function

        Private Function CreateSamplePolyline1() As Nevron.Nov.Diagram.NRoutableConnector
            Dim points As Nevron.Nov.Graphics.NPoint() = New Nevron.Nov.Graphics.NPoint() {New Nevron.Nov.Graphics.NPoint(10, 210), New Nevron.Nov.Graphics.NPoint(75, 10), New Nevron.Nov.Graphics.NPoint(75, 10), New Nevron.Nov.Graphics.NPoint(75, 175), New Nevron.Nov.Graphics.NPoint(145, 175), New Nevron.Nov.Graphics.NPoint(145, 10), New Nevron.Nov.Graphics.NPoint(210, 75), New Nevron.Nov.Graphics.NPoint(210, 210), New Nevron.Nov.Graphics.NPoint(105, 210), New Nevron.Nov.Graphics.NPoint(105, 105)}
            Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            connector.MakePolyline(points)
            connector.SetBeginPoint(points(0))
            connector.SetEndPoint(points(points.Length - 1))
            Return connector
        End Function

        Private Function CreateSamplePolyline2() As Nevron.Nov.Diagram.NRoutableConnector
            Dim points As Nevron.Nov.Graphics.NPoint() = New Nevron.Nov.Graphics.NPoint() {New Nevron.Nov.Graphics.NPoint(212, 250), New Nevron.Nov.Graphics.NPoint(174, 250), New Nevron.Nov.Graphics.NPoint(174, 169), New Nevron.Nov.Graphics.NPoint(242, 169), New Nevron.Nov.Graphics.NPoint(242, 208)}
            Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            connector.MakePolyline(points)
            connector.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.OrangeRed)
            connector.SetBeginPoint(points(0))
            connector.SetEndPoint(points(points.Length - 1))
            Return connector
        End Function

        Private Function CreateSampleLineDoubleBridge() As Nevron.Nov.Diagram.NRoutableConnector
            Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            connector.MakeLine()
            connector.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Green)
            connector.BeginX = 50
            connector.BeginY = 300
            connector.EndX = 206
            connector.EndY = 14
            Return connector
        End Function

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NLineJumpsExample.
        ''' </summary>
        Public Shared ReadOnly NLineJumpsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
