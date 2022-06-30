Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Themes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NLibrariesExample
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
            Nevron.Nov.Examples.Diagram.NLibrariesExample.NLibrariesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NLibrariesExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a drawing view
			Dim drawingView As Nevron.Nov.Diagram.NDrawingView = New Nevron.Nov.Diagram.NDrawingView()

			' Create a library view
			Me.m_LibraryView = New Nevron.Nov.Diagram.NLibraryView()
            Me.m_LibraryView.Document.HistoryService.Pause()

            Try
                Me.InitLibrary(Me.m_LibraryView.Document)
            Finally
                Me.m_LibraryView.Document.HistoryService.[Resume]()
            End Try

			' Associate the drawing view with the library view to make
			' the drawing theme update the appearance of shapes in the library
			Me.m_LibraryView.DrawingView = drawingView

			' Place the library view and the drawing view in a splitter
			Dim splitter As Nevron.Nov.UI.NSplitter = New Nevron.Nov.UI.NSplitter(Me.m_LibraryView, drawingView, Nevron.Nov.UI.ENSplitterSplitMode.OffsetFromNearSide, 275)

			' Create a diagram ribbon
			Dim builder As Nevron.Nov.Diagram.NDiagramRibbonBuilder = New Nevron.Nov.Diagram.NDiagramRibbonBuilder()
            Return builder.CreateUI(splitter, drawingView)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim openLibraryButton As Nevron.Nov.UI.NButton = Nevron.Nov.UI.NButton.CreateImageAndText(Nevron.Nov.Diagram.NResources.Image_Library_LibraryOpen_png, "Open Library...")
            openLibraryButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            openLibraryButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler openLibraryButton.Click, AddressOf Me.OnOpenLibraryButtonClick
            Return openLibraryButton
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example shows how to create various NOV Diagram library items and place them in a library.</p>
<p>If you want to open a NOV Diagram library file or a Visio Stencil, use the <b>Open Library</b> button on the right.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnOpenLibraryButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_LibraryView.OpenFile()
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub InitLibrary(ByVal libraryDocument As Nevron.Nov.Diagram.NLibraryDocument)
            Dim shapeBounds As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(0, 0, 150, 100)
            Dim library As Nevron.Nov.Diagram.NLibrary = libraryDocument.Content

			' 1. Rectangle shape library item
			Dim rectangleShape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            rectangleShape.SetBounds(shapeBounds)
            rectangleShape.Geometry.AddRelative(New Nevron.Nov.Diagram.NDrawRectangle(0, 0, 1, 1))
            library.Items.Add(New Nevron.Nov.Diagram.NLibraryItem(rectangleShape, "Rectangle Shape", "This is a rectangle shape"))

			' 2. Image shape library item
			Dim imageShape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            imageShape.SetBounds(0, 0, 128, 128)
            imageShape.ImageBlock.Image = Nevron.Nov.Diagram.NResources.Image__256x256_FemaleIcon_jpg
            library.Items.Add(New Nevron.Nov.Diagram.NLibraryItem(imageShape, "Image Shape", "This is a shape with an image"))

			' 3. Shape filled with a hatch
			Dim hatchShape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            hatchShape.SetBounds(shapeBounds)
            hatchShape.Geometry.AddRelative(New Nevron.Nov.Diagram.NDrawRectangle(0, 0, 1, 1))
            hatchShape.Geometry.Fill = New Nevron.Nov.Graphics.NHatchFill(Nevron.Nov.Graphics.ENHatchStyle.DiagonalCross, Nevron.Nov.Graphics.NColor.Black, Nevron.Nov.Graphics.NColor.White)
            hatchShape.Geometry.Stroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Black)
            library.Items.Add(New Nevron.Nov.Diagram.NLibraryItem(hatchShape, "Hatch Shape", "Shape filled with a theme independent hatch"))

			' 4. Shape filled with a theme based hatch
			Dim themeShape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            themeShape.SetBounds(shapeBounds)
            themeShape.Geometry.AddRelative(New Nevron.Nov.Diagram.NDrawRectangle(0, 0, 1, 1))

			' Make color1 a theme variant color
			Dim theme As Nevron.Nov.Diagram.Themes.NDrawingTheme = Nevron.Nov.Diagram.Themes.NDrawingTheme.MyDrawNature
            Dim color1 As Nevron.Nov.Graphics.NColor = theme.ColorPalette.Variants(0)(0)
            color1.Tag = New Nevron.Nov.Diagram.Themes.NThemeVariantColorInfo(0)

			' Make color2 a theme palette color
			Dim color2 As Nevron.Nov.Graphics.NColor = theme.ColorPalette.Light1
            color2.Tag = New Nevron.Nov.Diagram.Themes.NThemePaletteColorInfo(Nevron.Nov.Diagram.Themes.ENThemeColorName.Light1, 0)
            themeShape.Geometry.Fill = New Nevron.Nov.Graphics.NHatchFill(Nevron.Nov.Graphics.ENHatchStyle.DiagonalCross, color1, color2)
            library.Items.Add(New Nevron.Nov.Diagram.NLibraryItem(themeShape, "Theme Based Shape", "Shape filled with a theme dependent hatch"))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_LibraryView As Nevron.Nov.Diagram.NLibraryView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NLibrariesExample.
		''' </summary>
		Public Shared ReadOnly NLibrariesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
