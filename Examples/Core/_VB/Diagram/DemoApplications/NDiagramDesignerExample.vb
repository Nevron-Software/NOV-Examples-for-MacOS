Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.UI
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NDiagramDesignerExample
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
            Nevron.Nov.Examples.Diagram.NDiagramDesignerExample.NDiagramDesignerExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NDiagramDesignerExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a library browser that displays all predefined shape factories
			Me.m_LibraryBrowser = New Nevron.Nov.Diagram.NLibraryBrowser()
            Me.m_LibraryBrowser.LibraryViewType = Nevron.Nov.Diagram.ENLibraryViewType.Thumbnails

            ' create pan and zoom
            Me.m_PanAndZoom = New Nevron.Nov.Diagram.NPanAndZoomView()
            Me.m_PanAndZoom.PreferredSize = New Nevron.Nov.Graphics.NSize(150, 150)
            
            ' create side bar
            Me.m_SideBar = New Nevron.Nov.Diagram.UI.NSideBar()

            ' create a drawing view
			Me.m_DrawingView = New Nevron.Nov.Diagram.NDrawingView()
            Me.m_DrawingView.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Fit
            Me.m_DrawingView.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Fit

            ' bind components to drawing view
			Me.m_LibraryBrowser.DrawingView = Me.m_DrawingView
            Me.m_LibraryBrowser.ResetLibraries()
            Me.m_PanAndZoom.DrawingView = Me.m_DrawingView
            Me.m_SideBar.DrawingView = Me.m_DrawingView

            ' create splitters
            Dim libraryPanSplitter As Nevron.Nov.UI.NSplitter = New Nevron.Nov.UI.NSplitter()
            libraryPanSplitter.Orientation = Nevron.Nov.Layout.ENHVOrientation.Vertical
            libraryPanSplitter.SplitMode = Nevron.Nov.UI.ENSplitterSplitMode.OffsetFromFarSide
            libraryPanSplitter.Pane1.Content = Me.m_LibraryBrowser
            libraryPanSplitter.Pane2.Content = Me.m_PanAndZoom
            Dim leftSplitter As Nevron.Nov.UI.NSplitter = New Nevron.Nov.UI.NSplitter()
            leftSplitter.Orientation = Nevron.Nov.Layout.ENHVOrientation.Horizontal
            leftSplitter.SplitMode = Nevron.Nov.UI.ENSplitterSplitMode.OffsetFromNearSide
            leftSplitter.SplitOffset = 370
            leftSplitter.Pane1.Content = libraryPanSplitter
            leftSplitter.Pane2.Content = Me.m_DrawingView
            Dim rightSplitter As Nevron.Nov.UI.NSplitter = New Nevron.Nov.UI.NSplitter()
            rightSplitter.Orientation = Nevron.Nov.Layout.ENHVOrientation.Horizontal
            rightSplitter.SplitMode = Nevron.Nov.UI.ENSplitterSplitMode.OffsetFromFarSide
            rightSplitter.SplitOffset = 370
            rightSplitter.Pane1.Content = leftSplitter
            rightSplitter.Pane2.Content = Me.m_SideBar

			' Create the ribbon UI
			Dim builder As Nevron.Nov.Diagram.NDiagramRibbonBuilder = New Nevron.Nov.Diagram.NDiagramRibbonBuilder()
            Return builder.CreateUI(rightSplitter, Me.m_DrawingView)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>Demonstrates how to create a Diagram Designer Application</p>"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_LibraryBrowser As Nevron.Nov.Diagram.NLibraryBrowser
        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_PanAndZoom As Nevron.Nov.Diagram.NPanAndZoomView
        Private m_SideBar As Nevron.Nov.Diagram.UI.NSideBar

		#EndRegion

		#Region"Schema"

		''' <summary>
        ''' Schema associated with NDiagramDesignerExample.
        ''' </summary>
        Public Shared ReadOnly NDiagramDesignerExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
	End Class
End Namespace
