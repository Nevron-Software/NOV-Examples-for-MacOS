Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NDrawingThemesExample
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
            Nevron.Nov.Examples.Diagram.NDrawingThemesExample.NDrawingThemesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NDrawingThemesExample), NExampleBaseSchema)
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
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example shows how the different shape styles of a drawing theme look. Select a new page theme and
	page theme variant from the <b>Design</b> tab of the ribbon to see another theme.
</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Const ShapeWidth As Double = 70
            Const ShapeHeight As Double = 50
            Const Spacing As Double = 20
            Const ShapeType As Nevron.Nov.Diagram.Shapes.ENBasicShape = Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim page As Nevron.Nov.Diagram.NPage = drawing.ActivePage
            Dim factory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()

			' Hide ports
			drawing.ScreenVisibility.ShowPorts = False

			' Create the variant styled shapes
			Dim x As Double = 0
            Dim y As Double = 0

            For i As Integer = 0 To 4 - 1
                Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(ShapeType)
                shape.SetBounds(x, y, ShapeWidth, ShapeHeight)
                shape.Text = "Text"

				' Set the shape style
				Dim styleIndex As Integer = 100 + i
                Dim colorIndex As Integer = 100 + i
                shape.Style = New Nevron.Nov.Diagram.NShapeStyle(styleIndex, colorIndex)

				' Add the shape to the page
				page.Items.Add(shape)
                x += ShapeWidth + Spacing
            Next

            For i As Integer = 0 To 6 - 1
                Dim styleIndex As Integer = i
                y += ShapeHeight + Spacing
                x = 0

                For j As Integer = 0 To 7 - 1
                    Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(ShapeType)
                    shape.SetBounds(x, y, ShapeWidth, ShapeHeight)
                    shape.Text = "Text"

					' Set the shape style
					Dim colorIndex As Integer = 200 + j
                    shape.Style = New Nevron.Nov.Diagram.NShapeStyle(styleIndex, colorIndex)

					' Add the page to the shape
					page.Items.Add(shape)
                    x += ShapeWidth + Spacing
                Next
            Next

			' Connect 2 shapes with a connector
			Dim shape1 As Nevron.Nov.Diagram.NShape = CType(page.Items(0), Nevron.Nov.Diagram.NShape)
            Dim shape2 As Nevron.Nov.Diagram.NShape = CType(page.Items(11), Nevron.Nov.Diagram.NShape)
            Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            connector.Text = "Text"
            page.Items.Add(connector)
            connector.UserClass = Nevron.Nov.Diagram.NDR.StyleSheetNameConnectors
            connector.RerouteMode = Nevron.Nov.Diagram.ENRoutableConnectorRerouteMode.Always
            connector.GlueBeginToShape(shape1)
            connector.GlueEndToShape(shape2)
            shape1.AllowMoveX = False
            shape1.AllowMoveY = False
            page.SizeToContent()
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NDrawingThemesExample.
        ''' </summary>
        Public Shared ReadOnly NDrawingThemesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
