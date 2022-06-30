Imports System
Imports System.IO
Imports Nevron.Nov.Compression
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.IO
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NZipDecompressionExample
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
            Nevron.Nov.Examples.Framework.NZipDecompressionExample.NZipDecompressionExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NZipDecompressionExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_HeaderLabel = New Nevron.Nov.UI.NLabel("File: CSharph.zip")
            Me.m_TreeView = New Nevron.Nov.UI.NTreeView()
            Me.m_TreeView.MinWidth = 300
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(Me.m_HeaderLabel, Me.m_TreeView, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2)
            pairBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            pairBox.Spacing = Nevron.Nov.NDesign.VerticalSpacing
            Dim sourceCodeStream As System.IO.Stream = NResources.RBIN_SourceCode_CSharp_zip.Stream
            Me.DecompressZip(sourceCodeStream)
            Return pairBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim openFileButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Open ZIP Archive")
            AddHandler openFileButton.Click, AddressOf Me.OnOpenFileButtonClick
            openFileButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Return openFileButton
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to work with ZIP archives. By default the example extracts the file names
	of the source code files of all NOV examples. Using the <b>Open ZIP Archive</b> button you can see the
	contents of another ZIP file.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub DecompressZip(ByVal stream As System.IO.Stream)
            Me.m_TreeView.SelectedItem = Nothing
            Me.m_TreeView.Items.Clear()
            Dim decompressor As Nevron.Nov.Examples.Framework.NZipDecompressionExample.ZipDecompressor = New Nevron.Nov.Examples.Framework.NZipDecompressionExample.ZipDecompressor(Me.m_TreeView)
            Call Nevron.Nov.Compression.NCompression.DecompressZip(stream, decompressor)
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnOpenFileButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim openFileDialog As Nevron.Nov.UI.NOpenFileDialog = New Nevron.Nov.UI.NOpenFileDialog()
            openFileDialog.FileTypes = New Nevron.Nov.UI.NFileDialogFileType() {New Nevron.Nov.UI.NFileDialogFileType("ZIP Archives", "zip")}
            AddHandler openFileDialog.Closed, AddressOf Me.OnOpenFileDialogClosed
            openFileDialog.RequestShow()
        End Sub

        Private Sub OnOpenFileDialogClosed(ByVal arg As Nevron.Nov.UI.NOpenFileDialogResult)
            If arg.Result <> Nevron.Nov.UI.ENCommonDialogResult.OK OrElse arg.Files.Length <> 1 Then Return
            Dim file As Nevron.Nov.IO.NFile = arg.Files(0)
            file.OpenRead().[Then](Sub(ByVal stream As System.IO.Stream)
                                       Using stream
                                           Me.DecompressZip(stream)
                                       End Using

                                       Me.m_HeaderLabel.Text = "File: " & file.Path
                                   End Sub, Sub(ByVal ex As System.Exception) Call Nevron.Nov.UI.NMessageBox.ShowError(ex.Message, "Error"))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_HeaderLabel As Nevron.Nov.UI.NLabel
        Private m_TreeView As Nevron.Nov.UI.NTreeView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NZipDecompressionExample.
		''' </summary>
		Public Shared ReadOnly NZipDecompressionExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Nested Types"

		Private Class ZipDecompressor
            Implements Nevron.Nov.Compression.INZipDecompressor

            Public Sub New(ByVal treeView As Nevron.Nov.UI.NTreeView)
                Me.m_TreeView = treeView
            End Sub

            Public Function Filter(ByVal item As Nevron.Nov.Compression.NZipItem) As Boolean Implements Global.Nevron.Nov.Compression.INZipDecompressor.Filter
                Return True
            End Function

            Public Sub OnItemDecompressed(ByVal item As Nevron.Nov.Compression.NZipItem) Implements Global.Nevron.Nov.Compression.INZipDecompressor.OnItemDecompressed
                Dim partNames As String() = item.Name.Split(Nevron.Nov.Examples.Framework.NZipDecompressionExample.ZipDecompressor.PathDelimitersCharArray, System.StringSplitOptions.RemoveEmptyEntries)
				
				' Add the folders to the tree view
				Dim items As Nevron.Nov.UI.NTreeViewItemCollection = Me.m_TreeView.Items
                Dim i As Integer = 0, partNameCount As Integer = partNames.Length - 1

                While i < partNameCount
                    Dim partName As String = partNames(i)
                    Dim treeViewItem As Nevron.Nov.UI.NTreeViewItem = Nevron.Nov.Examples.Framework.NZipDecompressionExample.ZipDecompressor.GetItemByName(items, partName)

                    If treeViewItem Is Nothing Then
						' An item with the current entry name does not exist, so create it
						treeViewItem = Nevron.Nov.Examples.Framework.NZipDecompressionExample.ZipDecompressor.AddFolder(items, partName)
                    End If

                    items = treeViewItem.Items
                    i += 1
                End While

				' Add the file
				Call Nevron.Nov.Examples.Framework.NZipDecompressionExample.ZipDecompressor.AddFile(items, partNames(partNames.Length - 1))
            End Sub

            Private Shared Function GetItemByName(ByVal items As Nevron.Nov.UI.NTreeViewItemCollection, ByVal name As String) As Nevron.Nov.UI.NTreeViewItem
                Dim i As Integer = 0, count As Integer = items.Count

                While i < count
                    Dim item As Nevron.Nov.UI.NTreeViewItem = items(i)
                    Dim itemName As String = CStr(item.Tag)
                    If Equals(itemName, name) Then Return item
                    i += 1
                End While

                Return Nothing
            End Function

            Private Shared Function CreateItem(ByVal image As Nevron.Nov.Graphics.NImage, ByVal name As String) As Nevron.Nov.UI.NTreeViewItem
                Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(image, name)
                pairBox.Box2.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                pairBox.Spacing = Nevron.Nov.NDesign.HorizontalSpacing
                Dim item As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem(pairBox)
                item.Tag = name
                Return item
            End Function

            Private Shared Function AddFolder(ByVal items As Nevron.Nov.UI.NTreeViewItemCollection, ByVal name As String) As Nevron.Nov.UI.NTreeViewItem
				' Find the place for the folder item
				Dim i As Integer

                For i = items.Count - 1 To 0 Step -1

                    If items(CInt((i))).Items.Count > 0 Then
						' This is not a leaf node, which means we have reached the last folder in the given list of items
						Exit For
                    End If
                Next

				' Insert the folder item
				Dim item As Nevron.Nov.UI.NTreeViewItem = CreateItem(NResources.Image__16x16_Folders_png, name)
                items.Insert(i + 1, item)
                Return item
            End Function

            Private Shared Function AddFile(ByVal items As Nevron.Nov.UI.NTreeViewItemCollection, ByVal name As String) As Nevron.Nov.UI.NTreeViewItem
                Dim item As Nevron.Nov.UI.NTreeViewItem = CreateItem(NResources.Image__16x16_Contacts_png, name)
                items.Add(item)
                Return item
            End Function

            Private m_TreeView As Nevron.Nov.UI.NTreeView
            Private Shared ReadOnly PathDelimitersCharArray As Char() = New Char() {"\"c, "/"c}
        End Class

		#EndRegion
	End Class
End Namespace
