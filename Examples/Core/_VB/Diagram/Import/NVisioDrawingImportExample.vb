Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NVisioDrawingImportExample
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
            Nevron.Nov.Examples.Diagram.NVisioDrawingImportExample.NVisioDrawingImportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NVisioDrawingImportExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Protected Overrides"

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
            Return "<p>Demonstrates how to import a Visio drawing (in VSDX format) to NOV Diagram.</p>"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' Import a Visio diagram
            Me.m_DrawingView.LoadFromResource(Nevron.Nov.Diagram.NResources.RBIN_VSDX_CorporateDiagramShapes_vsdx)

            ' Hide ports
            drawingDocument.Content.ScreenVisibility.ShowPorts = False
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NVisioDrawingImportExample.
        ''' </summary>
        Public Shared ReadOnly NVisioDrawingImportExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
