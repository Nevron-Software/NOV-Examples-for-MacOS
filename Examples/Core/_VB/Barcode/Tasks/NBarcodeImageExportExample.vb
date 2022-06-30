Imports System
Imports Nevron.Nov.Barcode
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Barcode
    Public Class NBarcodeImageExportExample
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
            Nevron.Nov.Examples.Barcode.NBarcodeImageExportExample.NBarcodeImageExportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Barcode.NBarcodeImageExportExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Protected Overrides"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ImageBox = New Nevron.Nov.UI.NImageBox()
            Me.m_ImageBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Me.m_ImageBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Return Me.m_ImageBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_BarcodeTextBox = New Nevron.Nov.UI.NTextBox("Nevron Software" & System.Environment.NewLine & System.Environment.NewLine & "https://www.nevron.com")
            Me.m_BarcodeTextBox.Multiline = True
            Me.m_BarcodeTextBox.AcceptsEnter = True
            Me.m_BarcodeTextBox.PreferredHeight = 100
            AddHandler Me.m_BarcodeTextBox.TextChanged, AddressOf Me.OnBarcodeTextBoxTextChanged
            stack.Add(Me.m_BarcodeTextBox)
            Me.m_GenerateImageButton = New Nevron.Nov.UI.NButton("Generate Image")
            AddHandler Me.m_GenerateImageButton.Click, AddressOf Me.OnGenerateImageButtonClick
            stack.Add(Me.m_GenerateImageButton)
            Me.OnGenerateImageButtonClick(Nothing)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how export barcodes to raster images. Enter some text in the text box on the right
	and click the <b>Generate Image</b> button.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnBarcodeTextBoxTextChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim text As String = CStr(arg.NewValue)
            Me.m_GenerateImageButton.Enabled = Not Equals(text, Nothing) AndAlso text.Length > 0
        End Sub

        Private Sub OnGenerateImageButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim painter As Nevron.Nov.Barcode.NMatrixBarcodePainter = New Nevron.Nov.Barcode.NMatrixBarcodePainter()
            painter.Symbology = Nevron.Nov.Barcode.ENMatrixBarcodeSymbology.QrCode
            painter.Text = Me.m_BarcodeTextBox.Text
            Dim qrRaster As Nevron.Nov.Graphics.NRaster = painter.CreateRaster(100, 100, Nevron.Nov.Graphics.NRaster.DefaultResolution)
            Dim qrImage As Nevron.Nov.Graphics.NImage = New Nevron.Nov.Graphics.NImage(qrRaster)
            Me.m_ImageBox.Image = qrImage
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ImageBox As Nevron.Nov.UI.NImageBox
        Private m_BarcodeTextBox As Nevron.Nov.UI.NTextBox
        Private m_GenerateImageButton As Nevron.Nov.UI.NButton

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NBarcodeImageExportExample.
		''' </summary>
		Public Shared ReadOnly NBarcodeImageExportExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
