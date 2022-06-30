Imports Nevron.Nov.Barcode
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NBarcodesInDiagramExample
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
            Nevron.Nov.Examples.Diagram.NBarcodesInDiagramExample.NBarcodesInDiagramExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NBarcodesInDiagramExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a simple drawing
			Dim drawingViewWithRibbon As Nevron.Nov.Diagram.NDrawingViewWithRibbon = New Nevron.Nov.Diagram.NDrawingViewWithRibbon()
            Me.m_DrawingView = drawingViewWithRibbon.View
            Me.m_DrawingView.Document.HistoryService.Pause()

            Try
                Me.InitDiagram(Me.m_DrawingView.Document)
            Finally
                Me.m_DrawingView.Document.HistoryService.[Resume]()
            End Try

            Return drawingViewWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>Demonstrates how to create and host barcodes in diagram shapes.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim activePage As Nevron.Nov.Diagram.NPage = drawingDocument.Content.ActivePage

			' Create a barcode widget
			Dim barcode As Nevron.Nov.Barcode.NMatrixBarcode = New Nevron.Nov.Barcode.NMatrixBarcode(Nevron.Nov.Barcode.ENMatrixBarcodeSymbology.QrCode, "https://www.nevron.com")

			' Create a shape and place the barcode widget in it
			Dim shape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shape.SetBounds(100, 100, 100, 100)
            shape.Widget = barcode
            activePage.Items.Add(shape)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NBarcodesInDiagramExample.
		''' </summary>
		Public Shared ReadOnly NBarcodesInDiagramExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
