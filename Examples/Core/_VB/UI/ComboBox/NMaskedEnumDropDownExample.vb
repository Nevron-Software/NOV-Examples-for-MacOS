Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NMaskedEnumDropDownExample
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
            Nevron.Nov.Examples.UI.NMaskedEnumDropDownExample.NMaskedEnumDropDownExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NMaskedEnumDropDownExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Protected Overrides"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim dropDown As Nevron.Nov.Editors.NMaskedEnumDropDown = New Nevron.Nov.Editors.NMaskedEnumDropDown()
            dropDown.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            dropDown.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            dropDown.ColumnCount = 2
            dropDown.EnumType = Nevron.Nov.Dom.NDomType.FromType(GetType(Nevron.Nov.Text.ENTableStyleOptions))
            dropDown.Initialize()
            dropDown.EnumValue = Nevron.Nov.Text.ENTableStyleOptions.FirstRow
            Return dropDown
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to used the masked (flag) enum drop down widget to select enum flags.
</p>
"
        End Function

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NMaskedEnumDropDownExample.
		''' </summary>
		Public Shared ReadOnly NMaskedEnumDropDownExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
