Imports System
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NInstallProgramExample
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
            Nevron.Nov.Examples.Diagram.NInstallProgramExample.NInstallProgramExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NInstallProgramExample), NExampleBaseSchema)
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
            Return "<p>Demonstrates how to create a flowchart that describes a Software Installation process.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim sheet As Nevron.Nov.Dom.NStyleSheet = New Nevron.Nov.Dom.NStyleSheet()
            drawingDocument.StyleSheets.Add(sheet)

            ' create a rule that applies to the geometries of all shapes with user class Connectors
            Const connectorsClass As String = "Connector"

            If True Then
                Dim rule As Nevron.Nov.Dom.NRule = sheet.CreateRule(Sub(ByVal sb As Nevron.Nov.Dom.NSelectorBuilder)
                                                                        sb.Type(Nevron.Nov.Diagram.NGeometry.NGeometrySchema)
                                                                        sb.ChildOf()
                                                                        sb.UserClass(connectorsClass)
                                                                    End Sub)
                rule.AddValueDeclaration(Of Nevron.Nov.Diagram.NArrowhead)(Nevron.Nov.Diagram.NGeometry.EndArrowheadProperty, New Nevron.Nov.Diagram.NArrowhead(Nevron.Nov.Diagram.ENArrowheadShape.TriangleNoFill), True)
            End If

            ' create a rule that applies to the TextBlocks of all shapes with user class Connectors
            If True Then
                Dim rule As Nevron.Nov.Dom.NRule = sheet.CreateRule(Sub(ByVal sb As Nevron.Nov.Dom.NSelectorBuilder)
                                                                        sb.Type(Nevron.Nov.Diagram.NTextBlock.NTextBlockSchema)
                                                                        sb.ChildOf()
                                                                        sb.UserClass(connectorsClass)
                                                                    End Sub)
                rule.AddValueDeclaration(Of Nevron.Nov.Graphics.NFill)(Nevron.Nov.Diagram.NTextBlock.BackgroundFillProperty, New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White))
            End If

            ' create a rule that applies to shapes with user class  "STARTEND"
            If True Then
                Dim rule As Nevron.Nov.Dom.NRule = sheet.CreateRule(Sub(ByVal sb As Nevron.Nov.Dom.NSelectorBuilder)
                                                                        sb.Type(Nevron.Nov.Diagram.NGeometry.NGeometrySchema)
                                                                        sb.ChildOf()
                                                                        sb.UserClass("STARTEND")
                                                                    End Sub)
                rule.AddValueDeclaration(Of Nevron.Nov.Graphics.NFill)(Nevron.Nov.Diagram.NGeometry.FillProperty, New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Horizontal, Nevron.Nov.Graphics.ENGradientVariant.Variant1, New Nevron.Nov.Graphics.NColor(247, 150, 56), New Nevron.Nov.Graphics.NColor(251, 203, 156)))
            End If

            ' create a rule that applies to shapes with user class  "QUESTION"
            If True Then
                Dim rule As Nevron.Nov.Dom.NRule = sheet.CreateRule(Sub(ByVal sb As Nevron.Nov.Dom.NSelectorBuilder)
                                                                        sb.Type(Nevron.Nov.Diagram.NGeometry.NGeometrySchema)
                                                                        sb.ChildOf()
                                                                        sb.UserClass("QUESTION")
                                                                    End Sub)
                rule.AddValueDeclaration(Of Nevron.Nov.Graphics.NFill)(Nevron.Nov.Diagram.NGeometry.FillProperty, New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Horizontal, Nevron.Nov.Graphics.ENGradientVariant.Variant1, New Nevron.Nov.Graphics.NColor(129, 133, 133), New Nevron.Nov.Graphics.NColor(192, 194, 194)))
            End If

            ' create a rule that applies to shapes with user class  "ACTION"
            If True Then
                Dim rule As Nevron.Nov.Dom.NRule = sheet.CreateRule(Sub(ByVal sb As Nevron.Nov.Dom.NSelectorBuilder)
                                                                        sb.Type(Nevron.Nov.Diagram.NGeometry.NGeometrySchema)
                                                                        sb.ChildOf()
                                                                        sb.UserClass("ACTION")
                                                                    End Sub)
                rule.AddValueDeclaration(Of Nevron.Nov.Graphics.NFill)(Nevron.Nov.Diagram.NGeometry.FillProperty, New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Horizontal, Nevron.Nov.Graphics.ENGradientVariant.Variant1, New Nevron.Nov.Graphics.NColor(68, 90, 108), New Nevron.Nov.Graphics.NColor(162, 173, 182)))
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
            Dim bounds As Nevron.Nov.Graphics.NRectangle
            Dim vSpacing As Integer = 35
            Dim hSpacing As Integer = 45
            Dim topMargin As Integer = 10
            Dim leftMargin As Integer = 10
            Dim shapeWidth As Integer = 90
            Dim shapeHeight As Integer = 55
            Dim col1 As Integer = leftMargin
            Dim col2 As Integer = col1 + shapeWidth + hSpacing
            Dim col3 As Integer = col2 + shapeWidth + hSpacing
            Dim col4 As Integer = col3 + shapeWidth + hSpacing
            Dim row1 As Integer = topMargin
            Dim row2 As Integer = row1 + shapeHeight + vSpacing
            Dim row3 As Integer = row2 + shapeHeight + vSpacing
            Dim row4 As Integer = row3 + shapeHeight + vSpacing
            Dim row5 As Integer = row4 + shapeHeight + vSpacing
            Dim row6 As Integer = row5 + shapeHeight + vSpacing
            bounds = New Nevron.Nov.Graphics.NRectangle(col2, row1, shapeWidth, shapeHeight)
            Dim start As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Termination, bounds, "START", "STARTEND")

            ' row 2
            bounds = New Nevron.Nov.Graphics.NRectangle(col2, row2, shapeWidth, shapeHeight)
            Dim haveSerialNumber As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Decision, bounds, "Have a serial number?", "QUESTION")
            bounds = New Nevron.Nov.Graphics.NRectangle(col3, row2, shapeWidth, shapeHeight)
            Dim getSerialNumber As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process, bounds, "Get serial number", "ACTION")

            ' row 3
            bounds = New Nevron.Nov.Graphics.NRectangle(col1, row3, shapeWidth, shapeHeight)
            Dim enterSerialNumber As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process, bounds, "Enter serial number", "ACTION")
            bounds = New Nevron.Nov.Graphics.NRectangle(col2, row3, shapeWidth, shapeHeight)
            Dim haveDiskSpace As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Decision, bounds, "Have disk space?", "QUESTION")
            bounds = New Nevron.Nov.Graphics.NRectangle(col3, row3, shapeWidth, shapeHeight)
            Dim freeUpSpace As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process, bounds, "Free up space", "ACTION")

            ' row 4
            bounds = New Nevron.Nov.Graphics.NRectangle(col1, row4, shapeWidth, shapeHeight)
            Dim runInstallRect As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process, bounds, "Run install file", "ACTION")
            bounds = New Nevron.Nov.Graphics.NRectangle(col2, row4, shapeWidth, shapeHeight)
            Dim registerNow As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Decision, bounds, "Register now?", "QUESTION")
            bounds = New Nevron.Nov.Graphics.NRectangle(col3, row4, shapeWidth, shapeHeight)
            Dim fillForm As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process, bounds, "Fill out form", "ACTION")
            bounds = New Nevron.Nov.Graphics.NRectangle(col4, row4, shapeWidth, shapeHeight)
            Dim submitForm As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process, bounds, "Submit form", "ACTION")

            ' row 5
            bounds = New Nevron.Nov.Graphics.NRectangle(col1, row5, shapeWidth, shapeHeight)
            Dim finishInstall As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process, bounds, "Finish installation", "ACTION")
            bounds = New Nevron.Nov.Graphics.NRectangle(col2, row5, shapeWidth, shapeHeight)
            Dim restartNeeded As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Decision, bounds, "Restart needed?", "QUESTION")
            bounds = New Nevron.Nov.Graphics.NRectangle(col3, row5, shapeWidth, shapeHeight)
            Dim restart As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process, bounds, "Restart", "ACTION")

            ' row 6
            bounds = New Nevron.Nov.Graphics.NRectangle(col2, row6, shapeWidth, shapeHeight)
            Dim run As Nevron.Nov.Diagram.NShape = Me.CreateFlowChartingShape(Nevron.Nov.Diagram.Shapes.ENFlowchartingShape.Process, bounds, "RUN", "STARTEND")

            ' create connectors
            Me.CreateConnector(start, "Bottom", haveSerialNumber, "Top", Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line, "")
            Me.CreateConnector(getSerialNumber, "Top", haveSerialNumber, "Top", Nevron.Nov.Diagram.Shapes.ENConnectorShape.RoutableConnector, "")
            Me.CreateConnector(haveSerialNumber, "Right", getSerialNumber, "Left", Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line, "No")
            Me.CreateConnector(haveSerialNumber, "Bottom", enterSerialNumber, "Top", Nevron.Nov.Diagram.Shapes.ENConnectorShape.BottomToTop1, "Yes")
            Me.CreateConnector(enterSerialNumber, "Right", haveDiskSpace, "Left", Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line, "")
            Me.CreateConnector(freeUpSpace, "Top", haveDiskSpace, "Top", Nevron.Nov.Diagram.Shapes.ENConnectorShape.RoutableConnector, "")
            Me.CreateConnector(haveDiskSpace, "Right", freeUpSpace, "Left", Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line, "No")
            Me.CreateConnector(haveDiskSpace, "Bottom", runInstallRect, "Top", Nevron.Nov.Diagram.Shapes.ENConnectorShape.BottomToTop1, "Yes")
            Me.CreateConnector(registerNow, "Right", fillForm, "Left", Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line, "Yes")
            Me.CreateConnector(registerNow, "Bottom", finishInstall, "Top", Nevron.Nov.Diagram.Shapes.ENConnectorShape.BottomToTop1, "No")
            Me.CreateConnector(fillForm, "Right", submitForm, "Left", Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line, "")
            Me.CreateConnector(submitForm, "Bottom", finishInstall, "Top", Nevron.Nov.Diagram.Shapes.ENConnectorShape.BottomToTop1, "")
            Me.CreateConnector(finishInstall, "Right", restartNeeded, "Left", Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line, "")
            Me.CreateConnector(restart, "Bottom", run, "Top", Nevron.Nov.Diagram.Shapes.ENConnectorShape.BottomToTop1, "")
            Me.CreateConnector(restartNeeded, "Right", restart, "Left", Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line, "Yes")
            Me.CreateConnector(restartNeeded, "Bottom", run, "Top", Nevron.Nov.Diagram.Shapes.ENConnectorShape.Line, "No")
        End Sub

        #EndRegion

        #Region"Implementation"

        ''' <summary>
        ''' Creates a predefined basic shape
        ''' </summary>
        ''' <paramname="basicShape">basic shape</param>
        ''' <paramname="bounds">bounds</param>
        ''' <paramname="text">default label text</param>
        ''' <paramname="userClass">name of the stylesheet from which to inherit styles</param>
        ''' <returns>new basic shape</returns>
        Private Function CreateBasicShape(ByVal basicShape As Nevron.Nov.Diagram.Shapes.ENBasicShape, ByVal bounds As Nevron.Nov.Graphics.NRectangle, ByVal text As String, ByVal userClass As String) As Nevron.Nov.Diagram.NShape
            ' create shape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory().CreateShape(basicShape)

            ' set bounds, text and user class
            shape.SetBounds(bounds)
            shape.Text = text
            shape.UserClass = userClass

            ' add to active page
            Me.m_DrawingView.ActivePage.Items.Add(shape)
            Return shape
        End Function
        ''' <summary>
        ''' Creates a predefined flow charting shape
        ''' </summary>
        ''' <paramname="flowChartShape">flow charting shape</param>
        ''' <paramname="bounds">bounds</param>
        ''' <paramname="text">default label text</param>
        ''' <paramname="userClass">name of the stylesheet from which to inherit styles</param>
        ''' <returns>new basic shape</returns>
        Private Function CreateFlowChartingShape(ByVal flowChartShape As Nevron.Nov.Diagram.Shapes.ENFlowchartingShape, ByVal bounds As Nevron.Nov.Graphics.NRectangle, ByVal text As String, ByVal userClass As String) As Nevron.Nov.Diagram.NShape
            ' create shape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.Shapes.NFlowchartShapeFactory().CreateShape(flowChartShape)

            ' set bounds, text and user class
            shape.SetBounds(bounds)
            shape.Text = text
            shape.UserClass = userClass

            ' add to active page
            Me.m_DrawingView.ActivePage.Items.Add(shape)
            Return shape
        End Function
        ''' <summary>
        ''' Creates a new connector, which connects the specified shapes
        ''' </summary>
        ''' <paramname="fromShape"></param>
        ''' <paramname="fromPortName"></param>
        ''' <paramname="toShape"></param>
        ''' <paramname="toPortName"></param>
        ''' <paramname="connectorType"></param>
        ''' <paramname="text"></param>
        ''' <returns>new 1D shapes</returns>
        Private Function CreateConnector(ByVal fromShape As Nevron.Nov.Diagram.NShape, ByVal fromPortName As String, ByVal toShape As Nevron.Nov.Diagram.NShape, ByVal toPortName As String, ByVal connectorType As Nevron.Nov.Diagram.Shapes.ENConnectorShape, ByVal text As String) As Nevron.Nov.Diagram.NShape
            ' check arguments
            If fromShape Is Nothing Then Throw New System.ArgumentNullException("fromShape")
            If toShape Is Nothing Then Throw New System.ArgumentNullException("toShape")

            ' create the connector
            Dim connector As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.Shapes.NConnectorShapeFactory().CreateShape(connectorType)

            ' set text and user class
            connector.Text = text
            connector.UserClass = Nevron.Nov.Diagram.NDR.StyleSheetNameConnectors

            ' connect begin
            Dim fromPort As Nevron.Nov.Diagram.NPort = fromShape.Ports.GetPortByName(fromPortName)

            If fromPort IsNot Nothing Then
                connector.GlueBeginToPort(fromPort)
            Else
                connector.GlueBeginToShape(fromShape)
            End If

            ' connect end
            Dim toPort As Nevron.Nov.Diagram.NPort = toShape.Ports.GetPortByName(toPortName)

            If toPort IsNot Nothing Then
                connector.GlueEndToPort(toPort)
            Else
                connector.GlueEndToShape(toShape)
            End If

            ' add to active page
            Me.m_DrawingView.ActivePage.Items.Add(connector)
            Return connector
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
        ''' Schema associated with NInstallProgramExample.
        ''' </summary>
        Public Shared ReadOnly NInstallProgramExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
