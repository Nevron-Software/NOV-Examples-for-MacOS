Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NDockLayoutExample
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
            Nevron.Nov.Examples.Diagram.NDockLayoutExample.NDockLayoutExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NDockLayoutExample), NExampleBaseSchema)
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

            ' NOTE: For Cells layout we provide the user with the ability to add shapes with different sizes so that he/she can test the layouts
            Dim addSmallItemButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Add Small Shape")
            AddHandler addSmallItemButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAddSmallItemButtonClick)
            itemsStack.Add(addSmallItemButton)
            Dim addLargeItemButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Add Large Shape")
            AddHandler addLargeItemButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAddLargeItemButtonClick)
            itemsStack.Add(addLargeItemButton)
            Dim addRandomItemButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Add Random Shape")
            AddHandler addRandomItemButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAddRandomItemButtonClick)
            itemsStack.Add(addRandomItemButton)
            Dim removeAllItemsButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Remove All Shapes")
            AddHandler removeAllItemsButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRemoveAllItemsButtonClick)
            itemsStack.Add(removeAllItemsButton)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Items", itemsStack))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>     
    The dock layout is a space eating cells layout, which places vertices at per-vertex specified docking areas of the currently available layout area.
</p>
<p>
	The most important properties of this layout are:
	<ul>
		<li>
		    <b>HorizontalContentPlacement and VerticalContentPlacement</b> - determine the default placement
		        of the cell content in regards to the X or the Y dimension of the cell bounds.
		</li>
		<li>
		    <b>HorizontalSpacing and VerticalSpacing</b> - determine the minimal spacing between 2 cells in
		        horizontal and vertical direction respectively.
		</li>
		<li>
			<b>FillMode and FitMode</b> - when the size of the content is smaller than the container size 
			the FillMode property is taken into account. If the content size is greater than the container,
			then the layout takes the value of the FitMode into account. Possible values are:
			<ul>
			    <li>None - the dock layout does not attempt to resolve the available/insufficient area problem</li>
			    <li>Equal - the dock inflates/deflates the size of each object with equal amount of space in order
			        to resolve the available/insufficient area problem</li>
			    <li>CenterFirst - the dock inflates/deflates the size of the center object in the dock, then the size of the
			        pair formed by the previous and the next one and so on until the available/insufficient area
			        problem is resolved</li>
			    <li>SidesFirst - the dock inflates/deflates the size of the pair formed by the first and the last
			        object in the dock, then the size of the pair formed by the next and the previous one and so
			        on until the available/insufficient area problem is resolved</li>
			    <li>ForwardOrder - the bodies are resized in the order they were added</li>
			    <li>ReverseOrder - the bodies are resized in reverse order to the order they were added</li>
			</ul>
			In all cases the minimal and maximal size constraints of each shape are not broken, so it is possible
			the dock cannot resolve the available/insufficient area problem completely.
		</li>
	</ul>
</p>
<p>
	To experiment with the layout just change the properties of the layout in the property grid and click the <b>Layout</b> button.
</p>
            "
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' Hide ports
            drawingDocument.Content.ScreenVisibility.ShowPorts = False
            Dim activePage As Nevron.Nov.Diagram.NPage = drawingDocument.Content.ActivePage
            Dim basicShapes As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim min As Integer = 100
            Dim max As Integer = 200
            Dim shape As Nevron.Nov.Diagram.NShape
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 5 - 1
                shape = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
                Dim shapeLightColors As Nevron.Nov.Graphics.NColor() = New Nevron.Nov.Graphics.NColor() {New Nevron.Nov.Graphics.NColor(236, 97, 49), New Nevron.Nov.Graphics.NColor(247, 150, 56), New Nevron.Nov.Graphics.NColor(68, 90, 108), New Nevron.Nov.Graphics.NColor(129, 133, 133), New Nevron.Nov.Graphics.NColor(255, 165, 109)}
                Dim shapeDarkColors As Nevron.Nov.Graphics.NColor() = New Nevron.Nov.Graphics.NColor() {New Nevron.Nov.Graphics.NColor(246, 176, 152), New Nevron.Nov.Graphics.NColor(251, 203, 156), New Nevron.Nov.Graphics.NColor(162, 173, 182), New Nevron.Nov.Graphics.NColor(192, 194, 194), New Nevron.Nov.Graphics.NColor(255, 210, 182)}
                shape.Geometry.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Horizontal, Nevron.Nov.Graphics.ENGradientVariant.Variant3, shapeLightColors(i), shapeDarkColors(i))
                shape.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(1, New Nevron.Nov.Graphics.NColor(68, 90, 108))


                ' Generate random width and height
                Dim width As Single = random.[Next](min, max)
                Dim height As Single = random.[Next](min, max)

                Select Case i
                    Case 0
                        shape.LayoutData.DockArea = Nevron.Nov.Layout.ENDockArea.Top
                        shape.Text = "Top (" & i.ToString() & ")"
                    Case 1
                        shape.LayoutData.DockArea = Nevron.Nov.Layout.ENDockArea.Bottom
                        shape.Text = "Bottom (" & i.ToString() & ")"
                    Case 2
                        shape.LayoutData.DockArea = Nevron.Nov.Layout.ENDockArea.Left
                        shape.Text = "Left (" & i.ToString() & ")"
                    Case 3
                        shape.LayoutData.DockArea = Nevron.Nov.Layout.ENDockArea.Right
                        shape.Text = "Right (" & i.ToString() & ")"
                    Case 4
                        shape.LayoutData.DockArea = Nevron.Nov.Layout.ENDockArea.Center
                        shape.Text = "Center (" & i.ToString() & ")"
                End Select

                shape.SetBounds(New Nevron.Nov.Graphics.NRectangle(0, 0, width, height))
                activePage.Items.Add(shape)
            Next

            ' Arrange diagram
            Me.ArrangeDiagram(drawingDocument)

            ' Fit page
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

        Private Function CreateShape() As Nevron.Nov.Diagram.NShape
            Dim factory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Return factory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
        End Function

        Private Sub OnAddSmallItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim shape As Nevron.Nov.Diagram.NShape = Me.CreateShape()
            shape.Width = 25
            shape.Height = 25
            Me.m_DrawingView.ActivePage.Items.Add(shape)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        Private Sub OnAddLargeItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim shape As Nevron.Nov.Diagram.NShape = Me.CreateShape()
            shape.Width = 60
            shape.Height = 60
            Me.m_DrawingView.ActivePage.Items.Add(shape)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        Private Sub OnAddRandomItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim range As Integer = 30
            Dim rnd As System.Random = New System.Random()
            Dim shape As Nevron.Nov.Diagram.NShape = Me.CreateShape()
            shape.Width = rnd.[Next](range) + range
            shape.Height = rnd.[Next](range) + range
            Me.m_DrawingView.ActivePage.Items.Add(shape)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        Private Sub OnRemoveAllItemsButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_DrawingView.Document.StartHistoryTransaction("Remove All Items")

            Try
                Me.m_DrawingView.Document.Content.ActivePage.Items.Clear()
            Finally
                Me.m_DrawingView.Document.CommitHistoryTransaction()
            End Try
        End Sub

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnLayoutChanged(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        Protected Overridable Sub OnArrangeButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.ArrangeDiagram(Me.m_DrawingView.Document)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_Layout As Nevron.Nov.Layout.NDockLayout = New Nevron.Nov.Layout.NDockLayout()

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NDockLayoutExample.
        ''' </summary>
        Public Shared ReadOnly NDockLayoutExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
