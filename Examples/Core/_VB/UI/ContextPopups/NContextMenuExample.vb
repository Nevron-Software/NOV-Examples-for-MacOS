Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NContextMenuExample
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
            Nevron.Nov.Examples.UI.NContextMenuExample.NContextMenuExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NContextMenuExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_TextChecked = New Boolean() {True, True, False}
            Me.m_ImageAndTextChecked = New Boolean() {True, False, True}
            Dim leftStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            leftStack.Add(Me.CreateWidget("Text Only", New Global.Nevron.Nov.Examples.UI.NContextMenuExample.CreateMenuDelegate(AddressOf Me.CreateTextContextMenu)))
            leftStack.Add(Me.CreateWidget("Image and Text", New Global.Nevron.Nov.Examples.UI.NContextMenuExample.CreateMenuDelegate(AddressOf Me.CreateImageAndTextContextMenu)))
            leftStack.Add(Me.CreateWidget("Checkable Text Only", New Global.Nevron.Nov.Examples.UI.NContextMenuExample.CreateMenuDelegate(AddressOf Me.CreateCheckableTextContextMenu)))
            leftStack.Add(Me.CreateWidget("Checkable Image And Text", New Global.Nevron.Nov.Examples.UI.NContextMenuExample.CreateMenuDelegate(AddressOf Me.CreateCheckableImageAndTextContextMenu)))
            Dim rightStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            rightStack.Add(Me.CreateWidget("Text Only", New Global.Nevron.Nov.Examples.UI.NContextMenuExample.CreateMenuDelegate(AddressOf Me.CreateTextContextMenu)))
            rightStack.Add(Me.CreateWidget("Image and Text", New Global.Nevron.Nov.Examples.UI.NContextMenuExample.CreateMenuDelegate(AddressOf Me.CreateImageAndTextContextMenu)))
            rightStack.Add(Me.CreateWidget("Checkable Text Only", New Global.Nevron.Nov.Examples.UI.NContextMenuExample.CreateMenuDelegate(AddressOf Me.CreateCheckableTextContextMenu)))
            rightStack.Add(Me.CreateWidget("Checkable Image And Text", New Global.Nevron.Nov.Examples.UI.NContextMenuExample.CreateMenuDelegate(AddressOf Me.CreateCheckableImageAndTextContextMenu)))
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            stack.HorizontalSpacing = 10
            stack.Add(New Nevron.Nov.UI.NGroupBox("Left Button Context Menu", leftStack))
            stack.Add(New Nevron.Nov.UI.NGroupBox("Right Button Context Menu", rightStack))
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create context menus. It creates several widgets and a context menu
	for each of them. The example shows how to create different types of menu items such as text only
	menu items, menu items with image and text, checkable text only menu items and checkable image and
	text menu items.
</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateWidget(ByVal text As String, ByVal createMenuDelegate As Nevron.Nov.Examples.UI.NContextMenuExample.CreateMenuDelegate) As Nevron.Nov.UI.NWidget
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(text)
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            label.TextFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black)
            Dim widget As Nevron.Nov.UI.NContentHolder = New Nevron.Nov.UI.NContentHolder(label)
            widget.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            widget.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            widget.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.PapayaWhip)
            widget.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            widget.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            widget.PreferredSize = New Nevron.Nov.Graphics.NSize(200, 100)
            widget.Tag = createMenuDelegate
            AddHandler widget.MouseDown, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NMouseButtonEventArgs)(AddressOf Me.OnTargetWidgetMouseDown)
            Return widget
        End Function

        Private Function CreateTextContextMenu() As Nevron.Nov.UI.NMenu
            Dim contextMenu As Nevron.Nov.UI.NMenu = New Nevron.Nov.UI.NMenu()

            For i As Integer = 0 To 3 - 1
                contextMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Option " & (i + 1).ToString()))
            Next

            Return contextMenu
        End Function

        Private Function CreateImageAndTextContextMenu() As Nevron.Nov.UI.NMenu
            Dim contextMenu As Nevron.Nov.UI.NMenu = New Nevron.Nov.UI.NMenu()

            For i As Integer = 0 To 3 - 1
                contextMenu.Items.Add(New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Examples.UI.NContextMenuExample.MenuItemImages(i), "Option " & (i + 1).ToString()))
            Next

            Return contextMenu
        End Function

        Private Function CreateCheckableTextContextMenu() As Nevron.Nov.UI.NMenu
            Dim contextMenu As Nevron.Nov.UI.NMenu = New Nevron.Nov.UI.NMenu()

            For i As Integer = 0 To 3 - 1
                Dim menuItem As Nevron.Nov.UI.NCheckableMenuItem = New Nevron.Nov.UI.NCheckableMenuItem(Nothing, "Option " & (i + 1).ToString(), Me.m_TextChecked(i))
                menuItem.Tag = i
                AddHandler menuItem.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnTextCheckableMenuItemCheckedChanged)
                contextMenu.Items.Add(menuItem)
            Next

            Return contextMenu
        End Function

        Private Function CreateCheckableImageAndTextContextMenu() As Nevron.Nov.UI.NMenu
            Dim contextMenu As Nevron.Nov.UI.NMenu = New Nevron.Nov.UI.NMenu()

            For i As Integer = 0 To 3 - 1
                Dim menuItem As Nevron.Nov.UI.NCheckableMenuItem = New Nevron.Nov.UI.NCheckableMenuItem(Nevron.Nov.Examples.UI.NContextMenuExample.MenuItemImages(i), "Option " & (i + 1).ToString(), Me.m_ImageAndTextChecked(i))
                menuItem.Tag = i
                AddHandler menuItem.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnImageAndTextCheckableMenuItemCheckedChanged)
                contextMenu.Items.Add(menuItem)
            Next

            Return contextMenu
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnTextCheckableMenuItemCheckedChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim index As Integer = CInt(args.CurrentTargetNode.Tag)
            Me.m_TextChecked(index) = CBool(args.NewValue)
        End Sub

        Private Sub OnImageAndTextCheckableMenuItemCheckedChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim index As Integer = CInt(args.CurrentTargetNode.Tag)
            Me.m_ImageAndTextChecked(index) = CBool(args.NewValue)
        End Sub

        Private Sub OnTargetWidgetMouseDown(ByVal args As Nevron.Nov.UI.NMouseButtonEventArgs)
            Dim ownerGroupBox As Nevron.Nov.UI.NGroupBox = CType(args.CurrentTargetNode.GetFirstAncestor(Nevron.Nov.UI.NGroupBox.NGroupBoxSchema), Nevron.Nov.UI.NGroupBox)
            Dim groupBoxTitle As String = CType(ownerGroupBox.Header.Content, Nevron.Nov.UI.NLabel).Text
            If (groupBoxTitle.StartsWith("Left") AndAlso args.Button <> Nevron.Nov.UI.ENMouseButtons.Left) OrElse (groupBoxTitle.StartsWith("Right") AndAlso args.Button <> Nevron.Nov.UI.ENMouseButtons.Right) Then Return

			' Mark the event as handled
			args.Cancel = True

			' Create and show the popup
			Dim createMenuDelegate As Nevron.Nov.Examples.UI.NContextMenuExample.CreateMenuDelegate = CType(args.CurrentTargetNode.Tag, Nevron.Nov.Examples.UI.NContextMenuExample.CreateMenuDelegate)
            Dim contextMenu As Nevron.Nov.UI.NMenu = createMenuDelegate()
            Call Nevron.Nov.UI.NPopupWindow.OpenInContext(New Nevron.Nov.UI.NPopupWindow(contextMenu), args.CurrentTargetNode, args.ScreenPosition)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_TextChecked As Boolean()
        Private m_ImageAndTextChecked As Boolean()

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NContextMenuExample.
		''' </summary>
		Public Shared ReadOnly NContextMenuExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly MenuItemImages As Nevron.Nov.Graphics.NImage() = New Nevron.Nov.Graphics.NImage() {NResources.Image__16x16_Calendar_png, NResources.Image__16x16_Contacts_png, NResources.Image__16x16_Mail_png}

		#EndRegion

		#Region"Nested Types"

		Private Delegate Function CreateMenuDelegate() As Nevron.Nov.UI.NMenu

		#EndRegion
	End Class
End Namespace
