Imports System.IO
Imports System.Text
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI
Imports Nevron.Nov.Xml

Namespace Nevron.Nov.Examples.Framework
    Public Class NXmlSerializingExample
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
            Nevron.Nov.Examples.Framework.NXmlSerializingExample.NXmlSerializingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NXmlSerializingExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Properties"

		Private ReadOnly Property DocumentItem As Nevron.Nov.UI.NTreeViewItem
            Get
                Return Me.m_TreeView.Items(0)
            End Get
        End Property

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim splitter As Nevron.Nov.UI.NSplitter = New Nevron.Nov.UI.NSplitter()
            splitter.SplitMode = Nevron.Nov.UI.ENSplitterSplitMode.Proportional
            splitter.SplitFactor = 0.5

			' Create the "Dom Tree" group box
			Me.m_TreeView = Nevron.Nov.Examples.Framework.NXmlSerializingExample.CreateTreeView()
            AddHandler Me.m_TreeView.SelectedPathChanged, AddressOf Me.OnTreeViewSelectedPathChanged
            Dim toolBar As Nevron.Nov.UI.NToolBar = New Nevron.Nov.UI.NToolBar()
            Me.m_AddChildItemButton = CreateButton(Nevron.Nov.Text.NResources.Image_Add_png, "Add Child Item")
            AddHandler Me.m_AddChildItemButton.Click, AddressOf Me.OnAddChildItemButtonClick
            toolBar.Items.Add(Me.m_AddChildItemButton)
            Me.m_RemoveSelectedItemButton = CreateButton(Nevron.Nov.Text.NResources.Image_Delete_png, "Remove Selected Item")
            AddHandler Me.m_RemoveSelectedItemButton.Click, AddressOf Me.OnRemoveSelectedItemButtonClick
            toolBar.Items.Add(Me.m_RemoveSelectedItemButton)
            toolBar.Items.Add(New Nevron.Nov.UI.NCommandBarSeparator())
            Me.m_AddAttributeButton = CreateButton(Nevron.Nov.Text.NResources.Image_Add_png, "Add Attribute")
            AddHandler Me.m_AddAttributeButton.Click, AddressOf Me.OnAddAttributeButtonClick
            toolBar.Items.Add(Me.m_AddAttributeButton)
            Me.m_RemoveAttributeButton = CreateButton(Nevron.Nov.Text.NResources.Image_Delete_png, "Remove Attribute")
            AddHandler Me.m_RemoveAttributeButton.Click, AddressOf Me.OnRemoveAttributeButtonClick
            toolBar.Items.Add(Me.m_RemoveAttributeButton)
            toolBar.Items.Add(New Nevron.Nov.UI.NCommandBarSeparator())
            Me.m_SerializeButton = CreateButton(Nevron.Nov.Text.NResources.Image__16x16_Contacts_png, "Serialize")
            Me.m_SerializeButton.Enabled = True
            AddHandler Me.m_SerializeButton.Click, AddressOf Me.OnSerializeButtonClick
            toolBar.Items.Add(Me.m_SerializeButton)
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(Me.m_TreeView, toolBar, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2)
            pairBox.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
            pairBox.FitMode = Nevron.Nov.Layout.ENStackFitMode.First
            pairBox.Spacing = Nevron.Nov.NDesign.VerticalSpacing
            splitter.Pane1.Content = pairBox

			' Create the "XML output" group box
			Me.m_XmlTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_XmlTextBox.AcceptsEnter = True
            Me.m_XmlTextBox.AcceptsTab = True
            Me.m_XmlTextBox.Multiline = True
            Me.m_XmlTextBox.WordWrap = False
            Me.m_XmlTextBox.VScrollMode = Nevron.Nov.UI.ENScrollMode.WhenNeeded
            Me.m_XmlTextBox.HScrollMode = Nevron.Nov.UI.ENScrollMode.WhenNeeded
            splitter.Pane2.Content = Me.m_XmlTextBox

			' Select the "Document" tree view item
			Me.m_TreeView.SelectedItem = Me.DocumentItem
            Return splitter
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and serialize XML documents with Nevron Open Vision. Use the buttons below the tree
	view to create a DOM tree and when ready click the <b>Serialize</b> button to construct a XML document from it and serialize
	it to the text box on the right.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnAddChildItemButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim dialog As Nevron.Nov.UI.NTopLevelWindow = Nevron.Nov.NApplication.CreateTopLevelWindow(Nevron.Nov.UI.NWindow.GetFocusedWindowIfNull(DisplayWindow))
            dialog.SetupDialogWindow("Enter element's name", False)
            Dim textBox As Nevron.Nov.UI.NTextBox = New Nevron.Nov.UI.NTextBox()
            Dim buttonStrip As Nevron.Nov.UI.NButtonStrip = New Nevron.Nov.UI.NButtonStrip()
            buttonStrip.AddOKCancelButtons()
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(textBox, buttonStrip, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2)
            pairBox.Spacing = Nevron.Nov.NDesign.VerticalSpacing
            dialog.Content = pairBox
            AddHandler dialog.Opened, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) textBox.Focus()
            AddHandler dialog.Closed, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs)
                                          If dialog.Result = Nevron.Nov.UI.ENWindowResult.OK Then
					' Add an item with the specified name
					Me.m_TreeView.SelectedItem.Items.Add(Nevron.Nov.Examples.Framework.NXmlSerializingExample.CreateTreeViewItem(textBox.Text))
                                              Me.m_TreeView.SelectedItem.Expanded = True

                                              If Me.m_SerializeButton.Enabled = False Then
                                                  Me.m_SerializeButton.Enabled = True
                                              End If
                                          End If
                                      End Sub

            dialog.Open()
        End Sub

        Private Sub OnRemoveSelectedItemButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Call Nevron.Nov.UI.NMessageBox.Show(CObj((Nevron.Nov.NLoc.[Get](CStr(("Remove the selected tree view item"))))), CStr((Nevron.Nov.NLoc.[Get](CStr(("Question"))))), CType((Nevron.Nov.UI.ENMessageBoxButtons.YesNo), Nevron.Nov.UI.ENMessageBoxButtons), CType((Nevron.Nov.UI.ENMessageBoxIcon.Question), Nevron.Nov.UI.ENMessageBoxIcon)).[Then](Sub(ByVal result As Nevron.Nov.UI.ENWindowResult)
                                                                                                                                                                                                                                                                                                                                                                  If result = Nevron.Nov.UI.ENWindowResult.Yes Then
                                                                                                                                                                                                                                                                                                                                                                      Dim item As Nevron.Nov.UI.NTreeViewItem = Me.m_TreeView.SelectedItem
                                                                                                                                                                                                                                                                                                                                                                      Dim parentItem As Nevron.Nov.UI.NTreeViewItem = item.ParentItem
                                                                                                                                                                                                                                                                                                                                                                      Me.m_TreeView.SelectedItem = Nothing
                                                                                                                                                                                                                                                                                                                                                                      parentItem.Items.Remove(item)
                                                                                                                                                                                                                                                                                                                                                                      Me.m_TreeView.SelectedItem = parentItem

                                                                                                                                                                                                                                                                                                                                                                      If Me.DocumentItem.Items.Count = 0 Then
                                                                                                                                                                                                                                                                                                                                                                          Me.m_SerializeButton.Enabled = False
                                                                                                                                                                                                                                                                                                                                                                      End If
                                                                                                                                                                                                                                                                                                                                                                  End If
                                                                                                                                                                                                                                                                                                                                                              End Sub)
        End Sub

        Private Sub OnAddAttributeButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim dialog As Nevron.Nov.UI.NTopLevelWindow = Nevron.Nov.NApplication.CreateTopLevelWindow()
            dialog.SetupDialogWindow("Enter attribute's name and value", False)
            Dim table As Nevron.Nov.UI.NTableFlowPanel = New Nevron.Nov.UI.NTableFlowPanel()
            table.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            table.ColFillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            table.ColFitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            table.MaxOrdinal = 2
            Dim nameLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Name:")
            table.Add(nameLabel)
            Dim nameTextBox As Nevron.Nov.UI.NTextBox = New Nevron.Nov.UI.NTextBox()
            table.Add(nameTextBox)
            Dim valueLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Value:")
            table.Add(valueLabel)
            Dim valueTextBox As Nevron.Nov.UI.NTextBox = New Nevron.Nov.UI.NTextBox()
            table.Add(valueTextBox)
            table.Add(New Nevron.Nov.UI.NWidget())
            Dim buttonStrip As Nevron.Nov.UI.NButtonStrip = New Nevron.Nov.UI.NButtonStrip()
            buttonStrip.AddOKCancelButtons()
            table.Add(buttonStrip)
            dialog.Content = table
            AddHandler dialog.Opened, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) nameTextBox.Focus()
            AddHandler dialog.Closed, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs)
                                          If dialog.Result = Nevron.Nov.UI.ENWindowResult.OK Then
                                              Dim elementInfo As Nevron.Nov.Examples.Framework.NXmlSerializingExample.NElementInfo = CType(Me.m_TreeView.SelectedItem.Tag, Nevron.Nov.Examples.Framework.NXmlSerializingExample.NElementInfo)
                                              elementInfo.Attributes.[Set](nameTextBox.Text, valueTextBox.Text)
                                              Call Nevron.Nov.Examples.Framework.NXmlSerializingExample.UpdateTreeViewItemText(Me.m_TreeView.SelectedItem)

                                              If Me.m_RemoveAttributeButton.Enabled = False Then
                                                  Me.m_RemoveAttributeButton.Enabled = True
                                              End If
                                          End If
                                      End Sub

            dialog.Open()
        End Sub

        Private Sub OnRemoveAttributeButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim dialog As Nevron.Nov.UI.NTopLevelWindow = Nevron.Nov.NApplication.CreateTopLevelWindow()
            dialog.SetupDialogWindow("Select an Attribute to Remove", False)
            Dim listBox As Nevron.Nov.UI.NListBox = New Nevron.Nov.UI.NListBox()
            Dim elementInfo As Nevron.Nov.Examples.Framework.NXmlSerializingExample.NElementInfo = CType(Me.m_TreeView.SelectedItem.Tag, Nevron.Nov.Examples.Framework.NXmlSerializingExample.NElementInfo)
            Dim iter As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.NKeyValuePair(Of String, String)) = elementInfo.Attributes.GetIterator()

            While iter.MoveNext()
                listBox.Items.Add(New Nevron.Nov.UI.NListBoxItem(iter.Current.Key))
            End While

            Dim buttonStrip As Nevron.Nov.UI.NButtonStrip = New Nevron.Nov.UI.NButtonStrip()
            buttonStrip.AddOKCancelButtons()
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(listBox, buttonStrip, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2)
            pairBox.Spacing = Nevron.Nov.NDesign.VerticalSpacing
            dialog.Content = pairBox
            AddHandler dialog.Opened, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) listBox.Focus()
            AddHandler dialog.Closed, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs)
                                          If dialog.Result = Nevron.Nov.UI.ENWindowResult.OK Then
					' Remove the selected attribute
					Dim selectedItem As Nevron.Nov.UI.NListBoxItem = listBox.Selection.FirstSelected

                                              If selectedItem IsNot Nothing Then
                                                  Dim name As String = CType(selectedItem.Content, Nevron.Nov.UI.NLabel).Text
                                                  elementInfo.Attributes.Remove(name)
                                                  Call Nevron.Nov.Examples.Framework.NXmlSerializingExample.UpdateTreeViewItemText(Me.m_TreeView.SelectedItem)

                                                  If elementInfo.Attributes.Count = 0 Then
                                                      Me.m_RemoveAttributeButton.Enabled = False
                                                  End If
                                              End If
                                          End If
                                      End Sub

            dialog.Open()
        End Sub

        Private Sub OnSerializeButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' Create the XML document
			Dim document As Nevron.Nov.Xml.NXmlDocument = New Nevron.Nov.Xml.NXmlDocument()
            Dim documentItem As Nevron.Nov.UI.NTreeViewItem = Me.DocumentItem
            Dim i As Integer = 0, childCount As Integer = documentItem.Items.Count

            While i < childCount
                document.AddChild(Nevron.Nov.Examples.Framework.NXmlSerializingExample.SerializeTreeViewItem(documentItem.Items(i)))
                i += 1
            End While

			' Serialize the document to the XML text box
			Using stream As System.IO.MemoryStream = New System.IO.MemoryStream()
				' Serialize the document to a memory stream
				document.SaveToStream(stream, Nevron.Nov.Text.NEncoding.UTF8)

				' Populate the XML text box from the memory stream
				Dim data As Byte() = stream.ToArray()
                Me.m_XmlTextBox.Text = Nevron.Nov.Text.NEncoding.UTF8.GetString(data)
            End Using
        End Sub

        Private Sub OnTreeViewSelectedPathChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim selectItem As Nevron.Nov.UI.NTreeViewItem = Me.m_TreeView.SelectedItem

            If selectItem Is Nothing Then
                Me.m_AddChildItemButton.Enabled = False
                Me.m_RemoveSelectedItemButton.Enabled = False
                Me.m_AddAttributeButton.Enabled = False
                Me.m_RemoveAttributeButton.Enabled = False
                Return
            End If

            Me.m_AddChildItemButton.Enabled = True
            Me.m_RemoveSelectedItemButton.Enabled = selectItem IsNot Me.DocumentItem
            Dim elementInfo As Nevron.Nov.Examples.Framework.NXmlSerializingExample.NElementInfo = TryCast(selectItem.Tag, Nevron.Nov.Examples.Framework.NXmlSerializingExample.NElementInfo)
            Me.m_AddAttributeButton.Enabled = elementInfo IsNot Nothing
            Me.m_RemoveAttributeButton.Enabled = elementInfo IsNot Nothing AndAlso elementInfo.Attributes.Count > 0
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_TreeView As Nevron.Nov.UI.NTreeView
        Private m_XmlTextBox As Nevron.Nov.UI.NTextBox
        Private m_AddChildItemButton As Nevron.Nov.UI.NButton
        Private m_RemoveSelectedItemButton As Nevron.Nov.UI.NButton
        Private m_AddAttributeButton As Nevron.Nov.UI.NButton
        Private m_RemoveAttributeButton As Nevron.Nov.UI.NButton
        Private m_SerializeButton As Nevron.Nov.UI.NButton

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NXmlSerializingExample.
		''' </summary>
		Public Shared ReadOnly NXmlSerializingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateButton(ByVal image As Nevron.Nov.Graphics.NImage, ByVal text As String) As Nevron.Nov.UI.NButton
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(image, text)
            pairBox.Box1.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            pairBox.Box2.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            pairBox.Spacing = Nevron.Nov.NDesign.VerticalSpacing
            Dim button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton(pairBox)
            button.Enabled = False
            Return button
        End Function

        Private Shared Function CreateTreeViewItem(ByVal name As String) As Nevron.Nov.UI.NTreeViewItem
            Return Nevron.Nov.Examples.Framework.NXmlSerializingExample.CreateTreeViewItem(name, Nothing)
        End Function

        Private Shared Function CreateTreeViewItem(ByVal name As String, ByVal value As String) As Nevron.Nov.UI.NTreeViewItem
            Dim item As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem(name)
            item.Tag = New Nevron.Nov.Examples.Framework.NXmlSerializingExample.NElementInfo(name)

            If Not Equals(value, Nothing) Then
                item.Items.Add(New Nevron.Nov.UI.NTreeViewItem(value))
            End If

            Return item
        End Function

        Private Shared Function SerializeTreeViewItem(ByVal item As Nevron.Nov.UI.NTreeViewItem) As Nevron.Nov.Xml.NXmlNode
            Dim elementInfo As Nevron.Nov.Examples.Framework.NXmlSerializingExample.NElementInfo = CType(item.Tag, Nevron.Nov.Examples.Framework.NXmlSerializingExample.NElementInfo)

            If elementInfo Is Nothing Then
                Dim text As String = CType(item.Header.Content, Nevron.Nov.UI.NLabel).Text
                Return New Nevron.Nov.Xml.NXmlTextNode(Nevron.Nov.Xml.ENXmlNodeType.Text, text)
            End If

			' Create an XML element for the current tree view item
			Dim element As Nevron.Nov.Xml.NXmlElement = New Nevron.Nov.Xml.NXmlElement(elementInfo.Name)

            If elementInfo.Attributes.Count > 0 Then
				' Set the element's attributes
				Dim iter As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.NKeyValuePair(Of String, String)) = elementInfo.Attributes.GetIterator()

                While iter.MoveNext()
                    element.SetAttribute(iter.Current.Key, iter.Current.Value)
                End While
            End If

			' Loop through the item's children
			Dim i As Integer = 0, childCount As Integer = item.Items.Count

            While i < childCount
                element.AddChild(Nevron.Nov.Examples.Framework.NXmlSerializingExample.SerializeTreeViewItem(item.Items(i)))
                i += 1
            End While

            Return element
        End Function

        Private Shared Sub UpdateTreeViewItemText(ByVal item As Nevron.Nov.UI.NTreeViewItem)
            Dim elementInfo As Nevron.Nov.Examples.Framework.NXmlSerializingExample.NElementInfo = TryCast(item.Tag, Nevron.Nov.Examples.Framework.NXmlSerializingExample.NElementInfo)
            If elementInfo Is Nothing Then Return
            Dim text As String = elementInfo.Name

            If elementInfo.Attributes.Count > 0 Then
				' Iterate through the attributes and append them to the text
				Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder(text)
                Dim iter As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.NKeyValuePair(Of String, String)) = elementInfo.Attributes.GetIterator()

                While iter.MoveNext()
                    sb.Append(" ")
                    sb.Append(iter.Current.Key)
                    sb.Append("=""")
                    sb.Append(iter.Current.Value)
                    sb.Append("""")
                End While

                text = sb.ToString()
            End If

			' Update the text of the given tree view item
			CType(item.Header.Content, Nevron.Nov.UI.NLabel).Text = text
        End Sub

        Private Shared Function CreateTreeView() As Nevron.Nov.UI.NTreeView
            Dim treeView As Nevron.Nov.UI.NTreeView = New Nevron.Nov.UI.NTreeView()
            Dim root As Nevron.Nov.UI.NTreeViewItem = Nevron.Nov.Examples.Framework.NXmlSerializingExample.CreateTreeViewItem("Document")
            root.Expanded = True
            treeView.Items.Add(root)
            Dim book1 As Nevron.Nov.UI.NTreeViewItem = Nevron.Nov.Examples.Framework.NXmlSerializingExample.CreateTreeViewItem("book")
            book1.Expanded = True
            book1.Items.Add(Nevron.Nov.Examples.Framework.NXmlSerializingExample.CreateTreeViewItem("Author", "Gambardella, Matthew"))
            book1.Items.Add(Nevron.Nov.Examples.Framework.NXmlSerializingExample.CreateTreeViewItem("Title", "XML Developer's Guide"))
            root.Items.Add(book1)
            Dim book2 As Nevron.Nov.UI.NTreeViewItem = Nevron.Nov.Examples.Framework.NXmlSerializingExample.CreateTreeViewItem("book")
            book2.Expanded = True
            book2.Items.Add(Nevron.Nov.Examples.Framework.NXmlSerializingExample.CreateTreeViewItem("Author", "O'Brien, Tim"))
            book2.Items.Add(Nevron.Nov.Examples.Framework.NXmlSerializingExample.CreateTreeViewItem("Title", "MSXML3: A Comprehensive Guide"))
            root.Items.Add(book2)
            Return treeView
        End Function

		#EndRegion

		#Region"Nested Types"

		Private Class NElementInfo
            Public Sub New(ByVal name As String)
                Me.Name = name
                Me.Attributes = New Nevron.Nov.DataStructures.NMap(Of String, String)()
            End Sub

            Public Name As String
            Public Attributes As Nevron.Nov.DataStructures.NMap(Of String, String)
        End Class

		#EndRegion
	End Class
End Namespace
