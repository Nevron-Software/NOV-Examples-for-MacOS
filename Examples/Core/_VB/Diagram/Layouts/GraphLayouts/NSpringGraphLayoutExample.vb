Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NSpringGraphLayoutExample
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
            Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NSpringGraphLayoutExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample), NExampleBaseSchema)
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
            AddHandler Me.m_Layout.Changed, AddressOf Me.OnLayoutChanged
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            ' property editor
            Dim editor As Nevron.Nov.Editors.NEditor = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Layout), Nevron.Nov.Dom.NNode)).CreateInstanceEditor(Me.m_Layout)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Properties", editor))
            Dim arrangeButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Arrange Diagram")
            AddHandler arrangeButton.Click, AddressOf Me.OnArrangeButtonClick
            stack.Add(arrangeButton)

            ' items stack
            Dim itemsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            ' NOTE: For Graph layouts we provide the user with the ability to generate random graph diagrams so that he/she can test the layouts
            Dim randomGraph1Button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random Graph (10 vertices, 15 edges)")
            AddHandler randomGraph1Button.Click, AddressOf Me.OnRandomGraph1ButtonClick
            itemsStack.Add(randomGraph1Button)
            Dim randomGraph2Button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random Graph (20 vertices, 30 edges)")
            AddHandler randomGraph2Button.Click, AddressOf Me.OnRandomGraph2ButtonClick
            itemsStack.Add(randomGraph2Button)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Items", itemsStack))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    The spring layout method is a classical force directed layout, which uses spring and electrical forces.
</p>
<p>
    Graph edges are treated as springs. Springs aim to ensure that the distance between adjacent vertices is
	approximately equal to the spring length. The parameters of the spring force are controlled by an instance
	of the <b>NSpringForce</b> class, accessible from the SpringForce property.
</p>
<p>
    Graph vertices are treated as electrically charged particles, which repel each other. The electrical 
	force aims to ensure that vertices should not be close together. The parameters of the electrical force
	are controlled by an instance of the <b>NElectricalForce</b> class, accessible from the ElectricalForce property.
</p>
<p>
	The spring force accepts per edge defined spring lengths and spring stiffness. In this example the red
	connectors are with smaller spring length are with greater stiffness than the blue connectors. Because
	of that they tend to be displayed closer to each other.
</p>
<p> 
	The electrical force accepts per vertex provided electric charges. Thus some vertices may be more repulsive than others.
</p>
<p>
	To experiment with the layout behavior just change the properties of the layout in the property grid and
	click the <b>Layout</b> button.
</p>
            "
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' Hide ports
            drawingDocument.Content.ScreenVisibility.ShowPorts = False
            Dim activePage As Nevron.Nov.Diagram.NPage = drawingDocument.Content.ActivePage

            ' We will be using basic shapes for this example
            Dim basicShapesFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            basicShapesFactory.DefaultSize = New Nevron.Nov.Graphics.NSize(80, 80)
            Dim persons As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson)()

            ' Create persons
            Dim personEmil As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson = New Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson("Emil Moore", basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle))
            Dim personAndre As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson = New Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson("Andre Smith", basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle))
            Dim personRobert As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson = New Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson("Robert Johnson", basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle))
            Dim personBob As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson = New Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson("Bob Williams", basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle))
            Dim personPeter As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson = New Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson("Peter Brown", basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle))
            Dim personSilvia As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson = New Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson("Silvia Moore", basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle))
            Dim personEmily As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson = New Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson("Emily Smith", basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle))
            Dim personMonica As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson = New Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson("Monica Johnson", basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle))
            Dim personSamantha As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson = New Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson("Samantha Miller", basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle))
            Dim personIsabella As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson = New Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson("Isabella Davis", basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle))
            persons.Add(personEmil)
            persons.Add(personAndre)
            persons.Add(personRobert)
            persons.Add(personBob)
            persons.Add(personPeter)
            persons.Add(personSilvia)
            persons.Add(personEmily)
            persons.Add(personMonica)
            persons.Add(personSamantha)
            persons.Add(personIsabella)

            ' Create family relashionships
            personEmil.m_Family = personSilvia
            personAndre.m_Family = personEmily
            personRobert.m_Family = personMonica

            ' Create friend relationships
            personEmily.m_Friends.Add(personBob)
            personEmily.m_Friends.Add(personMonica)
            personAndre.m_Friends.Add(personPeter)
            personAndre.m_Friends.Add(personIsabella)
            personSilvia.m_Friends.Add(personBob)
            personSilvia.m_Friends.Add(personSamantha)
            personSilvia.m_Friends.Add(personIsabella)
            personEmily.m_Friends.Add(personIsabella)
            personEmily.m_Friends.Add(personPeter)
            personPeter.m_Friends.Add(personRobert)

            ' create the person vertices
            For i As Integer = 0 To persons.Count - 1
                activePage.Items.Add(persons(CInt((i))).m_Shape)
            Next

            ' Create the family relations
            For i As Integer = 0 To persons.Count - 1
                Dim currentPerson As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson = persons(i)

                If currentPerson.m_Family IsNot Nothing Then
                    Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
                    connector.MakeLine()
                    activePage.Items.Add(connector)
                    connector.GlueBeginToShape(currentPerson.m_Shape)
                    connector.GlueEndToShape(currentPerson.m_Family.m_Shape)
                    connector.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(2, Nevron.Nov.Graphics.NColor.Coral)
                    connector.LayoutData.SpringStiffness = 2
                    connector.LayoutData.SpringLength = 100
                End If
            Next

            For i As Integer = 0 To persons.Count - 1
                Dim currentPerson As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson = persons(i)

                For j As Integer = 0 To currentPerson.m_Friends.Count - 1
                    Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
                    connector.MakeLine()
                    activePage.Items.Add(connector)
                    connector.GlueBeginToShape(currentPerson.m_Shape)
                    connector.GlueEndToShape(currentPerson.m_Friends(CInt((j))).m_Shape)
                    connector.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(2, Nevron.Nov.Graphics.NColor.Green)
                    connector.LayoutData.SpringStiffness = 1
                    connector.LayoutData.SpringLength = 200
                Next
            Next

            ' Arrange diagram
            Me.ArrangeDiagram(drawingDocument)

            ' Fit active page
            drawingDocument.Content.ActivePage.ZoomMode = Nevron.Nov.UI.ENZoomMode.Fit
        End Sub

        #EndRegion

        #Region"Implementation"

        ''' <summary>
        ''' Arranges the shapes in the active page.
        ''' </summary>
        ''' <paramname="drawingDocument"></param>
        Private Sub ArrangeDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' get all top-level shapes that reside in the active page
            Dim activePage As Nevron.Nov.Diagram.NPage = drawingDocument.Content.ActivePage
            Dim shapes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = activePage.GetShapes(False)

            ' create a layout context and use it to arrange the shapes using the current layout
            Dim layoutContext As Nevron.Nov.Diagram.Layout.NDrawingLayoutContext = New Nevron.Nov.Diagram.Layout.NDrawingLayoutContext(drawingDocument, activePage)
            Me.m_Layout.Arrange(shapes.CastAll(Of Object)(), layoutContext)

            ' size the page to the content size
            activePage.SizeToContent()
        End Sub

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnRandomGraph1ButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim drawingDocument As Nevron.Nov.Diagram.NDrawingDocument = Me.m_DrawingView.Document
            drawingDocument.StartHistoryTransaction("Create Random Graph 1")

            Try
                Me.m_DrawingView.ActivePage.Items.Clear()

                ' create a test tree
                Dim graph As NRandomGraphTemplate = New NRandomGraphTemplate()
                graph.EdgesUserClass = "Connector"
                graph.VertexCount = 10
                graph.EdgeCount = 15
                graph.VerticesShape = Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.VertexShape
                graph.VerticesSize = Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.VertexSize
                graph.Create(drawingDocument)

                ' layout the tree
                Me.ArrangeDiagram(drawingDocument)
            Finally
                drawingDocument.CommitHistoryTransaction()
            End Try
        End Sub

        Private Sub OnRandomGraph2ButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim drawingDocument As Nevron.Nov.Diagram.NDrawingDocument = Me.m_DrawingView.Document
            drawingDocument.StartHistoryTransaction("Create Random Graph 2")

            Try
                Me.m_DrawingView.ActivePage.Items.Clear()

                ' create a test tree
                Dim graph As NRandomGraphTemplate = New NRandomGraphTemplate()
                graph.EdgesUserClass = "Connector"
                graph.VertexCount = 20
                graph.EdgeCount = 30
                graph.VerticesShape = Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.VertexShape
                graph.VerticesSize = Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.VertexSize
                graph.Create(drawingDocument)

                ' layout the tree
                Me.ArrangeDiagram(drawingDocument)
            Finally
                drawingDocument.CommitHistoryTransaction()
            End Try
        End Sub

        Private Sub OnLayoutChanged(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        Protected Overridable Sub OnArrangeButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_Layout As Nevron.Nov.Diagram.Layout.NSpringGraphLayout = New Nevron.Nov.Diagram.Layout.NSpringGraphLayout()

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NSpringGraphLayoutExample.
        ''' </summary>
        Public Shared ReadOnly NSpringGraphLayoutExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Constants"

        Private Const VertexShape As Nevron.Nov.Diagram.Shapes.ENBasicShape = Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle
        Private Shared ReadOnly VertexSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(50, 50)

        #EndRegion

        #Region"Nested Types"

        Public Class NPerson
            Public Sub New(ByVal name As String, ByVal shape As Nevron.Nov.Diagram.NShape)
                Me.m_Shape = shape
                Me.m_Shape.Text = name
                Me.m_Shape.DefaultShapeGlue = Nevron.Nov.Diagram.ENDefaultShapeGlue.GlueToGeometryIntersection
                Me.m_Name = name
                Me.m_Friends = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson)()
                Me.m_Family = Nothing
            End Sub

            Public m_Shape As Nevron.Nov.Diagram.NShape
            Public m_Name As String
            Public m_Friends As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson)
            Public m_Family As Nevron.Nov.Examples.Diagram.NSpringGraphLayoutExample.NPerson
        End Class

		#EndRegion
	End Class
End Namespace
