Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NRibbonAndCommandBarsExample
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
            Nevron.Nov.Examples.Diagram.NRibbonAndCommandBarsExample.NRibbonAndCommandBarsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NRibbonAndCommandBarsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a simple drawing
			Me.m_DrawingView = New Nevron.Nov.Diagram.NDrawingView()
            Me.m_DrawingView.Document.HistoryService.Pause()

            Try
                Me.InitDiagram(Me.m_DrawingView.Document)
            Finally
                Me.m_DrawingView.Document.HistoryService.[Resume]()
            End Try

			' Create and execute a ribbon UI builder
			Me.m_RibbonBuilder = New Nevron.Nov.Diagram.NDiagramRibbonBuilder()
            Return Me.m_RibbonBuilder.CreateUI(Me.m_DrawingView)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
			' Switch UI button
			Dim switchUIButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton(Nevron.Nov.Examples.Diagram.NRibbonAndCommandBarsExample.SwitchToCommandBars)
            switchUIButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler switchUIButton.Click, AddressOf Me.OnSwitchUIButtonClick
            Return switchUIButton
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to switch the NOV Diagram commanding interface between ribbon and command bars.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim factory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            shape.SetBounds(100, 100, 150, 100)
            drawingDocument.Content.ActivePage.Items.Add(shape)
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub SetUI(ByVal oldUiHolder As Nevron.Nov.UI.NCommandUIHolder, ByVal widget As Nevron.Nov.UI.NWidget)
            If TypeOf oldUiHolder.ParentNode Is Nevron.Nov.UI.NTabPage Then
                CType(oldUiHolder.ParentNode, Nevron.Nov.UI.NTabPage).Content = widget
            ElseIf TypeOf oldUiHolder.ParentNode Is Nevron.Nov.UI.NPairBox Then
                CType(oldUiHolder.ParentNode, Nevron.Nov.UI.NPairBox).Box1 = widget
            End If
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSwitchUIButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim switchUIButton As Nevron.Nov.UI.NButton = CType(arg.TargetNode, Nevron.Nov.UI.NButton)
            Dim label As Nevron.Nov.UI.NLabel = CType(switchUIButton.Content, Nevron.Nov.UI.NLabel)

			' Remove the drawing view from its parent
			Dim uiHolder As Nevron.Nov.UI.NCommandUIHolder = Me.m_DrawingView.GetFirstAncestor(Of Nevron.Nov.UI.NCommandUIHolder)()
            Me.m_DrawingView.ParentNode.RemoveChild(Me.m_DrawingView)

            If Equals(label.Text, Nevron.Nov.Examples.Diagram.NRibbonAndCommandBarsExample.SwitchToRibbon) Then
				' We are in "Command Bars" mode, so switch to "Ribbon"
				label.Text = Nevron.Nov.Examples.Diagram.NRibbonAndCommandBarsExample.SwitchToCommandBars

				' Create the ribbon
				Me.SetUI(uiHolder, Me.m_RibbonBuilder.CreateUI(Me.m_DrawingView))
            Else
				' We are in "Ribbon" mode, so switch to "Command Bars"
				label.Text = Nevron.Nov.Examples.Diagram.NRibbonAndCommandBarsExample.SwitchToRibbon

				' Create the command bars
				If Me.m_CommandBarBuilder Is Nothing Then
                    Me.m_CommandBarBuilder = New Nevron.Nov.Diagram.NDiagramCommandBarBuilder()
                End If

                Me.SetUI(uiHolder, Me.m_CommandBarBuilder.CreateUI(Me.m_DrawingView))
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_RibbonBuilder As Nevron.Nov.Diagram.NDiagramRibbonBuilder
        Private m_CommandBarBuilder As Nevron.Nov.Diagram.NDiagramCommandBarBuilder

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRibbonAndCommandBarsSwitchingExample.
		''' </summary>
		Public Shared ReadOnly NRibbonAndCommandBarsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Const SwitchToCommandBars As String = "Switch to Command Bars"
        Private Const SwitchToRibbon As String = "Switch to Ribbon"

		#EndRegion
	End Class
End Namespace
