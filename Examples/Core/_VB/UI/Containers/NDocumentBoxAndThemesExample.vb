Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
	#Region"Nested Types"

	''' <summary>
	''' A custom NOV theme based on the Windows 10 theme.
	''' </summary>
	Public Class NCustomTheme
        Inherits Nevron.Nov.UI.NWindows10Theme
		''' <summary>
		''' Static constructor.
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.UI.NCustomTheme.NCustomThemeSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NCustomTheme), Nevron.Nov.UI.NWindows10Theme.NWindows10ThemeSchema)
        End Sub

		''' <summary>
		''' Creates button styles.
		''' </summary>
		Protected Overrides Sub CreateButtonStyles(ByVal buttonSchema As Nevron.Nov.Dom.NSchema)
			' Change mouse over background fill to orange
			Dim mouseOverState As Nevron.Nov.UI.NColorSkinState = CType(MyBase.Skins.Button.GetState(Nevron.Nov.UI.NSkinState.MouseOver), Nevron.Nov.UI.NColorSkinState)
            mouseOverState.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Orange)

			' Change pressed button border to 3px purple border, background fill to dark red and text fill to white
			Dim pressedState As Nevron.Nov.UI.NColorSkinState = CType(MyBase.Skins.Button.GetState(Nevron.Nov.UI.NSkinState.Pressed), Nevron.Nov.UI.NColorSkinState)
            pressedState.SetBorderFill(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Green))
            pressedState.BorderThickness = New Nevron.Nov.Graphics.NMargins(3)
            pressedState.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkRed)
            pressedState.TextFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)

			' Call base to skin the buttons
			MyBase.CreateButtonStyles(buttonSchema)
        End Sub
		''' <summary>
		''' Creates flat button styles, which are buttons commonly in ribbon and toolbars.
		''' </summary>
		Protected Overrides Sub CreateFlatButtonStyles()
            Dim mouseOverState As Nevron.Nov.UI.NColorSkinState = CType(MyBase.Skins.FlatButton.GetState(Nevron.Nov.UI.NSkinState.MouseOver), Nevron.Nov.UI.NColorSkinState)
            mouseOverState.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Orange)

			' Call base to skin flat buttons
			MyBase.CreateFlatButtonStyles()
        End Sub
		''' <summary>
		''' Creates the tab styles. Overriden to make the mouse over state orange.
		''' </summary>
		Protected Overrides Sub CreateTabStyles()
			' Modify the tab skins
			Dim backgroundColor As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Orange
            Dim tabSkins As Nevron.Nov.UI.NSkin() = New Nevron.Nov.UI.NSkin() {MyBase.TabSkins.Top.FarTabPageHeaderSkin, MyBase.TabSkins.Top.InnerTabPageHeaderSkin, MyBase.TabSkins.Top.NearAndFarTabPageHeaderSkin, MyBase.TabSkins.Top.NearTabPageHeaderSkin}

            For i As Integer = 0 To tabSkins.Length - 1
                Dim state As Nevron.Nov.UI.NColorSkinState = CType(tabSkins(CInt((i))).GetState(Nevron.Nov.UI.NSkinState.MouseOver), Nevron.Nov.UI.NColorSkinState)
                state.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(backgroundColor)
            Next

			' Call base to skin the tab widget
			MyBase.CreateTabStyles()
        End Sub

		''' <summary>
		''' Schema associated with NCustomTheme.
		''' </summary>
		Public Shared ReadOnly NCustomThemeSchema As Nevron.Nov.Dom.NSchema
    End Class

	#EndRegion

	Public Class NDocumentBoxAndThemesExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NDocumentBoxAndThemesExample.NDocumentBoxAndThemesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NDocumentBoxAndThemesExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim table As Nevron.Nov.UI.NTableFlowPanel = New Nevron.Nov.UI.NTableFlowPanel()
            table.HorizontalSpacing = 10
            table.VerticalSpacing = 10
            table.MaxOrdinal = 3
            table.Add(Me.CreateGroupBox("Buttons", Me.CreateButtons()))
            table.Add(Me.CreateGroupBox("List Box", Me.CreateListBox()))
            table.Add(Me.CreateGroupBox("Tree View", Me.CreateTreeView()))
            table.Add(Me.CreateGroupBox("Drop Down Edits", Me.CreateDropDownEdits()))
            table.Add(Me.CreateGroupBox("Tab", Me.CreateTab()))
            table.Add(Me.CreateGroupBox("Range Based", Me.CreateRangeBased()))
            table.Add(Me.CreateGroupBox("Ribbon", Me.CreateRibbon()))
            table.Add(Me.CreateGroupBox("Command Bars", Me.CreateCommandBars()))
            table.Add(Me.CreateGroupBox("Windows", Me.CreateWindows()))

			' Create the document box
			Me.m_DocumentBox = New Nevron.Nov.UI.NDocumentBox()
            Me.m_DocumentBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_DocumentBox.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
            Me.m_DocumentBox.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            Me.m_DocumentBox.Surface = New Nevron.Nov.UI.NDocumentBoxSurface(table)
            Return Me.m_DocumentBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim enabledCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enabled", True)
            AddHandler enabledCheckBox.CheckedChanged, AddressOf Me.OnEnabledCheckBoxCheckedChanged
            stack.Add(enabledCheckBox)


			' Create the theme tree view
			Dim theme As Nevron.Nov.Dom.NTheme
            Dim rootItem, themeItem As Nevron.Nov.UI.NTreeViewItem
            Dim themeTreeView As Nevron.Nov.UI.NTreeView = New Nevron.Nov.UI.NTreeView()
            stack.Add(themeTreeView)

			'
			' Add the "Inherit Styles" root tree view item
			'
			rootItem = New Nevron.Nov.UI.NTreeViewItem("Inherit Styles")
            rootItem.Tag = "inherit"
            themeTreeView.Items.Add(rootItem)
            themeTreeView.SelectedItem = rootItem

			'
			' Add the part based UI themes to the tree view
			'
			rootItem = New Nevron.Nov.UI.NTreeViewItem("Part Based")
            rootItem.Expanded = True
            themeTreeView.Items.Add(rootItem)
            themeItem = New Nevron.Nov.UI.NTreeViewItem("Windows XP Blue")
            themeItem.Tag = New Nevron.Nov.UI.NWindowsXPBlueTheme()
            rootItem.Items.Add(themeItem)
            themeItem = New Nevron.Nov.UI.NTreeViewItem("Windows 7 Aero")
            themeItem.Tag = New Nevron.Nov.UI.NWindowsAeroTheme()
            rootItem.Items.Add(themeItem)
            themeItem = New Nevron.Nov.UI.NTreeViewItem("Windows 8")
            themeItem.Tag = New Nevron.Nov.UI.NWindows8Theme()
            rootItem.Items.Add(themeItem)
            themeItem = New Nevron.Nov.UI.NTreeViewItem("Windows 10")
            themeItem.Tag = New Nevron.Nov.UI.NWindows10Theme()
            rootItem.Items.Add(themeItem)
            themeItem = New Nevron.Nov.UI.NTreeViewItem("Mac OS X 10.7 Lion")
            themeItem.Tag = New Nevron.Nov.UI.NMacLionTheme()
            rootItem.Items.Add(themeItem)
            themeItem = New Nevron.Nov.UI.NTreeViewItem("Mac OS X 10.11 El Capitan")
            themeItem.Tag = New Nevron.Nov.UI.NMacElCapitanTheme()
            rootItem.Items.Add(themeItem)
            themeItem = New Nevron.Nov.UI.NTreeViewItem("Nevron Dark")
            themeItem.Tag = New Nevron.Nov.UI.NNevronDarkTheme()
            rootItem.Items.Add(themeItem)
            themeItem = New Nevron.Nov.UI.NTreeViewItem("Nevron Light")
            themeItem.Tag = New Nevron.Nov.UI.NNevronLightTheme()
            rootItem.Items.Add(themeItem)

			'
			' Add the windows classic UI themes to the tree view
			'
			rootItem = New Nevron.Nov.UI.NTreeViewItem("Windows Classic")
            rootItem.Expanded = True
            themeTreeView.Items.Add(rootItem)
            Dim windowsClassicSchemes As Nevron.Nov.UI.ENUIThemeScheme() = Nevron.Nov.NEnum.GetValues(Of Nevron.Nov.UI.ENUIThemeScheme)()
            Dim i As Integer = 0, count As Integer = windowsClassicSchemes.Length

            While i < count
                Dim scheme As Nevron.Nov.UI.ENUIThemeScheme = windowsClassicSchemes(i)
                theme = New Nevron.Nov.UI.NWindowsClassicTheme(scheme)
                themeItem = New Nevron.Nov.UI.NTreeViewItem(Nevron.Nov.NStringHelpers.InsertSpacesBeforeUppersAndDigits(scheme.ToString()))
                themeItem.Tag = theme
                rootItem.Items.Add(themeItem)
                i += 1
            End While

			'
			' Add the custom themes to the tree view
			'
			rootItem = New Nevron.Nov.UI.NTreeViewItem("Custom")
            rootItem.Expanded = True
            themeTreeView.Items.Add(rootItem)
            themeItem = New Nevron.Nov.UI.NTreeViewItem("Custom Theme")
            themeItem.Tag = New Nevron.Nov.Examples.UI.NCustomTheme()
            rootItem.Items.Add(themeItem)

			' Subscribe to the selected path changed event
			AddHandler themeTreeView.SelectedPathChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnThemeTreeViewSelectedPathChanged)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to use document boxes. The document box is a widget that has a document.
	If you set the <b>InheritStyleSheetsProperty</b> of the document box's document to false, then the widgets hosted
	in it will not inherit the styling of the document the document box is placed in.
</p>
<p>
	This example also demonstrates the UI themes incuded in Nevron Open Vision. Select a theme from the tree view on
	the right to apply it to the document box and see how the widgets look when that theme is applied.
</p>
<p>
	The last item in the tree view is a custom theme that changes the mouse over state of buttons, flat buttons (i.e.
	buttons in ribbon and toolbars) and tab page headers. The custom theme is defined in the beginning of the example's
	source code.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateButtons() As Nevron.Nov.UI.NStackPanel
			' Create buttons
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.VerticalSpacing = 10
            Dim checkBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Check Box")
            stack.Add(checkBox)
            Dim radioButton As Nevron.Nov.UI.NRadioButton = New Nevron.Nov.UI.NRadioButton("Radio Button")
            stack.Add(radioButton)
            Dim button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Button")
            stack.Add(button)
            Return stack
        End Function

        Private Function CreateListBox() As Nevron.Nov.UI.NListBox
            Dim listBox As Nevron.Nov.UI.NListBox = New Nevron.Nov.UI.NListBox()

            For i As Integer = 1 To 20
                listBox.Items.Add(New Nevron.Nov.UI.NListBoxItem("Item " & i))
            Next

            Return listBox
        End Function

        Private Function CreateTreeView() As Nevron.Nov.UI.NTreeView
            Dim treeView As Nevron.Nov.UI.NTreeView = New Nevron.Nov.UI.NTreeView()

            For i As Integer = 1 To 7
                Dim itemName As String = "Item " & i
                Dim item As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem(itemName)
                treeView.Items.Add(item)
                itemName += "."

                For j As Integer = 1 To 3
                    Dim childItem As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem(itemName & j)
                    item.Items.Add(childItem)
                Next
            Next

            Return treeView
        End Function

        Private Function CreateDropDownEdits() As Nevron.Nov.UI.NStackPanel
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.VerticalSpacing = 10
            stack.Add(Me.CreateComboBox())
            Dim colorBox As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            stack.Add(colorBox)
            Dim dateTimeBox As Nevron.Nov.UI.NDateTimeBox = New Nevron.Nov.UI.NDateTimeBox()
            stack.Add(dateTimeBox)
            Dim splitButton As Nevron.Nov.UI.NFillSplitButton = New Nevron.Nov.UI.NFillSplitButton()
            stack.Add(splitButton)
            Return stack
        End Function

        Private Function CreateComboBox() As Nevron.Nov.UI.NComboBox
            Dim comboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Item 1"))
            comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Item 2"))
            comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Item 3"))
            comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Item 4"))
            comboBox.SelectedIndex = 0
            Return comboBox
        End Function

        Private Function CreateTab() As Nevron.Nov.UI.NTab
            Dim tab As Nevron.Nov.UI.NTab = New Nevron.Nov.UI.NTab()
            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Page 1", "This is tab page 1"))
            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Page 2", "This is tab page 2"))
            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Page 3", "This is tab page 3"))
            Return tab
        End Function

        Private Function CreateRangeBased() As Nevron.Nov.UI.NStackPanel
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.VerticalSpacing = 10
            Dim numericUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            stack.Add(numericUpDown)
            Dim slider As Nevron.Nov.UI.NSlider = New Nevron.Nov.UI.NSlider()
            stack.Add(slider)
            Dim progressBar As Nevron.Nov.UI.NProgressBar = New Nevron.Nov.UI.NProgressBar()
            progressBar.PreferredHeight = 20
            progressBar.Value = 40
            stack.Add(progressBar)
            Return stack
        End Function

        Private Function CreateRibbon() As Nevron.Nov.UI.NRibbon
            Dim ribbon As Nevron.Nov.UI.NRibbon = New Nevron.Nov.UI.NRibbon()
            ribbon.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top

			'
			' Create the "Home" ribbon tab page
			'
			Dim pageHome As Nevron.Nov.UI.NRibbonTabPage = New Nevron.Nov.UI.NRibbonTabPage("Home")
            ribbon.Tab.TabPages.Add(pageHome)

			' Create the "Clipboard" group of the "Home" tab page
			Dim group As Nevron.Nov.UI.NRibbonGroup = New Nevron.Nov.UI.NRibbonGroup("Clipboard")
            group.Icon = NResources.Image_Ribbon_16x16_clipboard_copy_png
            pageHome.Groups.Add(group)
            Dim pasteSplitButton As Nevron.Nov.UI.NRibbonSplitButton = Nevron.Nov.UI.NRibbonSplitButton.CreateLarge("Paste", NResources.Image_Ribbon_32x32_clipboard_paste_png)
            pasteSplitButton.CollapseToMedium = Nevron.Nov.UI.ENCollapseCondition.Never
            pasteSplitButton.CollapseToSmall = Nevron.Nov.UI.ENCollapseCondition.Never
            Dim pasteMenu As Nevron.Nov.UI.NMenu = New Nevron.Nov.UI.NMenu()
            pasteMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Paste"))
            pasteMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Paste Special..."))
            pasteMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Paste as Link"))
            pasteSplitButton.Popup.Content = pasteMenu
            group.Items.Add(pasteSplitButton)
            Dim collapsiblePanel As Nevron.Nov.UI.NRibbonCollapsiblePanel = New Nevron.Nov.UI.NRibbonCollapsiblePanel()
            collapsiblePanel.InitialState = CInt(Nevron.Nov.UI.ENRibbonWidgetState.Medium)
            group.Items.Add(collapsiblePanel)
            collapsiblePanel.Add(New Nevron.Nov.UI.NRibbonButton("Cut", Nothing, NResources.Image_Ribbon_16x16_clipboard_cut_png))
            collapsiblePanel.Add(New Nevron.Nov.UI.NRibbonButton("Copy", Nothing, NResources.Image_Ribbon_16x16_clipboard_copy_png))
            collapsiblePanel.Add(New Nevron.Nov.UI.NRibbonButton("Format Painter", Nothing, NResources.Image_Ribbon_16x16_copy_format_png))

			' Create the "Format" group of the "Home" tab page
			group = New Nevron.Nov.UI.NRibbonGroup("Format")
            pageHome.Groups.Add(group)
            collapsiblePanel = New Nevron.Nov.UI.NRibbonCollapsiblePanel()
            collapsiblePanel.InitialState = CInt(Nevron.Nov.UI.ENRibbonWidgetState.Medium)
            group.Items.Add(collapsiblePanel)
            Dim fillSplitButton As Nevron.Nov.UI.NFillSplitButton = New Nevron.Nov.UI.NFillSplitButton()
            collapsiblePanel.Add(fillSplitButton)
            Dim strokeSplitButton As Nevron.Nov.UI.NStrokeSplitButton = New Nevron.Nov.UI.NStrokeSplitButton()
            collapsiblePanel.Add(strokeSplitButton)

			'
			' Add an "Insert" ribbon tab page
			'
			ribbon.Tab.TabPages.Add(New Nevron.Nov.UI.NRibbonTabPage("Insert"))
            Return ribbon
        End Function

        Private Function CreateCommandBars() As Nevron.Nov.UI.NCommandBarManager
            Dim manager As Nevron.Nov.UI.NCommandBarManager = New Nevron.Nov.UI.NCommandBarManager()

			' Create a menu bar in the first lane
			Dim lane1 As Nevron.Nov.UI.NCommandBarLane = New Nevron.Nov.UI.NCommandBarLane()
            manager.TopDock.Add(lane1)
            Dim menuBar As Nevron.Nov.UI.NMenuBar = New Nevron.Nov.UI.NMenuBar()
            menuBar.Pendant.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
            lane1.Add(menuBar)
            Dim fileMenu As Nevron.Nov.UI.NMenuDropDown = New Nevron.Nov.UI.NMenuDropDown("File")
            menuBar.Items.Add(fileMenu)
            Dim newMenuItem As Nevron.Nov.UI.NMenuItem = New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Presentation.NResources.Image_File_New_png, "New")
            fileMenu.Items.Add(newMenuItem)
            newMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("Project"))
            newMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("Web Site"))
            newMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("File"))
            fileMenu.Items.Add(New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Presentation.NResources.Image_File_Open_png, "Open"))
            fileMenu.Items.Add(New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Presentation.NResources.Image_File_Save_png, "Save"))
            fileMenu.Items.Add(New Nevron.Nov.UI.NMenuSeparator())
            fileMenu.Items.Add(New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Presentation.NResources.Image_File_Print_png, "Print"))
            Dim editMenu As Nevron.Nov.UI.NMenuDropDown = New Nevron.Nov.UI.NMenuDropDown("Edit")
            menuBar.Items.Add(editMenu)
            editMenu.Items.Add(New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Presentation.NResources.Image_Edit_Undo_png, "Undo"))
            editMenu.Items.Add(New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Presentation.NResources.Image_Edit_Redo_png, "Redo"))

			' Add a toolbar in the second lane
			Dim lane2 As Nevron.Nov.UI.NCommandBarLane = New Nevron.Nov.UI.NCommandBarLane()
            manager.TopDock.Add(lane2)
            Dim toolbar As Nevron.Nov.UI.NToolBar = New Nevron.Nov.UI.NToolBar()
            toolbar.Pendant.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
            lane2.Add(toolbar)
            toolbar.Items.Add(Nevron.Nov.UI.NButton.CreateImageAndText(Nevron.Nov.Presentation.NResources.Image_File_New_png, "New"))
            toolbar.Items.Add(Nevron.Nov.UI.NButton.CreateImageAndText(Nevron.Nov.Presentation.NResources.Image_File_Open_png, "Open"))
            toolbar.Items.Add(Nevron.Nov.UI.NButton.CreateImageAndText(Nevron.Nov.Presentation.NResources.Image_File_Save_png, "Save"))
            toolbar.Items.Add(New Nevron.Nov.UI.NCommandBarSeparator())
            toolbar.Items.Add(Nevron.Nov.UI.NButton.CreateImageAndText(Nevron.Nov.Presentation.NResources.Image_File_Print_png, "Print"))

			' Add a toolbar in the third lane
			Dim lane3 As Nevron.Nov.UI.NCommandBarLane = New Nevron.Nov.UI.NCommandBarLane()
            manager.TopDock.Add(lane3)
            toolbar = New Nevron.Nov.UI.NToolBar()
            toolbar.Pendant.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
            lane3.Add(toolbar)
            toolbar.Items.Add(Me.CreateComboBox())
            toolbar.Items.Add(New Nevron.Nov.UI.NCommandBarSeparator())
            toolbar.Items.Add(Nevron.Nov.UI.NToggleButton.CreateImageAndText(Nevron.Nov.Presentation.NResources.Image_FontStyle_Bold_png, "Bold"))
            toolbar.Items.Add(New Nevron.Nov.UI.NCheckBox("Plain Text"))
            Return manager
        End Function

        Private Function CreateWindows() As Nevron.Nov.UI.NStackPanel
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.VerticalSpacing = 10

			' create a button which shows a top-level window, added to the NDocumentBoxSurface.Windows collection.
			' such top-level windows will use the style sheets applied to the NDocumentBox.
			Dim windowButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Show Window...")
            AddHandler windowButton.Click, Sub(ByVal arg As Nevron.Nov.Dom.NEventArgs)
                                               Dim window As Nevron.Nov.UI.NTopLevelWindow = Nevron.Nov.NApplication.CreateTopLevelWindow(Me.m_DocumentBox.Surface)
                                               window.Title = "Top-Level Window"
                                               Dim windowStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                                               Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Top-level windows that are added to the NDocumentBoxSurface Windows collection," & Global.Microsoft.VisualBasic.Constants.vbCrLf & " will use the NDocumentBox styling.")
                                               windowStack.Add(label)
                                               Dim buttonStrip As Nevron.Nov.UI.NButtonStrip = New Nevron.Nov.UI.NButtonStrip()
                                               buttonStrip.AddCloseButton()
                                               windowStack.Add(buttonStrip)
                                               window.Content = windowStack
                                               window.Open()
                                           End Sub

            stack.Add(windowButton)

			' message box
			Dim showMessageBoxButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Show MessageBox...")
            AddHandler showMessageBoxButton.Click, Sub(ByVal arg As Nevron.Nov.Dom.NEventArgs)
                                                       Dim msgBoxSettings As Nevron.Nov.UI.NMessageBoxSettings = New Nevron.Nov.UI.NMessageBoxSettings("Message boxes that are added to the NDocumentBoxSurface Windows collection," & Global.Microsoft.VisualBasic.Constants.vbCrLf & " will use the NDocumentBox styling.", "Message box title")
                                                       msgBoxSettings.WindowsContainer = Me.m_DocumentBox.Surface
                                                       Call Nevron.Nov.UI.NMessageBox.Show(msgBoxSettings)
                                                   End Sub

            stack.Add(showMessageBoxButton)
            Return stack
        End Function

        Private Function CreateGroupBox(ByVal header As Object, ByVal content As Object) As Nevron.Nov.UI.NGroupBox
            Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox(header, content)

			' Check whether the application is in touch mode and set the size of the group box accordingly
			groupBox.PreferredSize = If(Nevron.Nov.NApplication.Desktop.TouchMode, New Nevron.Nov.Graphics.NSize(360, 250), New Nevron.Nov.Graphics.NSize(260, 180)) ' touch mode size
  ' regular mode size
            Return groupBox
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnEnabledCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_DocumentBox.Surface.Content.Enabled = CBool(arg.NewValue)
        End Sub

        Private Sub OnThemeTreeViewSelectedPathChanged(ByVal arg1 As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim treeView As Nevron.Nov.UI.NTreeView = CType(arg1.CurrentTargetNode, Nevron.Nov.UI.NTreeView)
            Dim selectedItem As Nevron.Nov.UI.NTreeViewItem = treeView.SelectedItem
            If selectedItem Is Nothing Then Return

            If TypeOf selectedItem.Tag Is Nevron.Nov.Dom.NTheme Then
				' Apply the selected theme to the document box's document
				Dim theme As Nevron.Nov.Dom.NTheme = CType(selectedItem.Tag, Nevron.Nov.Dom.NTheme)
                Me.m_DocumentBox.Document.InheritStyleSheets = False
                Me.m_DocumentBox.Document.StyleSheets.ApplyTheme(theme)
            ElseIf Nevron.Nov.NStringHelpers.Equals(selectedItem.Tag, "inherit") Then
				' Make the document inherit its style sheets and clear all current ones
				Me.m_DocumentBox.Document.InheritStyleSheets = True
                Me.m_DocumentBox.Document.StyleSheets.Clear()
            End If
        End Sub
		
		#EndRegion

		#Region"Fields"

		Private m_DocumentBox As Nevron.Nov.UI.NDocumentBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NDocumentBoxAndThemesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
