Imports Nevron.Nov.Barcode
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Barcode
    Public Class NUpcSup5BarcodeExample
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
            Nevron.Nov.Examples.Barcode.NUpcSup5BarcodeExample.NUpcSup5BarcodeExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Barcode.NUpcSup5BarcodeExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a linear barcode widget
			Me.m_Barcode = New Nevron.Nov.Barcode.NLinearBarcode()
            Me.m_Barcode.Symbology = Nevron.Nov.Barcode.ENLinearBarcodeSymbology.UPC_Supplemental_5Digit
            Me.m_Barcode.Text = "12345"
            Me.m_Barcode.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Me.m_Barcode.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Return Me.m_Barcode
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

			' Create the property editors
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Barcode), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_Barcode, Nevron.Nov.Barcode.NLinearBarcode.HorizontalPlacementProperty, Nevron.Nov.Barcode.NLinearBarcode.VerticalPlacementProperty, Nevron.Nov.Barcode.NLinearBarcode.BackgroundFillProperty, Nevron.Nov.Barcode.NLinearBarcode.TextFillProperty, Nevron.Nov.Barcode.NLinearBarcode.SizeModeProperty, Nevron.Nov.Barcode.NLinearBarcode.ScaleProperty)
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
	This example demonstrates how to create a 5 digit supplement of an UPC barcode. Use the controls on
	the right to change the appearance of the barcode widget.
</p>
"
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Barcode As Nevron.Nov.Barcode.NLinearBarcode

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NUpcSup5BarcodeExample.
		''' </summary>
		Public Shared ReadOnly NUpcSup5BarcodeExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
