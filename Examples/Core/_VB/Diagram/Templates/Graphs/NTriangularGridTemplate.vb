Imports System
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.DataStructures

Namespace Nevron.Nov.Examples.Diagram
	''' <summary>
	''' The NTriangularGridTemplate class represents a triangular grid template
	''' </summary>
	Public Class NTriangularGridTemplate
        Inherits NGraphTemplate
        #Region"Constructors"

        ''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
            MyBase.New("Triangular Grid")
            Me.m_nLevels = 3
            Me.m_bConnectGrid = True
        End Sub

        #EndRegion

        #Region"Properties"

        ''' <summary>
		''' Gets or sets the levels count of the triangluar grid
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
		''' Overriden to return the triangular grid description
		''' </summary>
		Public Overrides Function GetDescription() As String
            Dim description As String = System.[String].Format("##Triangular grid graph with {0} levels.", Me.m_nLevels)
            If Me.m_bConnectGrid Then description += " " & "##Each cell has two child cells and is connected with them as well as with the adjacent cells from the same level."
            Return description
        End Function

		#EndRegion

		#Region"Protected overrides"

		''' <summary>
		''' Overriden to create the triangular grid template in the specified document
		''' </summary>
		''' <paramname="document">document in which to create the template</param>
		Protected Overrides Sub CreateTemplate(ByVal document As Nevron.Nov.Diagram.NDrawingDocument)
            Dim page As Nevron.Nov.Diagram.NPage = document.Content.ActivePage
            Dim templateBounds As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(m_Origin.X, m_Origin.Y, Me.m_nLevels * m_VerticesSize.Width + (Me.m_nLevels - 1) * m_fHorizontalSpacing, Me.m_nLevels * m_VerticesSize.Height + (Me.m_nLevels - 1) * m_fVerticalSpacing)
            Dim location As Nevron.Nov.Graphics.NPoint
            Dim cur As Nevron.Nov.Diagram.NShape = Nothing, prev As Nevron.Nov.Diagram.NShape = Nothing
            Dim edge As Nevron.Nov.Diagram.NShape = Nothing
            Dim curRowNodes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = Nothing
            Dim prevRowNodes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = Nothing

            For level As Integer = 1 To Me.m_nLevels
				' determine the location of the first node in the level
				location = New Nevron.Nov.Graphics.NPoint(templateBounds.X + (templateBounds.Width - level * m_VerticesSize.Width - (level - 1) * m_fHorizontalSpacing) / 2, templateBounds.Y + (level - 1) * (m_VerticesSize.Height + m_fVerticalSpacing))
                curRowNodes = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape)()

                For i As Integer = 0 To level - 1
                    cur = CreateVertex(m_VerticesShape)
                    cur.SetBounds(New Nevron.Nov.Graphics.NRectangle(location, m_VerticesSize))
                    page.Items.AddChild(cur)
                    location.X += m_VerticesSize.Width + m_fHorizontalSpacing

					' connect the current node with its ancestors and prev node
					If Me.m_bConnectGrid = False Then Continue For
					
					' connect with prev
					If i > 0 Then
                        edge = CreateEdge(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
                        page.Items.AddChild(edge)
                        edge.GlueBeginToGeometryIntersection(prev)
                        edge.GlueEndToShape(cur)
                    End If

					' connect with ancestors
					If level > 1 Then
                        If i < prevRowNodes.Count Then
                            edge = CreateEdge(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
                            page.Items.AddChild(edge)
                            edge.GlueBeginToGeometryIntersection(CType(prevRowNodes(i), Nevron.Nov.Diagram.NShape))
                            edge.GlueEndToShape(cur)
                        End If

                        If i > 0 Then
                            edge = CreateEdge(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
                            page.Items.AddChild(edge)
                            edge.GlueBeginToGeometryIntersection(CType(prevRowNodes(i - 1), Nevron.Nov.Diagram.NShape))
                            edge.GlueEndToShape(cur)
                        End If
                    End If

                    curRowNodes.Add(cur)
                    prev = cur
                Next

                prevRowNodes = curRowNodes
            Next
        End Sub


		#EndRegion

		#Region"Fields"

		Friend m_nLevels As Integer
        Friend m_bConnectGrid As Boolean

		#EndRegion
	End Class
End Namespace
