Imports System.IO
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Data
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NMailMergeExample
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
            Nevron.Nov.Examples.Diagram.NMailMergeExample.NMailMergeExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NMailMergeExample), NExampleBaseSchema)
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
            Dim previewMailMergeUri As Nevron.Nov.NDataUri = Nevron.Nov.NDataUri.FromImage(NResources.Image_Documentation_PreviewResults_png)
            Return "
<p>
	This example demonstrates how to use the mail merge functionality of the Nevron Diagram control.
</p>
<p>
	Click the <b>Preview Mail Merge</b> button (&nbsp;<img src=""" & previewMailMergeUri.ToString() & """ />&nbsp;) from the <b>Mailings</b> ribbon tab to see the values for the currently selected
    mail merge record. When ready click the <b>Merge & Save</b> button to save all merged documents to a file.
</p>
<p>
	The <b>Merge & Save</b> button saves each of the individual documents result of the mail
	merge operation to a folder.	
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

            ' Hide the grid and the ports
            drawing.ScreenVisibility.ShowGrid = False
            drawing.ScreenVisibility.ShowPorts = False

            ' Create a shape factory
            Dim basicShapeFactory As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()
            basicShapeFactory.DefaultSize = New Nevron.Nov.Graphics.NSize(100, 100)

            ' Create the Name shape
            Dim nameShape As Nevron.Nov.Diagram.NShape = basicShapeFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            nameShape.Width = 150
            nameShape.PinX = activePage.Width / 2
            nameShape.PinY = 100
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            paragraph.Inlines.Add(New Nevron.Nov.Text.NFieldInline(New Nevron.Nov.Text.NMailMergePredefinedFieldValue(Nevron.Nov.Text.ENMailMergeDataField.FirstName)))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline(" "))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NFieldInline(New Nevron.Nov.Text.NMailMergePredefinedFieldValue(Nevron.Nov.Text.ENMailMergeDataField.LastName)))
            nameShape.GetTextBlock().Content.Blocks.Clear()
            nameShape.GetTextBlock().Content.Blocks.Add(paragraph)
            activePage.Items.Add(nameShape)

            ' Create the City shape
            Dim cityShape As Nevron.Nov.Diagram.NShape = basicShapeFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.SixPointStar)
            cityShape.PinX = nameShape.PinX - 150
            cityShape.PinY = nameShape.PinY + 200
            paragraph = New Nevron.Nov.Text.NParagraph()
            paragraph.Inlines.Add(New Nevron.Nov.Text.NFieldInline(New Nevron.Nov.Text.NMailMergePredefinedFieldValue(Nevron.Nov.Text.ENMailMergeDataField.City)))
            cityShape.GetTextBlock().Content.Blocks.Clear()
            cityShape.GetTextBlock().Content.Blocks.Add(paragraph)
            activePage.Items.Add(cityShape)

            ' Create the Birth Date shape
            Dim birthDateShape As Nevron.Nov.Diagram.NShape = basicShapeFactory.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Circle)
            birthDateShape.PinX = nameShape.PinX + 150
            birthDateShape.PinY = cityShape.PinY
            paragraph = New Nevron.Nov.Text.NParagraph()
            paragraph.Inlines.Add(New Nevron.Nov.Text.NFieldInline(New Nevron.Nov.Text.NMailMergeSourceFieldValue("BirthDate")))
            birthDateShape.GetTextBlock().Content.Blocks.Clear()
            birthDateShape.GetTextBlock().Content.Blocks.Add(paragraph)
            activePage.Items.Add(birthDateShape)

            ' Connect the shapes
            Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            connector.Text = "City"
            connector.TextBlock.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            connector.GlueBeginToNearestPort(nameShape)
            connector.GlueEndToNearestPort(cityShape)
            activePage.Items.Add(connector)
            connector = New Nevron.Nov.Diagram.NRoutableConnector()
            connector.Text = "Birth Date"
            connector.TextBlock.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            connector.GlueBeginToNearestPort(nameShape)
            connector.GlueEndToNearestPort(birthDateShape)
            activePage.Items.Add(connector)

            ' Load a mail merge data source from resource
            Dim stream As System.IO.Stream = NResources.Instance.GetResourceStream("RSTR_Employees_csv")
            Dim dataSource As Nevron.Nov.Text.NMailMergeDataSource = Nevron.Nov.Text.Data.NDataSourceFormat.Csv.LoadFromStream(stream, New Nevron.Nov.Text.Data.NDataSourceLoadSettings(Nothing, Nothing, True))

            ' Set some field mappings
            Dim fieldMap As Nevron.Nov.Text.NMailMergeFieldMap = New Nevron.Nov.Text.NMailMergeFieldMap()
            fieldMap.[Set](Nevron.Nov.Text.ENMailMergeDataField.CourtesyTitle, "TitleOfCourtesy")
            fieldMap.[Set](Nevron.Nov.Text.ENMailMergeDataField.FirstName, "FirstName")
            fieldMap.[Set](Nevron.Nov.Text.ENMailMergeDataField.LastName, "LastName")
            fieldMap.[Set](Nevron.Nov.Text.ENMailMergeDataField.City, "City")

            ' Configure the drawing's mail merge
            drawing.MailMerge.DataSource = dataSource
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NMailMergeExample.
        ''' </summary>
        Public Shared ReadOnly NMailMergeExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
