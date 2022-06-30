Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
	''' <summary>
	''' 
	''' </summary>
	Public Class NFindAndReplaceExample
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
            Nevron.Nov.Examples.Diagram.NFindAndReplaceExample.NFindAndReplaceExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NFindAndReplaceExample), NExampleBaseSchema)
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
            Me.m_FindTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_FindTextBox.Text = "quick"
            stack.Add(New Nevron.Nov.UI.NPairBox(New Nevron.Nov.UI.NLabel("Find:"), Me.m_FindTextBox, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2))
            Me.m_ReplaceTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_ReplaceTextBox.Text = "slow"
            stack.Add(New Nevron.Nov.UI.NPairBox(New Nevron.Nov.UI.NLabel("Replace:"), Me.m_ReplaceTextBox, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2))
            Dim findAllButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Find All")
            AddHandler findAllButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnFindAllButtonClick)
            stack.Add(findAllButton)
            Dim replaceAllButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Replace All")
            AddHandler replaceAllButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnReplaceAllButtonClick)
            stack.Add(replaceAllButton)
            Dim clearHighlightButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Clear Highlight")
            AddHandler clearHighlightButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnClearHighlightButtonClick)
            stack.Add(clearHighlightButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to find and replace text.</p>
<p>Press the ""Find All"" button to highlight all occurrences of ""Find"".</p>
<p>Press the ""Replace All"" button to replace and highlight all occurrences of ""Find"" with ""Replace""</p>
<p>Press the ""Clear Highlight"" button to clear all highlighting</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content

            ' hide the grid
            drawing.ScreenVisibility.ShowGrid = False
            Dim basicShapesFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim padding As Double = 10
            Dim sizeX As Double = 160
            Dim sizeY As Double = 160

            For x As Integer = 0 To 4 - 1

                For y As Integer = 0 To 4 - 1
                    Dim shape1 As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
                    shape1.SetBounds(padding + x * (padding + sizeX), padding + y * (padding + sizeY), sizeX, sizeY)
                    shape1.TextBlock = New Nevron.Nov.Diagram.NTextBlock()
                    shape1.TextBlock.Padding = New Nevron.Nov.Graphics.NMargins(20)
                    shape1.TextBlock.Text = "The quick brown fox jumps over the lazy dog"
                    drawing.ActivePage.Items.Add(shape1)
                Next
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"

		''' <summary>
		''' Called when the user presses the find all button
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnFindAllButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' init find settings
			Dim settings As Nevron.Nov.Diagram.NFindTextSettings = New Nevron.Nov.Diagram.NFindTextSettings()
            settings.FindWhat = Me.m_FindTextBox.Text
            settings.SearchDirection = Nevron.Nov.Diagram.ENDiagramTextSearchDirection.ForwardReading

			' loop through all occurrences
			Dim searcher As Nevron.Nov.Diagram.NTextSearcher = New Nevron.Nov.Diagram.NTextSearcher(Me.m_DrawingView, settings)
            searcher.ActivateEditor = False
            Dim state As Nevron.Nov.Diagram.NShapeTextSearchState

            While searcher.FindNext(state)
                state.Shape.GetTextSelection().SetHighlightFillToSelectedInlines(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Red))
            End While
        End Sub
		''' <summary>
		''' Called when the user presses the replace all button
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnReplaceAllButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' init find settings
			Dim settings As Nevron.Nov.Diagram.NFindTextSettings = New Nevron.Nov.Diagram.NFindTextSettings()
            settings.FindWhat = Me.m_FindTextBox.Text
            settings.SearchDirection = Nevron.Nov.Diagram.ENDiagramTextSearchDirection.ForwardReading

			' find all occurrences 
			Dim searcher As Nevron.Nov.Diagram.NTextSearcher = New Nevron.Nov.Diagram.NTextSearcher(Me.m_DrawingView, settings)
            searcher.ActivateEditor = False
            Dim state As Nevron.Nov.Diagram.NShapeTextSearchState

            While searcher.FindNext(state)
                Dim selection As Nevron.Nov.Text.NSelection = state.Shape.GetTextSelection()

				' replace 
				Dim selectedRange As Nevron.Nov.Graphics.NRangeI = selection.SelectedRange
                selection.InsertText(Me.m_ReplaceTextBox.Text)

                If Me.m_ReplaceTextBox.Text.Length > 0 Then
                    selection.SelectRange(New Nevron.Nov.Graphics.NRangeI(selectedRange.Begin, selectedRange.Begin + Me.m_ReplaceTextBox.Text.Length - 1))
                    selection.SetHighlightFillToSelectedInlines(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.LimeGreen))
                End If
            End While
        End Sub
		''' <summary>
		''' Called when the user presses clear highlight button
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnClearHighlightButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim shapes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Dom.NNode) = Me.m_DrawingView.Drawing.GetDescendants(Nevron.Nov.Diagram.NShape.NShapeSchema)

            For i As Integer = 0 To shapes.Count - 1
                Dim shape As Nevron.Nov.Diagram.NShape = CType(shapes(i), Nevron.Nov.Diagram.NShape)
                Dim rootTextElement As Nevron.Nov.Text.NRangeTextElement = CType(shape.GetTextBlockContentNoCreate(), Nevron.Nov.Text.NRangeTextElement)
                If rootTextElement Is Nothing Then Continue For
                rootTextElement.VisitRanges(Sub(ByVal range As Nevron.Nov.Text.NRangeTextElement)
                                                Dim inline As Nevron.Nov.Text.NInline = TryCast(range, Nevron.Nov.Text.NInline)

                                                If inline IsNot Nothing Then
                                                    inline.ClearLocalValue(Nevron.Nov.Text.NInline.HighlightFillProperty)
                                                End If
                                            End Sub)
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_FindTextBox As Nevron.Nov.UI.NTextBox
        Private m_ReplaceTextBox As Nevron.Nov.UI.NTextBox

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NFindAndReplaceExample.
		''' </summary>
		Public Shared ReadOnly NFindAndReplaceExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
