Imports System
Imports System.Collections.Generic
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.DataStructures

Namespace Nevron.Nov.Examples.Diagram
    ''' <summary>
    ''' Generates a connnected random graph.
    ''' </summary>
    Public Class NRandomGraphTemplate
        Inherits NGraphTemplate
        #Region"Constructors"

        ''' <summary>
        ''' Default constructor.
        ''' </summary>
        Public Sub New()
            MyBase.New("Random Graph")
            Me.m_nVertexCount = 10
            Me.m_nEdgeCount = 15
            Me.m_MinVerticesSize = m_VerticesSize
            Me.m_MaxVerticesSize = m_VerticesSize
        End Sub

        #EndRegion

        #Region"Properties"

        ''' <summary>
        ''' The number of edges to generate.
        ''' </summary>
        Public Property EdgeCount As Integer
            Get
                Return Me.m_nEdgeCount
            End Get
            Set(ByVal value As Integer)

                If Me.m_nEdgeCount <> value Then
                    Me.m_nEdgeCount = value
                    OnTemplateChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' The number of vertices to generate.
        ''' </summary>
        Public Property VertexCount As Integer
            Get
                Return Me.m_nVertexCount
            End Get
            Set(ByVal value As Integer)

                If Me.m_nVertexCount <> value Then
                    Me.m_nVertexCount = value
                    OnTemplateChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' The minimal size of a vertex in the graph.
        ''' </summary>
        Public Property MinVerticesSize As Nevron.Nov.Graphics.NSize
            Get
                Return Me.m_MinVerticesSize
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NSize)

                If Me.m_MinVerticesSize <> value Then
                    If value.Width > Me.m_MaxVerticesSize.Width OrElse value.Height > Me.m_MaxVerticesSize.Height Then
                        Throw New System.ArgumentException("MinVerticesSize must be smaller than or equal to MaxVertexSize")
                    End If

                    Me.m_MinVerticesSize = value
                    OnTemplateChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' The maximal size of a vertex in the graph.
        ''' </summary>
        Public Property MaxVerticesSize As Nevron.Nov.Graphics.NSize
            Get
                Return Me.m_MaxVerticesSize
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NSize)

                If Me.m_MaxVerticesSize <> value Then
                    If value.Width < Me.m_MinVerticesSize.Width OrElse value.Height < Me.m_MinVerticesSize.Height Then
                        Throw New System.ArgumentException("MexVerticesSize must be greater than or equal to MinVerticesSize")
                    End If

                    Me.m_MaxVerticesSize = value
                    OnTemplateChanged()
                End If
            End Set
        End Property

        #EndRegion

        #Region"Overrides"

        ''' <summary>
        ''' Overriden to provide a description of the template.
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function GetDescription() As String
            Return String.Format("##Generates a random graph with {0} vertices and {1} edges.", Me.m_nVertexCount, Me.m_nEdgeCount)
        End Function
        ''' <summary>
        ''' Overriden to create a random graph template in the specified document.
        ''' </summary>
        ''' <paramname="document">The document to create a graph in.</param>
        Protected Overrides Sub CreateTemplate(ByVal document As Nevron.Nov.Diagram.NDrawingDocument)
            If Me.m_nEdgeCount < Me.m_nVertexCount - 1 Then Throw New System.Exception("##The number of edges must be greater than or equal to the (number of vertices - 1) in order to generate a connected graph")
            If Me.m_nEdgeCount > Nevron.Nov.Examples.Diagram.NRandomGraphTemplate.MaxEdgeCount(Me.m_nVertexCount) Then Throw New System.Exception("##Too many edges wanted for the graph")
            Dim i As Integer
            Dim random As System.Random = New System.Random()
            Dim activePage As Nevron.Nov.Diagram.NPage = document.Content.ActivePage
            Dim vertices As Nevron.Nov.Diagram.NShape() = New Nevron.Nov.Diagram.NShape(Me.m_nVertexCount - 1) {}
            Dim edges As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Graphics.NPointI) = Nevron.Nov.Examples.Diagram.NRandomGraphTemplate.GetRandomMST(Me.m_nVertexCount)
            Dim edgeInfo As Nevron.Nov.Graphics.NPointI
            Dim minSize As Nevron.Nov.Graphics.NSizeI = Me.m_MinVerticesSize.Round()
            Dim maxSize As Nevron.Nov.Graphics.NSizeI = Me.m_MaxVerticesSize.Round()
            maxSize.Width += 1
            maxSize.Height += 1

            ' Create the vertices
            For i = 0 To Me.m_nVertexCount - 1
                vertices(i) = CreateVertex(m_VerticesShape)
                Dim width As Double = random.[Next](minSize.Width, maxSize.Width)
                Dim height As Double = random.[Next](minSize.Height, maxSize.Height)
                vertices(CInt((i))).SetBounds(New Nevron.Nov.Graphics.NRectangle(0, 0, width, height))
                activePage.Items.AddChild(vertices(i))
            Next

            ' Generate the edges
            For i = Me.m_nVertexCount - 1 To Me.m_nEdgeCount - 1

                Do   ' Generate a new edge
                    edgeInfo = New Nevron.Nov.Graphics.NPointI(random.[Next](Me.m_nVertexCount), random.[Next](Me.m_nVertexCount))
                Loop While edgeInfo.X = edgeInfo.Y OrElse edges.Contains(edgeInfo) OrElse edges.Contains(New Nevron.Nov.Graphics.NPointI(edgeInfo.Y, edgeInfo.X))

                edges.Add(edgeInfo)
            Next

            ' Create the edges
            For i = 0 To Me.m_nEdgeCount - 1
                edgeInfo = edges(i)
                Dim edge As Nevron.Nov.Diagram.NShape = CreateEdge(Nevron.Nov.Diagram.Shapes.ENConnectorShape.RoutableConnector)
                activePage.Items.AddChild(edge)
                edge.GlueBeginToGeometryIntersection(vertices(edgeInfo.X))
                edge.GlueEndToShape(vertices(edgeInfo.Y))
            Next

            ' Apply a table layout to the generated graph
            Dim tableLayout As Nevron.Nov.Layout.NTableFlowLayout = New Nevron.Nov.Layout.NTableFlowLayout()
            tableLayout.MaxOrdinal = CInt(System.Math.Sqrt(Me.m_nVertexCount)) + 1
            tableLayout.HorizontalSpacing = m_VerticesSize.Width / 5
            tableLayout.VerticalSpacing = m_VerticesSize.Width / 5
            Dim context As Nevron.Nov.Diagram.Layout.NDrawingLayoutContext = New Nevron.Nov.Diagram.Layout.NDrawingLayoutContext(document, activePage)
            tableLayout.Arrange(New Nevron.Nov.DataStructures.NList(Of Object)(Nevron.Nov.DataStructures.NArrayHelpers(Of Nevron.Nov.Diagram.NShape).CastAll(Of Object)(vertices)), context)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_nEdgeCount As Integer
        Private m_nVertexCount As Integer
        Private m_MinVerticesSize As Nevron.Nov.Graphics.NSize
        Private m_MaxVerticesSize As Nevron.Nov.Graphics.NSize

        #EndRegion

        #Region"Static"

        ''' <summary>
        ''' Returns the maximum number of edges for the specified number of vertices.
        ''' </summary>
        ''' <paramname="vertexCount"></param>
        ''' <returns></returns>
        Private Shared Function MaxEdgeCount(ByVal vertexCount As Integer) As Integer
            Return (vertexCount * (vertexCount - 1)) / 2
        End Function
        ''' <summary>
        ''' Creates a random minimum spannig tree (this ensures that the graph will be connected).
        ''' </summary>
        ''' <returns></returns>
        Private Shared Function GetRandomMST(ByVal vertexCount As Integer) As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Graphics.NPointI)
            Dim i, v1, v2 As Integer
            Dim random As System.Random = New System.Random()
            Dim edges As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Graphics.NPointI) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Graphics.NPointI)()
            Dim usedVertices As Nevron.Nov.DataStructures.NList(Of Integer) = New Nevron.Nov.DataStructures.NList(Of Integer)()
            Dim unusedVertices As Nevron.Nov.DataStructures.NList(Of Integer) = New Nevron.Nov.DataStructures.NList(Of Integer)(vertexCount)

            For i = 0 To vertexCount - 1
                unusedVertices.Add(i)
            Next

            ' Determine the root
            v1 = random.[Next](vertexCount)
            usedVertices.Add(v1)
            unusedVertices.RemoveAt(v1)

            For i = 1 To vertexCount - 1
                v1 = random.[Next](usedVertices.Count)
                v2 = random.[Next](unusedVertices.Count)
                edges.Add(New Nevron.Nov.Graphics.NPointI(usedVertices(v1), unusedVertices(v2)))
                usedVertices.Add(unusedVertices(v2))
                unusedVertices.RemoveAt(v2)
            Next

            Return edges
        End Function

        #EndRegion
    End Class
End Namespace
