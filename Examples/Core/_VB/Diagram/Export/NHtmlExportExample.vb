Imports System
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NHtmlExportExample
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
            Nevron.Nov.Examples.Diagram.NHtmlExportExample.NHtmlExportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NHtmlExportExample), NExampleBaseSchema)
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
            Dim saveAsButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Save as Web Page...")
            AddHandler saveAsButton.Click, AddressOf Me.OnSaveAsButtonClick
            stackPanel.Add(saveAsButton)
            Return stackPanel
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates how to export a NOV Diagram drawing to a web page (HTML). The drawing is exported
	to SVG that gets inserted into an HTML web page. If the drawing has multiple pages, then some
	CSS and JavaScript are inserted in the HTML web page in order to create a tab navigation interface.
	Each drawing page is inserted into a tab page.
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage
            drawing.ScreenVisibility.ShowGrid = False
            drawing.ScreenVisibility.ShowPorts = False
            Me.CreateDiagram(activePage)
            Dim page As Nevron.Nov.Diagram.NPage = New Nevron.Nov.Diagram.NPage("Page-2")
            drawing.Pages.Add(page)
            Me.CreateDiagram(page)
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub CreateDiagram(ByVal page As Nevron.Nov.Diagram.NPage)
            Dim basisShapes As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim flowChartingShapes As Nevron.Nov.Diagram.Shapes.NFlowchartShapeFactory = New Nevron.Nov.Diagram.Shapes.NFlowchartShapeFactory()
            Dim connectorShapes As Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory()
            Dim titleShape As Nevron.Nov.Diagram.NShape = basisShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            titleShape.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightGray)
            titleShape.Text = page.Title
            titleShape.SetBounds(10, 10, page.Width - 20, 50)
            page.Items.Add(titleShape)
            Dim nonPrintableShape As Nevron.Nov.Diagram.NShape = basisShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            nonPrintableShape.Text = "Non printable shape"
            nonPrintableShape.AllowPrint = False
            nonPrintableShape.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Tomato)
            nonPrintableShape.SetBounds(50, 150, 150, 50)
            page.Items.Add(nonPrintableShape)
            Dim isLifeGood As Nevron.Nov.Diagram.NShape = flowChartingShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Decision)
            isLifeGood.Text = "Is Life Good?"
            isLifeGood.SetBounds(300, 150, 150, 100)
            isLifeGood.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightSkyBlue)
            page.Items.Add(isLifeGood)
            Dim goodShape As Nevron.Nov.Diagram.NShape = flowChartingShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Termination)
            goodShape.Text = "Good"
            goodShape.SetBounds(200, 300, 100, 100)
            goodShape.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Gold)
            page.Items.Add(goodShape)
            Dim changeSomething As Nevron.Nov.Diagram.NShape = flowChartingShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process)
            changeSomething.Text = "Change Something"
            changeSomething.Geometry.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Thistle)
            changeSomething.SetBounds(450, 300, 100, 100)
            page.Items.Add(changeSomething)
            Dim yesConnector As Nevron.Nov.Diagram.NShape = connectorShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.RoutableConnector)
            yesConnector.Text = "Yes"
            yesConnector.GlueBeginToPort(isLifeGood.GetPortByName("Left"))
            yesConnector.GlueEndToPort(goodShape.GetPortByName("Top"))
            page.Items.Add(yesConnector)
            Dim noConnector As Nevron.Nov.Diagram.NShape = connectorShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.RoutableConnector)
            noConnector.Text = "No"
            noConnector.GlueBeginToPort(isLifeGood.GetPortByName("Right"))
            noConnector.GlueEndToPort(changeSomething.GetPortByName("Top"))
            page.Items.Add(noConnector)
            Dim gobackConnector As Nevron.Nov.Diagram.NShape = connectorShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.RoutableConnector)
            gobackConnector.GlueBeginToPort(changeSomething.GetPortByName("Right"))
            gobackConnector.GlueEndToPort(isLifeGood.GetPortByName("Top"))
            page.Items.Add(gobackConnector)
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSaveAsButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim fileName As String = Me.m_DrawingView.Drawing.Information.FileName

            If System.[String].IsNullOrEmpty(fileName) OrElse Not fileName.EndsWith("vsdx", System.StringComparison.OrdinalIgnoreCase) Then
				' The document has not been saved, yet, so set a file name with HTML extension
				' to make the default Save As dialog show Web Page as file save as type
				Me.m_DrawingView.Drawing.Information.FileName = "Document1.html"
            End If

            Me.m_DrawingView.SaveAs()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NHtmlExportExample.
		''' </summary>
		Public Shared ReadOnly NHtmlExportExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
