Imports System
Imports System.IO
Imports System.Text
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.IO
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.Xml

Namespace Nevron.Nov.Examples.Framework
    Public Class NXmlParsingExample
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
            Nevron.Nov.Examples.Framework.NXmlParsingExample.NXmlParsingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NXmlParsingExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim splitter As Nevron.Nov.UI.NSplitter = New Nevron.Nov.UI.NSplitter()
            splitter.SplitMode = Nevron.Nov.UI.ENSplitterSplitMode.Proportional
            splitter.SplitFactor = 0.5

			' Create the "XML content" group box
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.First
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
            stack.VerticalSpacing = Nevron.Nov.NDesign.VerticalSpacing
            Me.m_XmlTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_XmlTextBox.AcceptsEnter = True
            Me.m_XmlTextBox.AcceptsTab = True
            Me.m_XmlTextBox.Multiline = True
            Me.m_XmlTextBox.WordWrap = False
            Me.m_XmlTextBox.VScrollMode = Nevron.Nov.UI.ENScrollMode.WhenNeeded
            Me.m_XmlTextBox.HScrollMode = Nevron.Nov.UI.ENScrollMode.WhenNeeded
            Me.m_XmlTextBox.Text = Nevron.Nov.Examples.Framework.NXmlParsingExample.SampleXml
            stack.Add(Me.m_XmlTextBox)
            Dim parseButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Parse")
            parseButton.Content.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            AddHandler parseButton.Click, AddressOf Me.OnParseButtonClick
            stack.Add(parseButton)
            splitter.Pane1.Content = New Nevron.Nov.UI.NGroupBox("XML Content", stack)

			' Create the "DOM tree" group box
			Me.m_DomTree = New Nevron.Nov.UI.NTreeView()
            splitter.Pane2.Content = New Nevron.Nov.UI.NGroupBox("DOM Tree", Me.m_DomTree)
            Return splitter
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim openFileButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Open File")
            AddHandler openFileButton.Click, AddressOf Me.OnOpenFileButtonClick
            openFileButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Return openFileButton
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates the XML parsing engine provided by Nevron Open Vision. Edit the XML content and when ready click
	the <b>Parse</b> button to trigger the parsing of the XML content. You will see the resulting DOM tree on the right. You
	can also load a XML file for parsing by clicking the <b>Open File</b> button on the right.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation - Parsing"

		Private Sub Parse(ByVal data As Object)
			' Parse the content of the text box
			Dim document As Nevron.Nov.Xml.NXmlDocument = Nothing

            If TypeOf data Is String Then
				' Method 1 - use a parser and a listener
				Dim listener As Nevron.Nov.Xml.NXmlDocumentParserListener = New Nevron.Nov.Xml.NXmlDocumentParserListener()
                Dim parser As Nevron.Nov.Xml.NXmlParser = New Nevron.Nov.Xml.NXmlParser(listener)
                parser.Parse((CStr(data)).ToCharArray())
                document = listener.Document
            ElseIf TypeOf data Is System.IO.Stream Then
				' Method 2 - call the static Load method of the XML document class
				document = Nevron.Nov.Xml.NXmlDocument.LoadFromStream(CType(data, System.IO.Stream))
            Else
                Throw New System.ArgumentException("Unsupported data type", "data")
            End If

			' Populate the DOM tree view
			Me.m_DomTree.SelectedItem = Nothing
            Me.m_DomTree.Items.Clear()
            Me.m_DomTree.Items.Add(Nevron.Nov.Examples.Framework.NXmlParsingExample.CreateTreeViewItem(document))
			
			' Expand all items up to the second level
			Call Nevron.Nov.Examples.Framework.NXmlParsingExample.ExpandTreeViewItems(Me.m_DomTree.Items(0), 2, 1)
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnParseButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.Parse(Me.m_XmlTextBox.Text)
        End Sub

        Private Sub OnOpenFileButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim openFileDialog As Nevron.Nov.UI.NOpenFileDialog = New Nevron.Nov.UI.NOpenFileDialog()
            openFileDialog.FileTypes = New Nevron.Nov.UI.NFileDialogFileType() {New Nevron.Nov.UI.NFileDialogFileType("Xml Files (*.xml)", "xml")}
            AddHandler openFileDialog.Closed, AddressOf Me.OnOpenFileDialogClosed
            openFileDialog.RequestShow()
        End Sub

        Private Sub OnOpenFileDialogClosed(ByVal arg As Nevron.Nov.UI.NOpenFileDialogResult)
            If arg.Result <> Nevron.Nov.UI.ENCommonDialogResult.OK Then Return
            arg.Files(CInt((0))).OpenRead().[Then](Sub(ByVal stream As System.IO.Stream)
                                                       Using stream
                                                           Me.m_XmlTextBox.Text = Nevron.Nov.IO.NStreamHelpers.ReadToEndAsString(stream)
                                                           stream.Position = 0
                                                           Me.Parse(stream)
                                                       End Using
                                                   End Sub, Sub(ByVal ex As System.Exception) Call Nevron.Nov.UI.NMessageBox.ShowError(ex.Message, "Error"))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_XmlTextBox As Nevron.Nov.UI.NTextBox
        Private m_DomTree As Nevron.Nov.UI.NTreeView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NXmlParsingExample.
		''' </summary>
		Public Shared ReadOnly NXmlParsingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateTreeViewItem(ByVal node As Nevron.Nov.Xml.NXmlNode) As Nevron.Nov.UI.NTreeViewItem
			' Create a tree view item for the current XML node
			Dim item As Nevron.Nov.UI.NTreeViewItem

            Select Case node.NodeType
                Case Nevron.Nov.Xml.ENXmlNodeType.CDATA, Nevron.Nov.Xml.ENXmlNodeType.Comment, Nevron.Nov.Xml.ENXmlNodeType.Document
                    item = New Nevron.Nov.UI.NTreeViewItem(node.Name)
                Case Nevron.Nov.Xml.ENXmlNodeType.Declaration, Nevron.Nov.Xml.ENXmlNodeType.Element
                    Dim text As String = node.Name
                    Dim element As Nevron.Nov.Xml.NXmlElement = CType(node, Nevron.Nov.Xml.NXmlElement)
                    Dim attributesIter As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.NKeyValuePair(Of String, String)) = element.GetAttributesIterator()

                    If attributesIter IsNot Nothing Then
						' Append the element attributes
						Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder(text)

                        While attributesIter.MoveNext()
                            sb.Append(" ")
                            sb.Append(attributesIter.Current.Key)
                            sb.Append("=""")
                            sb.Append(attributesIter.Current.Value)
                            sb.Append("""")
                        End While

                        text = sb.ToString()
                    End If

                    item = New Nevron.Nov.UI.NTreeViewItem(text)
                Case Nevron.Nov.Xml.ENXmlNodeType.Text
                    item = New Nevron.Nov.UI.NTreeViewItem("Text: """ & CType(node, Nevron.Nov.Xml.NXmlTextNode).Text & """")
                Case Else
                    Throw New System.Exception("New ENXmlNodeType?")
            End Select

			' Traverse the node's children and create a child item for each of them
			Dim iter As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.Xml.NXmlNode) = node.GetChildNodesIterator()

            If iter IsNot Nothing Then
                While iter.MoveNext()
                    Dim childItem As Nevron.Nov.UI.NTreeViewItem = Nevron.Nov.Examples.Framework.NXmlParsingExample.CreateTreeViewItem(iter.Current)
                    item.Items.Add(childItem)
                End While
            End If

			' Return the created tree view item
			Return item
        End Function

        Private Shared Sub ExpandTreeViewItems(ByVal item As Nevron.Nov.UI.NTreeViewItem, ByVal levelsToExand As Integer, ByVal currentLevel As Integer)
			' Expand the current item
			item.Expanded = True

			' If the desired number of levels has been expanded, quit
			If currentLevel = levelsToExand Then Return

			' Expand the child items of the current item
			currentLevel += 1
            Dim i As Integer = 0, count As Integer = item.Items.Count

            While i < count
                Call Nevron.Nov.Examples.Framework.NXmlParsingExample.ExpandTreeViewItems(item.Items(i), levelsToExand, currentLevel)
                i += 1
            End While
        End Sub

		#EndRegion

		#Region"Constants"

		Private Const SampleXml As String = "
<?xml version=""1.0""?>
<catalog>
   <book id=""bk101"">
      <author>Gambardella, Matthew</author>
      <title>XML Developer's Guide</title>
      <genre>Computer</genre>
      <price>44.95</price>
      <publish_date>2000-10-01</publish_date>
      <description>An in-depth look at creating applications 
      with XML.</description>
   </book>
   <book id=""bk102"">
      <author>Ralls, Kim</author>
      <title>Midnight Rain</title>
      <genre>Fantasy</genre>
      <price>5.95</price>
      <publish_date>2000-12-16</publish_date>
      <description>A former architect battles corporate zombies, 
      an evil sorceress, and her own childhood to become queen 
      of the world.</description>
   </book>
   <book id=""bk103"">
      <author>Corets, Eva</author>
      <title>Maeve Ascendant</title>
      <genre>Fantasy</genre>
      <price>5.95</price>
      <publish_date>2000-11-17</publish_date>
      <description>After the collapse of a nanotechnology 
      society in England, the young survivors lay the 
      foundation for a new society.</description>
   </book>
   <book id=""bk104"">
      <author>Corets, Eva</author>
      <title>Oberon's Legacy</title>
      <genre>Fantasy</genre>
      <price>5.95</price>
      <publish_date>2001-03-10</publish_date>
      <description>In post-apocalypse England, the mysterious 
      agent known only as Oberon helps to create a new life 
      for the inhabitants of London. Sequel to Maeve 
      Ascendant.</description>
   </book>
   <book id=""bk105"">
      <author>Corets, Eva</author>
      <title>The Sundered Grail</title>
      <genre>Fantasy</genre>
      <price>5.95</price>
      <publish_date>2001-09-10</publish_date>
      <description>The two daughters of Maeve, half-sisters, 
      battle one another for control of England. Sequel to 
      Oberon's Legacy.</description>
   </book>
   <book id=""bk106"">
      <author>Randall, Cynthia</author>
      <title>Lover Birds</title>
      <genre>Romance</genre>
      <price>4.95</price>
      <publish_date>2000-09-02</publish_date>
      <description>When Carla meets Paul at an ornithology 
      conference, tempers fly as feathers get ruffled.</description>
   </book>
   <book id=""bk107"">
      <author>Thurman, Paula</author>
      <title>Splish Splash</title>
      <genre>Romance</genre>
      <price>4.95</price>
      <publish_date>2000-11-02</publish_date>
      <description>A deep sea diver finds true love twenty 
      thousand leagues beneath the sea.</description>
   </book>
   <book id=""bk108"">
      <author>Knorr, Stefan</author>
      <title>Creepy Crawlies</title>
      <genre>Horror</genre>
      <price>4.95</price>
      <publish_date>2000-12-06</publish_date>
      <description>An anthology of horror stories about roaches,
      centipedes, scorpions  and other insects.</description>
   </book>
   <book id=""bk109"">
      <author>Kress, Peter</author>
      <title>Paradox Lost</title>
      <genre>Science Fiction</genre>
      <price>6.95</price>
      <publish_date>2000-11-02</publish_date>
      <description>After an inadvertant trip through a Heisenberg
      Uncertainty Device, James Salway discovers the problems 
      of being quantum.</description>
   </book>
   <book id=""bk110"">
      <author>O'Brien, Tim</author>
      <title>Microsoft .NET: The Programming Bible</title>
      <genre>Computer</genre>
      <price>36.95</price>
      <publish_date>2000-12-09</publish_date>
      <description>Microsoft's .NET initiative is explored in 
      detail in this deep programmer's reference.</description>
   </book>
   <book id=""bk111"">
      <author>O'Brien, Tim</author>
      <title>MSXML3: A Comprehensive Guide</title>
      <genre>Computer</genre>
      <price>36.95</price>
      <publish_date>2000-12-01</publish_date>
      <description>The Microsoft MSXML3 parser is covered in 
      detail, with attention to XML DOM interfaces, XSLT processing, 
      SAX and more.</description>
   </book>
   <book id=""bk112"">
      <author>Galos, Mike</author>
      <title>Visual Studio 7: A Comprehensive Guide</title>
      <genre>Computer</genre>
      <price>49.95</price>
      <publish_date>2001-04-16</publish_date>
      <description>Microsoft Visual Studio 7 is explored in depth,
      looking at how Visual Basic, Visual C++, C#, and ASP+ are 
      integrated into a comprehensive development 
      environment.</description>
   </book>
</catalog>
"

		#EndRegion
	End Class
End Namespace
