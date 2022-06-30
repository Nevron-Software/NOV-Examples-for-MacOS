Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NSplitterExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NSplitterExample.NSplitterExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NSplitterExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a splitter
			Me.m_Splitter = New Nevron.Nov.UI.NSplitter()
            Me.m_Splitter.Pane1.Content = New Nevron.Nov.UI.NLabel("Pane 1")
            Me.m_Splitter.Pane1.Padding = New Nevron.Nov.Graphics.NMargins(Nevron.Nov.NDesign.HorizontalSpacing, Nevron.Nov.NDesign.VerticalSpacing)
            Me.m_Splitter.Pane1.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightGreen)
            Me.m_Splitter.Pane1.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            Me.m_Splitter.Pane1.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            Me.m_Splitter.Pane2.Content = New Nevron.Nov.UI.NLabel("Pane 2")
            Me.m_Splitter.Pane2.Padding = New Nevron.Nov.Graphics.NMargins(Nevron.Nov.NDesign.HorizontalSpacing, Nevron.Nov.NDesign.VerticalSpacing)
            Me.m_Splitter.Pane2.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightBlue)
            Me.m_Splitter.Pane2.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            Me.m_Splitter.Pane2.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            Me.m_Splitter.ResizeStep = 20

			' Host it
			Return Me.m_Splitter
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

			' Create some splitter property editors
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Splitter), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_Splitter, Nevron.Nov.UI.NSplitterBase.OrientationProperty, Nevron.Nov.UI.NSplitterBase.ResizeWhileDraggingProperty, Nevron.Nov.UI.NSplitterBase.ResizeStepProperty)
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To editors.Count - 1
                propertyStack.Add(editors(i))
            Next

            stack.Add(New Nevron.Nov.UI.NGroupBox("Splitter Properties", propertyStack))

			' Create splitter thumb property editors
			editors = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Splitter.Thumb), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_Splitter.Thumb, Nevron.Nov.UI.NSplitterThumb.SplitModeProperty, Nevron.Nov.UI.NSplitterThumb.SplitOffsetProperty, Nevron.Nov.UI.NSplitterThumb.SplitFactorProperty, Nevron.Nov.UI.NSplitterThumb.CollapseModeProperty)
            propertyStack = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To editors.Count - 1
                propertyStack.Add(editors(i))
            Next

            stack.Add(New Nevron.Nov.UI.NGroupBox("Splitter Thumb Properties", propertyStack))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and configure a splitter. The splitter is a widget that splits
	its content area into two resizable panes, which can be interactively resized with help of a thumb located
	in the middle. Using the <b>Orientation</b> property you can specify whether the splitter is horizontal or
	vertical. To control how the splitter splits its content area you can use the <b>SplitMode</b> property.
</p>
"
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Splitter As Nevron.Nov.UI.NSplitter

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NSplitterExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
