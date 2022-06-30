Imports System
Imports System.IO
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.IO
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Formats
Imports Nevron.Nov.Text.Formats.Html
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NHtmlExportExample
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
            Nevron.Nov.Examples.Text.NHtmlExportExample.NHtmlExportExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NHtmlExportExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim splitter As Nevron.Nov.UI.NSplitter = New Nevron.Nov.UI.NSplitter()
            splitter.SplitMode = Nevron.Nov.UI.ENSplitterSplitMode.Proportional
            splitter.SplitFactor = 0.5

            ' Create the rich text view
            Dim richTextWithRibbon As Nevron.Nov.Text.NRichTextViewWithRibbon = New Nevron.Nov.Text.NRichTextViewWithRibbon()
            Me.m_RichText = richTextWithRibbon.View
            Me.m_RichText.AcceptsTab = True
            Me.m_RichText.Content.Sections.Clear()

            ' Stack the rich text with ribbon and an export button
            Dim exportButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Export")
            exportButton.Content.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            AddHandler exportButton.Click, AddressOf Me.OnExportButtonClick
            splitter.Pane1.Content = Nevron.Nov.Examples.Text.NHtmlExportExample.CreatePairBox(richTextWithRibbon, exportButton)

            ' Create the HTML rich text box
			Me.m_HtmlTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_HtmlTextBox.AcceptsEnter = True
            Me.m_HtmlTextBox.AcceptsTab = True
            Me.m_HtmlTextBox.Multiline = True
            Me.m_HtmlTextBox.WordWrap = False
            Me.m_HtmlTextBox.VScrollMode = Nevron.Nov.UI.ENScrollMode.WhenNeeded
            Me.m_HtmlTextBox.HScrollMode = Nevron.Nov.UI.ENScrollMode.WhenNeeded
            Me.m_HtmlTextBox.[ReadOnly] = True
            splitter.Pane2.Content = New Nevron.Nov.UI.NGroupBox("Exported HTML", Me.m_HtmlTextBox)
            Return splitter
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.VerticalSpacing = 10

			' Create the export settings check boxes
			Me.m_InlineStylesCheckBox = New Nevron.Nov.UI.NCheckBox("Inline styles", False)
            AddHandler Me.m_InlineStylesCheckBox.CheckedChanged, AddressOf Me.OnSettingsCheckBoxCheckedChanged
            stack.Add(Me.m_InlineStylesCheckBox)
            Me.m_MinifyHtmlCheckBox = New Nevron.Nov.UI.NCheckBox("Minify HTML", False)
            AddHandler Me.m_MinifyHtmlCheckBox.CheckedChanged, AddressOf Me.OnSettingsCheckBoxCheckedChanged
            stack.Add(Me.m_MinifyHtmlCheckBox)

            ' Create the predefined tests list box
            Dim testListBox As Nevron.Nov.UI.NListBox = New Nevron.Nov.UI.NListBox()
            testListBox.Items.Add(Nevron.Nov.Examples.Text.NHtmlExportExample.CreateTestListBoxItem(New Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextBorders()))
            testListBox.Items.Add(Nevron.Nov.Examples.Text.NHtmlExportExample.CreateTestListBoxItem(New Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextLists()))
            testListBox.Items.Add(Nevron.Nov.Examples.Text.NHtmlExportExample.CreateTestListBoxItem(New Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextTables()))
            testListBox.Items.Add(Nevron.Nov.Examples.Text.NHtmlExportExample.CreateTestListBoxItem(New Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextTextStyles()))
            testListBox.Items.Add(Nevron.Nov.Examples.Text.NHtmlExportExample.CreateTestListBoxItem(New Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextElementPositioning()))
            AddHandler testListBox.Selection.Selected, AddressOf Me.OnTestListBoxItemSelected

            ' Add the list box in a group box
            stack.Add(New Nevron.Nov.UI.NGroupBox("Predefined text documents", testListBox))

            ' Create the Load from file group box
            Dim dockPanel As Nevron.Nov.UI.NDockPanel = New Nevron.Nov.UI.NDockPanel()
            dockPanel.HorizontalSpacing = 3
            dockPanel.VerticalSpacing = 3
            Dim loadButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Load")
            AddHandler loadButton.Click, AddressOf Me.OnLoadButtonClick
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(loadButton, Nevron.Nov.Layout.ENDockArea.Bottom)
            dockPanel.Add(loadButton)
            Me.m_FileNameTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_FileNameTextBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(Me.m_FileNameTextBox, Nevron.Nov.Layout.ENDockArea.Center)
            dockPanel.Add(Me.m_FileNameTextBox)
            Dim browseButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("...")
            AddHandler browseButton.Click, AddressOf Me.OnBrowseButtonClick
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(browseButton, Nevron.Nov.Layout.ENDockArea.Right)
            dockPanel.Add(browseButton)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Load from file", dockPanel))
            Me.m_ElapsedTimeLabel = New Nevron.Nov.UI.NLabel()
            stack.Add(Me.m_ElapsedTimeLabel)

            ' Select the initial test
            testListBox.Selection.SingleSelect(testListBox.Items(0))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates the HTML export capabilities of the Nevron Rich Text widget. Simply select one of the preloaded examples
    from the combo box to the right and see it exported. You can also edit the text document and press the <b>Export</b> button when ready.
</p>
"
        End Function

        #EndRegion

        #Region"Implementation"

        Private Sub ExportToHtml()
            Me.m_ElapsedTimeLabel.Text = Nothing
            Dim stopwatch As Nevron.Nov.NStopwatch = Nevron.Nov.NStopwatch.StartNew()

            Using stream As System.IO.MemoryStream = New System.IO.MemoryStream()
				' Create and configure HTML save settings
				Dim saveSettings As Nevron.Nov.Text.Formats.Html.NHtmlSaveSettings = New Nevron.Nov.Text.Formats.Html.NHtmlSaveSettings()
                saveSettings.InlineStyles = Me.m_InlineStylesCheckBox.Checked
                saveSettings.MinifyHtml = Me.m_MinifyHtmlCheckBox.Checked

				' Save to HTML
                Me.m_RichText.SaveToStream(stream, Nevron.Nov.Text.Formats.NTextFormat.Html, saveSettings)
                stopwatch.[Stop]()
                Me.LoadHtmlSource(stream)
            End Using

            Me.m_ElapsedTimeLabel.Text = "Export done in: " & stopwatch.ElapsedMilliseconds.ToString() & " ms."
        End Sub

        Private Sub LoadHtmlSource(ByVal stream As System.IO.Stream)
            stream.Position = 0
            Dim bytes As Byte() = Nevron.Nov.IO.NStreamHelpers.ReadToEnd(stream)
            Me.m_HtmlTextBox.Text = Nevron.Nov.Text.NEncoding.UTF8.GetString(bytes)
        End Sub

        Private Function LoadRtfFromFile(ByVal fileName As String) As Boolean
            Try
                Me.m_RichText.LoadFromFile(fileName)
            Catch
                Me.m_ElapsedTimeLabel.Text = "RTF loading failed."
                Return False
            End Try

            Return True
        End Function

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnExportButtonClick(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Me.ExportToHtml()
        End Sub

        Private Sub OnSettingsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.ExportToHtml()
        End Sub

        Private Sub OnTestListBoxItemSelected(ByVal arg1 As Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))
            Dim selectedItem As Nevron.Nov.UI.NListBoxItem = arg1.Item
            If selectedItem Is Nothing Then Return
            Dim example As Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextToHtmlExample = TryCast(selectedItem.Tag, Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextToHtmlExample)
            If example Is Nothing Then Return

            ' Recreate the content of the Nevron rich text widget
			Dim documentRoot As Nevron.Nov.Text.NDocumentBlock = example.CreateDocument()
            Dim document As Nevron.Nov.Text.NRichTextDocument = New Nevron.Nov.Text.NRichTextDocument()
            document.Content = documentRoot
            document.Evaluate()
            Me.m_RichText.Document = document
            Me.ExportToHtml()
        End Sub

        Private Sub OnBrowseButtonClick(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Dim openFileDialog As Nevron.Nov.UI.NOpenFileDialog = New Nevron.Nov.UI.NOpenFileDialog()
            openFileDialog.FileTypes = New Nevron.Nov.UI.NFileDialogFileType() {New Nevron.Nov.UI.NFileDialogFileType("Word Documents and Rich Text Files", New String() {"docx", "rtf"})}
            openFileDialog.SelectedFilterIndex = 0
            openFileDialog.MultiSelect = False
            openFileDialog.InitialDirectory = System.[String].Empty
            AddHandler openFileDialog.Closed, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NOpenFileDialogResult)(Sub(ByVal result As Nevron.Nov.UI.NOpenFileDialogResult)
                                                                                                                    If result.Result <> Nevron.Nov.UI.ENCommonDialogResult.OK Then Return
                                                                                                                    Dim fileName As String = result.Files(CInt((0))).Path
                                                                                                                    Me.m_FileNameTextBox.Text = fileName

                                                                                                                    If Me.LoadRtfFromFile(fileName) Then
                                                                                                                        Me.ExportToHtml()
                                                                                                                    End If
                                                                                                                End Sub)
            openFileDialog.RequestShow()
        End Sub

        Private Sub OnLoadButtonClick(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Dim fileName As String = Me.m_FileNameTextBox.Text
            Dim file As Nevron.Nov.IO.NFile = Nevron.Nov.IO.NFileSystem.Current.GetFile(fileName)
            If file Is Nothing Then Return
            file.Exists().[Then](Sub(ByVal exists As Boolean)
                                     If exists AndAlso Me.LoadRtfFromFile(fileName) Then Me.ExportToHtml()
                                 End Sub)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_HtmlTextBox As Nevron.Nov.UI.NTextBox
        Private m_InlineStylesCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_MinifyHtmlCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_FileNameTextBox As Nevron.Nov.UI.NTextBox
        Private m_ElapsedTimeLabel As Nevron.Nov.UI.NLabel

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NHtmlExportExample.
        ''' </summary>
        Public Shared ReadOnly NHtmlExportExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Static Methods"

        Private Shared Function CreateTestListBoxItem(ByVal example As Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextToHtmlExample) As Nevron.Nov.UI.NListBoxItem
            Dim item As Nevron.Nov.UI.NListBoxItem = New Nevron.Nov.UI.NListBoxItem(example.Title)
            item.Tag = example
            Return item
        End Function

        Private Shared Function CreatePairBox(ByVal widget1 As Nevron.Nov.UI.NWidget, ByVal widget2 As Nevron.Nov.UI.NWidget) As Nevron.Nov.UI.NPairBox
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(widget1, widget2, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2)
            pairBox.FitMode = Nevron.Nov.Layout.ENStackFitMode.First
            pairBox.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
            pairBox.Spacing = Nevron.Nov.NDesign.VerticalSpacing
            Return pairBox
        End Function

        #EndRegion

        #Region"Nested Types"

        Private MustInherit Class NRichTextToHtmlExample
            Public Sub New(ByVal title As String)
                Me.m_Title = title
            End Sub

            Public ReadOnly Property Title As String
                Get
                    Return Me.m_Title
                End Get
            End Property

            Public Overridable Function CreateDocument() As Nevron.Nov.Text.NDocumentBlock
                Dim document As Nevron.Nov.Text.NDocumentBlock = New Nevron.Nov.Text.NDocumentBlock()
                document.Information.Title = Me.m_Title
                Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
                document.Sections.Add(section)
                Dim heading As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(Me.m_Title)
                section.Blocks.Add(heading)
                heading.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Center
                heading.FontSize = 24
                Return document
            End Function

            Private m_Title As String
        End Class

        Private Class NRichTextBorders
            Inherits Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextToHtmlExample

            Public Sub New()
                MyBase.New("Borders")
            End Sub

            Public Overrides Function CreateDocument() As Nevron.Nov.Text.NDocumentBlock
                Dim document As Nevron.Nov.Text.NDocumentBlock = MyBase.CreateDocument()
                Dim section As Nevron.Nov.Text.NSection = document.Sections(0)
                Dim p As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Black solid border")
                section.Blocks.Add(p)
                p.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
                p.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
                p = New Nevron.Nov.Text.NParagraph("Black dashed border")
                section.Blocks.Add(p)
                p.Border = New Nevron.Nov.UI.NBorder()
                p.Border.MiddleStroke = New Nevron.Nov.Graphics.NStroke(5, Nevron.Nov.Graphics.NColor.Black, Nevron.Nov.Graphics.ENDashStyle.Dash)
                p.BorderThickness = New Nevron.Nov.Graphics.NMargins(5)
                p = New Nevron.Nov.Text.NParagraph("Green/DarkGreen two-color border")
                section.Blocks.Add(p)
                p.Border = Nevron.Nov.UI.NBorder.CreateTwoColorBorder(Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.Graphics.NColor.DarkGreen)
                p.BorderThickness = New Nevron.Nov.Graphics.NMargins(10)
                p = New Nevron.Nov.Text.NParagraph("A border with left, right and bottom sides and wide but not set top side")
                section.Blocks.Add(p)
                p.Border = New Nevron.Nov.UI.NBorder()
                p.Border.LeftSide = New Nevron.Nov.UI.NThreeColorsBorderSide(Nevron.Nov.Graphics.NColor.Black, Nevron.Nov.Graphics.NColor.Gray, Nevron.Nov.Graphics.NColor.LightGray)
                p.Border.RightSide = New Nevron.Nov.UI.NBorderSide()
                p.Border.RightSide.OuterStroke = New Nevron.Nov.Graphics.NStroke(10, Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.Graphics.ENDashStyle.Dot)
                p.Border.BottomSide = New Nevron.Nov.UI.NBorderSide(Nevron.Nov.Graphics.NColor.Red)
                p.BorderThickness = New Nevron.Nov.Graphics.NMargins(9, 50, 5, 5)
                Return document
            End Function
        End Class

        Private Class NRichTextTextStyles
            Inherits Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextToHtmlExample

            Public Sub New()
                MyBase.New("Text Styles")
            End Sub

            Public Overrides Function CreateDocument() As Nevron.Nov.Text.NDocumentBlock
                Dim document As Nevron.Nov.Text.NDocumentBlock = MyBase.CreateDocument()
                Dim section As Nevron.Nov.Text.NSection = document.Sections(0)
                section.Blocks.Add(New Nevron.Nov.Text.NParagraph("This is the first paragraph."))
                section.Blocks.Add(New Nevron.Nov.Text.NParagraph("This is the second paragraph." & Global.Microsoft.VisualBasic.Constants.vbLf & "This is part of the second paragraph, too."))
                Dim div As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
                section.Blocks.Add(div)
                div.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Red)
                Dim p As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("This is a paragraph in a div. It should have red underlined text.")
                div.Blocks.Add(p)
                p.FontStyle = Nevron.Nov.Graphics.ENFontStyle.Underline
                p = New Nevron.Nov.Text.NParagraph("This is another paragraph in the div. It contains a ")
                div.Blocks.Add(p)
                Dim inline As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline("bold italic blue inline")
                p.Inlines.Add(inline)
                inline.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Blue)
                inline.FontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold Or Nevron.Nov.Graphics.ENFontStyle.Italic
                p.Inlines.Add(New Nevron.Nov.Text.NTextInline("."))
                Return document
            End Function
        End Class

        Private Class NRichTextTables
            Inherits Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextToHtmlExample

            Public Sub New()
                MyBase.New("Tables")
            End Sub

            Public Overrides Function CreateDocument() As Nevron.Nov.Text.NDocumentBlock
                Dim document As Nevron.Nov.Text.NDocumentBlock = MyBase.CreateDocument()

                ' Create a simple 2x2 table
                Dim section As Nevron.Nov.Text.NSection = document.Sections(0)
                Dim table As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable(2, 2)
                section.Blocks.Add(table)
                Dim row As Integer = 0, i As Integer = 1

                While row < table.Rows.Count
                    Dim col As Integer = 0

                    While col < table.Columns.Count
                        Call Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextTables.InitCell(table.Rows(CInt((row))).Cells(col), "Cell " & i.ToString())
                        col += 1
                        i += 1
                    End While

                    row += 1
                End While

                ' Create a 3x3 table with rowspans and colspans
                table = New Nevron.Nov.Text.NTable(4, 3)
                section.Blocks.Add(table)
                Call Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextTables.InitCell(table.Rows(CInt((0))).Cells(0), 2, 1, "Cell 1 (2 rows)")
                Call Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextTables.InitCell(table.Rows(CInt((0))).Cells(1), "Cell 2")
                Call Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextTables.InitCell(table.Rows(CInt((0))).Cells(2), "Cell 3")
                Call Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextTables.InitCell(table.Rows(CInt((1))).Cells(1), 1, 2, "Cell 4 (2 cols)")
                Call Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextTables.InitCell(table.Rows(CInt((2))).Cells(0), "Cell 5")
                Call Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextTables.InitCell(table.Rows(CInt((2))).Cells(1), 2, 2, "Cell 6 (2 rows x 2 cols)")
                Call Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextTables.InitCell(table.Rows(CInt((3))).Cells(0), "Cell 7")
                Return document
            End Function

            Private Shared Sub InitCell(ByVal cell As Nevron.Nov.Text.NTableCell, ByVal text As String)
                Call Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextTables.InitCell(cell, 1, 1, text)
            End Sub

            Private Shared Sub InitCell(ByVal cell As Nevron.Nov.Text.NTableCell, ByVal rowSpan As Integer, ByVal colSpan As Integer, ByVal text As String)
                If rowSpan <> 1 Then
                    cell.RowSpan = rowSpan
                End If

                If colSpan <> 1 Then
                    cell.ColSpan = colSpan
                End If

                ' By default cells contain a single paragraph
                cell.Blocks.Clear()
                cell.Blocks.Add(New Nevron.Nov.Text.NParagraph(text))

                ' Create a border
                cell.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
                cell.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            End Sub
        End Class

        Private Class NRichTextLists
            Inherits Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextToHtmlExample

            Public Sub New()
                MyBase.New("Lists")
            End Sub

            Public Overrides Function CreateDocument() As Nevron.Nov.Text.NDocumentBlock
                Dim document As Nevron.Nov.Text.NDocumentBlock = MyBase.CreateDocument()
                Dim section As Nevron.Nov.Text.NSection = document.Sections(0)

                ' Add bullet lists of all unordered types
                Dim bulletTypes As Nevron.Nov.Text.ENBulletListTemplateType() = New Nevron.Nov.Text.ENBulletListTemplateType() {Nevron.Nov.Text.ENBulletListTemplateType.Bullet}

                For i As Integer = 0 To bulletTypes.Length - 1
                    Dim bulletList As Nevron.Nov.Text.NBulletList = New Nevron.Nov.Text.NBulletList(Nevron.Nov.Text.ENBulletListTemplateType.Bullet)
                    document.BulletLists.Add(bulletList)

                    For j As Integer = 1 To 3
                        Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("This is parargaph number " & j.ToString() & ". This paragraph is contained in a bullet list of type " & bulletTypes(CInt((i))).ToString())
                        paragraph.SetBulletList(bulletList, 0)
                        section.Blocks.Add(paragraph)
                    Next
                Next

                ' Add bullet lists of all ordered types
                bulletTypes = New Nevron.Nov.Text.ENBulletListTemplateType() {Nevron.Nov.Text.ENBulletListTemplateType.[Decimal], Nevron.Nov.Text.ENBulletListTemplateType.LowerAlpha, Nevron.Nov.Text.ENBulletListTemplateType.LowerRoman, Nevron.Nov.Text.ENBulletListTemplateType.UpperAlpha, Nevron.Nov.Text.ENBulletListTemplateType.UpperRoman}

                For i As Integer = 0 To bulletTypes.Length - 1
                    section.Blocks.Add(New Nevron.Nov.Text.NParagraph())
                    Dim bulletList As Nevron.Nov.Text.NBulletList = New Nevron.Nov.Text.NBulletList(bulletTypes(i))

                    For j As Integer = 1 To 3
                        Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Bullet List Item " & j.ToString(), bulletList, 0)
                        section.Blocks.Add(paragraph)

                        For z As Integer = 1 To 3
                            Dim par2 As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("Bullet List Sub Item " & z.ToString(), bulletList, 1)
                            section.Blocks.Add(par2)
                        Next
                    Next
                Next

                Return document
            End Function
        End Class

        Private Class NRichTextElementPositioning
            Inherits Nevron.Nov.Examples.Text.NHtmlExportExample.NRichTextToHtmlExample

            Public Sub New()
                MyBase.New("Element Positioning")
            End Sub

            Public Overrides Function CreateDocument() As Nevron.Nov.Text.NDocumentBlock
                Dim document As Nevron.Nov.Text.NDocumentBlock = MyBase.CreateDocument()
                Dim section As Nevron.Nov.Text.NSection = document.Sections(0)
                Dim p As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("This is a red paragraph on the left.")
                p.HorizontalAnchor = Nevron.Nov.Text.ENHorizontalAnchor.Ancestor
                p.HorizontalBlockAlignment = Nevron.Nov.Text.ENHorizontalBlockAlignment.Left
                p.VerticalAnchor = Nevron.Nov.Text.ENVerticalAnchor.Ancestor
                p.XOffset = 20
                p.YOffset = 200
                p.PreferredWidth = Nevron.Nov.NMultiLength.NewPercentage(25)
                p.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Red)
                section.Blocks.Add(p)
                p = New Nevron.Nov.Text.NParagraph("This is a green paragraph on the top.")
                p.HorizontalAnchor = Nevron.Nov.Text.ENHorizontalAnchor.Ancestor
                p.VerticalAnchor = Nevron.Nov.Text.ENVerticalAnchor.Ancestor
                p.VerticalBlockAlignment = Nevron.Nov.Text.ENVerticalBlockAlignment.Top
                p.XOffset = 120
                p.YOffset = 100
                p.PreferredWidth = Nevron.Nov.NMultiLength.NewPercentage(50)
                p.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Green)
                section.Blocks.Add(p)
                p = New Nevron.Nov.Text.NParagraph("This is a blue paragraph on the right.")
                p.HorizontalAnchor = Nevron.Nov.Text.ENHorizontalAnchor.Ancestor
                p.HorizontalBlockAlignment = Nevron.Nov.Text.ENHorizontalBlockAlignment.Right
                p.VerticalAnchor = Nevron.Nov.Text.ENVerticalAnchor.Ancestor
                p.XOffset = 20
                p.YOffset = 200
                p.PreferredWidth = Nevron.Nov.NMultiLength.NewPercentage(25)
                p.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Blue)
                p.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
                section.Blocks.Add(p)
                Return document
            End Function
        End Class

        #EndRegion
    End Class
End Namespace
