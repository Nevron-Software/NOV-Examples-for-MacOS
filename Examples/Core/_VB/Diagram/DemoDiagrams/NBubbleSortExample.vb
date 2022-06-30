Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NBubbleSortExample
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
            Nevron.Nov.Examples.Diagram.NBubbleSortExample.NBubbleSortExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NBubbleSortExample), NExampleBaseSchema)
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
	Demonstrates how to create a flowchart that describes the Bubble Sort algorithm.
</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim sheet As Nevron.Nov.Dom.NStyleSheet = New Nevron.Nov.Dom.NStyleSheet()
            drawingDocument.StyleSheets.Add(sheet)

            ' create a rule that applies to the TextBlocks of all shapes with user class Connectors
			Dim connectorClass As String = Nevron.Nov.Diagram.NDR.StyleSheetNameConnectors

            If True Then
                Dim rule2 As Nevron.Nov.Dom.NRule = New Nevron.Nov.Dom.NRule()
                sheet.Add(rule2)
                Dim sb As Nevron.Nov.Dom.NSelectorBuilder = rule2.GetSelectorBuilder()
                sb.Start()
                sb.Type(Nevron.Nov.Diagram.NTextBlock.NTextBlockSchema)
                sb.ChildOf()
                sb.UserClass(connectorClass)
                sb.[End]()
                rule2.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Graphics.NFill)(Nevron.Nov.Diagram.NTextBlock.BackgroundFillProperty, New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)))
            End If

            ' get drawing and active page
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

            ' hide ports and grid
            drawing.ScreenVisibility.ShowGrid = False
            drawing.ScreenVisibility.ShowPorts = False
            Dim basicShapesFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim flowChartingShapesFactory As Nevron.Nov.Diagram.Shapes.NFlowchartShapeFactory = New Nevron.Nov.Diagram.Shapes.NFlowchartShapeFactory()
            Dim connectorShapesFactory As Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory()

            ' create title
            Dim titleShape As Nevron.Nov.Diagram.NShape = basicShapesFactory.CreateTextShape("Bubble Sort")
            titleShape.SetBounds(Me.GetGridCell(0, 1, 2, 1))
            titleShape.TextBlock.FontName = "Arial"
            titleShape.TextBlock.FontSize = 40
            titleShape.TextBlock.FontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold
            titleShape.TextBlock.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(68, 90, 108))
            titleShape.TextBlock.Shadow = New Nevron.Nov.Graphics.NShadow()
            activePage.Items.AddChild(titleShape)

            ' begin shape
            Dim shapeBegin As Nevron.Nov.Diagram.NShape = flowChartingShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Termination)
            shapeBegin.SetBounds(Me.GetGridCell(0, 0))
            shapeBegin.Text = "BEGIN"
            activePage.Items.Add(shapeBegin)

            ' get array item shape
            Dim shapeGetItem As Nevron.Nov.Diagram.NShape = flowChartingShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Data)
            shapeGetItem.SetBounds(Me.GetGridCell(1, 0))
            shapeGetItem.Text = "Get array item [1...n]"
            activePage.Items.Add(shapeGetItem)

            ' i = 1 shape
            Dim shapeI1 As Nevron.Nov.Diagram.NShape = flowChartingShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process)
            shapeI1.SetBounds(Me.GetGridCell(2, 0))
            shapeI1.Text = "i = 1"
            activePage.Items.Add(shapeI1)

            ' j = n shape
            Dim shapeJEN As Nevron.Nov.Diagram.NShape = flowChartingShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process)
            shapeJEN.SetBounds(Me.GetGridCell(3, 0))
            shapeJEN.Text = "j = n"
            activePage.Items.Add(shapeJEN)

            ' less comparison shape
            Dim shapeLess As Nevron.Nov.Diagram.NShape = flowChartingShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Decision)
            shapeLess.SetBounds(Me.GetGridCell(4, 0))
            shapeLess.Text = "item[i] < item[j - 1]?"
            activePage.Items.Add(shapeLess)

            ' swap shape
            Dim shapeSwap As Nevron.Nov.Diagram.NShape = flowChartingShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process)
            shapeSwap.SetBounds(Me.GetGridCell(4, 1))
            shapeSwap.Text = "Swap item[i] and item[j-1]"
            activePage.Items.Add(shapeSwap)

            ' j > i + 1? shape
            Dim shapeJQ As Nevron.Nov.Diagram.NShape = flowChartingShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Decision)
            shapeJQ.SetBounds(Me.GetGridCell(5, 0))
            shapeJQ.Text = "j = (i + 1)?"
            activePage.Items.Add(shapeJQ)

            ' dec j shape
            Dim shapeDecJ As Nevron.Nov.Diagram.NShape = flowChartingShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process)
            shapeDecJ.SetBounds(Me.GetGridCell(5, 1))
            shapeDecJ.Text = "j = j - 1"
            activePage.Items.Add(shapeDecJ)

            ' i > n - 1? shape
            Dim shapeIQ As Nevron.Nov.Diagram.NShape = flowChartingShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Decision)
            shapeIQ.SetBounds(Me.GetGridCell(6, 0))
            shapeIQ.Text = "i = (n - 1)?"
            activePage.Items.Add(shapeIQ)

            ' inc i shape
            Dim shapeIncI As Nevron.Nov.Diagram.NShape = flowChartingShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process)
            shapeIncI.SetBounds(Me.GetGridCell(6, 1))
            shapeIncI.Text = "i = i + 1"
            activePage.Items.Add(shapeIncI)

            ' end shape
            Dim shapeEnd As Nevron.Nov.Diagram.NShape = flowChartingShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Termination)
            shapeEnd.SetBounds(Me.GetGridCell(7, 0))
            shapeEnd.Text = "END"
            activePage.Items.Add(shapeEnd)

            ' connect begin with get array item
            Dim connector1 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
            connector1.UserClass = connectorClass
            activePage.Items.AddChild(connector1)
            connector1.GlueBeginToShape(shapeBegin)
            connector1.GlueEndToShape(shapeGetItem)

            ' connect get array item with i = 1
            Dim connector2 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
            connector2.UserClass = connectorClass
            activePage.Items.AddChild(connector2)
            connector2.GlueBeginToShape(shapeGetItem)
            connector2.GlueEndToShape(shapeI1)

            ' connect i = 1 and j = n
            Dim connector3 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
            connector3.UserClass = connectorClass
            activePage.Items.AddChild(connector3)
            connector3.GlueBeginToShape(shapeI1)
            connector3.GlueEndToShape(shapeJEN)

            ' connect j = n and item[i] < item[j-1]?
            Dim connector4 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
            connector4.UserClass = connectorClass
            activePage.Items.AddChild(connector4)
            connector4.GlueBeginToShape(shapeJEN)
            connector4.GlueEndToShape(shapeLess)

            ' connect item[i] < item[j-1]? and j = (i + 1)? 
            Dim connector5 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
            connector5.UserClass = connectorClass
            connector5.Text = "No"
            activePage.Items.AddChild(connector5)
            connector5.GlueBeginToShape(shapeLess)
            connector5.GlueEndToShape(shapeJQ)

            ' connect j = (i + 1)? and i = (n - 1)?
            Dim connector6 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
            connector6.UserClass = connectorClass
            activePage.Items.AddChild(connector6)
            connector6.GlueBeginToShape(shapeJQ)
            connector6.GlueEndToShape(shapeIQ)

            ' connect i = (n - 1) and END
            Dim connector7 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
            connector7.UserClass = connectorClass
            activePage.Items.AddChild(connector7)
            connector7.GlueBeginToShape(shapeIQ)
            connector7.GlueEndToShape(shapeEnd)

            ' connect item[i] < item[j-1]? and Swap
            Dim connector8 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
            connector8.UserClass = connectorClass
            connector8.Text = "Yes"
            activePage.Items.AddChild(connector8)
            connector8.GlueBeginToShape(shapeLess)
            connector8.GlueEndToShape(shapeSwap)

            ' connect j = (i + 1)? and j = (j - 1)
            Dim connector9 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
            connector9.UserClass = connectorClass
            activePage.Items.AddChild(connector9)
            connector9.GlueBeginToShape(shapeJQ)
            connector9.GlueEndToShape(shapeDecJ)

            ' connect i = (n - 1)? and i = (i + 1)
            Dim connector10 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line)
            connector10.UserClass = connectorClass
            activePage.Items.AddChild(connector10)
            connector10.GlueBeginToShape(shapeIQ)
            connector10.GlueEndToShape(shapeIncI)

            ' connect Swap to No connector
            Dim connector11 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENConnectorShape.TopBottomToSide)
            connector11.UserClass = connectorClass
            activePage.Items.AddChild(connector11)
            connector11.GlueBeginToShape(shapeSwap)
            connector11.GlueEndToShape(connector5)

            ' connect i = i + 1 to connector3
            Dim connector12 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateSideToSide(Me.m_GridSpacing.Width * 2)
            connector12.UserClass = connectorClass
            activePage.Items.AddChild(connector12)
            connector12.GlueBeginToPort(shapeIncI.GetPortByName("Right"))
            connector12.GlueEndToGeometryContour(connector3, 0.5F)

            ' connect j = j - 1 to connector4
            Dim connector13 As Nevron.Nov.Diagram.NShape = connectorShapesFactory.CreateSideToSide(Me.m_GridSpacing.Width)
            connector13.UserClass = connectorClass
            activePage.Items.AddChild(connector13)
            connector13.GlueBeginToPort(shapeDecJ.GetPortByName(("Right")))
            connector13.GlueEndToGeometryContour(connector4, 0.5F)
        End Sub

        #EndRegion

        #Region"Implementation"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <paramname="row"></param>
        ''' <paramname="col"></param>
        ''' <returns></returns>
        Protected Function GetGridCell(ByVal row As Integer, ByVal col As Integer) As Nevron.Nov.Graphics.NRectangle
            Return Me.GetGridCell(row, col, Me.m_GridOrigin, Me.m_GridCellSize, Me.m_GridSpacing)
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <paramname="row"></param>
        ''' <paramname="col"></param>
        ''' <paramname="rowSpan"></param>
        ''' <paramname="colSpan"></param>
        ''' <returns></returns>
        Protected Function GetGridCell(ByVal row As Integer, ByVal col As Integer, ByVal rowSpan As Integer, ByVal colSpan As Integer) As Nevron.Nov.Graphics.NRectangle
            Dim cell1 As Nevron.Nov.Graphics.NRectangle = Me.GetGridCell(row, col, Me.m_GridOrigin, Me.m_GridCellSize, Me.m_GridSpacing)
            Dim cell2 As Nevron.Nov.Graphics.NRectangle = Me.GetGridCell(row + rowSpan, col + colSpan, Me.m_GridOrigin, Me.m_GridCellSize, Me.m_GridSpacing)
            Return Nevron.Nov.Graphics.NRectangle.Union(cell1, cell2)
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <paramname="row"></param>
        ''' <paramname="col"></param>
        ''' <paramname="origin"></param>
        ''' <paramname="size"></param>
        ''' <paramname="spacing"></param>
        ''' <returns></returns>
        Protected Function GetGridCell(ByVal row As Integer, ByVal col As Integer, ByVal origin As Nevron.Nov.Graphics.NPoint, ByVal size As Nevron.Nov.Graphics.NSize, ByVal spacing As Nevron.Nov.Graphics.NSize) As Nevron.Nov.Graphics.NRectangle
            Return New Nevron.Nov.Graphics.NRectangle(origin.X + col * (size.Width + spacing.Width), origin.Y + row * (size.Height + spacing.Height), size.Width, size.Height)
        End Function

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_GridOrigin As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(30, 30)
        Private m_GridCellSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(180, 70)
        Private m_GridSpacing As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(50, 40)

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NBubbleSortExample.
        ''' </summary>
        Public Shared ReadOnly NBubbleSortExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
