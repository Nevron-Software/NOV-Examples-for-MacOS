Imports Nevron.Nov.Dom
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.DrawingCommands
Imports Nevron.Nov.UI
Imports Nevron.Nov.Diagram.Shapes

Namespace Nevron.Nov.Examples.Diagram
    Public Class NCommandBarsCustomizationExample
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
            Nevron.Nov.Examples.Diagram.NCommandBarsCustomizationExample.NCommandBarsCustomizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NCommandBarsCustomizationExample), NExampleBaseSchema)
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

			' Create and customize a command bar UI builder
			Me.m_CommandBarBuilder = New Nevron.Nov.Diagram.NDiagramCommandBarBuilder()

			' Add the custom command action to the drawing view's commander
			Me.m_DrawingView.Commander.Add(New Nevron.Nov.Examples.Diagram.NCommandBarsCustomizationExample.CustomCommandAction())

			' Remove the "Edit" menu and insert a custom one
			Me.m_CommandBarBuilder = New Nevron.Nov.Diagram.NDiagramCommandBarBuilder()
            Me.m_CommandBarBuilder.MenuDropDownBuilders.Remove(Nevron.Nov.Diagram.NDiagramCommandBarBuilder.MenuEditName)
            Me.m_CommandBarBuilder.MenuDropDownBuilders.Insert(1, New Nevron.Nov.Examples.Diagram.NCommandBarsCustomizationExample.CustomMenuBuilder())

			' Remove the "Standard" toolbar and insert a custom one
			Me.m_CommandBarBuilder.ToolBarBuilders.Remove(Nevron.Nov.Diagram.NDiagramCommandBarBuilder.ToolbarStandardName)
            Me.m_CommandBarBuilder.ToolBarBuilders.Insert(0, New Nevron.Nov.Examples.Diagram.NCommandBarsCustomizationExample.CustomToolBarBuilder())
            Return Me.m_CommandBarBuilder.CreateUI(Me.m_DrawingView)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to customize the NOV diagram command bars (menus and toolbars).</p>
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
        Private m_CommandBarBuilder As Nevron.Nov.Diagram.NDiagramCommandBarBuilder

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCommandBarsCustomizationExample.
		''' </summary>
		Public Shared ReadOnly NCommandBarsCustomizationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Public Shared ReadOnly CustomCommand As Nevron.Nov.UI.NCommand = Nevron.Nov.UI.NCommand.Create(GetType(Nevron.Nov.Examples.Diagram.NCommandBarsCustomizationExample), "CustomCommand", "Custom Command")

		#EndRegion

		#Region"Nested Types"

		Public Class CustomMenuBuilder
            Inherits Nevron.Nov.UI.NMenuDropDownBuilder

            Public Sub New()
                MyBase.New("Custom Menu")
            End Sub

            Protected Overrides Sub AddItems(ByVal items As Nevron.Nov.UI.NMenuItemCollection)
				' Add the "Copy" menu item
				items.Add(MyBase.CreateMenuItem(Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, Nevron.Nov.Diagram.NDrawingView.CopyCommand))

				' Add the custom command menu item
				items.Add(CreateMenuItem(Nevron.Nov.Diagram.NResources.Image_Ribbon_16x16_smiley_png, Nevron.Nov.Examples.Diagram.NCommandBarsCustomizationExample.CustomCommand))
            End Sub
        End Class

        Public Class CustomToolBarBuilder
            Inherits Nevron.Nov.UI.NToolBarBuilder

            Public Sub New()
                MyBase.New("Custom Toolbar")
            End Sub

            Protected Overrides Sub AddItems(ByVal items As Nevron.Nov.UI.NCommandBarItemCollection)
				' Add the "Copy" button
				items.Add(MyBase.CreateButton(Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, Nevron.Nov.Diagram.NDrawingView.CopyCommand))

				' Add the custom command button
				items.Add(CreateButton(Nevron.Nov.Diagram.NResources.Image_Ribbon_16x16_smiley_png, Nevron.Nov.Examples.Diagram.NCommandBarsCustomizationExample.CustomCommand))
            End Sub
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
                Nevron.Nov.Examples.Diagram.NCommandBarsCustomizationExample.CustomCommandAction.CustomCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NCommandBarsCustomizationExample.CustomCommandAction), Nevron.Nov.Diagram.DrawingCommands.NDrawingCommandAction.NDrawingCommandActionSchema)
            End Sub

			#EndRegion

			#Region"Public Overrides"

			''' <summary>
			''' Gets the command associated with this command action.
			''' </summary>
			''' <returns></returns>
			Public Overrides Function GetCommand() As Nevron.Nov.UI.NCommand
                Return Nevron.Nov.Examples.Diagram.NCommandBarsCustomizationExample.CustomCommand
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
