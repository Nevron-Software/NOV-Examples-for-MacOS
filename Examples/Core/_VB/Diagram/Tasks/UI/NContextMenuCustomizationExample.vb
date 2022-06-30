Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.DrawingCommands
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NContextMenuCustomizationExample
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
            Nevron.Nov.Examples.Diagram.NContextMenuCustomizationExample.NContextMenuCustomizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NContextMenuCustomizationExample), NExampleBaseSchema)
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

			' Add the custom command action to the drawing view's commander
			Me.m_DrawingView.Commander.Add(New Nevron.Nov.Examples.Diagram.NContextMenuCustomizationExample.CustomCommandAction())

			' Change the context menu factory to the custom one
			Me.m_DrawingView.ContextMenu = New Nevron.Nov.Examples.Diagram.NContextMenuCustomizationExample.CustomContextMenu()
            Dim ribbonBuilder As Nevron.Nov.Diagram.NDiagramRibbonBuilder = New Nevron.Nov.Diagram.NDiagramRibbonBuilder()
            Return ribbonBuilder.CreateUI(Me.m_DrawingView)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to customize the NOV drawing view's context menu. A custom command is added
at the end of the context menu.</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim factory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            Dim shape As Nevron.Nov.Diagram.NShape = factory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            shape.SetBounds(100, 100, 150, 100)
            drawingDocument.Content.ActivePage.Items.Add(shape)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NContextMenuCustomizationExample.
		''' </summary>
		Public Shared ReadOnly NContextMenuCustomizationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Public Shared ReadOnly CustomCommand As Nevron.Nov.UI.NCommand = Nevron.Nov.UI.NCommand.Create(GetType(Nevron.Nov.Examples.Diagram.NContextMenuCustomizationExample), "CustomCommand", "Custom Command")

		#EndRegion

		#Region"Nested Types"

		Public Class CustomContextMenu
            Inherits Nevron.Nov.Diagram.NDrawingContextMenu
			''' <summary>
			''' Default constructor.
			''' </summary>
			Public Sub New()
            End Sub
			''' <summary>
			''' Static constructor.
			''' </summary>
			Shared Sub New()
                Nevron.Nov.Examples.Diagram.NContextMenuCustomizationExample.CustomContextMenu.CustomContextMenuSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NContextMenuCustomizationExample.CustomContextMenu), Nevron.Nov.Diagram.NDrawingContextMenu.NDrawingContextMenuSchema)
            End Sub

            Protected Overrides Sub CreateCustomCommands(ByVal menu As Nevron.Nov.UI.NMenu, ByVal builder As Nevron.Nov.UI.NContextMenuBuilder)
                MyBase.CreateCustomCommands(menu, builder)

				' Add a custom command
				builder.AddMenuItem(menu, Nevron.Nov.Diagram.NResources.Image_Ribbon_16x16_smiley_png, Nevron.Nov.Examples.Diagram.NContextMenuCustomizationExample.CustomCommand)
            End Sub

			''' <summary>
			''' Schema associated with CustomContextMenu.
			''' </summary>
			Public Shared ReadOnly CustomContextMenuSchema As Nevron.Nov.Dom.NSchema
        End Class

        Public Class CustomCommandAction
            Inherits Nevron.Nov.Diagram.DrawingCommands.NDrawingCommandAction
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
                Nevron.Nov.Examples.Diagram.NContextMenuCustomizationExample.CustomCommandAction.CustomCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NContextMenuCustomizationExample.CustomCommandAction), Nevron.Nov.Diagram.DrawingCommands.NDrawingCommandAction.NDrawingCommandActionSchema)
            End Sub

			#EndRegion

			#Region"Public Overrides"

			''' <summary>
			''' Gets the command associated with this command action.
			''' </summary>
			''' <returns></returns>
			Public Overrides Function GetCommand() As Nevron.Nov.UI.NCommand
                Return Nevron.Nov.Examples.Diagram.NContextMenuCustomizationExample.CustomCommand
            End Function
			''' <summary>
			''' Executes the command action.
			''' </summary>
			''' <paramname="target"></param>
			''' <paramname="parameter"></param>
			Public Overrides Sub Execute(ByVal target As Nevron.Nov.Dom.NNode, ByVal parameter As Object)
                Dim drawingView As Nevron.Nov.Diagram.NDrawingView = MyBase.GetDrawingView(target)
                Call Nevron.Nov.UI.NMessageBox.Show("Drawing Custom Command executed!", "Custom Command", Nevron.Nov.UI.ENMessageBoxButtons.OK, Nevron.Nov.UI.ENMessageBoxIcon.Information)
            End Sub

			#EndRegion

			#Region"Schema"

			''' <summary>
			''' Schema associated with CustomCommandAction.
			''' </summary>
			Public Shared ReadOnly CustomCommandActionSchema As Nevron.Nov.Dom.NSchema

			#EndRegion
		End Class

		#EndRegion
	End Class
End Namespace
