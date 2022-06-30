Imports System
Imports System.Collections
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Graphics

Namespace Nevron.Nov.Examples.Diagram
	''' <summary>
	''' The NEllipticalGridTemplate class represents a rectangular grid template
	''' </summary>
	Public Class NRectangularGridTemplate
        Inherits NGraphTemplate
        #Region"Constructors"

        ''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
            MyBase.New("Rectangular Grid")
            Me.m_nRows = 3
            Me.m_nColumns = 3
            Me.m_bConnectGrid = True
        End Sub

        #EndRegion

        #Region"Properties"

        ''' <summary>
		''' Gets or sets the columns count
		''' </summary>
		''' <remarks>
		''' By default set to 3
		''' </remarks>
		Public Property ColumnCount As Integer
            Get
                Return Me.m_nColumns
            End Get
            Set(ByVal value As Integer)
                If value = Me.m_nColumns Then Return
                If value < 1 Then Throw New System.ArgumentOutOfRangeException("The value must be > 0.")
                Me.m_nColumns = value
                OnTemplateChanged()
            End Set
        End Property

		''' <summary>
		''' Gets or sets the rows count
		''' </summary>
		''' <remarks>
		''' By default set to 3
		''' </remarks>
		Public Property RowCount As Integer
            Get
                Return Me.m_nRows
            End Get
            Set(ByVal value As Integer)
                If value = Me.m_nRows Then Return
                If value < 1 Then Throw New System.ArgumentOutOfRangeException("The value must be > 0.")
                Me.m_nRows = value
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
		''' Overriden to return the rectangular grid description
		''' </summary>
		Public Overrides Function GetDescription() As String
            Dim description As String = System.[String].Format("##Rectangular grid graph with {0} columns and {1} rows.", Me.m_nColumns, Me.m_nRows)
            If Me.m_bConnectGrid Then description += " " & "##Each cell is connected with the horizontally and vertically adjacent cells."
            Return description
        End Function

		''' <summary>
		''' Overriden to create the rectangular grid template in the specified document
		''' </summary>
		''' <paramname="document">document in which to create the template</param>
		Protected Overrides Sub CreateTemplate(ByVal document As Nevron.Nov.Diagram.NDrawingDocument)
            Dim page As Nevron.Nov.Diagram.NPage = document.Content.ActivePage
            Dim edge As Nevron.Nov.Diagram.NShape = Nothing
            Dim vertex As Nevron.Nov.Diagram.NShape
            Dim vertexGrid As Nevron.Nov.Diagram.NShape(,) = New Nevron.Nov.Diagram.NShape(Me.m_nRows - 1, Me.m_nColumns - 1) {}

            For row As Integer = 0 To Me.m_nRows - 1

                For col As Integer = 0 To Me.m_nColumns - 1
					' create the vertex
					vertex = CreateVertex(m_VerticesShape)
                    vertex.SetBounds(New Nevron.Nov.Graphics.NRectangle(m_Origin.X + col * (m_VerticesSize.Width + m_fHorizontalSpacing), m_Origin.Y + row * (m_VerticesSize.Height + m_fVerticalSpacing), m_VerticesSize.Width, m_VerticesSize.Height))
                    page.Items.AddChild(vertex)

					' connect it with its X and Y predecessors
					If Me.m_bConnectGrid = False Then Continue For
                    vertexGrid(row, col) = vertex

					' connect X 
					If col > 0 Then
                        edge = CreateEdge(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
                        page.Items.AddChild(edge)
                        edge.GlueBeginToGeometryIntersection(vertexGrid(row, col - 1))
                        edge.GlueEndToShape(vertex)
                    End If

					' connect Y
					If row > 0 Then
                        edge = CreateEdge(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
                        page.Items.AddChild(edge)
                        edge.GlueBeginToGeometryIntersection(vertexGrid(row - 1, col))
                        edge.GlueEndToShape(vertex)
                    End If
                Next
            Next
        End Sub


		#EndRegion

		#Region"Fields"

		Friend m_nRows As Integer
        Friend m_nColumns As Integer
        Friend m_bConnectGrid As Boolean

		#EndRegion
	End Class
End Namespace
