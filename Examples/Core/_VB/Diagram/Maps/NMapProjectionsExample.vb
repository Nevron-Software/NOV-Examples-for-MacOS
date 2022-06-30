Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Import.Map
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

#Region"InternalCode"

' Map data from: http://www.naturalearthdata.com/features/

#EndRegion

Namespace Nevron.Nov.Examples.Diagram
    Public Class NMapProjectionsExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor.
		''' </summary>
		Public Sub New()
            Me.m_Projections = New Nevron.Nov.Diagram.Import.Map.NMapProjection() {New Nevron.Nov.Diagram.Import.Map.NAitoffProjection(), New Nevron.Nov.Diagram.Import.Map.NBonneProjection(), New Nevron.Nov.Diagram.Import.Map.NCylindricalEqualAreaProjection(Nevron.Nov.Diagram.Import.Map.ENCylindricalEqualAreaProjectionType.Lambert), New Nevron.Nov.Diagram.Import.Map.NCylindricalEqualAreaProjection(Nevron.Nov.Diagram.Import.Map.ENCylindricalEqualAreaProjectionType.Behrmann), New Nevron.Nov.Diagram.Import.Map.NCylindricalEqualAreaProjection(Nevron.Nov.Diagram.Import.Map.ENCylindricalEqualAreaProjectionType.TristanEdwards), New Nevron.Nov.Diagram.Import.Map.NCylindricalEqualAreaProjection(Nevron.Nov.Diagram.Import.Map.ENCylindricalEqualAreaProjectionType.Peters), New Nevron.Nov.Diagram.Import.Map.NCylindricalEqualAreaProjection(Nevron.Nov.Diagram.Import.Map.ENCylindricalEqualAreaProjectionType.Gall), New Nevron.Nov.Diagram.Import.Map.NCylindricalEqualAreaProjection(Nevron.Nov.Diagram.Import.Map.ENCylindricalEqualAreaProjectionType.Balthasart), New Nevron.Nov.Diagram.Import.Map.NEckertIVProjection(), New Nevron.Nov.Diagram.Import.Map.NEckertVIProjection(), New Nevron.Nov.Diagram.Import.Map.NEquirectangularProjection(), New Nevron.Nov.Diagram.Import.Map.NHammerProjection(), New Nevron.Nov.Diagram.Import.Map.NKavrayskiyVIIProjection(), New Nevron.Nov.Diagram.Import.Map.NMercatorProjection(), New Nevron.Nov.Diagram.Import.Map.NMillerCylindricalProjection(), New Nevron.Nov.Diagram.Import.Map.NMollweideProjection(), New Nevron.Nov.Diagram.Import.Map.NOrthographicProjection(), New Nevron.Nov.Diagram.Import.Map.NRobinsonProjection(), New Nevron.Nov.Diagram.Import.Map.NStereographicProjection(), New Nevron.Nov.Diagram.Import.Map.NVanDerGrintenProjection(), New Nevron.Nov.Diagram.Import.Map.NWagnerVIProjection(), New Nevron.Nov.Diagram.Import.Map.NWinkelTripelProjection()}
        End Sub

		''' <summary>
		''' Static constructor.
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Diagram.NMapProjectionsExample.NMapProjectionsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NMapProjectionsExample), NExampleBaseSchema)
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
			
			' Create the projection combo box
			Me.m_ProjectionComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_ProjectionComboBox.FillFromArray(Me.m_Projections)
            Me.m_ProjectionComboBox.SelectedIndex = Nevron.Nov.Examples.Diagram.NMapProjectionsExample.DefaultProjectionIndex
            AddHandler Me.m_ProjectionComboBox.SelectedIndexChanged, AddressOf Me.OnProjectionComboSelectedIndexChanged
            Dim pairBox As Nevron.Nov.UI.NPairBox = Nevron.Nov.UI.NPairBox.Create("Projection:", Me.m_ProjectionComboBox)
            stack.Add(pairBox)

			' Create the label arcs check box
			Dim labelArcsCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox()
            AddHandler labelArcsCheckBox.CheckedChanged, AddressOf Me.OnLabelArcsCheckBoxCheckedChanged
            labelArcsCheckBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            labelArcsCheckBox.Padding = Nevron.Nov.Graphics.NMargins.Zero
            pairBox = Nevron.Nov.UI.NPairBox.Create("Label arcs:", labelArcsCheckBox)
            stack.Add(pairBox)

			' Create the center parallel numeric up down
			Me.m_CenterParalelNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CenterParalelNumericUpDown.Minimum = -90
            Me.m_CenterParalelNumericUpDown.Maximum = 90
            Me.m_CenterParalelNumericUpDown.[Step] = 15
            AddHandler Me.m_CenterParalelNumericUpDown.ValueChanged, AddressOf Me.OnCenterParallelNumericUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Central parallel:", Me.m_CenterParalelNumericUpDown))

			' Create the center meridian numeric up down
			Me.m_CenterMeridianNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CenterMeridianNumericUpDown.Minimum = -180
            Me.m_CenterMeridianNumericUpDown.Maximum = 180
            Me.m_CenterMeridianNumericUpDown.[Step] = 15
            AddHandler Me.m_CenterMeridianNumericUpDown.ValueChanged, AddressOf Me.OnCenterMeridianNumericUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Central meridian:", Me.m_CenterMeridianNumericUpDown))
            Dim settingsGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Settings", New Nevron.Nov.UI.NUniSizeBoxGroup(stack))
            Return settingsGroupBox
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    <b>NOV Diagram</b> makes it easy to import geographical data from ESRI shapefiles. You
    can control the way the shapes are rendered by applying various fill rules to them. You can
	also specify a map projection to be used for transforming the 3D geographical data to 2D
	screen coordinates as this example demonstrates. Using the controls on the right you can
	change the map projection and turn on or off the arc labelling. Note that some projections
	also lets you specify a central parallel and/or meridian.
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
            page.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightBlue)
            page.Bounds = New Nevron.Nov.Graphics.NRectangle(0, 0, 10000, 10000)
            page.ZoomMode = Nevron.Nov.UI.ENZoomMode.Fit

			' Create a map importer
			Me.m_MapImporter = New Nevron.Nov.Diagram.Import.Map.NEsriMapImporter()
            Me.m_MapImporter.MapBounds = Nevron.Nov.Diagram.Import.Map.NMapBounds.World
            Me.m_MapImporter.MeridianSettings.RenderMode = Nevron.Nov.Diagram.Import.Map.ENArcRenderMode.BelowObjects
            Me.m_MapImporter.ParallelSettings.RenderMode = Nevron.Nov.Diagram.Import.Map.ENArcRenderMode.BelowObjects
            Me.m_MapImporter.Projection = Me.m_Projections(Nevron.Nov.Examples.Diagram.NMapProjectionsExample.DefaultProjectionIndex)

			' Add an ESRI shapefile
			Dim countries As Nevron.Nov.Diagram.Import.Map.NEsriShapefile = New Nevron.Nov.Diagram.Import.Map.NEsriShapefile(Nevron.Nov.Diagram.NResources.RBIN_countries_zip)
            countries.NameColumn = "name_long"
            countries.FillRule = New Nevron.Nov.Diagram.Import.Map.NMapFillRuleValue("mapcolor8", Nevron.Nov.Examples.Diagram.NMapProjectionsExample.Colors)
            Me.m_MapImporter.AddShapefile(countries)

			' Read the map data
			Me.m_MapImporter.Read()
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
            rule.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Graphics.NStroke)(Nevron.Nov.Diagram.NGeometry.StrokeProperty, New Nevron.Nov.Graphics.NStroke(New Nevron.Nov.Graphics.NColor(68, 90, 108))))
            styleSheet.Add(rule)
        End Sub

        Private Sub ImportMap()
            Dim page As Nevron.Nov.Diagram.NPage = Me.m_DrawingView.ActivePage
            page.Items.Clear()

			' Import the map to the drawing document
			Me.m_MapImporter.Import(Me.m_DrawingView.Document, page.Bounds)
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnProjectionComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim projectionCombo As Nevron.Nov.UI.NComboBox = CType(arg.TargetNode, Nevron.Nov.UI.NComboBox)
            Dim projection As Nevron.Nov.Diagram.Import.Map.NMapProjection = CType(projectionCombo.SelectedItem.Tag, Nevron.Nov.Diagram.Import.Map.NMapProjection)
            Me.m_MapImporter.Projection = projection

			' Reimport the map applying the newly selected projection
			If TypeOf Me.m_MapImporter.Projection Is Nevron.Nov.Diagram.Import.Map.NOrthographicProjection Then
                Nevron.Nov.Examples.Diagram.NMapProjectionsExample.GetOwnerPairBox(CType((Me.m_CenterParalelNumericUpDown), Nevron.Nov.UI.NWidget)).Visibility = Nevron.Nov.UI.ENVisibility.Visible
                Nevron.Nov.Examples.Diagram.NMapProjectionsExample.GetOwnerPairBox(CType((Me.m_CenterMeridianNumericUpDown), Nevron.Nov.UI.NWidget)).Visibility = Nevron.Nov.UI.ENVisibility.Visible
                Dim ortographicProjection As Nevron.Nov.Diagram.Import.Map.NOrthographicProjection = CType(Me.m_MapImporter.Projection, Nevron.Nov.Diagram.Import.Map.NOrthographicProjection)
                ortographicProjection.CenterPoint = New Nevron.Nov.Graphics.NPoint(Me.m_CenterMeridianNumericUpDown.Value, Me.m_CenterParalelNumericUpDown.Value)
            ElseIf TypeOf Me.m_MapImporter.Projection Is Nevron.Nov.Diagram.Import.Map.NBonneProjection Then
                Nevron.Nov.Examples.Diagram.NMapProjectionsExample.GetOwnerPairBox(CType((Me.m_CenterParalelNumericUpDown), Nevron.Nov.UI.NWidget)).Visibility = Nevron.Nov.UI.ENVisibility.Visible
                Nevron.Nov.Examples.Diagram.NMapProjectionsExample.GetOwnerPairBox(CType((Me.m_CenterMeridianNumericUpDown), Nevron.Nov.UI.NWidget)).Visibility = Nevron.Nov.UI.ENVisibility.Hidden
                CType(Me.m_MapImporter.Projection, Nevron.Nov.Diagram.Import.Map.NBonneProjection).StandardParallel = Me.m_CenterParalelNumericUpDown.Value
            Else
                Nevron.Nov.Examples.Diagram.NMapProjectionsExample.GetOwnerPairBox(CType((Me.m_CenterParalelNumericUpDown), Nevron.Nov.UI.NWidget)).Visibility = Nevron.Nov.UI.ENVisibility.Hidden
                Nevron.Nov.Examples.Diagram.NMapProjectionsExample.GetOwnerPairBox(CType((Me.m_CenterMeridianNumericUpDown), Nevron.Nov.UI.NWidget)).Visibility = Nevron.Nov.UI.ENVisibility.Hidden
            End If

            Me.ImportMap()
        End Sub

        Private Sub OnLabelArcsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim showLabels As Boolean = CBool(arg.NewValue)
            Me.m_MapImporter.ParallelSettings.ShowLabels = showLabels
            Me.m_MapImporter.MeridianSettings.ShowLabels = showLabels
            Me.ImportMap()
        End Sub

        Private Sub OnCenterParallelNumericUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim value As Double = CDbl(arg.NewValue)

            If TypeOf Me.m_MapImporter.Projection Is Nevron.Nov.Diagram.Import.Map.NBonneProjection Then
                CType(Me.m_MapImporter.Projection, Nevron.Nov.Diagram.Import.Map.NBonneProjection).StandardParallel = value
                Me.ImportMap()
            ElseIf TypeOf Me.m_MapImporter.Projection Is Nevron.Nov.Diagram.Import.Map.NOrthographicProjection Then
                Dim ortographicProjection As Nevron.Nov.Diagram.Import.Map.NOrthographicProjection = CType(Me.m_MapImporter.Projection, Nevron.Nov.Diagram.Import.Map.NOrthographicProjection)
                ortographicProjection.CenterPoint = New Nevron.Nov.Graphics.NPoint(ortographicProjection.CenterPoint.X, value)
                Me.ImportMap()
            End If
        End Sub

        Private Sub OnCenterMeridianNumericUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim value As Double = CDbl(arg.NewValue)

            If TypeOf Me.m_MapImporter.Projection Is Nevron.Nov.Diagram.Import.Map.NOrthographicProjection Then
                Dim ortographicProjection As Nevron.Nov.Diagram.Import.Map.NOrthographicProjection = CType(Me.m_MapImporter.Projection, Nevron.Nov.Diagram.Import.Map.NOrthographicProjection)
                ortographicProjection.CenterPoint = New Nevron.Nov.Graphics.NPoint(value, ortographicProjection.CenterPoint.Y)
                Me.ImportMap()
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_Projections As Nevron.Nov.Diagram.Import.Map.NMapProjection()
        Private m_MapImporter As Nevron.Nov.Diagram.Import.Map.NEsriMapImporter
        Private m_ProjectionComboBox As Nevron.Nov.UI.NComboBox
        Private m_CenterParalelNumericUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_CenterMeridianNumericUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NMapProjectionsExample.
		''' </summary>
		Public Shared ReadOnly NMapProjectionsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function GetOwnerPairBox(ByVal widget As Nevron.Nov.UI.NWidget) As Nevron.Nov.UI.NPairBox
            Return CType(widget.GetFirstAncestor(Nevron.Nov.UI.NPairBox.NPairBoxSchema), Nevron.Nov.UI.NPairBox)
        End Function

		#EndRegion

		#Region"Constants"

		Private Const DefaultProjectionIndex As Integer = 16

		''' <summary>
		''' The colors used to fill the countries.
		''' </summary>
		Private Shared ReadOnly Colors As Nevron.Nov.Graphics.NColor() = New Nevron.Nov.Graphics.NColor() {Nevron.Nov.Graphics.NColor.OldLace, Nevron.Nov.Graphics.NColor.PaleGreen, Nevron.Nov.Graphics.NColor.Gold, Nevron.Nov.Graphics.NColor.Khaki, Nevron.Nov.Graphics.NColor.Tan, Nevron.Nov.Graphics.NColor.Orange, Nevron.Nov.Graphics.NColor.Salmon, Nevron.Nov.Graphics.NColor.PaleGoldenrod}

		#EndRegion
	End Class
End Namespace
