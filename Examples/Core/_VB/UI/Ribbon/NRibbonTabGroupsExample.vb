Imports System.Globalization
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NRibbonTabGroupsExample
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
            Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.NRibbonTabGroupsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NRibbonTabGroupsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim ribbon As Nevron.Nov.UI.NRibbon = New Nevron.Nov.UI.NRibbon()
            ribbon.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top

			' The application menu
			ribbon.Tab.ApplicationMenu = Me.CreateMenu()

			' The "Home" page
			ribbon.Tab.TabPages.Add(Me.CreateHomePage())

			' The "Insert" page
			ribbon.Tab.TabPages.Add(Me.CreateInsertPage())

			' The "View" page
			Dim viewPage As Nevron.Nov.UI.NRibbonTabPage = New Nevron.Nov.UI.NRibbonTabPage("View")
            ribbon.Tab.TabPages.Add(viewPage)

			' Ribbon tab page groups
			ribbon.Tab.TabPageGroups = New Nevron.Nov.UI.NRibbonTabPageGroupCollection()
            ribbon.Tab.TabPageGroups.Add(Me.CreateTableTabPageGroup())
            ribbon.Tab.TabPageGroups.Add(Me.CreateImageTabPageGroup())

			' The help button
			ribbon.Tab.AdditionalContent = New Nevron.Nov.UI.NRibbonHelpButton()

			' The ribbon search box
			ribbon.Tab.SearchBox = New Nevron.Nov.UI.NRibbonSearchBox()
            ribbon.Tab.SearchBox.InitFromOwnerRibbon()

			' Subscribe to ribbon button Click events
			ribbon.AddEventHandler(Nevron.Nov.UI.NButtonBase.ClickEvent, New Nevron.Nov.Dom.NEventHandler(Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRibbonButtonClicked))
            Return ribbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            Dim tableCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox()
            tableCheckBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            tableCheckBox.Checked = True
            AddHandler tableCheckBox.CheckedChanged, AddressOf Me.OnTableCheckBoxCheckedChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Table group visible:", tableCheckBox))
            Dim tableColorBox As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            tableColorBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            tableColorBox.SelectedColor = Me.m_TableTabPageGroup.StripeFill.GetPrimaryColor()
            AddHandler tableColorBox.SelectedColorChanged, AddressOf Me.OnTableColorBoxSelectedColorChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Table group color:", tableColorBox))
            Dim imageCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox()
            imageCheckBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            imageCheckBox.Checked = True
            AddHandler imageCheckBox.CheckedChanged, AddressOf Me.OnImageCheckBoxCheckedChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Image group visible:", imageCheckBox))
            Dim imageColorBox As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            imageColorBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            imageColorBox.SelectedColor = Me.m_ImageTabPageGroup.StripeFill.GetPrimaryColor()
            AddHandler imageColorBox.SelectedColorChanged, AddressOf Me.OnImageColorBoxSelectedColorChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Image group color:", imageColorBox))
            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and configure ribbon tab groups. Ribbon tab groups are collections of ribbon tab pages
	grouped because they contain logically connected commands. Each ribbon tab group can have a specific stripe fill which is painted
	at the top of each tab page header in the group. This makes it easy to distinguish tabs from different groups. Tab groups are
	typically used to provide contextual ribbon tab functionallity.
</p>
<p>
	Use the controls on the right to show and hide ribbon tab groups and configure their appearance.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		#Region"Tab Pages"

		Private Function CreateMenu() As Nevron.Nov.UI.NApplicationMenu
            Dim appMenu As Nevron.Nov.UI.NApplicationMenu = New Nevron.Nov.UI.NApplicationMenu("File")
            Dim menu As Nevron.Nov.UI.NMenu = appMenu.MenuPane

			' Create the "Open" and "Save" menu items
			menu.Items.Add(New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Text.NResources.Image_Ribbon_32x32_folder_action_open_png, "Open"))
            menu.Items.Add(New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Text.NResources.Image_Ribbon_32x32_save_png, "Save"))

			' Create the "Save As" menu item and its sub items
			Dim saveAsMenuItem As Nevron.Nov.UI.NMenuItem = New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Text.NResources.Image_Ribbon_32x32_save_as_png, "Save As")
            saveAsMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("PNG Image"))
            saveAsMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("JPEG Image"))
            saveAsMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("BMP Image"))
            saveAsMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("GIF Image"))
            menu.Items.Add(saveAsMenuItem)

			' Create the rest of the menu items
			menu.Items.Add(New Nevron.Nov.UI.NMenuSeparator())
            menu.Items.Add(New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Text.NResources.Image_Ribbon_32x32_print_png, "Print"))
            menu.Items.Add(New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Text.NResources.Image_Ribbon_32x32_settings_png, "Options"))
            menu.Items.Add(New Nevron.Nov.UI.NMenuSeparator())
            menu.Items.Add(New Nevron.Nov.UI.NMenuItem(Nevron.Nov.Text.NResources.Image_Ribbon_32x32_exit_png, "Exit"))

			' Create a label for the content pane
			appMenu.ContentPane = New Nevron.Nov.UI.NLabel("This is the content pane")

			' Create 2 buttons for the footer pane
			appMenu.FooterPane = New Nevron.Nov.UI.NApplicationMenuFooterPanel()
            appMenu.FooterPane.Add(New Nevron.Nov.UI.NButton("Options..."))
            appMenu.FooterPane.Add(New Nevron.Nov.UI.NButton("Exit"))
            Return appMenu
        End Function

        Private Function CreateHomePage() As Nevron.Nov.UI.NRibbonTabPage
            Dim page As Nevron.Nov.UI.NRibbonTabPage = New Nevron.Nov.UI.NRibbonTabPage("Home")

			#Region"Clipboard Group"

			Dim group As Nevron.Nov.UI.NRibbonGroup = New Nevron.Nov.UI.NRibbonGroup("Clipboard")
            group.Icon = Nevron.Nov.Text.NResources.Image_Ribbon_16x16_clipboard_copy_png
            page.Groups.Add(group)
            Dim pasteSplitButton As Nevron.Nov.UI.NRibbonSplitButton = Nevron.Nov.UI.NRibbonSplitButton.CreateLarge("Paste", Nevron.Nov.Text.NResources.Image_Ribbon_32x32_clipboard_paste_png)
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
            collapsiblePanel.Add(New Nevron.Nov.UI.NRibbonButton("Cut", Nothing, Nevron.Nov.Text.NResources.Image_Ribbon_16x16_clipboard_cut_png))
            collapsiblePanel.Add(New Nevron.Nov.UI.NRibbonButton("Copy", Nothing, Nevron.Nov.Text.NResources.Image_Ribbon_16x16_clipboard_copy_png))
            collapsiblePanel.Add(New Nevron.Nov.UI.NRibbonButton("Format Painter", Nothing, Nevron.Nov.Text.NResources.Image_Ribbon_16x16_copy_format_png))

			#EndRegion

			#Region"Font Group"

			group = New Nevron.Nov.UI.NRibbonGroup("Font")
            group.Icon = Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_change_case_png
            page.Groups.Add(group)
            Dim wrapPanel As Nevron.Nov.UI.NRibbonWrapFlowPanel = New Nevron.Nov.UI.NRibbonWrapFlowPanel()
            wrapPanel.HorizontalSpacing = Nevron.Nov.NDesign.HorizontalSpacing
            group.Items.Add(wrapPanel)
            Dim stackPanel As Nevron.Nov.UI.NRibbonStackPanel = Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateStackPanel()
            wrapPanel.Add(stackPanel)
            Dim fontNameComboBox As Nevron.Nov.UI.NFontNameComboBox = New Nevron.Nov.UI.NFontNameComboBox()
            fontNameComboBox.SelectedIndex = 5
            stackPanel.Add(fontNameComboBox)
            Dim fontSizeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            Call Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.FillFontSizeCombo(fontSizeComboBox)
            fontSizeComboBox.SelectedIndex = 2
            stackPanel.Add(fontSizeComboBox)
            stackPanel = Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateStackPanel()
            stackPanel.Add(Nevron.Nov.UI.NRibbonButton.CreateSmall("Grow Font", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_font_grow_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonButton.CreateSmall("Shrink Font", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_font_shrink_png))
            stackPanel.Add(New Nevron.Nov.UI.NRibbonSeparator())
            wrapPanel.Add(stackPanel)
            Dim changeCaseMenu As Nevron.Nov.UI.NRibbonMenuDropDown = Nevron.Nov.UI.NRibbonMenuDropDown.CreateSmall("Change Case", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_change_case_png)
            changeCaseMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("lowercase"))
            changeCaseMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("UPPERCASE"))
            changeCaseMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Capitalize Each Word"))
            wrapPanel.Add(changeCaseMenu)
            stackPanel = Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateStackPanel()
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Bold", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_bold_small_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Italic", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_italic_small_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Underline", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_underline_small_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Strikethrough", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_strikethrough_small_png))
            Dim panel2 As Nevron.Nov.UI.NRibbonStackPanel = Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateStackPanel()
            panel2.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Subscript", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_subscript_small_png))
            panel2.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Superscript", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_superscript_small_png))
            stackPanel.Add(New Nevron.Nov.UI.NToggleButtonGroup(panel2))
            stackPanel.Add(New Nevron.Nov.UI.NRibbonSeparator())
            wrapPanel.Add(stackPanel)
            Dim fillSplitButton As Nevron.Nov.UI.NFillSplitButton = New Nevron.Nov.UI.NFillSplitButton()
            fillSplitButton.Image = Nevron.Nov.Text.NResources.Image_Ribbon_16x16_text_fill_png
            wrapPanel.Add(fillSplitButton)

			#EndRegion

			#Region"Paragraph Group"

			group = New Nevron.Nov.UI.NRibbonGroup("Paragraph")
            group.Icon = Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_align_center_png
            page.Groups.Add(group)
            wrapPanel = New Nevron.Nov.UI.NRibbonWrapFlowPanel()
            wrapPanel.HorizontalSpacing = Nevron.Nov.NDesign.HorizontalSpacing
            group.Items.Add(wrapPanel)
            stackPanel = Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateStackPanel()
            stackPanel.Add(Nevron.Nov.UI.NRibbonSplitButton.CreateSmall("Bullets", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_list_bullets_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonSplitButton.CreateSmall("Numbering", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_list_numbers_png))
            Dim multilevelListMenu As Nevron.Nov.UI.NRibbonMenuDropDown = Nevron.Nov.UI.NRibbonMenuDropDown.CreateSmall("Multilevel List", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_list_multilevel_png)
            multilevelListMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Alpha and Numeric"))
            multilevelListMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Alpha and Roman"))
            multilevelListMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Numeric and Roman"))
            stackPanel.Add(multilevelListMenu)
            stackPanel.Add(New Nevron.Nov.UI.NRibbonSeparator())
            wrapPanel.Add(stackPanel)
            stackPanel = Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateStackPanel()
            stackPanel.Add(Nevron.Nov.UI.NRibbonButton.CreateSmall("Decrease Indent", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_indent_left_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonButton.CreateSmall("Increase Indent", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_indent_right_png))
            stackPanel.Add(New Nevron.Nov.UI.NRibbonSeparator())
            wrapPanel.Add(stackPanel)
            stackPanel = Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateStackPanel()
            stackPanel.Add(Nevron.Nov.UI.NRibbonButton.CreateSmall("Sort", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_sort_az_png))
            stackPanel.Add(New Nevron.Nov.UI.NRibbonSeparator())
            stackPanel.Add(Nevron.Nov.UI.NRibbonButton.CreateSmall("Marks", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_marker_small_png))
            wrapPanel.Add(stackPanel)
            stackPanel = Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateStackPanel()
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Align Left", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_align_left_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Align Center", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_align_center_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Align Right", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_align_right_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Justify", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_align_justified_png))
            stackPanel.Add(New Nevron.Nov.UI.NRibbonSeparator())
            wrapPanel.Add(New Nevron.Nov.UI.NToggleButtonGroup(stackPanel))
            stackPanel = Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateStackPanel()
            Dim lineSpacingMenu As Nevron.Nov.UI.NRibbonMenuDropDown = Nevron.Nov.UI.NRibbonMenuDropDown.CreateSmall("Line Spacing", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_spacing_before_png)
            lineSpacingMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("1.0"))
            lineSpacingMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("1.15"))
            lineSpacingMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("1.5"))
            lineSpacingMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("2.0"))
            lineSpacingMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("3.0"))
            lineSpacingMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuSeparator())
            lineSpacingMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Line Spacing Options..."))
            stackPanel.Add(lineSpacingMenu)
            stackPanel.Add(New Nevron.Nov.UI.NRibbonSeparator())
            wrapPanel.Add(stackPanel)
            fillSplitButton = New Nevron.Nov.UI.NFillSplitButton()
            wrapPanel.Add(fillSplitButton)

			#EndRegion

			#Region"Text Styles Group"

			group = New Nevron.Nov.UI.NRibbonGroup("Text Styles")
            group.Icon = Nevron.Nov.Text.NResources.Image_Ribbon_16x16_text_fill_png
            page.Groups.Add(group)
            Dim gallery As Nevron.Nov.UI.NRibbonGallery = New Nevron.Nov.UI.NRibbonGallery("Text Style", Nevron.Nov.Text.NResources.Image_Ribbon_32x32_cover_page_png, New Nevron.Nov.Text.NTextStylePicker())
            group.Items.Add(gallery)

			#EndRegion

			Return page
        End Function

        Private Function CreateInsertPage() As Nevron.Nov.UI.NRibbonTabPage
            Dim page As Nevron.Nov.UI.NRibbonTabPage = New Nevron.Nov.UI.NRibbonTabPage("Insert")

			' The "Page" group
			Dim group As Nevron.Nov.UI.NRibbonGroup = New Nevron.Nov.UI.NRibbonGroup("Page")
            group.Icon = Nevron.Nov.Text.NResources.Image_Ribbon_16x16_cover_page_png
            Dim panel As Nevron.Nov.UI.NRibbonCollapsiblePanel = New Nevron.Nov.UI.NRibbonCollapsiblePanel()
            group.Items.Add(panel)
            Dim button As Nevron.Nov.UI.NRibbonButton = New Nevron.Nov.UI.NRibbonButton("Cover Page")
            button.LargeImage = Nevron.Nov.Text.NResources.Image_Ribbon_32x32_cover_page_png
            button.SmallImage = Nevron.Nov.Text.NResources.Image_Ribbon_16x16_cover_page_png
            panel.Add(button)
            Dim imageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_bold_small_png)
            button = New Nevron.Nov.UI.NRibbonButton("Blank Page")
            button.LargeImage = Nevron.Nov.Text.NResources.Image_Ribbon_32x32_page_png
            button.SmallImage = Nevron.Nov.Text.NResources.Image_Ribbon_16x16_page_png
            panel.Add(button)
            button = New Nevron.Nov.UI.NRibbonButton("Page Break")
            button.LargeImage = Nevron.Nov.Text.NResources.Image_Ribbon_32x32_page_break_png
            button.SmallImage = Nevron.Nov.Text.NResources.Image_Ribbon_16x16_page_break_png
            panel.Add(button)
            page.Groups.Add(group)
            Return page
        End Function

		#EndRegion

		#Region"Table Tab Page Group"

		Private Function CreateTableTabPageGroup() As Nevron.Nov.UI.NRibbonTabPageGroup
            Me.m_TableTabPageGroup = New Nevron.Nov.UI.NRibbonTabPageGroup("Table", Nevron.Nov.UI.ENRibbonStripeColor.Yellow)
            Me.m_TableTabPageGroup.TabPages.Add(Me.CreateTableDesignPage())
            Me.m_TableTabPageGroup.TabPages.Add(Me.CreateTableLayoutPage())
            Return Me.m_TableTabPageGroup
        End Function

        Private Function CreateTableDesignPage() As Nevron.Nov.UI.NRibbonTabPage
            Dim page As Nevron.Nov.UI.NRibbonTabPage = New Nevron.Nov.UI.NRibbonTabPage("Design")
            Dim group As Nevron.Nov.UI.NRibbonGroup = New Nevron.Nov.UI.NRibbonGroup("Table Styles")
            group.Icon = Nevron.Nov.Text.NResources.Image_Ribbon_16x16_table_design_png
            page.Groups.Add(group)
            Dim stylePicker As Nevron.Nov.Text.NTableStylePicker = New Nevron.Nov.Text.NTableStylePicker()
            Dim gallery As Nevron.Nov.UI.NRibbonGallery = New Nevron.Nov.UI.NRibbonGallery("Table Style", Nevron.Nov.Text.NResources.Image_Ribbon_32x32_table_design_png, stylePicker)
            gallery.ColumnCountStep = stylePicker.MaxNumberOfColumns
            gallery.MinimumPopupColumnCount = stylePicker.MaxNumberOfColumns
            gallery.PopupMenu = New Nevron.Nov.UI.NMenu()
            gallery.PopupMenu.Items.Add(New Nevron.Nov.UI.NMenuSeparator())
            gallery.PopupMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Modify Table Style..."))
            gallery.PopupMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("New Table Style..."))
            group.Items.Add(gallery)
            Return page
        End Function

        Private Function CreateTableLayoutPage() As Nevron.Nov.UI.NRibbonTabPage
            Dim page As Nevron.Nov.UI.NRibbonTabPage = New Nevron.Nov.UI.NRibbonTabPage("Layout")
            Dim group As Nevron.Nov.UI.NRibbonGroup = New Nevron.Nov.UI.NRibbonGroup("Table")
            group.Icon = Nevron.Nov.Text.NResources.Image_Ribbon_16x16_page_break_png
            page.Groups.Add(group)
            group.Items.Add(Nevron.Nov.UI.NRibbonButton.CreateLarge("Properties", Nevron.Nov.Text.NResources.Image_Ribbon_32x32_table_design_png))
            Return page
        End Function

		#EndRegion

		#Region"Image Tab Page Group"

		Private Function CreateImageTabPageGroup() As Nevron.Nov.UI.NRibbonTabPageGroup
            Me.m_ImageTabPageGroup = New Nevron.Nov.UI.NRibbonTabPageGroup("Image", Nevron.Nov.UI.ENRibbonStripeColor.Purple)
            Me.m_ImageTabPageGroup.TabPages.Add(Me.CreateImageEditPage())
            Me.m_ImageTabPageGroup.TabPages.Add(Me.CreateImageEffectsPage())
            Return Me.m_ImageTabPageGroup
        End Function

        Private Function CreateImageEditPage() As Nevron.Nov.UI.NRibbonTabPage
            Dim page As Nevron.Nov.UI.NRibbonTabPage = New Nevron.Nov.UI.NRibbonTabPage("Edit")
            Return page
        End Function

        Private Function CreateImageEffectsPage() As Nevron.Nov.UI.NRibbonTabPage
            Dim page As Nevron.Nov.UI.NRibbonTabPage = New Nevron.Nov.UI.NRibbonTabPage("Effects")
            Return page
        End Function

		#EndRegion

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnRibbonButtonClicked(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim button As Nevron.Nov.UI.INRibbonButton = TryCast(arg.TargetNode, Nevron.Nov.UI.INRibbonButton)

            If button IsNot Nothing Then
                Me.m_EventsLog.LogEvent(button.Text & " clicked")
            End If
        End Sub

        Private Sub OnTableCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim visible As Boolean = CBool(arg.NewValue)
            Me.m_TableTabPageGroup.Visibility = If(visible, Nevron.Nov.UI.ENVisibility.Visible, Nevron.Nov.UI.ENVisibility.Collapsed)
        End Sub

        Private Sub OnTableColorBoxSelectedColorChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim color As Nevron.Nov.Graphics.NColor = CType(arg.NewValue, Nevron.Nov.Graphics.NColor)

			' Set the stripe fill of the tab page headers in the tab group
			Me.m_TableTabPageGroup.StripeFill = New Nevron.Nov.Graphics.NColorFill(color)

			' Set a lighter color fill of the inactive tab page headers in the tab group
			Me.m_TableTabPageGroup.InactiveHeaderFill = New Nevron.Nov.Graphics.NColorFill(color.Lighten(0.9F))
        End Sub

        Private Sub OnImageCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim visible As Boolean = CBool(arg.NewValue)
            Me.m_ImageTabPageGroup.Visibility = If(visible, Nevron.Nov.UI.ENVisibility.Visible, Nevron.Nov.UI.ENVisibility.Collapsed)
        End Sub

        Private Sub OnImageColorBoxSelectedColorChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim color As Nevron.Nov.Graphics.NColor = CType(arg.NewValue, Nevron.Nov.Graphics.NColor)

			' Set the stripe fill of the tab page headers in the tab group
			Me.m_ImageTabPageGroup.StripeFill = New Nevron.Nov.Graphics.NColorFill(color)

			' Set a lighter color fill of the inactive tab page headers in the tab group
			Me.m_ImageTabPageGroup.InactiveHeaderFill = New Nevron.Nov.Graphics.NColorFill(color.Lighten(0.9F))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_TableTabPageGroup As Nevron.Nov.UI.NRibbonTabPageGroup
        Private m_ImageTabPageGroup As Nevron.Nov.UI.NRibbonTabPageGroup
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRibbonTabGroupsExample.
		''' </summary>
		Public Shared ReadOnly NRibbonTabGroupsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateStackPanel() As Nevron.Nov.UI.NRibbonStackPanel
            Dim stackPanel As Nevron.Nov.UI.NRibbonStackPanel = New Nevron.Nov.UI.NRibbonStackPanel()
            stackPanel.HorizontalSpacing = Nevron.Nov.NDesign.HorizontalSpacing
            Return stackPanel
        End Function
		''' <summary>
		''' Fills the given combo box with the commonly used font sizes.
		''' </summary>
		''' <returns></returns>
		Private Shared Sub FillFontSizeCombo(ByVal comboBox As Nevron.Nov.UI.NComboBox)
            Dim items As Nevron.Nov.UI.NComboBoxItemCollection = comboBox.Items
            items.Clear()
            Dim i As Integer = 8

            While i < 12
                items.Add(Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateFontSizeComboBoxItem(i))
                i += 1
            End While

            While i <= 28
                items.Add(Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateFontSizeComboBoxItem(i))
                i += 2
            End While

            items.Add(Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateFontSizeComboBoxItem(36))
            items.Add(Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateFontSizeComboBoxItem(48))
            items.Add(Nevron.Nov.Examples.UI.NRibbonTabGroupsExample.CreateFontSizeComboBoxItem(72))
        End Sub

        Private Shared Function CreateFontSizeComboBoxItem(ByVal fontSize As Integer) As Nevron.Nov.UI.NComboBoxItem
            Dim item As Nevron.Nov.UI.NComboBoxItem = New Nevron.Nov.UI.NComboBoxItem(fontSize.ToString(System.Globalization.CultureInfo.InvariantCulture))
            Return item
        End Function

		#EndRegion
	End Class
End Namespace
