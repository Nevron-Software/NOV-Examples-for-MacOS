Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Themes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NThemeBasedShapesExample
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
            Nevron.Nov.Examples.Diagram.NThemeBasedShapesExample.NThemeBasedShapesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NThemeBasedShapesExample), NExampleBaseSchema)
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
	This example demonstrates how to create shapes whose appearance depends on
	the currently selected page theme and page theme variant. To do that, set
	the <b>Tag</b> property of the shapes to a theme based color info.
</p>
<p>
	Select a new page theme or page theme variant from the <b>Design</b> tab of
	the ribbon to see how the style of the shape will change.
</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
			' Create a rectangle shape
			Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Text = "Shape"
            shape.Geometry.AddRelative(New Nevron.Nov.Diagram.NDrawRectangle(0, 0, 1, 1))
            shape.SetBounds(100, 100, 200, 150)

			' Make color1 a theme variant color
			Dim theme As Nevron.Nov.Diagram.Themes.NDrawingTheme = Nevron.Nov.Diagram.Themes.NDrawingTheme.MyDrawNature
            Dim color1 As Nevron.Nov.Graphics.NColor = theme.ColorPalette.Variants(0)(0)
            color1.Tag = New Nevron.Nov.Diagram.Themes.NThemeVariantColorInfo(0)

			' Make color2 a theme palette color
			Dim color2 As Nevron.Nov.Graphics.NColor = theme.ColorPalette.Light1
            color2.Tag = New Nevron.Nov.Diagram.Themes.NThemePaletteColorInfo(Nevron.Nov.Diagram.Themes.ENThemeColorName.Light1, 0)

			' Set the fill of the geometry to a hatch that depends on the theme
			shape.Geometry.Fill = New Nevron.Nov.Graphics.NHatchFill(Nevron.Nov.Graphics.ENHatchStyle.DiagonalCross, color1, color2)

			' Add the theme based shape to the active page of the drawing
			Dim activePage As Nevron.Nov.Diagram.NPage = drawingDocument.Content.ActivePage
            activePage.Items.Add(shape)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NThemeBasedShapesExample.
		''' </summary>
		Public Shared ReadOnly NThemeBasedShapesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
