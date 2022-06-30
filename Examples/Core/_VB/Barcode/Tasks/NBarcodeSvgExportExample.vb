Imports System
Imports System.IO
Imports Nevron.Nov.Barcode
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Formats.Svg
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Barcode
    Public Class NBarcodeSvgExportExample
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
            Nevron.Nov.Examples.Barcode.NBarcodeSvgExportExample.NBarcodeSvgExportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Barcode.NBarcodeSvgExportExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_Barcode = New Nevron.Nov.Barcode.NMatrixBarcode(Nevron.Nov.Barcode.ENMatrixBarcodeSymbology.QrCode, "Nevron Software" & System.Environment.NewLine & System.Environment.NewLine & "https://www.nevron.com")
            Me.m_Barcode.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Me.m_Barcode.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Return Me.m_Barcode
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

			' Create the property editors
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Barcode), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_Barcode, Nevron.Nov.Barcode.NMatrixBarcode.BackgroundFillProperty, Nevron.Nov.Barcode.NMatrixBarcode.TextFillProperty, Nevron.Nov.Barcode.NMatrixBarcode.ErrorCorrectionProperty, Nevron.Nov.Barcode.NMatrixBarcode.SizeModeProperty, Nevron.Nov.Barcode.NMatrixBarcode.ScaleProperty, Nevron.Nov.Barcode.NMatrixBarcode.TextProperty)
            Dim i As Integer = 0, count As Integer = editors.Count

            While i < count
                stack.Add(editors(i))
                i += 1
            End While

            Dim exportToSvgButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Export to SVG...")
            AddHandler exportToSvgButton.Click, AddressOf Me.OnExportToSvgButtonClick
            stack.Add(exportToSvgButton)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to export barcodes to SVG. Enter some text in the text box on the right
	and click the <b>Export to SVG...</b> button.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnExportToSvgButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim exportToSvgDialog As Nevron.Nov.UI.NSaveFileDialog = New Nevron.Nov.UI.NSaveFileDialog()
            exportToSvgDialog.FileTypes = New Nevron.Nov.UI.NFileDialogFileType() {New Nevron.Nov.UI.NFileDialogFileType("SVG Files", "svg")}
            AddHandler exportToSvgDialog.Closed, AddressOf Me.OnExportToSvgDialogClosed
            exportToSvgDialog.RequestShow()
        End Sub

        Private Sub OnExportToSvgDialogClosed(ByVal arg As Nevron.Nov.UI.NSaveFileDialogResult)
            If arg.Result <> Nevron.Nov.UI.ENCommonDialogResult.OK Then Return

			' Generate an SVG document from the barcode
			Dim svgExporter As Nevron.Nov.UI.NContinuousMediaDocument(Of Nevron.Nov.Barcode.NBarcode) = New Nevron.Nov.UI.NContinuousMediaDocument(Of Nevron.Nov.Barcode.NBarcode)(Me.m_Barcode)
            Dim svgDocument As Nevron.Nov.Formats.Svg.NSvgDocument = svgExporter.CreateSvg(Me.m_Barcode, 0)

			' Save the SVG document to a file
			arg.File.Create().[Then](Sub(ByVal stream As System.IO.Stream)
                                         Using stream
                                             svgDocument.SaveToStream(stream)
                                         End Using
                                     End Sub)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Barcode As Nevron.Nov.Barcode.NBarcode

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NBarcodeSvgExportExample.
		''' </summary>
		Public Shared ReadOnly NBarcodeSvgExportExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
