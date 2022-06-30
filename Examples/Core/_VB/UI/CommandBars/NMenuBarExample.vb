Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NMenuBarExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NMenuBarExample.NMenuBarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NMenuBarExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_MenuBar = New Nevron.Nov.UI.NMenuBar()
            Me.m_MenuBar.Text = "My Menu"
            Me.m_MenuBar.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_MenuBar.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_MenuBar.Items.Add(Me.CreateFileMenuDropDown())
            Me.m_MenuBar.Items.Add(Me.CreateEditMenuDropDown())
            Me.m_MenuBar.Items.Add(Me.CreateViewMenuDropDown())
            Me.m_MenuBar.AddEventHandler(Nevron.Nov.UI.NMenuPopupHost.ClickEvent, New Nevron.Nov.Dom.NEventHandler(Of Nevron.Nov.Dom.NEventArgs)(New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnMenuItemClicked)))
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Direction = Nevron.Nov.Layout.ENHVDirection.TopToBottom
            stack.Add(Me.m_MenuBar)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_MenuBar), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_MenuBar, Nevron.Nov.UI.NMenuBar.OrientationProperty, Nevron.Nov.UI.NMenuBar.OpenPopupsOnMouseInProperty, Nevron.Nov.UI.NMenuBar.ClosePopupsOnMouseOutProperty)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Call Nevron.Nov.NTrace.WriteLine("Create Menu Example Controls")
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create drop down menus, nested menu items, menu separators and checkable menu items.
	The example also shows how to create a set of menu items, which behaves like a radio button group, i.e. when you click
	on one of the checkable menu items the previously checked one becomes unchecked.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateFileMenuDropDown() As Nevron.Nov.UI.NMenuDropDown
            Dim fileMenu As Nevron.Nov.UI.NMenuDropDown = Nevron.Nov.Examples.UI.NMenuBarExample.CreateMenuDropDown("File")
            Dim newMenuItem As Nevron.Nov.UI.NMenuItem = New Nevron.Nov.UI.NMenuItem("New")
            fileMenu.Items.Add(newMenuItem)
            newMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("Project"))
            newMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("Web Site"))
            newMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("File"))
            Dim openMenuItem As Nevron.Nov.UI.NMenuItem = New Nevron.Nov.UI.NMenuItem(NResources.Image_ToolBar_16x16_Open_png, "Open")
            fileMenu.Items.Add(openMenuItem)
            openMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("Project"))
            openMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("Web Site"))
            openMenuItem.Items.Add(New Nevron.Nov.UI.NMenuItem("File"))
            fileMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Save"))
            fileMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Save As..."))
            fileMenu.Items.Add(New Nevron.Nov.UI.NMenuSeparator())
            fileMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Exit"))
            Return fileMenu
        End Function

        Private Function CreateEditMenuDropDown() As Nevron.Nov.UI.NMenuDropDown
            Dim editMenu As Nevron.Nov.UI.NMenuDropDown = Nevron.Nov.Examples.UI.NMenuBarExample.CreateMenuDropDown("Edit")
            editMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Undo"))
            editMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Redo"))
            editMenu.Items.Add(New Nevron.Nov.UI.NMenuSeparator())
            editMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Cut"))
            editMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Copy"))
            editMenu.Items.Add(New Nevron.Nov.UI.NMenuItem("Paste"))
            Return editMenu
        End Function

        Private Function CreateViewMenuDropDown() As Nevron.Nov.UI.NMenuDropDown
            Dim viewMenu As Nevron.Nov.UI.NMenuDropDown = Nevron.Nov.Examples.UI.NMenuBarExample.CreateMenuDropDown("View")
            Me.m_ViewLayoutMenuItems = New Nevron.Nov.UI.NCheckableMenuItem() {New Nevron.Nov.UI.NCheckableMenuItem(Nothing, "Normal Layout", True), New Nevron.Nov.UI.NCheckableMenuItem("Web Layout"), New Nevron.Nov.UI.NCheckableMenuItem("Print Layout"), New Nevron.Nov.UI.NCheckableMenuItem("Reading Layout")}
            Dim i As Integer = 0, count As Integer = Me.m_ViewLayoutMenuItems.Length

            While i < count
                Dim viewLayoutMenuItem As Nevron.Nov.UI.NCheckableMenuItem = Me.m_ViewLayoutMenuItems(i)
                AddHandler viewLayoutMenuItem.CheckedChanging, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnViewLayoutMenuItemCheckedChanging)
                AddHandler viewLayoutMenuItem.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnViewLayoutMenuItemCheckedChanged)
                viewMenu.Items.Add(viewLayoutMenuItem)
                i += 1
            End While

            viewMenu.Items.Add(New Nevron.Nov.UI.NMenuSeparator())
            viewMenu.Items.Add(New Nevron.Nov.UI.NCheckableMenuItem(Nothing, "Task Pane", True))
            viewMenu.Items.Add(New Nevron.Nov.UI.NCheckableMenuItem(Nothing, "Toolbars", False))
            viewMenu.Items.Add(New Nevron.Nov.UI.NCheckableMenuItem(Nothing, "Ruler", True))
            Return viewMenu
        End Function

		#EndRegion

		#Region"Protected - Event Handlers"

		Private Sub OnMenuItemClicked(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim itemBase As Nevron.Nov.UI.NMenuPopupHost = CType(args.TargetNode, Nevron.Nov.UI.NMenuPopupHost)

            If TypeOf itemBase.Content Is Nevron.Nov.UI.NLabel Then
                Me.m_EventsLog.LogEvent(CType(itemBase.Content, Nevron.Nov.UI.NLabel).Text & " Clicked")
            Else
                Me.m_EventsLog.LogEvent(itemBase.ToString() & " Clicked")
            End If
        End Sub

        Private Sub OnViewLayoutMenuItemCheckedChanging(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim isChecked As Boolean = CBool(args.NewValue)
            If isChecked Then Return

			' Make sure the user is not trying to uncheck the checked item
			Dim item As Nevron.Nov.UI.NCheckableMenuItem = CType(args.TargetNode, Nevron.Nov.UI.NCheckableMenuItem)
            Dim i As Integer = 0, count As Integer = Me.m_ViewLayoutMenuItems.Length

            While i < count
                Dim currentItem As Nevron.Nov.UI.NCheckableMenuItem = Me.m_ViewLayoutMenuItems(i)
                If currentItem IsNot item AndAlso currentItem.Checked Then Return
                i += 1
            End While

            args.Cancel = True
        End Sub

        Private Sub OnViewLayoutMenuItemCheckedChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim isChecked As Boolean = CBool(args.NewValue)
            If isChecked = False Then Return
            Dim item As Nevron.Nov.UI.NCheckableMenuItem = CType(args.TargetNode, Nevron.Nov.UI.NCheckableMenuItem)
            Dim i As Integer = 0, count As Integer = Me.m_ViewLayoutMenuItems.Length

            While i < count
                Dim currentItem As Nevron.Nov.UI.NCheckableMenuItem = Me.m_ViewLayoutMenuItems(i)

                If currentItem IsNot item AndAlso currentItem.Checked Then
					' We've found the previously checked item, so uncheck it
					currentItem.Checked = False
                    Exit While
                End If

                i += 1
            End While
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_MenuBar As Nevron.Nov.UI.NMenuBar
        Private m_EventsLog As NExampleEventsLog
        Private m_ViewLayoutMenuItems As Nevron.Nov.UI.NCheckableMenuItem()

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NMenuBarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateMenuDropDown(ByVal text As String) As Nevron.Nov.UI.NMenuDropDown
            Dim menuDropDown As Nevron.Nov.UI.NMenuDropDown = New Nevron.Nov.UI.NMenuDropDown(text)
            Call Nevron.Nov.UI.NCommandBar.SetText(menuDropDown, text)
            Call Nevron.Nov.UI.NCommandBar.SetImage(menuDropDown, Nothing)
            Return menuDropDown
        End Function

		#EndRegion
	End Class
End Namespace
