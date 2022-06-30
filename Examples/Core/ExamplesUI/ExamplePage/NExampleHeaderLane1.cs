using System;

using Nevron.Nov.DataStructures;
using Nevron.Nov.Dom;
using Nevron.Nov.Editors;
using Nevron.Nov.Graphics;
using Nevron.Nov.Layout;
using Nevron.Nov.UI;
using Nevron.Nov.Xml;

namespace Nevron.Nov.Examples
{
	/// <summary>
	/// The first lane of the Example header.
	/// </summary>
	internal class NExampleHeaderLane1 : NToolBar
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NExampleHeaderLane1()
			: this(null)
		{
		}
		/// <summary>
		/// Initializing constructor.
		/// </summary>
		/// <param name="examplesWidgetMap"></param>
		public NExampleHeaderLane1(NStringMap<NWidget> examplesWidgetMap)
		{
			m_ExamplesMap = examplesWidgetMap;

			// Hide toolbar gripper and pendant
			Gripper.Visibility = ENVisibility.Collapsed;
			Pendant.Visibility = ENVisibility.Collapsed;

			// Configure toolbar
			Padding = new NMargins(NDesign.HorizontalSpacing, NDesign.VerticalSpacing);
			StackFillMode = ENStackFillMode.First;
			StackFitMode = ENStackFitMode.First;

			// Create toolbar items
			CreateItems();

			// Subscribe to the ExampleOptions Changed event
			NExamplesOptions.Instance.Changed += OnExampleOptionsChanged;
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NExampleHeaderLane1()
		{
			NExampleHeaderLane1Schema = NSchema.Create(typeof(NExampleHeaderLane1), NToolBarSchema);
		}

		#endregion

		#region Events

		/// <summary>
		/// Called when a favorite or a recent example menu item has been clicked.
		/// </summary>
		public event Function<NXmlElement> ExampleMenuItemClick;

		#endregion

		#region Implementation - UI

		private void CreateItems()
		{
			NImageBox logoImageBox = new NImageBox(NResources.Image_ExamplesUI_Logos_Nevron_emf);
			logoImageBox.PreferredSize = new NSize(179, 24);
			logoImageBox.HorizontalPlacement = ENHorizontalPlacement.Left;
			Items.Add(logoImageBox);

			m_FavoriteExamplesDropDown = new NMenuDropDown(NPairBox.Create(NResources.Image_ExamplesUI_Icons_Favorites_png, "Favorite Examples"));
			NStylePropertyEx.SetFlatExtendedLook(m_FavoriteExamplesDropDown);
			Items.Add(m_FavoriteExamplesDropDown);

			m_RecentExamplesDropDown = new NMenuDropDown(NPairBox.Create(Presentation.NResources.Image_File_RecentDocuments_png, "Recent Examples"));
			NStylePropertyEx.SetFlatExtendedLook(m_RecentExamplesDropDown);
			Items.Add(m_RecentExamplesDropDown);

			m_ThemesDropDown = CreateThemesMenuDropDown();
			Items.Add(m_ThemesDropDown);
			Items.Add(CreateOptionsMenuDropDown());

			#region InternalCode

			Items.Add(CreateDebugMenuDropDown());

			#endregion
		}

		#endregion

		#region Implementation - Themes and Options Menus

		/// <summary>
		/// Creates the menu drop down that allows the user to select a theme.
		/// </summary>
		/// <returns></returns>
		private NMenuDropDown CreateThemesMenuDropDown()
		{
			NMenuDropDown themesDropDown = new NMenuDropDown(NPairBox.Create(NResources.Image_ExampleIcons_UI_ThemeBuilder_png, "Theme"));
			NStylePropertyEx.SetFlatExtendedLook(themesDropDown);

			// Add the Windows Classic themes
			NMenuItem windowsClassicMenuItem = new NMenuItem("Windows Classic");
			themesDropDown.Items.Add(windowsClassicMenuItem);

			ENUIThemeScheme[] windowsClassicSchemes = NEnum.GetValues<ENUIThemeScheme>();
			for (int i = 0, count = windowsClassicSchemes.Length; i < count; i++)
			{
				ENUIThemeScheme scheme = (ENUIThemeScheme)windowsClassicSchemes.GetValue(i);
				NWindowsClassicTheme theme = new NWindowsClassicTheme(scheme);
				NCheckableMenuItem themeItem = new NCheckableMenuItem(NStringHelpers.InsertSpacesBeforeUppersAndDigits(scheme.ToString()));
				themeItem.Tag = theme;
				themeItem.Click += OnThemeMenuItemClick;
				windowsClassicMenuItem.Items.Add(themeItem);
			}

			// Add the touch themes
			NMenuItem touchThemesMenuItem = new NMenuItem("Touch Themes");
			themesDropDown.Items.Add(touchThemesMenuItem);

			// Add the Windows 8 touch theme 
			NCheckableMenuItem windows8ThemeMenuItemTouch = new NCheckableMenuItem("Windows 8 Touch");
			windows8ThemeMenuItemTouch.Tag = new NWindows8Theme(true);
			windows8ThemeMenuItemTouch.Click += OnThemeMenuItemClick;
			touchThemesMenuItem.Items.Add(windows8ThemeMenuItemTouch);

			// Add the dark theme
			NCheckableMenuItem darkThemeTouch = new NCheckableMenuItem("Nevron Dark Touch");
			darkThemeTouch.Tag = new NNevronDarkTheme(true);
			darkThemeTouch.Click += OnThemeMenuItemClick;
			touchThemesMenuItem.Items.Add(darkThemeTouch);

			// Add the light theme
			NCheckableMenuItem lightThemeTouch = new NCheckableMenuItem("Nevron Light Touch");
			lightThemeTouch.Tag = new NNevronLightTheme(true);
			lightThemeTouch.Click += OnThemeMenuItemClick;
			touchThemesMenuItem.Items.Add(lightThemeTouch);

			// Add the Windows XP Blue theme
			NCheckableMenuItem xpBlueMenuItem = new NCheckableMenuItem("Windows XP Blue");
			xpBlueMenuItem.Tag = new NWindowsXPBlueTheme();
			xpBlueMenuItem.Click += OnThemeMenuItemClick;
			themesDropDown.Items.Add(xpBlueMenuItem);

			// Add the Windows Aero theme
			NCheckableMenuItem aeroThemeMenuItem = new NCheckableMenuItem("Windows 7 Aero");
			aeroThemeMenuItem.Tag = new NWindowsAeroTheme();
			aeroThemeMenuItem.Click += OnThemeMenuItemClick;
			themesDropDown.Items.Add(aeroThemeMenuItem);

			// Add the Windows 8 theme
			NCheckableMenuItem windows8ThemeMenuItem = new NCheckableMenuItem("Windows 8");
			windows8ThemeMenuItem.Tag = new NWindows8Theme();
			windows8ThemeMenuItem.Click += OnThemeMenuItemClick;
			themesDropDown.Items.Add(windows8ThemeMenuItem);
			m_CurrentThemeMenuItem = windows8ThemeMenuItem;

			// Add the Windows 10 theme (the default theme)
			NCheckableMenuItem windows10ThemeMenuItem = new NCheckableMenuItem("Windows 10");
			windows10ThemeMenuItem.Tag = new NWindows10Theme();
			windows10ThemeMenuItem.Click += OnThemeMenuItemClick;
			themesDropDown.Items.Add(windows10ThemeMenuItem);
			m_CurrentThemeMenuItem = windows10ThemeMenuItem;

			// Add the Mac Lion theme
			NCheckableMenuItem macLionThemeMenuItem = new NCheckableMenuItem("Mac OS X Lion");
			macLionThemeMenuItem.Tag = new NMacLionTheme();
			macLionThemeMenuItem.Click += OnThemeMenuItemClick;
			themesDropDown.Items.Add(macLionThemeMenuItem);

			// Add the Mac El Capitan theme
			NCheckableMenuItem macElCapitanTheme = new NCheckableMenuItem("Mac OS X El Capitan");
			macElCapitanTheme.Tag = new NMacElCapitanTheme();
			macElCapitanTheme.Click += OnThemeMenuItemClick;
			themesDropDown.Items.Add(macElCapitanTheme);

			// Add the dark theme
			NCheckableMenuItem darkTheme = new NCheckableMenuItem("Nevron Dark");
			darkTheme.Tag = new NNevronDarkTheme();
			darkTheme.Click += OnThemeMenuItemClick;
			themesDropDown.Items.Add(darkTheme);

			// Add the light theme
			NCheckableMenuItem lightTheme = new NCheckableMenuItem("Nevron Light");
			lightTheme.Tag = new NNevronLightTheme();
			lightTheme.Click += OnThemeMenuItemClick;
			themesDropDown.Items.Add(lightTheme);

			// Check the default theme
			if (NApplication.IntegrationPlatform == ENIntegrationPlatform.XamarinMac)
			{
				macElCapitanTheme.Checked = true;
			}
			else
			{
				windows10ThemeMenuItem.Checked = true;
			}

			return themesDropDown;
		}
		/// <summary>
		/// Creates the Options drop down menu.
		/// </summary>
		/// <returns></returns>
		private NMenuDropDown CreateOptionsMenuDropDown()
		{
			NMenuDropDown optionsDropDown = new NMenuDropDown(NPairBox.Create(NResources.Image_ToolBar_16x16_Options_png, "Options"));
			NStylePropertyEx.SetFlatExtendedLook(optionsDropDown);

			m_DeveloperModeMenuItem = new NCheckableMenuItem("Developer Mode");
			m_DeveloperModeMenuItem.Click += OnDeveloperModeMenuItemClick;
			optionsDropDown.Items.Add(m_DeveloperModeMenuItem);

			return optionsDropDown;
		}

		/// <summary>
		/// Updates the selected theme from NOV Examples options.
		/// </summary>
		/// <param name="menuItems"></param>
		/// <returns></returns>
		private bool UpdateSelectedTheme(NMenuItemCollection menuItems)
		{
			ENUIThemeScheme themeScheme = NExamplesOptions.Instance.ThemeScheme;

			// Loop through the first level of menu items
			for (int i = 0; i < menuItems.Count; i++)
			{
				NCheckableMenuItem checkableMenuItem = menuItems[i] as NCheckableMenuItem;
				if (checkableMenuItem != null &&
					((NUITheme)checkableMenuItem.Tag).Scheme == themeScheme)
				{
					m_CurrentThemeMenuItem.Checked = false;
					m_CurrentThemeMenuItem = (NCheckableMenuItem)checkableMenuItem;
					m_CurrentThemeMenuItem.Checked = true;
					return true;
				}
			}

			// Loop through the sub items of menu items
			for (int i = 0; i < menuItems.Count; i++)
			{
				NMenuItem menuItem = menuItems[i] as NMenuItem;
				if (menuItem != null && menuItem.Items != null && menuItem.Items.Count > 0)
				{
					if (UpdateSelectedTheme(menuItem.Items))
						return true;
				}
			}

			NDebug.Assert(false, "New UI theme scheme?");
			return false;
		}

		#region InternalCode

		/// <summary>
		/// Creates the menu drop down that allows the user to perform certain actions related to application diagnostics.
		/// </summary>
		/// <returns></returns>
		private NMenuDropDown CreateDebugMenuDropDown()
		{
			NMenuDropDown debugDropDown = new NMenuDropDown("DEBUG");
			NStylePropertyEx.SetFlatExtendedLook(debugDropDown);

			NMenuItem printMenuItem = new NMenuItem("Print");
			printMenuItem.Click += new Function<NEventArgs>(OnPrintMenuItemClick);
			debugDropDown.Items.Add(printMenuItem);

			NMenuItem exportWithDirect2DMenuItem = new NMenuItem("Export with D2D");
			exportWithDirect2DMenuItem.Click += new Function<NEventArgs>(OnExportWithDirect2DMenuItemClick);
			debugDropDown.Items.Add(exportWithDirect2DMenuItem);

			debugDropDown.Items.Add(new NMenuSeparator());

			NMenuItem dumpBugLogMenuItem = new NMenuItem("DUMP BUG LOG");
			dumpBugLogMenuItem.Click += new Function<NEventArgs>(OnDumpBugLog);
			debugDropDown.Items.Add(dumpBugLogMenuItem);

			NMenuItem clearBugLogMenuItem = new NMenuItem("CLEAR BUG LOG");
			clearBugLogMenuItem.Click += new Function<NEventArgs>(OnClearBugLog);
			debugDropDown.Items.Add(clearBugLogMenuItem);

			NMenuItem gcCollectMenuItem = new NMenuItem("GC Collect");
			gcCollectMenuItem.Click += new Function<NEventArgs>(OnGCCollectMenuItemClick);
			debugDropDown.Items.Add(gcCollectMenuItem);

			NMenuItem showApplicationStyleSheets = new NMenuItem("Show Application StyleSheets");
			showApplicationStyleSheets.Click += new Function<NEventArgs>(OnShowApplicationStyleSheetsClick);
			debugDropDown.Items.Add(showApplicationStyleSheets);

			NCheckableMenuItem showRepaintAreasMenuCheck = new NCheckableMenuItem("Show Repaint Areas");
			showRepaintAreasMenuCheck.Checked = NApplication.ShowRepaintAreas;
			showRepaintAreasMenuCheck.CheckedChanged += new Function<NValueChangeEventArgs>(OnShowRepaintAreasMenuCheckCheckedChanged);
			debugDropDown.Items.Add(showRepaintAreasMenuCheck);

			NCheckableMenuItem showPaintCacheAreasMenuCheck = new NCheckableMenuItem("Show Paint Cache Areas");
			showPaintCacheAreasMenuCheck.Checked = NApplication.ShowRepaintAreas;
			showPaintCacheAreasMenuCheck.CheckedChanged += new Function<NValueChangeEventArgs>(OnShowPaintCacheAreasMenuCheckCheckedChanged);
			debugDropDown.Items.Add(showPaintCacheAreasMenuCheck);

			NCheckableMenuItem enablePaintCacheMenuCheck = new NCheckableMenuItem("Enable Paint Cache");
			enablePaintCacheMenuCheck.Checked = NApplication.PaintCacheSettings.Enabled;
			enablePaintCacheMenuCheck.CheckedChanged += new Function<NValueChangeEventArgs>(OnEnablePaintCacheMenuCheckCheckedChanged);
			debugDropDown.Items.Add(enablePaintCacheMenuCheck);

			NCheckableMenuItem enableMultiThreadedPainting = new NCheckableMenuItem("Enable Multi Threaded Painting");
			enableMultiThreadedPainting.Checked = NApplication.EnableMultiThreadedPainting;
			enableMultiThreadedPainting.CheckedChanged += new Function<NValueChangeEventArgs>(OnEnableMultiThreadedPaintingCheckedChanged);
			debugDropDown.Items.Add(enableMultiThreadedPainting);

			return debugDropDown;
		}

		#endregion

		#endregion

		#region Event Handlers - Favorite and Recent Examples

		private void OnExampleOptionsChanged(NEventArgs arg)
		{
			NValueChangeEventArgs changeArg = arg as NValueChangeEventArgs;
			if (changeArg == null)
				return;

			if (changeArg.Property == NExamplesOptions.RecentExamplesProperty)
			{
				// Update the Recent Examples menu drop down
				NExamplesUiHelpers.PopulateExamplesDropDown(
					m_RecentExamplesDropDown,
					NExamplesOptions.Instance.RecentExamples.GetReverseIterator(), // Most recent examples should be first
					m_ExamplesMap,
					OnExampleMenuItemClick);
			}
			else if (changeArg.Property == NExamplesOptions.FavoriteExamplesProperty)
			{
				// Update the Favorite Examples menu drop down
				NExamplesUiHelpers.PopulateExamplesDropDown(
					m_FavoriteExamplesDropDown,
					NExamplesOptions.Instance.FavoriteExamples.GetIterator(),
					m_ExamplesMap,
					OnExampleMenuItemClick);
			}
			else if (changeArg.Property == NExamplesOptions.ThemeSchemeProperty)
			{
				// Update selected theme
				UpdateSelectedTheme(m_ThemesDropDown.Items);
			}
			else if (changeArg.Property == NExamplesOptions.DeveloperModeProperty)
			{
				// Update developer mode
				m_DeveloperModeMenuItem.Checked = (bool)changeArg.NewValue;
			}
		}

		private void OnExampleMenuItemClick(NEventArgs arg)
		{
			if (ExampleMenuItemClick != null)
			{
				NXmlElement xmlElement = NExamplesUiHelpers.GetMenuItemExample((NMenuItem)arg.CurrentTargetNode);
				ExampleMenuItemClick(xmlElement);
			}
		}

		#endregion

		#region Event Handles - Menus

		#region Theme Menu

		/// <summary>
		/// Called when a theme menu item is clicked -> applies the specified theme
		/// </summary>
		/// <param name="arg1"></param>
		private void OnThemeMenuItemClick(NEventArgs arg1)
		{
			NCheckableMenuItem item = (NCheckableMenuItem)arg1.CurrentTargetNode;
			NUITheme theme = item.Tag as NUITheme;

			if (theme != null)
			{
				// Update example options with selected theme's scheme
				NExamplesOptions.Instance.ThemeScheme = theme.Scheme;

				// Apply the selected theme
				NApplication.ApplyTheme(theme);

				// Update the current theme menu item and check it
				m_CurrentThemeMenuItem.Checked = false;
				item.Checked = true;
				m_CurrentThemeMenuItem = item;

				// Resize the right panel if the theme is in touch mode.
				NExampleBase exampleBase = ParentNode.GetFirstDescendant<NExampleBase>();
				if (exampleBase == null)
					return;

				NSplitter splitter = exampleBase.Content as NSplitter;
				if (splitter == null)
					return;

				NSplitter exampleSplitter = splitter.Pane1.Content as NSplitter;
				if (exampleSplitter == null)
					return;

				bool touchMode = NApplication.Desktop.TouchMode;
				exampleSplitter.SplitOffset = touchMode ? 450 : 300;
			}
			else
			{
				throw new Exception("Clicked menu item not a theme?");
			}
		}

		#endregion

		#region Options Menu

		private void OnDeveloperModeMenuItemClick(NEventArgs arg)
		{
			NApplication.DeveloperMode = !NApplication.DeveloperMode;
			NExamplesOptions.Instance.DeveloperMode = NApplication.DeveloperMode;
		}

		#endregion

		#region InternalCode

		private void OnPrintMenuItemClick(NEventArgs args)
		{
			Random rand = new Random();

			NPrintDocument printDocument = new NPrintDocument();
			printDocument.DocumentName = "T:" + rand.Next().ToString();
			printDocument.BeginPrint += new Function<NPrintDocument, NBeginPrintEventArgs>(OnBeginPrint);
			printDocument.PrintPage += new Function<NPrintDocument, NPrintPageEventArgs>(OnPrintPage);
			printDocument.EndPrint += new Function<NPrintDocument, NEndPrintEventArgs>(OnEndPrint);

			NPrintDialog pd = new NPrintDialog();
			pd.EnableCustomPageRange = true;
			pd.EnableCurrentPage = true;
			pd.PrintRangeMode = ENPrintRangeMode.AllPages;
			pd.CustomPageRange = new NRangeI(1, 100);
			pd.NumberOfCopies = 1;
			pd.Collate = true;
			pd.PrintDocument = printDocument;

			pd.RequestShow();
		}
		private void OnBeginPrint(NPrintDocument sender, NBeginPrintEventArgs e)
		{
		}
		private void OnEndPrint(NPrintDocument sender, NEndPrintEventArgs e)
		{
		}
		private void OnPrintPage(NPrintDocument sender, NPrintPageEventArgs e)
		{
			NSize pageSizeDIP = new NSize(this.Width, this.Height);

			try
			{
				double clipW = e.PrintableArea.Width;
				double clipH = e.PrintableArea.Height;

				NRegion clip = NRegion.FromRectangle(new NRectangle(0, 0, clipW, clipH));
				NMatrix transform = new NMatrix(e.PrintableArea.X, e.PrintableArea.Y);

				NPaintVisitor visitor = new NPaintVisitor(e.Graphics, 300, transform, clip);

				// forward traverse the display tree
				VisitDisplaySubtree(visitor);

				e.HasMorePages = false;
			}
			catch (Exception ex)
			{
				NMessageBox.Show(null, ex.Message, "Exception", ENMessageBoxButtons.OK, ENMessageBoxIcon.Error);
			}
		}
		private void OnExportWithDirect2DMenuItemClick(NEventArgs args)
		{
#if SUPPORT_DIRECT2D && DEBUG
			string fileName = "d:\\D2D_output.png";
			NSize imgSize = new NSize(this.Width, this.Height);

			try
			{
				Nevron.Windows.DirectX.ND2DGraphicsHelper gh = new Nevron.Windows.DirectX.ND2DGraphicsHelper();
				INGraphics2D pdfG = gh.CreateGraphics((int)imgSize.Width, (int)imgSize.Height);

				NRegion clip = NRegion.FromRectangle(new NRectangle(0, 0, imgSize.Width, imgSize.Height));

				NMatrix canvasTransform = NMatrix.Identity;
				NMatrix invertedCT = canvasTransform;
				invertedCT.Invert();

				NPaintVisitor visitor = new NPaintVisitor(pdfG, 96, invertedCT, clip);
				// assign media

				// forward traverse the display tree
				visitor.BeginPainting();
				VisitDisplaySubtree(visitor);
				visitor.EndPainting();

				gh.SaveToFileAndDispose(fileName);

				System.Diagnostics.Process.Start(fileName);
			}
			catch (Exception x)
			{
				NMessageBox.Show(null, x.Message, "Exception", ENMessageBoxButtons.OK);
			}
#endif
		}

		private void OnClearBugLog(NEventArgs args)
		{
			//			NSystem.m_ErrorLog.Clear();
		}
		private void OnDumpBugLog(NEventArgs args)
		{
			/*			NTopLevelWindow wnd = new NTopLevelWindow();
						NApplication.Desktop.Windows.Add(wnd);

						NListBox listBox = new NListBox();

						listBox.MaxWidth = 500;
						listBox.MaxHeight = 500;

						NList<string> errorLog = NSystem.m_ErrorLog;

						for (int i = 0; i < errorLog.Count; i++)
						{
							listBox.Items.Add(new NListBoxItem(errorLog[i]));
						}

						wnd.Content = listBox;
						wnd.Title = "Error";
						wnd.Modal = true;
						wnd.Open();*/
		}
		/// <summary>
		/// Called when the GC Collect button is clicked - collects the garbage
		/// </summary>
		/// <param name="args"></param>
		private void OnGCCollectMenuItemClick(NEventArgs args)
		{
			GC.Collect();
		}
		/// <summary>
		/// Called when the Show Application StyleSheets is clicked - shows the NApplicaiton.
		/// </summary>
		/// <param name="arg1"></param>
		private void OnShowApplicationStyleSheetsClick(NEventArgs arg1)
		{
			NDesigner designer = NDesigner.GetDesigner(NApplication.Desktop.OwnerDocument.StyleSheets);
			NEditor editor = designer.CreateInstanceEditor(NApplication.Desktop.OwnerDocument.StyleSheets);

			NEditorWindow window = new NEditorWindow();
			window.RemoveFromParentOnClose = true;
			window.Editor = editor;

			NApplication.Desktop.Windows.Add(window);
			window.Open();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		private void OnShowRepaintAreasMenuCheckCheckedChanged(NValueChangeEventArgs args)
		{
			NApplication.ShowRepaintAreas = ((NCheckableMenuItem)args.TargetNode).Checked;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		private void OnShowPaintCacheAreasMenuCheckCheckedChanged(NValueChangeEventArgs args)
		{
			NApplication.ShowPaintCacheAreas = ((NCheckableMenuItem)args.TargetNode).Checked;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		private void OnEnablePaintCacheMenuCheckCheckedChanged(NValueChangeEventArgs args)
		{
			NApplication.PaintCacheSettings.Enabled = ((NCheckableMenuItem)args.TargetNode).Checked;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		private void OnEnableMultiThreadedPaintingCheckedChanged(NValueChangeEventArgs args)
		{
			NApplication.EnableMultiThreadedPainting = ((NCheckableMenuItem)args.TargetNode).Checked;
		}

		#endregion

		#endregion

		#region Fields

		private NMenuDropDown m_RecentExamplesDropDown;
		private NMenuDropDown m_FavoriteExamplesDropDown;
		private NMenuDropDown m_ThemesDropDown;
		private NCheckableMenuItem m_CurrentThemeMenuItem;
		private NCheckableMenuItem m_DeveloperModeMenuItem;

		private NStringMap<NWidget> m_ExamplesMap;

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NExampleHeaderLane1.
		/// </summary>
		public static readonly NSchema NExampleHeaderLane1Schema;

		#endregion
	}
}