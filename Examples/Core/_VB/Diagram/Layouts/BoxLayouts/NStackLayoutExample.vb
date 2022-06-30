Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NStackLayoutExample
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
            Nevron.Nov.Examples.Diagram.NStackLayoutExample.NStackLayoutExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NStackLayoutExample), NExampleBaseSchema)
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
    The stack layout is a directed constrained cells layout, which stacks the cells in horizontal or vertical order.
    Depending on the layout direction the layout is constrained by either width or height.
</p>
<p>
	The most important properties of this layout are:
	<ul>
		<li>
			<b>Direction</b> - determines the direction in which the layout arranges adjacent cells.
		</li>
		<li>
		    <b>HorizontalContentPlacement and VerticalContentPlacement</b> - determine the default placement
		        of the cell content in regards to the X or the Y dimension of the cell bounds.
		</li>
		<li>
		    <b>HorizontalSpacing and VerticalSpacing</b> - determine the minimal spacing between 2 cells in
		        horizontal and vertical direction respectively.
		</li>
		<li>
			<b>FillMode</b> - when the size of the content is smaller than the container size 
			the FillMode property is taken into account. Possible values are:
			<ul>
			    <li>None - no filling is performed</li>
			    <li>Equal - all shapes are equally inflated</li>
			    <li>Proportional - shapes are inflated proportionally to their size</li>
			    <li>First - shapes are inflated in forward order until content fills the area</li>
			    <li>Last - shapes are inflated in reverse order until content fills the area</li>
			</ul>
			In all cases the maximal size constraints of each shape are not broken.
		</li>
		<li>
			<b>FitMode</b> - when the size of the content is larger than the container size 
			the FitMode property is taken into account. Possible values are:
			<ul>
			    <li>None - no fitting is performed</li>
			    <li>Equal - all shapes are equally deflated</li>
			    <li>Proportional - shapes are deflated proportionally to their size</li>
			    <li>First - shapes are deflated in forward order until content fits the area</li>
			    <li>Last - shapes are deflated in reverse order until content fits the area</li>
			</ul>
			In all cases the minimal size constraints of each shape are not broken.
		</li>
	</ul>
</p>
<p>
	To experiment with their behavior just change the properties of the layout in the property
	grid and click the button <b>Layout</b> button. 
</p>		
<p>	
	Change the drawing width and height to see how the layout behaves with a different layout area.
</p>
            "
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' Hide ports
            drawingDocument.Content.ScreenVisibility.ShowPorts = False

            ' Create some shapes
            Dim activePage As Nevron.Nov.Diagram.NPage = drawingDocument.Content.ActivePage
            Dim basicShapes As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()

            For i As Integer = 0 To 20 - 1
                Dim shape As Nevron.Nov.Diagram.NShape = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
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
        Private m_Layout As Nevron.Nov.Layout.NStackLayout = New Nevron.Nov.Layout.NStackLayout()

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NStackLayoutExample.
        ''' </summary>
        Public Shared ReadOnly NStackLayoutExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
