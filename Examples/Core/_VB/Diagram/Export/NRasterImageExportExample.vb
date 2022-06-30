Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Export
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NRasterImageExportExample
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
            Nevron.Nov.Examples.Diagram.NRasterImageExportExample.NRasterImageExportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NRasterImageExportExample), NExampleBaseSchema)
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
            Dim stackPanel As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim showImageExportButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Show Raster Image Export Dialog...")
            AddHandler showImageExportButton.Click, AddressOf Me.OnShowImageExportButtonClick
            stackPanel.Add(showImageExportButton)
            Dim copyImageToClipboardButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Copy Raster Image to Clipboard")
            AddHandler copyImageToClipboardButton.Click, AddressOf Me.OnCopyImageToClipboardButtonClick
            stackPanel.Add(copyImageToClipboardButton)
            Dim saveAsRasterImageFileButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Save as Raster Image File...")
            AddHandler saveAsRasterImageFileButton.Click, AddressOf Me.OnSaveAsRasterImageFileButtonClick
            stackPanel.Add(saveAsRasterImageFileButton)
            Dim saveAsVectorImageFileButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Save as Vector Image File...")
            AddHandler saveAsVectorImageFileButton.Click, AddressOf Me.OnSaveAsVectorImageFileButtonClick
            stackPanel.Add(saveAsVectorImageFileButton)
            Return stackPanel
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates the image exporters, with the help of which you can export the active page or any portion of it
	to a raster (PNG, JPG, BMP, GIF) or a vector (SVG) image.
</p>
            "
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage
            drawing.ScreenVisibility.ShowGrid = False
            drawing.ScreenVisibility.ShowPorts = False
            Dim basisShapes As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim flowChartingShapes As Nevron.Nov.Diagram.Shapes.NFlowchartShapeFactory = New Nevron.Nov.Diagram.Shapes.NFlowchartShapeFactory()
            Dim connectorShapes As Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory()
            Dim nonPrintableShape As Nevron.Nov.Diagram.NShape = basisShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            nonPrintableShape.Text = "Non printable shape"
            nonPrintableShape.AllowPrint = False
            nonPrintableShape.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Tomato)
            nonPrintableShape.SetBounds(50, 50, 150, 50)
            activePage.Items.Add(nonPrintableShape)
            Dim isLifeGood As Nevron.Nov.Diagram.NShape = flowChartingShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Decision)
            isLifeGood.Text = "Is Life Good?"
            isLifeGood.SetBounds(300, 50, 150, 100)
            isLifeGood.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightSkyBlue)
            activePage.Items.Add(isLifeGood)
            Dim goodShape As Nevron.Nov.Diagram.NShape = flowChartingShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Termination)
            goodShape.Text = "Good"
            goodShape.SetBounds(200, 200, 100, 100)
            goodShape.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Gold)
            activePage.Items.Add(goodShape)
            Dim changeSomething As Nevron.Nov.Diagram.NShape = flowChartingShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process)
            changeSomething.Text = "Change Something"
            changeSomething.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Thistle)
            changeSomething.SetBounds(450, 200, 100, 100)
            activePage.Items.Add(changeSomething)
            Dim yesConnector As Nevron.Nov.Diagram.NShape = connectorShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.RoutableConnector)
            yesConnector.Text = "Yes"
            yesConnector.GlueBeginToPort(isLifeGood.GetPortByName("Left"))
            yesConnector.GlueEndToPort(goodShape.GetPortByName("Top"))
            activePage.Items.Add(yesConnector)
            Dim noConnector As Nevron.Nov.Diagram.NShape = connectorShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.RoutableConnector)
            noConnector.Text = "No"
            noConnector.GlueBeginToPort(isLifeGood.GetPortByName("Right"))
            noConnector.GlueEndToPort(changeSomething.GetPortByName("Top"))
            activePage.Items.Add(noConnector)
            Dim gobackConnector As Nevron.Nov.Diagram.NShape = connectorShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.RoutableConnector)
            gobackConnector.GlueBeginToPort(changeSomething.GetPortByName("Right"))
            gobackConnector.GlueEndToPort(isLifeGood.GetPortByName("Top"))
            activePage.Items.Add(gobackConnector)
        End Sub

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnShowImageExportButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim imageExporter As Nevron.Nov.Diagram.Export.NDrawingRasterImageExporter = New Nevron.Nov.Diagram.Export.NDrawingRasterImageExporter(Me.m_DrawingView.Drawing)
            imageExporter.ShowDialog(DisplayWindow, True)
        End Sub

        Private Sub OnCopyImageToClipboardButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim imageExporter As Nevron.Nov.Diagram.Export.NDrawingRasterImageExporter = New Nevron.Nov.Diagram.Export.NDrawingRasterImageExporter(Me.m_DrawingView.Drawing)
            imageExporter.CopyToClipboard()
        End Sub

        Private Sub OnSaveAsRasterImageFileButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim imageExporter As Nevron.Nov.Diagram.Export.NDrawingRasterImageExporter = New Nevron.Nov.Diagram.Export.NDrawingRasterImageExporter(Me.m_DrawingView.Drawing)
            imageExporter.SaveAsImage()
        End Sub

        Private Sub OnSaveAsVectorImageFileButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim imageExporter As Nevron.Nov.Diagram.Export.NDrawingVectorImageExporter = New Nevron.Nov.Diagram.Export.NDrawingVectorImageExporter(Me.m_DrawingView.Drawing)
            imageExporter.SaveAsImage()
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NRasterImageExportExample.
        ''' </summary>
        Public Shared ReadOnly NRasterImageExportExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
