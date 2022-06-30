Imports System.Globalization
Imports System.IO
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Formats
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NClipboardExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NClipboardExample.NClipboardExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NClipboardExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim tab As Nevron.Nov.UI.NTab = New Nevron.Nov.UI.NTab()
            tab.HeadersPosition = Nevron.Nov.UI.ENTabHeadersPosition.Left
            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Clipboard Text", Me.CreateTextDemo()))
            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Clipboard RTF", Me.CreateRTFDemo()))
            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Clipboard Raster", Me.CreateRasterDemo()))
            Return tab
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>The example demonstrates how to use Clipboard in NOV. Click the <b>Set Clipboard Text</b> button to set the text of the text box
to the clipboard and clear the text box. Click <b>Get Clipboard Text</b> to load the text from the clipboard to the text box. The same
goes for the rich text and the images examples.</p>"
        End Function

		#EndRegion

		#Region"Implementation - Text"

		Private Function CreateTextDemo() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            Dim setTextButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Set Clipboard Text")
            AddHandler setTextButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSetTextButtonClick)
            Dim getTextButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Get Clipboard Text")
            AddHandler getTextButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnGetTextButtonClick)
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(setTextButton, getTextButton)
            pairBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            pairBox.Spacing = Nevron.Nov.NDesign.HorizontalSpacing
            stack.Add(pairBox)
            Me.m_TextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_TextBox.Text = "This is some text. You can edit it or enter more if you want." & Global.Microsoft.VisualBasic.Constants.vbLf & Global.Microsoft.VisualBasic.Constants.vbLf & "When ready click the ""Set Clipboard Text"" button to move it to the clipboard."
            Me.m_TextBox.Multiline = True
            stack.Add(Me.m_TextBox)
            Return stack
        End Function

        Private Sub OnGetTextButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim dataObject As Nevron.Nov.UI.NDataObject = Nevron.Nov.UI.NClipboard.GetDataObject()
            Dim data As Object = dataObject.GetData(Nevron.Nov.UI.NDataFormat.TextFormatString)

            If data IsNot Nothing Then
                Me.m_TextBox.Text = CStr(data)
            End If
        End Sub

        Private Sub OnSetTextButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim dataObject As Nevron.Nov.UI.NDataObject = New Nevron.Nov.UI.NDataObject()
            dataObject.SetData(Nevron.Nov.UI.NDataFormat.TextFormatString, Me.m_TextBox.Text)
            Call Nevron.Nov.UI.NClipboard.SetDataObject(dataObject)
            Me.m_TextBox.Text = "Text box content moved to clipboard."
        End Sub

		#EndRegion

		#Region"Implementation - Rich Text"

		Private Function CreateRTFDemo() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            Dim setButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Set Clipboard RTF")
            AddHandler setButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSetRTFButtonClick)
            Dim getButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Get Clipboard RTF")
            AddHandler getButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnGetRTFButtonClick)
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(setButton, getButton)
            pairBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            pairBox.Spacing = Nevron.Nov.NDesign.HorizontalSpacing
            stack.Add(pairBox)

			' Create a rich text view and some content
			Me.m_RichText = New Nevron.Nov.Text.NRichTextView()
            Me.m_RichText.PreferredSize = New Nevron.Nov.Graphics.NSize(400, 300)
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            Dim table As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable(2, 2)
            table.AllowSpacingBetweenCells = False
            section.Blocks.Add(table)

            For i As Integer = 0 To 4 - 1
                Dim cell As Nevron.Nov.Text.NTableCell = table.Rows(CInt((i / 2))).Cells(i Mod 2)
                cell.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
                cell.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
                Dim paragraph As Nevron.Nov.Text.NParagraph = CType(cell.Blocks(0), Nevron.Nov.Text.NParagraph)
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("Cell " & (i + 1).ToString(System.Globalization.CultureInfo.InvariantCulture)))
            Next

            stack.Add(Me.m_RichText)
            Return stack
        End Function

        Private Sub OnGetRTFButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim dataObject As Nevron.Nov.UI.NDataObject = Nevron.Nov.UI.NClipboard.GetDataObject()
            Dim data As Byte() = dataObject.GetRTF()

            If data IsNot Nothing Then
                Me.m_RichText.LoadFromStream(New System.IO.MemoryStream(data), Nevron.Nov.Text.Formats.NTextFormat.Rtf)
            End If
        End Sub

        Private Sub OnSetRTFButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim dataObject As Nevron.Nov.UI.NDataObject = New Nevron.Nov.UI.NDataObject()

            Using stream As System.IO.MemoryStream = New System.IO.MemoryStream()
                Me.m_RichText.SaveToStream(stream, Nevron.Nov.Text.Formats.NTextFormat.Rtf)
                dataObject.SetData(Nevron.Nov.UI.NDataFormat.RTFFormat, stream.ToArray())
                Call Nevron.Nov.UI.NClipboard.SetDataObject(dataObject)
            End Using

			' Clear the rich text
			Me.m_RichText.Content.Sections.Clear()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(New Nevron.Nov.Text.NParagraph("Rich text content moved to clipboard."))
        End Sub

		#EndRegion

		#Region"Implementation - Images"

		Private Function CreateRasterDemo() As Nevron.Nov.UI.NWidget
            Dim rasterStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            rasterStack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            rasterStack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' create the controls that demonstrate how to place image content on the clipboard
			If True Then
                Dim setRastersGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Setting images on the clipboard")
                rasterStack.Add(setRastersGroupBox)
                Dim setRastersStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                setRastersStack.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
                setRastersGroupBox.Content = setRastersStack

                For i As Integer = 0 To 3 - 1
                    Dim pair As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox()

                    Select Case i
                        Case 0
                            pair.Box1 = New Nevron.Nov.UI.NImageBox(Nevron.Nov.Text.NResources.Image__48x48_Book_png)
                        Case 1
                            pair.Box1 = New Nevron.Nov.UI.NImageBox(Nevron.Nov.Text.NResources.Image__48x48_Clock_png)
                        Case 2
                            pair.Box1 = New Nevron.Nov.UI.NImageBox(Nevron.Nov.Text.NResources.Image__48x48_Darts_png)
                    End Select

                    pair.Box2 = New Nevron.Nov.UI.NLabel("Set me on the clipboard")
                    pair.Box2.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                    pair.BoxesRelation = Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2
                    Dim setRasterButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton(pair)
                    setRasterButton.Tag = i
                    AddHandler setRasterButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSetRasterButtonClick)
                    setRastersStack.Add(setRasterButton)
                Next
            End If

			' create the controls that demonstrate how to get image content from the clipboard
			If True Then
                Dim getRastersGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Getting images from the clipboard")
                rasterStack.Add(getRastersGroupBox)
                Dim getRastersStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                getRastersStack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
                getRastersStack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
                getRastersGroupBox.Content = getRastersStack
                Me.m_ImageBox = New Nevron.Nov.UI.NImageBox()
                Me.m_ImageBox.Margins = New Nevron.Nov.Graphics.NMargins(10)
                Me.m_ImageBox.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
                Me.m_ImageBox.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
                Me.m_ImageBox.Visibility = Nevron.Nov.UI.ENVisibility.Hidden
                Dim getRasterButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Get image from the clipboard")
                getRasterButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
                AddHandler getRasterButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnGetRasterButtonClick)
                getRastersStack.Add(getRasterButton)
                Dim scrollContent As Nevron.Nov.UI.NScrollContent = New Nevron.Nov.UI.NScrollContent()
                scrollContent.BackgroundFill = New Nevron.Nov.Graphics.NHatchFill(Nevron.Nov.Graphics.ENHatchStyle.LargeCheckerBoard, Nevron.Nov.Graphics.NColor.Gray, Nevron.Nov.Graphics.NColor.LightGray)
                scrollContent.Content = Me.m_ImageBox
                scrollContent.NoScrollHAlign = Nevron.Nov.UI.ENNoScrollHAlign.Left
                scrollContent.NoScrollVAlign = Nevron.Nov.UI.ENNoScrollVAlign.Top
                getRastersStack.Add(scrollContent)
            End If

            Return rasterStack
        End Function

        Private Sub OnSetRasterButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
			' get a raster to place on the clipbar
			Dim raster As Nevron.Nov.Graphics.NRaster = Nothing

            Select Case CInt(args.TargetNode.Tag)
                Case 0
                    raster = Nevron.Nov.Text.NResources.Image__48x48_Book_png.ImageSource.CreateRaster()
                Case 1
                    raster = Nevron.Nov.Text.NResources.Image__48x48_Clock_png.ImageSource.CreateRaster()
                Case 2
                    raster = Nevron.Nov.Text.NResources.Image__48x48_Darts_png.ImageSource.CreateRaster()
            End Select

			' create a data object
			Dim dataObject As Nevron.Nov.UI.NDataObject = New Nevron.Nov.UI.NDataObject()
            dataObject.SetData(Nevron.Nov.UI.NDataFormat.RasterFormat, raster)

			' set it on the clipboard
			Call Nevron.Nov.UI.NClipboard.SetDataObject(dataObject)
        End Sub

        Private Sub OnGetRasterButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
			' get a data object from the clipboard
			Dim dataObject As Nevron.Nov.UI.NDataObject = Nevron.Nov.UI.NClipboard.GetDataObject()

			' try get a raster from the data object
			Dim data As Object = dataObject.GetData(Nevron.Nov.UI.NDataFormat.RasterFormat)
            If data Is Nothing Then Return

			' place it inside the image box
			Dim raster As Nevron.Nov.Graphics.NRaster = CType(data, Nevron.Nov.Graphics.NRaster)
            Me.m_ImageBox.Image = New Nevron.Nov.Graphics.NImage(raster)
            Me.m_ImageBox.Visibility = Nevron.Nov.UI.ENVisibility.Visible
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_TextBox As Nevron.Nov.UI.NTextBox
        Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_ImageBox As Nevron.Nov.UI.NImageBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NClipboardExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
