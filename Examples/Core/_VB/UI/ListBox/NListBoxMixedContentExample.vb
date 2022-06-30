Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NListBoxMixedContentExample
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
            Nevron.Nov.Examples.UI.NListBoxMixedContentExample.NListBoxMixedContentExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NListBoxMixedContentExample), NExampleBase.NExampleBaseSchema)

			' Properties
			Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ContentTypeProperty = Nevron.Nov.Examples.UI.NListBoxMixedContentExample.NListBoxMixedContentExampleSchema.AddSlot("ContentType", GetType(Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ENListBoxContentType), Nevron.Nov.Examples.UI.NListBoxMixedContentExample.defaultContentType)
            Call Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ContentTypeProperty.AddValueChangedCallback(Sub(ByVal t As Nevron.Nov.Dom.NNode, ByVal d As Nevron.Nov.Dom.NValueChangeData) CType(t, Nevron.Nov.Examples.UI.NListBoxMixedContentExample).OnContentTypeChanged(d))

			' Designer
			Call Nevron.Nov.Examples.UI.NListBoxMixedContentExample.NListBoxMixedContentExampleSchema.SetMetaUnit(New Nevron.Nov.Editors.NDesignerMetaUnit(GetType(Nevron.Nov.Examples.UI.NListBoxMixedContentExample.NListBoxMixedContentDesigner)))
        End Sub

		#EndRegion

		#Region"Properties"

		''' <summary>
		''' Gets or sets the value of the ContentType property.
		''' </summary>
		Public Property ContentType As Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ENListBoxContentType
            Get
                Return CType(GetValue(Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ContentTypeProperty), Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ENListBoxContentType)
            End Get
            Set(ByVal value As Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ENListBoxContentType)
                SetValue(Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ContentTypeProperty, value)
            End Set
        End Property

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a list box
			Me.m_ListBox = New Nevron.Nov.UI.NListBox()
            Me.m_ListBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_ListBox.PreferredSize = New Nevron.Nov.Graphics.NSize(200, 400)

			' Fill the image Box
			Me.FillWithImageCheckBoxAndTitle()

			' Hook to list box selection events
			AddHandler Me.m_ListBox.Selection.Selected, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))(AddressOf Me.OnListBoxItemSelected)
            AddHandler Me.m_ListBox.Selection.Deselected, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))(AddressOf Me.OnListBoxItemDeselected)
            Return Me.m_ListBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' Create the content type group box
			Dim contentTypePropertyEditor As Nevron.Nov.Editors.NPropertyEditor = Nevron.Nov.Editors.NDesigner.GetDesigner(Me).CreatePropertyEditor(Me, Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ContentTypeProperty)
            stack.Add(contentTypePropertyEditor)

			' Create the properties group box
			Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ListBox), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_ListBox, Nevron.Nov.UI.NWidget.EnabledProperty, Nevron.Nov.UI.NWidget.HorizontalPlacementProperty, Nevron.Nov.UI.NWidget.VerticalPlacementProperty, Nevron.Nov.UI.NScrollContentBase.HScrollModeProperty, Nevron.Nov.UI.NScrollContentBase.VScrollModeProperty, Nevron.Nov.UI.NScrollContentBase.NoScrollHAlignProperty, Nevron.Nov.UI.NScrollContentBase.NoScrollVAlignProperty, Nevron.Nov.UI.NListBox.IntegralVScrollProperty)
            Dim i As Integer = 0, count As Integer = editors.Count

            While i < count
                propertiesStack.Add(editors(i))
                i += 1
            End While

            Dim propertiesGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack))
            stack.Add(propertiesGroupBox)

			' Create the events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a list box and add items with various content to it - text only items, items with image and text,
	checkable items and so on. Using the controls to the right you can modify the appearance and the behavior of the list box.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateListBoxItem(ByVal index As Integer, ByVal hasCheckBox As Boolean, ByVal hasImage As Boolean) As Nevron.Nov.UI.NListBoxItem
            Dim text As String = "Item " & index.ToString()
            If hasCheckBox = False AndAlso hasImage = False Then Return New Nevron.Nov.UI.NListBoxItem(text)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            stack.HorizontalSpacing = 3

            If hasCheckBox Then
                Dim checkBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox()
                checkBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                stack.Add(checkBox)
            End If

            If hasImage Then
                Dim imageName As String = Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ImageNames(index Mod Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ImageNames.Length)
                Dim icon As Nevron.Nov.Graphics.NImage = New Nevron.Nov.Graphics.NImage(New Nevron.Nov.NEmbeddedResourceRef(NResources.Instance, "RIMG__16x16_" & imageName & "_png"))
                Dim imageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(icon)
                imageBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                imageBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                stack.Add(imageBox)
            End If

            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(text)
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            stack.Add(label)
            Return New Nevron.Nov.UI.NListBoxItem(stack)
        End Function

        Private Sub FillWithImageCheckBoxAndTitle()
            Me.m_ListBox.Items.Clear()

            For i As Integer = 0 To 100 - 1
                Dim index As Integer = (i Mod 32) / 8
                Dim checkBox As Boolean = index = 1 OrElse index = 3
                Dim image As Boolean = index = 2 OrElse index = 3
                Me.m_ListBox.Items.Add(Me.CreateListBoxItem(i, checkBox, image))
            Next
        End Sub

        Private Function CreateDetailedListBoxItem(ByVal index As Integer) As Nevron.Nov.UI.NListBoxItem
            Dim dock As Nevron.Nov.UI.NDockPanel = New Nevron.Nov.UI.NDockPanel()
            dock.HorizontalSpacing = 3
            dock.Padding = New Nevron.Nov.Graphics.NMargins(0, 2, 0, 2)

			' Add the image
			Dim imageName As String = Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ImageNames(index Mod Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ImageNames.Length)
            Dim icon As Nevron.Nov.Graphics.NImage = New Nevron.Nov.Graphics.NImage(New Nevron.Nov.NEmbeddedResourceRef(NResources.Instance, "RIMG__24x24_" & imageName & "_png"))
            Dim imageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(icon)
            imageBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            imageBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(imageBox, Nevron.Nov.Layout.ENDockArea.Left)
            dock.Add(imageBox)

			' Add the title
			Dim titleLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Item " & index.ToString())
            titleLabel.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 10, Nevron.Nov.Graphics.ENFontStyle.Bold)
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(titleLabel, Nevron.Nov.Layout.ENDockArea.Top)
            dock.AddChild(titleLabel)

			' Add the description
			Dim descriptionLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("This is item " & index.ToString() & "'s description.")
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(descriptionLabel, Nevron.Nov.Layout.ENDockArea.Center)
            dock.AddChild(descriptionLabel)
            Return New Nevron.Nov.UI.NListBoxItem(dock)
        End Function

        Private Sub FillWithImageTitleAndDetails()
            Me.m_ListBox.Items.Clear()

            For i As Integer = 0 To 100 - 1
                Me.m_ListBox.Items.Add(Me.CreateDetailedListBoxItem(i))
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnListBoxItemSelected(ByVal args As Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))
            Dim item As Nevron.Nov.UI.NListBoxItem = args.Item
            Dim index As Integer = item.GetAggregationInfo().Index
            Me.m_EventsLog.LogEvent("Selected Item: " & index.ToString())
        End Sub

        Private Sub OnListBoxItemDeselected(ByVal args As Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))
            Dim item As Nevron.Nov.UI.NListBoxItem = args.Item
            Dim index As Integer = item.GetAggregationInfo().Index
            Me.m_EventsLog.LogEvent("Deselected Item: " & index.ToString())
        End Sub
		''' <summary>
		''' Called when the ContentType property has changed.
		''' </summary>
		''' <paramname="data"></param>
		Private Sub OnContentTypeChanged(ByVal data As Nevron.Nov.Dom.NValueChangeData)
            Select Case CType(data.NewValue, Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ENListBoxContentType)
                Case Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ENListBoxContentType.ImageCheckBoxAndTitle
                    Me.FillWithImageCheckBoxAndTitle()
                Case Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ENListBoxContentType.ImageTitleAndDetails
                    Me.FillWithImageTitleAndDetails()
                Case Else
                    Throw New System.Exception("New ENListBoxContentType?")
            End Select
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ListBox As Nevron.Nov.UI.NListBox
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NListBoxMixedContentExample.
		''' </summary>
		Public Shared ReadOnly NListBoxMixedContentExampleSchema As Nevron.Nov.Dom.NSchema
		''' <summary>
		''' Reference to the ContentType property.
		''' </summary>
		Public Shared ReadOnly ContentTypeProperty As Nevron.Nov.Dom.NProperty

		#EndRegion

		#Region"Constants"

		Private Const defaultContentType As Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ENListBoxContentType = Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ENListBoxContentType.ImageCheckBoxAndTitle
        Private Shared ReadOnly ImageNames As String() = New String() {"Calendar", "Contacts", "Folders", "Journal", "Mail", "Notes", "Shortcuts", "Tasks"}

		#EndRegion

		#Region"Nested Types"

		Public Enum ENListBoxContentType
            ImageCheckBoxAndTitle
            ImageTitleAndDetails
        End Enum

		''' <summary>
		''' Designer for NListBoxMixedContent.
		''' </summary>
		Public Class NListBoxMixedContentDesigner
            Inherits Nevron.Nov.Editors.NDesigner
			''' <summary>
			''' Default constructor.
			''' </summary>
			Public Sub New()
                MyBase.SetPropertyEditor(Nevron.Nov.Examples.UI.NListBoxMixedContentExample.ContentTypeProperty, Nevron.Nov.Editors.NEnumPropertyEditor.VerticalRadioGroupTemplate)
            End Sub
        End Class

		#EndRegion
	End Class
End Namespace
