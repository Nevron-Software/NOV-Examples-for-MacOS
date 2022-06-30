Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.DrawingCommands
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Diagram.UI
Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NRibbonCustomizationExample
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
            Nevron.Nov.Examples.Diagram.NRibbonCustomizationExample.NRibbonCustomizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NRibbonCustomizationExample), NExampleBaseSchema)
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

			' Create and customize a ribbon UI builder
			Me.m_RibbonBuilder = New Nevron.Nov.Diagram.NDiagramRibbonBuilder()

			' Add the custom command action to the drawing view's commander
			Me.m_DrawingView.Commander.Add(New Nevron.Nov.Examples.Diagram.NRibbonCustomizationExample.CustomCommandAction())

			' Rename the "Home" ribbon tab page
			Dim homeTabBuilder As Nevron.Nov.UI.NRibbonTabPageBuilder = Me.m_RibbonBuilder.TabPageBuilders(Nevron.Nov.Diagram.NDiagramRibbonBuilder.TabPageHomeName)
            homeTabBuilder.Name = "Start"

			' Rename the "Text" ribbon group of the "Home" tab page
			Dim fontGroupBuilder As Nevron.Nov.UI.NRibbonGroupBuilder = homeTabBuilder.RibbonGroupBuilders(Nevron.Nov.Diagram.UI.NHomeTabPageBuilder.GroupTextName)
            fontGroupBuilder.Name = "Custom Name"

			' Remove the "Clipboard" ribbon group from the "Home" tab page
			homeTabBuilder.RibbonGroupBuilders.Remove(Nevron.Nov.Diagram.UI.NHomeTabPageBuilder.GroupClipboardName)

			' Insert the custom ribbon group at the beginning of the home tab page
			homeTabBuilder.RibbonGroupBuilders.Insert(0, New Nevron.Nov.Examples.Diagram.NRibbonCustomizationExample.CustomRibbonGroupBuilder())
            Return Me.m_RibbonBuilder.CreateUI(Me.m_DrawingView)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to customize the NOV diagram ribbon.</p>"
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
        Private m_RibbonBuilder As Nevron.Nov.Diagram.NDiagramRibbonBuilder

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRibbonCustomizationExample.
		''' </summary>
		Public Shared ReadOnly NRibbonCustomizationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Public Shared ReadOnly CustomCommand As Nevron.Nov.UI.NCommand = Nevron.Nov.UI.NCommand.Create(GetType(Nevron.Nov.Examples.Diagram.NRibbonCustomizationExample), "CustomCommand", "Custom Command")

		#EndRegion

		#Region"Nested Types"

		Public Class CustomRibbonGroupBuilder
            Inherits Nevron.Nov.UI.NRibbonGroupBuilder
			#Region"Constructors"

			Public Sub New()
                MyBase.New("Custom Group", Nevron.Nov.Diagram.NResources.Image_Ribbon_16x16_smiley_png)
            End Sub

			#EndRegion

			#Region"Protected Overrides"

			Protected Overrides Sub AddRibbonGroupItems(ByVal items As Nevron.Nov.UI.NRibbonGroupItemCollection)
				' Add the "Copy" command
				items.Add(CreateRibbonButton(Nevron.Nov.Diagram.NResources.Image_Ribbon_32x32_clipboard_copy_png, Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, Nevron.Nov.Diagram.NDrawingView.CopyCommand))

				' Add the custom command
				items.Add(CreateRibbonButton(Nevron.Nov.Diagram.NResources.Image_Ribbon_32x32_smiley_png, Nevron.Nov.Diagram.NResources.Image_Ribbon_16x16_smiley_png, Nevron.Nov.Examples.Diagram.NRibbonCustomizationExample.CustomCommand))
            End Sub

			#EndRegion
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
                Nevron.Nov.Examples.Diagram.NRibbonCustomizationExample.CustomCommandAction.CustomCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NRibbonCustomizationExample.CustomCommandAction), Nevron.Nov.Diagram.DrawingCommands.NDrawingCommandAction.NDrawingCommandActionSchema)
            End Sub

			#EndRegion

			#Region"Public Overrides"

			''' <summary>
			''' Gets the command associated with this command action.
			''' </summary>
			''' <returns></returns>
			Public Overrides Function GetCommand() As Nevron.Nov.UI.NCommand
                Return Nevron.Nov.Examples.Diagram.NRibbonCustomizationExample.CustomCommand
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
