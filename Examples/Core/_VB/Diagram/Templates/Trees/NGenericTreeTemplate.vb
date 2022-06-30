Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout

Namespace Nevron.Nov.Examples.Diagram
    ''' <summary>
    ''' The NGenericTreeTemplate class represents a generic tree template
    ''' </summary>
    Public Class NGenericTreeTemplate
        Inherits NGraphTemplate
        #Region"Constructors"

        ''' <summary>
        ''' Default constructor
        ''' </summary>
        Public Sub New()
            MyBase.New("Generic Tree")
            Me.m_nLevels = 3
            Me.m_nBranchNodes = 2
            Me.m_nVertexSizeDeviation = 0.0F
            Me.m_bBalanced = True
            Me.m_LayoutDirection = Nevron.Nov.Layout.ENHVDirection.TopToBottom
            Me.m_LayoutType = ENTreeLayoutType.Layered
            Me.m_ConnectorShape = Nevron.Nov.Diagram.Shapes.ENConnectorShape.RoutableConnector
        End Sub

        #EndRegion

        #Region"Properties"

        ''' <summary>
        ''' Gets or sets the count of the child nodes for each branch
        ''' </summary>
        ''' <remarks>
        ''' By default set to 2
        ''' </remarks>
        Public Property BranchNodes As Integer
            Get
                Return Me.m_nBranchNodes
            End Get
            Set(ByVal value As Integer)
                If value = Me.m_nBranchNodes Then Return
                If value < 1 Then Throw New System.ArgumentOutOfRangeException("The value must be > 0.")
                Me.m_nBranchNodes = value
                OnTemplateChanged()
            End Set
        End Property
        ''' <summary>
        ''' Gets or sets the tree levels count
        ''' </summary>
        ''' <remarks>
        ''' By default set to 3 
        ''' </remarks>
        Public Property Levels As Integer
            Get
                Return Me.m_nLevels
            End Get
            Set(ByVal value As Integer)
                If value = Me.m_nLevels Then Return
                If value < 1 Then Throw New System.ArgumentOutOfRangeException("The value must be > 0.")
                Me.m_nLevels = value
                OnTemplateChanged()
            End Set
        End Property
        ''' <summary>
        ''' Gets or sets if the tree is balanced or not
        ''' </summary>
        ''' <remarks>
        ''' By default set to true 
        ''' </remarks>
        Public Property Balanced As Boolean
            Get
                Return Me.m_bBalanced
            End Get
            Set(ByVal value As Boolean)

                If Me.m_bBalanced <> value Then
                    Me.m_bBalanced = value
                    OnTemplateChanged()
                End If
            End Set
        End Property
        ''' <summary>
        ''' Specifies the tree layout expand
        ''' </summary>
        ''' <remarks>
        ''' By default set to TopToBottom
        ''' </remarks>
        Public Property LayoutDirection As Nevron.Nov.Layout.ENHVDirection
            Get
                Return Me.m_LayoutDirection
            End Get
            Set(ByVal value As Nevron.Nov.Layout.ENHVDirection)
                If value = Me.m_LayoutDirection Then Return
                Me.m_LayoutDirection = value
                OnTemplateChanged()
            End Set
        End Property
        ''' <summary>
        ''' Specifies the tree layout scheme
        ''' </summary>
        ''' <remarks>
        ''' By default set to NormalCompressed
        ''' </remarks>
        Public Property LayoutType As ENTreeLayoutType
            Get
                Return Me.m_LayoutType
            End Get
            Set(ByVal value As ENTreeLayoutType)
                If value Is Me.m_LayoutType Then Return
                Me.m_LayoutType = value
                OnTemplateChanged()
            End Set
        End Property
        ''' <summary>
        ''' Specifies the edge connector type
        ''' </summary>
        ''' <remarks>
        ''' By default set to DynamicPolyline
        ''' </remarks>
        Public Property ConnectorType As Nevron.Nov.Diagram.Shapes.ENConnectorShape
            Get
                Return Me.m_ConnectorShape
            End Get
            Set(ByVal value As Nevron.Nov.Diagram.Shapes.ENConnectorShape)

                If Me.m_ConnectorShape <> value Then
                    Me.m_ConnectorShape = value
                    OnTemplateChanged()
                End If
            End Set
        End Property
        ''' <summary>
        ''' Specifies the deviation of the vertex size according to the VerticesSize (0 for none)
        ''' </summary>
        ''' <remarks>
        ''' By default set to 0
        ''' </remarks>
        Public Property VertexSizeDeviation As Double
            Get
                Return Me.m_nVertexSizeDeviation
            End Get
            Set(ByVal value As Double)
                If value < 0 Then Throw New System.ArgumentOutOfRangeException("Negative values are not allowed.")

                If Me.m_nVertexSizeDeviation <> value Then
                    Me.m_nVertexSizeDeviation = value
                    OnTemplateChanged()
                End If
            End Set
        End Property

        #EndRegion

        #Region"Overrides"

        ''' <summary>
        ''' Overriden to return the tree description
        ''' </summary>
        Public Overrides Function GetDescription() As String
            Dim description As String = System.[String].Format("##Tree with {0} levels and {1} branch elements.", Me.m_nLevels, Me.m_nBranchNodes)
            Return description
        End Function

        #EndRegion

        #Region"Protected overrides"

        ''' <summary>
        ''' Overriden to create the tree template
        ''' </summary>
        ''' <paramname="document">document in which to create the template</param>
        Protected Overrides Sub CreateTemplate(ByVal document As Nevron.Nov.Diagram.NDrawingDocument)
            ' create the tree
            Dim elements As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = Me.CreateTree(document)
            If Me.m_LayoutType Is ENTreeLayoutType.None Then Return

            ' layout the tree
            Dim layout As Nevron.Nov.Diagram.Layout.NLayeredTreeLayout = New Nevron.Nov.Diagram.Layout.NLayeredTreeLayout()

            ' sync measurement unit 
            layout.OrthogonalEdgeRouting = False

            Select Case Me.m_LayoutType
                Case ENTreeLayoutType.Layered
                    layout.VertexSpacing = m_fHorizontalSpacing
                    layout.LayerSpacing = m_fVerticalSpacing
                Case ENTreeLayoutType.LayeredLeftAligned
                    layout.VertexSpacing = m_fHorizontalSpacing
                    layout.LayerSpacing = m_fVerticalSpacing
                    layout.ParentPlacement.Anchor = Nevron.Nov.Diagram.Layout.ENParentAnchor.SubtreeNear
                    layout.ParentPlacement.Alignment = Nevron.Nov.ENRelativeAlignment.Near
                Case ENTreeLayoutType.LayeredRightAligned
                    layout.VertexSpacing = m_fHorizontalSpacing
                    layout.LayerSpacing = m_fVerticalSpacing
                    layout.ParentPlacement.Anchor = Nevron.Nov.Diagram.Layout.ENParentAnchor.SubtreeFar
                    layout.ParentPlacement.Alignment = Nevron.Nov.ENRelativeAlignment.Far
            End Select

            ' apply layout
            Dim context As Nevron.Nov.Diagram.Layout.NDrawingLayoutContext = New Nevron.Nov.Diagram.Layout.NDrawingLayoutContext(document, New Nevron.Nov.Graphics.NRectangle(Origin, Nevron.Nov.Graphics.NSize.Zero))
            layout.Arrange(elements.CastAll(Of Object)(), context)
        End Sub

        #EndRegion

        #Region"Protected overridable"

        ''' <summary>
        ''' Gets the size for a new vertex taking into account the VertexSizeDeviation property.
        ''' </summary>
        ''' <returns></returns>
        Protected Overridable Function GetSize(ByVal rnd As System.Random) As Nevron.Nov.Graphics.NSize
            If Me.m_nVertexSizeDeviation = 0 Then Return m_VerticesSize
            Dim factor As Double = Me.m_nVertexSizeDeviation + 1
            Dim width As Double = rnd.[Next](CInt((m_VerticesSize.Width / factor)), CInt((m_VerticesSize.Width * factor)))
            Dim height As Double = rnd.[Next](CInt((m_VerticesSize.Height / factor)), CInt((m_VerticesSize.Height * factor)))
            Return New Nevron.Nov.Graphics.NSize(width, height)
        End Function
        ''' <summary>
        ''' Creates a tree in the specified document
        ''' </summary>
        ''' <paramname="document">document in which to create a tree</param>
        ''' <returns>tree elements</returns>
        Protected Overridable Function CreateTree(ByVal document As Nevron.Nov.Diagram.NDrawingDocument) As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape)
            Dim page As Nevron.Nov.Diagram.NPage = document.Content.ActivePage
            Dim elements As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape)()
            Dim cur As Nevron.Nov.Diagram.NShape = Nothing
            Dim edge As Nevron.Nov.Diagram.NShape = Nothing
            Dim curRowVertices As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = Nothing
            Dim prevRowVertices As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = Nothing
            Dim i, j, level As Integer
            Dim childrenCount, levelNodesCount As Integer
            Dim rnd As System.Random = New System.Random()

            For level = 1 To Me.m_nLevels
                curRowVertices = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape)()

                If Me.m_bBalanced Then
                    'Create a balanced tree
                    levelNodesCount = CInt(System.Math.Pow(Me.m_nBranchNodes, level - 1))

                    For i = 0 To levelNodesCount - 1
                        ' create the cur node
                        cur = CreateVertex(m_VerticesShape)
                        cur.SetBounds(New Nevron.Nov.Graphics.NRectangle(m_Origin, Me.GetSize(rnd)))
                        page.Items.AddChild(cur)
                        elements.Add(cur)

                        ' connect with ancestor
                        If level > 1 Then
                            edge = CreateEdge(Me.m_ConnectorShape)
                            page.Items.AddChild(edge)
                            Dim parentIndex As Integer = CInt(System.Math.Floor(CDbl((i / Me.m_nBranchNodes))))
                            edge.GlueBeginToGeometryIntersection(prevRowVertices(parentIndex))
                            edge.GlueEndToShape(cur)
                        End If

                        curRowVertices.Add(cur)
                    Next
                Else
                    'Create an unbalanced tree
                    If level = 1 Then
                        ' Create the current node
                        cur = CreateVertex(m_VerticesShape)
                        cur.SetBounds(New Nevron.Nov.Graphics.NRectangle(m_Origin, Me.GetSize(rnd)))
                        page.Items.AddChild(cur)
                        elements.Add(cur)
                        curRowVertices.Add(cur)
                    Else
                        levelNodesCount = prevRowVertices.Count

                        Do
                            ' Ensure that the desired level depth is reached
                            For i = 0 To levelNodesCount - 1
                                childrenCount = rnd.[Next](0, Me.m_nBranchNodes + 1)

                                For j = 0 To childrenCount - 1
                                    ' Create the current node
                                    cur = CreateVertex(m_VerticesShape)
                                    cur.SetBounds(New Nevron.Nov.Graphics.NRectangle(m_Origin, Me.GetSize(rnd)))
                                    page.Items.AddChild(cur)
                                    elements.Add(cur)
                                    curRowVertices.Add(cur)

                                    ' Connect with ancestor
                                    edge = CreateEdge(Me.m_ConnectorShape)
                                    page.Items.AddChild(edge)
                                    edge.GlueBeginToGeometryIntersection(prevRowVertices(i))
                                    edge.GlueEndToShape(cur)
                                Next
                            Next
                        Loop While level < Me.m_nLevels AndAlso curRowVertices.Count = 0
                    End If
                End If

                prevRowVertices = curRowVertices
            Next

            Return elements
        End Function

        #EndRegion

        #Region"Fields"

        Friend m_nLevels As Integer
        Friend m_nBranchNodes As Integer
        Friend m_bBalanced As Boolean
        Friend m_LayoutDirection As Nevron.Nov.Layout.ENHVDirection
        Friend m_LayoutType As ENTreeLayoutType
        Friend m_ConnectorShape As Nevron.Nov.Diagram.Shapes.ENConnectorShape
        Friend m_nVertexSizeDeviation As Double

        #EndRegion
    End Class
End Namespace
