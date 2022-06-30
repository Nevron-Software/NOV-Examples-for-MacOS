Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NDropDownButtonExample
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
            Nevron.Nov.Examples.UI.NDropDownButtonExample.NDropDownButtonExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NDropDownButtonExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Protected Overrides - Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_DropDownButton = New Nevron.Nov.UI.NDropDownButton(Nevron.Nov.UI.NPairBox.Create(NResources.Image__16x16_Calendar_png, "Event"))
            Me.m_DropDownButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_DropDownButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_DropDownButton.Margins = New Nevron.Nov.Graphics.NMargins(0, Nevron.Nov.NDesign.VerticalSpacing, 0, 0)

			' Create the drop down button Popup content
			Dim popupStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            popupStack.Padding = New Nevron.Nov.Graphics.NMargins(Nevron.Nov.NDesign.HorizontalSpacing, Nevron.Nov.NDesign.VerticalSpacing)
            popupStack.Add(New Nevron.Nov.UI.NLabel("Event Date:"))
            popupStack.Add(New Nevron.Nov.UI.NCalendar())
            popupStack.Add(New Nevron.Nov.UI.NLabel("Event Color:"))
            popupStack.Add(New Nevron.Nov.UI.NPaletteColorPicker())
            Dim buttonStrip As Nevron.Nov.UI.NButtonStrip = New Nevron.Nov.UI.NButtonStrip()
            buttonStrip.AddOKCancelButtons()
            popupStack.Add(buttonStrip)
            Me.m_DropDownButton.Popup.Content = popupStack
            Return Me.m_DropDownButton
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_DropDownButton), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_DropDownButton, Nevron.Nov.UI.NWidget.EnabledProperty, Nevron.Nov.UI.NStylePropertyEx.ExtendedLookPropertyEx)

			' Change the text of the extended look property editor
			Dim label As Nevron.Nov.UI.NLabel = CType(editors(CInt((0))).GetFirstDescendant(New Nevron.Nov.Dom.NInstanceOfSchemaFilter(Nevron.Nov.UI.NLabel.NLabelSchema)), Nevron.Nov.UI.NLabel)
            label.Text = "Extended Look:"
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a drop down button and set the content of its popup.</p>"
        End Function

		#EndRegion

		#Region"Fields"

		Private m_DropDownButton As Nevron.Nov.UI.NDropDownButton

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NDropDownButtonExample.
		''' </summary>
		Public Shared ReadOnly NDropDownButtonExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
