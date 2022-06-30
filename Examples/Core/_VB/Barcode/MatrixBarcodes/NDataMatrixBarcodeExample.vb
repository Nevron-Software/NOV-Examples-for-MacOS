Imports System
Imports Nevron.Nov.Barcode
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Barcode
    Public Class NDataMatrixBarcodeExample
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
            Nevron.Nov.Examples.Barcode.NDataMatrixBarcodeExample.NDataMatrixBarcodeExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Barcode.NDataMatrixBarcodeExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_Barcode = New Nevron.Nov.Barcode.NMatrixBarcode()
            Me.m_Barcode.Symbology = Nevron.Nov.Barcode.ENMatrixBarcodeSymbology.DataMatrix
            Me.m_Barcode.Text = "Nevron Software" & Global.Microsoft.VisualBasic.Constants.vbCrLf & Global.Microsoft.VisualBasic.Constants.vbCrLf & "https://www.nevron.com"
            Me.m_Barcode.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
            Me.m_Barcode.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            Return Me.m_Barcode
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

			' Create the property editors
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Barcode), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_Barcode, Nevron.Nov.Barcode.NMatrixBarcode.HorizontalPlacementProperty, Nevron.Nov.Barcode.NMatrixBarcode.VerticalPlacementProperty, Nevron.Nov.Barcode.NMatrixBarcode.BackgroundFillProperty, Nevron.Nov.Barcode.NMatrixBarcode.TextFillProperty, Nevron.Nov.Barcode.NMatrixBarcode.SizeModeProperty, Nevron.Nov.Barcode.NMatrixBarcode.ScaleProperty, Nevron.Nov.Barcode.NMatrixBarcode.TextProperty)
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
	This example demonstrates how to create Data Matrix 2D barcodes. Use the controls on the right to change
	the appearance of the barcode widget.
</p>
"
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Barcode As Nevron.Nov.Barcode.NMatrixBarcode

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NDataMatrixBarcodeExample.
		''' </summary>
		Public Shared ReadOnly NDataMatrixBarcodeExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
