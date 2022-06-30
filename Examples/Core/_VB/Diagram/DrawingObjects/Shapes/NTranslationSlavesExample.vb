Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NTranslationSlavesExample
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
            Nevron.Nov.Examples.Diagram.NTranslationSlavesExample.NTranslationSlavesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NTranslationSlavesExample), NExampleBaseSchema)
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
            Me.m_PropertyEditorHolder = New Nevron.Nov.UI.NContentHolder()
            Return Me.m_PropertyEditorHolder
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
This example demonstrates the shape move slaves. 
</p>
<p>
It is in many cases necessary to implement rigid connected shapes structures. In NOV Diagram this is achieved with the help of shape move slaves. 
</p>
<p>
The move slaves of a shape are such shapes, which are moved together with the shape. The accumulation of the shape move slaves is recursive. For example: if a shape has to be moved, because it is a move slave, then its move slaves will also be moved. 
</p>
<p>
To explore move slaves behavior just select a shape in the diagram and check the shapes, which must be moved when it is move. Then move the shape. 
</p>
<p>
Note that the example will automatically highlight the move slaves of the currently selected shape. 
</p>
            "
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim template As NGraphTemplate

			' create rectangular grid template
			template = New NRectangularGridTemplate()
            template.Origin = New Nevron.Nov.Graphics.NPoint(10, 23)
            template.VerticesShape = Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle
            template.EdgesUserClass = Nevron.Nov.Diagram.NDR.StyleSheetNameConnectors
            template.Create(drawingDocument)

			' create tree template
			template = New NGenericTreeTemplate()
            template.Origin = New Nevron.Nov.Graphics.NPoint(250, 23)
            template.VerticesShape = Nevron.Nov.Diagram.Shapes.ENBasicShape.Triangle
            template.EdgesUserClass = Nevron.Nov.Diagram.NDR.StyleSheetNameConnectors
            template.Create(drawingDocument)

			' create elliptical grid template
			template = New NEllipticalGridTemplate()
            template.Origin = New Nevron.Nov.Graphics.NPoint(10, 250)
            template.VerticesShape = Nevron.Nov.Diagram.Shapes.ENBasicShape.Ellipse
            template.EdgesUserClass = Nevron.Nov.Diagram.NDR.StyleSheetNameConnectors
            template.Create(drawingDocument)

            ' hook selection events
            Dim selection As Nevron.Nov.Diagram.NPageSelection = drawingDocument.Content.ActivePage.Selection
            selection.Mode = Nevron.Nov.UI.ENSelectionMode.[Single]
            AddHandler selection.Selected, AddressOf Me.OnSelectionSelected
            AddHandler selection.Deselected, AddressOf Me.OnSelectionDeselected
        End Sub


        #EndRegion

        #Region"Implementation"

        ''' <summary>
        ''' Called when a diagram item has been selected.
        ''' </summary>
        ''' <paramname="arg"></param>
        Private Sub OnSelectionSelected(ByVal arg As Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.Diagram.NDiagramItem))
            Dim shape As Nevron.Nov.Diagram.NShape = TryCast(arg.Item, Nevron.Nov.Diagram.NShape)
            If shape Is Nothing Then Return
            
            ' create the shape move slaves property editor
            Me.m_PropertyEditorHolder.Content = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((shape), Nevron.Nov.Dom.NNode)).CreatePropertyEditor(shape, Nevron.Nov.Diagram.NShape.MoveSlavesProperty)
                
            ' subscribe for move slaves property changes.
            shape.AddEventHandler(Nevron.Nov.Diagram.NShape.MoveSlavesProperty.ValueChangedEvent, New Nevron.Nov.Dom.NEventHandler(Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMoveSlavesPropertyChanged))

            ' highlight the shape current slaves
            Me.HighlightSlaves(shape)
        End Sub
        ''' <summary>
        ''' Called when a diagram item has been deselected.
        ''' </summary>
        ''' <paramname="arg"></param>
        Private Sub OnSelectionDeselected(ByVal arg As Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.Diagram.NDiagramItem))
            Dim shape As Nevron.Nov.Diagram.NShape = TryCast(arg.Item, Nevron.Nov.Diagram.NShape)
            If shape Is Nothing Then Return

            ' unhook move slaves property changed event
            shape.RemoveEventHandler(Nevron.Nov.Diagram.NShape.MoveSlavesProperty.ValueChangedEvent, New Nevron.Nov.Dom.NEventHandler(Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMoveSlavesPropertyChanged))
            
            ' clear hightlights
            Me.ClearHighlights()

            ' destroy property editor
            Me.m_PropertyEditorHolder.Content = Nothing
        End Sub
        ''' <summary>
        ''' Called when the MoveSlaves property of the currently selected shape has changed.
        ''' </summary>
        ''' <paramname="arg"></param>
        Private Sub OnMoveSlavesPropertyChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim shape As Nevron.Nov.Diagram.NShape = TryCast(arg.TargetNode, Nevron.Nov.Diagram.NShape)
            If shape Is Nothing Then Return

            ' highligh the shape move slaves
            Me.HighlightSlaves(shape)
        End Sub
        ''' <summary>
        ''' Clears the highlighting on all shapes in the active page.
        ''' </summary>
        Private Sub ClearHighlights()
            Dim shapes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = Me.m_DrawingView.ActivePage.GetShapes(True)

            For i As Integer = 0 To shapes.Count - 1
                Dim shape As Nevron.Nov.Diagram.NShape = shapes(i)
                shape.Geometry.ClearValue(Nevron.Nov.Diagram.NGeometry.FillProperty)
                shape.Geometry.ClearValue(Nevron.Nov.Diagram.NGeometry.StrokeProperty)
            Next
        End Sub
        ''' <summary>
        ''' Highlights the move slaves of the specified shape
        ''' </summary>
        ''' <paramname="shape"></param>
        Private Sub HighlightSlaves(ByVal shape As Nevron.Nov.Diagram.NShape)
            Me.ClearHighlights()
            Dim shapes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = shape.GetMoveSlaves()

            For i As Integer = 0 To shapes.Count - 1
                Dim cur As Nevron.Nov.Diagram.NShape = shapes(i)
                cur.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightCoral)
                cur.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(2, Nevron.Nov.Graphics.NColor.DarkRed)
            Next
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_PropertyEditorHolder As Nevron.Nov.UI.NContentHolder

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NTranslationSlavesExample.
        ''' </summary>
        Public Shared ReadOnly NTranslationSlavesExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
