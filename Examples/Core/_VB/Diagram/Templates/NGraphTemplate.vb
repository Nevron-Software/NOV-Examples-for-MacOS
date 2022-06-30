Imports System
Imports System.Globalization
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes

Namespace Nevron.Nov.Examples.Diagram
    ''' <summary>
    ''' The NGraphTemplate class is a template, which serves as base class for all templates which create graph
    ''' </summary>
    ''' <remarks>
    ''' It enhances its base with the following features:
    ''' <listtype="bullet">
    ''' <item>
    ''' 		<term>Vertex style and Edge style attributes</term>
    ''' 		<description>Exposed by the VerticesUserClass and EdgesUserClass properties
    ''' 		</description>
    ''' 	</item>
    ''' <item>
    ''' 		<term>Control over the vertices shape and size</term>
    ''' 		<description>Exposed by the VerticesSize and VerticesShape properties
    ''' 		</description>
    ''' 	</item>
    ''' <item>
    ''' 		<term>Generic spacing control</term>
    ''' 		<description>Exposed by the HorizontalSpacing and VerticalSpacing properties
    ''' 		</description>
    ''' 	</item>
    ''' <item>
    ''' 		<term>Ability to create new vertices and edges which conform to the template settings</term>
    ''' 		<description>
    ''' 		Achieved with the help of the CreateLineGraphEdge and CreateGraphVertex methods.
    ''' 		</description>
    ''' 	</item>
    ''' 	</list>
    ''' </remarks>
    Public MustInherit Class NGraphTemplate
        Inherits NTemplate
        #Region"Constructors"

        ''' <summary>
        ''' Default constructor
        ''' </summary>
        Public Sub New()
            Me.Initialize()
        End Sub
        ''' <summary>
        ''' Initializer constructor
        ''' </summary>
        ''' <paramname="name">template name</param>
        Public Sub New(ByVal name As String)
            MyBase.New(name)
            Me.Initialize()
        End Sub
        ''' <summary>
        ''' Static constructor.
        ''' </summary>
        Shared Sub New()
            Nevron.Nov.Examples.Diagram.NGraphTemplate.CurrentEdgeIndex = 1
            Nevron.Nov.Examples.Diagram.NGraphTemplate.CurrentVertexIndex = 1
        End Sub

        #EndRegion

        #Region"Properties"

        ''' <summary>
        ''' Gets or sets the size of the vertices constructed by this template
        ''' </summary>
        Public Property VerticesSize As Nevron.Nov.Graphics.NSize
            Get
                Return Me.m_VerticesSize
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NSize)
                If value = Me.m_VerticesSize Then Return
                If value.Width <= 0 OrElse value.Height <= 0 Then Throw New System.ArgumentOutOfRangeException()
                Me.m_VerticesSize = value
                OnTemplateChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the shape of the vertices constructed by this template
        ''' </summary>
        ''' <remarks>
        ''' By default set to Rectangle
        ''' </remarks>
        Public Property VerticesShape As Nevron.Nov.Diagram.Shapes.ENBasicShape
            Get
                Return Me.m_VerticesShape
            End Get
            Set(ByVal value As Nevron.Nov.Diagram.Shapes.ENBasicShape)
                If Me.m_VerticesShape = value Then Return
                Me.m_VerticesShape = value
                OnTemplateChanged()
            End Set
        End Property
        ''' <summary>
        ''' Gets or sets the horizontal spacing between vertices
        ''' </summary>
        Public Property HorizontalSpacing As Double
            Get
                Return Me.m_fHorizontalSpacing
            End Get
            Set(ByVal value As Double)
                If value = Me.m_fHorizontalSpacing Then Return
                If value < 0 Then Throw New System.ArgumentOutOfRangeException()
                Me.m_fHorizontalSpacing = value
                OnTemplateChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the vertical spacing between vertices
        ''' </summary>
        Public Property VerticalSpacing As Double
            Get
                Return Me.m_fVerticalSpacing
            End Get
            Set(ByVal value As Double)
                If value = Me.m_fVerticalSpacing Then Return
                If value < 0 Then Throw New System.ArgumentOutOfRangeException()
                Me.m_fVerticalSpacing = value
                OnTemplateChanged()
            End Set
        End Property
        ''' <summary>
        ''' Specifies the default style applied to vertices
        ''' </summary>
        Public Property VerticesUserClass As String
            Get
                Return Me.m_VerticesUserClass
            End Get
            Set(ByVal value As String)
                If Equals(Me.m_VerticesUserClass, Nothing) Then Throw New System.ArgumentNullException()
                Me.m_VerticesUserClass = value
                OnTemplateChanged()
            End Set
        End Property

        ''' <summary>
        ''' Specifies the default style applied to edges
        ''' </summary>
        Public Property EdgesUserClass As String
            Get
                Return Me.m_EdgesUserClass
            End Get
            Set(ByVal value As String)
                If Equals(Me.m_EdgesUserClass, Nothing) Then Throw New System.ArgumentNullException()
                Me.m_EdgesUserClass = value
                OnTemplateChanged()
            End Set
        End Property

        #EndRegion

        #Region"Protected Overridable"

        ''' <summary>
        ''' Creates a new connector from the specified type
        ''' </summary>
        ''' <remarks>
        ''' The new connector style uses a copy of EdgesUserClass style
        ''' </remarks>
        ''' <paramname="type"></param>
        ''' <returns>new connector</returns> 
        Protected Overridable Function CreateEdge(ByVal type As Nevron.Nov.Diagram.Shapes.ENConnectorShape) As Nevron.Nov.Diagram.NShape
            Dim factory As Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory()
            Dim connector As Nevron.Nov.Diagram.NShape = factory.CreateShape(type)
            connector.Name = m_sName & " Edge " & Nevron.Nov.Examples.Diagram.NGraphTemplate.CurrentEdgeIndex.ToString(System.Globalization.CultureInfo.InvariantCulture)
            connector.UserClass = Me.m_EdgesUserClass
            Nevron.Nov.Examples.Diagram.NGraphTemplate.CurrentEdgeIndex += 1
            Return connector
        End Function
        ''' <summary>
        ''' Creates a new graph vertex with the specified predefined shape
        ''' </summary>
        ''' <remarks>
        ''' The new graph vertex style is a copy of the VerticesUserClass style
        ''' </remarks>
        ''' <paramname="shape">predefined shape</param>
        ''' <returns>new graph vertex</returns>
        Protected Overridable Function CreateVertex(ByVal shape As Nevron.Nov.Diagram.Shapes.ENBasicShape) As Nevron.Nov.Diagram.NShape
            Dim vertex As Nevron.Nov.Diagram.NShape = Me.m_ShapeFactory.CreateShape(shape)
            vertex.Name = m_sName & " Vertex " & Nevron.Nov.Examples.Diagram.NGraphTemplate.CurrentVertexIndex.ToString(System.Globalization.CultureInfo.InvariantCulture)
            vertex.UserClass = Me.m_VerticesUserClass
            Nevron.Nov.Examples.Diagram.NGraphTemplate.CurrentVertexIndex += 1
            Return vertex
        End Function

        #EndRegion

        #Region"Implementation"

        Private Sub Initialize()
            Me.m_fHorizontalSpacing = 30
            Me.m_fVerticalSpacing = 30
            Me.m_VerticesSize = New Nevron.Nov.Graphics.NSize(40, 40)
            Me.m_VerticesShape = Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle
            Me.m_VerticesUserClass = ""
            Me.m_EdgesUserClass = ""
            Me.m_ShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
        End Sub

        #EndRegion

        #Region"Fields"

        Friend m_fHorizontalSpacing As Double
        Friend m_fVerticalSpacing As Double
        Friend m_VerticesSize As Nevron.Nov.Graphics.NSize
        Friend m_VerticesShape As Nevron.Nov.Diagram.Shapes.ENBasicShape
        Friend m_VerticesUserClass As String
        Friend m_EdgesUserClass As String
        Private m_ShapeFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory

        #EndRegion

        #Region"Static Fields"

        Public Shared CurrentEdgeIndex As Long
        Public Shared CurrentVertexIndex As Long

        #EndRegion
    End Class
End Namespace
