Imports System.IO
Imports Nevron.Nov.Compression
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NAutoCadDxfImportExample
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
            Nevron.Nov.Examples.Diagram.NAutoCadDxfImportExample.NAutoCadDxfImportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NAutoCadDxfImportExample), NExampleBaseSchema)
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
            Return "<p>Demonstrates how to import an AutoCAD Drawing Interchange Drawing (DXF) to NOV Diagram.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
			' Decompress the ZIP archive that contains the AutoCAD DXF Drawing
			Dim zipDecompressor As Nevron.Nov.Compression.NZipDecompressor = New Nevron.Nov.Compression.NZipDecompressor()

            Using stream As System.IO.Stream = New System.IO.MemoryStream(Nevron.Nov.Diagram.NResources.RBIN_DXF_FloorPlan_zip.Data)
                Call Nevron.Nov.Compression.NCompression.DecompressZip(stream, zipDecompressor)
            End Using

			' Import the AutoCAD DXF Drawing
			Me.m_DrawingView.LoadFromStream(zipDecompressor.Items(CInt((0))).Stream)

			' Hide ports
			Me.m_DrawingView.Drawing.ScreenVisibility.ShowPorts = False
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NAutoCadDxfImportExample.
		''' </summary>
		Public Shared ReadOnly NAutoCadDxfImportExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
