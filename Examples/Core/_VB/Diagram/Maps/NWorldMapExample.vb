Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Import.Map
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

#Region"InternalCode"

' Map data from: http://www.naturalearthdata.com/features/

#EndRegion

Namespace Nevron.Nov.Examples.Diagram
    Public Class NWorldMapExample
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
            Nevron.Nov.Examples.Diagram.NWorldMapExample.NWorldMapExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NWorldMapExample), NExampleBaseSchema)
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
    <b>NOV Diagram</b> makes it easy to import geographical data from ESRI shapefiles. You
    can control the way the shapes are rendered by applying various fill rules to them. You can
	also specify a zoom range in which the shapes and/or texts of a shapefile should be visible.
	For example when you zoom this map to 50% you will notice that labels appear for the countries.
</p>
<p>
	Upon import of a shape additional information from the DBF file that accompanies the shapefile
	is provided (e.g. Country Name, Population, Currency, GDP, etc.). You can use these values to
	customize the name, the text and the fill of the shape. You can also provide an INShapeCreatedListener
	implementation to the shape importer of the map in order to get notified when a shape is imported
	and use the values from the DBF file for this shape to customize it even further.
</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
			' Configure the document
			Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            drawing.ScreenVisibility.ShowGrid = False

			' Add styles
			Me.AddStyles(drawingDocument)

			' Configure the active page
			Dim page As Nevron.Nov.Diagram.NPage = drawing.ActivePage
            page.Bounds = New Nevron.Nov.Graphics.NRectangle(0, 0, 10000, 10000)
            page.ZoomMode = Nevron.Nov.UI.ENZoomMode.Fit
            page.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightBlue)

			' Create a map importer
			Dim mapImporter As Nevron.Nov.Diagram.Import.Map.NEsriMapImporter = New Nevron.Nov.Diagram.Import.Map.NEsriMapImporter()
            mapImporter.MapBounds = Nevron.Nov.Diagram.Import.Map.NMapBounds.World

			' Add an ESRI shapefile
			Dim countries As Nevron.Nov.Diagram.Import.Map.NEsriShapefile = New Nevron.Nov.Diagram.Import.Map.NEsriShapefile(Nevron.Nov.Diagram.NResources.RBIN_countries_zip)
            countries.NameColumn = "name_long"
            countries.TextColumn = "name_long"
            countries.MinTextZoomPercent = 50
            countries.FillRule = New Nevron.Nov.Diagram.Import.Map.NMapFillRuleValue("mapcolor8", Nevron.Nov.Examples.Diagram.NWorldMapExample.Colors)
            mapImporter.AddShapefile(countries)

			' Read the map data
			mapImporter.Read()

			' Import the map to the drawing document
			mapImporter.Import(drawingDocument, page.Bounds)

			' Size page to content
			page.SizeToContent()
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub AddStyles(ByVal document As Nevron.Nov.Diagram.NDrawingDocument)
			' Create a style sheet
			Dim styleSheet As Nevron.Nov.Dom.NStyleSheet = New Nevron.Nov.Dom.NStyleSheet()
            document.StyleSheets.Add(styleSheet)

			' Add some styling for the shapes
			Dim rule As Nevron.Nov.Dom.NRule = New Nevron.Nov.Dom.NRule()
            Dim sb As Nevron.Nov.Dom.NSelectorBuilder = rule.GetSelectorBuilder()
            sb.Start()
            sb.Type(Nevron.Nov.Diagram.NGeometry.NGeometrySchema)
            sb.ChildOf()
            sb.Type(Nevron.Nov.Diagram.NShape.NShapeSchema)
            sb.[End]()
            rule.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Graphics.NStroke)(Nevron.Nov.Diagram.NGeometry.StrokeProperty, New Nevron.Nov.Graphics.NStroke(New Nevron.Nov.Graphics.NColor(68, 90, 108))))
            styleSheet.Add(rule)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NWorldMapExample.
		''' </summary>
		Public Shared ReadOnly NWorldMapExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		''' <summary>
		''' The colors used to fill the countries.
		''' </summary>
		Private Shared ReadOnly Colors As Nevron.Nov.Graphics.NColor() = New Nevron.Nov.Graphics.NColor() {Nevron.Nov.Graphics.NColor.OldLace, Nevron.Nov.Graphics.NColor.PaleGreen, Nevron.Nov.Graphics.NColor.Gold, Nevron.Nov.Graphics.NColor.Khaki, Nevron.Nov.Graphics.NColor.Tan, Nevron.Nov.Graphics.NColor.Orange, Nevron.Nov.Graphics.NColor.Salmon, Nevron.Nov.Graphics.NColor.PaleGoldenrod}

		#EndRegion
	End Class
End Namespace
