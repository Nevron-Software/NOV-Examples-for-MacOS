Imports System.Globalization
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NRibbonExample
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
            Nevron.Nov.Examples.UI.NRibbonExample.NRibbonExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NRibbonExample), NExampleBase.NExampleBaseSchema)
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
            Me.m_EventsLog = New NExampleEventsLog()
            Return Me.m_EventsLog
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and populate a ribbon. It also demonstrates how to create and initialize a ribbon search box.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

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
            Dim stackPanel As Nevron.Nov.UI.NRibbonStackPanel = Nevron.Nov.Examples.UI.NRibbonExample.CreateStackPanel()
            wrapPanel.Add(stackPanel)
            Dim fontNameComboBox As Nevron.Nov.UI.NFontNameComboBox = New Nevron.Nov.UI.NFontNameComboBox()
            fontNameComboBox.SelectedIndex = 5
            stackPanel.Add(fontNameComboBox)
            Dim fontSizeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            Call Nevron.Nov.Examples.UI.NRibbonExample.FillFontSizeCombo(fontSizeComboBox)
            fontSizeComboBox.SelectedIndex = 2
            stackPanel.Add(fontSizeComboBox)
            stackPanel = Nevron.Nov.Examples.UI.NRibbonExample.CreateStackPanel()
            stackPanel.Add(Nevron.Nov.UI.NRibbonButton.CreateSmall("Grow Font", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_font_grow_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonButton.CreateSmall("Shrink Font", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_font_shrink_png))
            stackPanel.Add(New Nevron.Nov.UI.NRibbonSeparator())
            wrapPanel.Add(stackPanel)
            Dim changeCaseMenu As Nevron.Nov.UI.NRibbonMenuDropDown = Nevron.Nov.UI.NRibbonMenuDropDown.CreateSmall("Change Case", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_change_case_png)
            changeCaseMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("lowercase"))
            changeCaseMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("UPPERCASE"))
            changeCaseMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Capitalize Each Word"))
            wrapPanel.Add(changeCaseMenu)
            stackPanel = Nevron.Nov.Examples.UI.NRibbonExample.CreateStackPanel()
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Bold", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_bold_small_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Italic", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_italic_small_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Underline", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_underline_small_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Strikethrough", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_character_strikethrough_small_png))
            Dim panel2 As Nevron.Nov.UI.NRibbonStackPanel = Nevron.Nov.Examples.UI.NRibbonExample.CreateStackPanel()
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
            stackPanel = Nevron.Nov.Examples.UI.NRibbonExample.CreateStackPanel()
            stackPanel.Add(Nevron.Nov.UI.NRibbonSplitButton.CreateSmall("Bullets", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_list_bullets_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonSplitButton.CreateSmall("Numbering", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_list_numbers_png))
            Dim multilevelListMenu As Nevron.Nov.UI.NRibbonMenuDropDown = Nevron.Nov.UI.NRibbonMenuDropDown.CreateSmall("Multilevel List", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_list_multilevel_png)
            multilevelListMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Alpha and Numeric"))
            multilevelListMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Alpha and Roman"))
            multilevelListMenu.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Numeric and Roman"))
            stackPanel.Add(multilevelListMenu)
            stackPanel.Add(New Nevron.Nov.UI.NRibbonSeparator())
            wrapPanel.Add(stackPanel)
            stackPanel = Nevron.Nov.Examples.UI.NRibbonExample.CreateStackPanel()
            stackPanel.Add(Nevron.Nov.UI.NRibbonButton.CreateSmall("Decrease Indent", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_indent_left_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonButton.CreateSmall("Increase Indent", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_indent_right_png))
            stackPanel.Add(New Nevron.Nov.UI.NRibbonSeparator())
            wrapPanel.Add(stackPanel)
            stackPanel = Nevron.Nov.Examples.UI.NRibbonExample.CreateStackPanel()
            stackPanel.Add(Nevron.Nov.UI.NRibbonButton.CreateSmall("Sort", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_sort_az_png))
            stackPanel.Add(New Nevron.Nov.UI.NRibbonSeparator())
            stackPanel.Add(Nevron.Nov.UI.NRibbonButton.CreateSmall("Marks", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_marker_small_png))
            wrapPanel.Add(stackPanel)
            stackPanel = Nevron.Nov.Examples.UI.NRibbonExample.CreateStackPanel()
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Align Left", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_align_left_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Align Center", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_align_center_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Align Right", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_align_right_png))
            stackPanel.Add(Nevron.Nov.UI.NRibbonToggleButton.CreateSmall("Justify", Nevron.Nov.Text.NResources.Image_Ribbon_16x16_paragraph_align_justified_png))
            stackPanel.Add(New Nevron.Nov.UI.NRibbonSeparator())
            wrapPanel.Add(New Nevron.Nov.UI.NToggleButtonGroup(stackPanel))
            stackPanel = Nevron.Nov.Examples.UI.NRibbonExample.CreateStackPanel()
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

			#Region"Styles Group"

			group = New Nevron.Nov.UI.NRibbonGroup("Styles")
            group.Icon = Nevron.Nov.Text.NResources.Image_Ribbon_16x16_table_design_png
            page.Groups.Add(group)
            Dim gallery As Nevron.Nov.UI.NRibbonGallery = New Nevron.Nov.UI.NRibbonGallery("Table Style", Nevron.Nov.Text.NResources.Image_Ribbon_32x32_table_design_png, New Nevron.Nov.Text.NTableStylePicker())
            gallery.MinimumPopupColumnCount = 7
            gallery.PopupMenu = New Nevron.Nov.UI.NMenu()
            gallery.PopupMenu.Items.Add(New Nevron.Nov.UI.NMenuSeparator())
            gallery.PopupMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Modify Table Style..."))
            gallery.PopupMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("New Table Style..."))
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

		#Region"Event Handlers"

		Private Sub OnRibbonButtonClicked(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim button As Nevron.Nov.UI.INRibbonButton = TryCast(arg.TargetNode, Nevron.Nov.UI.INRibbonButton)

            If button IsNot Nothing Then
                Me.m_EventsLog.LogEvent(button.Text & " clicked")
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRibbonExample.
		''' </summary>
		Public Shared ReadOnly NRibbonExampleSchema As Nevron.Nov.Dom.NSchema

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
                items.Add(Nevron.Nov.Examples.UI.NRibbonExample.CreateFontSizeComboBoxItem(i))
                i += 1
            End While

            While i <= 28
                items.Add(Nevron.Nov.Examples.UI.NRibbonExample.CreateFontSizeComboBoxItem(i))
                i += 2
            End While

            items.Add(Nevron.Nov.Examples.UI.NRibbonExample.CreateFontSizeComboBoxItem(36))
            items.Add(Nevron.Nov.Examples.UI.NRibbonExample.CreateFontSizeComboBoxItem(48))
            items.Add(Nevron.Nov.Examples.UI.NRibbonExample.CreateFontSizeComboBoxItem(72))
        End Sub

        Private Shared Function CreateFontSizeComboBoxItem(ByVal fontSize As Integer) As Nevron.Nov.UI.NComboBoxItem
            Dim item As Nevron.Nov.UI.NComboBoxItem = New Nevron.Nov.UI.NComboBoxItem(fontSize.ToString(System.Globalization.CultureInfo.InvariantCulture))
            Return item
        End Function

		#EndRegion
	End Class
End Namespace
