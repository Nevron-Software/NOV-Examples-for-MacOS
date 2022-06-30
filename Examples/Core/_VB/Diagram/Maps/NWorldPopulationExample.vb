Imports Nevron.Nov.Data
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Import.Map
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

#Region"InternalCode"

' Map data from: http://www.naturalearthdata.com/features/

#EndRegion

Namespace Nevron.Nov.Examples.Diagram
    Public Class NWorldPopulationExample
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
            Nevron.Nov.Examples.Diagram.NWorldPopulationExample.NWorldPopulationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NWorldPopulationExample), NExampleBaseSchema)
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
			
			' Create radio buttons for the different data groupings
			stack.Add(New Nevron.Nov.UI.NRadioButton("Optimal"))
            stack.Add(New Nevron.Nov.UI.NRadioButton("Equal Interval"))
            stack.Add(New Nevron.Nov.UI.NRadioButton("Equal Distribution"))

			' Create a radio button group to hold the radio buttons
			Dim group As Nevron.Nov.UI.NRadioButtonGroup = New Nevron.Nov.UI.NRadioButtonGroup(stack)
            group.SelectedIndex = 0
            AddHandler group.SelectedIndexChanged, AddressOf Me.OnDataGroupingSelectedIndexChanged

			' Create the data grouping group box
			Dim dataGroupingGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Data Grouping", group)
            Return dataGroupingGroupBox
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example shows a world population map by countries (darker countries have larger population).
    A fill rule is used to automatically color the countries on the map based on their population attribute.
</p>
<p>
	Upon import of a shape additional information from the DBF file that accompanies the shapefile
	is provided (e.g. Country Name, Population, Currency, GDP, etc.). You can use these values to
	customze the name, the text and the fill of the shape. You can also provide an INShapeCreatedListener
	implementation to the shape importer of the map in order to get notified when a shape is imported
	and use the values from the DBF file for this shape to customize it even further. This example
	demonstrates how to create and use such interface implementation to add tooltips for the shapes.
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
			' Configure the document
			Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            drawing.ScreenVisibility.ShowGrid = False

			' Add styles
			Me.AddStyles(drawingDocument)

			' Configure the active page
			Dim page As Nevron.Nov.Diagram.NPage = drawing.ActivePage
            page.ZoomMode = Nevron.Nov.UI.ENZoomMode.Fit
            page.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightBlue)

			' Create a map importer
			Me.m_MapImporter = New Nevron.Nov.Diagram.Import.Map.NEsriMapImporter()
            Me.m_MapImporter.MapBounds = Nevron.Nov.Diagram.Import.Map.NMapBounds.World

			' Add a shapefile
			Dim countries As Nevron.Nov.Diagram.Import.Map.NEsriShapefile = New Nevron.Nov.Diagram.Import.Map.NEsriShapefile(Nevron.Nov.Diagram.NResources.RBIN_countries_zip)
            countries.NameColumn = "name_long"
            countries.TextColumn = "name_long"
            countries.MinTextZoomPercent = 50
            countries.FillRule = New Nevron.Nov.Diagram.Import.Map.NMapFillRuleRange("pop_est", Nevron.Nov.Graphics.NColor.White, New Nevron.Nov.Graphics.NColor(0, 80, 0), 12)
            Me.m_MapImporter.AddShapefile(countries)

			' Set the shape created listener
			Me.m_MapImporter.ShapeCreatedListener = New Nevron.Nov.Examples.Diagram.NWorldPopulationExample.NCustomShapeCreatedListener()

			' Read the map data
			Me.m_MapImporter.Read()

			' Import the map to the drawing document
			Me.ImportMap()
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
            rule.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Graphics.NStroke)(Nevron.Nov.Diagram.NGeometry.StrokeProperty, New Nevron.Nov.Graphics.NStroke(New Nevron.Nov.Graphics.NColor(80, 80, 80))))
            styleSheet.Add(rule)
        End Sub

        Private Sub ImportMap()
            Dim page As Nevron.Nov.Diagram.NPage = Me.m_DrawingView.ActivePage
            page.Items.Clear()
            page.Bounds = New Nevron.Nov.Graphics.NRectangle(0, 0, 10000, 10000)

			' Import the map to the drawing document
			Me.m_MapImporter.Import(Me.m_DrawingView.Document, page.Bounds)

			' Size page to content
			page.SizeToContent()
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnDataGroupingSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim fillRule As Nevron.Nov.Diagram.Import.Map.NMapFillRuleRange = CType(Me.m_MapImporter.GetShapefileAt(CInt((0))).FillRule, Nevron.Nov.Diagram.Import.Map.NMapFillRuleRange)

            Select Case CInt(arg.NewValue)
                Case 0
                    fillRule.DataGrouping = New Nevron.Nov.Diagram.Import.Map.NDataGroupingOptimal()
                Case 1
                    fillRule.DataGrouping = New Nevron.Nov.Diagram.Import.Map.NDataGroupingEqualInterval()
                Case 2
                    fillRule.DataGrouping = New Nevron.Nov.Diagram.Import.Map.NDataGroupingEqualDistribution()
            End Select

            Me.ImportMap()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_MapImporter As Nevron.Nov.Diagram.Import.Map.NEsriMapImporter

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NWorldPopulationExample.
		''' </summary>
		Public Shared ReadOnly NWorldPopulationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Nested Types"

		Private Class NCustomShapeCreatedListener
            Inherits Nevron.Nov.Diagram.Import.Map.NShapeCreatedListener

            Public Overrides Function OnMultiPolygonCreated(ByVal shape As Nevron.Nov.Diagram.NShape, ByVal feature As Nevron.Nov.Diagram.Import.Map.NGisFeature) As Boolean
                Return Me.OnPolygonCreated(shape, feature)
            End Function

            Public Overrides Function OnPolygonCreated(ByVal shape As Nevron.Nov.Diagram.NShape, ByVal feature As Nevron.Nov.Diagram.Import.Map.NGisFeature) As Boolean
                Dim row As Nevron.Nov.Data.NDataTableRow = feature.Attributes
                Dim countryName As String = CStr(row("name_long"))
                Dim population As Decimal = CDec(row("pop_est"))
                Dim tooltip As String = countryName & "'s population: " & population.ToString("N0")
                shape.Tooltip = New Nevron.Nov.UI.NTooltip(tooltip)
                Return True
            End Function
        End Class

		#EndRegion
	End Class
End Namespace
