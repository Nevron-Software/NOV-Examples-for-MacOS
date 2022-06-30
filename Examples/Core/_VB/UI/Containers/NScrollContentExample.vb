Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NScrollContentExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor.
		''' </summary>
		Public Sub New()
            Me.m_ScrollContent = Nothing
        End Sub
		''' <summary>
		''' Static constructor.
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.UI.NScrollContentExample.NScrollContentExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NScrollContentExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ScrollContent = New Nevron.Nov.UI.NScrollContent()
            Me.m_ScrollContent.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_ScrollContent.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_ScrollContent.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
            Me.m_ScrollContent.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            Me.m_ScrollContent.PreferredSize = New Nevron.Nov.Graphics.NSize(300, 250)

			' Create a table with some buttons
			Dim table As Nevron.Nov.UI.NTableFlowPanel = New Nevron.Nov.UI.NTableFlowPanel()
            table.MaxOrdinal = 10

            For i As Integer = 1 To 150
                table.Add(New Nevron.Nov.UI.NButton("Button " & i.ToString()))
            Next

            Me.m_ScrollContent.Content = table
            Return Me.m_ScrollContent
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ScrollContent), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_ScrollContent, Nevron.Nov.UI.NScrollContent.EnabledProperty, Nevron.Nov.UI.NScrollContent.HorizontalPlacementProperty, Nevron.Nov.UI.NScrollContent.VerticalPlacementProperty)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
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
	This example demonstrates how to use the scroll content widget. The scroll content is a widget,
	which contains a single other widget, and allows for its scrolling. It measures to fit the
	contained element without scrollbars, but if this is not possible, the scroll content element
	will display scrollbars. The contained element is always sized to its desired size.
</p>
"
        End Function

		#EndRegion

		#Region"Fields"

		Private m_ScrollContent As Nevron.Nov.UI.NScrollContent

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NScrollContentExample.
		''' </summary>
		Public Shared ReadOnly NScrollContentExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
