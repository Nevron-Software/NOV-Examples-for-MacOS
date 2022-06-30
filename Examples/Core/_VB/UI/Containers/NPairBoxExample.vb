Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NPairBoxExample
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
            Nevron.Nov.Examples.UI.NPairBoxExample.NPairBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NPairBoxExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_PairBox = New Nevron.Nov.UI.NPairBox(Me.CreateBoxContent("Box 1", Nevron.Nov.Graphics.NColor.Blue), Me.CreateBoxContent("Box 2", Nevron.Nov.Graphics.NColor.Red))
            Me.m_PairBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_PairBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top

			' The Spacing property is automatically set from the UI theme to NDesign.HorizontalSpacing,
			' so you don't need to set it. It is set here only for the purposes of the example.
			Me.m_PairBox.Spacing = Nevron.Nov.NDesign.HorizontalSpacing
            Return Me.m_PairBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_PairBox), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_PairBox, Nevron.Nov.UI.NPairBox.EnabledProperty, Nevron.Nov.UI.NPairBox.HorizontalPlacementProperty, Nevron.Nov.UI.NPairBox.VerticalPlacementProperty, Nevron.Nov.UI.NPairBox.BoxesRelationProperty, Nevron.Nov.UI.NPairBox.FitModeProperty, Nevron.Nov.UI.NPairBox.FillModeProperty, Nevron.Nov.UI.NPairBox.SpacingProperty)
            Dim i As Integer = 0, count As Integer = editors.Count

            While i < count
                stack.Add(editors(i))
                i += 1
            End While

            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a pair box. The pair box is a widget, which consists of 2 other widgets - <b>Box1</b> and <b>Box2</b>.
	You can change the relative alignment, the spacing and the size mode of this widgets using the controls to the right.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateBoxContent(ByVal text As String, ByVal borderColor As Nevron.Nov.Graphics.NColor) As Nevron.Nov.UI.NWidget
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(text)
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Dim contentElement As Nevron.Nov.UI.NContentHolder = New Nevron.Nov.UI.NContentHolder(label)
            contentElement.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(borderColor)
            contentElement.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            contentElement.Padding = New Nevron.Nov.Graphics.NMargins(2)
            Return contentElement
        End Function

		#EndRegion

		#Region"Fields"

		Private m_PairBox As Nevron.Nov.UI.NPairBox

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NPairBoxExample.
		''' </summary>
		Public Shared ReadOnly NPairBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
