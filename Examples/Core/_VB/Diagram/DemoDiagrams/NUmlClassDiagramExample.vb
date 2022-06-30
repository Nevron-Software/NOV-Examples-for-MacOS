Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NUmlClassDiagramExample
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
            Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NUmlClassDiagramExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample), NExampleBaseSchema)
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
            Return "<p>This example demonstrates how to create an UML class diagrams.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' Get drawing and active page
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

            ' Hide ports and grid
            drawing.ScreenVisibility.ShowGrid = False

            ' Create styles
            Dim rule As Nevron.Nov.Dom.NRule = Me.CreateConnectorOneToManyRule()
            Dim styleSheet As Nevron.Nov.Dom.NStyleSheet = New Nevron.Nov.Dom.NStyleSheet()
            styleSheet.Add(rule)
            drawingDocument.StyleSheets.Add(styleSheet)

            ' Create some UML shapes
            Dim shapeDevice As Nevron.Nov.Diagram.NShape = Me.CreateUmlShape("Device", New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo() {New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Public], "DeviceID", "integer(10)"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Protected], "DeviceCategory", "integer(10)"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Private], "name", "varchar(50)"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Private], "description", "blob"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Private], "detail", "blob")})
            activePage.Items.Add(shapeDevice)
            Dim shapeDeviceCategory As Nevron.Nov.Diagram.NShape = Me.CreateUmlShape("DeviceCategory", New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo() {New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Public], "CategoryID", "integer(10)"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Private], "description", "blob")})
            activePage.Items.Add(shapeDeviceCategory)
            Dim shapeSupportRequest As Nevron.Nov.Diagram.NShape = Me.CreateUmlShape("SupportRequest", New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo() {New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Protected], "Device", "integer(10)"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Public], "RequestID", "integer(10)"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Protected], "User", "integer(10)"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Private], "reportDate", "date"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Private], "description", "blob")})
            activePage.Items.Add(shapeSupportRequest)
            Dim shapeUser As Nevron.Nov.Diagram.NShape = Me.CreateUmlShape("User", New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo() {New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Public], "ID", "integer(10)"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Public], "firstName", "varchar(50)"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Protected], "lastName", "varchar(50)"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Private], "phone", "varchar(12)"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Private], "email", "varchar(50)"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Private], "address", "blob"), New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Private], "remarks", "blob")})
            activePage.Items.Add(shapeUser)

            ' Connect the shapes
            Me.Connect(Me.GetShapeByName("DeviceCategory"), "CategoryID", Me.GetShapeByName("Device"), "DeviceCategory")
            Me.Connect(Me.GetShapeByName("Device"), "DeviceID", Me.GetShapeByName("SupportRequest"), "Device")
            Me.Connect(Me.GetShapeByName("User"), "ID", Me.GetShapeByName("SupportRequest"), "User")

            ' Subscribe to the drawing view's Registered event to layout the shapes
            ' when the drawing view is registered in its owner document
            AddHandler Me.m_DrawingView.Registered, AddressOf Me.OnDrawingViewRegistered
        End Sub

        #EndRegion

        #Region"Implementation"

        ''' <summary>
        ''' Creates the rule for one to many connectors.
        ''' </summary>
        ''' <returns></returns>
        Private Function CreateConnectorOneToManyRule() As Nevron.Nov.Dom.NRule
            Dim rule As Nevron.Nov.Dom.NRule = New Nevron.Nov.Dom.NRule()
            Dim sb As Nevron.Nov.Dom.NSelectorBuilder = rule.GetSelectorBuilder()
            sb.Start()
            sb.Type(Nevron.Nov.Diagram.NGeometry.NGeometrySchema)
            sb.ChildOf()
            sb.UserClass(Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ConnectorOneToManyClassName)
            sb.[End]()
            rule.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Graphics.NStroke)(Nevron.Nov.Diagram.NGeometry.StrokeProperty, New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Black, Nevron.Nov.Graphics.ENDashStyle.Dash)))
            rule.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Diagram.NArrowhead)(Nevron.Nov.Diagram.NGeometry.BeginArrowheadProperty, New Nevron.Nov.Diagram.NArrowhead(Nevron.Nov.Diagram.ENArrowheadShape.VerticalLine, 8, 8, Nothing, New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Black))))
            rule.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Diagram.NArrowhead)(Nevron.Nov.Diagram.NGeometry.EndArrowheadProperty, New Nevron.Nov.Diagram.NArrowhead(Nevron.Nov.Diagram.ENArrowheadShape.InvertedLineArrowWithCircleNoFill, 8, 8, Nothing, New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Black))))
            Return rule
        End Function
        ''' <summary>
        ''' Creates a one to many connector from the member1 of shape1 to
        ''' member2 of shape2.
        ''' </summary>
        ''' <paramname="shape1"></param>
        ''' <paramname="member1"></param>
        ''' <paramname="shape2"></param>
        ''' <paramname="member2"></param>
        Private Sub Connect(ByVal shape1 As Nevron.Nov.Diagram.NShape, ByVal member1 As String, ByVal shape2 As Nevron.Nov.Diagram.NShape, ByVal member2 As String)
            Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            connector.UserClass = Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ConnectorOneToManyClassName
            Me.m_DrawingView.ActivePage.Items.Add(connector)

            ' Get or create the ports
            Dim port1 As Nevron.Nov.Diagram.NPort = Me.GetOrCreatePort(shape1, member1)
            Dim port2 As Nevron.Nov.Diagram.NPort = Me.GetOrCreatePort(shape2, member2)
            If port1 Is Nothing Then Throw New System.ArgumentException("A member with name '" & member1 & "' not found in shape '" & shape1.Name & "'", "member")
            If port1 Is Nothing Then Throw New System.ArgumentException("A member with name '" & member2 & "' not found in shape '" & shape2.Name & "'", "member")

            ' Connect the ports
            connector.GlueBeginToPort(port1)
            connector.GlueEndToPort(port2)
        End Sub
        ''' <summary>
        ''' Gets the port with the given name or creates one if a port with the given name
        ''' does not exist in the specified shape.
        ''' </summary>
        ''' <paramname="shape"></param>
        ''' <paramname="member"></param>
        ''' <returns></returns>
        Private Function GetOrCreatePort(ByVal shape As Nevron.Nov.Diagram.NShape, ByVal member As String) As Nevron.Nov.Diagram.NPort
            Dim port As Nevron.Nov.Diagram.NPort = shape.GetPortByName(member)
            If port IsNot Nothing Then Return port

            ' The port does not exist, so create it
            Dim label As Nevron.Nov.UI.NLabel = CType(shape.Widget.GetFirstDescendant(New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NLabelByTextFilter(member)), Nevron.Nov.UI.NLabel)
            If label Is Nothing Then Return Nothing
            Dim pairBox As Nevron.Nov.UI.NPairBox = CType(label.GetFirstAncestor(Nevron.Nov.UI.NPairBox.NPairBoxSchema), Nevron.Nov.UI.NPairBox)
            Dim stack As Nevron.Nov.UI.NStackPanel = CType(pairBox.ParentNode, Nevron.Nov.UI.NStackPanel)
            Dim yRelative As Double = (pairBox.GetAggregationInfo().Index + 0.5) / stack.Count
            port = New Nevron.Nov.Diagram.NPort(0.5, yRelative, True)
            port.SetDirection(Nevron.Nov.ENBoxDirection.Right)
            shape.Ports.Add(port)
            Return port
        End Function

        ''' <summary>
        ''' Creates an UML class diagram shape.
        ''' </summary>
        ''' <paramname="name"></param>
        ''' <paramname="memberInfos"></param>
        ''' <returns></returns>
        Private Function CreateUmlShape(ByVal name As String, ByVal memberInfos As Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo()) As Nevron.Nov.Diagram.NShape
            ' Create the shape
            Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.Name = name

            ' Set a rounded rectangle geometry
            Dim drawRectangle As Nevron.Nov.Diagram.NDrawRectangle = New Nevron.Nov.Diagram.NDrawRectangle(0, 0, 1, 1)
            drawRectangle.Relative = True
            shape.Geometry.Add(drawRectangle)
            shape.Geometry.CornerRounding = 15

            ' Create a stack panel
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Margins = New Nevron.Nov.Graphics.NMargins(5)
            stack.Direction = Nevron.Nov.Layout.ENHVDirection.TopToBottom
            
			' Create the title label
			Dim titleLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(name)
            titleLabel.TextAlignment = Nevron.Nov.ENContentAlignment.MiddleCenter
            titleLabel.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            titleLabel.BorderThickness = New Nevron.Nov.Graphics.NMargins(0, 0, 0, 1)
            stack.Add(titleLabel)
			
			' Create the member info pair boxes
			For i As Integer = 0 To memberInfos.Length - 1
                stack.Add(Me.CreatePairBox(memberInfos(i)))
            Next

            shape.Widget = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Me.BindSizeToDesiredSize(shape)
            Return shape
        End Function
        ''' <summary>
        ''' Creates a pair box from the given member information.
        ''' </summary>
        ''' <paramname="memberInfo"></param>
        ''' <returns></returns>
        Private Function CreatePairBox(ByVal memberInfo As Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NMemberInfo) As Nevron.Nov.UI.NPairBox
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(CChar(memberInfo.Visibility) & memberInfo.Name, memberInfo.Type, True)
            pairBox.Spacing = Nevron.Nov.NDesign.HorizontalSpacing * 2
            pairBox.FillMode = Nevron.Nov.Layout.ENStackFillMode.Equal
            pairBox.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

            Select Case memberInfo.Visibility
                Case Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Public]
                    ' Set bold font style
                    pairBox.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 9, Nevron.Nov.Graphics.ENFontStyle.Bold)
                Case Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Protected]
                    ' Set italic font style
                    pairBox.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 9, Nevron.Nov.Graphics.ENFontStyle.Italic)
                    ' Do not change the font style
                    Case Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility.[Private]
            End Select

            Return pairBox
        End Function
        ''' <summary>
        ''' Binds the size of the shape to the embedded widget desired size.
        ''' </summary>
        ''' <paramname="shape"></param>
        Private Sub BindSizeToDesiredSize(ByVal shape As Nevron.Nov.Diagram.NShape)
            Dim widget As Nevron.Nov.UI.NWidget = shape.Widget

            ' bind shape width to button desired width
            Dim bx As Nevron.Nov.Dom.NBindingFx = New Nevron.Nov.Dom.NBindingFx(widget, Nevron.Nov.UI.NButton.DesiredWidthProperty)
            bx.Guard = True
            shape.SetFx(Nevron.Nov.Diagram.NShape.WidthProperty, bx)

            ' bind shape height to button desired height
            Dim by As Nevron.Nov.Dom.NBindingFx = New Nevron.Nov.Dom.NBindingFx(widget, Nevron.Nov.UI.NButton.DesiredHeightProperty)
            by.Guard = True
            shape.SetFx(Nevron.Nov.Diagram.NShape.HeightProperty, by)
            shape.AllowResizeX = False
            shape.AllowRotate = False
            shape.AllowResizeY = False
        End Sub
        ''' <summary>
        ''' Gets the first shape with the given name in the drawing document's active page.
        ''' </summary>
        ''' <paramname="name"></param>
        ''' <returns></returns>
        Private Function GetShapeByName(ByVal name As String) As Nevron.Nov.Diagram.NShape
            Dim activePage As Nevron.Nov.Diagram.NPage = Me.m_DrawingView.ActivePage
            Return CType(activePage.GetFirstDescendant(New Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.NShapeByNameFilter(name)), Nevron.Nov.Diagram.NShape)
        End Function

        ''' <summary>
        ''' Arranges the shapes in the active page.
        ''' </summary>
        Private Sub ArrangeDiagram()
            ' Create and configure a layout
            Dim layout As Nevron.Nov.Diagram.Layout.NLayeredGraphLayout = New Nevron.Nov.Diagram.Layout.NLayeredGraphLayout()

            ' Get all top-level shapes that reside in the active page
            Dim activePage As Nevron.Nov.Diagram.NPage = Me.m_DrawingView.ActivePage
            Dim shapes As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Diagram.NShape) = activePage.GetShapes(False)

            ' Create a layout context and use it to arrange the shapes using the current layout
            Dim layoutContext As Nevron.Nov.Diagram.Layout.NDrawingLayoutContext = New Nevron.Nov.Diagram.Layout.NDrawingLayoutContext(Me.m_DrawingView.Document, activePage)
            layout.Arrange(shapes.CastAll(Of Object)(), layoutContext)

            ' Size the page to the content size
            activePage.SizeToContent()
        End Sub

        #EndRegion

        #Region"Event Handlers"

        ''' <summary>
        ''' Called when the drawing view is registered to its owner document.
        ''' </summary>
        ''' <paramname="arg"></param>
        Private Sub OnDrawingViewRegistered(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            ' Evaluate the drawing document
            Me.m_DrawingView.Document.Evaluate()

            ' Layout the shapes
            Me.ArrangeDiagram()
        End Sub

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NUmlClassDiagramExample.
        ''' </summary>
        Public Shared ReadOnly NUmlClassDiagramExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Constants"

        Private Const ConnectorOneToManyClassName As String = "ConnectorOneToMany"

        #EndRegion

        #Region"Nested Types"

        Private Enum ENMemberVisibility
            [Public] = Microsoft.VisualBasic.AscW("+"c)
            [Protected] = Microsoft.VisualBasic.AscW("#"c)
            [Private] = Microsoft.VisualBasic.AscW("-"c)
        End Enum

        Private Structure NMemberInfo
            Public Sub New(ByVal visibility As Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility, ByVal name As String, ByVal type As String)
                Me.Visibility = visibility
                Me.Name = name
                Me.Type = type
            End Sub

            Public Visibility As Nevron.Nov.Examples.Diagram.NUmlClassDiagramExample.ENMemberVisibility
            Public Name As String
            Public Type As String
        End Structure

        Private Class NShapeByNameFilter
            Implements Nevron.Nov.DataStructures.INFilter(Of Nevron.Nov.Dom.NNode)

            Public Sub New(ByVal name As String)
                Me.Name = name
            End Sub

            Public Function Filter(ByVal item As Nevron.Nov.Dom.NNode) As Boolean Implements Global.Nevron.Nov.DataStructures.INFilter(Of Global.Nevron.Nov.Dom.NNode).Filter
                Dim shape As Nevron.Nov.Diagram.NShape = TryCast(item, Nevron.Nov.Diagram.NShape)
                Return shape IsNot Nothing AndAlso Equals(shape.Name, Me.Name)
            End Function

            Public Name As String
        End Class

        Private Class NLabelByTextFilter
            Implements Nevron.Nov.DataStructures.INFilter(Of Nevron.Nov.Dom.NNode)

            Public Sub New(ByVal text As String)
                Me.Text = text
            End Sub

            Public Function Filter(ByVal item As Nevron.Nov.Dom.NNode) As Boolean Implements Global.Nevron.Nov.DataStructures.INFilter(Of Global.Nevron.Nov.Dom.NNode).Filter
                Dim label As Nevron.Nov.UI.NLabel = TryCast(item, Nevron.Nov.UI.NLabel)
                Return label IsNot Nothing AndAlso Equals(label.Text.Substring(1), Me.Text)
            End Function

            Public Text As String
        End Class

        #EndRegion
    End Class
End Namespace
