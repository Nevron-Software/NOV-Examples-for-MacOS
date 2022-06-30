Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NCommandBarsExample
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
            Nevron.Nov.Examples.UI.NCommandBarsExample.NCommandBarsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NCommandBarsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"
		
		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim manager As Nevron.Nov.UI.NCommandBarManager = New Nevron.Nov.UI.NCommandBarManager()

			' create two lanes
			Dim lane0 As Nevron.Nov.UI.NCommandBarLane = New Nevron.Nov.UI.NCommandBarLane()
            manager.TopDock.Add(lane0)
            Dim lane1 As Nevron.Nov.UI.NCommandBarLane = New Nevron.Nov.UI.NCommandBarLane()
            manager.TopDock.Add(lane1)
            Dim lane2 As Nevron.Nov.UI.NCommandBarLane = New Nevron.Nov.UI.NCommandBarLane()
            manager.TopDock.Add(lane2)
            Dim lane3 As Nevron.Nov.UI.NCommandBarLane = New Nevron.Nov.UI.NCommandBarLane()
            manager.TopDock.Add(lane3)

			' create a menu bar in the first lane
			Dim menuBar As Nevron.Nov.UI.NMenuBar = New Nevron.Nov.UI.NMenuBar()
            lane0.Add(menuBar)
            menuBar.Items.Add(Me.CreateFileMenu())
            menuBar.Items.Add(Me.CreateEditMenu())
            menuBar.Items.Add(Me.CreateViewMenu())
            menuBar.Text = "Main Menu"

			'Create File toolbar.
			Dim fileToolBar As Nevron.Nov.UI.NToolBar = New Nevron.Nov.UI.NToolBar()
            lane1.Add(fileToolBar)
            fileToolBar.Text = "File"
            Me.AddToolBarItem(fileToolBar, Nevron.Nov.Presentation.NResources.Image_File_New_png, Nothing, "New")
            Me.AddToolBarItem(fileToolBar, Nevron.Nov.Presentation.NResources.Image_File_Open_png, Nothing, "Open")
            fileToolBar.Items.Add(New Nevron.Nov.UI.NCommandBarSeparator())
            Me.AddToolBarItem(fileToolBar, Nevron.Nov.Presentation.NResources.Image_File_Save_png, Nothing, "Save...")
            Me.AddToolBarItem(fileToolBar, Nevron.Nov.Presentation.NResources.Image_File_SaveAs_png, Nothing, "Save As...")
			
			'Create Edit toolbar.
			Dim editToolBar As Nevron.Nov.UI.NToolBar = New Nevron.Nov.UI.NToolBar()
            lane1.Add(editToolBar)
            editToolBar.Text = "Edit"
            Me.AddToolBarItem(editToolBar, Nevron.Nov.Presentation.NResources.Image_Edit_Undo_png, "Undo")
            Me.AddToolBarItem(editToolBar, Nevron.Nov.Presentation.NResources.Image_Edit_Redo_png, "Redo")
            editToolBar.Items.Add(New Nevron.Nov.UI.NCommandBarSeparator())
            Me.AddToolBarItem(editToolBar, Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png, "Copy")
            Me.AddToolBarItem(editToolBar, Nevron.Nov.Presentation.NResources.Image_Edit_Cut_png, "Cut")
            Me.AddToolBarItem(editToolBar, Nevron.Nov.Presentation.NResources.Image_Edit_Paste_png, "Paste")

			'Create View toolbar.
			Dim viewToolBar As Nevron.Nov.UI.NToolBar = New Nevron.Nov.UI.NToolBar()
            lane1.Add(viewToolBar)
            viewToolBar.Text = "View"

			'Add toggle buttons in a toggle button group which acts like radio buttons.
			Me.AddToggleToolBarItem(viewToolBar, Nevron.Nov.Text.NResources.Image_Layout_Normal_png, "Normal Layout")
            Me.AddToggleToolBarItem(viewToolBar, Nevron.Nov.Text.NResources.Image_Layout_Web_png, "Web Layout")
            Me.AddToggleToolBarItem(viewToolBar, Nevron.Nov.Text.NResources.Image_Layout_Print_png, "Print Layout")
            viewToolBar.Items.Add(New Nevron.Nov.UI.NCommandBarSeparator())
            Me.AddToolBarItem(viewToolBar, Nothing, "Task Pane")
            Me.AddToolBarItem(viewToolBar, Nothing, "Toolbars")
            Me.AddToolBarItem(viewToolBar, Nothing, "Ruller")
            Dim toolbar As Nevron.Nov.UI.NToolBar = New Nevron.Nov.UI.NToolBar()
            lane2.Add(toolbar)
            toolbar.Text = "Toolbar"
            toolbar.Wrappable = True
            Dim colorBoxItem As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            colorBoxItem.Tooltip = New Nevron.Nov.UI.NTooltip("Select Color")
            Call Nevron.Nov.UI.NCommandBar.SetText(colorBoxItem, "Select Color")
            toolbar.Items.Add(colorBoxItem)
            Dim splitButton As Nevron.Nov.UI.NMenuSplitButton = New Nevron.Nov.UI.NMenuSplitButton()
            splitButton.ActionButton.Content = Nevron.Nov.UI.NWidget.FromObject("Send/Receive")
            splitButton.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Send Receive All"))
            AddHandler splitButton.SelectedIndexChanged, AddressOf Me.OnSplitButtonSelectedIndexChanged
            splitButton.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Send All"))
            splitButton.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Receive All"))
            toolbar.Items.Add(splitButton)

			'Add toggle button which enable/disables the next fill split button.
			Dim toggleButton As Nevron.Nov.UI.NToggleButton = New Nevron.Nov.UI.NToggleButton("Enable")
            AddHandler toggleButton.CheckedChanged, AddressOf Me.OnToggleButtonCheckedChanged
            toolbar.Items.Add(toggleButton)

			' Add fill split button
			Dim fillButton As Nevron.Nov.UI.NFillSplitButton = New Nevron.Nov.UI.NFillSplitButton()
            fillButton.Tooltip = New Nevron.Nov.UI.NTooltip("Select Fill")
            fillButton.Enabled = False
            toolbar.Items.Add(fillButton)

			' Add shadow split button
			Dim shadowButton As Nevron.Nov.UI.NShadowSplitButton = New Nevron.Nov.UI.NShadowSplitButton()
            shadowButton.Tooltip = New Nevron.Nov.UI.NTooltip("Select Shadow")
            toolbar.Items.Add(shadowButton)

			' Add stroke split button
			Dim strokeButton As Nevron.Nov.UI.NStrokeSplitButton = New Nevron.Nov.UI.NStrokeSplitButton()
            strokeButton.Tooltip = New Nevron.Nov.UI.NTooltip("Select Stroke")
            toolbar.Items.Add(strokeButton)
            manager.Content = New Nevron.Nov.UI.NLabel("Content Goes Here")
            manager.Content.AllowFocus = True
            AddHandler manager.Content.MouseDown, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NMouseButtonEventArgs)(AddressOf Me.OnContentMouseDown)
            manager.Content.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            manager.Content.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            manager.Content.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            AddHandler manager.Content.GotFocus, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NFocusChangeEventArgs)(AddressOf Me.OnContentGotFocus)
            AddHandler manager.Content.LostFocus, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NFocusChangeEventArgs)(AddressOf Me.OnContentLostFocus)
            Return manager
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and populate various types of command bars such as menu bars and toolbars.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateFileMenu() As Nevron.Nov.UI.NMenuDropDown
            Dim file As Nevron.Nov.UI.NMenuDropDown = New Nevron.Nov.UI.NMenuDropDown("File")
            Dim newMenuItem As Nevron.Nov.UI.NMenuItem = Me.CreateMenuItem("New", Nevron.Nov.Presentation.NResources.Image_File_New_png)
            file.Items.Add(newMenuItem)
            newMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("Project"))
            newMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("Web Site"))
            newMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("File"))
            Dim openMenuItem As Nevron.Nov.UI.NMenuItem = Me.CreateMenuItem("Open", Nevron.Nov.Presentation.NResources.Image_File_Open_png)
            file.Items.Add(openMenuItem)
            openMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("Project"))
            openMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("Web Site"))
            openMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("File"))
            file.Items.Add(New Nevron.Nov.UI.NMenuSeparator())
            file.Items.Add(Me.CreateMenuItem("Save", Nevron.Nov.Presentation.NResources.Image_File_Save_png))
            file.Items.Add(Me.CreateMenuItem("Save As...", Nevron.Nov.Presentation.NResources.Image_File_SaveAs_png))
            Return file
        End Function

        Private Function CreateEditMenu() As Nevron.Nov.UI.NMenuDropDown
            Dim edit As Nevron.Nov.UI.NMenuDropDown = New Nevron.Nov.UI.NMenuDropDown("Edit")
            edit.Items.Add(Me.CreateMenuItem("Undo", Nevron.Nov.Presentation.NResources.Image_Edit_Undo_png))
            edit.Items.Add(Me.CreateMenuItem("Redo", Nevron.Nov.Presentation.NResources.Image_Edit_Redo_png))
            edit.Items.Add(New Nevron.Nov.UI.NMenuSeparator())
            edit.Items.Add(Me.CreateMenuItem("Cut", Nevron.Nov.Presentation.NResources.Image_Edit_Cut_png))
            edit.Items.Add(Me.CreateMenuItem("Copy", Nevron.Nov.Presentation.NResources.Image_Edit_Copy_png))
            edit.Items.Add(Me.CreateMenuItem("Paste", Nevron.Nov.Presentation.NResources.Image_Edit_Paste_png))
            Return edit
        End Function

        Private Function CreateViewMenu() As Nevron.Nov.UI.NMenuDropDown
            Dim view As Nevron.Nov.UI.NMenuDropDown = New Nevron.Nov.UI.NMenuDropDown("View")
            view.Items.Add(Me.CreateCheckableMenuItem("Normal", Nevron.Nov.Text.NResources.Image_Layout_Normal_png))
            view.Items.Add(Me.CreateCheckableMenuItem("Web Layout", Nevron.Nov.Text.NResources.Image_Layout_Web_png))
            view.Items.Add(Me.CreateCheckableMenuItem("Print Layout", Nevron.Nov.Text.NResources.Image_Layout_Print_png))
            view.Items.Add(New Nevron.Nov.UI.NMenuSeparator())
            view.Items.Add(New Nevron.Nov.UI.NMenuItem("Task Pane"))
            view.Items.Add(New Nevron.Nov.UI.NMenuItem("Toolbars"))
            view.Items.Add(New Nevron.Nov.UI.NMenuItem("Ruler"))
            Return view
        End Function

        Private Sub AddToggleToolBarItem(ByVal toolBar As Nevron.Nov.UI.NToolBar, ByVal image As Nevron.Nov.Graphics.NImage, ByVal tooltip As String)
            Dim item As Nevron.Nov.UI.NToggleButton = New Nevron.Nov.UI.NToggleButton(image)
            item.Tooltip = New Nevron.Nov.UI.NTooltip(tooltip)
            Call Nevron.Nov.UI.NCommandBar.SetText(item, tooltip)
            Call Nevron.Nov.UI.NCommandBar.SetImage(item, image)
            toolBar.Items.Add(item)
        End Sub

        Private Sub AddToolBarItem(ByVal toolBar As Nevron.Nov.UI.NToolBar, ByVal image As Nevron.Nov.Graphics.NImage)
            Me.AddToolBarItem(toolBar, image, Nothing)
        End Sub

        Private Sub AddToolBarItem(ByVal toolBar As Nevron.Nov.UI.NToolBar, ByVal image As Nevron.Nov.Graphics.NImage, ByVal text As String)
            Me.AddToolBarItem(toolBar, image, text, text)
        End Sub

        Private Sub AddToolBarItem(ByVal toolBar As Nevron.Nov.UI.NToolBar, ByVal image As Nevron.Nov.Graphics.NImage, ByVal text As String, ByVal tooltip As String)
            Dim item As Nevron.Nov.UI.NWidget

            If Equals(text, Nothing) Then
                text = String.Empty
            End If

            If image Is Nothing Then
                item = New Nevron.Nov.UI.NButton(text)
            Else
                item = New Nevron.Nov.UI.NButton(Nevron.Nov.UI.NPairBox.Create(image, text))
            End If

            If Not String.IsNullOrEmpty(tooltip) Then
                item.Tooltip = New Nevron.Nov.UI.NTooltip(tooltip)
            End If

            toolBar.Items.Add(item)
            Call Nevron.Nov.UI.NCommandBar.SetText(item, text)

            If image IsNot Nothing Then
                Call Nevron.Nov.UI.NCommandBar.SetImage(item, CType(image.DeepClone(), Nevron.Nov.Graphics.NImage))
            End If
        End Sub

        Private Function CreateMenuItem(ByVal text As String, ByVal image As Nevron.Nov.Graphics.NImage) As Nevron.Nov.UI.NMenuItem
            If image Is Nothing Then
                Return New Nevron.Nov.UI.NMenuItem(text)
            Else
                Dim imageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(image)
                imageBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                imageBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                Return New Nevron.Nov.UI.NMenuItem(imageBox, text)
            End If
        End Function

        Private Function CreateCheckableMenuItem(ByVal text As String, ByVal image As Nevron.Nov.Graphics.NImage) As Nevron.Nov.UI.NCheckableMenuItem
            Dim item As Nevron.Nov.UI.NCheckableMenuItem = New Nevron.Nov.UI.NCheckableMenuItem(text)
            AddHandler item.CheckedChanging, AddressOf Me.item_CheckedChanging
            AddHandler item.CheckedChanged, AddressOf Me.item_CheckedChanged

            If image IsNot Nothing Then
                Dim imageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(image)
                imageBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                imageBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                item.Header = imageBox
                item.Content = New Nevron.Nov.UI.NLabel(text)
            End If

            Return item
        End Function
				
		#EndRegion

		#Region"Event Handlers"

		Private Sub OnContentLostFocus(ByVal args As Nevron.Nov.UI.NFocusChangeEventArgs)
            TryCast(args.TargetNode, Nevron.Nov.UI.NLabel).Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
        End Sub

        Private Sub OnContentGotFocus(ByVal args As Nevron.Nov.UI.NFocusChangeEventArgs)
            TryCast(args.TargetNode, Nevron.Nov.UI.NLabel).Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
        End Sub

        Private Sub OnContentMouseDown(ByVal args As Nevron.Nov.UI.NMouseButtonEventArgs)
            TryCast(args.TargetNode, Nevron.Nov.UI.NLabel).Focus()
        End Sub

        Private Sub OnSplitButtonSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim menuSplitButton As Nevron.Nov.UI.NMenuSplitButton = CType(arg.CurrentTargetNode, Nevron.Nov.UI.NMenuSplitButton)
            Dim menuItem As Nevron.Nov.UI.NMenuItem = CType(menuSplitButton.Items(menuSplitButton.SelectedIndex), Nevron.Nov.UI.NMenuItem)
            Dim label As Nevron.Nov.UI.NLabel = CType(menuItem.Content, Nevron.Nov.UI.NLabel)
            menuSplitButton.ActionButton.Content = Nevron.Nov.UI.NWidget.FromObject(label.Text)
        End Sub

        Private Sub OnToggleButtonCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim button As Nevron.Nov.UI.NToggleButton = TryCast(arg.CurrentTargetNode, Nevron.Nov.UI.NToggleButton)

            If button Is Nothing Then
                Return
            End If

			'Get the toolbar items collection and enable or disable the next button in the collection
			Dim toolbarItems As Nevron.Nov.UI.NCommandBarItemCollection = TryCast(button.ParentNode, Nevron.Nov.UI.NCommandBarItemCollection)

            If toolbarItems Is Nothing Then
                Return
            End If

            Dim buttonIndex As Integer = toolbarItems.IndexOfChild(button)
            Dim fillSplitButton As Nevron.Nov.UI.NFillSplitButton = TryCast(toolbarItems(buttonIndex + 1), Nevron.Nov.UI.NFillSplitButton)

            If fillSplitButton Is Nothing Then
                Return
            End If

            If button.Checked Then
                fillSplitButton.Enabled = True
                CType(button.Content, Nevron.Nov.UI.NLabel).Text = "Disable"
            Else
                fillSplitButton.Enabled = False
                CType(button.Content, Nevron.Nov.UI.NLabel).Text = "Enable"
            End If
        End Sub

        Private Sub item_CheckedChanging(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim isChecked As Boolean = CBool(arg.NewValue)
            If isChecked Then Return

			' Make sure the user is not trying to uncheck the checked item
			Dim item As Nevron.Nov.UI.NCheckableMenuItem = CType(arg.TargetNode, Nevron.Nov.UI.NCheckableMenuItem)
            Dim items As Nevron.Nov.UI.NMenuItemCollection = TryCast(item.ParentNode, Nevron.Nov.UI.NMenuItemCollection)
            Dim i As Integer = 0, count As Integer = items.Count

            While i < count
                Dim currentItem As Nevron.Nov.UI.NCheckableMenuItem = TryCast(items(i), Nevron.Nov.UI.NCheckableMenuItem)

                If currentItem Is Nothing Then
                    Continue While
                End If

                If currentItem IsNot item AndAlso currentItem.Checked Then Return
                i += 1
            End While

            arg.Cancel = True
        End Sub

        Private Sub item_CheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim isChecked As Boolean = CBool(arg.NewValue)
            If isChecked = False Then Return
            Dim item As Nevron.Nov.UI.NCheckableMenuItem = CType(arg.TargetNode, Nevron.Nov.UI.NCheckableMenuItem)
            Dim items As Nevron.Nov.UI.NMenuItemCollection = TryCast(item.ParentNode, Nevron.Nov.UI.NMenuItemCollection)
            Dim i As Integer = 0, count As Integer = items.Count

            While i < count
                Dim currentItem As Nevron.Nov.UI.NCheckableMenuItem = TryCast(items(i), Nevron.Nov.UI.NCheckableMenuItem)

                If currentItem Is Nothing Then
                    Continue While
                End If

                If currentItem IsNot item AndAlso currentItem.Checked Then
					' We've found the previously checked item, so uncheck it
					currentItem.Checked = False
                    Exit While
                End If

                i += 1
            End While
        End Sub
		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCommandBarsExample.
		''' </summary>
		Public Shared ReadOnly NCommandBarsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
