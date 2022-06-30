Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NSpellCheckExample
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
            Nevron.Nov.Examples.Diagram.NSpellCheckExample.NSpellCheckExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NSpellCheckExample), NExampleBaseSchema)
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
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim enableSpellCheck As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enable Spell Check")
            AddHandler enableSpellCheck.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnEnableSpellCheckButtonClick)
            stack.Add(enableSpellCheck)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>
						Demonstrates how to enable the build in spell check.
					</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' Hide the grid
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            drawing.ScreenVisibility.ShowGrid = False
            Dim basicShapesFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim shape1 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            shape1.SetBounds(10, 10, 200, 200)
            shape1.TextBlock = New Nevron.Nov.Diagram.NTextBlock()
            shape1.TextBlock.Padding = New Nevron.Nov.Graphics.NMargins(20)
            shape1.TextBlock.Text = "This text cantains many typpos. This text contuins manyy typos."
            drawing.ActivePage.Items.Add(shape1)
        End Sub

		#EndRegion

		#Region"Event Handlers"

		''' <summary>
		''' Called when the user presses the find all button
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnEnableSpellCheckButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_DrawingView.SpellChecker.Enabled = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NSpellCheckExample.
        ''' </summary>
        Public Shared ReadOnly NSpellCheckExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
