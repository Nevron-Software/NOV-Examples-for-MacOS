Imports System
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Graphics

Namespace Nevron.Nov.Examples.Diagram
	''' <summary>
	''' The NFamilyTreeTemplate class represents a family tree template
	''' </summary>
	Public Class NFamilyTreeTemplate
        Inherits NGraphTemplate
		#Region"Constructors"

		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
            MyBase.New("Family Tree")
            Me.m_nChildrenCount = 2
        End Sub


		#EndRegion

		#Region"Properties"

		''' <summary>
		''' Gets or sets the children count
		''' </summary>
		''' <remarks>
		''' By default set to 2
		''' </remarks>
		Public Property ChildrenCount As Integer
            Get
                Return Me.m_nChildrenCount
            End Get
            Set(ByVal value As Integer)
                If value < 0 Then Throw New System.ArgumentOutOfRangeException("The value must be >= 0")
                Me.m_nChildrenCount = value
                OnTemplateChanged()
            End Set
        End Property


		#EndRegion

		#Region"Overrides"

		''' <summary>
		''' Overriden to return the family tree description
		''' </summary>
		Public Overrides Function GetDescription() As String
            Dim description As String = "##Family tree with two parents and " & Me.m_nChildrenCount.ToString() & " " & (If(Me.m_nChildrenCount = 1, " child", " children")) & "."
            Return description
        End Function

		#EndRegion

		#Region"Protected overrides"

		''' <summary>
		''' Overriden to create the family tree template
		''' </summary>
		''' <paramname="document">document in which to create the template</param>
		Protected Overrides Sub CreateTemplate(ByVal document As Nevron.Nov.Diagram.NDrawingDocument)
            Dim pt As Nevron.Nov.Graphics.NPoint
            Dim node As Nevron.Nov.Diagram.NShape
            Dim edge As Nevron.Nov.Diagram.NShape = Nothing
            Dim page As Nevron.Nov.Diagram.NPage = document.Content.ActivePage
			
			' determine the elements dimensions
			Dim childrenWidth As Double = Me.m_nChildrenCount * m_VerticesSize.Width + (Me.m_nChildrenCount - 1) * m_fHorizontalSpacing
            Dim parentsWidth As Double = m_VerticesSize.Width * 2 + m_fHorizontalSpacing
			
			' determine the template dimensions
			Dim templateWidth As Double = System.Math.Max(childrenWidth, parentsWidth)
            Dim templateBounds As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(m_Origin.X, m_Origin.Y, templateWidth, m_VerticesSize.Height * 2 + m_fVerticalSpacing)
            Dim center As Nevron.Nov.Graphics.NPoint = templateBounds.Center

			' create the parent nodes
			Dim father As Nevron.Nov.Diagram.NShape = CreateVertex(m_VerticesShape)
            pt = New Nevron.Nov.Graphics.NPoint(center.X - (m_VerticesSize.Width + m_fHorizontalSpacing / 2), templateBounds.Y)
            father.SetBounds(New Nevron.Nov.Graphics.NRectangle(pt, m_VerticesSize))
            page.Items.AddChild(father)
            Dim mother As Nevron.Nov.Diagram.NShape = CreateVertex(m_VerticesShape)
            pt = New Nevron.Nov.Graphics.NPoint(center.X + m_fHorizontalSpacing / 2, templateBounds.Y)
            mother.SetBounds(New Nevron.Nov.Graphics.NRectangle(pt, m_VerticesSize))
            page.Items.AddChild(mother)

			' create the children
			If Me.m_nChildrenCount > 0 Then
                Dim childrenY As Double = templateBounds.Y + m_VerticesSize.Height + m_fVerticalSpacing

                For i As Integer = 0 To Me.m_nChildrenCount - 1
					' create the child
					node = CreateVertex(m_VerticesShape)
                    pt = New Nevron.Nov.Graphics.NPoint(i * (m_VerticesSize.Width + m_fHorizontalSpacing), childrenY)
                    node.SetBounds(New Nevron.Nov.Graphics.NRectangle(pt, m_VerticesSize))
                    page.Items.AddChild(node)

					' attach it to the parents
					edge = CreateEdge(Nevron.Nov.Diagram.Shapes.ENConnectorShape.BottomToTop1)
                    page.Items.AddChild(edge)
                    edge.GlueBeginToGeometryIntersection(father)
                    edge.GlueEndToShape(node)
                    edge = CreateEdge(Nevron.Nov.Diagram.Shapes.ENConnectorShape.BottomToTop1)
                    page.Items.AddChild(edge)
                    edge.GlueBeginToGeometryIntersection(mother)
                    edge.GlueEndToShape(node)
                Next
            End If
        End Sub


		#EndRegion

		#Region"Fields"

		Friend m_nChildrenCount As Integer

		#EndRegion
	End Class
End Namespace#Region"AUTO_CODE_BLOCK [NFamilyTreeTemplate]"
#EndRegion
