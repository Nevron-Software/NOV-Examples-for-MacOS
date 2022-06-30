Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Diagram.Batches
Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI
Imports Nevron.Nov.DataStructures

Namespace Nevron.Nov.Examples.Diagram
    ''' <summary>
    ''' 
    ''' </summary>
    Public Class NGroupsExample
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
            Nevron.Nov.Examples.Diagram.NGroupsExample.NGroupsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NGroupsExample), NExampleBaseSchema)
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
            Return "Demonstrates how to create and use groups."
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' create networks
            Dim network1 As Nevron.Nov.Diagram.NGroup = Me.CreateNetwork(New Nevron.Nov.Graphics.NPoint(200, 20), "Network 1")
            Dim network2 As Nevron.Nov.Diagram.NGroup = Me.CreateNetwork(New Nevron.Nov.Graphics.NPoint(400, 250), "Network 2")
            Dim network3 As Nevron.Nov.Diagram.NGroup = Me.CreateNetwork(New Nevron.Nov.Graphics.NPoint(20, 250), "Network 3")
            Dim network4 As Nevron.Nov.Diagram.NGroup = Me.CreateNetwork(New Nevron.Nov.Graphics.NPoint(200, 475), "Network 4")

            ' connect networks
            Me.ConnectNetworks(network1, network2)
            Me.ConnectNetworks(network1, network3)
            Me.ConnectNetworks(network1, network4)

            ' hide some elements
            drawingDocument.Content.ScreenVisibility.ShowPorts = False
            drawingDocument.Content.ScreenVisibility.ShowGrid = False
        End Sub

        #EndRegion

        #Region"Implementation"

        Protected Function CreateNetwork(ByVal location As Nevron.Nov.Graphics.NPoint, ByVal labelText As String) As Nevron.Nov.Diagram.NGroup
            Dim rectValid As Boolean = False
            Dim rect As Nevron.Nov.Graphics.NRectangle = Nevron.Nov.Graphics.NRectangle.Zero
            Dim activePage As Nevron.Nov.Diagram.NPage = Me.m_DrawingView.ActivePage
            Dim shapes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = activePage.GetShapes(False)

            For i As Integer = 0 To shapes.Count - 1
                Dim bounds As Nevron.Nov.Graphics.NRectangle = shapes(CInt((i))).GetAlignBoxInPage()

                If rectValid Then
                    rect = Nevron.Nov.Graphics.NRectangle.Union(rect, bounds)
                Else
                    rect = bounds
                    rectValid = True
                End If
            Next

            If rectValid Then
                ' determine how much is out of layout area
            End If

            ' create computer1
            Dim computer1 As Nevron.Nov.Diagram.NShape = Me.CreateComputer()
            computer1.SetBounds(0, 0, computer1.Width, computer1.Height)

            ' create computer2
            Dim computer2 As Nevron.Nov.Diagram.NShape = Me.CreateComputer()
            computer2.SetBounds(150, 0, computer2.Width, computer2.Height)

            ' create computer3
            Dim computer3 As Nevron.Nov.Diagram.NShape = Me.CreateComputer()
            computer3.SetBounds(75, 120, computer3.Width, computer3.Height)

            ' create the group that contains the comptures
            Dim group As Nevron.Nov.Diagram.NGroup = New Nevron.Nov.Diagram.NGroup()
            Dim batchGroup As Nevron.Nov.Diagram.Batches.NBatchGroup = New Nevron.Nov.Diagram.Batches.NBatchGroup(Me.m_DrawingView.Document)
            batchGroup.Build(computer1, computer2, computer3)
            batchGroup.Group(Me.m_DrawingView.ActivePage, group)

            ' connect the computers in the network
            Me.ConnectComputers(computer1, computer2, group)
            Me.ConnectComputers(computer2, computer3, group)
            Me.ConnectComputers(computer3, computer1, group)

            ' insert a frame
            Dim drawRect As Nevron.Nov.Diagram.NDrawRectangle = New Nevron.Nov.Diagram.NDrawRectangle(0, 0, 1, 1)
            drawRect.Relative = True
            group.Geometry.Add(drawRect)

            ' change group fill style
            group.Geometry.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.FromCenter, Nevron.Nov.Graphics.ENGradientVariant.Variant2, Nevron.Nov.Graphics.NColor.Gainsboro, Nevron.Nov.Graphics.NColor.White)

            ' reposition and resize the group
            group.SetBounds(location.X, location.Y, group.Width, group.Height)

            ' set label text
            group.TextBlock.Text = labelText
            Return group
        End Function

        Protected Function CreateComputer() As Nevron.Nov.Diagram.NShape
            Dim networkShapes As Nevron.Nov.Diagram.Shapes.NNetworkShapeFactory = New Nevron.Nov.Diagram.Shapes.NNetworkShapeFactory()
            Dim computerShape As Nevron.Nov.Diagram.NShape = networkShapes.CreateShape(CInt(Nevron.Nov.Diagram.Shapes.ENNetworkShape.Computer))
            Return computerShape
        End Function

        Private Sub ConnectNetworks(ByVal fromNetwork As Nevron.Nov.Diagram.NGroup, ByVal toNetwork As Nevron.Nov.Diagram.NGroup)
            Dim connectorShapes As Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory()
            Dim lineShape As Nevron.Nov.Diagram.NShape = connectorShapes.CreateShape(CInt(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line))
            lineShape.GlueBeginToShape(fromNetwork)
            lineShape.GlueEndToShape(toNetwork)
            Me.m_DrawingView.ActivePage.Items.Add(lineShape)
        End Sub

        Private Sub ConnectComputers(ByVal fromComputer As Nevron.Nov.Diagram.NShape, ByVal toComputer As Nevron.Nov.Diagram.NShape, ByVal group As Nevron.Nov.Diagram.NGroup)
            Dim connectorShapes As Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory()
            Dim lineShape As Nevron.Nov.Diagram.NShape = connectorShapes.CreateShape(CInt(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line))
            lineShape.GlueBeginToShapeBoxIntersection(fromComputer)
            lineShape.GlueEndToShapeBoxIntersection(toComputer)
            group.Shapes.Add(lineShape)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NGroupsExample.
        ''' </summary>
        Public Shared ReadOnly NGroupsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
