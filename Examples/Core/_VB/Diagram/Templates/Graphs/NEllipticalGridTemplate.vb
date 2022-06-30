Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Graphics

Namespace Nevron.Nov.Examples.Diagram
	''' <summary>
	''' The NEllipticalGridTemplate class represents an elliptical grid template
	''' </summary>
	Public Class NEllipticalGridTemplate
        Inherits NGraphTemplate
		#Region"Constructors"

		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
            MyBase.New("Elliptical Grid")
            Me.m_nRimNodesCount = 6
            Me.m_dRadiusY = 100
            Me.m_dRadiusX = 100
            Me.m_bHasCenter = True
            Me.m_bConnectGrid = True
        End Sub

		#EndRegion

		#Region"Properties"

		''' <summary>
		''' Gets or sets the count of the nodes on the ellipse rim
		''' </summary>
		''' <remarks>
		''' By default set to 6
		''' </remarks>
		Public Property RimNodesCount As Integer
            Get
                Return Me.m_nRimNodesCount
            End Get
            Set(ByVal value As Integer)
                If value = Me.m_nRimNodesCount Then Return
                If value < 3 Then Throw New System.ArgumentOutOfRangeException("The value must be > 2.")
                Me.m_nRimNodesCount = value
                OnTemplateChanged()
            End Set
        End Property

		''' <summary>
		''' Controls the X radius of the ellipse
		''' </summary>		
		Public Property RadiusX As Double
            Get
                Return Me.m_dRadiusX
            End Get
            Set(ByVal value As Double)
                If value = Me.m_dRadiusX Then Return
                If value <= 0 Then Throw New System.ArgumentOutOfRangeException("The value must be > 0.")
                Me.m_dRadiusX = value
                OnTemplateChanged()
            End Set
        End Property

		''' <summary>
		''' Controls the Y radius of the ellipse
		''' </summary>		
		Public Property RadiusY As Double
            Get
                Return Me.m_dRadiusY
            End Get
            Set(ByVal value As Double)
                If value = Me.m_dRadiusY Then Return
                If value <= 0 Then Throw New System.ArgumentOutOfRangeException("The value must be > 0.")
                Me.m_dRadiusY = value
                OnTemplateChanged()
            End Set
        End Property

		''' <summary>
		''' Specifies whether the grid has a center node
		''' </summary>
		''' <remarks>
		''' By default set to true
		''' </remarks>
		Public Property HasCenter As Boolean
            Get
                Return Me.m_bHasCenter
            End Get
            Set(ByVal value As Boolean)
                If value = Me.m_bHasCenter Then Return
                Me.m_bHasCenter = value
                OnTemplateChanged()
            End Set
        End Property

		''' <summary>
		''' Specifies whether the grid vertices are connected
		''' </summary>
		''' <remarks>
		''' By default set to true
		''' </remarks>
		Public Property ConnectGrid As Boolean
            Get
                Return Me.m_bConnectGrid
            End Get
            Set(ByVal value As Boolean)
                If value = Me.m_bConnectGrid Then Return
                Me.m_bConnectGrid = value
                OnTemplateChanged()
            End Set
        End Property

		#EndRegion

		#Region"Overrides"

		''' <summary>
		''' Overriden to return the elliptical grid description
		''' </summary>
		Public Overrides Function GetDescription() As String
            Dim description As String = System.[String].Format("##Elliptical grid graph with {0} nodes on the rim.", Me.m_nRimNodesCount)

            If Me.m_bHasCenter Then
                description += " " & "##Has center node."
            End If

            If Me.m_bConnectGrid Then
                description += " " & "##Each node is connected with the next node on the rim."
                If Me.m_bHasCenter Then description += " " & "##Each node is also connected with the center node."
            End If

            Return description
        End Function

		#EndRegion

		#Region"Protected overrides"

		''' <summary>
		''' Overriden to create the elliptical grid template in the specified document
		''' </summary>
		''' <paramname="document">document in which to create the template</param>
		Protected Overrides Sub CreateTemplate(ByVal document As Nevron.Nov.Diagram.NDrawingDocument)
            Dim i As Integer
            Dim node As Nevron.Nov.Diagram.NShape
            Dim edge As Nevron.Nov.Diagram.NShape = Nothing
            Dim pt As Nevron.Nov.Graphics.NPoint
            Dim nodes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape)()
            Dim page As Nevron.Nov.Diagram.NPage = document.Content.ActivePage

			' create the ellipse nodes
			Dim curAngle As Double = 0
            Dim stepAngle As Double = Nevron.Nov.NMath.PI2 / Me.m_nRimNodesCount
            Dim center As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(m_Origin.X + Me.m_dRadiusX + m_VerticesSize.Width / 2, m_Origin.Y + Me.m_dRadiusY + m_VerticesSize.Height / 2)

            For i = 0 To Me.m_nRimNodesCount - 1
                pt = New Nevron.Nov.Graphics.NPoint(center.X + Me.m_dRadiusX * CDbl(System.Math.Cos(curAngle)) - m_VerticesSize.Width / 2, center.Y + Me.m_dRadiusY * CDbl(System.Math.Sin(curAngle)) - m_VerticesSize.Height / 2)
                node = CreateVertex(m_VerticesShape)
                node.SetBounds(New Nevron.Nov.Graphics.NRectangle(pt, m_VerticesSize))
                page.Items.AddChild(node)
                nodes.Add(node)
                curAngle += stepAngle
            Next

			' connect the ellipse nodes
			If Me.m_bConnectGrid Then
                For i = 0 To Me.m_nRimNodesCount - 1
                    edge = CreateEdge(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
                    page.Items.AddChild(edge)
                    edge.GlueBeginToGeometryIntersection(nodes(i))
                    edge.GlueEndToGeometryIntersection(nodes((i + 1) Mod Me.m_nRimNodesCount))
                Next
            End If

            If Me.m_bHasCenter = False Then Return

			' create the center
			node = CreateVertex(m_VerticesShape)
            pt = New Nevron.Nov.Graphics.NPoint(center.X - m_VerticesSize.Width / 2, center.Y - m_VerticesSize.Height / 2)
            node.SetBounds(New Nevron.Nov.Graphics.NRectangle(pt, m_VerticesSize))
            page.Items.AddChild(node)

			' connect the ellipse nodes with the center
			If Me.m_bConnectGrid Then
                For i = 0 To Me.m_nRimNodesCount - 1
                    edge = CreateEdge(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
                    page.Items.AddChild(edge)
                    edge.GlueBeginToGeometryIntersection(node)
                    edge.GlueEndToGeometryIntersection(nodes(i))
                Next
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Friend m_nRimNodesCount As Integer
        Friend m_dRadiusY As Double
        Friend m_dRadiusX As Double
        Friend m_bHasCenter As Boolean
        Friend m_bConnectGrid As Boolean

		#EndRegion
	End Class
End Namespace
